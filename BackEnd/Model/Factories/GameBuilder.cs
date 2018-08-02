using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using BackEnd.Model.GameInstance;
using BackEnd.Model.Instance;
using BackEnd.Presenter;
using log4net;

namespace BackEnd.Model {
	/// <summary>
	/// Arranges a new game according to injected settings.
	/// </summary>
	internal class GameBuilder {
		private static readonly ILog Logger = Logging.GetLogger();
		private const int MAX_PICK_ITERATIONS = 10;

		/// <summary>
		/// Injected random number generator.
		/// </summary>
		private readonly Random _rng;

		/// <summary>
		/// Injected user interface.
		/// </summary>
		private readonly UIAdapter _frontEnd;

		/// <summary>
		/// All species in the system; used to pick up alternative choices.
		/// </summary>
		private readonly IEnumerable<Species> _allSpecies;

		/// <summary>
		/// Construct new instance.
		/// </summary>
		/// <param name="rng">Random number generator to use.</param>
		public GameBuilder(UIAdapter frontEnd, Random rng, IEnumerable<Species> allSpecies) {
			_rng = rng;
			_frontEnd = frontEnd;
			_allSpecies = allSpecies;

			Logger.Debug("GameFactory constructed.");
		}

		/// <summary>
		/// Build a new instance of a game.
		/// </summary>
		/// <param name="settings">Settings for the game.</param>
		/// <returns></returns>
		public GameHandler BuildGame(GameSettings settings) {
			#region Validation

			if (settings == null)
				throw new ArgumentException("Null settings.", nameof(settings));
			if (!IsGameSettingsValid(settings))
				throw new ArgumentException("Invalid settings.", nameof(settings));

			#endregion

			Logger.Debug("Building new GameInstance.");

			// Track used images, do not show same image twice.
			var usedImages = new HashSet<SpeciesImage>();
			// Track used species, allow duplicate according to configuration.
			var usedSpecies = new HashSet<Species>();

			// Fetch all species that are allowed to be picked up based on their class(es).
			IList<Species> allowedSpecies
				= _allSpecies.Where(
					species => species.Classes.Any(
						speciesClass => settings.SpeciesClasses.Contains(speciesClass)
					)
				).ToList();

			// Created game images.
			LinkedList<GameQuestion> gameImages = new LinkedList<GameQuestion>();

			Int32 imagesToPick = settings.NumberOfQuestions;
			Logger.Debug($"Picking {imagesToPick} images.");

			Int32 pickedImagesCount = 0;
			while (pickedImagesCount < imagesToPick) {
				Int32 newSpeciesIndex = _rng.Next(allowedSpecies.Count);
				Species pickedSpecies = allowedSpecies[newSpeciesIndex];

				if (!settings.AllowDuplicates
					&& usedSpecies.Contains(pickedSpecies)
					&& usedSpecies.Count < allowedSpecies.Count)
					continue;

				// Pick a preferably unused image from picked species.

				Boolean imageUsed = false;
				Int32 imagePickIteration = 0;
				SpeciesImage pickedImage;

				bool allImagesUsed = false;

				IReadOnlyList<SpeciesImage> allowedImages
					= pickedSpecies
						.Images
						.Where(psi => !usedImages.Contains(psi))
						.ToList();

				if (!allowedImages.Any()) {
					allowedImages = pickedSpecies.Images;
					allImagesUsed = true;
				}

				Int32 speciesImageCount = allowedImages.Count;
				if (speciesImageCount == 0)
					throw new InvalidOperationException($"Species {pickedSpecies.Name} has no images.");

				do {
					Int32 newImageIndex = _rng.Next(speciesImageCount);
					pickedImage = allowedImages[newImageIndex];

					if (!allImagesUsed && usedImages.Contains(pickedImage))
						imageUsed = true;

					ISet<SpeciesClass> pickedSpeciesClasses
						= pickedSpecies.Classes;

					// Get settings.NumberOfChoices alternatives to present with the correct answer.
					IEnumerable<Species> alternativeSource
						= allowedSpecies.Where(
							species => species.Classes.Any(
								speciesClass => pickedSpeciesClasses.Contains(speciesClass)
							));

					if (!settings.GetChoicesFromSameClass) {
						alternativeSource = _allSpecies;
					}

					// Shuffle.
					alternativeSource
						= alternativeSource
							.OrderBy(s => Guid.NewGuid())
							.ToList();

					IList<Species> possibleSpecies =
						alternativeSource
							.Where(species => species != pickedSpecies)
							.Take(settings.NumberOfChoices - 1)
							.Concat(new List<Species> { pickedSpecies })
							.OrderBy(s => Guid.NewGuid())
							.ToList();

					gameImages.AddLast(GetGameQuestion(pickedSpecies, possibleSpecies, pickedImage));

					// ImagePickIteration prevents loop from hanging in borderline cases.
					imagePickIteration++;
				} while (imagePickIteration < MAX_PICK_ITERATIONS && imageUsed);

				if (imagePickIteration == MAX_PICK_ITERATIONS) {
					Logger.Warn("Exceeded the iterations trying to pick unique images; reusing");
				}

				// Track picked species and images.
				usedSpecies.Add(pickedSpecies);
				usedImages.Add(pickedImage);

				pickedImagesCount++;
			}

			var gameInstance
				= new Instance.GameInstance(gameImages);

			var gameHandler
				= new GameHandler(gameInstance, _frontEnd, settings.TimePerImage);

			Logger.Info("New game built.");

			return gameHandler;
		}

		/// <summary>
		/// Check if injected game settings are valid.
		/// </summary>
		/// <param name="settings">Instance of game settings.</param>
		/// <returns>Boolean indicating if the settings are valid.</returns>
		private bool IsGameSettingsValid(GameSettings settings) {
			if (settings.NumberOfQuestions <= 1) return false;
			if (settings.NumberOfChoices <= 2) return false;
			if (settings.InputStyle == InputStyle.Unkown) return false;

			return true;
		}

		/// <summary>
		/// Create a new instance of game image, eg. a single `question`.
		/// </summary>
		/// <param name="correctSpecies">Species the image represents.</param>
		/// <param name="alternativeAnswers"></param>
		/// <param name="image">The actual image.</param>
		/// <returns></returns>
		private GameQuestion GetGameQuestion(
			Species correctSpecies, IList<Species> alternativeAnswers, SpeciesImage image)
			=> new GameQuestion(image.Image) {
				Correct = false,
				Done = false,
				CorrectAnswer
					= correctSpecies.Name.ToLower(
						CultureInfo.InvariantCulture),
				GivenAnswer = null,
				AlternativeAnswers
					= alternativeAnswers.Select(
							ans => ans.Name.ToLower(CultureInfo.InvariantCulture)
						).ToList()
			};
	}
}
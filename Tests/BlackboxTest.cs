using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BackEnd;
using BackEnd.Model;
using BackEnd.Model.Instance;
using BackEnd.Model.Utils;
using BackEnd.Presenter;
using Xunit;

namespace Tests {
	public class BlackboxTest {
		private readonly Random rng = new Random(1337);

		/// <summary>
		/// Run a full mock game and check that the status is as expected.
		/// </summary>
		/// <param name="questions">Number of questions to ask in total.</param>
		/// <param name="choices">Number of choices to give to player.</param>
		/// <returns></returns>
		[Theory]
		[InlineData(1, 3, true)]
		[InlineData(2, 3, false)]
		[InlineData(3, 3, true)]
		[InlineData(4, 2, false)]
		[InlineData(5, 3, true)]
		[InlineData(6, 4, false)]
		[InlineData(7, 5, true)]
		[InlineData(8, 6, false)]
		[InlineData(9, 7, true)]
		[InlineData(10, 8, false)]
		[InlineData(11, 9, true)]
		[InlineData(12, 10, false)]
		[InlineData(13, 11, true)]
		[InlineData(14, 12, false)]
		public async Task ShouldRunSuccessfulGame(int questions, int choices, bool fromSameClass) {
			(BackendInstance backEnd, IEnumerable<SpeciesClass> allClasses, MockUI ui)
				= CreateBackend();

			IEnumerable<Species> loadedSpecies
				= backEnd.GetLoadedSpecies();

			GameSettings settings = new GameSettings {
				NumberOfQuestions = questions,
				AllowDuplicates = false,
				SpeciesClasses = allClasses,
				NumberOfChoices = choices,
				TimePerImage = null,
				InputStyle = InputStyle.MultiChoice,
				GetChoicesFromSameClass = fromSameClass
			};

			// Start game loop
			Task gameLoop = Task.Run(() => backEnd.StartGame(settings));

			await Task.Delay(TimeSpan.FromMilliseconds(500));
			ui.Start();

			bool answerCorrect = false;
			int correctCount = 0;

			for (int i = 0; i < questions; i++) {
				await ui.WaitState();

				if (i > 0) {
					// Not first answer
					Assert.True(ui.IsNewQuestion);

					if (answerCorrect) {
						Assert.NotNull(ui.IsCorrect);
						Assert.True(ui.IsCorrect.Value);
						correctCount++;
					} else {
						Assert.NotNull(ui.IsWrong);
						Assert.True(ui.IsWrong.Value);
					}
				}

				Assert.NotNull(ui.Image);
				Assert.Contains(ui.Correct, ui.Alternatives);

				if (fromSameClass) {
					int choiceMaxLength = settings.NumberOfChoices;
					if (settings.GetChoicesFromSameClass) {
						Species correctSpecies = GetSpeciesByName(loadedSpecies, ui.Correct);
						SpeciesClass correctSpeciesClass = correctSpecies.Classes.First();
						choiceMaxLength =
							Math.Min(
								choiceMaxLength,
								loadedSpecies.Count(
									s => s.Classes.Any(sc => sc == correctSpeciesClass)
								)
							);
					}
					Assert.Equal(choiceMaxLength, ui.Alternatives.Length);

					IList<SpeciesClass> alternativeClasses
						= ui.Alternatives
							.Select(
								s => GetSpeciesByName(loadedSpecies, s))
							.Select(s => s.Classes.First()).ToList();
					Assert.Single(alternativeClasses.Distinct());
				} else {
					Assert.Equal(
						Math.Min(
							loadedSpecies.Count()
							, settings.NumberOfChoices)
						, ui.Alternatives.Length);
				}

				Assert.True(ui.Image.Width > 0);
				Assert.True(ui.Image.Height > 0);

				Assert.Null(ui.ErrorMessage);
				Assert.Null(ui.GameResult);
				Assert.False(ui.IsTimeout);
				Assert.False(ui.IsEnd);

				answerCorrect = RandomBool();

				string chosenAnswer
					= answerCorrect
						? ui.Correct
						: ui.Alternatives.Where(a => a != ui.Correct)
							.OrderBy(e => Guid.NewGuid()).First();

				ui.ClearState();
				ui.Choose(chosenAnswer);
			}

			await ui.WaitState();

			Assert.False(ui.IsNewQuestion);

			if (answerCorrect) {
				Assert.NotNull(ui.IsCorrect);
				Assert.True(ui.IsCorrect.Value);
				correctCount++;
			} else {
				Assert.NotNull(ui.IsWrong);
				Assert.True(ui.IsWrong.Value);
			}

			Assert.True(gameLoop.IsCompleted);
			Assert.False(gameLoop.IsCanceled);
			Assert.False(gameLoop.IsFaulted);

			Assert.True(ui.IsEnd);
			Assert.NotNull(ui.GameResult);
			Assert.Null(ui.ErrorMessage);

			GameResult result = ui.GameResult;
			Assert.Equal(correctCount, result.CorrectAnswers);
			Assert.Equal(questions, result.TotalAnswers);
		}

		public Species GetSpeciesByName(IEnumerable<Species> allSpecies, string name)
			=> allSpecies.Single(s => s.Name.ToLowerInvariant() == name);

		/// <returns>Random boolean value.</returns>
		public bool RandomBool() => rng.Next(2) == 0;

		/// <summary>
		/// Create UI mock to communicate with the backend.
		/// </summary>
		/// 
		/// <returns>
		/// New <see cref="BackendInstance"/> instance with related objects in a tuple.
		/// </returns>
		public (BackendInstance, IEnumerable<SpeciesClass>, MockUI ui) CreateBackend() {
			MockUI ui = new MockUI();
			BackendInstance backEnd = BackendInstance.GetInstance(ui, rng);

			IList<Species> loadedSpecies
				= backEnd.GetLoadedSpecies().ToList();

			Assert.NotEmpty(loadedSpecies);

			IEnumerable<SpeciesClass> allClasses
				= SpeciesUtils.GetSpeciesClasses(loadedSpecies);

			Assert.NotEmpty(allClasses);

			return (backEnd, allClasses, ui);
		}
	}

	public class MockUI : UIAdapter {
		public bool IsError;
		public bool IsEnd;
		public bool IsNewQuestion;
		public bool? IsCorrect;
		public bool? IsWrong;
		public bool IsTimeout;

		public string ErrorMessage;
		public GameResult GameResult;

		public Image Image;
		public String[] Alternatives;
		public String Correct;

		private volatile bool _stateUpdated;

		public override void OnError(string error) {
			IsError = true;
			ErrorMessage = error;

			_stateUpdated = true;
		}

		public override void OnProgress(Int32 percent) {
			throw new NotImplementedException();
		}

		public override void OnNewQuestion(Image image, String[] alternatives, String correct) {
			Image = image;
			Alternatives = alternatives;
			Correct = correct;
			IsNewQuestion = true;

			_stateUpdated = true;
		}

		public override void OnEnd(GameResult result) {
			GameResult = result;
			IsEnd = true;

			_stateUpdated = true;
		}

		public override void OnAnswerCorrect() {
			IsCorrect = true;

			_stateUpdated = true;
		}

		public override void OnAnswerWrong(string correct) {
			IsWrong = true;

			_stateUpdated = true;
		}

		public override void OnTimeout() {
			IsTimeout = true;

			_stateUpdated = true;
		}

		public void ClearState() {
			IsCorrect = false;
			IsWrong = false;
			IsEnd = false;
			IsError = false;
			IsTimeout = false;
			IsNewQuestion = false;
			_stateUpdated = false;
		}

		public void Start() => InvokeOnStart();
		public void Choose(string input) => InvokeOnChoice(input);

		public async Task WaitState() {
			DateTime start = DateTime.UtcNow;
			TimeSpan timeout = TimeSpan.FromMilliseconds(500);

			while (!_stateUpdated) {
				await Task.Delay(50);

				if (DateTime.UtcNow - start > timeout)
					throw new TimeoutException("MockUI state did not update");
			}
		}
	}
}
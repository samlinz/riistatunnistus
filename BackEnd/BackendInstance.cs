using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Model;
using BackEnd.Model.Factories;
using BackEnd.Model.Instance;
using BackEnd.Presenter;
using log4net;
using Xunit.Sdk;

namespace BackEnd {
	public class BackendInstance {
		private static readonly ILog Logger = Logging.GetLogger();

		/*
		 * Backend state
		 */
		private readonly IEnumerable<Species> _species;
		private readonly UIAdapter _ui;
		private readonly Random _rng;

		internal BackendInstance(UIAdapter ui, IEnumerable<Species> species, Random rng) {
			_species = species;
			_ui = ui;
			_rng = rng;
		}

		public IEnumerable<Species> GetLoadedSpecies()
			=> _species.ToList();

		public void StartGame(GameSettings settings) {
			GameBuilder gameBuilder = new GameBuilder(_ui, _rng, _species);
			GameHandler gameHandler = gameBuilder.BuildGame(settings);
			try {
				gameHandler.Process();
			} catch (Exception ex) {
				Logger.Fatal($"Received error from GameHandler: {ex.Message}");
				_ui.OnError(ex.Message);
			}
		}

		public static BackendInstance GetInstance(UIAdapter ui, Random rng = null) {
			Logger.Debug("Creating backend instance.");

			SpeciesFileDescription speciesFileDescription = FileSystemService.GetSpeciesMappings();

			// Map species classes to list of species
			IEnumerable<(SpeciesClass, IList<SpeciesMapping>)> speciesClassMappings =
				speciesFileDescription
					.SpeciesClassMappings
					.Select(classMapping => (
							SpeciesFactory.GetSpeciesClass(classMapping)
							, classMapping.SpeciesClassSpeciesMappings))
					.ToList();

			IEnumerable<Species> species
				= speciesClassMappings
					.AsParallel()
					.SelectMany(tuple => {
						SpeciesClass parentClass = tuple.Item1;
						IList<SpeciesMapping> speciesMappings = tuple.Item2;

						IEnumerable<Species> childrenSpecies
							= new List<Species>(speciesMappings.Count);

						childrenSpecies
							.Concat(speciesMappings.Select(
								s => SpeciesFactory.GetSpecies(s, parentClass)));

						return childrenSpecies;
					}).ToList();

			Random rngToUse = rng ?? new Random();

			BackendInstance instance = new BackendInstance(ui, species, rngToUse);

			Logger.Debug("Species loaded and models created.");

			return instance;
		}
	}
}
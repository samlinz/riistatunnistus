using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BackEnd;
using BackEnd.Model;
using BackEnd.Model.Utils;
using log4net;

namespace RiistaTunnistusOhjelma {
	/// <summary>
	/// Main class and entry point for the program.
	/// </summary>
	static class Program {
		private static readonly ILog Logger
			= Logging.GetLogger();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Logger.Info("Starting Win32 frontend.");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			SignalHandler globalSignalHandler = new SignalHandler();

			// Initialize UI handler.
			UIAdapterImplementation winUi
				= new UIAdapterImplementation(globalSignalHandler);

			// Initialize rng.
			Random rng = new Random();

			// Create game logic instance.
			BackendInstance backend
				= BackendInstance.GetInstance(winUi, rng);

			IEnumerable<Species> loadedSpecies
				= backend.GetLoadedSpecies();
			IEnumerable<SpeciesClass> loadedSpeciesClasses
				= SpeciesUtils.GetSpeciesClasses(loadedSpecies);

			// Initialize default settings.
			GameSettings settings = new GameSettings {
				InputStyle = InputStyle.MultiChoice,
				NumberOfChoices = 3,
				GetChoicesFromSameClass = true,
				NumberOfQuestions = 10,
				AllowDuplicates = false,
				TimePerImage = null,
				SpeciesClasses = loadedSpeciesClasses
			};

			string[] infoText = GetInfoText(loadedSpecies, loadedSpeciesClasses);

			var startForm
				= new RiistaTunnistusOhjelma(
						globalSignalHandler
						, backend
						, winUi
						, settings
						, infoText
					);

			Application.Run(startForm);
		}

		private static string[] GetInfoText(IEnumerable<Species> species
			, IEnumerable<SpeciesClass> speciesClasses) {
			int speciesCount = species.Count();
			int speciesClassCount = speciesClasses.Count();
			int imageCount = species.Select(s => s.ImageCount).Sum();

			var assembly = Assembly.GetExecutingAssembly();
			var version = assembly.GetName().Version;

			const string appName = "RiistaTunnistusOhjelma";

			string[] infoText = {
				$"{appName}"
				, $@"Versio: {version.Major}.{version.Minor}"
				, $"Revisio: {version.Revision}"
				, $"Build: {version.Build}"
				, $"Lajeja: {speciesCount}"
				, $"Lajiluokkia: {speciesClassCount}"
				, $"Kuvia yhteensä: {imageCount}"
			};

			return infoText;
		}
	}
}

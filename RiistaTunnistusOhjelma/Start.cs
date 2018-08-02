using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;
using BackEnd.Model;
using BackEnd.Presenter;
using log4net;

namespace RiistaTunnistusOhjelma {
	public partial class RiistaTunnistusOhjelma : Form {
		private static readonly ILog Logger = Logging.GetLogger();

		private readonly GameSettings _settings;
		private readonly SignalHandler _signalHandler;
		private readonly BackendInstance _backend;
		private readonly UIAdapterImplementation _ui;

		private readonly IEnumerable<SpeciesClass> _allSpeciesClasses;

		/// <summary>
		/// Create the main form.
		/// </summary>
		/// <param name="signalHandler">Injected signal handler.</param>
		/// <param name="backend">Injected game logic.</param>
		/// <param name="uiAdapter">UI Adapter used to communicate with backend.</param>
		/// <param name="settings">Initial settings.</param>
		/// <param name="infoText"></param>
		internal RiistaTunnistusOhjelma(
			SignalHandler signalHandler
			, BackendInstance backend
			, UIAdapterImplementation uiAdapter
			, GameSettings settings
			, string[] infoText) {
			_signalHandler = signalHandler;
			_backend = backend;
			_settings = settings;
			_ui = uiAdapter;

			_allSpeciesClasses = _settings.SpeciesClasses;

			signalHandler.FatalError += (sender, s) => Exit();

			InitializeComponent();
			InitializeControlValues(settings, infoText);
		}

		private void InitializeControlValues(GameSettings settings, string[] infoText) {
			switch (settings.InputStyle) {
				case InputStyle.MultiChoice:
					settingsMultichoice.Checked = true;
					settingsFreeInput.Checked = false;
					break;
				case InputStyle.FreeForm:
					settingsMultichoice.Checked = false;
					settingsFreeInput.Checked = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			settingsAllowDuplicates.Checked = settings.AllowDuplicates;
			settingsFromSameClass.Checked = settings.GetChoicesFromSameClass;
			settingsChoices.Value = settings.NumberOfChoices;
			settingsQuestions.Value = settings.NumberOfQuestions;
			settingsTimeout.Value = settings.TimePerImage?.Seconds ?? 0;

			foreach (SpeciesClass speciesClass in settings.SpeciesClasses) {
				settingsClasses.Items.Add(speciesClass.Name, true);
			}

			infoBox.Clear();
			bool firstLine = true;
			foreach (string s in infoText) {
				infoBox.AppendText($"{(firstLine ? "" : "\r\n")}{s}");
				firstLine = false;
			}

			startGameButton.Focus();
		}
		/// <summary>
		/// Close the main form programmatically.
		/// <remarks>This causes the application to end.</remarks>
		/// </summary>
		internal void Exit() {
			Logger.Info("Closing the main form.");
			ChangeUI(Close);
		}

		/*
		 * User input handlers.
		 */

		private void startGameButton_Click(object sender, EventArgs e) {
			Logger.Info("Start game clicked.");

			// Start game logic in another task.
			Task.Run(() => _backend.StartGame(_settings));

			// Create and show the game form.
			Game gameForm = new Game(_settings, _ui) {
				Location = Location,
				StartPosition = FormStartPosition.Manual
			};

			gameForm.FormClosing += (o, args) => Show();
			gameForm.Show();
			Hide();
		}

		private void clearDataButton_Click(object sender, EventArgs e) {
			throw new NotImplementedException("fug");
		}

		private void settingsTimeout_ValueChanged(object sender, EventArgs e) {
			int newTimeout = (int)settingsTimeout.Value;
			_settings.TimePerImage =
				newTimeout == 0
				? (TimeSpan?) null
				: TimeSpan.FromSeconds(newTimeout);

			Logger.Info($"Timeout changed to {_settings.TimePerImage}.");
		}

		private void settingsQuestions_ValueChanged(object sender, EventArgs e) {
			_settings.NumberOfQuestions = (int) settingsQuestions.Value;
			Logger.Info($"NumberOfQuestions changed to {_settings.NumberOfQuestions}.");
		}

		private void settingsChoices_ValueChanged(object sender, EventArgs e) {
			_settings.NumberOfChoices = (int)settingsChoices.Value;
			Logger.Info($"NumberOfChoices changed to {_settings.NumberOfChoices}.");
		}

		private void settingsFromSameClass_CheckedChanged(object sender, EventArgs e) {
			_settings.GetChoicesFromSameClass
				= settingsFromSameClass.CheckState == CheckState.Checked;
			Logger.Info($"GetChoicesFromSameClass changed to {_settings.GetChoicesFromSameClass}.");
		}

		private void settingsAllowDuplicates_CheckedChanged(object sender, EventArgs e) {
			_settings.AllowDuplicates
				= settingsAllowDuplicates.Checked;
			Logger.Info($"AllowDuplicates changed to {_settings.AllowDuplicates}.");
		}

		private void settingsMultichoice_CheckedChanged(object sender, EventArgs e) {
			if (!settingsMultichoice.Checked) return;

			_settings.InputStyle = InputStyle.MultiChoice;
			Logger.Info($"InputStyle changed to {InputStyle.MultiChoice}.");
		}

		private void settingsFreeInput_CheckedChanged(object sender, EventArgs e) {
			if (!settingsFreeInput.Checked) return;

			_settings.InputStyle = InputStyle.FreeForm;
			Logger.Info($"InputStyle changed to {InputStyle.FreeForm}.");
		}

		private void settingsClasses_MouseUp(object sender, MouseEventArgs e) {

			IList<SpeciesClass> selectedClasses = new List<SpeciesClass>();
			foreach (string className in settingsClasses.CheckedItems) {
				SpeciesClass speciesClass
					= _allSpeciesClasses.Single(sc => sc.Name == className);
				selectedClasses.Add(speciesClass);
			}

			_settings.SpeciesClasses = selectedClasses;

			Logger.Info($"Selected classes changed. Selected classes: {selectedClasses.Count}");
		}



		/// <summary>
		/// Modify UI from another thread.
		/// </summary>
		/// <param name="action">Action to run in UI thread.</param>
		private void ChangeUI(Action action) {
			if (InvokeRequired) {
				Invoke(action);
			} else {
				action();
			}
		}
	}
}

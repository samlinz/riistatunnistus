using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;
using BackEnd.Model.Instance;
using log4net;
using log4net.Repository.Hierarchy;

namespace RiistaTunnistusOhjelma {
	/// <summary>
	/// Form displaying running game with choices and images.
	/// </summary>
	public partial class Game : Form {
		private static readonly ILog Logger = Logging.GetLogger();

		private const int ProgressBarMax = 100;

		// State.
		private UserControl _choiceControl;
		private InputStyle _inputStyle;

		// Injected dependecies.
		private readonly GameSettings _settings;
		private readonly UIAdapterImplementation _ui;

		private bool _started = false;

		/// <summary>
		/// Construct <see cref="Game"/> form.
		/// </summary>
		/// <param name="settings">Injected game settings.</param>
		/// <param name="ui">Injected UI adapter.</param>
		internal Game(GameSettings settings, UIAdapterImplementation ui) {
			_settings = settings;
			_ui = ui;

			_ui.RegisterUserInterface(this);

			InitializeComponent();
			Logger.Debug($"{nameof(Game)} initialized.");

			if (settings.TimePerImage == null) {
				progressBar1.Hide();
			} else {
				progressBar1.Minimum = 0;
				progressBar1.Maximum = ProgressBarMax;
			}

			// Signal the logic to start the game with a tiny delay.
			Task.Delay(500).ContinueWith(t => _ui.InvokeOnStart());
		}

		/// <summary>
		/// Received new question; render new image and controls.
		/// </summary>
		/// <param name="image">Image data.</param>
		/// <param name="alternatives">Alternative answers.</param>
		internal void RenderQuestion(Image image, IList<string> alternatives) {
			Logger.Info("Rendering new question.");

			_started = true;

			// Display new image.
			speciesImage.Image = image;
			speciesImage.SizeMode = PictureBoxSizeMode.StretchImage;

			ChangeUI(() => {
				switch (_settings.InputStyle) {
					case InputStyle.Unkown:
						throw new InvalidOperationException($"{nameof(_settings.InputStyle)} is invalid.");
					case InputStyle.MultiChoice:
						InitializeMultiChoice(alternatives);
						break;
					case InputStyle.FreeForm:
						InitializeFreeForm();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			});
		}

		internal void SetTimeoutProgress(int percent) {
			if (!_started) return;

			ChangeUI(() => {
				progressBar1.Value = percent;
			});
		}

		/// <summary>
		/// Handle exit back to the main form.
		/// </summary>
		internal void ExitGame() {
			Logger.Info($"Closing the {nameof(Game)} form.");

			_ui.InvokeOnExit();
			_ui.UnregisterUserInterface(this);
			Close();
		}

		/// <summary>
		/// Handle game over situation.
		/// </summary>
		/// <param name="finalResult"></param>
		internal void GameEnded(GameResult finalResult) {
			string text = $"Sait {finalResult.CorrectAnswers}"
						+ $" oikein {finalResult.TotalAnswers} kysymyksestä.";

			MessageBox.Show(text, "Peli ohi!", MessageBoxButtons.OK, MessageBoxIcon.Information);

			_ui.UnregisterUserInterface(this);
			Close();
		}

		internal void WrongAnswer(string correct) {
			string text = $"Oikea vastaus oli {correct.ToUpperInvariant()}";
			MessageBox.Show(text, "Väärä vastaus!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		/// <summary>
		/// Initialize multi-choice buttons.
		/// </summary>
		/// <param name="choices"></param>
		private void InitializeMultiChoice(IList<string> choices) {
			if (_choiceControl != null) {
				choicePanel.Controls.Remove(_choiceControl);
				_choiceControl.Dispose();
			}

			var control = new MultiChooseControl(choices) {
				Dock = DockStyle.Fill
			};
			control.Chosen += (sender, s) => OnChoice(s);
			control.Dock = DockStyle.Fill;

			choicePanel.Controls.Add(control);
			_choiceControl = control;
		}

		/// <summary>
		/// Initialize free form text field.
		/// </summary>
		private void InitializeFreeForm() {
			if (_choiceControl != null) {
				choicePanel.Controls.Remove(_choiceControl);
				_choiceControl.Dispose();
			}

			var control = new InputControl {
				Dock = DockStyle.Fill
			};
			control.Dock = DockStyle.Fill;
			control.Submit += (sender, s) => OnChoice(s);

			choicePanel.Controls.Add(control);
			_choiceControl = control;
		}

		/// <summary>
		/// Exit button clicked.
		/// </summary>
		/// 
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitButton_Click(object sender, EventArgs e) {
			Logger.Info("Exit button clicked.");
			ExitGame();
		}

		/// <summary>
		/// Handle choice made in subcontrol.
		/// </summary>
		/// 
		/// <param name="input">Made choice as string.</param>
		internal void OnChoice(string input) {
			Logger.Info($"Choosing {input}");
			_ui.InvokeOnChoice(input);
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

		private void Game_KeyUp(object sender, KeyEventArgs e) {
			if (!_choiceControl.Focused) {
				Keys pressedKey = e.KeyCode;
				if (pressedKey >= Keys.D1 && pressedKey < Keys.D9) {
					MultiChooseControl multiControl
						= _choiceControl as MultiChooseControl;
					if (multiControl == null) return;

					int order = pressedKey - Keys.D0;
					multiControl.InvokeClickHandlerByOrder(order);
				}
			}
		}
	}
}

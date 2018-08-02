using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BackEnd;
using BackEnd.Model.Instance;
using BackEnd.Presenter;
using log4net;

namespace RiistaTunnistusOhjelma {
	internal class UIAdapterImplementation : UIAdapter {
		private static readonly ILog Logger = Logging.GetLogger();

		// Injected dependencies.
		private readonly SignalHandler _signalHandler;
		private Game _ui;

		internal UIAdapterImplementation(SignalHandler signalHandler) {
			_signalHandler = signalHandler;
		}

		public override void OnProgress(int progress) {
			if (!uiRegistered) return;
			//Logger.Debug($"Timeout progress {progress}");
			_ui.SetTimeoutProgress(progress);
		}

		public override void OnError(String error) {
			Logger.Error($"Received error; {error}");

			// Show message box.
			MessageBox.Show(
				$"Ohjelma koki virhetilanteen ja sammutetaan. Virhe: {error}"
				, "Virhetilanne!"
				, MessageBoxButtons.OK
				, MessageBoxIcon.Error);

			// All errors are fatal.
			_signalHandler.OnFatalError(error);
		}

		public override void OnNewQuestion(Image image, String[] alternatives, String correct) {
			if (!uiRegistered) return;
			Logger.Info("New question");
			_ui.RenderQuestion(image, alternatives.ToList());
		}

		public override void OnEnd(GameResult finalResult) {
			if (!uiRegistered) return;
			Logger.Info("Game ended");
			_ui.GameEnded(finalResult);
		}

		public override void OnAnswerCorrect() {
			if (!uiRegistered) return;
			Logger.Info("Correct answer!");
		}

		public override void OnAnswerWrong(string correct) {
			if (!uiRegistered) return;
			Logger.Info("Wrong answer!");
			_ui.WrongAnswer(correct);
		}

		public override void OnTimeout() {
			if (!uiRegistered) return;
			Logger.Info("Timeout!");
		}

		internal void RegisterUserInterface(Game ui) {
			if (_ui != null)
				throw new InvalidOperationException($"{nameof(ui)} was still reigstered.");

			uiRegistered = true;
			_ui = ui;
		}

		internal void UnregisterUserInterface(Game ui) {
			if (_ui == null)
				throw new InvalidOperationException($"{nameof(ui)} was not reigstered.");
			if (_ui != ui)
				throw new InvalidOperationException($"{nameof(ui)} did not match the argument.");

			uiRegistered = false;
			_ui = null;
		}
	}
}

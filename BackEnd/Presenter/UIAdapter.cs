using System;
using System.Drawing;
using BackEnd.Model.Instance;

namespace BackEnd.Presenter {
	/// <summary>
	/// Adapter implemented by UI implementation.
	/// </summary>
	public abstract class UIAdapter {
		public delegate void OnChoiceHandler(string input);

		// Backend -> Frontend
		public abstract void OnError(string error);
		public abstract void OnNewQuestion(Image image, string[] alternatives, string correct);
		public abstract void OnEnd(GameResult finalResult);
		public abstract void OnAnswerCorrect();
		public abstract void OnAnswerWrong();
		public abstract void OnTimeout();

		// Frontend -> Backend
		public event OnChoiceHandler OnChoice;
		public event Action OnStart;
	}
}
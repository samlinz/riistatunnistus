using System;
using System.Drawing;
using BackEnd.Model.Instance;

namespace BackEnd.Presenter {
	/// <summary>
	/// Adapter implemented by UI implementation.
	/// </summary>
	public abstract class UIAdapter {
		protected volatile bool uiRegistered;

		public delegate void OnChoiceHandler(string input);

		// Backend -> Frontend
		public abstract void OnError(string error);
		public abstract void OnNewQuestion(Image image, string[] alternatives, string correct);
		public abstract void OnEnd(GameResult finalResult);
		public abstract void OnAnswerCorrect();
		public abstract void OnAnswerWrong(string correct, string[] hints);
		public abstract void OnTimeout();
		public abstract void OnProgress(int percent);

		// Frontend -> Backend
		public event OnChoiceHandler OnChoice;
		public event Action OnStart;
		public event Action OnExit;

		public virtual void InvokeOnExit() {
			OnExit?.Invoke();
		}
		public virtual void InvokeOnStart() {
			OnStart?.Invoke();
		}
		public virtual void InvokeOnChoice(string input) {
			OnChoice?.Invoke(input);
		}
	}
}
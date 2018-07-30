using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Model.GameInstance;
using BackEnd.Presenter;
using log4net;

namespace BackEnd.Model.Instance {
	/// <summary>
	/// Object which handles the communication between UI and GameInstance.
	/// Pulls new state, passes input and invokes event handlers.
	/// </summary>
	internal class GameHandler : IDisposable {
		private static readonly ILog Logger = Logging.GetLogger();

		private readonly UIAdapter _frontEnd;
		private readonly GameInstance _gameInstance;

		private readonly AutoResetEvent _eventSignal = new AutoResetEvent(false);
		private Object _lastArgument;

		private Task timeoutTask;
		private readonly TimeSpan? _timeoutDelay;

		/// <summary>
		/// Construct game handler instance.
		/// </summary>
		/// <param name="gameInstance">Injected game logic instance.</param>
		/// <param name="frontEnd">Injected UI.</param>
		/// <param name="timeout">Time to answer a question. Null if no limit.</param>
		public GameHandler(GameInstance gameInstance, UIAdapter frontEnd, TimeSpan? timeout) {
			_gameInstance = gameInstance;
			_frontEnd = frontEnd;
			_timeoutDelay = timeout;

			// Attach event handlers
			QuestionTimeout += OnTimeout;
			QuestionTimeout += frontEnd.OnTimeout;

			AnswerCorrect += frontEnd.OnAnswerCorrect;
			AnswerWrong += frontEnd.OnAnswerWrong;

			_frontEnd.OnChoice += OnChoiceFromFrontend;
			_frontEnd.OnStart += OnStarted;
		}

		// Events
		internal event Action QuestionTimeout;
		internal event Action AnswerCorrect;
		internal event Action AnswerWrong;

		/// <summary>
		/// Handle choice made from frontend.
		/// </summary>
		/// <param name="input">Input from frontend.</param>
		private void OnChoiceFromFrontend(string input) {
			if (String.IsNullOrEmpty(input)) {
				string err = "Null choice.";
				Logger.Error(err);
				_frontEnd.OnError(err);
				return;
			}

			_eventSignal.Set();
		}

		/// <summary>
		/// Handle startup event from frontend.
		/// </summary>
		private void OnStarted() {
			Logger.Info("GameHandler starting.");
			_eventSignal.Set();
		}

		/// <summary>
		///	Handle timeout.
		/// </summary>
		private void OnTimeout() {
			_eventSignal.Set();
		}

		/// <summary>
		/// Task which invokes QuestionTimeout event after a given interval,
		/// has it not been cancelled by an answer before.
		/// </summary>
		/// <param name="ct">CancellationToken to cancel timeout.</param>
		/// <returns></returns>
		private async Task Timeout(CancellationToken ct) {
			await Task.Delay(_timeoutDelay.Value, ct);
			if (ct.IsCancellationRequested)
				return;

			Logger.Info("Timeout!");
			_lastArgument = "timeout" + Guid.NewGuid();

			QuestionTimeout();
		}

		/// <summary>
		/// Start game loop.
		/// </summary>
		public void Process() {
			Boolean running = true;
			GameState _currentState = GameState.NotStarted;
			GameQuestion currentQuestion = null;
			CancellationTokenSource cts = new CancellationTokenSource();

			while (running) {
				if (_timeoutDelay.HasValue) {
					timeoutTask = Timeout(cts.Token);
				}

				// Wait for a signal
				_eventSignal.WaitOne();

				// Update state with input
				GameQuestion previousQuestion = currentQuestion;
				(_currentState, currentQuestion)
					= _gameInstance.GetNext(_currentState, _lastArgument);
				_lastArgument = null;

				// Cancel timeout
				if (_timeoutDelay.HasValue && !timeoutTask.IsCompleted) {
					cts.Cancel();
				}

				if (previousQuestion.Done) {
					// Call UI to inform the user about answer correctness.
					if (previousQuestion.Correct)
						AnswerCorrect();
					else AnswerWrong();
				} else {
					throw new InvalidOperationException("Previous question was not marked as done.");
				}

				// Handle new state
				switch (_currentState) {
					case GameState.Unkown:
						throw new InvalidOperationException("Unkown state.");
					case GameState.NotStarted:
						throw new InvalidOperationException("Game was unable to start.");
					case GameState.InGame:
						_frontEnd.OnNewQuestion(
							currentQuestion.ImageData.Value
							, currentQuestion.AlternativeAnswers.ToArray()
							, currentQuestion.CorrectAnswer);
						break;
					case GameState.Finished:
						Logger.Info("Game finished.");
						running = false;

						GameResult result = _gameInstance.GetFinalResult();
						_frontEnd.OnEnd(result);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(_currentState));
				}

				_lastArgument = null;
			}
		}

		public void Dispose() {
			// Detach events
			QuestionTimeout -= OnTimeout;
			QuestionTimeout -= _frontEnd.OnTimeout;

			AnswerCorrect -= _frontEnd.OnAnswerCorrect;
			AnswerWrong -= _frontEnd.OnAnswerWrong;

			timeoutTask?.Dispose();
		}
	}
}
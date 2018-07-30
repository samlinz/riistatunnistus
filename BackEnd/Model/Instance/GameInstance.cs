using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BackEnd.Model.GameInstance;
using log4net;

namespace BackEnd.Model.Instance {
	/// <summary>
	/// Handles the state of a single game.
	/// </summary>
	public class GameInstance {
		private static readonly ILog Logger = Logging.GetLogger();

		private readonly Guid _guid = Guid.NewGuid();
		private readonly Queue<GameQuestion> _imagesDone = new Queue<GameQuestion>();

		private readonly Queue<GameQuestion> _imagesInQueue = new Queue<GameQuestion>();

		// Current displayed question
		private GameQuestion _currentQuestion;

		// Has the game instance been started
		private bool _started;

		// Construct a game instance
		internal GameInstance(IEnumerable<GameQuestion> questions) {
			// Fill the queue
			foreach (GameQuestion gameImage in questions)
				_imagesInQueue.Enqueue(gameImage);

			Logger.Debug($"GameInstance {_guid.ToString()} created");
		}

		/// <summary>
		/// Dequeue new question.
		/// </summary>
		/// <returns>True if questions have ended.</returns>
		private bool NextQuestion() {
			_imagesDone.Enqueue(_currentQuestion);
			if (_imagesInQueue.Count == 0) {
				Logger.Info("All questions done.");
				return true;
			}

			Logger.Info("New question.");
			_currentQuestion = _imagesInQueue.Dequeue();

			return false;
		}

		/// <summary>
		/// Handle state according to previous state and given input.
		/// </summary>
		/// <param name="previousState"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		internal (GameState state, GameQuestion gameImage) GetNext(GameState previousState, Object input) {
			switch (previousState) {
				case GameState.Unkown:
					throw new InvalidOperationException("Unkown state.");
				case GameState.NotStarted:
					Logger.Info("Starting game.");
					return GetNext(GameState.InGame, null);
				case GameState.InGame:
					if (input == null) {
						if (_started)
							throw new InvalidOperationException("Null input.");
						_started = true;

						Logger.Info("Popping first question.");

						// New game
						return NextQuestion()
							? throw new InvalidOperationException("No questions in game.")
							: (GameState.InGame, _currentQuestion);
					}

					if (!(input is String choice))
						throw new InvalidOperationException($"Invalid choice {input}");

					if (_currentQuestion == null)
						throw new InvalidOperationException($"Invalid current image {_currentQuestion}");

					String expectedAnswer
						= _currentQuestion.CorrectAnswer.ToLower(CultureInfo.InvariantCulture).Trim();

					String givenAnswer
						= choice.ToLower().Trim();

					if (expectedAnswer == givenAnswer) {
						Logger.Info("Correct answer.");
						_currentQuestion.Correct = true;
					} else {
						Logger.Info($"Wrong answer. Expected `{expectedAnswer}`, got `{givenAnswer}`");
						_currentQuestion.Correct = false;
					}

					_currentQuestion.GivenAnswer = givenAnswer;
					_currentQuestion.Done = true;

					// Pop a new question if queue is not empty.
					return NextQuestion()
						? (GameState.Finished, null)
						: (GameState.InGame, _currentQuestion);

					// Start game
				case GameState.Finished:
					throw new InvalidOperationException("Game is finished.");
				default:
					throw new ArgumentOutOfRangeException(nameof(previousState), previousState, null);
			}
		}

		public bool IsGameFinished() {
			if (_imagesInQueue.Count > 0)
				return false;
			if (_imagesDone.Any(img => !img.Done))
				return false;

			return true;
		}

		public GameResult GetFinalResult() {
			if (!IsGameFinished())
				throw new InvalidOperationException("Game is not finished yet");

			GameResult result = new GameResult {
				CorrectAnswers = _imagesDone.Count(img => img.Correct),
				TotalAnswers = _imagesDone.Count
			};

			return result;
		}
	}

	public enum GameState {
		Unkown = 0,
		NotStarted = 1,
		InGame = 2,
		Finished = 3
	}
}
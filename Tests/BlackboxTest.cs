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
		[Fact]
		public void RunBlackbox() {
			MockUI ui = new MockUI();
			Random rng = new Random(1337);

			BackendInstance backEnd = BackendInstance.GetInstance(ui, rng);

			IList<Species> loadedSpecies
				= backEnd.GetLoadedSpecies().ToList();

			Assert.NotEmpty(loadedSpecies);

			IEnumerable<SpeciesClass> allClasses
				= SpeciesUtils.GetSpeciesClasses(loadedSpecies);

			Assert.NotEmpty(allClasses);

			GameSettings settings = new GameSettings {
				NumberOfQuestions = 3,
				AllowDuplicates = false,
				SpeciesClasses = allClasses,
				NumberOfChoices = 3,
				TimePerImage = null,
				InputStyle = InputStyle.MultiChoice,
				GetChoicesFromSameClass = true
			};
		}
	}

	public class MockUI : UIAdapter {
		public bool IsError;
		public bool IsEnd;
		public bool IsNewQuestion;
		public bool IsCorrect;
		public bool IsWrong;
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

		public override void OnNewQuestion(Image image, String[] alternatives, String correct) {
			Image = image;
			Alternatives = alternatives;
			Correct = correct;

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

		public override void OnAnswerWrong() {
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

		public async Task WaitState() {
			DateTime start = DateTime.UtcNow;
			TimeSpan timeout = TimeSpan.FromMilliseconds(500);

			while (!_stateUpdated) {
				await Task.Delay(5);

				if (DateTime.UtcNow - start > timeout)
					throw new TimeoutException("MockUI state did not update");
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace BackEnd.Model.GameInstance {
	/// <summary>
	/// Store the state of a single image question in a single game.
	/// </summary>
	public class GameQuestion {
		/// <summary>
		/// Image data.
		/// </summary>
		public Lazy<Image> ImageData { get; }

		/// <summary>
		/// Has the image been shown already.
		/// </summary>
		public bool Done { get; set; }

		/// <summary>
		/// Was the answer correct.
		/// </summary>
		public bool Correct { get; set; }

		/// <summary>
		/// Hints for the species.
		/// </summary>
		public string[] Hints { get; set; }

		/// <summary>
		/// Expected answer.
		/// </summary>
		public string CorrectAnswer { get; set; }

		/// <summary>
		/// All alternatives presented to user.
		/// </summary>
		public IList<string> AlternativeAnswers { get; set; }

		/// <summary>
		/// Attempted answer.
		/// </summary>
		public String GivenAnswer { get; set; }

		public GameQuestion(Lazy<Image> imageData) {
			ImageData = imageData;
		}
	}
}

using System;
using System.Collections.Generic;
using BackEnd.Model;

namespace BackEnd {
	/// <summary>
	///	Represents configuration for a single game.
	/// </summary>
	public class GameSettings {
		public IEnumerable<SpeciesClass> SpeciesClasses { get; set; }
		public TimeSpan? TimePerImage { get; set; }
		public InputStyle InputStyle { get; set; }
		public Int32 NumberOfQuestions { get; set; }
		public Int32 NumberOfChoices { get; set; }
		public Boolean AllowDuplicates { get; set; }
		public Boolean GetChoicesFromSameClass { get; set; }
	}

	public enum InputStyle {
		Unkown = 0,
		MultiChoice = 1,
		FreeForm = 2
	}
}
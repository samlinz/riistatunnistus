using System;
using System.Collections.Generic;

namespace BackEnd.Model {
	public class Species {
		internal IReadOnlyList<SpeciesImage> Images { get; }
		internal String Name { get; }
		internal ISet<SpeciesClass> Classes { get; }

		public Species(IReadOnlyList<SpeciesImage> images, String name, ISet<SpeciesClass> classes) {
			Images = images;
			Name = name;
			Classes = classes;
		}
	}
}

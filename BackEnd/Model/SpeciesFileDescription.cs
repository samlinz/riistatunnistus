using System;
using System.Collections.Generic;

namespace BackEnd.Model {
	internal class SpeciesFileDescription {
		internal IList<SpeciesClassMapping> SpeciesClassMappings { get; set; }
	}

	internal class SpeciesClassMapping {
		public String SpeciesClassName { get; set; }
		public IList<SpeciesMapping> SpeciesClassSpeciesMappings { get; set; }
	}

	internal class SpeciesMapping {
		public String SpeciesName { get; set; }
		public String SpeciesImageDirectory { get; set; }
	}
}
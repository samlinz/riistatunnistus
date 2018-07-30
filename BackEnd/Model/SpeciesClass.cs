using System;
using System.Collections.Generic;

namespace BackEnd.Model {
	/// <summary>
	/// Represents a named group used to group similar species.
	/// </summary>
	public class SpeciesClass {
		public String Name { get; }

		internal SpeciesClass(String name) {
			Name = name;
		}

		public override Boolean Equals(Object obj) {
			if (!(obj is SpeciesClass))
				return false;

			SpeciesClass secondSpeciesClass
				= (SpeciesClass) obj;

			return Name == secondSpeciesClass.Name;
		}

		public override int GetHashCode() {
			return 539060726
					+ EqualityComparer<string>.Default.GetHashCode(Name);
		}
	}
}

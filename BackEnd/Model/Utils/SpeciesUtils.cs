using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model.Utils {
	public static class SpeciesUtils {
		/// <summary>
		/// Get IEnumerable of unique species classes from IEnumerable provided.
		/// </summary>
		/// <param name="species">Species from which to extract classes.</param>
		/// <returns>IEnumerable of unique species classes.</returns>
		public static IEnumerable<SpeciesClass> GetSpeciesClasses(IEnumerable<Species> species)
			=> species.SelectMany(sp => sp.Classes).Distinct();
	}
}

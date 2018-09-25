using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using log4net;

namespace BackEnd.Model.Factories {
	internal class SpeciesFactory {
		private static readonly ILog Logger = Logging.GetLogger();

		internal static SpeciesClass GetSpeciesClass(SpeciesClassMapping speciesClassMapping)
			=> new SpeciesClass(speciesClassMapping.SpeciesClassName);

		internal static Species GetSpecies(SpeciesMapping speciesMapping, SpeciesClass speciesClass) {
			String name = speciesMapping.SpeciesName;
			String speciesdirectoryName = speciesMapping.SpeciesImageDirectory;
			IEnumerable<string> speciesHints = speciesMapping.SpeciesHints.ToArray();
			String directory = Path.Combine(Constants.ImageDirectory, speciesdirectoryName);

			Logger.Debug($"Building species ${name}");

			if (!Directory.Exists(directory))
				throw new FileNotFoundException($"Directory {directory} was not found.");

			List<SpeciesImage> speciesImages = new List<SpeciesImage>();

			String[] files = Directory.GetFiles(directory);
			foreach (String file in files) {
				String fileLower = file.ToLower(CultureInfo.InvariantCulture);
				if (!Constants.ImageExtensions.Any(ext => fileLower.EndsWith(ext))) {
					Logger.Warn($"Found file in species folders without correct extension: {file}");
				} else {
					SpeciesImage speciesImage = new SpeciesImage(file);
					speciesImages.Add(speciesImage);
				}
			}

			ISet<SpeciesClass> speciesClasses = new HashSet<SpeciesClass> {
				speciesClass
			};

			return new Species(speciesImages.AsReadOnly(), name, speciesClasses, speciesHints);
		}
	}
}
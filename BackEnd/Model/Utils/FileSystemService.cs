

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;

namespace BackEnd.Model {
	/// <summary>
	/// Handle paths and filesystem.
	/// </summary>
	internal static class FileSystemService {
		private static readonly ILog Logger = Logging.GetLogger();

		internal static SpeciesFileDescription GetSpeciesMappings() {
			string speciesFile = Constants.SpeciesFile;
			Logger.Debug($"Reading species mappings from {speciesFile}.");

			if (!File.Exists(speciesFile))
				throw new FileNotFoundException($"Species file {speciesFile} not found.");

			string speciesFileText = File.ReadAllText(speciesFile);
			var speciesFileDescription
				= JsonConvert.DeserializeObject<SpeciesFileDescription>(speciesFileText);

			if (speciesFileDescription == null)
				throw new InvalidOperationException("Species json was in invalid form.");

			Logger.Info("Species mappings read.");

			return speciesFileDescription;
		}
	}
}

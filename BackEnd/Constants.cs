using System;
using System.IO;

namespace BackEnd {
	internal static class Constants {
		/// <summary>
		///     Root directory for all files.
		/// </summary>
		private const String DataFolder = "data";

		/// <summary>
		///     Image folder name.
		/// </summary>
		private const String ImageFolder = "elaimet";

		/// <summary>
		///     Allowed image extensions; case insensitive.
		/// </summary>
		internal static readonly String[] ImageExtensions = {
			"png",
			"jpg",
			"bmp"
		};

		/// <summary>
		///     Species configuration file.
		/// </summary>
		internal static readonly String SpeciesFile = Path.Combine(DataFolder, "data.json");

		/// <summary>
		///     Base directory for images.
		/// </summary>
		internal static readonly String ImageDirectory = Path.Combine(DataFolder, ImageFolder);
	}
}
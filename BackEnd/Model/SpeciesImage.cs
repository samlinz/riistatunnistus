using System;
using System.Drawing;
using log4net;

namespace BackEnd.Model {
	public class SpeciesImage {
		private static readonly ILog Logger = Logging.GetLogger();

		private Image _image;

		public Lazy<Image> Image => new Lazy<Image>(GetImage);
		public String Filename { get; }

		public SpeciesImage(String filename) {
			if (String.IsNullOrEmpty(filename))
				throw new ArgumentException("Empty filename.", nameof(filename));

			Filename = filename;
		}

		/// <summary>
		/// Get Image object for drawing.
		/// </summary>
		/// <remarks>Causes file to be loaded from disk if it hasn't been loade already.</remarks>
		/// <returns>Image object representing the image file loade to memory.</returns>
		private Image GetImage() {
			if (_image == null) {
				_image = System.Drawing.Image.FromFile(Filename);
				Logger.Debug($"Loaded image {Filename}");
			}

			return _image;
		}

		private bool IsImageLoaded => _image != null;
	}
}
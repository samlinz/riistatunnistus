using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Media;
using BackEnd;
using log4net;

namespace RiistaTunnistusOhjelma {
	/// <summary>
	/// Class for playing sound effects.
	/// </summary>
	internal class SoundTool {
		private static readonly ILog Logger = Logging.GetLogger();

		// Map from sound effect identifier to location on disk.
		private readonly IDictionary<SoundEffect, SoundPlayer> _sounds
			= new ConcurrentDictionary<SoundEffect, SoundPlayer>();

		internal void AddSound(SoundEffect effect, string location) {
			if (!File.Exists(location)) {
				Logger.Warn($"Could not load {effect}; file does not exists.");
				return;
			}

			SoundPlayer soundPlayer;
			try {
				soundPlayer = new SoundPlayer(location);
			} catch (Exception ex) {
				Logger.Error($"Failed to load sound {effect}. Exception {ex.Message}.");
				return;
			}

			_sounds.Add(effect, soundPlayer);
			Logger.Info($"Added sound file {effect} at {location}.");
		}

		internal void PlaySound(SoundEffect effect) {
			if (_sounds.TryGetValue(effect, out SoundPlayer player)) {
				player.Play();
				Logger.Debug($"Playing sound {effect}.");
				return;
			}

			Logger.Warn($"Could not play sound {effect}; not loaded.");
		}
	}

	internal enum SoundEffect {
		Unkown = 0,
		Wrong = 1,
		Correct = 2,
		Done = 3,
		Error = 4
	}
}
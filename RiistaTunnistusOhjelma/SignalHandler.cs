using System;

namespace RiistaTunnistusOhjelma {
	internal class SignalHandler {
		internal event EventHandler<string> FatalError;

		internal void OnFatalError(string err)
			=> FatalError?.Invoke(null, err);
	}
}

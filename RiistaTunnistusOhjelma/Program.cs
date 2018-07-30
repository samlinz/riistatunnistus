using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;
using log4net;

namespace RiistaTunnistusOhjelma {
	static class Program {
		private static readonly ILog Logger
			= Logging.GetLogger(LoggingSources.Frontend);

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Logger.Info("Starting frontend.");
			Console.Read();
			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new RiistaTunnistusOhjelma());
		}
	}
}

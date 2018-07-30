using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace BackEnd {
	/// <summary>
	/// Handle global logger.
	/// </summary>
	public class Logging {
		public static ILog GetLogger() {
			AssemblyName assemblyName
				= Assembly.GetCallingAssembly().GetName();

			return LogManager.GetLogger(assemblyName.Name);
		}
	}
}
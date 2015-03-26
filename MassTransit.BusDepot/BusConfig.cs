using System.Collections.Specialized;
using System.Configuration;

namespace MassTransit.BusDepot
{
	/// <summary>
	/// Used for loading and storing configuration values for the bus.
	/// </summary>
	public class BusConfig
	{
		/// <summary/>
		public string EndpointAddress { get; set; }
		/// <summary/>
		public string UserName { get; set; }
		/// <summary/>
		public string Password { get; set; }

		/// <summary>
		/// Load the bus configuration values from the .config file.
		/// </summary>
		/// <returns>
		/// A populated instance of the config.
		/// </returns>
		public static BusConfig Load()
		{
			var secureSettings = (NameValueCollection)ConfigurationManager.GetSection("secureAppSettings");

			var endpointAddress = ConfigurationManager.AppSettings["MassTransit.EndpointAddress"];
			if (string.IsNullOrWhiteSpace(endpointAddress))
			{
				throw new ConfigurationErrorsException("Bus endpoint is not configured");
			}

			var userName = secureSettings["MassTransit.BusUserName"];
			if (userName == null)
			{
				throw new ConfigurationErrorsException("Bus userName is not configured");
			}

			var password = secureSettings["MassTransit.BusPassword"];
			if (password == null)
			{
				throw new ConfigurationErrorsException("Bus password is not configured");
			}

			return new BusConfig
			{
				EndpointAddress = endpointAddress,
				UserName = userName,
				Password = password
			};
		}

	}
}

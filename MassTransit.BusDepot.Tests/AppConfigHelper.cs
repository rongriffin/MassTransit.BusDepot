using System.Configuration;

namespace MassTransit.BusDepot.Tests
{
	internal static class AppConfigHelper
	{
		/// <summary>
		/// Reset the app settings for a test.
		/// </summary>
		/// <param name="endpointAddress">The endpoint address to configure.</param>
		/// <param name="userName">The username to configure</param>
		/// <param name="password">The password to configure</param>
		public  static void ResetAppSettings(string endpointAddress, string userName, string password)
		{

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			config.AppSettings.Settings.Remove("MassTransit.EndpointAddress");
			if (endpointAddress != null)
				config.AppSettings.Settings.Add("MassTransit.EndpointAddress", endpointAddress);

			var secureAppSettings = config.GetSection("secureAppSettings") as AppSettingsSection;
			if (secureAppSettings == null) throw new ConfigurationErrorsException("secureAppSettings is misconfigured");

			secureAppSettings.Settings.Remove("MassTransit.BusUserName");
			if (userName != null)
				secureAppSettings.Settings.Add("MassTransit.BusUserName", userName);

			secureAppSettings.Settings.Remove("MassTransit.BusPassword");
			if (password != null)
				secureAppSettings.Settings.Add("MassTransit.BusPassword", password);

			config.Save(ConfigurationSaveMode.Modified, true);
			ConfigurationManager.RefreshSection("appSettings");
			ConfigurationManager.RefreshSection("secureAppSettings");
		}
	}
}

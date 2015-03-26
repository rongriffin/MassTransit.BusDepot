using System;
using System.Configuration;
using MassTransit;
using MassTransit.BusConfigurators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MassTransit.BusDepot.Tests
{
	[TestClass]
	public class BusDepotTests
	{

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConfigureTransport_InvalidAddressScheme_ThrowsException()
		{
			var config = new BusConfig { EndpointAddress = "crap://test" };
			BusDepot.ConfigureTransport(new ServiceBusConfiguratorImpl(new ServiceBusDefaultSettings()), config);
		}

		[TestMethod]
		public void CreateBus_Loopback_CreatesBusInstance()
		{
			const string expectedUri = "loopback://localhost/queue";
			BusDepot._bus = null;  //reset since this is static
			AppConfigHelper.ResetAppSettings(expectedUri, string.Empty, string.Empty);
			var bus = BusDepot.Bus;

			Assert.IsNotNull(bus);
			Assert.AreEqual(expectedUri, bus.Endpoint.Address.Uri.OriginalString);
		}

		[TestMethod]
		public void CreateBus_RabbitMq_CreatesBusInstance()
		{
			var runThisTest = bool.Parse(ConfigurationManager.AppSettings["RabbitMqInstalled"]);

			if (runThisTest)
			{
				const string expectedUri = "rabbitmq://localhost/BusDepotTestQueue";
				BusDepot._bus = null;  //reset since this is static
				AppConfigHelper.ResetAppSettings(expectedUri, "guest", "guest");
				var bus = BusDepot.Bus;

				Assert.IsNotNull(bus);
				Assert.AreEqual(expectedUri, bus.Endpoint.Address.Uri.OriginalString);
			}

		}

	}
}

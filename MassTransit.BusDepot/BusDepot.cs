using System;
using System.Security.Policy;
using MassTransit;
using MassTransit.BusConfigurators;

namespace MassTransit.BusDepot
{
	/// <summary>
	/// Used to get an instance of the Bus
	/// </summary>
	public static class BusDepot
	{
		internal static IServiceBus _bus;

		/// <summary>
		/// Return an instance of the bus.
		/// </summary>
		public static IServiceBus Bus
		{
			get
			{
				var config = BusConfig.Load();

				return _bus ?? (_bus = ServiceBusFactory.New(sbc =>
				{
					ConfigureTransport(sbc, config);
					sbc.UseJsonSerializer();
					sbc.ReceiveFrom(config.EndpointAddress);
				}));
			}
		}


		/// <summary>
		/// Resolve the bus transport
		/// </summary>
		/// <param name="sbc">The bus configurator used in the factory</param>
		/// <param name="config">The bus configuration</param>
		internal static void ConfigureTransport(ServiceBusConfigurator sbc, BusConfig config)
		{
			if (sbc == null || config == null) return;
			var uri = new Uri(config.EndpointAddress);

			switch (uri.Scheme.ToLowerInvariant())
			{
				case "rabbitmq":
					sbc.UseRabbitMq(r => r.ConfigureHost(new Uri(config.EndpointAddress), h =>
							{
								h.SetUsername(config.UserName);
								h.SetPassword(config.Password);
							})
					);
					break;
				case "loopback":
					// don't do anything.  loopback is the default.
					break;
				default:
					throw new InvalidOperationException(string.Format("Unknown transport protocol '{0}'", uri.Scheme));
			}
		}

	}
}

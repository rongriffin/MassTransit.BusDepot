using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MassTransit.BusDepot.Tests
{
	/// <summary>
	/// Tests for the BusConfig class in the service.
	/// </summary>
	[TestClass]
	public class BusConfigTests
	{
		/// <summary>
		/// Verify that loading a successful configuration works.
		/// </summary>
		[TestMethod]
		public void BusConfig_Load_Success()
		{
			const string testEndpoint = "testEndpoint";
			const string testUserName = "testUser";
			const string testPassword = "testPass";
			AppConfigHelper.ResetAppSettings(testEndpoint, testUserName, testPassword);
			var config = BusConfig.Load();

			Assert.IsNotNull(config);
			Assert.IsNotNull(config.EndpointAddress);
			Assert.AreEqual(config.EndpointAddress, testEndpoint);
			Assert.IsNotNull(config.UserName);
			Assert.AreEqual(config.UserName, testUserName);
			Assert.IsNotNull(config.Password);
			Assert.AreEqual(config.Password, testPassword);
		}

		/// <summary>
		/// Verify that missing bus endpoing config throws exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void BusConfig_MissingEndpoint_ThrowsException()
		{
			AppConfigHelper.ResetAppSettings(null, "test", "test");
			BusConfig.Load();
		}

		/// <summary>
		/// Verify that empty endpoint address throws exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void BusConfig_EmptyEndpoint_ThrowsException()
		{
			AppConfigHelper.ResetAppSettings(string.Empty, "test", "test");
			BusConfig.Load();
		}

		/// <summary>
		/// Verify that missing username config throws exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void BusConfig_MissingUserName_ThrowsException()
		{
			AppConfigHelper.ResetAppSettings("testendpoint", null, "test");
			BusConfig.Load();
		}

		/// <summary>
		/// Verify that missing password config throws exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void BusConfig_MissingBusPassword_ThrowsException()
		{
			AppConfigHelper.ResetAppSettings("testendpoint", "test", null);
			BusConfig.Load();
		}

	}
}
namespace GooglePlayGames
{
	public static class GameInfo
	{
		private const string UnescapedApplicationId = "APP_ID";

		private const string UnescapedIosClientId = "IOS_CLIENTID";

		private const string UnescapedWebClientId = "WEB_CLIENTID";

		private const string UnescapedNearbyServiceId = "NEARBY_SERVICE_ID";

		public const string ApplicationId = "683498632423";

		public const string IosClientId = "";

		public const string WebClientId = "683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com";

		public const string NearbyConnectionServiceId = "";

		public static bool ApplicationIdInitialized()
		{
			if (!string.IsNullOrEmpty("683498632423"))
			{
				return !"683498632423".Equals(ToEscapedToken("APP_ID"));
			}
			return false;
		}

		public static bool IosClientIdInitialized()
		{
			if (!string.IsNullOrEmpty(""))
			{
				return !"".Equals(ToEscapedToken("IOS_CLIENTID"));
			}
			return false;
		}

		public static bool WebClientIdInitialized()
		{
			if (!string.IsNullOrEmpty("683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com"))
			{
				return !"683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com".Equals(ToEscapedToken("WEB_CLIENTID"));
			}
			return false;
		}

		public static bool NearbyConnectionsInitialized()
		{
			if (!string.IsNullOrEmpty(""))
			{
				return !"".Equals(ToEscapedToken("NEARBY_SERVICE_ID"));
			}
			return false;
		}

		private static string ToEscapedToken(string token)
		{
			return $"__{token}__";
		}
	}
}

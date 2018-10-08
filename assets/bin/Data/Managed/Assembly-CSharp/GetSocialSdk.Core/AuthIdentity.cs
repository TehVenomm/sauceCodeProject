using UnityEngine;

namespace GetSocialSdk.Core
{
	public class AuthIdentity : IConvertableToNative
	{
		private readonly string _providerId;

		private readonly string _providerUserId;

		private readonly string _accessToken;

		private AuthIdentity(string providerName, string userId, string accessToken)
		{
			_providerId = providerName;
			_providerUserId = userId;
			_accessToken = accessToken;
		}

		public static AuthIdentity CreateFacebookIdentity(string accessToken)
		{
			return CreateCustomIdentity("facebook", null, accessToken);
		}

		public static AuthIdentity CreateCustomIdentity(string providerName, string userId, string accessToken)
		{
			return new AuthIdentity(providerName, userId, accessToken);
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass ajo = new AndroidJavaClass("im.getsocial.sdk.usermanagement.AuthIdentity");
			return ajo.CallStaticAJO("createCustomIdentity", _providerId, _providerUserId, _accessToken);
		}
	}
}

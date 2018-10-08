using System;

namespace Facebook.Unity.Example
{
	internal class AccessTokenMenu : MenuBase
	{
		protected unsafe override void GetGui()
		{
			if (Button("Refresh Access Token"))
			{
				Mobile.RefreshCurrentAccessToken(new FacebookDelegate<IAccessTokenRefreshResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}
}

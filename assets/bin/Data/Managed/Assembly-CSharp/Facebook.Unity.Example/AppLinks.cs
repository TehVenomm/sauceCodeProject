using System;

namespace Facebook.Unity.Example
{
	internal class AppLinks : MenuBase
	{
		protected unsafe override void GetGui()
		{
			if (Button("Get App Link"))
			{
				FB.GetAppLink(new FacebookDelegate<IAppLinkResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			if (Constants.get_IsMobile() && Button("Fetch Deferred App Link"))
			{
				Mobile.FetchDeferredAppLinkData(new FacebookDelegate<IAppLinkResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}
}

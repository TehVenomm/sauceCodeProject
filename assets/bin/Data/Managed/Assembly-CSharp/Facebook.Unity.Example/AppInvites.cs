using System;

namespace Facebook.Unity.Example
{
	internal class AppInvites : MenuBase
	{
		protected unsafe override void GetGui()
		{
			if (Button("Android Invite"))
			{
				base.Status = "Logged FB.AppEvent";
				FacebookDelegate<IAppInviteResult> val = new FacebookDelegate<IAppInviteResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), (Uri)null, val);
			}
			if (Button("Android Invite With Custom Image"))
			{
				base.Status = "Logged FB.AppEvent";
				Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), new Uri("http://i.imgur.com/zkYlB.jpg"), new FacebookDelegate<IAppInviteResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			if (Button("iOS Invite"))
			{
				base.Status = "Logged FB.AppEvent";
				FacebookDelegate<IAppInviteResult> val = new FacebookDelegate<IAppInviteResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), (Uri)null, val);
			}
			if (Button("iOS Invite With Custom Image"))
			{
				base.Status = "Logged FB.AppEvent";
				Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), new Uri("http://i.imgur.com/zkYlB.jpg"), new FacebookDelegate<IAppInviteResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}
}

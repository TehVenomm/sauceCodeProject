using Facebook.Unity;
using GetSocialSdk.Core;
using GetSocialSdk.Ui;
using System;

[Obsolete("Facebook is deprecating App Invites from February 5, 2018. Use FacebookSharePlugin instead.", false)]
public class FacebookInvitePlugin : InviteChannelPlugin
{
	public bool IsAvailableForDevice(InviteChannel inviteChannel)
	{
		return true;
	}

	public void PresentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
	{
		GetSocialUi.CloseView(true);
		Uri imageUrl = null;
		if (invitePackage.ImageUrl != null)
		{
			imageUrl = new Uri(invitePackage.ImageUrl);
		}
		SendInvite(invitePackage.ReferralDataUrl, imageUrl, onComplete, onCancel, onFailure);
	}

	private unsafe static void SendInvite(string referralDataUrl, Uri imageUrl, Action completeCallback, Action cancelCallback, Action<GetSocialError> errorCallback)
	{
		_003CSendInvite_003Ec__AnonStorey7E9 _003CSendInvite_003Ec__AnonStorey7E;
		Mobile.AppInvite(new Uri(referralDataUrl), imageUrl, new FacebookDelegate<IAppInviteResult>((object)_003CSendInvite_003Ec__AnonStorey7E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}

using Facebook.Unity;
using GetSocialSdk.Core;
using GetSocialSdk.Ui;
using System;

public class FacebookSharePlugin : InviteChannelPlugin
{
	public bool IsAvailableForDevice(InviteChannel inviteChannel)
	{
		return true;
	}

	public void PresentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
	{
		GetSocialUi.CloseView(true);
		SendInvite(invitePackage.ReferralDataUrl, onComplete, onCancel, onFailure);
	}

	private unsafe static void SendInvite(string referralDataUrl, Action completeCallback, Action cancelCallback, Action<GetSocialError> errorCallback)
	{
		Mobile.set_ShareDialogMode(2);
		_003CSendInvite_003Ec__AnonStorey7EA _003CSendInvite_003Ec__AnonStorey7EA;
		FacebookDelegate<IShareResult> val = new FacebookDelegate<IShareResult>((object)_003CSendInvite_003Ec__AnonStorey7EA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		FB.ShareLink(new Uri(referralDataUrl), string.Empty, string.Empty, (Uri)null, val);
	}
}

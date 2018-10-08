using System;

namespace GetSocialSdk.Core
{
	public interface InviteChannelPlugin
	{
		bool IsAvailableForDevice(InviteChannel inviteChannel);

		void PresentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, Action onComplete, Action onCancel, Action<GetSocialError> onFailure);
	}
}

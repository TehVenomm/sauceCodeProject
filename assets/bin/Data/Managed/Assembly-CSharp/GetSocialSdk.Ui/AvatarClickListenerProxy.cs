using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public class AvatarClickListenerProxy : JavaInterfaceProxy
	{
		private readonly Action<PublicUser> _avatarClickListener;

		public AvatarClickListenerProxy(Action<PublicUser> avatarClickListener)
			: base("im.getsocial.sdk.ui.AvatarClickListener")
		{
			_avatarClickListener = avatarClickListener;
		}

		private unsafe void onAvatarClicked(AndroidJavaObject publicUserAjo)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			Debug.Log((object)">>>>>>> XXXX");
			PublicUser publicUser = new PublicUser().ParseFromAJO(publicUserAjo);
			_003ConAvatarClicked_003Ec__AnonStorey7F3 _003ConAvatarClicked_003Ec__AnonStorey7F;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConAvatarClicked_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}

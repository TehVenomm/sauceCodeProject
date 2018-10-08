using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class InviteChannelPluginProxy : JavaInterfaceProxy
	{
		private readonly InviteChannelPlugin _invitePlugin;

		public InviteChannelPluginProxy(InviteChannelPlugin invitePlugin)
			: base("im.getsocial.sdk.internal.unity.InviteChannelPluginAdapter$InviteChannelPluginInterface")
		{
			_invitePlugin = invitePlugin;
		}

		private bool isAvailableForDevice(AndroidJavaObject inviteChannelAJO)
		{
			InviteChannel inviteChannel = new InviteChannel().ParseFromAJO(inviteChannelAJO);
			return _invitePlugin.IsAvailableForDevice(inviteChannel);
		}

		private unsafe void presentChannelInterface(AndroidJavaObject inviteChannelAJO, AndroidJavaObject invitePackageAJO, AndroidJavaObject callbackAJO)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			_003CpresentChannelInterface_003Ec__AnonStorey7F0 _003CpresentChannelInterface_003Ec__AnonStorey7F;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003CpresentChannelInterface_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}

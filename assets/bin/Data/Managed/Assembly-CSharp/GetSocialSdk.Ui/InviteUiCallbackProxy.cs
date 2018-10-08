using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	public class InviteUiCallbackProxy : JavaInterfaceProxy
	{
		private readonly Action<string> _onComplete;

		private readonly Action<string> _onCancel;

		private readonly Action<string, GetSocialError> _onFailure;

		public InviteUiCallbackProxy(Action<string> onComplete, Action<string> onCancel, Action<string, GetSocialError> onFailure)
			: base("im.getsocial.sdk.ui.invites.InviteUiCallback")
		{
			_onComplete = onComplete;
			_onCancel = onCancel;
			_onFailure = onFailure;
		}

		private unsafe void onComplete(string channelId)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			Debug.Log((object)"Complete");
			_003ConComplete_003Ec__AnonStorey808 _003ConComplete_003Ec__AnonStorey;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConComplete_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private unsafe void onCancel(string channelId)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			Debug.Log((object)"Cancel");
			_003ConCancel_003Ec__AnonStorey809 _003ConCancel_003Ec__AnonStorey;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConCancel_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private unsafe void onError(string channelId, AndroidJavaObject throwable)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			Debug.Log((object)"Failure");
			_003ConError_003Ec__AnonStorey80A _003ConError_003Ec__AnonStorey80A;
			JavaInterfaceProxy.ExecuteOnMainThread(new Action((object)_003ConError_003Ec__AnonStorey80A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}

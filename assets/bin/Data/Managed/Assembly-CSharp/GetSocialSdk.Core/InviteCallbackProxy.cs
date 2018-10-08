using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class InviteCallbackProxy : JavaInterfaceProxy
	{
		private readonly Action _onComplete;

		private readonly Action _onCancel;

		private readonly Action<GetSocialError> _onFailure;

		public InviteCallbackProxy(Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.invites.InviteCallback")
		{
			_onComplete = onComplete;
			_onCancel = onCancel;
			_onFailure = onFailure;
		}

		private void onComplete()
		{
			Debug.Log((object)"Complete");
			JavaInterfaceProxy.ExecuteOnMainThread(_onComplete);
		}

		private void onCancel()
		{
			Debug.Log((object)"Cancel");
			JavaInterfaceProxy.ExecuteOnMainThread(_onCancel);
		}

		private void onError(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}

using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class AddAuthIdentityCallbackProxy : JavaInterfaceProxy
	{
		private readonly Action _onComplete;

		private readonly Action<GetSocialError> _onFailure;

		private readonly Action<ConflictUser> _onConflict;

		public AddAuthIdentityCallbackProxy(Action onComplete, Action<GetSocialError> onFailure, Action<ConflictUser> onConflict)
			: base("im.getsocial.sdk.usermanagement.AddAuthIdentityCallback")
		{
			_onComplete = onComplete;
			_onFailure = onFailure;
			_onConflict = onConflict;
		}

		private void onComplete()
		{
			JavaInterfaceProxy.ExecuteOnMainThread(_onComplete);
		}

		private void onFailure(AndroidJavaObject throwableAJO)
		{
			HandleError(throwableAJO, _onFailure);
		}

		private void onConflict(AndroidJavaObject conflictUserAJO)
		{
			ConflictUser value = new ConflictUser().ParseFromAJO(conflictUserAJO);
			HandleValue(value, _onConflict);
		}
	}
}

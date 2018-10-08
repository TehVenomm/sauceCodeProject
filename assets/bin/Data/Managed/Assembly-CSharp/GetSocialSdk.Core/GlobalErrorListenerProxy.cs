using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class GlobalErrorListenerProxy : JavaInterfaceProxy
	{
		private readonly Action<GetSocialError> _onError;

		public GlobalErrorListenerProxy(Action<GetSocialError> onError)
			: base("im.getsocial.sdk.GlobalErrorListener")
		{
			_onError = onError;
		}

		private void onError(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onError);
		}
	}
}

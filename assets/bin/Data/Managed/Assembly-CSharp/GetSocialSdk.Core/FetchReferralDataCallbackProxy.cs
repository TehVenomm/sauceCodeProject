using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class FetchReferralDataCallbackProxy : JavaInterfaceProxy
	{
		private readonly Action<ReferralData> _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public FetchReferralDataCallbackProxy(Action<ReferralData> onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.invites.FetchReferralDataCallback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess(AndroidJavaObject referralDataAJO)
		{
			ReferralData value = new ReferralData().ParseFromAJO(referralDataAJO);
			HandleValue(value, _onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}

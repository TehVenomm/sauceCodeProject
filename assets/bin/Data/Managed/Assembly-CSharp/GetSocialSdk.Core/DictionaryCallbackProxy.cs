using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class DictionaryCallbackProxy<TValue> : JavaInterfaceProxy where TValue : IConvertableFromNative<TValue>, new()
	{
		private readonly Action<Dictionary<string, TValue>> _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public DictionaryCallbackProxy(Action<Dictionary<string, TValue>> onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.Callback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess(AndroidJavaObject resultAJO)
		{
			Dictionary<string, TValue> dictionary = new Dictionary<string, TValue>();
			if (resultAJO != null && !resultAJO.IsJavaNull())
			{
				AndroidJavaObject ajo = resultAJO.CallAJO("keySet").CallAJO("iterator");
				while (ajo.CallBool("hasNext"))
				{
					string text = ajo.CallStr("next");
					TValue value = new TValue().ParseFromAJO(resultAJO.Call<AndroidJavaObject>("get", new object[1]
					{
						text
					}));
					dictionary.Add(text, value);
				}
			}
			base.HandleValue<Dictionary<string, TValue>>(dictionary, _onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}

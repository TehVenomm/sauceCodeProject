using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class ListCallbackProxy<T> : JavaInterfaceProxy where T : IConvertableFromNative<T>, new()
	{
		private readonly Action<List<T>> _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public ListCallbackProxy(Action<List<T>> onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.Callback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess(AndroidJavaObject resultAJO)
		{
			List<T> value = Enumerable.ToList<T>((IEnumerable<T>)resultAJO.FromJavaList().ConvertAll(delegate(AndroidJavaObject ajo)
			{
				try
				{
					return new T().ParseFromAJO(ajo);
					IL_003a:
					T result;
					return result;
				}
				finally
				{
					((IDisposable)ajo)?.Dispose();
				}
			}));
			base.HandleValue<List<T>>(value, _onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}

using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public class JavaInterfaceProxy
	{
		private static JavaInterfaceProxy _comparingWho;

		internal JavaInterfaceProxy(string javaInterface)
			: this(javaInterface)
		{
		}

		public bool equals(AndroidJavaObject other)
		{
			bool result;
			if (_comparingWho != null)
			{
				result = (_comparingWho == this);
				_comparingWho = null;
			}
			else
			{
				_comparingWho = this;
				result = other.Call<bool>("equals", new object[1]
				{
					other
				});
			}
			return result;
		}

		public string toString()
		{
			return GetType().Name;
		}

		protected static void ExecuteOnMainThread(Action action)
		{
			MainThreadExecutor.Queue(action);
		}

		protected unsafe void HandleError(AndroidJavaObject throwable, Action<GetSocialError> onFailure)
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			if (onFailure != null)
			{
				try
				{
					GetSocialError ex = throwable.ToGetSocialError();
					_003CHandleError_003Ec__AnonStorey7EE _003CHandleError_003Ec__AnonStorey7EE;
					ExecuteOnMainThread(new Action((object)_003CHandleError_003Ec__AnonStorey7EE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				finally
				{
					((IDisposable)throwable)?.Dispose();
				}
			}
		}

		protected unsafe void HandleValue<T>(T value, Action<T> onSuccess)
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			if (onSuccess != null)
			{
				_003CHandleValue_003Ec__AnonStorey7EF<T> _003CHandleValue_003Ec__AnonStorey7EF;
				ExecuteOnMainThread(new Action((object)_003CHandleValue_003Ec__AnonStorey7EF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}
}

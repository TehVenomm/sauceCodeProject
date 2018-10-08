using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
	internal static class Callbacks
	{
		internal enum Type
		{
			Permanent,
			Temporary
		}

		internal delegate void ShowUICallbackInternal(CommonErrorStatus.UIStatus status, IntPtr data);

		internal static readonly Action<CommonErrorStatus.UIStatus> NoopUICallback = delegate(CommonErrorStatus.UIStatus status)
		{
			Logger.d("Received UI callback: " + status);
		};

		internal static IntPtr ToIntPtr<T>(Action<T> callback, Func<IntPtr, T> conversionFunction) where T : BaseReferenceHolder
		{
			Action<IntPtr> callback2 = delegate(IntPtr result)
			{
				using (T obj = conversionFunction.Invoke(result))
				{
					if (callback != null)
					{
						callback(obj);
					}
				}
			};
			return ToIntPtr(callback2);
		}

		internal unsafe static IntPtr ToIntPtr<T, P>(Action<T, P> callback, Func<IntPtr, P> conversionFunction) where P : BaseReferenceHolder
		{
			_003CToIntPtr_003Ec__AnonStorey83E<T, P> _003CToIntPtr_003Ec__AnonStorey83E;
			Action<T, IntPtr> callback2 = new Action<_003F, IntPtr>((object)_003CToIntPtr_003Ec__AnonStorey83E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			return ToIntPtr((Delegate)callback2);
		}

		internal static IntPtr ToIntPtr(Delegate callback)
		{
			if ((object)callback == null)
			{
				return IntPtr.Zero;
			}
			GCHandle value = GCHandle.Alloc(callback);
			return GCHandle.ToIntPtr(value);
		}

		internal static T IntPtrToTempCallback<T>(IntPtr handle) where T : class
		{
			return IntPtrToCallback<T>(handle, true);
		}

		private static T IntPtrToCallback<T>(IntPtr handle, bool unpinHandle) where T : class
		{
			if (!PInvokeUtilities.IsNull(handle))
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(handle);
				try
				{
					return (T)gCHandle.Target;
					IL_002b:
					T result;
					return result;
				}
				catch (InvalidCastException ex)
				{
					Logger.e("GC Handle pointed to unexpected type: " + gCHandle.Target.ToString() + ". Expected " + typeof(T));
					throw ex;
					IL_006f:
					T result;
					return result;
				}
				finally
				{
					if (unpinHandle)
					{
						gCHandle.Free();
					}
				}
			}
			return (T)null;
		}

		internal static T IntPtrToPermanentCallback<T>(IntPtr handle) where T : class
		{
			return IntPtrToCallback<T>(handle, false);
		}

		[MonoPInvokeCallback(typeof(ShowUICallbackInternal))]
		internal static void InternalShowUICallback(CommonErrorStatus.UIStatus status, IntPtr data)
		{
			Logger.d("Showing UI Internal callback: " + status);
			Action<CommonErrorStatus.UIStatus> action = IntPtrToTempCallback<Action<CommonErrorStatus.UIStatus>>(data);
			try
			{
				action(status);
			}
			catch (Exception arg)
			{
				Logger.e("Error encountered executing InternalShowAllUICallback. Smothering to avoid passing exception into Native: " + arg);
			}
		}

		internal static void PerformInternalCallback(string callbackName, Type callbackType, IntPtr response, IntPtr userData)
		{
			Logger.d("Entering internal callback for " + callbackName);
			Action<IntPtr> action = (callbackType != 0) ? IntPtrToTempCallback<Action<IntPtr>>(userData) : IntPtrToPermanentCallback<Action<IntPtr>>(userData);
			if (action != null)
			{
				try
				{
					action(response);
				}
				catch (Exception ex)
				{
					Logger.e("Error encountered executing " + callbackName + ". Smothering to avoid passing exception into Native: " + ex);
				}
			}
		}

		internal static void PerformInternalCallback<T>(string callbackName, Type callbackType, T param1, IntPtr param2, IntPtr userData)
		{
			Logger.d("Entering internal callback for " + callbackName);
			Action<T, IntPtr> val = null;
			try
			{
				val = ((callbackType != 0) ? IntPtrToTempCallback<Action<T, IntPtr>>(userData) : IntPtrToPermanentCallback<Action<T, IntPtr>>(userData));
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered converting " + callbackName + ". Smothering to avoid passing exception into Native: " + ex);
				return;
				IL_005f:;
			}
			Logger.d("Internal Callback converted to action");
			if (val != null)
			{
				try
				{
					val.Invoke(param1, param2);
				}
				catch (Exception ex2)
				{
					Logger.e("Error encountered executing " + callbackName + ". Smothering to avoid passing exception into Native: " + ex2);
				}
			}
		}

		internal unsafe static Action<T> AsOnGameThreadCallback<T>(Action<T> toInvokeOnGameThread)
		{
			return delegate
			{
				//IL_0027: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Expected O, but got Unknown
				if (toInvokeOnGameThread != null)
				{
					_003CAsOnGameThreadCallback_003Ec__AnonStorey83F<T>._003CAsOnGameThreadCallback_003Ec__AnonStorey840 _003CAsOnGameThreadCallback_003Ec__AnonStorey;
					PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CAsOnGameThreadCallback_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
			};
		}

		internal unsafe static Action<T1, T2> AsOnGameThreadCallback<T1, T2>(Action<T1, T2> toInvokeOnGameThread)
		{
			_003CAsOnGameThreadCallback_003Ec__AnonStorey841<T1, T2> _003CAsOnGameThreadCallback_003Ec__AnonStorey;
			return new Action<_003F, _003F>((object)_003CAsOnGameThreadCallback_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}

		internal static void AsCoroutine(IEnumerator routine)
		{
			PlayGamesHelperObject.RunCoroutine(routine);
		}

		internal static byte[] IntPtrAndSizeToByteArray(IntPtr data, UIntPtr dataLength)
		{
			if (dataLength.ToUInt64() == 0L)
			{
				return null;
			}
			byte[] array = new byte[dataLength.ToUInt32()];
			Marshal.Copy(data, array, 0, (int)dataLength.ToUInt32());
			return array;
		}
	}
}

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
	internal class NativeEndpointDiscoveryListenerHelper : BaseReferenceHolder
	{
		internal NativeEndpointDiscoveryListenerHelper()
			: base(EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_Construct())
		{
		}

		protected override void CallDispose(HandleRef selfPointer)
		{
			EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_Dispose(selfPointer);
		}

		internal unsafe void SetOnEndpointFound(Action<long, NativeEndpointDetails> callback)
		{
			EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_SetOnEndpointFoundCallback(SelfPtr(), InternalOnEndpointFoundCallback, Callbacks.ToIntPtr<long, NativeEndpointDetails>(callback, new Func<IntPtr, NativeEndpointDetails>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		}

		[MonoPInvokeCallback(typeof(EndpointDiscoveryListenerHelper.OnEndpointFoundCallback))]
		private static void InternalOnEndpointFoundCallback(long id, IntPtr data, IntPtr userData)
		{
			Callbacks.PerformInternalCallback("NativeEndpointDiscoveryListenerHelper#InternalOnEndpointFoundCallback", Callbacks.Type.Permanent, id, data, userData);
		}

		internal void SetOnEndpointLostCallback(Action<long, string> callback)
		{
			EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_SetOnEndpointLostCallback(SelfPtr(), InternalOnEndpointLostCallback, Callbacks.ToIntPtr((Delegate)callback));
		}

		[MonoPInvokeCallback(typeof(EndpointDiscoveryListenerHelper.OnEndpointLostCallback))]
		private static void InternalOnEndpointLostCallback(long id, string lostEndpointId, IntPtr userData)
		{
			Action<long, string> val = Callbacks.IntPtrToPermanentCallback<Action<long, string>>(userData);
			if (val != null)
			{
				try
				{
					val.Invoke(id, lostEndpointId);
				}
				catch (Exception arg)
				{
					Logger.e("Error encountered executing NativeEndpointDiscoveryListenerHelper#InternalOnEndpointLostCallback. Smothering to avoid passing exception into Native: " + arg);
				}
			}
		}
	}
}

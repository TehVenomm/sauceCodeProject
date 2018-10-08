using AOT;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GooglePlayGames.Native.PInvoke
{
	internal class NearbyConnectionsManager : BaseReferenceHolder
	{
		private static readonly string sServiceId = ReadServiceId();

		public string AppBundleId
		{
			get
			{
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_000a: Expected O, but got Unknown
				AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				try
				{
					AndroidJavaObject @static = val.GetStatic<AndroidJavaObject>("currentActivity");
					return @static.Call<string>("getPackageName", new object[0]);
					IL_002e:
					string result;
					return result;
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
		}

		public static string ServiceId => sServiceId;

		internal NearbyConnectionsManager(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnections.NearbyConnections_Dispose(selfPointer);
		}

		internal void SendUnreliable(string remoteEndpointId, byte[] payload)
		{
			NearbyConnections.NearbyConnections_SendUnreliableMessage(SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong)payload.Length));
		}

		internal void SendReliable(string remoteEndpointId, byte[] payload)
		{
			NearbyConnections.NearbyConnections_SendReliableMessage(SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong)payload.Length));
		}

		internal unsafe void StartAdvertising(string name, List<NativeAppIdentifier> appIds, long advertisingDuration, Action<long, NativeStartAdvertisingResult> advertisingCallback, Action<long, NativeConnectionRequest> connectionRequestCallback)
		{
			HandleRef self = SelfPtr();
			if (_003C_003Ef__am_0024cache1 == null)
			{
				_003C_003Ef__am_0024cache1 = new Func<NativeAppIdentifier, IntPtr>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			NearbyConnections.NearbyConnections_StartAdvertising(self, name, appIds.Select<NativeAppIdentifier, IntPtr>(_003C_003Ef__am_0024cache1).ToArray(), new UIntPtr((ulong)appIds.Count), advertisingDuration, InternalStartAdvertisingCallback, Callbacks.ToIntPtr<long, NativeStartAdvertisingResult>(advertisingCallback, new Func<IntPtr, NativeStartAdvertisingResult>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)), InternalConnectionRequestCallback, Callbacks.ToIntPtr<long, NativeConnectionRequest>(connectionRequestCallback, new Func<IntPtr, NativeConnectionRequest>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		}

		[MonoPInvokeCallback(typeof(NearbyConnectionTypes.StartAdvertisingCallback))]
		private static void InternalStartAdvertisingCallback(long id, IntPtr result, IntPtr userData)
		{
			Callbacks.PerformInternalCallback("NearbyConnectionsManager#InternalStartAdvertisingCallback", Callbacks.Type.Permanent, id, result, userData);
		}

		[MonoPInvokeCallback(typeof(NearbyConnectionTypes.ConnectionRequestCallback))]
		private static void InternalConnectionRequestCallback(long id, IntPtr result, IntPtr userData)
		{
			Callbacks.PerformInternalCallback("NearbyConnectionsManager#InternalConnectionRequestCallback", Callbacks.Type.Permanent, id, result, userData);
		}

		internal void StopAdvertising()
		{
			NearbyConnections.NearbyConnections_StopAdvertising(SelfPtr());
		}

		internal unsafe void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<long, NativeConnectionResponse> callback, NativeMessageListenerHelper listener)
		{
			NearbyConnections.NearbyConnections_SendConnectionRequest(SelfPtr(), name, remoteEndpointId, payload, new UIntPtr((ulong)payload.Length), InternalConnectResponseCallback, Callbacks.ToIntPtr<long, NativeConnectionResponse>(callback, new Func<IntPtr, NativeConnectionResponse>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)), listener.AsPointer());
		}

		[MonoPInvokeCallback(typeof(NearbyConnectionTypes.ConnectionResponseCallback))]
		private static void InternalConnectResponseCallback(long localClientId, IntPtr response, IntPtr userData)
		{
			Callbacks.PerformInternalCallback("NearbyConnectionManager#InternalConnectResponseCallback", Callbacks.Type.Temporary, localClientId, response, userData);
		}

		internal void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, NativeMessageListenerHelper listener)
		{
			NearbyConnections.NearbyConnections_AcceptConnectionRequest(SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong)payload.Length), listener.AsPointer());
		}

		internal void DisconnectFromEndpoint(string remoteEndpointId)
		{
			NearbyConnections.NearbyConnections_Disconnect(SelfPtr(), remoteEndpointId);
		}

		internal void StopAllConnections()
		{
			NearbyConnections.NearbyConnections_Stop(SelfPtr());
		}

		internal void StartDiscovery(string serviceId, long duration, NativeEndpointDiscoveryListenerHelper listener)
		{
			NearbyConnections.NearbyConnections_StartDiscovery(SelfPtr(), serviceId, duration, listener.AsPointer());
		}

		internal void StopDiscovery(string serviceId)
		{
			NearbyConnections.NearbyConnections_StopDiscovery(SelfPtr(), serviceId);
		}

		internal void RejectConnectionRequest(string remoteEndpointId)
		{
			NearbyConnections.NearbyConnections_RejectConnectionRequest(SelfPtr(), remoteEndpointId);
		}

		internal void Shutdown()
		{
			NearbyConnections.NearbyConnections_Stop(SelfPtr());
		}

		internal string LocalEndpointId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnections.NearbyConnections_GetLocalEndpointId(SelfPtr(), out_arg, out_size));
		}

		internal string LocalDeviceId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnections.NearbyConnections_GetLocalDeviceId(SelfPtr(), out_arg, out_size));
		}

		internal static string ReadServiceId()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			Debug.Log((object)"Initializing ServiceId property!!!!");
			AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			try
			{
				AndroidJavaObject @static = val.GetStatic<AndroidJavaObject>("currentActivity");
				try
				{
					string text = @static.Call<string>("getPackageName", new object[0]);
					AndroidJavaObject val2 = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
					AndroidJavaObject val3 = val2.Call<AndroidJavaObject>("getApplicationInfo", new object[2]
					{
						text,
						128
					});
					AndroidJavaObject val4 = val3.Get<AndroidJavaObject>("metaData");
					string text2 = val4.Call<string>("getString", new object[1]
					{
						"com.google.android.gms.nearby.connection.SERVICE_ID"
					});
					Debug.Log((object)("SystemId from Manifest: " + text2));
					return text2;
					IL_00ad:
					string result;
					return result;
				}
				finally
				{
					((IDisposable)@static)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}
}

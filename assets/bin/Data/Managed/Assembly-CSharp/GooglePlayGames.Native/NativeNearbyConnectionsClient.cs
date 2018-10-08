using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.Native
{
	internal class NativeNearbyConnectionsClient : INearbyConnectionClient
	{
		protected class OnGameThreadMessageListener : IMessageListener
		{
			private readonly IMessageListener mListener;

			public OnGameThreadMessageListener(IMessageListener listener)
			{
				mListener = Misc.CheckNotNull(listener);
			}

			public unsafe void OnMessageReceived(string remoteEndpointId, byte[] data, bool isReliableMessage)
			{
				//IL_0029: Unknown result type (might be due to invalid IL or missing references)
				//IL_002e: Expected O, but got Unknown
				_003COnMessageReceived_003Ec__AnonStorey7FA _003COnMessageReceived_003Ec__AnonStorey7FA;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003COnMessageReceived_003Ec__AnonStorey7FA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void OnRemoteEndpointDisconnected(string remoteEndpointId)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003COnRemoteEndpointDisconnected_003Ec__AnonStorey7FB _003COnRemoteEndpointDisconnected_003Ec__AnonStorey7FB;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003COnRemoteEndpointDisconnected_003Ec__AnonStorey7FB, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		protected class OnGameThreadDiscoveryListener : IDiscoveryListener
		{
			private readonly IDiscoveryListener mListener;

			public OnGameThreadDiscoveryListener(IDiscoveryListener listener)
			{
				mListener = Misc.CheckNotNull(listener);
			}

			public unsafe void OnEndpointFound(EndpointDetails discoveredEndpoint)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003COnEndpointFound_003Ec__AnonStorey7FC _003COnEndpointFound_003Ec__AnonStorey7FC;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003COnEndpointFound_003Ec__AnonStorey7FC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}

			public unsafe void OnEndpointLost(string lostEndpointId)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003COnEndpointLost_003Ec__AnonStorey7FD _003COnEndpointLost_003Ec__AnonStorey7FD;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003COnEndpointLost_003Ec__AnonStorey7FD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		private readonly NearbyConnectionsManager mManager;

		internal NativeNearbyConnectionsClient(NearbyConnectionsManager manager)
		{
			mManager = Misc.CheckNotNull(manager);
		}

		public int MaxUnreliableMessagePayloadLength()
		{
			return 1168;
		}

		public int MaxReliableMessagePayloadLength()
		{
			return 4096;
		}

		public void SendReliable(List<string> recipientEndpointIds, byte[] payload)
		{
			InternalSend(recipientEndpointIds, payload, true);
		}

		public void SendUnreliable(List<string> recipientEndpointIds, byte[] payload)
		{
			InternalSend(recipientEndpointIds, payload, false);
		}

		private void InternalSend(List<string> recipientEndpointIds, byte[] payload, bool isReliable)
		{
			if (recipientEndpointIds == null)
			{
				throw new ArgumentNullException("recipientEndpointIds");
			}
			if (payload == null)
			{
				throw new ArgumentNullException("payload");
			}
			if (recipientEndpointIds.Contains(null))
			{
				throw new InvalidOperationException("Cannot send a message to a null recipient");
			}
			if (recipientEndpointIds.Count == 0)
			{
				Logger.w("Attempted to send a reliable message with no recipients");
			}
			else
			{
				if (isReliable)
				{
					if (payload.Length > MaxReliableMessagePayloadLength())
					{
						throw new InvalidOperationException("cannot send more than " + MaxReliableMessagePayloadLength() + " bytes");
					}
				}
				else if (payload.Length > MaxUnreliableMessagePayloadLength())
				{
					throw new InvalidOperationException("cannot send more than " + MaxUnreliableMessagePayloadLength() + " bytes");
				}
				foreach (string recipientEndpointId in recipientEndpointIds)
				{
					if (isReliable)
					{
						mManager.SendReliable(recipientEndpointId, payload);
					}
					else
					{
						mManager.SendUnreliable(recipientEndpointId, payload);
					}
				}
			}
		}

		public unsafe void StartAdvertising(string name, List<string> appIdentifiers, TimeSpan? advertisingDuration, Action<AdvertisingResult> resultCallback, Action<ConnectionRequest> requestCallback)
		{
			Misc.CheckNotNull(appIdentifiers, "appIdentifiers");
			Misc.CheckNotNull(resultCallback, "resultCallback");
			Misc.CheckNotNull(requestCallback, "connectionRequestCallback");
			if (advertisingDuration.HasValue && advertisingDuration.Value.Ticks < 0)
			{
				throw new InvalidOperationException("advertisingDuration must be positive");
			}
			resultCallback = Callbacks.AsOnGameThreadCallback(resultCallback);
			requestCallback = Callbacks.AsOnGameThreadCallback(requestCallback);
			_003CStartAdvertising_003Ec__AnonStorey7F6 _003CStartAdvertising_003Ec__AnonStorey7F;
			mManager.StartAdvertising(name, appIdentifiers.Select<string, NativeAppIdentifier>(new Func<string, NativeAppIdentifier>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList(), ToTimeoutMillis(advertisingDuration), new Action<long, NativeStartAdvertisingResult>((object)_003CStartAdvertising_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<long, NativeConnectionRequest>((object)_003CStartAdvertising_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private static long ToTimeoutMillis(TimeSpan? span)
		{
			return (!span.HasValue) ? 0 : PInvokeUtilities.ToMilliseconds(span.Value);
		}

		public void StopAdvertising()
		{
			mManager.StopAdvertising();
		}

		public unsafe void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<ConnectionResponse> responseCallback, IMessageListener listener)
		{
			Misc.CheckNotNull(remoteEndpointId, "remoteEndpointId");
			Misc.CheckNotNull(payload, "payload");
			Misc.CheckNotNull(responseCallback, "responseCallback");
			Misc.CheckNotNull(listener, "listener");
			responseCallback = Callbacks.AsOnGameThreadCallback(responseCallback);
			using (NativeMessageListenerHelper listener2 = ToMessageListener(listener))
			{
				_003CSendConnectionRequest_003Ec__AnonStorey7F7 _003CSendConnectionRequest_003Ec__AnonStorey7F;
				mManager.SendConnectionRequest(name, remoteEndpointId, payload, new Action<long, NativeConnectionResponse>((object)_003CSendConnectionRequest_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), listener2);
			}
		}

		private unsafe static NativeMessageListenerHelper ToMessageListener(IMessageListener listener)
		{
			listener = new OnGameThreadMessageListener(listener);
			NativeMessageListenerHelper nativeMessageListenerHelper = new NativeMessageListenerHelper();
			nativeMessageListenerHelper.SetOnMessageReceivedCallback(delegate(long localClientId, string endpointId, byte[] data, bool isReliable)
			{
				listener.OnMessageReceived(endpointId, data, isReliable);
			});
			_003CToMessageListener_003Ec__AnonStorey7F8 _003CToMessageListener_003Ec__AnonStorey7F;
			nativeMessageListenerHelper.SetOnDisconnectedCallback(new Action<long, string>((object)_003CToMessageListener_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			return nativeMessageListenerHelper;
		}

		public void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, IMessageListener listener)
		{
			Misc.CheckNotNull(remoteEndpointId, "remoteEndpointId");
			Misc.CheckNotNull(payload, "payload");
			Misc.CheckNotNull(listener, "listener");
			Logger.d("Calling AcceptConncectionRequest");
			mManager.AcceptConnectionRequest(remoteEndpointId, payload, ToMessageListener(listener));
			Logger.d("Called!");
		}

		public void StartDiscovery(string serviceId, TimeSpan? advertisingTimeout, IDiscoveryListener listener)
		{
			Misc.CheckNotNull(serviceId, "serviceId");
			Misc.CheckNotNull(listener, "listener");
			using (NativeEndpointDiscoveryListenerHelper listener2 = ToDiscoveryListener(listener))
			{
				mManager.StartDiscovery(serviceId, ToTimeoutMillis(advertisingTimeout), listener2);
			}
		}

		private unsafe static NativeEndpointDiscoveryListenerHelper ToDiscoveryListener(IDiscoveryListener listener)
		{
			listener = new OnGameThreadDiscoveryListener(listener);
			NativeEndpointDiscoveryListenerHelper nativeEndpointDiscoveryListenerHelper = new NativeEndpointDiscoveryListenerHelper();
			_003CToDiscoveryListener_003Ec__AnonStorey7F9 _003CToDiscoveryListener_003Ec__AnonStorey7F;
			nativeEndpointDiscoveryListenerHelper.SetOnEndpointFound(new Action<long, NativeEndpointDetails>((object)_003CToDiscoveryListener_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			nativeEndpointDiscoveryListenerHelper.SetOnEndpointLostCallback(new Action<long, string>((object)_003CToDiscoveryListener_003Ec__AnonStorey7F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			return nativeEndpointDiscoveryListenerHelper;
		}

		public void StopDiscovery(string serviceId)
		{
			Misc.CheckNotNull(serviceId, "serviceId");
			mManager.StopDiscovery(serviceId);
		}

		public void RejectConnectionRequest(string requestingEndpointId)
		{
			Misc.CheckNotNull(requestingEndpointId, "requestingEndpointId");
			mManager.RejectConnectionRequest(requestingEndpointId);
		}

		public void DisconnectFromEndpoint(string remoteEndpointId)
		{
			mManager.DisconnectFromEndpoint(remoteEndpointId);
		}

		public void StopAllConnections()
		{
			mManager.StopAllConnections();
		}

		public string LocalEndpointId()
		{
			return mManager.LocalEndpointId();
		}

		public string LocalDeviceId()
		{
			return mManager.LocalDeviceId();
		}

		public string GetAppBundleId()
		{
			return mManager.AppBundleId;
		}

		public string GetServiceId()
		{
			return NearbyConnectionsManager.ServiceId;
		}
	}
}

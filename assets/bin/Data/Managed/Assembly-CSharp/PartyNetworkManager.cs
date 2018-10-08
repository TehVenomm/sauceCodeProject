using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyNetworkManager : MonoBehaviourSingleton<PartyNetworkManager>
{
	public class Pool_List_CoopPacket
	{
	}

	public class ConnectData
	{
		public string path = string.Empty;

		public List<int> ports = new List<int>();

		public int fromId;

		public int ackPrefix;

		public string roomId = string.Empty;

		public int owner;

		public string ownerToken = string.Empty;

		public int uid;

		public string signature = string.Empty;
	}

	private const float CONNECT_TIMEOUT = 15f;

	private const float ALIVE_SENDTIME = 20f;

	private int sendId;

	public Party_Model_RegisterACK registerAck;

	private PartyPacketReceiver packetReceiver
	{
		get;
		set;
	}

	private ChatPartyConnection chatConnection
	{
		get;
		set;
	}

	public static void ClearPoolObjects()
	{
		rymTPool<List<CoopPacket>>.Clear();
	}

	public ChatPartyConnection CreateChatConnection()
	{
		if (chatConnection == null)
		{
			chatConnection = new ChatPartyConnection();
		}
		return chatConnection;
	}

	protected override void Awake()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		packetReceiver = this.get_gameObject().AddComponent<PartyPacketReceiver>();
	}

	private void Update()
	{
		packetReceiver.OnUpdate();
		if (CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected())
		{
			float num = Time.get_time() - MonoBehaviourSingleton<PartyWebSocket>.I.packetSendTime;
			if (num >= 20f)
			{
				Alive();
			}
		}
	}

	public void EraseAllPackets()
	{
		packetReceiver.EraseAllPackets();
	}

	public void Clear()
	{
		sendId = 0;
		registerAck = null;
		EraseAllPackets();
	}

	private void Logd(string str, params object[] objs)
	{
		if (!Log.enabled)
		{
			return;
		}
	}

	private string GetRelayServerPath(string path, int port)
	{
		UriBuilder uriBuilder = new UriBuilder(path);
		uriBuilder.Port = port;
		return uriBuilder.Uri.ToString();
	}

	public void Connect(ConnectData conn_data, Action<bool> call_back)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(RequestCoroutineConnect(conn_data, call_back));
	}

	private IEnumerator RequestCoroutineConnect(ConnectData conn_data, Action<bool> call_back)
	{
		yield return (object)this.StartCoroutine(RequestCoroutineClose(1000, "Bye!", null));
		if (string.IsNullOrEmpty(conn_data.path))
		{
			Logd("Connect fail. nothing connection path...");
			call_back?.Invoke(false);
		}
		else
		{
			if (conn_data.ports.Count == 0)
			{
				Uri uri = new Uri(conn_data.path);
				conn_data.ports.Add(uri.Port);
			}
			bool is_success = false;
			foreach (int port in conn_data.ports)
			{
				float timeoutTimer = 15f;
				string connectPath = GetRelayServerPath(conn_data.path, port);
				Logd("Connect. path={0}", connectPath);
				MonoBehaviourSingleton<PartyWebSocket>.I.Connect(connectPath, conn_data.fromId, conn_data.ackPrefix);
				while (!MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected() && 0f < timeoutTimer && MonoBehaviourSingleton<PartyWebSocket>.I.CurrentConnectionStatus != CoopWebSocketSingleton<PartyWebSocket>.CONNECTION_STATUS.ERROR)
				{
					timeoutTimer -= Time.get_deltaTime();
					yield return (object)new WaitForEndOfFrame();
				}
				if (MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
				{
					is_success = true;
					RegisterPacketReceiveAction();
					break;
				}
			}
			call_back?.Invoke(is_success);
		}
	}

	public void Close(ushort code = 1000, string msg = "Bye!", Action call_back = null)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Logd("Close.");
		this.StartCoroutine(RequestCoroutineClose(code, msg, call_back));
	}

	private IEnumerator RequestCoroutineClose(ushort code = 1000, string msg = "Bye!", Action call_back = null)
	{
		if (MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
		{
			MonoBehaviourSingleton<PartyWebSocket>.I.Close(code, msg);
			while (MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
			{
				yield return (object)new WaitForEndOfFrame();
			}
		}
		Clear();
		if (call_back != null)
		{
			call_back.Invoke();
		}
	}

	public unsafe void Regist(ConnectData conn_data, Action<bool> call_back)
	{
		Party_Model_Register party_Model_Register = new Party_Model_Register();
		party_Model_Register.roomId = conn_data.roomId;
		party_Model_Register.owner = conn_data.owner;
		party_Model_Register.ownerToken = conn_data.ownerToken;
		party_Model_Register.uid = conn_data.uid;
		party_Model_Register.signature = conn_data.signature;
		Logd("Regist. roomId={0}", conn_data.roomId);
		registerAck = null;
		_003CRegist_003Ec__AnonStorey54F _003CRegist_003Ec__AnonStorey54F;
		SendServer(party_Model_Register, true, new Func<Coop_Model_ACK, bool>((object)_003CRegist_003Ec__AnonStorey54F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
	}

	public void ConnectAndRegist(ConnectData conn_data, Action<bool, bool> call_back)
	{
		Connect(conn_data, delegate(bool is_connect)
		{
			Logd("Connected. valid={0}", is_connect);
			if (!is_connect)
			{
				if (call_back != null)
				{
					call_back.Invoke(is_connect, false);
				}
			}
			else
			{
				Regist(conn_data, delegate(bool is_regist)
				{
					Logd("Registed. valid={0}", is_regist);
					if (call_back != null)
					{
						call_back.Invoke(is_connect, is_regist);
					}
				});
			}
		});
	}

	public void Disconnect(ushort code)
	{
		Coop_Model_Disconnect coop_Model_Disconnect = new Coop_Model_Disconnect();
		coop_Model_Disconnect.code = code;
		SendServer(coop_Model_Disconnect, false, null, null);
	}

	public void Alive()
	{
		Coop_Model_Alive model = new Coop_Model_Alive();
		SendServer(model, false, null, null);
	}

	public void ChatMessage(string message)
	{
		if (UserInfoManager.IsValidUser())
		{
			Coop_Model_StageChatMessage coop_Model_StageChatMessage = new Coop_Model_StageChatMessage();
			coop_Model_StageChatMessage.id = 1004;
			coop_Model_StageChatMessage.text = message;
			coop_Model_StageChatMessage.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			coop_Model_StageChatMessage.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			SendBroadcast(coop_Model_StageChatMessage, false, null, null);
		}
	}

	public void ChatStamp(int stamp_id)
	{
		if (UserInfoManager.IsValidUser())
		{
			Coop_Model_StageChatStamp coop_Model_StageChatStamp = new Coop_Model_StageChatStamp();
			coop_Model_StageChatStamp.id = 1004;
			coop_Model_StageChatStamp.stamp_id = stamp_id;
			coop_Model_StageChatStamp.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			coop_Model_StageChatStamp.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			SendBroadcast(coop_Model_StageChatStamp, false, null, null);
		}
	}

	public void SyncSend()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (sendId > 0)
		{
			this.StartCoroutine(CoroutineSyncSend(sendId));
		}
	}

	public IEnumerator CoroutineSyncSend(int id)
	{
		while (!MonoBehaviourSingleton<PartyWebSocket>.I.IsCompleteSend(id) && MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
		{
			Logd("Sync send. id={0}", id);
			yield return (object)new WaitForEndOfFrame();
		}
	}

	private int Send(int to_client_id, Coop_Model_Base model, Type type, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		if (!MonoBehaviourSingleton<PartyWebSocket>.I.IsConnected())
		{
			return -1;
		}
		sendId = 0;
		sendId = MonoBehaviourSingleton<PartyWebSocket>.I.Send(model, type, to_client_id, promise, onReceiveAck, onPreResend);
		return sendId;
	}

	public int SendServer<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(-1000, model, typeof(T), promise, onReceiveAck, onPreResend);
	}

	public int SendTo<T>(int to_client_id, T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(to_client_id, model, typeof(T), promise, onReceiveAck, onPreResend);
	}

	public int SendBroadcast<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (model.id == -1)
		{
			Log.Warning(LOG.COOP, "PartyNetwork(" + ((CoopWebSocketSingleton<PartyWebSocket>)MonoBehaviourSingleton<PartyWebSocket>.I).IsConnected() + "): model.id not set...");
			return -1;
		}
		return Send(-2000, model, typeof(T), promise, onReceiveAck, onPreResend);
	}

	private unsafe void RegisterPacketReceiveAction()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		PartyWebSocket i = MonoBehaviourSingleton<PartyWebSocket>.I;
		i.ReceivePacketAction = (Action<CoopPacket>)Delegate.Combine(i.ReceivePacketAction, (Action<CoopPacket>)delegate(CoopPacket packet)
		{
			if (packet.destObjectId != -1 && packet.packetType != PACKET_TYPE.HEARTBEAT)
			{
				packetReceiver.Set(packet);
			}
		});
		PartyWebSocket i2 = MonoBehaviourSingleton<PartyWebSocket>.I;
		i2.PrepareCloseOccurred = Delegate.Combine((Delegate)i2.PrepareCloseOccurred, (Delegate)new Action<ushort, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		PartyWebSocket i3 = MonoBehaviourSingleton<PartyWebSocket>.I;
		i3.CloseOccurred = Delegate.Combine((Delegate)i3.CloseOccurred, (Delegate)new Action<ushort, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		PartyWebSocket i4 = MonoBehaviourSingleton<PartyWebSocket>.I;
		i4.ErrorOccurred = (Action<Exception>)Delegate.Combine(i4.ErrorOccurred, (Action<Exception>)delegate(Exception ex)
		{
			Logd("ErrorOccurred. ex={0}", ex);
			LoopBackRoomLeave();
		});
		PartyWebSocket i5 = MonoBehaviourSingleton<PartyWebSocket>.I;
		i5.HeartbeatDisconnected = Delegate.Combine((Delegate)i5.HeartbeatDisconnected, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public static CoopPacket CreateLoopBackRoomLeavedPacket()
	{
		Party_Model_RoomLeaved party_Model_RoomLeaved = new Party_Model_RoomLeaved();
		party_Model_RoomLeaved.id = 1000;
		party_Model_RoomLeaved.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		party_Model_RoomLeaved.token = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
		return CoopPacket.Create(party_Model_RoomLeaved, -1000, -2000, false, -8989);
	}

	public void LoopBackRoomLeave()
	{
		CoopPacket coopPacket = CreateLoopBackRoomLeavedPacket();
		Logd("LoopBackRoomLeave. is_connect={0}", CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected());
		if (CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected())
		{
			MonoBehaviourSingleton<PartyWebSocket>.I.ReceivePacketAction(coopPacket);
		}
		else
		{
			packetReceiver.ForcePacketProcess(coopPacket);
		}
	}

	public bool OnRecvRoomJoined(Party_Model_RoomJoined model)
	{
		Logd("OnRecvRoomJoined. cid={0}", model.cid);
		return true;
	}

	public bool OnRecvRoomLeaved(Party_Model_RoomLeaved model)
	{
		Logd("OnRecvRoomLeaved. cid={0}", model.cid);
		if (model.cid != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			string text = string.Empty;
			PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(model.cid);
			if (slotInfoByUserId != null)
			{
				text = slotInfoByUserId.userInfo.name;
			}
			else if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				InGameRecorder.PlayerRecord playerByUserId = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayerByUserId(model.cid);
				if (playerByUserId != null)
				{
					text = playerByUserId.charaInfo.name;
				}
			}
			if (chatConnection != null && !string.IsNullOrEmpty(text))
			{
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.CHAT, 6u, text));
			}
		}
		return true;
	}

	public bool OnRecvChatMessage(Coop_Model_StageChatMessage model)
	{
		Logd("OnRecvChatMessage. user_id={0},text={1}", model.user_id, model.text);
		if (!PartyManager.IsValidInParty())
		{
			return true;
		}
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(model.user_id);
		if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null)
		{
			return true;
		}
		if (chatConnection != null)
		{
			chatConnection.OnReceiveMessage(model.user_id, slotInfoByUserId.userInfo.name, model.text);
		}
		return true;
	}

	public bool OnRecvChatStamp(Coop_Model_StageChatStamp model)
	{
		Logd("OnRecvChatStamp. user_id={0},stamp_id={1}", model.user_id, model.stamp_id);
		if (!PartyManager.IsValidInParty())
		{
			return true;
		}
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(model.user_id);
		if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null)
		{
			return true;
		}
		if (chatConnection != null)
		{
			chatConnection.OnReceiveStamp(model.user_id, slotInfoByUserId.userInfo.name, model.stamp_id);
		}
		return true;
	}

	private IEnumerator OnApplicationPause(bool paused)
	{
		if (PartyManager.IsValidInParty() && MonoBehaviourSingleton<PartyWebSocket>.IsValid())
		{
			Logd("OnApplicationPause. pause={0}, is_connect={1}", paused, CoopWebSocketSingleton<PartyWebSocket>.IsValidConnected());
			if (paused)
			{
				MonoBehaviourSingleton<PartyWebSocket>.I.Close(1000, "Bye!");
			}
		}
		yield break;
	}
}

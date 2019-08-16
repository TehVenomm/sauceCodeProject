using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeNetworkManager : MonoBehaviourSingleton<LoungeNetworkManager>
{
	public class Pool_List_CoopPacket : rymTPool<List<CoopPacket>>
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

	private LoungePacketReceiver packetReceiver
	{
		get;
		set;
	}

	private ChatLoungeConnection chatConnection
	{
		get;
		set;
	}

	public static void ClearPoolObjects()
	{
		rymTPool<List<CoopPacket>>.Clear();
	}

	public ChatLoungeConnection CreateChatConnection()
	{
		if (chatConnection == null)
		{
			chatConnection = new ChatLoungeConnection();
		}
		return chatConnection;
	}

	protected override void Awake()
	{
		base.Awake();
		packetReceiver = this.get_gameObject().AddComponent<LoungePacketReceiver>();
	}

	private void Update()
	{
		packetReceiver.OnUpdate();
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			float num = Time.get_time() - MonoBehaviourSingleton<LoungeWebSocket>.I.packetSendTime;
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
		this.StartCoroutine(RequestCoroutineConnect(conn_data, call_back));
	}

	private IEnumerator RequestCoroutineConnect(ConnectData conn_data, Action<bool> call_back)
	{
		yield return this.StartCoroutine(RequestCoroutineClose(1000));
		if (string.IsNullOrEmpty(conn_data.path))
		{
			Logd("Connect fail. nothing connection path...");
			call_back?.Invoke(obj: false);
			yield break;
		}
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
			MonoBehaviourSingleton<LoungeWebSocket>.I.Connect(connectPath, conn_data.fromId, conn_data.ackPrefix);
			while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected() && 0f < timeoutTimer && MonoBehaviourSingleton<LoungeWebSocket>.I.CurrentConnectionStatus != CoopWebSocketSingleton<LoungeWebSocket>.CONNECTION_STATUS.ERROR)
			{
				timeoutTimer -= Time.get_deltaTime();
				yield return (object)new WaitForEndOfFrame();
			}
			if (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
			{
				is_success = true;
				RegisterPacketReceiveAction();
				break;
			}
		}
		call_back?.Invoke(is_success);
	}

	public void Close(ushort code = 1000, string msg = "Bye!", Action call_back = null)
	{
		Logd("Close.");
		this.StartCoroutine(RequestCoroutineClose(code, msg, call_back));
	}

	private IEnumerator RequestCoroutineClose(ushort code = 1000, string msg = "Bye!", Action call_back = null)
	{
		if (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
		{
			MonoBehaviourSingleton<LoungeWebSocket>.I.Close(code, msg);
			while (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
			{
				yield return (object)new WaitForEndOfFrame();
			}
		}
		Clear();
		call_back?.Invoke();
	}

	public void Regist(ConnectData conn_data, Action<bool> call_back)
	{
		Party_Model_Register party_Model_Register = new Party_Model_Register();
		party_Model_Register.roomId = conn_data.roomId;
		party_Model_Register.owner = conn_data.owner;
		party_Model_Register.ownerToken = conn_data.ownerToken;
		party_Model_Register.uid = conn_data.uid;
		party_Model_Register.signature = conn_data.signature;
		Logd("Regist. roomId={0}", conn_data.roomId);
		registerAck = null;
		SendServer(party_Model_Register, promise: true, delegate(Coop_Model_ACK ack)
		{
			bool obj = true;
			registerAck = (ack as Party_Model_RegisterACK);
			if (ack == null || !ack.positive)
			{
				obj = false;
				MonoBehaviourSingleton<LoungeWebSocket>.I.Close(1000);
			}
			if (call_back != null)
			{
				call_back(obj);
			}
			return true;
		});
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
					call_back(is_connect, arg2: false);
				}
			}
			else
			{
				Regist(conn_data, delegate(bool is_regist)
				{
					Logd("Registed. valid={0}", is_regist);
					if (call_back != null)
					{
						call_back(is_connect, is_regist);
					}
				});
			}
		});
	}

	public void Disconnect(ushort code)
	{
		Coop_Model_Disconnect coop_Model_Disconnect = new Coop_Model_Disconnect();
		coop_Model_Disconnect.code = code;
		SendServer(coop_Model_Disconnect, promise: false);
	}

	public void Alive()
	{
		Coop_Model_Alive model = new Coop_Model_Alive();
		SendServer(model, promise: false);
	}

	public void ChatMessage(string message)
	{
		if (UserInfoManager.IsValidUser())
		{
			Coop_Model_StageChatMessage coop_Model_StageChatMessage = new Coop_Model_StageChatMessage();
			coop_Model_StageChatMessage.id = 1005;
			coop_Model_StageChatMessage.text = message;
			coop_Model_StageChatMessage.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			coop_Model_StageChatMessage.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			SendBroadcast(coop_Model_StageChatMessage);
		}
	}

	public void ChatStamp(int stamp_id)
	{
		if (UserInfoManager.IsValidUser())
		{
			Coop_Model_StageChatStamp coop_Model_StageChatStamp = new Coop_Model_StageChatStamp();
			coop_Model_StageChatStamp.id = 1005;
			coop_Model_StageChatStamp.stamp_id = stamp_id;
			coop_Model_StageChatStamp.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			coop_Model_StageChatStamp.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			SendBroadcast(coop_Model_StageChatStamp);
		}
	}

	public void RoomPosition(int targetUserId, Vector3 position, LOUNGE_ACTION_TYPE type)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Lounge_Model_RoomPosition lounge_Model_RoomPosition = new Lounge_Model_RoomPosition();
		lounge_Model_RoomPosition.id = 1005;
		lounge_Model_RoomPosition.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomPosition.pos = position;
		lounge_Model_RoomPosition.aid = (int)type;
		Send(targetUserId, lounge_Model_RoomPosition, typeof(Lounge_Model_RoomPosition));
	}

	public void JoinNotification(CharaInfo userInfo)
	{
		if (chatConnection != null && !string.IsNullOrEmpty(userInfo.name))
		{
			chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 10u, userInfo.name), string.Empty);
		}
	}

	public void SyncSend()
	{
		if (sendId > 0)
		{
			this.StartCoroutine(CoroutineSyncSend(sendId));
		}
	}

	public IEnumerator CoroutineSyncSend(int id)
	{
		while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsCompleteSend(id) && MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
		{
			Logd("Sync send. id={0}", id);
			yield return (object)new WaitForEndOfFrame();
		}
	}

	private int Send(int to_client_id, Coop_Model_Base model, Type type, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		if (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
		{
			return -1;
		}
		sendId = 0;
		sendId = MonoBehaviourSingleton<LoungeWebSocket>.I.Send(model, type, to_client_id, promise, onReceiveAck, onPreResend);
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

	public int SendBroadcast<T>(T model, bool promise = false, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (model.id == -1)
		{
			Log.Warning(LOG.COOP, "LoungeNetwork(" + MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected() + "): model.id not set...");
			return -1;
		}
		return Send(-2000, model, typeof(T), promise, onReceiveAck, onPreResend);
	}

	private void RegisterPacketReceiveAction()
	{
		LoungeWebSocket i = MonoBehaviourSingleton<LoungeWebSocket>.I;
		i.ReceivePacketAction = (Action<CoopPacket>)Delegate.Combine(i.ReceivePacketAction, (Action<CoopPacket>)delegate(CoopPacket packet)
		{
			if (packet.destObjectId != -1 && packet.packetType != PACKET_TYPE.HEARTBEAT)
			{
				packetReceiver.Set(packet);
			}
		});
		LoungeWebSocket i2 = MonoBehaviourSingleton<LoungeWebSocket>.I;
		i2.PrepareCloseOccurred = (Action<ushort, string>)Delegate.Combine(i2.PrepareCloseOccurred, (Action<ushort, string>)delegate(ushort code, string msg)
		{
			Logd("PrepareCloseOccurred. code={0}, msg={1}", code, msg);
			Disconnect(code);
		});
		LoungeWebSocket i3 = MonoBehaviourSingleton<LoungeWebSocket>.I;
		i3.CloseOccurred = (Action<ushort, string>)Delegate.Combine(i3.CloseOccurred, (Action<ushort, string>)delegate(ushort code, string msg)
		{
			Logd("CloseOccurred. code={0}, msg={1}", code, msg);
			LoopBackRoomLeave();
		});
		LoungeWebSocket i4 = MonoBehaviourSingleton<LoungeWebSocket>.I;
		i4.ErrorOccurred = (Action<Exception>)Delegate.Combine(i4.ErrorOccurred, (Action<Exception>)delegate(Exception ex)
		{
			Logd("ErrorOccurred. ex={0}", ex);
			LoopBackRoomLeave();
		});
		LoungeWebSocket i5 = MonoBehaviourSingleton<LoungeWebSocket>.I;
		i5.HeartbeatDisconnected = (Action)Delegate.Combine(i5.HeartbeatDisconnected, (Action)delegate
		{
			Logd("HeartbeatDisconnected.");
			LoopBackRoomLeave();
		});
	}

	public static CoopPacket CreateLoopBackRoomLeavedPacket()
	{
		Lounge_Model_RoomLeaved lounge_Model_RoomLeaved = new Lounge_Model_RoomLeaved();
		lounge_Model_RoomLeaved.id = 1000;
		lounge_Model_RoomLeaved.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomLeaved.token = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
		return CoopPacket.Create(lounge_Model_RoomLeaved, -1000, -2000, promise: false, -8989);
	}

	public void LoopBackRoomLeave()
	{
		CoopPacket coopPacket = CreateLoopBackRoomLeavedPacket();
		Logd("LoopBackRoomLeave. is_connect={0}", CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected());
		if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
		{
			MonoBehaviourSingleton<LoungeWebSocket>.I.ReceivePacketAction(coopPacket);
		}
		else
		{
			packetReceiver.ForcePacketProcess(coopPacket);
		}
	}

	public bool OnRecvRoomJoined(Lounge_Model_RoomJoined model)
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		Logd("OnRecvRoomJoined. cid={0}", model.cid);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomJoined(model.cid);
		}
		if (model.cid != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id && MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			if (!MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				return true;
			}
			if (MonoBehaviourSingleton<LoungeManager>.I.IHomePeople == null)
			{
				return true;
			}
			if (MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara == null)
			{
				return true;
			}
			Vector3 position = MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara._transform.get_position();
			LOUNGE_ACTION_TYPE actionType = MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara.GetActionType();
			RoomPosition(model.cid, position, actionType);
		}
		if (FieldManager.IsValidInGame())
		{
			Protocol.Try(delegate
			{
				MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
				{
				});
			});
		}
		string empty = string.Empty;
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
		if (slotInfoByUserId != null)
		{
			empty = slotInfoByUserId.userInfo.name;
		}
		else if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			InGameRecorder.PlayerRecord playerByUserId = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayerByUserId(model.cid);
			if (playerByUserId != null)
			{
				empty = playerByUserId.charaInfo.name;
			}
		}
		return true;
	}

	public bool OnRecvRoomLeaved(Lounge_Model_RoomLeaved model)
	{
		Logd("OnRecvRoomLeaved. cid={0}", model.cid);
		if (model.cid != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			string text = string.Empty;
			PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
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
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 11u, text), string.Empty);
			}
		}
		if (FieldManager.IsValidInGame())
		{
			Protocol.Try(delegate
			{
				MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
				{
				});
			});
		}
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomLeaved(model.cid);
		}
		return true;
	}

	public bool OnRecvRoomPoisition(Lounge_Model_RoomPosition model)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Logd("OnRecvRoomPosition. cid={0}, pos={1}", model.cid, model.pos);
		this.StartCoroutine(LoungeManagerRecvRoomPosition(model.cid, model.pos, model.aid));
		return true;
	}

	private IEnumerator LoungeManagerRecvRoomPosition(int userId, Vector3 pos, int aid)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		while (!MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			yield return null;
		}
		while (!MonoBehaviourSingleton<LoungeManager>.I.IsInitialized)
		{
			yield return null;
		}
		MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomPosition(userId, pos, (LOUNGE_ACTION_TYPE)aid);
	}

	public bool OnRecvRoomMove(Lounge_Model_RoomMove model)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		Logd("OnRecvRoomMove. cid={0}", model.cid);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomMove(model.cid, model.pos);
		}
		return true;
	}

	public bool OnRecvRoomAction(Lounge_Model_RoomAction model)
	{
		Logd("OnRecvRoomAction. cid={0}. aid={1}", model.cid, model.aid);
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomAction(model.cid, model.aid);
		}
		return true;
	}

	public bool OnRecvRoomKick(Lounge_Model_RoomKick model)
	{
		Logd("OnRecvKick. cId = {0}", model.cid);
		if (!LoungeMatchingManager.IsValidInLounge())
		{
			return true;
		}
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
		if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null)
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.Kick(model.cid);
		if (chatConnection != null)
		{
			chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 12u, slotInfoByUserId.userInfo.name), string.Empty);
		}
		return true;
	}

	public bool OnRecvRoomAFKKick(Lounge_Model_AFK_Kick model)
	{
		Logd("OnRecvAFKKick. cId = {0}", model.cid);
		if (!LoungeMatchingManager.IsValidInLounge())
		{
			return true;
		}
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
		if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null)
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.Kick(model.cid);
		if (chatConnection != null)
		{
			chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 19u, slotInfoByUserId.userInfo.name), string.Empty);
		}
		return true;
	}

	public bool OnRecvRoomHostChanged(Lounge_Model_RoomHostChanged model)
	{
		Logd("OnRecvHostChanged. hostId = {0}", model.hostid);
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.hostid))
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.ChangeOwner(model.hostid);
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.hostid);
		if (chatConnection != null)
		{
			chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 13u, slotInfoByUserId.userInfo.name), string.Empty);
		}
		return true;
	}

	public bool OnRecvMemberLounge(Lounge_Model_MemberLounge model)
	{
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveLounge(model.cid);
		return true;
	}

	public bool OnRecvMemberQuest(Lounge_Model_MemberQuest model)
	{
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveQuest(model);
		return true;
	}

	public bool OnRecvMemberField(Lounge_Model_MemberField model)
	{
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveField(model);
		return true;
	}

	public bool OnRecvMemberArena(Lounge_Model_MemberArena model)
	{
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
		{
			return true;
		}
		MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveArena(model);
		return true;
	}

	public bool OnRecvChatMessage(Coop_Model_StageChatMessage model)
	{
		Logd("OnRecvChatMessage. user_id={0},text={1}", model.user_id, model.text);
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.user_id))
		{
			return true;
		}
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.user_id);
		if (chatConnection != null)
		{
			chatConnection.OnReceiveMessage(model.user_id, slotInfoByUserId.userInfo.name, model.text, string.Empty);
		}
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeManager>.I.OnRecvChatMessage(model.user_id);
		}
		return true;
	}

	public bool OnRecvChatStamp(Coop_Model_StageChatStamp model)
	{
		Logd("OnRecvChatStamp. user_id={0},stamp_id={1}", model.user_id, model.stamp_id);
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.user_id))
		{
			return true;
		}
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.user_id);
		if (chatConnection != null)
		{
			chatConnection.OnReceiveStamp(model.user_id, slotInfoByUserId.userInfo.name, model.stamp_id, string.Empty);
		}
		return true;
	}

	public void MoveLoungeNotification(LoungeMemberStatus.MEMBER_STATUS beforeStatus, LoungeMemberStatus after)
	{
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(after.userId))
		{
			return;
		}
		LoungeMemberStatus.MEMBER_STATUS status = after.GetStatus();
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(after.userId);
		switch (beforeStatus)
		{
		case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
			switch (status)
			{
			case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
				if (after.isHost)
				{
					chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 14u, slotInfoByUserId.userInfo.name), string.Empty);
				}
				break;
			case LoungeMemberStatus.MEMBER_STATUS.FIELD:
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 15u, slotInfoByUserId.userInfo.name), string.Empty);
				break;
			case LoungeMemberStatus.MEMBER_STATUS.ARENA:
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 20u, slotInfoByUserId.userInfo.name), string.Empty);
				break;
			}
			break;
		case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
			if (status == LoungeMemberStatus.MEMBER_STATUS.QUEST)
			{
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 16u, slotInfoByUserId.userInfo.name), string.Empty);
			}
			break;
		case LoungeMemberStatus.MEMBER_STATUS.QUEST:
			if (status == LoungeMemberStatus.MEMBER_STATUS.LOUNGE)
			{
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 17u, slotInfoByUserId.userInfo.name), string.Empty);
			}
			break;
		case LoungeMemberStatus.MEMBER_STATUS.ARENA:
			if (status == LoungeMemberStatus.MEMBER_STATUS.LOUNGE)
			{
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 21u, slotInfoByUserId.userInfo.name), string.Empty);
			}
			break;
		default:
			if (status == LoungeMemberStatus.MEMBER_STATUS.LOUNGE)
			{
				chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 18u, slotInfoByUserId.userInfo.name), string.Empty);
			}
			break;
		}
	}
}

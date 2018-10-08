using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopNetworkManager : MonoBehaviourSingleton<CoopNetworkManager>
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

		public string token = string.Empty;
	}

	private const float CONNECT_TIMEOUT = 15f;

	private const float ALIVE_SENDTIME = 20f;

	private int sendId;

	public Coop_Model_RegisterACK registerAck;

	private DoubleUIntKeyTable<List<int>> recvPromisePacketSequenceNoTable = new DoubleUIntKeyTable<List<int>>();

	public CoopNetworkPacketReceiver packetReceiver
	{
		get;
		private set;
	}

	public static void ClearPoolObjects()
	{
		rymTPool<List<CoopPacket>>.Clear();
	}

	protected override void Awake()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		packetReceiver = this.get_gameObject().AddComponent<CoopNetworkPacketReceiver>();
	}

	private void Update()
	{
		packetReceiver.OnUpdate();
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			float num = Time.get_time() - MonoBehaviourSingleton<KtbWebSocket>.I.packetSendTime;
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
		recvPromisePacketSequenceNoTable.Clear();
	}

	private void Logd(string str, params object[] objs)
	{
		if (!Log.enabled)
		{
			return;
		}
	}

	public void SetRegisterSID(int sid)
	{
		if (registerAck != null)
		{
			Logd("SetRegisterSID. {0} => {1}", registerAck.sid, sid);
			registerAck.sid = sid;
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
				MonoBehaviourSingleton<KtbWebSocket>.I.Connect(connectPath, conn_data.fromId, conn_data.ackPrefix);
				while (!MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() && 0f < timeoutTimer && MonoBehaviourSingleton<KtbWebSocket>.I.CurrentConnectionStatus != CoopWebSocketSingleton<KtbWebSocket>.CONNECTION_STATUS.ERROR)
				{
					timeoutTimer -= Time.get_deltaTime();
					yield return (object)new WaitForEndOfFrame();
				}
				if (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
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
		if (MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen())
		{
			MonoBehaviourSingleton<KtbWebSocket>.I.Close(code, msg);
			while (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
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
		Coop_Model_Register coop_Model_Register = new Coop_Model_Register();
		coop_Model_Register.roomId = conn_data.roomId;
		coop_Model_Register.token = conn_data.token;
		Logd("Regist. roomId={0}, token={1}", conn_data.roomId, conn_data.token);
		registerAck = null;
		_003CRegist_003Ec__AnonStorey4E3 _003CRegist_003Ec__AnonStorey4E;
		SendServer(coop_Model_Register, true, new Func<Coop_Model_ACK, bool>((object)_003CRegist_003Ec__AnonStorey4E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
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

	public void Standby()
	{
		Coop_Model_Standby model = new Coop_Model_Standby();
		SendServer(model, false, null, null);
	}

	public void Resume()
	{
		Coop_Model_Resume model = new Coop_Model_Resume();
		SendServer(model, false, null, null);
	}

	public void Alive()
	{
		Coop_Model_Alive model = new Coop_Model_Alive();
		SendServer(model, false, null, null);
	}

	public void RoomEntryClose(int reason)
	{
		Coop_Model_RoomEntryClose coop_Model_RoomEntryClose = new Coop_Model_RoomEntryClose();
		coop_Model_RoomEntryClose.reason = reason;
		SendServer(coop_Model_RoomEntryClose, false, null, null);
	}

	public void RoomStageRequest()
	{
		Coop_Model_RoomStageRequest model = new Coop_Model_RoomStageRequest();
		SendServer(model, false, null, null);
	}

	public void RoomStageChange(int questId, int idx)
	{
		Coop_Model_RoomStageChange coop_Model_RoomStageChange = new Coop_Model_RoomStageChange();
		coop_Model_RoomStageChange.qId = questId;
		coop_Model_RoomStageChange.idx = idx;
		SendServer(coop_Model_RoomStageChange, false, null, null);
	}

	public void BattleStart()
	{
		Coop_Model_BattleStart model = new Coop_Model_BattleStart();
		SendServer(model, false, null, null);
	}

	public void EnemyAttack(int sid, int dmg)
	{
		Coop_Model_EnemyAttack coop_Model_EnemyAttack = new Coop_Model_EnemyAttack();
		coop_Model_EnemyAttack.sid = sid;
		coop_Model_EnemyAttack.dmg = dmg;
		SendServer(coop_Model_EnemyAttack, false, null, null);
	}

	public void EnemyOut(int sid, Vector3 pos)
	{
		Coop_Model_EnemyOut coop_Model_EnemyOut = new Coop_Model_EnemyOut();
		coop_Model_EnemyOut.sid = sid;
		coop_Model_EnemyOut.x = (int)pos.x;
		coop_Model_EnemyOut.z = (int)pos.z;
		SendServer(coop_Model_EnemyOut, false, null, null);
	}

	public void EnemyOutEscape(int sid, Vector3 pos)
	{
		Coop_Model_EnemyOut coop_Model_EnemyOut = new Coop_Model_EnemyOut();
		coop_Model_EnemyOut.sid = sid;
		coop_Model_EnemyOut.x = (int)pos.x;
		coop_Model_EnemyOut.z = (int)pos.z;
		coop_Model_EnemyOut.isEscape = true;
		SendServer(coop_Model_EnemyOut, false, null, null);
	}

	public void EnemyForcePop(PopSignatureInfo psig, Vector3 pos)
	{
		Coop_Model_EnemyForcePop coop_Model_EnemyForcePop = new Coop_Model_EnemyForcePop();
		coop_Model_EnemyForcePop.psig = psig.signature;
		coop_Model_EnemyForcePop.keyId = psig.popKeyId;
		coop_Model_EnemyForcePop.eid = psig.enemyId;
		coop_Model_EnemyForcePop.lv = psig.enemyLv;
		coop_Model_EnemyForcePop.popType = psig.enemyPopType;
		coop_Model_EnemyForcePop.x = pos.x;
		coop_Model_EnemyForcePop.z = pos.z;
		SendServer(coop_Model_EnemyForcePop, false, null, null);
	}

	public void RewardGet(int rewardId)
	{
		Coop_Model_RewardGet coop_Model_RewardGet = new Coop_Model_RewardGet();
		coop_Model_RewardGet.rewardId = rewardId;
		SendServer(coop_Model_RewardGet, false, null, null);
	}

	public void UpdateBoost()
	{
		Coop_Model_UpdateBoost coop_Model_UpdateBoost = new Coop_Model_UpdateBoost();
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			coop_Model_UpdateBoost.expUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.EXP_UP);
			coop_Model_UpdateBoost.moneyUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.MONEY_UP);
			coop_Model_UpdateBoost.dropUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.DROP_UP);
		}
		SendServer(coop_Model_UpdateBoost, false, null, null);
	}

	public void RoomTimeCheck(float elapsed_sec = 0)
	{
		Coop_Model_RoomTimeCheck coop_Model_RoomTimeCheck = new Coop_Model_RoomTimeCheck();
		coop_Model_RoomTimeCheck.elapsedSec = (int)elapsed_sec;
		SendServer(coop_Model_RoomTimeCheck, false, null, null);
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
		while (!MonoBehaviourSingleton<KtbWebSocket>.I.IsCompleteSend(id) && MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			Logd("Sync send. id={0}", id);
			yield return (object)new WaitForEndOfFrame();
		}
	}

	private int Send(int to_client_id, Coop_Model_Base model, Type type, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null, bool is_stage = false, bool is_battle = false)
	{
		if (!MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
		{
			return -1;
		}
		if (is_stage && !MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsStage())
		{
			return -1;
		}
		if (is_battle && !MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsBattle())
		{
			return -1;
		}
		sendId = 0;
		sendId = MonoBehaviourSingleton<KtbWebSocket>.I.Send(model, type, to_client_id, promise, onReceiveAck, onPreResend);
		return sendId;
	}

	public int SendServer<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (!((CoopWebSocketSingleton<KtbWebSocket>)MonoBehaviourSingleton<KtbWebSocket>.I).IsConnected() && MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
		{
			return MonoBehaviourSingleton<CoopOfflineManager>.I.Send(model, promise, onReceiveAck);
		}
		return Send(-1000, model, typeof(T), promise, onReceiveAck, onPreResend, false, false);
	}

	public int SendTo<T>(int to_client_id, T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(to_client_id, model, typeof(T), promise, onReceiveAck, onPreResend, false, false);
	}

	public int SendBroadcast<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(-2000, model, typeof(T), promise, onReceiveAck, onPreResend, false, false);
	}

	public int SendToInStage<T>(int to_client_id, T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(to_client_id, model, typeof(T), promise, onReceiveAck, onPreResend, true, false);
	}

	public int SendToInBattle<T>(int to_client_id, T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(to_client_id, model, typeof(T), promise, onReceiveAck, onPreResend, true, true);
	}

	public int SendBroadcastInStage<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(-2000, model, typeof(T), promise, onReceiveAck, onPreResend, true, false);
	}

	public int SendBroadcastInBattle<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		return Send(-2000, model, typeof(T), promise, onReceiveAck, onPreResend, true, true);
	}

	private unsafe void RegisterPacketReceiveAction()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		KtbWebSocket i = MonoBehaviourSingleton<KtbWebSocket>.I;
		i.ReceivePacketAction = (Action<CoopPacket>)Delegate.Combine(i.ReceivePacketAction, (Action<CoopPacket>)delegate(CoopPacket packet)
		{
			if (packet.destObjectId != -1 && packet.packetType != PACKET_TYPE.HEARTBEAT && (!packet.promise || !packet.model.IsPromiseOverAgainCheck() || !ReceivePromisePacketOverAgainCheck(packet)))
			{
				packetReceiver.Set(packet);
			}
		});
		KtbWebSocket i2 = MonoBehaviourSingleton<KtbWebSocket>.I;
		i2.PrepareCloseOccurred = Delegate.Combine((Delegate)i2.PrepareCloseOccurred, (Delegate)new Action<ushort, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		KtbWebSocket i3 = MonoBehaviourSingleton<KtbWebSocket>.I;
		i3.CloseOccurred = Delegate.Combine((Delegate)i3.CloseOccurred, (Delegate)new Action<ushort, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		KtbWebSocket i4 = MonoBehaviourSingleton<KtbWebSocket>.I;
		i4.ErrorOccurred = (Action<Exception>)Delegate.Combine(i4.ErrorOccurred, (Action<Exception>)delegate(Exception ex)
		{
			Logd("ErrorOccurred. ex={0}", ex);
			LoopBackRoomLeave(false);
			if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopOfflineManager>.I.Activate();
			}
		});
		KtbWebSocket i5 = MonoBehaviourSingleton<KtbWebSocket>.I;
		i5.HeartbeatDisconnected = Delegate.Combine((Delegate)i5.HeartbeatDisconnected, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private bool ReceivePromisePacketOverAgainCheck(CoopPacket packet)
	{
		if (packet.fromClientId <= 0)
		{
			return false;
		}
		List<int> list = recvPromisePacketSequenceNoTable.Get((uint)packet.fromClientId, (uint)packet.packetType);
		if (list != null && list.Find((int x) => x == packet.sequenceNo) > 0)
		{
			Logd("Receive promise packet over again!!. fromId={0}, packet={1}, no={2}", packet.fromClientId, packet.packetType, packet.sequenceNo);
			return true;
		}
		Logd("Receive promise packet over again add sequenceNo. fromId={0}, packet={1}, no={2}", packet.fromClientId, packet.packetType, packet.sequenceNo);
		if (list == null)
		{
			list = new List<int>();
			recvPromisePacketSequenceNoTable.Add((uint)packet.fromClientId, (uint)packet.packetType, list);
		}
		list.Add(packet.sequenceNo);
		return false;
	}

	public static CoopPacket CreateLoopBackRoomLeavedPacket()
	{
		Coop_Model_RoomLeaved coop_Model_RoomLeaved = new Coop_Model_RoomLeaved();
		coop_Model_RoomLeaved.id = 1000;
		coop_Model_RoomLeaved.cid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
		coop_Model_RoomLeaved.token = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userToken;
		coop_Model_RoomLeaved.stgid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.stageId;
		coop_Model_RoomLeaved.stghostid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
		return CoopPacket.Create(coop_Model_RoomLeaved, -1000, -2000, false, -8989);
	}

	public void LoopBackRoomLeave(bool is_force = false)
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			CoopPacket coopPacket = CreateLoopBackRoomLeavedPacket();
			Logd("LoopBackRoomLeave. is_connect={0}", CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && !is_force)
			{
				MonoBehaviourSingleton<KtbWebSocket>.I.ReceivePacketAction(coopPacket);
			}
			else
			{
				MonoBehaviourSingleton<CoopManager>.I.ForcePacketProcess(coopPacket);
			}
		}
	}

	public void KickRoomLeave()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			CoopPacket packet = CreateLoopBackRoomLeavedPacket();
			Logd("KickRoomLeave. is_connect={0}", CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
			MonoBehaviourSingleton<CoopManager>.I.ForcePacketProcess(packet);
		}
	}
}

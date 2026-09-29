// Decompiled with JetBrains decompiler
// Type: CoopNetworkManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopNetworkManager : MonoBehaviourSingleton<CoopNetworkManager>
{
  private const float CONNECT_TIMEOUT = 15f;
  private const float ALIVE_SENDTIME = 20f;
  private int sendId;
  public Coop_Model_RegisterACK registerAck;
  private DoubleUIntKeyTable<List<int>> recvPromisePacketSequenceNoTable = new DoubleUIntKeyTable<List<int>>();

  public static void ClearPoolObjects() => rymTPool<List<CoopPacket>>.Clear();

  public CoopNetworkPacketReceiver packetReceiver { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<CoopNetworkPacketReceiver>();
  }

  private void Update()
  {
    this.packetReceiver.OnUpdate();
    if (!CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() || (double) Time.time - (double) MonoBehaviourSingleton<KtbWebSocket>.I.packetSendTime < 20.0)
      return;
    this.Alive();
  }

  public void EraseAllPackets() => this.packetReceiver.EraseAllPackets();

  public void Clear()
  {
    this.sendId = 0;
    this.registerAck = (Coop_Model_RegisterACK) null;
    this.recvPromisePacketSequenceNoTable.Clear();
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public void SetRegisterSID(int sid)
  {
    if (this.registerAck == null)
      return;
    this.Logd("SetRegisterSID. {0} => {1}", (object) this.registerAck.sid, (object) sid);
    this.registerAck.sid = sid;
  }

  private string GetRelayServerPath(string path, int port)
  {
    return new UriBuilder(path) { Port = port }.Uri.ToString();
  }

  public void Connect(CoopNetworkManager.ConnectData conn_data, Action<bool> call_back)
  {
    this.StartCoroutine(this.RequestCoroutineConnect(conn_data, call_back));
  }

  private IEnumerator RequestCoroutineConnect(
    CoopNetworkManager.ConnectData conn_data,
    Action<bool> call_back)
  {
    yield return (object) this.StartCoroutine(this.RequestCoroutineClose());
    if (string.IsNullOrEmpty(conn_data.path))
    {
      this.Logd("Connect fail. nothing connection path...");
      if (call_back != null)
        call_back(false);
    }
    else
    {
      if (conn_data.ports.Count == 0)
        conn_data.ports.Add(new Uri(conn_data.path).Port);
      bool is_success = false;
      foreach (int port in conn_data.ports)
      {
        float timeoutTimer = 15f;
        string relayServerPath = this.GetRelayServerPath(conn_data.path, port);
        this.Logd("Connect. path={0}", (object) relayServerPath);
        MonoBehaviourSingleton<KtbWebSocket>.I.Connect(relayServerPath, conn_data.fromId, conn_data.ackPrefix);
        while (!MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() && 0.0 < (double) timeoutTimer && MonoBehaviourSingleton<KtbWebSocket>.I.CurrentConnectionStatus != CoopWebSocketSingleton<KtbWebSocket>.CONNECTION_STATUS.ERROR)
        {
          timeoutTimer -= Time.deltaTime;
          yield return (object) new WaitForEndOfFrame();
        }
        if (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
        {
          is_success = true;
          this.RegisterPacketReceiveAction();
          break;
        }
      }
      if (call_back != null)
        call_back(is_success);
    }
  }

  public void Close(ushort code = 1000, string msg = "Bye!", System.Action call_back = null)
  {
    this.Logd("Close.");
    this.StartCoroutine(this.RequestCoroutineClose(code, msg, call_back));
  }

  private IEnumerator RequestCoroutineClose(ushort code = 1000, string msg = "Bye!", System.Action call_back = null)
  {
    if (MonoBehaviourSingleton<KtbWebSocket>.I.IsOpen())
    {
      MonoBehaviourSingleton<KtbWebSocket>.I.Close(code, msg);
      while (MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
        yield return (object) new WaitForEndOfFrame();
    }
    this.Clear();
    if (call_back != null)
      call_back();
  }

  public void Regist(CoopNetworkManager.ConnectData conn_data, Action<bool> call_back)
  {
    Coop_Model_Register model = new Coop_Model_Register();
    model.roomId = conn_data.roomId;
    model.token = conn_data.token;
    this.Logd("Regist. roomId={0}, token={1}", (object) conn_data.roomId, (object) conn_data.token);
    this.registerAck = (Coop_Model_RegisterACK) null;
    this.SendServer<Coop_Model_Register>(model, onReceiveAck: (Func<Coop_Model_ACK, bool>) (ack =>
    {
      bool flag = true;
      this.registerAck = ack as Coop_Model_RegisterACK;
      if (ack == null || !ack.positive)
      {
        flag = false;
        MonoBehaviourSingleton<KtbWebSocket>.I.Close();
      }
      if (call_back != null)
        call_back(flag);
      return true;
    }));
  }

  public void ConnectAndRegist(
    CoopNetworkManager.ConnectData conn_data,
    Action<bool, bool> call_back)
  {
    this.Connect(conn_data, (Action<bool>) (is_connect =>
    {
      this.Logd("Connected. valid={0}", (object) is_connect);
      if (!is_connect)
      {
        if (call_back == null)
          return;
        call_back(is_connect, false);
      }
      else
        this.Regist(conn_data, (Action<bool>) (is_regist =>
        {
          this.Logd("Registed. valid={0}", (object) is_regist);
          if (call_back == null)
            return;
          call_back(is_connect, is_regist);
        }));
    }));
  }

  public void Disconnect(ushort code)
  {
    this.SendServer<Coop_Model_Disconnect>(new Coop_Model_Disconnect()
    {
      code = (int) code
    }, false);
  }

  public void Standby() => this.SendServer<Coop_Model_Standby>(new Coop_Model_Standby(), false);

  public void Resume() => this.SendServer<Coop_Model_Resume>(new Coop_Model_Resume(), false);

  public void Alive() => this.SendServer<Coop_Model_Alive>(new Coop_Model_Alive(), false);

  public void RoomEntryClose(int reason)
  {
    this.SendServer<Coop_Model_RoomEntryClose>(new Coop_Model_RoomEntryClose()
    {
      reason = reason
    }, false);
  }

  public void RoomStageRequest()
  {
    this.SendServer<Coop_Model_RoomStageRequest>(new Coop_Model_RoomStageRequest(), false);
  }

  public void RoomStageChange(int questId, int idx)
  {
    this.SendServer<Coop_Model_RoomStageChange>(new Coop_Model_RoomStageChange()
    {
      qId = questId,
      idx = idx
    }, false);
  }

  public void BattleStart()
  {
    this.SendServer<Coop_Model_BattleStart>(new Coop_Model_BattleStart(), false);
  }

  public void EnemyAttack(int sid, int dmg)
  {
    this.SendServer<Coop_Model_EnemyAttack>(new Coop_Model_EnemyAttack()
    {
      sid = sid,
      dmg = dmg
    }, false);
  }

  public void EnemyOut(int sid, Vector3 pos)
  {
    this.SendServer<Coop_Model_EnemyOut>(new Coop_Model_EnemyOut()
    {
      sid = sid,
      x = (int) pos.x,
      z = (int) pos.z
    }, false);
  }

  public void EnemyOutEscape(int sid, Vector3 pos)
  {
    this.SendServer<Coop_Model_EnemyOut>(new Coop_Model_EnemyOut()
    {
      sid = sid,
      x = (int) pos.x,
      z = (int) pos.z,
      isEscape = true
    }, false);
  }

  public void EnemyForcePop(PopSignatureInfo psig, Vector3 pos)
  {
    this.SendServer<Coop_Model_EnemyForcePop>(new Coop_Model_EnemyForcePop()
    {
      psig = psig.signature,
      keyId = psig.popKeyId,
      eid = psig.enemyId,
      lv = psig.enemyLv,
      popType = psig.enemyPopType,
      x = pos.x,
      z = pos.z
    }, false);
  }

  public void RewardGet(int rewardId)
  {
    this.SendServer<Coop_Model_RewardGet>(new Coop_Model_RewardGet()
    {
      rewardId = rewardId
    }, false);
  }

  public void UpdateBoost()
  {
    Coop_Model_UpdateBoost model = new Coop_Model_UpdateBoost();
    if (MonoBehaviourSingleton<StatusManager>.IsValid())
    {
      model.expUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.EXP_UP);
      model.moneyUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.MONEY_UP);
      model.dropUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.DROP_UP);
      model.happenQuestUpEnd = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP);
    }
    this.SendServer<Coop_Model_UpdateBoost>(model, false);
  }

  public void RoomTimeCheck(float elapsed_sec = 0.0f)
  {
    this.SendServer<Coop_Model_RoomTimeCheck>(new Coop_Model_RoomTimeCheck()
    {
      elapsedSec = (int) elapsed_sec
    }, false);
  }

  public void SyncSend()
  {
    if (this.sendId <= 0)
      return;
    this.StartCoroutine(this.CoroutineSyncSend(this.sendId));
  }

  public IEnumerator CoroutineSyncSend(int id)
  {
    while (!MonoBehaviourSingleton<KtbWebSocket>.I.IsCompleteSend(id) && MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected())
    {
      this.Logd("Sync send. id={0}", (object) id);
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private int Send(
    int to_client_id,
    Coop_Model_Base model,
    System.Type type,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null,
    bool is_stage = false,
    bool is_battle = false)
  {
    if (!MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() || is_stage && !MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsStage() || is_battle && !MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsBattle())
      return -1;
    this.sendId = 0;
    this.sendId = MonoBehaviourSingleton<KtbWebSocket>.I.Send(model, type, to_client_id, promise, onReceiveAck, onPreResend);
    return this.sendId;
  }

  public int SendServer<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return !MonoBehaviourSingleton<KtbWebSocket>.I.IsConnected() && MonoBehaviourSingleton<CoopOfflineManager>.IsValid() ? MonoBehaviourSingleton<CoopOfflineManager>.I.Send<T>(model, promise, onReceiveAck) : this.Send(-1000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
  }

  public int SendTo<T>(
    int to_client_id,
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(to_client_id, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
  }

  public int SendBroadcast<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(-2000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
  }

  public int SendToInStage<T>(
    int to_client_id,
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(to_client_id, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend, true);
  }

  public int SendToInBattle<T>(
    int to_client_id,
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(to_client_id, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend, true, true);
  }

  public int SendBroadcastInStage<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(-2000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend, true);
  }

  public int SendBroadcastInBattle<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(-2000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend, true, true);
  }

  private void RegisterPacketReceiveAction()
  {
    KtbWebSocket i1 = MonoBehaviourSingleton<KtbWebSocket>.I;
    i1.ReceivePacketAction = i1.ReceivePacketAction + (Action<CoopPacket>) (packet =>
    {
      if (packet.destObjectId == -1 || packet.packetType == PACKET_TYPE.HEARTBEAT || packet.promise && packet.model.IsPromiseOverAgainCheck() && this.ReceivePromisePacketOverAgainCheck(packet))
        return;
      this.packetReceiver.Set(packet);
    });
    KtbWebSocket i2 = MonoBehaviourSingleton<KtbWebSocket>.I;
    i2.PrepareCloseOccurred = i2.PrepareCloseOccurred + (Action<ushort, string>) ((code, msg) =>
    {
      this.Logd("PrepareCloseOccurred. code={0}, msg={1}", (object) code, (object) msg);
      this.Disconnect(code);
    });
    KtbWebSocket i3 = MonoBehaviourSingleton<KtbWebSocket>.I;
    i3.CloseOccurred = i3.CloseOccurred + (Action<ushort, string>) ((code, msg) =>
    {
      this.Logd("CloseOccurred. code={0}, msg={1}", (object) code, (object) msg);
      this.LoopBackRoomLeave();
      if (!MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
        return;
      MonoBehaviourSingleton<CoopOfflineManager>.I.Activate();
    });
    KtbWebSocket i4 = MonoBehaviourSingleton<KtbWebSocket>.I;
    i4.ErrorOccurred = i4.ErrorOccurred + (Action<Exception>) (ex =>
    {
      this.Logd("ErrorOccurred. ex={0}", (object) ex);
      this.LoopBackRoomLeave();
      if (!MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
        return;
      MonoBehaviourSingleton<CoopOfflineManager>.I.Activate();
    });
    KtbWebSocket i5 = MonoBehaviourSingleton<KtbWebSocket>.I;
    i5.HeartbeatDisconnected = i5.HeartbeatDisconnected + (System.Action) (() =>
    {
      this.Logd("HeartbeatDisconnected.");
      this.LoopBackRoomLeave();
    });
  }

  private bool ReceivePromisePacketOverAgainCheck(CoopPacket packet)
  {
    if (packet.fromClientId <= 0)
      return false;
    List<int> intList = this.recvPromisePacketSequenceNoTable.Get((uint) packet.fromClientId, (uint) packet.packetType);
    if (intList != null && intList.Find((Predicate<int>) (x => x == packet.sequenceNo)) > 0)
    {
      this.Logd("Receive promise packet over again!!. fromId={0}, packet={1}, no={2}", (object) packet.fromClientId, (object) packet.packetType, (object) packet.sequenceNo);
      return true;
    }
    this.Logd("Receive promise packet over again add sequenceNo. fromId={0}, packet={1}, no={2}", (object) packet.fromClientId, (object) packet.packetType, (object) packet.sequenceNo);
    if (intList == null)
    {
      intList = new List<int>();
      this.recvPromisePacketSequenceNoTable.Add((uint) packet.fromClientId, (uint) packet.packetType, intList);
    }
    intList.Add(packet.sequenceNo);
    return false;
  }

  public static CoopPacket CreateLoopBackRoomLeavedPacket()
  {
    Coop_Model_RoomLeaved model = new Coop_Model_RoomLeaved();
    model.id = 1000;
    model.cid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
    model.token = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.userToken;
    model.stgid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.stageId;
    model.stghostid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
    return CoopPacket.Create((Coop_Model_Base) model, -1000, -2000, false, -8989);
  }

  public void LoopBackRoomLeave(bool is_force = false)
  {
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    CoopPacket roomLeavedPacket = CoopNetworkManager.CreateLoopBackRoomLeavedPacket();
    this.Logd("LoopBackRoomLeave. is_connect={0}", (object) CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
    if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && !is_force)
      MonoBehaviourSingleton<KtbWebSocket>.I.ReceivePacketAction(roomLeavedPacket);
    else
      MonoBehaviourSingleton<CoopManager>.I.ForcePacketProcess(roomLeavedPacket);
  }

  public void KickRoomLeave()
  {
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    CoopPacket roomLeavedPacket = CoopNetworkManager.CreateLoopBackRoomLeavedPacket();
    this.Logd("KickRoomLeave. is_connect={0}", (object) CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected());
    MonoBehaviourSingleton<CoopManager>.I.ForcePacketProcess(roomLeavedPacket);
  }

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
    public string token = string.Empty;
  }
}

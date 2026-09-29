// Decompiled with JetBrains decompiler
// Type: LoungeNetworkManager
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
public class LoungeNetworkManager : MonoBehaviourSingleton<LoungeNetworkManager>
{
  private const float CONNECT_TIMEOUT = 15f;
  private const float ALIVE_SENDTIME = 20f;
  private int sendId;
  public Party_Model_RegisterACK registerAck;

  public static void ClearPoolObjects() => rymTPool<List<CoopPacket>>.Clear();

  private LoungePacketReceiver packetReceiver { get; set; }

  private ChatLoungeConnection chatConnection { get; set; }

  public ChatLoungeConnection CreateChatConnection()
  {
    if (this.chatConnection == null)
      this.chatConnection = new ChatLoungeConnection();
    return this.chatConnection;
  }

  protected override void Awake()
  {
    base.Awake();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<LoungePacketReceiver>();
  }

  private void Update()
  {
    this.packetReceiver.OnUpdate();
    if (!CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected() || (double) Time.time - (double) MonoBehaviourSingleton<LoungeWebSocket>.I.packetSendTime < 20.0)
      return;
    this.Alive();
  }

  public void EraseAllPackets() => this.packetReceiver.EraseAllPackets();

  public void Clear()
  {
    this.sendId = 0;
    this.registerAck = (Party_Model_RegisterACK) null;
    this.EraseAllPackets();
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  private string GetRelayServerPath(string path, int port)
  {
    return new UriBuilder(path) { Port = port }.Uri.ToString();
  }

  public void Connect(LoungeNetworkManager.ConnectData conn_data, Action<bool> call_back)
  {
    this.StartCoroutine(this.RequestCoroutineConnect(conn_data, call_back));
  }

  private IEnumerator RequestCoroutineConnect(
    LoungeNetworkManager.ConnectData conn_data,
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
        MonoBehaviourSingleton<LoungeWebSocket>.I.Connect(relayServerPath, conn_data.fromId, conn_data.ackPrefix);
        while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected() && 0.0 < (double) timeoutTimer && MonoBehaviourSingleton<LoungeWebSocket>.I.CurrentConnectionStatus != CoopWebSocketSingleton<LoungeWebSocket>.CONNECTION_STATUS.ERROR)
        {
          timeoutTimer -= Time.deltaTime;
          yield return (object) new WaitForEndOfFrame();
        }
        if (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
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
    if (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
    {
      MonoBehaviourSingleton<LoungeWebSocket>.I.Close(code, msg);
      while (MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
        yield return (object) new WaitForEndOfFrame();
    }
    this.Clear();
    if (call_back != null)
      call_back();
  }

  public void Regist(LoungeNetworkManager.ConnectData conn_data, Action<bool> call_back)
  {
    Party_Model_Register model = new Party_Model_Register();
    model.roomId = conn_data.roomId;
    model.owner = conn_data.owner;
    model.ownerToken = conn_data.ownerToken;
    model.uid = conn_data.uid;
    model.signature = conn_data.signature;
    this.Logd("Regist. roomId={0}", (object) conn_data.roomId);
    this.registerAck = (Party_Model_RegisterACK) null;
    this.SendServer<Party_Model_Register>(model, onReceiveAck: (Func<Coop_Model_ACK, bool>) (ack =>
    {
      bool flag = true;
      this.registerAck = ack as Party_Model_RegisterACK;
      if (ack == null || !ack.positive)
      {
        flag = false;
        MonoBehaviourSingleton<LoungeWebSocket>.I.Close();
      }
      if (call_back != null)
        call_back(flag);
      return true;
    }));
  }

  public void ConnectAndRegist(
    LoungeNetworkManager.ConnectData conn_data,
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

  public void Alive() => this.SendServer<Coop_Model_Alive>(new Coop_Model_Alive(), false);

  public void ChatMessage(string message)
  {
    if (!UserInfoManager.IsValidUser())
      return;
    Coop_Model_StageChatMessage model = new Coop_Model_StageChatMessage();
    model.id = 1005;
    model.text = message;
    model.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.SendBroadcast<Coop_Model_StageChatMessage>(model);
  }

  public void ChatStamp(int stamp_id)
  {
    if (!UserInfoManager.IsValidUser())
      return;
    Coop_Model_StageChatStamp model = new Coop_Model_StageChatStamp();
    model.id = 1005;
    model.stamp_id = stamp_id;
    model.chara_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.SendBroadcast<Coop_Model_StageChatStamp>(model);
  }

  public void RoomPosition(int targetUserId, Vector3 position, LOUNGE_ACTION_TYPE type)
  {
    Lounge_Model_RoomPosition model = new Lounge_Model_RoomPosition();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.pos = position;
    model.aid = (int) type;
    this.Send(targetUserId, (Coop_Model_Base) model, typeof (Lounge_Model_RoomPosition));
  }

  public void JoinNotification(CharaInfo userInfo)
  {
    if (this.chatConnection == null || string.IsNullOrEmpty(userInfo.name))
      return;
    this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 10U, (object) userInfo.name));
  }

  public void SyncSend()
  {
    if (this.sendId <= 0)
      return;
    this.StartCoroutine(this.CoroutineSyncSend(this.sendId));
  }

  public IEnumerator CoroutineSyncSend(int id)
  {
    while (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsCompleteSend(id) && MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
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
    Func<Coop_Model_Base, bool> onPreResend = null)
  {
    if (!MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected())
      return -1;
    this.sendId = 0;
    this.sendId = MonoBehaviourSingleton<LoungeWebSocket>.I.Send(model, type, to_client_id, promise, onReceiveAck, onPreResend);
    return this.sendId;
  }

  public int SendServer<T>(
    T model,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return this.Send(-1000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
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
    bool promise = false,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    if (model.id != -1)
      return this.Send(-2000, (Coop_Model_Base) model, typeof (T), promise, onReceiveAck, onPreResend);
    Log.Warning(LOG.COOP, $"LoungeNetwork({MonoBehaviourSingleton<LoungeWebSocket>.I.IsConnected().ToString()}): model.id not set...");
    return -1;
  }

  private void RegisterPacketReceiveAction()
  {
    LoungeWebSocket i1 = MonoBehaviourSingleton<LoungeWebSocket>.I;
    i1.ReceivePacketAction = i1.ReceivePacketAction + (Action<CoopPacket>) (packet =>
    {
      if (packet.destObjectId == -1 || packet.packetType == PACKET_TYPE.HEARTBEAT)
        return;
      this.packetReceiver.Set(packet);
    });
    LoungeWebSocket i2 = MonoBehaviourSingleton<LoungeWebSocket>.I;
    i2.PrepareCloseOccurred = i2.PrepareCloseOccurred + (Action<ushort, string>) ((code, msg) =>
    {
      this.Logd("PrepareCloseOccurred. code={0}, msg={1}", (object) code, (object) msg);
      this.Disconnect(code);
    });
    LoungeWebSocket i3 = MonoBehaviourSingleton<LoungeWebSocket>.I;
    i3.CloseOccurred = i3.CloseOccurred + (Action<ushort, string>) ((code, msg) =>
    {
      this.Logd("CloseOccurred. code={0}, msg={1}", (object) code, (object) msg);
      this.LoopBackRoomLeave();
    });
    LoungeWebSocket i4 = MonoBehaviourSingleton<LoungeWebSocket>.I;
    i4.ErrorOccurred = i4.ErrorOccurred + (Action<Exception>) (ex =>
    {
      this.Logd("ErrorOccurred. ex={0}", (object) ex);
      this.LoopBackRoomLeave();
    });
    LoungeWebSocket i5 = MonoBehaviourSingleton<LoungeWebSocket>.I;
    i5.HeartbeatDisconnected = i5.HeartbeatDisconnected + (System.Action) (() =>
    {
      this.Logd("HeartbeatDisconnected.");
      this.LoopBackRoomLeave();
    });
  }

  public static CoopPacket CreateLoopBackRoomLeavedPacket()
  {
    Lounge_Model_RoomLeaved model = new Lounge_Model_RoomLeaved();
    model.id = 1000;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.token = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
    return CoopPacket.Create((Coop_Model_Base) model, -1000, -2000, false, -8989);
  }

  public void LoopBackRoomLeave()
  {
    CoopPacket roomLeavedPacket = LoungeNetworkManager.CreateLoopBackRoomLeavedPacket();
    this.Logd("LoopBackRoomLeave. is_connect={0}", (object) CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected());
    if (CoopWebSocketSingleton<LoungeWebSocket>.IsValidConnected())
      MonoBehaviourSingleton<LoungeWebSocket>.I.ReceivePacketAction(roomLeavedPacket);
    else
      this.packetReceiver.ForcePacketProcess(roomLeavedPacket);
  }

  public bool OnRecvRoomJoined(Lounge_Model_RoomJoined model)
  {
    this.Logd("OnRecvRoomJoined. cid={0}", (object) model.cid);
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomJoined(model.cid);
    if (model.cid != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id && MonoBehaviourSingleton<LoungeManager>.IsValid())
    {
      if (!MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<LoungeManager>.I.IHomePeople == null || Object.op_Equality((Object) MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara, (Object) null))
        return true;
      Vector3 position = MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara._transform.position;
      LOUNGE_ACTION_TYPE actionType = MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara.GetActionType();
      this.RoomPosition(model.cid, position, actionType);
    }
    if (FieldManager.IsValidInGame())
      Protocol.Try((System.Action) (() => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (isSuccess => { }))));
    string empty = string.Empty;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
    if (slotInfoByUserId != null)
    {
      string name1 = slotInfoByUserId.userInfo.name;
    }
    else if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
    {
      InGameRecorder.PlayerRecord playerByUserId = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayerByUserId(model.cid);
      if (playerByUserId != null)
      {
        string name2 = playerByUserId.charaInfo.name;
      }
    }
    return true;
  }

  public bool OnRecvRoomLeaved(Lounge_Model_RoomLeaved model)
  {
    this.Logd("OnRecvRoomLeaved. cid={0}", (object) model.cid);
    if (model.cid != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      string str = string.Empty;
      PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
      if (slotInfoByUserId != null)
        str = slotInfoByUserId.userInfo.name;
      else if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      {
        InGameRecorder.PlayerRecord playerByUserId = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayerByUserId(model.cid);
        if (playerByUserId != null)
          str = playerByUserId.charaInfo.name;
      }
      if (this.chatConnection != null && !string.IsNullOrEmpty(str))
        this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 11U, (object) str));
    }
    if (FieldManager.IsValidInGame())
      Protocol.Try((System.Action) (() => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (isSuccess => { }))));
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomLeaved(model.cid);
    return true;
  }

  public bool OnRecvRoomPoisition(Lounge_Model_RoomPosition model)
  {
    this.Logd("OnRecvRoomPosition. cid={0}, pos={1}", (object) model.cid, (object) model.pos);
    this.StartCoroutine(this.LoungeManagerRecvRoomPosition(model.cid, model.pos, model.aid));
    return true;
  }

  private IEnumerator LoungeManagerRecvRoomPosition(int userId, Vector3 pos, int aid)
  {
    while (!MonoBehaviourSingleton<LoungeManager>.IsValid())
      yield return (object) null;
    while (!MonoBehaviourSingleton<LoungeManager>.I.IsInitialized)
      yield return (object) null;
    MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomPosition(userId, pos, (LOUNGE_ACTION_TYPE) aid);
  }

  public bool OnRecvRoomMove(Lounge_Model_RoomMove model)
  {
    this.Logd("OnRecvRoomMove. cid={0}", (object) model.cid);
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomMove(model.cid, model.pos);
    return true;
  }

  public bool OnRecvRoomAction(Lounge_Model_RoomAction model)
  {
    this.Logd("OnRecvRoomAction. cid={0}. aid={1}", (object) model.cid, (object) model.aid);
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      MonoBehaviourSingleton<LoungeManager>.I.OnRecvRoomAction(model.cid, model.aid);
    return true;
  }

  public bool OnRecvRoomKick(Lounge_Model_RoomKick model)
  {
    this.Logd("OnRecvKick. cId = {0}", (object) model.cid);
    if (!LoungeMatchingManager.IsValidInLounge())
      return true;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
    if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null)
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.Kick(model.cid);
    if (this.chatConnection != null)
      this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 12U, (object) slotInfoByUserId.userInfo.name));
    return true;
  }

  public bool OnRecvRoomAFKKick(Lounge_Model_AFK_Kick model)
  {
    this.Logd("OnRecvAFKKick. cId = {0}", (object) model.cid);
    if (!LoungeMatchingManager.IsValidInLounge())
      return true;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.cid);
    if (slotInfoByUserId == null || slotInfoByUserId.userInfo == null)
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.Kick(model.cid);
    if (this.chatConnection != null)
      this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 19U, (object) slotInfoByUserId.userInfo.name));
    return true;
  }

  public bool OnRecvRoomHostChanged(Lounge_Model_RoomHostChanged model)
  {
    this.Logd("OnRecvHostChanged. hostId = {0}", (object) model.hostid);
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.hostid))
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.ChangeOwner(model.hostid);
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.hostid);
    if (this.chatConnection != null)
      this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 13U, (object) slotInfoByUserId.userInfo.name));
    return true;
  }

  public bool OnRecvMemberLounge(Lounge_Model_MemberLounge model)
  {
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveLounge(model.cid);
    return true;
  }

  public bool OnRecvMemberQuest(Lounge_Model_MemberQuest model)
  {
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveQuest(model);
    return true;
  }

  public bool OnRecvMemberField(Lounge_Model_MemberField model)
  {
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveField(model);
    return true;
  }

  public bool OnRecvMemberArena(Lounge_Model_MemberArena model)
  {
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.cid))
      return true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.OnRecvMemberMoveArena(model);
    return true;
  }

  public bool OnRecvChatMessage(Coop_Model_StageChatMessage model)
  {
    this.Logd("OnRecvChatMessage. user_id={0},text={1}", (object) model.user_id, (object) model.text);
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.user_id))
      return true;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.user_id);
    if (this.chatConnection != null)
      this.chatConnection.OnReceiveMessage(model.user_id, slotInfoByUserId.userInfo.name, model.text);
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      MonoBehaviourSingleton<LoungeManager>.I.OnRecvChatMessage(model.user_id);
    return true;
  }

  public bool OnRecvChatStamp(Coop_Model_StageChatStamp model)
  {
    this.Logd("OnRecvChatStamp. user_id={0},stamp_id={1}", (object) model.user_id, (object) model.stamp_id);
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(model.user_id))
      return true;
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(model.user_id);
    if (this.chatConnection != null)
      this.chatConnection.OnReceiveStamp(model.user_id, slotInfoByUserId.userInfo.name, model.stamp_id);
    return true;
  }

  public void MoveLoungeNotification(
    LoungeMemberStatus.MEMBER_STATUS beforeStatus,
    LoungeMemberStatus after)
  {
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsUserInLounge(after.userId))
      return;
    LoungeMemberStatus.MEMBER_STATUS status = after.GetStatus();
    PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(after.userId);
    switch (beforeStatus)
    {
      case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
        switch (status)
        {
          case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
            if (!after.isHost)
              return;
            this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 14U, (object) slotInfoByUserId.userInfo.name));
            return;
          case LoungeMemberStatus.MEMBER_STATUS.QUEST:
            return;
          case LoungeMemberStatus.MEMBER_STATUS.FIELD:
            this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 15U, (object) slotInfoByUserId.userInfo.name));
            return;
          case LoungeMemberStatus.MEMBER_STATUS.ARENA:
            this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 20U, (object) slotInfoByUserId.userInfo.name));
            return;
          default:
            return;
        }
      case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
        if (status != LoungeMemberStatus.MEMBER_STATUS.QUEST)
          break;
        this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 16U /*0x10*/, (object) slotInfoByUserId.userInfo.name));
        break;
      case LoungeMemberStatus.MEMBER_STATUS.QUEST:
        if (status != LoungeMemberStatus.MEMBER_STATUS.LOUNGE)
          break;
        this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 17U, (object) slotInfoByUserId.userInfo.name));
        break;
      case LoungeMemberStatus.MEMBER_STATUS.ARENA:
        if (status != LoungeMemberStatus.MEMBER_STATUS.LOUNGE)
          break;
        this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 21U, (object) slotInfoByUserId.userInfo.name));
        break;
      default:
        if (status != LoungeMemberStatus.MEMBER_STATUS.LOUNGE)
          break;
        this.chatConnection.OnReceiveNotification(StringTable.Format(STRING_CATEGORY.LOUNGE, 18U, (object) slotInfoByUserId.userInfo.name));
        break;
    }
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
    public int owner;
    public string ownerToken = string.Empty;
    public int uid;
    public string signature = string.Empty;
  }
}

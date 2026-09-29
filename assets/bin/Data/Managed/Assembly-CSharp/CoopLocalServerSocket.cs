// Decompiled with JetBrains decompiler
// Type: CoopLocalServerSocket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class CoopLocalServerSocket
{
  private CoopLocalServerStage stage;

  public void InitStage(
    uint map_id,
    List<CoopOfflineManager.EnemyPopParam> enemy_pop_params,
    int now_enemy_id)
  {
    this.stage = new CoopLocalServerStage(this);
    this.stage.Init(map_id, enemy_pop_params, now_enemy_id);
  }

  public void Clear() => this.stage = (CoopLocalServerStage) null;

  public void Update()
  {
    if (this.stage == null)
      return;
    this.stage.Update();
  }

  public Coop_Model_ACK Recv(Coop_Model_Base model)
  {
    Coop_Model_ACK coopModelAck = (Coop_Model_ACK) null;
    switch ((PACKET_TYPE) model.c)
    {
      case PACKET_TYPE.REGISTER:
        coopModelAck = this.RecvRegister(model as Coop_Model_Register);
        break;
      case PACKET_TYPE.ROOM_STAGE_REQUEST:
        coopModelAck = this.RecvRoomStageRequest(model as Coop_Model_RoomStageRequest);
        break;
      case PACKET_TYPE.ENEMY_OUT:
        coopModelAck = this.RecvEnemyOut(model as Coop_Model_EnemyOut);
        break;
    }
    return coopModelAck;
  }

  private Coop_Model_ACK RecvRegister(Coop_Model_Register model)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return (Coop_Model_ACK) null;
    Coop_Model_RegisterACK modelRegisterAck = new Coop_Model_RegisterACK();
    modelRegisterAck.ack = model.u;
    modelRegisterAck.positive = true;
    modelRegisterAck.sid = MonoBehaviourSingleton<CoopManager>.I.GetSelfID();
    modelRegisterAck.of = false;
    modelRegisterAck.ids.Add(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    modelRegisterAck.stgids.Add(1);
    modelRegisterAck.stgidxs.Add(0);
    modelRegisterAck.stghosts.Add(true);
    return (Coop_Model_ACK) modelRegisterAck;
  }

  private Coop_Model_ACK RecvRoomStageRequest(Coop_Model_RoomStageRequest model)
  {
    if (this.stage == null)
      return (Coop_Model_ACK) null;
    this.stage.StartEnemyPop();
    if (QuestManager.IsValidInGameSeriesArena())
      this.SendEnemyPopForSeriesArena(0);
    this.SendRoomStageRequested();
    return (Coop_Model_ACK) null;
  }

  private Coop_Model_ACK RecvEnemyOut(Coop_Model_EnemyOut model)
  {
    if (this.stage == null)
      return (Coop_Model_ACK) null;
    this.stage.OutEnemy(model.sid);
    return (Coop_Model_ACK) null;
  }

  public void Send<T>(T model, bool promise = true) where T : Coop_Model_Base
  {
    model.id = 1000;
    MonoBehaviourSingleton<CoopOfflineManager>.I.Recv(CoopPacket.Create((Coop_Model_Base) model, -1000, -2000, promise, 0));
  }

  public void SendRoomStageRequested()
  {
    this.Send<Coop_Model_RoomStageRequested>(new Coop_Model_RoomStageRequested()
    {
      cid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId
    });
  }

  public void SendEnemyPop(CoopLocalServerEnemy enemy)
  {
    this.Send<Coop_Model_EnemyPop>(new Coop_Model_EnemyPop()
    {
      sid = enemy.sid,
      ownerClientId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId,
      popIndex = enemy.popIndex
    });
  }

  public void SendEnemyPopForSeriesArena(int index)
  {
    this.Send<Coop_Model_EnemyPop>(new Coop_Model_EnemyPop()
    {
      sid = this.stage.GenerateEnemyUniqId(),
      ownerClientId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId,
      popIndex = index,
      seriesIdx = index
    });
  }

  public void SendEnemyExtermination()
  {
    this.Send<Coop_Model_EnemyExtermination>(new Coop_Model_EnemyExtermination());
  }
}

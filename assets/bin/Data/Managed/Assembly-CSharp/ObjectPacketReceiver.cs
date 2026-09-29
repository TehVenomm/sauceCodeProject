// Decompiled with JetBrains decompiler
// Type: ObjectPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ObjectPacketReceiver : PacketReceiver
{
  private static List<bool> forceFlags;

  public StageObject owner { get; protected set; }

  public ObjectPacketReceiver.FILTER_MODE filterMode { get; protected set; }

  public static ObjectPacketReceiver SetupComponent(StageObject set_object)
  {
    switch (set_object)
    {
      case Enemy _:
        return (ObjectPacketReceiver) ((Component) set_object).gameObject.AddComponent<EnemyPacketReceiver>();
      case Player _:
        return (ObjectPacketReceiver) ((Component) set_object).gameObject.AddComponent<PlayerPacketReceiver>();
      case Character _:
        return (ObjectPacketReceiver) ((Component) set_object).gameObject.AddComponent<CharacterPacketReceiver>();
      default:
        return ((Component) set_object).gameObject.AddComponent<ObjectPacketReceiver>();
    }
  }

  protected virtual void Awake() => this.owner = ((Component) this).GetComponent<StageObject>();

  public override void SetStopPacketUpdate(bool is_stop) => base.SetStopPacketUpdate(is_stop);

  public override void Set(CoopPacket packet)
  {
    base.Set(packet);
    packet.GetModel<Coop_Model_ObjectBase>()?.SetReceiveTime(Time.time);
  }

  protected override void PacketUpdate()
  {
    if (this.stopPacketUpdate)
      return;
    if (ObjectPacketReceiver.forceFlags == null)
    {
      ObjectPacketReceiver.forceFlags = new List<bool>(this.packets.Count);
    }
    else
    {
      ObjectPacketReceiver.forceFlags.Clear();
      if (ObjectPacketReceiver.forceFlags.Capacity < this.packets.Count)
        ObjectPacketReceiver.forceFlags.Capacity = this.packets.Count;
    }
    int index1 = 0;
    for (int count = this.packets.Count; index1 < count; ++index1)
    {
      CoopPacket packet = this.packets[index1];
      bool flag = false;
      Coop_Model_ObjectBase model = packet.GetModel<Coop_Model_ObjectBase>();
      if (model != null)
        flag = model.IsForceHandleBefore(this.owner);
      ObjectPacketReceiver.forceFlags.Add(flag);
    }
    int index2 = 0;
    for (int count = this.packets.Count; index2 < count; ++index2)
    {
      CoopPacket packet = this.packets[index2];
      if (!this.CheckFilterPacket(packet))
      {
        this.AddDeleteQueue(packet);
      }
      else
      {
        bool flag1 = true;
        Coop_Model_ObjectBase model = packet.GetModel<Coop_Model_ObjectBase>();
        if (model != null)
        {
          bool flag2 = false;
          for (int index3 = index2 + 1; index3 < count; ++index3)
          {
            if (ObjectPacketReceiver.forceFlags[index3])
            {
              flag2 = true;
              break;
            }
          }
          if (!flag2)
          {
            float num1 = 0.0f;
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
              num1 = MonoBehaviourSingleton<InGameSettingsManager>.I.stageObject.packetHandleMarginTime;
            if ((double) Time.time > (double) model.GetReceiveTime() + (double) num1)
            {
              flag1 = true;
              if (!model.IsHandleable(this.owner))
              {
                int num2 = -1;
                Character owner = this.owner as Character;
                if (Object.op_Inequality((Object) owner, (Object) null))
                  num2 = (int) owner.actionID;
                Log.Warning(LOG.COOP, $"ObjectPacketReceiver::PacketUpdate() Err. ( Over packetHandleMarginTime. ) type : {(object) packet.packetType}, action_id : {(object) num2}");
              }
            }
            else
              flag1 = model.IsHandleable(this.owner);
          }
        }
        if (flag1 && this.HandleCoopEvent(packet))
        {
          this.AddDeleteQueue(packet);
          if (this.stopPacketUpdate)
            break;
        }
        else
        {
          if ((double) Time.time > (double) model.GetReceiveTime() + 20.0)
          {
            Log.Warning(LOG.COOP, "ObjectPacketReceiver::PacketUpdate() Err. ( Over 20 Second. ) type : " + (object) packet.packetType);
            break;
          }
          break;
        }
      }
    }
    this.EraseUsedPacket();
  }

  public virtual void SetFilterMode(ObjectPacketReceiver.FILTER_MODE filter_mode)
  {
    this.filterMode = filter_mode;
    if (this.filterMode == ObjectPacketReceiver.FILTER_MODE.NONE)
      return;
    int index = 0;
    for (int count = this.packets.Count; index < count; ++index)
    {
      CoopPacket packet = this.packets[index];
      if (!this.CheckFilterPacket(packet))
        this.AddDeleteQueue(packet);
    }
    this.EraseUsedPacket();
  }

  protected virtual bool CheckFilterPacket(CoopPacket packet)
  {
    return this.filterMode == ObjectPacketReceiver.FILTER_MODE.NONE || packet.packetType == PACKET_TYPE.OBJECT_DESTROY;
  }

  public virtual bool GetPredictivePosition(out Vector3 pos)
  {
    pos = Vector3.zero;
    for (int index = this.packets.Count - 1; index >= 0; --index)
    {
      Coop_Model_ObjectBase model = this.packets[index].GetModel<Coop_Model_ObjectBase>();
      if (model != null && model.IsHaveObjectPosition())
      {
        pos = model.GetObjectPosition();
        return true;
      }
    }
    return false;
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    switch (packet.packetType)
    {
      case PACKET_TYPE.OBJECT_DESTROY:
        return this.owner is Self || this.owner.DestroyObject();
      case PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER:
        AttackedHitStatusOwner status1;
        packet.GetModel<Coop_Model_ObjectAttackedHitOwner>().CopyAttackedHitStatus(out status1);
        if (this.owner.IsEnableAttackedHitOwner())
        {
          this.owner.OnAttackedHitOwner(status1);
          AttackedHitStatusFix status2 = new AttackedHitStatusFix(status1.origin);
          this.owner.OnAttackedHitFix(status2);
          if (Object.op_Inequality((Object) this.owner.packetSender, (Object) null))
          {
            this.owner.packetSender.OnAttackedHitFix(status2);
            break;
          }
          break;
        }
        break;
      case PACKET_TYPE.OBJECT_ATTACKED_HIT_FIX:
        AttackedHitStatusFix status3;
        packet.GetModel<Coop_Model_ObjectAttackedHitFix>().CopyAttackedHitStatus(out status3);
        this.owner.OnAttackedHitFix(status3);
        break;
      case PACKET_TYPE.OBJECT_KEEP_WAITING_PACKET:
        this.owner.KeepWaitingPacket((StageObject.WAITING_PACKET) packet.GetModel<Coop_Model_ObjectKeepWaitingPacket>().type);
        break;
      case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_SET:
        this.owner.RegisterObservableID(packet.GetModel<Coop_Model_ObjectBulletObservableSet>().observedID);
        break;
      case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_BROKEN:
        this.owner.OnBreak(packet.GetModel<Coop_Model_ObjectBulletObservableBroken>().observedID, false);
        break;
      case PACKET_TYPE.OBJECT_SHOT_GIMMICK_GENERATOR:
        Coop_Model_ObjectShotGimmickGenerator model1 = packet.GetModel<Coop_Model_ObjectShotGimmickGenerator>();
        GimmickGeneratorObject owner = this.owner as GimmickGeneratorObject;
        if (Object.op_Inequality((Object) owner, (Object) null))
        {
          owner.OnGenerateForLinearMove(model1.pos);
          break;
        }
        break;
      case PACKET_TYPE.OBJECT_COOP_INFO:
        this.owner.OnRecvSetCoopMode(packet.GetModel<Coop_Model_ObjectCoopInfo>(), packet);
        break;
      case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_SEARCH_TARGET:
        Coop_Model_ObjectBulletObservableSearchTarget model2 = packet.GetModel<Coop_Model_ObjectBulletObservableSearchTarget>();
        this.owner.OnSetSearchTarget(model2.observedID, model2.targetId);
        break;
      case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_TURRETBIT_TARGET:
        Coop_Model_ObjectBulletObservableTurretBitTarget model3 = packet.GetModel<Coop_Model_ObjectBulletObservableTurretBitTarget>();
        this.owner.OnSetTurretBitTarget(model3.observedID, model3.targetId, model3.regionId);
        break;
      default:
        Log.Warning(LOG.COOP, "not valid packet");
        return true;
    }
    return true;
  }

  public enum FILTER_MODE
  {
    NONE,
    WAIT_INITIALIZE,
  }
}

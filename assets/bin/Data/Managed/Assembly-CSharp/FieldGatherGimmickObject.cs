// Decompiled with JetBrains decompiler
// Type: FieldGatherGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldGatherGimmickObject : FieldGimmickObject
{
  protected Transform _transform;
  protected Transform targetMarkerTrans;
  protected float radius = 1.5f;
  protected float sqlRadius = 2.25f;
  protected List<Player> userList = new List<Player>();

  public int lotId { get; protected set; }

  protected override void Awake()
  {
    this._transform = ((Component) this).transform;
    SphereCollider componentInChildren = ((Component) this._transform).GetComponentInChildren<SphereCollider>();
    if (componentInChildren != null)
    {
      this.radius = componentInChildren.radius;
      this.sqlRadius = this.radius * this.radius;
    }
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
  }

  protected override void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "lid")
      {
        int result = 0;
        int.TryParse(strArray[1], out result);
        this.lotId = result;
      }
    }
  }

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (this.CanUse() & isNear && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction(this.GetTargetActionId()))
    {
      if (Object.op_Equality((Object) this.targetMarkerTrans, (Object) null))
        this.targetMarkerTrans = EffectManager.GetEffect(this.GetMarkerName(), this._transform);
      if (!Object.op_Inequality((Object) this.targetMarkerTrans, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
      this.targetMarkerTrans.Set(Vector3.op_Addition(Vector3.op_Addition(((Vector3) ref vector3).normalized, Vector3.up), this._transform.position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetMarkerTrans, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetMarkerTrans).gameObject);
    }
  }

  public virtual bool StartAction(Player player, bool isSend = false)
  {
    if (!this.IsValid())
      return false;
    this.OnUseStart(player, isSend);
    return true;
  }

  public void OnUseStart(Player player, bool isSend = false)
  {
    if (Object.op_Equality((Object) player, (Object) null) || this.userList.Contains(player))
      return;
    this.userList.Add(player);
    if (!isSend)
      return;
    player.playerSender.OnGatherGimmickInfo(this.m_id, true);
  }

  public void OnUseEnd(Player player, bool isSend = false)
  {
    if (Object.op_Equality((Object) player, (Object) null) || !this.userList.Contains(player))
      return;
    this.userList.Remove(player);
    if (!isSend)
      return;
    player.playerSender.OnGatherGimmickInfo(this.m_id, false);
  }

  protected virtual bool IsValid()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    return !Object.op_Equality((Object) self, (Object) null) && !self.isDead;
  }

  public override float GetTargetSqrRadius() => this.sqlRadius;

  public override string GetObjectName() => "GatherGimmick";

  public virtual string GetMarkerName() => "ef_btl_target_common_01";

  public virtual GATHER_GIMMICK_TYPE GetGatherGimmickType() => GATHER_GIMMICK_TYPE.NONE;

  public virtual Character.ACTION_ID GetTargetActionId() => (Character.ACTION_ID) 28;

  public virtual bool IsUseOnly() => true;

  public virtual bool CanUse()
  {
    if (!this.IsUseOnly())
      return true;
    for (int index = 0; index < this.userList.Count; ++index)
    {
      bool flag = false;
      Player user = this.userList[index];
      if (Object.op_Equality((Object) user, (Object) null))
        flag = true;
      else if (Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(user.id), (Object) null))
        flag = true;
      if (flag)
      {
        this.userList.RemoveAt(index);
        --index;
      }
    }
    return this.userList.Count == 0;
  }
}

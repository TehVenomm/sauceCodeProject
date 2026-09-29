// Decompiled with JetBrains decompiler
// Type: FieldGimmickCoopFishing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldGimmickCoopFishing : FieldGatherGimmickObject
{
  private const string OBJECT_NAME = "CoopFishing";
  public const string COOP_FISHING_MARKER_NAME = "ef_btl_target_fishing_02";
  private Transform targetMarker;
  private Player owner;
  private bool isActive;

  protected override void Awake()
  {
    this.SetTransform(((Component) this).transform);
    this.isActive = true;
  }

  public override string GetObjectName() => "CoopFishing";

  public override string GetMarkerName() => "ef_btl_target_fishing_02";

  public override bool IsUseOnly() => false;

  public override GATHER_GIMMICK_TYPE GetGatherGimmickType() => GATHER_GIMMICK_TYPE.COOP_FISHING;

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (isNear && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction((Character.ACTION_ID) 41))
    {
      if (Object.op_Equality((Object) this.targetMarker, (Object) null))
        this.targetMarker = EffectManager.GetEffect("ef_btl_target_fishing_02", this.m_transform);
      if (!Object.op_Inequality((Object) this.targetMarker, (Object) null))
        return;
      Vector3 position1 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.position;
      Quaternion rotation = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.rotation;
      Vector3 position2 = this.m_transform.position;
      Vector3 vector3 = Vector3.op_Subtraction(position1, position2);
      this.targetMarker.Set(Vector3.op_Addition(Vector3.op_Addition(((Vector3) ref vector3).normalized, Vector3.up), this.m_transform.position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetMarker, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetMarker).gameObject);
    }
  }

  public override bool IsSearchableNearest() => this.isActive && !(this.owner is Self);

  public void SetOwner(Player player) => this.owner = player;

  public int GetOwnerPlayerId() => this.owner.id;

  public int GetOwnerClientId() => this.owner.coopClientId;

  public int GetOwnerUserId() => this.owner.createInfo.charaInfo.userId;

  public void Deactivate() => this.isActive = false;

  public Vector3 GetCoopPos()
  {
    return Object.op_Equality((Object) this.owner, (Object) null) ? this.m_transform.position : Vector3.op_Addition(this.m_transform.position, Vector3.op_Multiply(this.owner._forward, -1f));
  }

  public Quaternion GetCoopRot()
  {
    return Object.op_Equality((Object) this.owner, (Object) null) ? Quaternion.identity : Quaternion.LookRotation(this.owner._forward, Vector3.up);
  }

  public void SetPosition(Vector3 pos) => this.m_transform.position = pos;
}

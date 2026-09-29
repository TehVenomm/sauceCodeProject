// Decompiled with JetBrains decompiler
// Type: AttackRestraintObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackRestraintObject : MonoBehaviour
{
  public const string ANIM_STATE_LOOP = "LOOP";
  public const string ANIM_STATE_FLICK_ACTION = "ACT";
  public const string ANIM_STATE_END = "END";
  public const string EFFECT_CENTER_OBJECT_NAME = "Center";
  public const string EFFECT_NAME_FLICK_WARNING = "ef_btl_target_flick";
  private Player m_targetPlayer;
  private GameObject m_effectFlickWarning;
  private GameObject m_effectRestraint;
  private Transform m_effectCenterTrans;
  private TargetPoint m_targetPoint;
  private bool m_isValidFlickInput;
  private bool m_isDeleted;
  private bool m_isDisableRemoveRestraintByAttack;

  private void Awake() => Utility.SetLayerWithChildren(((Component) this).transform, 11);

  public void Initialize(Player targetPlayer, RestraintInfo restInfo)
  {
    Transform parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    this.m_targetPlayer = targetPlayer;
    Transform transform = targetPlayer._transform;
    ((Component) this).transform.parent = parent;
    Transform effect1 = EffectManager.GetEffect(restInfo.effectName, parent);
    if (Object.op_Inequality((Object) effect1, (Object) null))
    {
      effect1.position = transform.position;
      effect1.localScale = Vector3.one;
      effect1.localRotation = Quaternion.identity;
      this.m_effectRestraint = ((Component) effect1).gameObject;
      this.m_effectCenterTrans = effect1.Find("Center");
    }
    SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    ((Collider) sphereCollider).isTrigger = true;
    sphereCollider.radius = restInfo.radius;
    this.m_isValidFlickInput = (double) restInfo.reduceTimeByFlick > 0.0;
    this.m_isDisableRemoveRestraintByAttack = restInfo.isDisableRemoveByPlayerAttack;
    if (targetPlayer.objectType == StageObject.OBJECT_TYPE.SELF)
    {
      if (this.IsValidFlickInput)
      {
        Transform effect2 = EffectManager.GetEffect("ef_btl_target_flick", ((Component) this).transform);
        if (Object.op_Inequality((Object) effect2, (Object) null))
        {
          effect2.localPosition = Vector3.zero;
          effect2.localScale = Vector3.one;
          effect2.localRotation = Quaternion.identity;
          this.m_effectFlickWarning = ((Component) effect2).gameObject;
        }
      }
    }
    else if (!this.m_isDisableRemoveRestraintByAttack)
    {
      TargetPoint targetPoint = ((Component) this).gameObject.AddComponent<TargetPoint>();
      targetPoint.markerZShift = 0.0f;
      targetPoint.offset = Vector3.op_Multiply(Vector3.up, 0.5f);
      targetPoint.regionID = -1;
      targetPoint.isTargetEnable = true;
      targetPoint.isAimEnable = true;
      targetPoint.bleedOffsetPos = Vector3.zero;
      targetPoint.bleedOffsetRot = Vector3.zero;
      targetPoint.aimMarkerPointRate = 1f;
      targetPoint.ForceDisplay();
      this.m_targetPoint = targetPoint;
    }
    this.AdjustPosition();
  }

  public void DeleteThis()
  {
    if (this.m_isDeleted)
      return;
    if (Object.op_Inequality((Object) this.m_effectRestraint, (Object) null))
    {
      EffectManager.ReleaseEffect(this.m_effectRestraint);
      this.m_effectRestraint = (GameObject) null;
    }
    if (Object.op_Inequality((Object) this.m_effectFlickWarning, (Object) null))
    {
      EffectManager.ReleaseEffect(this.m_effectFlickWarning);
      this.m_effectFlickWarning = (GameObject) null;
    }
    this.m_isDeleted = true;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void LateUpdate() => this.AdjustPosition();

  private void OnTriggerEnter(Collider collider)
  {
    if (Object.op_Equality((Object) this.m_targetPlayer, (Object) null))
    {
      this.DeleteThis();
    }
    else
    {
      if (!this.CheckValidAttack(((Component) collider).gameObject))
        return;
      this.m_targetPlayer.ActRestraintEnd();
      this.m_targetPlayer = (Player) null;
    }
  }

  public void OnFlick()
  {
    if (Object.op_Equality((Object) this.m_effectRestraint, (Object) null) || !this.IsValidFlickInput)
      return;
    Animator component = this.m_effectRestraint.GetComponent<Animator>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    int hash = Animator.StringToHash("ACT");
    if (!component.HasState(0, hash))
      return;
    AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash == Animator.StringToHash("END"))
      return;
    component.Play(hash, 0, 0.0f);
    component.Update(0.0f);
  }

  private bool CheckValidAttack(GameObject hitObj)
  {
    if (this.m_isDisableRemoveRestraintByAttack || hitObj == null)
      return false;
    IAttackCollider component = hitObj.GetComponent<IAttackCollider>();
    if (component == null || component is HealAttackObject)
      return false;
    StageObject fromObject = component.GetFromObject();
    return fromObject != null && fromObject is Player;
  }

  private void AdjustPosition()
  {
    if (Object.op_Equality((Object) this.m_effectCenterTrans, (Object) null))
      return;
    Vector3 position = this.m_effectCenterTrans.position;
    if (Object.op_Inequality((Object) this.m_targetPoint, (Object) null))
    {
      Vector3 vector3 = position;
      vector3.y -= 0.85f;
      this.m_targetPlayer._transform.position = vector3;
    }
    ((Component) this).transform.position = position;
  }

  public TargetPoint BreakTargetPoint => this.m_targetPoint;

  public bool IsValidFlickInput => this.m_isValidFlickInput;
}

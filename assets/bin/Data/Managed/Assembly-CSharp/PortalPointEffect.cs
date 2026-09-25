// Decompiled with JetBrains decompiler
// Type: PortalPointEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PortalPointEffect : MonoBehaviour
{
  protected bool isDelete;
  protected InGameSettingsManager.Portal.PointEffect parameter;
  protected FloatInterpolator anim = new FloatInterpolator();
  protected int animStep = -1;

  public Transform _transform { get; protected set; }

  public Rigidbody _rigidbody { get; protected set; }

  public PortalObject targetPortal { get; protected set; }

  public Coop_Model_EnemyDefeat defeatModel { get; protected set; }

  public static PortalPointEffect Create(PortalObject portal_object, Coop_Model_EnemyDefeat model)
  {
    if (model == null)
      return (PortalPointEffect) null;
    string effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect.normalEffectName;
    if (model.ppt > 1)
      effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect.largeEffectName;
    Transform effect = EffectManager.GetEffect(effect_name);
    if (Object.op_Equality((Object) effect, (Object) null))
      return (PortalPointEffect) null;
    PortalPointEffect portalPointEffect = ((Component) effect).gameObject.AddComponent<PortalPointEffect>();
    if (Object.op_Inequality((Object) portalPointEffect, (Object) null))
      portalPointEffect.Drop(portal_object, model);
    return portalPointEffect;
  }

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    Utility.SetLayerWithChildren(this._transform, 19);
    ((Component) this).gameObject.SetActive(false);
  }

  public void Drop(PortalObject portal_object, Coop_Model_EnemyDefeat model)
  {
    this.targetPortal = portal_object;
    this.defeatModel = model;
    ((Component) this).gameObject.SetActive(true);
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect;
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector((float) model.x, 0.0f, (float) model.z);
    this._rigidbody.useGravity = false;
    this._transform.position = vector3;
    this.anim.Set(this.parameter.popHeightAnimTime, 0.0f, this.parameter.popHeight, this.parameter.popHeightAnim, 0.0f, (AnimationCurve) null);
    this.anim.Play();
    this.anim.Update(0.0f);
    vector3.y = this.anim.Get();
    this._transform.position = vector3;
    this.animStep = 0;
  }

  private void FixedUpdate()
  {
    if (this.isDelete || !Object.op_Implicit((Object) this.targetPortal))
      return;
    switch (this.animStep)
    {
      case 0:
        if (this.anim.IsPlaying())
        {
          Vector3 position = this._transform.position;
          position.y = this.anim.Update();
          if (!this.anim.IsPlaying())
            break;
          this._transform.position = position;
          break;
        }
        ++this.animStep;
        this.anim.Set(this.parameter.getSpeedAnimTime, 0.0f, this.parameter.getSpeed, this.parameter.getSpeedAnim, 0.0f, (AnimationCurve) null);
        this.anim.Play();
        this.anim.Update(0.0f);
        Vector3 position1 = this.targetPortal._transform.position;
        position1.y += this.parameter.targetHeight;
        Vector3 vector3_1 = Vector3.op_Subtraction(position1, this._transform.position);
        this._rigidbody.velocity = Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, this.anim.Get());
        SoundManager.PlayOneShotUISE(40000070);
        break;
      case 1:
        Vector3 position2 = this.targetPortal._transform.position;
        position2.y += this.parameter.targetHeight;
        Vector3 vector3_2 = Vector3.op_Subtraction(position2, this._transform.position);
        float num = this.anim.Update();
        if ((double) num * (double) Time.fixedDeltaTime >= (double) ((Vector3) ref vector3_2).magnitude)
        {
          this.OnHitTarget();
          break;
        }
        if ((double) Vector3.Dot(vector3_2, this._rigidbody.velocity) < 0.0)
        {
          this.OnHitTarget();
          break;
        }
        this._rigidbody.velocity = Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, num);
        break;
    }
  }

  private void OnHitTarget()
  {
    if (this.isDelete)
      return;
    this._rigidbody.velocity = Vector3.zero;
    if (Object.op_Inequality((Object) this.targetPortal, (Object) null))
    {
      Vector3 position = this.targetPortal._transform.position;
      position.y += this.parameter.targetHeight;
      this._transform.position = position;
      this.targetPortal.OnGetPortalPoint(this.defeatModel.ppt);
    }
    EffectManager.ReleaseEffect(((Component) this).gameObject);
    this.isDelete = true;
  }
}

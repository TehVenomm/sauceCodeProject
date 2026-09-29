// Decompiled with JetBrains decompiler
// Type: UIHGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIHGauge : MonoBehaviour
{
  protected const float ANIM_WAIT_TIME = 3f;
  protected const float ANIM_MOVE_TIME = 0.5f;
  [SerializeField]
  protected UISlider gaugeUI;
  [SerializeField]
  protected UISlider gaugeEffectUI;
  protected bool initialized;
  protected float oldPercent = 1f;
  protected UIHGauge.ANIM_PHASE animPhase;
  protected float animTime;

  public float nowPercent { get; protected set; }

  public UIHGauge() => this.nowPercent = 1f;

  private void Awake()
  {
    if (!Object.op_Inequality((Object) this.gaugeUI, (Object) null))
      return;
    this.initialized = true;
  }

  public void SetPercent(float percent, bool anim = true)
  {
    if ((double) percent < 0.0)
      percent = 0.0f;
    if ((double) percent > 1.0)
      percent = 1f;
    float nowPercent = this.nowPercent;
    this.nowPercent = percent;
    this.oldPercent = percent;
    if (anim)
    {
      this.oldPercent = nowPercent;
      this.animPhase = UIHGauge.ANIM_PHASE.WAIT;
      this.animTime = 3f;
    }
    else
      this.animPhase = UIHGauge.ANIM_PHASE.NONE;
  }

  private void LateUpdate()
  {
    if (this.animPhase == UIHGauge.ANIM_PHASE.WAIT)
    {
      this.animTime -= Time.deltaTime;
      if ((double) this.animTime <= 0.0)
      {
        this.animPhase = UIHGauge.ANIM_PHASE.MOVE;
        this.animTime = 0.5f;
      }
    }
    else if (this.animPhase == UIHGauge.ANIM_PHASE.MOVE)
    {
      this.animTime -= Time.deltaTime;
      if ((double) this.animTime <= 0.0)
      {
        this.animPhase = UIHGauge.ANIM_PHASE.NONE;
        this.animTime = 0.0f;
      }
    }
    this.UpdateGauge();
  }

  protected virtual void UpdateGauge()
  {
    if (!Object.op_Implicit((Object) this.gaugeUI))
      return;
    this.gaugeUI.value = this.nowPercent;
    if (!Object.op_Inequality((Object) this.gaugeEffectUI, (Object) null))
      return;
    float num = this.nowPercent;
    if (this.animPhase == UIHGauge.ANIM_PHASE.WAIT)
      num = this.oldPercent;
    else if (this.animPhase == UIHGauge.ANIM_PHASE.MOVE)
      num = this.nowPercent + (float) (((double) this.oldPercent - (double) this.nowPercent) * ((double) this.animTime / 0.5));
    this.gaugeEffectUI.value = num;
  }

  public Transform GetGaugeTransform()
  {
    return Object.op_Implicit((Object) this.gaugeUI) ? ((Component) this.gaugeUI).gameObject.transform : ((Component) this).gameObject.transform;
  }

  public enum ANIM_PHASE
  {
    NONE,
    WAIT,
    MOVE,
  }
}

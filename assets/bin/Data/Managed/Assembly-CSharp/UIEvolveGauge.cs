// Decompiled with JetBrains decompiler
// Type: UIEvolveGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIEvolveGauge : MonoBehaviour
{
  public GameObject longTouchTarget;
  public UISprite gauge;
  public GameObject maxEffect;
  public UISprite weaponIcon;
  public UITexture evolveIcon;
  private Transform effectTrans;
  private Vector3 effectPos = new Vector3(-3f, -26f, 0.0f);
  private Vector3 effectScale = new Vector3(0.85f, 0.85f, 0.0f);

  protected void Awake()
  {
    UILongTouch.Set(this.longTouchTarget, "EVOLVE");
    UITouchAndRelease.Set(this.longTouchTarget, "EVOLVE_TOUCH");
    this.effectTrans = EffectManager.GetUIEffect("ef_ui_skillgauge_blue_01", ((Component) this.gauge).transform.parent, 0.0f, 1, (UIWidget) this.gauge);
    if (this.effectTrans == null)
      return;
    ((Component) this.effectTrans).gameObject.SetActive(false);
  }

  private void OnDestroy() => EffectManager.ReleaseEffect(ref this.effectTrans);

  public void SetRate(float rate)
  {
    if ((double) rate <= 0.0)
      rate = 0.0f;
    if ((double) rate >= 1.0)
      rate = 1f;
    this.gauge.fillAmount = rate;
    this.maxEffect.SetActive((double) rate >= 1.0);
    this._CalcGaugeEffect(rate);
  }

  public void SetEvolveIcon(uint evolveId)
  {
    ResourceLoad.LoadEvolveIconTexture(this.evolveIcon, evolveId);
  }

  public void EnableEvolveIcon(bool isEnable)
  {
    ((Behaviour) this.weaponIcon).enabled = !isEnable;
    ((Component) this.evolveIcon).gameObject.SetActive(isEnable);
  }

  private void _CalcGaugeEffect(float rate)
  {
    if (this.effectTrans == null)
      return;
    this.effectTrans.localPosition = new Vector3(-4f, (float) (60.0 * (double) rate - 28.0), 0.0f);
    float num = (float) (0.85000002384185791 * (double) Mathf.Sin(rate * 3.14159274f) + 0.20000000298023224);
    if ((double) num >= 0.800000011920929)
      num = 0.8f;
    if ((double) num <= 0.0)
      num = 0.0f;
    this.effectTrans.localScale = new Vector3(num, num, 1f);
    ((Component) this.effectTrans).gameObject.SetActive((double) rate > 0.0 && (double) rate < 1.0);
  }
}

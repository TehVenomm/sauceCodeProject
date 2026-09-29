// Decompiled with JetBrains decompiler
// Type: SkillGrowProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SkillGrowProgress : MonoBehaviour
{
  public UIProgressBar progressBar;
  public UISprite gaugeNormal;
  public UISprite gaugeGrow;
  public UISprite gaugeExceed;

  public void SetGrowMode()
  {
    ((Component) this.gaugeGrow).gameObject.SetActive(true);
    ((Component) this.gaugeExceed).gameObject.SetActive(false);
    this.progressBar.foregroundWidget = (UIWidget) this.gaugeGrow;
  }

  public void SetExceedMode()
  {
    ((Component) this.gaugeGrow).gameObject.SetActive(false);
    ((Component) this.gaugeExceed).gameObject.SetActive(true);
    this.progressBar.foregroundWidget = (UIWidget) this.gaugeExceed;
  }

  public void SetBaseGauge(bool is_visible, float fill_amount)
  {
    ((Behaviour) this.gaugeNormal).enabled = is_visible;
    this.gaugeNormal.fillAmount = fill_amount;
  }
}

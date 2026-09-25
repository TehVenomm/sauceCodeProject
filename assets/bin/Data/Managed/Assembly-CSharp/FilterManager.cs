// Decompiled with JetBrains decompiler
// Type: FilterManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class FilterManager : MonoBehaviourSingleton<FilterManager>
{
  private BlurFilter blurFilter;

  public bool IsEnabledBlur()
  {
    return !Object.op_Equality((Object) this.blurFilter, (Object) null) && ((Behaviour) this.blurFilter).enabled;
  }

  public void StartBlur(float time = 1f, float strength = 0.25f, float delay = 0.0f)
  {
    if (Object.op_Equality((Object) this.blurFilter, (Object) null))
      return;
    this.blurFilter.blurStrength = 0.0f;
    this.blurFilter.StartFilter();
    this.StartCoroutine(this.ChangeBlurStrength(time, 0.0f, strength, delay, (System.Action) null));
  }

  public void StopBlur(float time, float delay = 0.0f)
  {
    if (Object.op_Equality((Object) this.blurFilter, (Object) null) || !((Behaviour) this.blurFilter).enabled)
      return;
    if ((double) time <= 0.0)
      this.StopBlur();
    else
      this.StartCoroutine(this.ChangeBlurStrength(time, this.blurFilter.blurStrength, 0.0f, delay, (System.Action) (() => this.StopBlur())));
  }

  public void StopBlur()
  {
    if (Object.op_Equality((Object) this.blurFilter, (Object) null) || !((Behaviour) this.blurFilter).enabled)
      return;
    this.blurFilter.StopFilter();
    ((Behaviour) this.blurFilter).enabled = false;
  }

  private IEnumerator ChangeBlurStrength(
    float time,
    float startStrength,
    float targetStrength,
    float delay,
    System.Action onComplete)
  {
    if (!Object.op_Equality((Object) this.blurFilter, (Object) null))
    {
      ((Behaviour) this.blurFilter).enabled = true;
      yield return (object) new WaitForSeconds(delay);
      for (float _time = 0.0f; (double) _time < (double) time; _time += Time.deltaTime)
      {
        float num = _time / time;
        this.blurFilter.blurStrength = (float) ((double) startStrength * (1.0 - (double) num) + (double) targetStrength * (double) num);
        yield return (object) null;
      }
      this.blurFilter.blurStrength = targetStrength;
      if (onComplete != null)
        onComplete();
    }
  }

  private void Start()
  {
    this.blurFilter = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<BlurFilter>();
    ((Behaviour) this.blurFilter).enabled = false;
  }

  public GameObject tubulanceCamera { set; get; }

  public void StartTubulanceFilter(float power, Vector2 center, System.Action callback)
  {
    this.StartCoroutine(this._StartTubulanceFilter(power, center, callback));
  }

  private IEnumerator _StartTubulanceFilter(float power, Vector2 center, System.Action callback)
  {
    if (Object.op_Equality((Object) this.tubulanceCamera, (Object) null))
      this.tubulanceCamera = ResourceUtility.Instantiate<GameObject>(Resources.Load<GameObject>("Filter/TurbulanceFilterCamera"));
    BlurAndTurbulanceFilter filter = this.tubulanceCamera.GetComponent<BlurAndTurbulanceFilter>();
    float time = 0.0f;
    while ((double) time < 2.0)
    {
      time += Time.deltaTime;
      filter.SetBlurPram(Mathf.Lerp(0.0f, power, Mathf.Clamp01(time / 0.6f)), center);
      float num = Mathf.Clamp01((float) (((double) time - 0.20000000298023224) / 1.0));
      filter.SetTurbulanceParam(Mathf.Lerp(0.0f, 0.15f, num), Mathf.Lerp(1f, 1.4f, num), Mathf.Lerp(0.0f, 1f, num));
      yield return (object) null;
    }
    if (callback != null)
      callback();
  }

  public void StopTubulanceFilter()
  {
    if (!Object.op_Inequality((Object) this.tubulanceCamera, (Object) null))
      return;
    Object.Destroy((Object) this.tubulanceCamera);
    this.tubulanceCamera = (GameObject) null;
  }
}

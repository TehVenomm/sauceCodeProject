// Decompiled with JetBrains decompiler
// Type: UIScreenRotationHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class UIScreenRotationHandler : MonoBehaviour
{
  [SerializeField]
  private bool autoInvoke;
  private bool prevIsPortrait;

  protected abstract void OnScreenRotate(bool is_portrait);

  private void Awake()
  {
    if (!this.autoInvoke || !MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    this.InvokeRotate();
  }

  public void InvokeRotate()
  {
    this.prevIsPortrait = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
    this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
  }

  private void OnEnable()
  {
    if (!this.autoInvoke || !MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    if (this.prevIsPortrait == MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
      return;
    this.InvokeRotate();
  }

  private void OnDisable()
  {
    if (!this.autoInvoke || !MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.prevIsPortrait = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
  }
}

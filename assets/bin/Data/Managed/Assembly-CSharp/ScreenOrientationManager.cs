// Decompiled with JetBrains decompiler
// Type: ScreenOrientationManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ScreenOrientationManager : MonoBehaviourSingleton<ScreenOrientationManager>
{
  private float timer;

  public event ScreenOrientationManager.OnScreenRotateDelegate OnScreenRotate;

  public bool isPortrait { get; protected set; }

  protected override void Awake() => this.isPortrait = this.CheckIsPortrait();

  private void Start() => this.EventScreenRotate(this.isPortrait);

  private void Update()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
    {
      this.timer += Time.deltaTime;
      if ((double) this.timer >= 2.0)
      {
        this.timer = 0.0f;
        GameSceneGlobalSettings.SetOrientation(MonoBehaviourSingleton<GameSceneManager>.I.isAvailableScreenRotationScene());
      }
    }
    int num1 = this.isPortrait ? 1 : 0;
    this.isPortrait = this.CheckIsPortrait();
    int num2 = this.isPortrait ? 1 : 0;
    if (num1 == num2)
      return;
    this.EventScreenRotate(this.isPortrait);
  }

  protected bool CheckIsPortrait() => Screen.width < Screen.height;

  public void EventScreenRotate(bool is_portrait)
  {
    if (this.OnScreenRotate == null)
      return;
    this.OnScreenRotate(is_portrait);
  }

  public delegate void OnScreenRotateDelegate(bool is_portrait);
}

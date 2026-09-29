// Decompiled with JetBrains decompiler
// Type: ConfigConfirmSwitchServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ConfigConfirmSwitchServer : GameSection
{
  protected UIWidget widget;
  protected UITweenCtrl tweenCtrl;

  public override void Initialize()
  {
    base.Initialize();
    this.Start();
  }

  private void Start()
  {
    this.widget = this.GetComponent<UIWidget>((Enum) ConfigConfirmSwitchServer.UI.OBJ_FRAME);
    this.tweenCtrl = this.GetComponent<UITweenCtrl>((Enum) ConfigConfirmSwitchServer.UI.OBJ_FRAME);
    this.tweenCtrl.Reset();
    this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() => { }));
  }

  public virtual void OnQuery_CREATE_ACCOUNT()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(nameof (ConfigConfirmSwitchServer));
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "AccountScene"))
      return;
    GameSection.ChangeEvent("[BACK]");
  }

  public virtual void OnQuery_CHANGE_SERVER()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(nameof (ConfigConfirmSwitchServer));
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "AccountScene"))
      return;
    GameSection.ChangeEvent("[BACK]");
  }

  private enum UI
  {
    OBJ_FRAME,
  }
}

// Decompiled with JetBrains decompiler
// Type: ConfigTouch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ConfigTouch : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void Exit()
  {
    GameSaveData.Save();
    base.Exit();
  }

  public override void UpdateUI()
  {
    this.SetProgressInt((Enum) ConfigTouch.UI.SLD_TOUCH_FLICK, Mathf.RoundToInt(GameSaveData.instance.touchInGameFlick * 100f), 0, 100, new EventDelegate.Callback(this.OnChangeTouchFlick));
    this.SetProgressInt((Enum) ConfigTouch.UI.SLD_TOUCH_LONG, Mathf.RoundToInt(GameSaveData.instance.touchInGameLong * 100f), 0, 100, new EventDelegate.Callback(this.OnChangeTouchLong));
  }

  private void OnChangeTouchFlick()
  {
    GameSaveData.instance.touchInGameFlick = (float) this.GetProgressInt((Enum) ConfigTouch.UI.SLD_TOUCH_FLICK) * 0.01f;
    if (!MonoBehaviourSingleton<InputManager>.IsValid())
      return;
    MonoBehaviourSingleton<InputManager>.I.UpdateConfigInput();
  }

  private void OnChangeTouchLong()
  {
    GameSaveData.instance.touchInGameLong = (float) this.GetProgressInt((Enum) ConfigTouch.UI.SLD_TOUCH_LONG) * 0.01f;
  }

  private enum UI
  {
    SLD_TOUCH_FLICK,
    SLD_TOUCH_LONG,
  }
}

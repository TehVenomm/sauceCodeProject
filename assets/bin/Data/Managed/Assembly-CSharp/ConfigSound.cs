// Decompiled with JetBrains decompiler
// Type: ConfigSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ConfigSound : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void Exit()
  {
    GameSaveData.Save();
    base.Exit();
  }

  public override void UpdateUI()
  {
    this.SetProgressInt((Enum) ConfigSound.UI.SLD_BGM_VOLUME, Mathf.RoundToInt(GameSaveData.instance.volumeBGM * 100f), 0, 100, new EventDelegate.Callback(this.OnChangeBGMVolume));
    this.SetProgressInt((Enum) ConfigSound.UI.SLD_SE_VOLUME, Mathf.RoundToInt(GameSaveData.instance.volumeSE * 100f), 0, 100, new EventDelegate.Callback(this.OnChangeSEVolume));
    this.SetToggle((Enum) ConfigSound.UI.TGL_VOICE_EN, GameSaveData.instance.voiceOption == 0);
    this.SetToggle((Enum) ConfigSound.UI.TGL_VOICE_JP, GameSaveData.instance.voiceOption == 1);
    this.SetToggle((Enum) ConfigSound.UI.TGL_VOICE_MUTE, GameSaveData.instance.voiceOption == 2);
  }

  private void OnChangeBGMVolume()
  {
    GameSaveData.instance.volumeBGM = (float) this.GetProgressInt((Enum) ConfigSound.UI.SLD_BGM_VOLUME) * 0.01f;
    MonoBehaviourSingleton<SoundManager>.I.UpdateConfigVolume();
  }

  private void OnChangeSEVolume()
  {
    GameSaveData.instance.volumeSE = (float) this.GetProgressInt((Enum) ConfigSound.UI.SLD_SE_VOLUME) * 0.01f;
    MonoBehaviourSingleton<SoundManager>.I.UpdateConfigVolume();
  }

  private void OnQuery_VOICE_ENGLISH()
  {
    if (GameSaveData.instance.voiceOption == 0)
      return;
    GameSaveData.instance.voiceOption = 0;
    this.RefreshUI();
    this.OnReturnToTitle();
  }

  private void OnQuery_VOICE_JAPANESE()
  {
    if (GameSaveData.instance.voiceOption == 1)
      return;
    GameSaveData.instance.voiceOption = 1;
    this.RefreshUI();
    this.OnReturnToTitle();
  }

  private void OnQuery_VOICE_MUTE()
  {
    if (GameSaveData.instance.voiceOption == 2)
      return;
    GameSaveData.instance.voiceOption = 2;
    this.RefreshUI();
  }

  private void OnReturnToTitle()
  {
    if (!MonoBehaviourSingleton<AppMain>.IsValid())
      return;
    MonoBehaviourSingleton<AppMain>.I.Reset(false, false);
  }

  private enum UI
  {
    SLD_BGM_VOLUME,
    SLD_SE_VOLUME,
    TGL_VOICE_EN,
    TGL_VOICE_JP,
    TGL_VOICE_MUTE,
  }
}

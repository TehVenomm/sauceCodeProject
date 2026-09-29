// Decompiled with JetBrains decompiler
// Type: QuestAcceptRoomSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestAcceptRoomSettings : QuestRoomSettings
{
  public override void Initialize() => base.Initialize();

  private void OnCloseDialog_QuestAcceptRoomSettingsLevel()
  {
    if (MonoBehaviourSingleton<PartyManager>.I.partySetting == null)
      return;
    this.setting.level = MonoBehaviourSingleton<PartyManager>.I.partySetting.level;
    this.setting.reserveLimitLevel = MonoBehaviourSingleton<PartyManager>.I.partySetting.level;
    MonoBehaviourSingleton<PartyManager>.I.SetPartySetting((PartyManager.PartySetting) null);
    this.RefreshUI();
  }
}

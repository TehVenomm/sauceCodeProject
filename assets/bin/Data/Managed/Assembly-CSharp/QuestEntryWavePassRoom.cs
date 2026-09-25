// Decompiled with JetBrains decompiler
// Type: QuestEntryWavePassRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestEntryWavePassRoom : QuestEntryPassRoom
{
  protected override void OnQuery_ROOM()
  {
    this.SendApply(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId());
  }

  protected new enum UI
  {
    LBL_INPUT_PASS_1,
    LBL_INPUT_PASS_2,
    LBL_INPUT_PASS_3,
    LBL_INPUT_PASS_4,
    LBL_INPUT_PASS_5,
    STR_NON_SETTINGS,
  }
}

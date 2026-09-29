// Decompiled with JetBrains decompiler
// Type: QuestStartChangeEquipSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestStartChangeEquipSet : QuestChangeEquipSet
{
  private QuestSelect selectSection;

  protected override bool IsRoomObserve() => false;

  public override void Initialize()
  {
    this.selectSection = (GameSection.GetEventData() as object[])[0] as QuestSelect;
    GameSection.SetEventData((object) new object[2]
    {
      (object) false,
      (object) false
    });
    base.Initialize();
  }

  protected override void OnQuery_DECISION()
  {
    GameSection.ChangeEvent("[BACK]");
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(this.selfCharaEquipSetNo, (Action<bool>) (is_success =>
    {
      if (is_success)
        this.selectSection.SuccessChangeEquipSet();
      GameSection.ResumeEvent(is_success);
    }));
  }
}

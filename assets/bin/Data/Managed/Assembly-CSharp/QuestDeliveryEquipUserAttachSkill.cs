// Decompiled with JetBrains decompiler
// Type: QuestDeliveryEquipUserAttachSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestDeliveryEquipUserAttachSkill : EquipSetDetailAttachSkillDialog
{
  protected override void OnQuery_SLOT()
  {
    int eventData = (int) GameSection.GetEventData();
    this.selectEquipIndex = eventData >> 16 /*0x10*/;
    this.selectSkillIndex = eventData % 65536 /*0x010000*/;
    if (this.selectSkillIndex < 0 || this.selectEquipIndex < 0)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[4]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.QUEST_ROOM,
        null,
        (object) this.equipAndSkill[this.selectEquipIndex].equipItemInfo,
        (object) this.selectSkillIndex
      });
  }
}

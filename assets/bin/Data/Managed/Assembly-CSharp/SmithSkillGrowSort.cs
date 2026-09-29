// Decompiled with JetBrains decompiler
// Type: SmithSkillGrowSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithSkillGrowSort : SkillGrowSortBase
{
  private bool isExceed;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    if (eventData[0] is SkillItemInfo skillItemInfo)
      this.baseItemID = skillItemInfo.tableID;
    SortSettings event_data = eventData[1] as SortSettings;
    GameSection.SetEventData((object) event_data);
    this.isExceed = event_data.settingsType == SortSettings.SETTINGS_TYPE.EXCEED_SKILL_ITEM;
    if (this.isExceed)
    {
      this.visible_type_flag_skill = 128 /*0x80*/;
      switch (skillItemInfo.tableData.type)
      {
        case SKILL_SLOT_TYPE.ATTACK:
          this.visible_type_flag_skill |= 1;
          break;
        case SKILL_SLOT_TYPE.SUPPORT:
          this.visible_type_flag_skill |= 2;
          break;
        case SKILL_SLOT_TYPE.HEAL:
          this.visible_type_flag_skill |= 4;
          break;
      }
    }
    base.Initialize();
  }
}

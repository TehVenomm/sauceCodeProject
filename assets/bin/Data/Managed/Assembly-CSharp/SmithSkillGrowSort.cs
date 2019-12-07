public class SmithSkillGrowSort : SkillGrowSortBase
{
	private bool isExceed;

	public override void Initialize()
	{
		object[] obj = GameSection.GetEventData() as object[];
		SkillItemInfo skillItemInfo = obj[0] as SkillItemInfo;
		if (skillItemInfo != null)
		{
			baseItemID = skillItemInfo.tableID;
		}
		SortSettings sortSettings = obj[1] as SortSettings;
		GameSection.SetEventData(sortSettings);
		isExceed = (sortSettings.settingsType == SortSettings.SETTINGS_TYPE.EXCEED_SKILL_ITEM);
		if (isExceed)
		{
			visible_type_flag_skill = 128;
			switch (skillItemInfo.tableData.type)
			{
			case SKILL_SLOT_TYPE.ATTACK:
				visible_type_flag_skill |= 1;
				break;
			case SKILL_SLOT_TYPE.SUPPORT:
				visible_type_flag_skill |= 2;
				break;
			case SKILL_SLOT_TYPE.HEAL:
				visible_type_flag_skill |= 4;
				break;
			}
		}
		base.Initialize();
	}
}

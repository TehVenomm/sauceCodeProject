public class QuestDeliveryEquipUserAttachSkill : EquipSetDetailAttachSkillDialog
{
	protected override void OnQuery_SLOT()
	{
		int num = (int)GameSection.GetEventData();
		selectEquipIndex = num >> 16;
		selectSkillIndex = num % 65536;
		if (selectSkillIndex < 0 || selectEquipIndex < 0)
		{
			GameSection.StopEvent();
			return;
		}
		EquipItemInfo equipItemInfo = equipAndSkill[selectEquipIndex].equipItemInfo;
		GameSection.SetEventData(new object[4]
		{
			ItemDetailEquip.CURRENT_SECTION.QUEST_ROOM,
			null,
			equipItemInfo,
			selectSkillIndex
		});
	}
}

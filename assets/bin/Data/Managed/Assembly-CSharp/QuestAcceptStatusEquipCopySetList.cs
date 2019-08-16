public class QuestAcceptStatusEquipCopySetList : StatusEquipCopySetList
{
	private void OnQuery_QuestAcceptStatusOrderEquipCopyConfirm_YES()
	{
		OrderEquipSetCopy();
	}

	private void OnQuery_QuestAcceptStatusEquipingCopyConfirm_YES()
	{
		EquipSetCopy();
	}

	private void OnQuery_QuestAcceptStatusOrderEquipingCopyConfirm_YES()
	{
		OrderEquipSetCopy();
	}

	protected override void TO_EQUIP_TOP()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptSeriesArenaRoomChangeEquipSet");
		EquipSetCopy();
	}

	protected override void EquipSetCopy()
	{
		base.EquipSetCopy();
	}
}

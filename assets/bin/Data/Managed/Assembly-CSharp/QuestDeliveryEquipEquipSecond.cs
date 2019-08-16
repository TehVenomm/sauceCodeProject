public class QuestDeliveryEquipEquipSecond : QuestAcceptEquipSecond
{
	private void OnCloseDialog_QuestAcceptEquipSort()
	{
		OnCloseSortDialog();
	}

	private void OnQuery_QuestDeliveryEquipMigrationSkillConfirm_YES()
	{
		base.OnQuery_StatusMigrationSkillConfirm_YES();
	}

	private void OnQuery_QuestDeliveryEquipMigrationSkillConfirm_NO()
	{
		base.OnQuery_StatusMigrationSkillConfirm_NO();
	}

	private void OnQuery_QuestDeliveryEquipSwapEquipConfirm_YES()
	{
		OnQuery_StatusSwapEquipConfirm_YES();
	}
}

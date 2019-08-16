public class QuestEntryWavePassRoom : QuestEntryPassRoom
{
	protected new enum UI
	{
		LBL_INPUT_PASS_1,
		LBL_INPUT_PASS_2,
		LBL_INPUT_PASS_3,
		LBL_INPUT_PASS_4,
		LBL_INPUT_PASS_5,
		STR_NON_SETTINGS
	}

	protected override void OnQuery_ROOM()
	{
		SendApply(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId());
	}
}

using Network;

public class LoungeEntryPassRoom : QuestEntryPassRoom
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

	private void OnQuery_LOUNGE()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(string.Join(string.Empty, passCode), delegate(bool is_apply, Error ret_code)
		{
			if (is_apply && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
			{
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate
					{
					});
				});
			}
			else if (ret_code == Error.WRN_PARTY_SEARCH_NOT_FOUND_PARTY || ret_code == Error.WRN_PARTY_OWNER_REJOIN)
			{
				GameSection.ChangeStayEvent("NOT_FOUND_PARTY");
				GameSection.ResumeEvent(is_resume: true);
			}
			else
			{
				GameSection.ResumeEvent(is_apply);
			}
		});
	}
}

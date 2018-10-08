using Network;
using System.Collections;

public class GuildEntryPassRoom : QuestEntryPassRoom
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

	private GuildStatisticInfo _info;

	private IEnumerator GetClanStatistic(int clanID)
	{
		bool finish_get_statistic = false;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, delegate(bool success, GuildStatisticInfo info)
		{
			((_003CGetClanStatistic_003Ec__Iterator57)/*Error near IL_0033: stateMachine*/)._003Cfinish_get_statistic_003E__0 = true;
			((_003CGetClanStatistic_003Ec__Iterator57)/*Error near IL_0033: stateMachine*/)._003C_003Ef__this._info = info;
		});
		while (!finish_get_statistic)
		{
			yield return (object)null;
		}
		HandleEvent(clanID);
	}

	private void OnQuery_FIND()
	{
		GameSection.StayEvent();
		try
		{
			int clanId = int.Parse(string.Join(string.Empty, passCode));
			MonoBehaviourSingleton<GuildManager>.I.SendSearchWithID(clanId, delegate(bool is_success, Error err)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
		catch
		{
			GameSection.ResumeEvent(true, null);
		}
	}

	private void HandleEvent(int clanId)
	{
		if (_info != null)
		{
			if (_info.privacy == 0)
			{
				MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(clanId, -1, delegate
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Guild", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
				});
			}
			else if (_info.privacy == 1)
			{
				MonoBehaviourSingleton<GuildManager>.I.SendRequestRequest(clanId, -1, delegate(bool isSuccess, Error error)
				{
					if (isSuccess)
					{
						OpenDialog();
					}
				});
			}
			GameSection.ResumeEvent(true, null);
		}
	}

	private void OpenDialog()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, base.sectionData.GetText("TEXT_NOITICE"), null, null, null, null), delegate
		{
		}, false, 0);
	}
}

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

	public override string overrideBackKeyEvent => "[BACK]";

	private IEnumerator GetClanStatistic(int clanID)
	{
		bool finish_get_statistic = false;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, delegate(bool success, GuildStatisticInfo info)
		{
			finish_get_statistic = true;
			_info = info;
		});
		while (!finish_get_statistic)
		{
			yield return null;
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
				GameSection.ResumeEvent(is_success);
			});
		}
		catch
		{
			GameSection.ResumeEvent(is_resume: true);
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
					MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Guild");
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
			GameSection.ResumeEvent(is_resume: true);
		}
	}

	private void OpenDialog()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, base.sectionData.GetText("TEXT_NOITICE")), delegate
		{
		});
	}
}

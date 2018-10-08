using Network;
using System;
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

	private unsafe IEnumerator GetClanStatistic(int clanID)
	{
		bool finish_get_statistic = false;
		MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, new Action<bool, GuildStatisticInfo>((object)/*Error near IL_0033: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (!finish_get_statistic)
		{
			yield return (object)null;
		}
		HandleEvent(clanID);
	}

	private unsafe void OnQuery_FIND()
	{
		GameSection.StayEvent();
		try
		{
			int num = int.Parse(string.Join(string.Empty, passCode));
			GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
			int clanId = num;
			if (_003C_003Ef__am_0024cache1 == null)
			{
				_003C_003Ef__am_0024cache1 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendSearchWithID(clanId, _003C_003Ef__am_0024cache1);
		}
		catch
		{
			GameSection.ResumeEvent(true, null, false);
		}
	}

	private unsafe void HandleEvent(int clanId)
	{
		if (_info != null)
		{
			if (_info.privacy == 0)
			{
				GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
				if (_003C_003Ef__am_0024cache2 == null)
				{
					_003C_003Ef__am_0024cache2 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				i.SendRequestJoin(clanId, -1, _003C_003Ef__am_0024cache2);
			}
			else if (_info.privacy == 1)
			{
				MonoBehaviourSingleton<GuildManager>.I.SendRequestRequest(clanId, -1, new Action<bool, Error>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			GameSection.ResumeEvent(true, null, false);
		}
	}

	private void OpenDialog()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, base.sectionData.GetText("TEXT_NOITICE"), null, null, null, null), delegate
		{
		}, false, 0);
	}
}

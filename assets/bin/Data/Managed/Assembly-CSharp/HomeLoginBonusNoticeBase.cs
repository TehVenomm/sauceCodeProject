using Network;
using System.Text;

public class HomeLoginBonusNoticeBase : GameSection
{
	private enum UI
	{
		LBL_BONUS_NAME,
		ProvisionalLabel
	}

	private LoginBonus bonus;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		bonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
		MonoBehaviourSingleton<AccountManager>.I.logInBonus.Remove(bonus);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (bonus != null)
		{
			SetLabelText(UI.LBL_BONUS_NAME, bonus.name);
			StringBuilder sb = new StringBuilder();
			int count = bonus.reward.Count;
			int index = 0;
			bonus.reward.ForEach(delegate(LoginBonus.LoginBonusReward o)
			{
				int num = ++index;
				if (index < count)
				{
					sb.AppendLine(o.name);
				}
				else
				{
					sb.Append(o.name);
				}
			});
			SetLabelText(UI.ProvisionalLabel, sb.ToString());
			UpdateAnchors();
		}
	}

	private void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}

using Network;
using System.Collections;

public class LimitedLoginBonusCheck : GameSection
{
	public override void Initialize()
	{
		base.Initialize();
		this.StartCoroutine("DoCheck");
	}

	private IEnumerator DoCheck()
	{
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return null;
		}
		CheckNextLoginBonus();
	}

	private void CheckNextLoginBonus()
	{
		if (MonoBehaviourSingleton<AccountManager>.I.logInBonus == null)
		{
			GameSection.BackSection();
			return;
		}
		if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count == 0)
		{
			GameSection.BackSection();
			return;
		}
		LoginBonus loginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
		if (loginBonus.priority > 0)
		{
			DispatchEvent("LIMITED_LOGIN_BONUS");
		}
		else if (loginBonus.type == 0)
		{
			DispatchEvent("LOGIN_BONUS");
		}
		else
		{
			GameSection.BackSection();
		}
	}

	public void OnCloseDialog(string section_name)
	{
		this.StartCoroutine("DoCheck");
	}
}

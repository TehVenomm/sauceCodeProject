using Network;
using System.Collections;

public class LimitedLoginBonusCheck : GameSection
{
	public override void Initialize()
	{
		base.Initialize();
		StartCoroutine("DoCheck");
	}

	private IEnumerator DoCheck()
	{
		while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			yield return (object)null;
		}
		CheckNextLoginBonus();
	}

	private void CheckNextLoginBonus()
	{
		if (MonoBehaviourSingleton<AccountManager>.I.logInBonus == null)
		{
			GameSection.BackSection();
		}
		else if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count == 0)
		{
			GameSection.BackSection();
		}
		else
		{
			LoginBonus loginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
			if (loginBonus.priority > 0)
			{
				DispatchEvent("LIMITED_LOGIN_BONUS", null);
			}
			else if (loginBonus.type == 0)
			{
				DispatchEvent("LOGIN_BONUS", null);
			}
			else
			{
				GameSection.BackSection();
			}
		}
	}

	public void OnCloseDialog(string section_name)
	{
		StartCoroutine("DoCheck");
	}
}

using System;

public class CheckTutorialBit
{
	public string TutorialBitString;

	public CheckTutorialBit()
		: this()
	{
	}

	private void Start()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
		{
			TUTORIAL_MENU_BIT bit = (TUTORIAL_MENU_BIT)(int)Enum.Parse(typeof(TUTORIAL_MENU_BIT), TutorialBitString);
			if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(bit))
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}
}

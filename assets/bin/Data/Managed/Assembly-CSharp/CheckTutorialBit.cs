using System;
using UnityEngine;

public class CheckTutorialBit : MonoBehaviour
{
	public string TutorialBitString;

	public CheckTutorialBit()
		: this()
	{
	}

	private void Start()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
		{
			TUTORIAL_MENU_BIT bit = (TUTORIAL_MENU_BIT)Enum.Parse(typeof(TUTORIAL_MENU_BIT), TutorialBitString);
			if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(bit))
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}
}

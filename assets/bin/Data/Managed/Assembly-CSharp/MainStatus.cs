using Network;
using System;

public class MainStatus : UIBehaviour
{
	private enum UI
	{
		LBL_NAME,
		LBL_LEVEL,
		LBL_CRYSTAL,
		LBL_MONEY,
		PBR_EXP,
		SPR_BG02,
		SPR_EXP_NEXT,
		STR_NEXT_EXP,
		LBL_NEXT_EXP,
		STR_NOW_EXP,
		LBL_NOW_EXP,
		SPR_BADGE,
		BTN_MENU,
		LBL_BOOST_RATE,
		LBL_BOOST_TIME,
		OBJ_BOOST_EXP_ROOT,
		OBJ_BOOST_DROP_ROOT,
		OBJ_BOOST_GOLD_ROOT,
		OBJ_BOOST_HGP_ROOT,
		OBJ_BOOST_NOVICE_DROP_ROOT,
		SPR_TUTORIAL_CURSOR_UP
	}

	private StatusBoostAnimator boostAnimator;

	protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return GameSection.NOTIFY_FLAG.PRETREAT_SCENE | GameSection.NOTIFY_FLAG.CHANGED_SCENE | GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM | GameSection.NOTIFY_FLAG.UPDATE_USER_INFO | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS;
	}

	public override void OnNotify(GameSection.NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) != (GameSection.NOTIFY_FLAG)0L)
		{
			NoEventReleaseTouchAndRelease((Enum)UI.SPR_BG02);
			OnQuery_EXP_NEXT_HIDE();
		}
	}

	public override void UpdateUI()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		SetSupportEncoding(base._transform, UI.LBL_NAME, true);
		SetLabelText((Enum)UI.LBL_NAME, Utility.GetNameWithColoredClanTag(string.Empty, userInfo.name, true, true));
		SetLabelText((Enum)UI.LBL_LEVEL, userStatus.level.ToString());
		SetLabelText((Enum)UI.LBL_CRYSTAL, userStatus.crystal.ToString("N0"));
		SetLabelText((Enum)UI.LBL_MONEY, userStatus.money.ToString("N0"));
		SetProgressValue((Enum)UI.PBR_EXP, userStatus.ExpProgress01);
		InitDeactive((Enum)UI.SPR_EXP_NEXT);
		if (TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && TutorialMessage.GetCursor(0) == null && userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			SetTouchAndRelease((Enum)UI.SPR_BG02, "EXP_NEXT_SHOW", "EXP_NEXT_HIDE", (object)null);
		}
		SetBadge((Enum)UI.BTN_MENU, MonoBehaviourSingleton<PresentManager>.I.presentNum + MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum + (GameSaveData.instance.IsShowNewsNotification() ? 1 : 0), 3, -15, -8, false);
		if (boostAnimator == null)
		{
			boostAnimator = this.GetComponentInChildren<StatusBoostAnimator>();
		}
		boostAnimator.SetupUI(delegate(BoostStatus update_boost)
		{
			if (update_boost != null)
			{
				UpdateShowBoost(update_boost);
			}
			else
			{
				EndShowBoost();
			}
		}, delegate(BoostStatus change_boost)
		{
			if (change_boost != null)
			{
				ChangeShowBoost(change_boost.type);
				UpdateShowBoost(change_boost);
			}
			else
			{
				EndShowBoost();
			}
		});
		SetFontStyle((Enum)UI.LBL_BOOST_RATE, 2);
		SetFontStyle((Enum)UI.LBL_BOOST_TIME, 2);
		if (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("HomeScene"))
		{
			SetActive((Enum)UI.SPR_TUTORIAL_CURSOR_UP, false);
		}
	}

	public void OnQuery_EXP_NEXT_SHOW()
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		SetActive((Enum)UI.SPR_EXP_NEXT, true);
		SetLabelText((Enum)UI.STR_NOW_EXP, StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 2u));
		SetLabelText((Enum)UI.STR_NEXT_EXP, StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 3u));
		SetLabelText((Enum)UI.LBL_NOW_EXP, userStatus.exp.ToString());
		if ((int)userStatus.level >= Singleton<UserLevelTable>.I.GetMaxLevel())
		{
			SetLabelText((Enum)UI.LBL_NEXT_EXP, "-");
		}
		else
		{
			SetLabelText((Enum)UI.LBL_NEXT_EXP, (userStatus.ExpNext - (int)userStatus.exp).ToString());
		}
	}

	public void OnQuery_EXP_NEXT_HIDE()
	{
		SetActive((Enum)UI.SPR_EXP_NEXT, false);
	}

	public void OnQuery_SHOW_GEMS_DIALOG()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CrystalShop", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	public void OnQuery_SHOW_PROFILE_DIALOG()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Profile", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
		{
			TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS, delegate
			{
				SetTutArrowActive(false);
			});
		}
	}

	public void SetMenuButtonEnable(bool is_enable)
	{
		SetButtonEnabled((Enum)UI.BTN_MENU, is_enable);
	}

	private void EndShowBoost()
	{
		ChangeShowBoost(0);
	}

	private void ChangeShowBoost(int _show_type)
	{
		SetActive((Enum)UI.LBL_BOOST_RATE, _show_type != 0);
		SetActive((Enum)UI.OBJ_BOOST_DROP_ROOT, _show_type == 3);
		SetActive((Enum)UI.OBJ_BOOST_EXP_ROOT, _show_type == 1);
		SetActive((Enum)UI.OBJ_BOOST_GOLD_ROOT, _show_type == 2);
		SetActive((Enum)UI.OBJ_BOOST_HGP_ROOT, _show_type == 201);
		SetActive((Enum)UI.OBJ_BOOST_NOVICE_DROP_ROOT, _show_type == 210);
		switch (_show_type)
		{
		case 3:
			ResetTween((Enum)UI.OBJ_BOOST_DROP_ROOT, 0);
			PlayTween((Enum)UI.OBJ_BOOST_DROP_ROOT, true, (EventDelegate.Callback)null, false, 0);
			break;
		case 1:
			ResetTween((Enum)UI.OBJ_BOOST_EXP_ROOT, 0);
			PlayTween((Enum)UI.OBJ_BOOST_EXP_ROOT, true, (EventDelegate.Callback)null, false, 0);
			break;
		case 2:
			ResetTween((Enum)UI.OBJ_BOOST_GOLD_ROOT, 0);
			PlayTween((Enum)UI.OBJ_BOOST_GOLD_ROOT, true, (EventDelegate.Callback)null, false, 0);
			break;
		case 201:
			ResetTween((Enum)UI.OBJ_BOOST_HGP_ROOT, 0);
			PlayTween((Enum)UI.OBJ_BOOST_HGP_ROOT, true, (EventDelegate.Callback)null, false, 0);
			break;
		case 210:
			ResetTween((Enum)UI.OBJ_BOOST_NOVICE_DROP_ROOT, 0);
			PlayTween((Enum)UI.OBJ_BOOST_NOVICE_DROP_ROOT, true, (EventDelegate.Callback)null, false, 0);
			break;
		}
	}

	private void UpdateShowBoost(BoostStatus boost)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		switch (boost.type)
		{
		case 1:
		case 2:
		case 3:
		case 201:
		case 210:
			SetColor((Enum)UI.LBL_BOOST_RATE, boostAnimator.GetRateColor(boost.value));
			SetLabelText((Enum)UI.LBL_BOOST_RATE, boost.GetBoostRateText());
			SetLabelText((Enum)UI.LBL_BOOST_TIME, (boost.type != 210) ? boost.GetRemainTime() : string.Empty);
			break;
		}
	}

	public void SetTutArrowActive(bool isActive)
	{
		if (isActive)
		{
			SetActive((Enum)UI.SPR_TUTORIAL_CURSOR_UP, true);
		}
		else
		{
			SetActive((Enum)UI.SPR_TUTORIAL_CURSOR_UP, false);
		}
	}
}

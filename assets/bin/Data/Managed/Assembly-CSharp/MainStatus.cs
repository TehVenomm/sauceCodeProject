using Network;
using UnityEngine;

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
		OBJ_BOOST_NOVICE_DROP_ROOT
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
			NoEventReleaseTouchAndRelease(UI.SPR_BG02);
			OnQuery_EXP_NEXT_HIDE();
		}
	}

	public override void UpdateUI()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		SetSupportEncoding(base._transform, UI.LBL_NAME, true);
		SetLabelText(UI.LBL_NAME, Utility.GetNameWithColoredClanTag(string.Empty, userInfo.name, true, true));
		SetLabelText(UI.LBL_LEVEL, userStatus.level.ToString());
		SetLabelText(UI.LBL_CRYSTAL, userStatus.crystal.ToString("N0"));
		SetLabelText(UI.LBL_MONEY, userStatus.money.ToString("N0"));
		SetProgressValue(UI.PBR_EXP, userStatus.ExpProgress01);
		InitDeactive(UI.SPR_EXP_NEXT);
		if (TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && (Object)TutorialMessage.GetCursor(0) == (Object)null)
		{
			SetTouchAndRelease(UI.SPR_BG02, "EXP_NEXT_SHOW", "EXP_NEXT_HIDE", null);
		}
		SetBadge(UI.BTN_MENU, MonoBehaviourSingleton<PresentManager>.I.presentNum + MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum + (GameSaveData.instance.IsShowNewsNotification() ? 1 : 0), SpriteAlignment.TopRight, -15, -8, false);
		if ((Object)boostAnimator == (Object)null)
		{
			boostAnimator = GetComponentInChildren<StatusBoostAnimator>();
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
		SetFontStyle(UI.LBL_BOOST_RATE, FontStyle.Italic);
		SetFontStyle(UI.LBL_BOOST_TIME, FontStyle.Italic);
	}

	public void OnQuery_EXP_NEXT_SHOW()
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		SetActive(UI.SPR_EXP_NEXT, true);
		SetLabelText(UI.STR_NOW_EXP, StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 2u));
		SetLabelText(UI.STR_NEXT_EXP, StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 3u));
		SetLabelText(UI.LBL_NOW_EXP, userStatus.exp.ToString());
		if ((int)userStatus.level >= Singleton<UserLevelTable>.I.GetMaxLevel())
		{
			SetLabelText(UI.LBL_NEXT_EXP, "-");
		}
		else
		{
			SetLabelText(UI.LBL_NEXT_EXP, (userStatus.ExpNext - (int)userStatus.exp).ToString());
		}
	}

	public void OnQuery_EXP_NEXT_HIDE()
	{
		SetActive(UI.SPR_EXP_NEXT, false);
	}

	public void OnQuery_SHOW_GEMS_DIALOG()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CrystalShop", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	public void OnQuery_SHOW_PROFILE_DIALOG()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Profile", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	public void SetMenuButtonEnable(bool is_enable)
	{
		SetButtonEnabled(UI.BTN_MENU, is_enable);
	}

	private void EndShowBoost()
	{
		ChangeShowBoost(0);
	}

	private void ChangeShowBoost(int _show_type)
	{
		SetActive(UI.LBL_BOOST_RATE, _show_type != 0);
		SetActive(UI.OBJ_BOOST_DROP_ROOT, _show_type == 3);
		SetActive(UI.OBJ_BOOST_EXP_ROOT, _show_type == 1);
		SetActive(UI.OBJ_BOOST_GOLD_ROOT, _show_type == 2);
		SetActive(UI.OBJ_BOOST_HGP_ROOT, _show_type == 201);
		SetActive(UI.OBJ_BOOST_NOVICE_DROP_ROOT, _show_type == 210);
		switch (_show_type)
		{
		case 3:
			ResetTween(UI.OBJ_BOOST_DROP_ROOT, 0);
			PlayTween(UI.OBJ_BOOST_DROP_ROOT, true, null, false, 0);
			break;
		case 1:
			ResetTween(UI.OBJ_BOOST_EXP_ROOT, 0);
			PlayTween(UI.OBJ_BOOST_EXP_ROOT, true, null, false, 0);
			break;
		case 2:
			ResetTween(UI.OBJ_BOOST_GOLD_ROOT, 0);
			PlayTween(UI.OBJ_BOOST_GOLD_ROOT, true, null, false, 0);
			break;
		case 201:
			ResetTween(UI.OBJ_BOOST_HGP_ROOT, 0);
			PlayTween(UI.OBJ_BOOST_HGP_ROOT, true, null, false, 0);
			break;
		case 210:
			ResetTween(UI.OBJ_BOOST_NOVICE_DROP_ROOT, 0);
			PlayTween(UI.OBJ_BOOST_NOVICE_DROP_ROOT, true, null, false, 0);
			break;
		}
	}

	private void UpdateShowBoost(BoostStatus boost)
	{
		switch (boost.type)
		{
		case 1:
		case 2:
		case 3:
		case 201:
		case 210:
			SetColor(UI.LBL_BOOST_RATE, boostAnimator.GetRateColor(boost.value));
			SetLabelText(UI.LBL_BOOST_RATE, boost.GetBoostRateText());
			SetLabelText(UI.LBL_BOOST_TIME, (boost.type != 210) ? boost.GetRemainTime() : string.Empty);
			break;
		}
	}
}

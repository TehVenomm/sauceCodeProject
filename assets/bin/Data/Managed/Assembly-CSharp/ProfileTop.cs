using Network;
using System;
using System.Collections;
using UnityEngine;

public class ProfileTop : GameSection
{
	private enum UI
	{
		LBL_USER_ID,
		LBL_COMMENT,
		LBL_NAME,
		LBL_LEVEL,
		BTN_LOGIN,
		BTN_DISCONNECT,
		BTN_MESSAGE,
		TEX_MODEL,
		OBJ_PROFILE_BG,
		BTN_DEGREE,
		OBJ_DEGREE_ROOT
	}

	private PlayerLoader playerLoader;

	private UIRenderTexture renderTexture;

	private UITexture uiTexture;

	private Transform playerShadow;

	private DegreePlate degree;

	private UIEventListener eventListener;

	protected void OnEnable()
	{
		if ((UnityEngine.Object)eventListener != (UnityEngine.Object)null)
		{
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
	}

	protected void OnDisable()
	{
		UIEventListener uIEventListener = eventListener;
		uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Remove(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		if (!((UnityEngine.Object)playerLoader == (UnityEngine.Object)null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ProfileTop"))
		{
			playerLoader.transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}

	private void OnDrag(GameObject obj, Vector2 move)
	{
		if (!((UnityEngine.Object)playerLoader == (UnityEngine.Object)null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ProfileTop"))
		{
			playerLoader.transform.Rotate(GameDefine.GetCharaRotateVector(move));
		}
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		degree = GetCtrl(UI.OBJ_DEGREE_ROOT).GetComponent<DegreePlate>();
		UserStatus status = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		EquipSetInfo equip_set = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(status.eSetNo);
		uint visual_armor = uint.Parse(status.armorUniqId);
		uint visual_helm = uint.Parse(status.helmUniqId);
		uint visual_arm = uint.Parse(status.armUniqId);
		uint visual_leg = uint.Parse(status.legUniqId);
		bool is_show_helm = MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(status.eSetNo) == 1;
		PlayerLoadInfo load_info = new PlayerLoadInfo();
		load_info.SetupLoadInfo(equip_set, 0uL, visual_armor, visual_helm, visual_arm, visual_leg, is_show_helm);
		OutGameSettingsManager.ProfileScene param = MonoBehaviourSingleton<OutGameSettingsManager>.I.profileScene;
		UIRenderTexture rt = InitRenderTexture(UI.TEX_MODEL, param.cameraFieldOfView, false);
		if (!object.ReferenceEquals(rt, null))
		{
			rt.nearClipPlane = param.nearClip;
		}
		EnableRenderTexture(UI.TEX_MODEL);
		SetRenderPlayerModel(UI.TEX_MODEL, load_info, PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), param.playerPos, new Vector3(0f, param.playerRot, 0f), is_show_helm, delegate(PlayerLoader x)
		{
			((_003CDoInitialize_003Ec__Iterator10B)/*Error near IL_01e7: stateMachine*/)._003C_003Ef__this.playerLoader = x;
			((_003CDoInitialize_003Ec__Iterator10B)/*Error near IL_01e7: stateMachine*/)._003Cwait_003E__0 = false;
		});
		if ((UnityEngine.Object)eventListener == (UnityEngine.Object)null)
		{
			eventListener = GetCtrl(UI.OBJ_PROFILE_BG).GetComponent<UIEventListener>();
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
		while (wait)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetSupportEncoding(base._transform, UI.LBL_NAME, true);
		SetLabelText(UI.LBL_NAME, Utility.GetNameWithColoredClanTag(string.Empty, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, true, true));
		SetLabelText(UI.LBL_USER_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
		SetLabelText(UI.LBL_COMMENT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment);
		SetLabelText(UI.LBL_LEVEL, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level.ToString());
		_UpdateFB();
		SetActive(UI.BTN_DEGREE, GameDefine.ACTIVE_DEGREE);
		if (GameDefine.ACTIVE_DEGREE)
		{
			degree.Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, false, delegate
			{
				degree.gameObject.SetActive(false);
				degree.gameObject.SetActive(true);
			});
		}
	}

	private void _UpdateFB()
	{
		SetActive(UI.BTN_LOGIN, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
		SetActive(UI.BTN_DISCONNECT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((NOTIFY_FLAG.FACEBOOK_LOGIN & flags) != (NOTIFY_FLAG)0L)
		{
			RefreshUI();
		}
	}

	protected override void OnDestroy()
	{
		if ((UnityEngine.Object)uiTexture != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(uiTexture.gameObject);
		}
		if ((UnityEngine.Object)playerShadow != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(playerShadow.gameObject);
		}
		if ((UnityEngine.Object)renderTexture != (UnityEngine.Object)null)
		{
			renderTexture.Disable();
		}
		base.OnDestroy();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_INFO | NOTIFY_FLAG.UPDATE_DEGREE_FRAME;
	}

	private void OnQuery_CHARA_MAKE()
	{
		GameSection.SetEventData(new object[3]
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus,
			2
		});
	}

	private void OnQuery_NAMECHANGE()
	{
		GameSection.SetEventData(new object[3]
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus,
			1
		});
	}

	private void OnQuery_SECTION_BACK()
	{
		Close(UITransition.TYPE.CLOSE);
	}

	private void OnQuery_LOGIN_FB()
	{
		GameSection.StayEvent();
		if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
		{
			_SendRegistLinkFacebook();
		}
		else
		{
			MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission(delegate(bool success, string s)
			{
				if (success)
				{
					_SendRegistLinkFacebook();
				}
				else
				{
					GameSection.ResumeEvent(success, null);
				}
			});
		}
	}

	private void _SendRegistLinkFacebook()
	{
		MonoBehaviourSingleton<AccountManager>.I.SendRegistLinkFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, delegate(bool success, RegistLinkFacebookModel ret)
		{
			if (success)
			{
				MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate
				{
					if (success)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.FACEBOOK_LOGIN);
						GameSection.ChangeStayEvent("ACCOUNT_LOGIN", null);
					}
					GameSection.ResumeEvent(success, null);
				});
			}
			else
			{
				if (ret.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED)
				{
					GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", ret.existInfo);
					success = true;
				}
				GameSection.ResumeEvent(success, null);
			}
		});
	}

	private void OnQuery_DISCONNECT_FB()
	{
		GameSection.ChangeEvent("ACCOUNT_UNBIND_CONFIRM", null);
	}

	private void OnQuery_COPY_CLIPBOARD()
	{
		ClipBoard.SetClipBoard(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
		GameSection.SetEventData(new object[1]
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code
		});
	}

	private void OnQuery_ProfileAccountUnbindConfirm_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FBManager>.I.Logout(delegate(bool fb_success, string s)
		{
			if (fb_success)
			{
				MonoBehaviourSingleton<AccountManager>.I.SendRegistUnlinkFacebook(delegate(bool success)
				{
					if (success)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.FACEBOOK_LOGIN);
					}
					GameSection.ResumeEvent(success, null);
				});
			}
			else
			{
				GameSection.ResumeEvent(fb_success, null);
			}
		});
	}
}

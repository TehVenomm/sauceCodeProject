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
		if (eventListener != null)
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ProfileTop"))
		{
			playerLoader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}

	private void OnDrag(GameObject obj, Vector2 move)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ProfileTop"))
		{
			playerLoader.get_transform().Rotate(GameDefine.GetCharaRotateVector(move));
		}
	}

	public override void Initialize()
	{
		this.StartCoroutine(DoInitialize());
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
		UIRenderTexture rt = InitRenderTexture(UI.TEX_MODEL, param.cameraFieldOfView);
		if (!object.ReferenceEquals(rt, null))
		{
			rt.nearClipPlane = param.nearClip;
		}
		EnableRenderTexture(UI.TEX_MODEL);
		SetRenderPlayerModel((Enum)UI.TEX_MODEL, load_info, PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), param.playerPos, new Vector3(0f, param.playerRot, 0f), is_show_helm, (Action<PlayerLoader>)delegate(PlayerLoader x)
		{
			playerLoader = x;
			wait = false;
		});
		if (eventListener == null)
		{
			eventListener = GetCtrl(UI.OBJ_PROFILE_BG).GetComponent<UIEventListener>();
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
		while (wait)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetSupportEncoding(base._transform, UI.LBL_NAME, isEnable: true);
		SetLabelText((Enum)UI.LBL_NAME, Utility.GetNameWithColoredClanTag(string.Empty, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, own: true, isSameTeam: true));
		SetLabelText((Enum)UI.LBL_USER_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
		SetLabelText((Enum)UI.LBL_COMMENT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment);
		SetLabelText((Enum)UI.LBL_LEVEL, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level.ToString());
		_UpdateFB();
		SetActive((Enum)UI.BTN_DEGREE, GameDefine.ACTIVE_DEGREE);
		if (GameDefine.ACTIVE_DEGREE)
		{
			degree.Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, isButton: false, delegate
			{
				degree.get_gameObject().SetActive(false);
				degree.get_gameObject().SetActive(true);
			});
		}
	}

	private void _UpdateFB()
	{
		SetActive((Enum)UI.BTN_LOGIN, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
		SetActive((Enum)UI.BTN_DISCONNECT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
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
		if (uiTexture != null)
		{
			Object.Destroy(uiTexture.get_gameObject());
		}
		if (playerShadow != null)
		{
			Object.Destroy(playerShadow.get_gameObject());
		}
		if (renderTexture != null)
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
		Close();
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
					GameSection.ResumeEvent(success);
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
						GameSection.ChangeStayEvent("ACCOUNT_LOGIN");
					}
					GameSection.ResumeEvent(success);
				});
			}
			else
			{
				if (ret.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED)
				{
					GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", ret.existInfo);
					success = true;
				}
				GameSection.ResumeEvent(success);
			}
		});
	}

	private void OnQuery_DISCONNECT_FB()
	{
		GameSection.ChangeEvent("ACCOUNT_UNBIND_CONFIRM");
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
					GameSection.ResumeEvent(success);
				});
			}
			else
			{
				GameSection.ResumeEvent(fb_success);
			}
		});
	}
}

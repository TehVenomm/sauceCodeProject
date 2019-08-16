using Network;
using System;
using UnityEngine;

public class StatusStageManager : MonoBehaviourSingleton<StatusStageManager>
{
	public enum VIEW_TYPE
	{
		INIT,
		STATUS,
		SMITH
	}

	public enum VIEW_MODE
	{
		EQUIP,
		AVATAR
	}

	private VIEW_TYPE viewType;

	private VIEW_MODE viewMode;

	private UITexture uiTexture;

	private UIRenderTexture renderTexture;

	private PlayerLoader playerLoader;

	private Transform playerShadow;

	private Camera targetCamera;

	private Transform targetCameraTransform;

	private OutGameSettingsManager.StatusScene parameter;

	private Vector3Interpolator cameraPosAnim = new Vector3Interpolator();

	private QuaternionInterpolator cameraRotAnim = new QuaternionInterpolator();

	private QuaternionInterpolator playerRotAnim = new QuaternionInterpolator();

	private FloatInterpolator cameraFovAnim = new FloatInterpolator();

	private bool cameraTurningMode;

	private StatusEquip.LocalEquipSetData equipSetData;

	private EquipItemInfo equipInfo;

	private StatusSmithCharacter m_stSmithCharacter;

	private StatusSmithCharacter m_stUniqueSmithCharacter;

	public bool isBusy => cameraPosAnim.IsPlaying() || cameraRotAnim.IsPlaying();

	public PlayerLoader GetPlayerLoader()
	{
		return playerLoader;
	}

	public int GetPlayerLayer()
	{
		return renderTexture.renderLayer;
	}

	protected override void Awake()
	{
		base.Awake();
		targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		parameter = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
		uiTexture = (Utility.CreateGameObjectAndComponent("UITexture", MonoBehaviourSingleton<UIManager>.I.system._transform, 5) as UITexture);
		uiTexture.shader = ResourceUtility.FindShader("Unlit/ui_render_tex");
		uiTexture.SetAnchor(MonoBehaviourSingleton<UIManager>.I.system.get_gameObject(), 0, 0, 0, 0);
		uiTexture.UpdateAnchors();
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			UIWidget component = uiTexture.get_gameObject().GetComponent<UIWidget>();
			component.width = 480;
			component.height = 854;
		}
		m_stSmithCharacter = (StatusSmithCharacter)Utility.CreateGameObjectAndComponent("StatusSmithCharacter", base._transform);
		m_stSmithCharacter.isUnique = false;
		m_stUniqueSmithCharacter = (StatusSmithCharacter)Utility.CreateGameObjectAndComponent("StatusSmithCharacter", base._transform);
		m_stUniqueSmithCharacter.isUnique = true;
	}

	protected override void _OnDestroy()
	{
		if (uiTexture != null)
		{
			Object.Destroy(uiTexture.get_gameObject());
		}
		if (playerShadow != null)
		{
			Object.Destroy(playerShadow.get_gameObject());
		}
	}

	private void OnEnable()
	{
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			if (!(currentSectionName != "StatusTop") || !(currentSectionName != "StatusAvatar") || !(currentSectionName != "StatusAccessory") || !(currentSectionName != "UniqueStatusTop"))
			{
				playerLoader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
			}
		}
	}

	public void SetUITextureActive(bool active)
	{
		uiTexture.get_gameObject().SetActive(active);
	}

	public void ClearPlayerLoaded(PlayerLoadInfo load_info)
	{
		if ((!(playerLoader != null) || !playerLoader.loadInfo.Equals(load_info)) && renderTexture != null)
		{
			Object.DestroyImmediate(renderTexture);
		}
	}

	public void LoadPlayer(PlayerLoadInfo load_info, int anim_id = 0)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (playerShadow == null)
		{
			playerShadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, fixedY0: false);
			playerShadow.get_transform().set_position(parameter.playerPos + new Vector3(0f, 0.005f, 0f));
		}
		ShaderGlobal.lightProbe = false;
		if (!(playerLoader != null) || !playerLoader.loadInfo.Equals(load_info))
		{
			if (renderTexture != null)
			{
				Object.DestroyImmediate(renderTexture);
			}
			renderTexture = UIRenderTexture.Get(uiTexture, -1f, link_main_camera: true);
			renderTexture.Disable();
			renderTexture.nearClipPlane = parameter.renderTextureNearClip;
			int num = -1;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
			{
				num = ((!MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isChangeHairShader) ? (-1) : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hairColorId);
			}
			int num2 = anim_id;
			if (num2 == 0)
			{
				num2 = PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
			}
			playerLoader = renderTexture.modelTransform.get_gameObject().AddComponent<PlayerLoader>();
			PlayerLoader obj = playerLoader;
			int renderLayer = renderTexture.renderLayer;
			int anim_id2 = num2;
			bool need_anim_event = false;
			bool need_foot_stamp = false;
			bool need_shadow = false;
			bool enable_light_probes = false;
			bool need_action_voice = false;
			bool need_high_reso_tex = true;
			bool need_res_ref_count = true;
			bool need_dev_frame_instantiate = true;
			SHADER_TYPE shader_type = SHADER_TYPE.NORMAL;
			PlayerLoader.OnCompleteLoad callback = delegate
			{
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0056: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
				//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
				playerLoader.get_transform().set_position(parameter.playerPos);
				playerLoader.get_transform().set_eulerAngles(new Vector3(0f, (viewMode != 0) ? parameter.avatarPlayerRot : parameter.playerRot, 0f));
				if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
				{
					UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
					float num3 = (userStatus.sex != 0) ? parameter.playerScaleFemale : parameter.playerScaleMale;
					playerLoader.get_transform().set_localScale(playerLoader.get_transform().get_localScale().Mul(new Vector3(num3, num3, num3)));
				}
				renderTexture.Enable();
			};
			int use_hair_overlay = num;
			obj.StartLoad(load_info, renderLayer, anim_id2, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick: true, use_hair_overlay);
		}
	}

	public void SetViewMode(VIEW_MODE view_mode)
	{
		if (viewMode != view_mode)
		{
			equipSetData = null;
			if (viewType == VIEW_TYPE.STATUS)
			{
				MoveCamera(viewType, viewType, viewMode, view_mode);
			}
			viewMode = view_mode;
		}
	}

	public void SetEquipSetData(StatusEquip.LocalEquipSetData equip_set_data)
	{
		if (equipSetData != equip_set_data)
		{
			equipInfo = null;
			equipSetData = equip_set_data;
			MoveCamera(viewType, viewType, viewMode, viewMode);
		}
	}

	public void SetEquipInfo(EquipItemInfo equip_info)
	{
		PlayerLoadInfo playerLoadInfo = playerLoader.loadInfo.Clone();
		if (equip_info != null)
		{
			playerLoadInfo.SetEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip_info.tableData);
		}
		else if (viewMode == VIEW_MODE.AVATAR)
		{
			int num = EQUIP_SLOT.AvatatToEquip(equipSetData.index);
			equip_info = equipSetData.equipSetInfo.item[num];
			if (equip_info == null)
			{
				playerLoadInfo.RemoveEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, num);
			}
			else
			{
				playerLoadInfo.SetEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip_info.tableData);
			}
		}
		else if (equipSetData != null)
		{
			playerLoadInfo.RemoveEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equipSetData.index);
		}
		if (equipInfo != equip_info)
		{
			equipInfo = equip_info;
			MoveCamera(viewType, viewType, viewMode, viewMode);
		}
	}

	public void UpdateCamera(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		if (!section_data.type.IsDialog())
		{
			VIEW_TYPE vIEW_TYPE = viewType;
			vIEW_TYPE = (((scene_name == "StatusScene" || scene_name == "UniqueStatusScene") && section_name != "StatusToSmith" && section_name != "ItemStorageSell" && !section_name.Contains("Exchange") && section_name != "UniqueStatusToSmith") ? VIEW_TYPE.STATUS : VIEW_TYPE.SMITH);
			if (viewType != vIEW_TYPE)
			{
				MoveCamera(viewType, vIEW_TYPE, viewMode, viewMode);
				viewType = vIEW_TYPE;
			}
		}
	}

	private void MoveCamera(VIEW_TYPE type_from, VIEW_TYPE type_to, VIEW_MODE mode_from, VIEW_MODE mode_to)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		Quaternion val = Quaternion.Euler(0f, parameter.playerRot, 0f);
		Vector3 val2;
		Quaternion end_value;
		float end_value2;
		if (type_to == VIEW_TYPE.STATUS)
		{
			Vector3 playerPos = parameter.playerPos;
			if (mode_to == VIEW_MODE.AVATAR)
			{
				val = Quaternion.Euler(0f, parameter.avatarPlayerRot, 0f);
			}
			if (equipSetData != null)
			{
				OutGameSettingsManager.StatusScene.EquipViewInfo equipViewInfo = null;
				if (equipInfo != null)
				{
					equipViewInfo = parameter.GetEquipViewInfo(equipInfo.tableData.type.ToString());
				}
				if (equipViewInfo == null)
				{
					equipViewInfo = parameter.GetEquipViewInfo(EQUIP_SLOT.ToType((viewMode != VIEW_MODE.AVATAR) ? equipSetData.index : EQUIP_SLOT.AvatatToEquip(equipSetData.index)).ToString());
				}
				Vector3 cameraTargetPos = equipViewInfo.cameraTargetPos;
				if (mode_to == VIEW_MODE.AVATAR)
				{
					cameraTargetPos.x = 0f;
				}
				playerPos = val * cameraTargetPos + playerPos;
				val2 = Quaternion.AngleAxis(0f - equipViewInfo.cameraXAngle, Vector3.get_right()) * Quaternion.AngleAxis(0f - equipViewInfo.cameraYAngle, Vector3.get_up()) * Vector3.get_forward();
				val2 = val * val2;
				val2 = playerPos + val2 * equipViewInfo.cameraDistance;
			}
			else
			{
				Vector3 val3 = val * Vector3.get_forward();
				val2 = playerPos + val3 * parameter.cameraTargetDistance + new Vector3(0f, parameter.cameraHeight, 0f);
				playerPos += new Vector3(0f, parameter.cameraTargetHeight, 0f);
			}
			end_value = Quaternion.LookRotation(playerPos - val2);
			end_value2 = parameter.cameraFieldOfView;
		}
		else
		{
			OutGameSettingsManager.SmithScene smithScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene;
			val2 = smithScene.createCameraPos;
			end_value = Quaternion.Euler(smithScene.createCameraRot);
			end_value2 = smithScene.createCameraFieldOfView;
			val = Quaternion.Euler(0f, parameter.playerRot, 0f);
		}
		float num = parameter.cameraMoveTime;
		if (MonoBehaviourSingleton<TransitionManager>.I.isTransing && !MonoBehaviourSingleton<TransitionManager>.I.isChanging)
		{
			num = 0f;
		}
		cameraTurningMode = (num > 0f && type_from == type_to && type_from == VIEW_TYPE.STATUS && mode_from != mode_to);
		cameraPosAnim.Set(num, targetCameraTransform.get_position(), val2);
		cameraPosAnim.Play();
		cameraRotAnim.Set(num, targetCameraTransform.get_rotation(), end_value);
		cameraRotAnim.Play();
		cameraFovAnim.Set(num, targetCamera.get_fieldOfView(), end_value2, null, 0f);
		cameraFovAnim.Play();
		if (playerLoader != null && !playerLoader.isLoading)
		{
			playerRotAnim.Set(num * 1.25f, playerLoader.get_transform().get_rotation(), val);
			playerRotAnim.Play();
		}
	}

	private void LateUpdate()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (playerRotAnim.IsPlaying() && playerLoader != null && !playerLoader.isLoading)
		{
			playerLoader.get_transform().set_rotation(playerRotAnim.Update());
		}
		if (cameraPosAnim.IsPlaying())
		{
			targetCamera.set_fieldOfView(cameraFovAnim.Update());
			targetCameraTransform.set_position(cameraPosAnim.Update());
			targetCameraTransform.set_rotation(cameraRotAnim.Update());
			if (cameraTurningMode)
			{
				Vector3 val = -targetCameraTransform.get_forward();
				val.x *= parameter.cameraTargetDistance;
				val.y = parameter.cameraHeight;
				val.z *= parameter.cameraTargetDistance;
				targetCameraTransform.set_position(val + parameter.playerPos);
			}
		}
	}

	public void SetSmithCharacterActivate(bool active)
	{
		if (m_stSmithCharacter != null)
		{
			m_stSmithCharacter.SetActive(active);
		}
	}

	public void SetUniqueSmithCharacterActivate(bool active)
	{
		if (m_stUniqueSmithCharacter != null)
		{
			m_stUniqueSmithCharacter.SetActive(active);
		}
	}

	public void SetEnableSmithCharacterActivate(bool active)
	{
		if (StatusManager.IsUnique())
		{
			SetUniqueSmithCharacterActivate(active);
		}
		else
		{
			SetSmithCharacterActivate(active);
		}
	}

	public void SetDisableSmithCharacterActivate(bool active)
	{
		if (!StatusManager.IsUnique())
		{
			SetUniqueSmithCharacterActivate(active);
		}
		else
		{
			SetSmithCharacterActivate(active);
		}
	}
}

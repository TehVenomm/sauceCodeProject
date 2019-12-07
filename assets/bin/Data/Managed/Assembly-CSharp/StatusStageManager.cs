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

	public bool isBusy
	{
		get
		{
			if (!cameraPosAnim.IsPlaying())
			{
				return cameraRotAnim.IsPlaying();
			}
			return true;
		}
	}

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
		uiTexture.SetAnchor(MonoBehaviourSingleton<UIManager>.I.system.gameObject, 0, 0, 0, 0);
		uiTexture.UpdateAnchors();
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			UIWidget component = uiTexture.gameObject.GetComponent<UIWidget>();
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
			UnityEngine.Object.Destroy(uiTexture.gameObject);
		}
		if (playerShadow != null)
		{
			UnityEngine.Object.Destroy(playerShadow.gameObject);
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
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			if (!(currentSectionName != "StatusTop") || !(currentSectionName != "StatusAvatar") || !(currentSectionName != "StatusAccessory") || !(currentSectionName != "UniqueStatusTop"))
			{
				playerLoader.transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
			}
		}
	}

	public void SetUITextureActive(bool active)
	{
		uiTexture.gameObject.SetActive(active);
	}

	public void ClearPlayerLoaded(PlayerLoadInfo load_info)
	{
		if ((!(playerLoader != null) || !playerLoader.loadInfo.Equals(load_info)) && renderTexture != null)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture);
		}
	}

	public void LoadPlayer(PlayerLoadInfo load_info, int anim_id = 0)
	{
		if (playerShadow == null)
		{
			playerShadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, fixedY0: false);
			playerShadow.transform.position = parameter.playerPos + new Vector3(0f, 0.005f, 0f);
		}
		ShaderGlobal.lightProbe = false;
		if (!(playerLoader != null) || !playerLoader.loadInfo.Equals(load_info))
		{
			if (renderTexture != null)
			{
				UnityEngine.Object.DestroyImmediate(renderTexture);
			}
			renderTexture = UIRenderTexture.Get(uiTexture, -1f, link_main_camera: true);
			renderTexture.Disable();
			renderTexture.nearClipPlane = parameter.renderTextureNearClip;
			int use_hair_overlay = -1;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
			{
				use_hair_overlay = (MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isChangeHairShader ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hairColorId : (-1));
			}
			int num = anim_id;
			if (num == 0)
			{
				num = PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
			}
			playerLoader = renderTexture.modelTransform.gameObject.AddComponent<PlayerLoader>();
			playerLoader.StartLoad(load_info, renderTexture.renderLayer, num, need_anim_event: false, need_foot_stamp: false, need_shadow: false, enable_light_probes: false, need_action_voice: false, need_high_reso_tex: true, need_res_ref_count: true, need_dev_frame_instantiate: true, SHADER_TYPE.NORMAL, delegate
			{
				playerLoader.transform.position = parameter.playerPos;
				playerLoader.transform.eulerAngles = new Vector3(0f, (viewMode == VIEW_MODE.EQUIP) ? parameter.playerRot : parameter.avatarPlayerRot, 0f);
				if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
				{
					float num2 = (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex == 0) ? parameter.playerScaleMale : parameter.playerScaleFemale;
					playerLoader.transform.localScale = playerLoader.transform.localScale.Mul(new Vector3(num2, num2, num2));
				}
				renderTexture.Enable();
			}, enable_eye_blick: true, use_hair_overlay);
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
		Quaternion quaternion = Quaternion.Euler(0f, parameter.playerRot, 0f);
		Vector3 point;
		Quaternion end_value;
		float end_value2;
		if (type_to == VIEW_TYPE.STATUS)
		{
			Vector3 vector = parameter.playerPos;
			if (mode_to == VIEW_MODE.AVATAR)
			{
				quaternion = Quaternion.Euler(0f, parameter.avatarPlayerRot, 0f);
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
					equipViewInfo = parameter.GetEquipViewInfo(EQUIP_SLOT.ToType((viewMode == VIEW_MODE.AVATAR) ? EQUIP_SLOT.AvatatToEquip(equipSetData.index) : equipSetData.index).ToString());
				}
				Vector3 cameraTargetPos = equipViewInfo.cameraTargetPos;
				if (mode_to == VIEW_MODE.AVATAR)
				{
					cameraTargetPos.x = 0f;
				}
				vector = quaternion * cameraTargetPos + vector;
				point = Quaternion.AngleAxis(0f - equipViewInfo.cameraXAngle, Vector3.right) * Quaternion.AngleAxis(0f - equipViewInfo.cameraYAngle, Vector3.up) * Vector3.forward;
				point = quaternion * point;
				point = vector + point * equipViewInfo.cameraDistance;
			}
			else
			{
				Vector3 a = quaternion * Vector3.forward;
				point = vector + a * parameter.cameraTargetDistance + new Vector3(0f, parameter.cameraHeight, 0f);
				vector += new Vector3(0f, parameter.cameraTargetHeight, 0f);
			}
			end_value = Quaternion.LookRotation(vector - point);
			end_value2 = parameter.cameraFieldOfView;
		}
		else
		{
			OutGameSettingsManager.SmithScene smithScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene;
			point = smithScene.createCameraPos;
			end_value = Quaternion.Euler(smithScene.createCameraRot);
			end_value2 = smithScene.createCameraFieldOfView;
			quaternion = Quaternion.Euler(0f, parameter.playerRot, 0f);
		}
		float num = parameter.cameraMoveTime;
		if (MonoBehaviourSingleton<TransitionManager>.I.isTransing && !MonoBehaviourSingleton<TransitionManager>.I.isChanging)
		{
			num = 0f;
		}
		cameraTurningMode = (num > 0f && type_from == type_to && type_from == VIEW_TYPE.STATUS && mode_from != mode_to);
		cameraPosAnim.Set(num, targetCameraTransform.position, point);
		cameraPosAnim.Play();
		cameraRotAnim.Set(num, targetCameraTransform.rotation, end_value);
		cameraRotAnim.Play();
		cameraFovAnim.Set(num, targetCamera.fieldOfView, end_value2, null, 0f);
		cameraFovAnim.Play();
		if (playerLoader != null && !playerLoader.isLoading)
		{
			playerRotAnim.Set(num * 1.25f, playerLoader.transform.rotation, quaternion);
			playerRotAnim.Play();
		}
	}

	private void LateUpdate()
	{
		if (playerRotAnim.IsPlaying() && playerLoader != null && !playerLoader.isLoading)
		{
			playerLoader.transform.rotation = playerRotAnim.Update();
		}
		if (cameraPosAnim.IsPlaying())
		{
			targetCamera.fieldOfView = cameraFovAnim.Update();
			targetCameraTransform.position = cameraPosAnim.Update();
			targetCameraTransform.rotation = cameraRotAnim.Update();
			if (cameraTurningMode)
			{
				Vector3 a = -targetCameraTransform.forward;
				a.x *= parameter.cameraTargetDistance;
				a.y = parameter.cameraHeight;
				a.z *= parameter.cameraTargetDistance;
				targetCameraTransform.position = a + parameter.playerPos;
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

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

	public bool isBusy => cameraPosAnim.IsPlaying() || cameraRotAnim.IsPlaying();

	protected override void Awake()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		base.Awake();
		targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		parameter = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
		uiTexture = (Utility.CreateGameObjectAndComponent("UITexture", MonoBehaviourSingleton<UIManager>.I.system._transform, 5) as UITexture);
		uiTexture.shader = ResourceUtility.FindShader("Unlit/ui_render_tex");
		uiTexture.SetAnchor(MonoBehaviourSingleton<UIManager>.I.system.get_gameObject(), 0, 0, 0, 0);
		uiTexture.UpdateAnchors();
		m_stSmithCharacter = (StatusSmithCharacter)Utility.CreateGameObjectAndComponent("StatusSmithCharacter", base._transform, -1);
	}

	protected override void _OnDestroy()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			if (!(currentSectionName != "StatusTop") || !(currentSectionName != "StatusAvatar"))
			{
				playerLoader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
			}
		}
	}

	public void SetUITextureActive(bool active)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		uiTexture.get_gameObject().SetActive(active);
	}

	public void ClearPlayerLoaded(PlayerLoadInfo load_info)
	{
		if ((!(playerLoader != null) || !playerLoader.loadInfo.Equals(load_info)) && renderTexture != null)
		{
			Object.DestroyImmediate(renderTexture);
		}
	}

	public void LoadPlayer(PlayerLoadInfo load_info)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		if (playerShadow == null)
		{
			playerShadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, false, -1, false);
			playerShadow.get_transform().set_position(parameter.playerPos + new Vector3(0f, 0.005f, 0f));
		}
		ShaderGlobal.lightProbe = false;
		if (!(playerLoader != null) || !playerLoader.loadInfo.Equals(load_info))
		{
			if (renderTexture != null)
			{
				Object.DestroyImmediate(renderTexture);
			}
			renderTexture = UIRenderTexture.Get(uiTexture, -1f, true, -1);
			renderTexture.Disable();
			renderTexture.nearClipPlane = parameter.renderTextureNearClip;
			int num = -1;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
			{
				num = ((!MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isChangeHairShader) ? (-1) : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hairColorId);
			}
			playerLoader = renderTexture.modelTransform.get_gameObject().AddComponent<PlayerLoader>();
			PlayerLoader obj = playerLoader;
			int use_hair_overlay = num;
			obj.StartLoad(load_info, renderTexture.renderLayer, PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), false, false, false, false, false, true, true, true, SHADER_TYPE.NORMAL, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0021: Unknown result type (might be due to invalid IL or missing references)
				//IL_0056: Unknown result type (might be due to invalid IL or missing references)
				//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
				//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
				playerLoader.get_transform().set_position(parameter.playerPos);
				playerLoader.get_transform().set_eulerAngles(new Vector3(0f, (viewMode != 0) ? parameter.avatarPlayerRot : parameter.playerRot, 0f));
				if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
				{
					UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
					float num2 = (userStatus.sex != 0) ? parameter.playerScaleFemale : parameter.playerScaleMale;
					playerLoader.get_transform().set_localScale(playerLoader.get_transform().get_localScale().Mul(new Vector3(num2, num2, num2)));
				}
				renderTexture.Enable(0.25f);
			}, true, use_hair_overlay);
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
			playerLoadInfo.SetEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip_info.tableData, true, true, true);
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
				playerLoadInfo.SetEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip_info.tableData, true, true, true);
			}
		}
		else if (equipSetData != null)
		{
			playerLoadInfo.RemoveEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equipSetData.index);
		}
		LoadPlayer(playerLoadInfo);
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
			vIEW_TYPE = ((scene_name == "StatusScene" && section_name != "StatusToSmith" && section_name != "ItemStorageSell" && !section_name.Contains("Exchange")) ? VIEW_TYPE.STATUS : VIEW_TYPE.SMITH);
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
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
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
		cameraPosAnim.Set(num, targetCameraTransform.get_position(), val2, null, default(Vector3), null);
		cameraPosAnim.Play();
		cameraRotAnim.Set(num, targetCameraTransform.get_rotation(), end_value, null, default(Quaternion), null);
		cameraRotAnim.Play();
		cameraFovAnim.Set(num, targetCamera.get_fieldOfView(), end_value2, null, 0f, null);
		cameraFovAnim.Play();
		if (playerLoader != null && !playerLoader.isLoading)
		{
			playerRotAnim.Set(num * 1.25f, playerLoader.get_transform().get_rotation(), val, null, default(Quaternion), null);
			playerRotAnim.Play();
		}
	}

	private void LateUpdate()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
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

	public void SetSmithCharacterActivateFlag(bool isActivate)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (m_stSmithCharacter != null)
		{
			m_stSmithCharacter.get_gameObject().SetActive(isActivate);
		}
	}
}

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
		m_stSmithCharacter = (StatusSmithCharacter)Utility.CreateGameObjectAndComponent("StatusSmithCharacter", base._transform, -1);
	}

	protected override void _OnDestroy()
	{
		if ((UnityEngine.Object)uiTexture != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(uiTexture.gameObject);
		}
		if ((UnityEngine.Object)playerShadow != (UnityEngine.Object)null)
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
		if (!((UnityEngine.Object)playerLoader == (UnityEngine.Object)null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			if (!(currentSectionName != "StatusTop") || !(currentSectionName != "StatusAvatar") || !(currentSectionName != "StatusAccessory"))
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
		if ((!((UnityEngine.Object)playerLoader != (UnityEngine.Object)null) || !playerLoader.loadInfo.Equals(load_info)) && (UnityEngine.Object)renderTexture != (UnityEngine.Object)null)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture);
		}
	}

	public void LoadPlayer(PlayerLoadInfo load_info)
	{
		if ((UnityEngine.Object)playerShadow == (UnityEngine.Object)null)
		{
			playerShadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, false, -1, false);
			playerShadow.transform.position = parameter.playerPos + new Vector3(0f, 0.005f, 0f);
		}
		ShaderGlobal.lightProbe = false;
		if (!((UnityEngine.Object)playerLoader != (UnityEngine.Object)null) || !playerLoader.loadInfo.Equals(load_info))
		{
			if ((UnityEngine.Object)renderTexture != (UnityEngine.Object)null)
			{
				UnityEngine.Object.DestroyImmediate(renderTexture);
			}
			renderTexture = UIRenderTexture.Get(uiTexture, -1f, true, -1);
			renderTexture.Disable();
			renderTexture.nearClipPlane = parameter.renderTextureNearClip;
			int num = -1;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
			{
				num = ((!MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isChangeHairShader) ? (-1) : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hairColorId);
			}
			playerLoader = renderTexture.modelTransform.gameObject.AddComponent<PlayerLoader>();
			PlayerLoader obj = playerLoader;
			int use_hair_overlay = num;
			obj.StartLoad(load_info, renderTexture.renderLayer, PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), false, false, false, false, false, true, true, true, SHADER_TYPE.NORMAL, delegate
			{
				playerLoader.transform.position = parameter.playerPos;
				playerLoader.transform.eulerAngles = new Vector3(0f, (viewMode != 0) ? parameter.avatarPlayerRot : parameter.playerRot, 0f);
				if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
				{
					UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
					float num2 = (userStatus.sex != 0) ? parameter.playerScaleFemale : parameter.playerScaleMale;
					playerLoader.transform.localScale = playerLoader.transform.localScale.Mul(new Vector3(num2, num2, num2));
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
		Quaternion quaternion = Quaternion.Euler(0f, parameter.playerRot, 0f);
		Vector3 point;
		Quaternion end_value;
		float end_value2;
		if (type_to == VIEW_TYPE.STATUS)
		{
			Vector3 playerPos = parameter.playerPos;
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
					equipViewInfo = parameter.GetEquipViewInfo(EQUIP_SLOT.ToType((viewMode != VIEW_MODE.AVATAR) ? equipSetData.index : EQUIP_SLOT.AvatatToEquip(equipSetData.index)).ToString());
				}
				Vector3 cameraTargetPos = equipViewInfo.cameraTargetPos;
				if (mode_to == VIEW_MODE.AVATAR)
				{
					cameraTargetPos.x = 0f;
				}
				playerPos = quaternion * cameraTargetPos + playerPos;
				point = Quaternion.AngleAxis(0f - equipViewInfo.cameraXAngle, Vector3.right) * Quaternion.AngleAxis(0f - equipViewInfo.cameraYAngle, Vector3.up) * Vector3.forward;
				point = quaternion * point;
				point = playerPos + point * equipViewInfo.cameraDistance;
			}
			else
			{
				Vector3 a = quaternion * Vector3.forward;
				point = playerPos + a * parameter.cameraTargetDistance + new Vector3(0f, parameter.cameraHeight, 0f);
				playerPos += new Vector3(0f, parameter.cameraTargetHeight, 0f);
			}
			end_value = Quaternion.LookRotation(playerPos - point);
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
		cameraPosAnim.Set(num, targetCameraTransform.position, point, null, default(Vector3), null);
		cameraPosAnim.Play();
		cameraRotAnim.Set(num, targetCameraTransform.rotation, end_value, null, default(Quaternion), null);
		cameraRotAnim.Play();
		cameraFovAnim.Set(num, targetCamera.fieldOfView, end_value2, null, 0f, null);
		cameraFovAnim.Play();
		if ((UnityEngine.Object)playerLoader != (UnityEngine.Object)null && !playerLoader.isLoading)
		{
			playerRotAnim.Set(num * 1.25f, playerLoader.transform.rotation, quaternion, null, default(Quaternion), null);
			playerRotAnim.Play();
		}
	}

	private void LateUpdate()
	{
		if (playerRotAnim.IsPlaying() && (UnityEngine.Object)playerLoader != (UnityEngine.Object)null && !playerLoader.isLoading)
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

	public void SetSmithCharacterActivateFlag(bool isActivate)
	{
		if ((UnityEngine.Object)m_stSmithCharacter != (UnityEngine.Object)null)
		{
			m_stSmithCharacter.gameObject.SetActive(isActivate);
		}
	}
}

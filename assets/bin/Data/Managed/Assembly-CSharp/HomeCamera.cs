using System;
using System.Collections;
using UnityEngine;

public class HomeCamera : MonoBehaviour
{
	public enum VIEW_MODE
	{
		NORMAL,
		SITTING
	}

	private Vector3 targetPos;

	private Vector3 cameraPos;

	private float lerpRate;

	private const float BaseCameraOffset = 2f;

	private HomeSelfCharacter chara;

	private OutGameSettingsManager.HomeScene homeSceneParam;

	private float defaultFov;

	private Vector3 beforeNormalCameraPos;

	private Quaternion beforeNormalCameraRot;

	private float beforeNormalCameraFov;

	public Camera targetCamera
	{
		get;
		private set;
	}

	public Transform targetCameraTransform
	{
		get;
		private set;
	}

	public bool isInitialized
	{
		get;
		private set;
	}

	public bool isChanging
	{
		get;
		private set;
	}

	public VIEW_MODE viewMode
	{
		get;
		private set;
	}

	public HomeCamera()
		: this()
	{
	}

	public void ChangeView(VIEW_MODE view)
	{
		if (!IsValidChangeView(view))
		{
			return;
		}
		isChanging = true;
		VIEW_MODE viewMode = this.viewMode;
		switch (view)
		{
		case VIEW_MODE.SITTING:
			this.viewMode = VIEW_MODE.SITTING;
			this.StartCoroutine(DoSit(delegate
			{
				isChanging = false;
			}));
			break;
		case VIEW_MODE.NORMAL:
			this.viewMode = VIEW_MODE.NORMAL;
			if (viewMode == VIEW_MODE.SITTING)
			{
				this.StartCoroutine(DoStand(delegate
				{
					isChanging = false;
				}));
			}
			else
			{
				isChanging = false;
			}
			break;
		default:
			isChanging = false;
			break;
		}
	}

	public void LateUpdate()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (chara == null)
		{
			chara = GetSelfCharacter();
		}
		if (IsValidCameraUpdate() && viewMode == VIEW_MODE.NORMAL)
		{
			targetPos = Vector3.get_zero();
			cameraPos = Vector3.get_zero();
			GetTargetAndNormalCameraPosition(ref targetPos, ref cameraPos);
			cameraPos = CheckCollision(targetPos, cameraPos);
			UpdateCameraTransform(targetPos, cameraPos);
		}
	}

	private void Start()
	{
		viewMode = VIEW_MODE.NORMAL;
		targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		targetCameraTransform = targetCamera.get_transform();
		defaultFov = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
		IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		homeSceneParam = currentIHomeManager.GetSceneSetting();
		isInitialized = true;
	}

	private bool IsValidChangeView(VIEW_MODE view)
	{
		if (viewMode == view)
		{
			return false;
		}
		if (isChanging)
		{
			return false;
		}
		if (chara == null)
		{
			return false;
		}
		return true;
	}

	private bool IsValidCameraUpdate()
	{
		if (!HomeSelfCharacter.CTRL)
		{
			return false;
		}
		if (chara == null)
		{
			return false;
		}
		if (chara.InitedAnimation && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "GuildTop")
		{
			return false;
		}
		if (isChanging)
		{
			return false;
		}
		return true;
	}

	private void GetTargetAndNormalCameraPosition(ref Vector3 targetPos, ref Vector3 cameraPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Vector3 initPos = Vector3.get_zero();
		float cameraDistance = 0f;
		float cameraHeight = 0f;
		float targetHeight = 0f;
		GetNeededNormalCameraParam(ref initPos, ref cameraDistance, ref cameraHeight, ref targetHeight);
		targetPos = GetTargetPositionForNormalCamera();
		cameraPos = GetNormalCameraPosition(initPos, cameraDistance, cameraHeight, targetPos);
		targetPos.y += targetHeight;
		cameraPos.y += cameraHeight;
	}

	private Vector3 CheckCollision(Vector3 charaPos, Vector3 requestPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = requestPos - charaPos;
		float cameraDistance = GetCameraDistance();
		float cameraDistanceMin = GetCameraDistanceMin();
		float lerpTime = GetLerpTime();
		RaycastHit val2 = default(RaycastHit);
		Ray val3 = default(Ray);
		val3._002Ector(charaPos, val.get_normalized());
		if (Physics.Raycast(val3, ref val2, cameraDistance, 512))
		{
			float distance = val2.get_distance();
			distance = Mathf.Max(distance, cameraDistanceMin);
			distance -= cameraDistanceMin;
			float num = cameraDistance - cameraDistanceMin;
			float num2 = 0f;
			if (num > 0f)
			{
				num2 = lerpTime * (distance / num);
			}
			float num3 = num2 / lerpTime;
			requestPos = charaPos + val3.get_direction() * Mathf.Lerp(cameraDistanceMin, cameraDistance, num3);
		}
		return requestPos;
	}

	private void GetNeededNormalCameraParam(ref Vector3 initPos, ref float cameraDistance, ref float cameraHeight, ref float targetHeight)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		initPos = homeSceneParam.selfInitPos;
		cameraHeight = homeSceneParam.GetSelfCameraHeight();
		targetHeight = homeSceneParam.selfCameraTagetHeight;
		cameraDistance = GetCameraDistance();
	}

	private float GetCameraDistance()
	{
		return homeSceneParam.GetSelfCameraDistance();
	}

	private float GetCameraDistanceMin()
	{
		return homeSceneParam.selfCameraDistanceMin;
	}

	private float GetLerpTime()
	{
		return 1.5f;
	}

	private void UpdateCameraTransform(Vector3 targetPos, Vector3 cameraPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		targetCameraTransform.set_position(cameraPos);
		targetCameraTransform.LookAt(targetPos);
		if (targetCamera.get_fieldOfView() != defaultFov)
		{
			targetCamera.set_fieldOfView(defaultFov);
		}
	}

	private Vector3 GetNormalCameraPosition(Vector3 initPos, float cameraDistance, float cameraHeight, Vector3 targetPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Vector3 baseCameraPosition = GetBaseCameraPosition(initPos);
		baseCameraPosition.z -= 2f;
		Vector3 val = baseCameraPosition - targetPos;
		val.y = 0f;
		val.Normalize();
		return val * cameraDistance + targetPos;
	}

	private Vector3 GetTargetPositionForNormalCamera()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return chara._transform.get_position();
	}

	private HomeSelfCharacter GetSelfCharacter()
	{
		return GameSceneGlobalSettings.GetCurrentIHomeManager()?.IHomePeople.selfChara;
	}

	private Vector3 GetBaseCameraPosition(Vector3 target_pos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = default(Vector3);
		result = Quaternion.AngleAxis(homeSceneParam.selfCameraAngleY, Vector3.get_up()) * Vector3.get_forward() * homeSceneParam.selfCameraDistanceMax + target_pos;
		result.y += homeSceneParam.GetSelfCameraHeight();
		return result;
	}

	private void OnPinch(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1, float pinch_length)
	{
		if (touch_info0 != null && touch_info1 != null && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && GetSelfCharacter().IsEnableControl() && viewMode == VIEW_MODE.NORMAL)
		{
			homeSceneParam.selfCameraZoomRate = Mathf.Clamp01(homeSceneParam.selfCameraZoomRate + pinch_length * homeSceneParam.selfCameraZoomCoef);
		}
	}

	private void OnEnable()
	{
		if (HomeSelfCharacter.CTRL)
		{
			InputManager.OnPinch = (InputManager.OnPinchDelegate)Delegate.Combine(InputManager.OnPinch, new InputManager.OnPinchDelegate(OnPinch));
		}
	}

	private void OnDisable()
	{
		if (HomeSelfCharacter.CTRL)
		{
			InputManager.OnPinch = (InputManager.OnPinchDelegate)Delegate.Remove(InputManager.OnPinch, new InputManager.OnPinchDelegate(OnPinch));
		}
	}

	private IEnumerator DoSit(Action callback = null)
	{
		beforeNormalCameraPos = targetCameraTransform.get_position();
		beforeNormalCameraRot = targetCameraTransform.get_rotation();
		beforeNormalCameraFov = targetCamera.get_fieldOfView();
		TablePoint table = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearTablePoint() : MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearTablePoint();
		yield return this.StartCoroutine(LerpCamera(targetPos: table.cameraPosition, targetRot: Quaternion.Euler(table.cameraRotation), targetFov: table.cameraFov, prevPos: beforeNormalCameraPos, prevRot: beforeNormalCameraRot, prevFov: beforeNormalCameraFov));
		callback?.Invoke();
	}

	private IEnumerator DoStand(Action callback = null)
	{
		Vector3 prevPos = targetCameraTransform.get_position();
		Quaternion prevRot = targetCameraTransform.get_rotation();
		float prevFov = targetCamera.get_fieldOfView();
		Quaternion targetRot = beforeNormalCameraRot;
		Vector3 targetPos = beforeNormalCameraPos;
		float targetFov = beforeNormalCameraFov;
		yield return this.StartCoroutine(LerpCamera(prevPos, targetPos, prevRot, targetRot, prevFov, targetFov));
		callback?.Invoke();
	}

	private IEnumerator LerpCamera(Vector3 prevPos, Vector3 targetPos, Quaternion prevRot, Quaternion targetRot, float prevFov, float targetFov)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		bool wait3 = true;
		float time = 0f;
		OutGameSettingsManager.LoungeScene loungeSceneParam = homeSceneParam as OutGameSettingsManager.LoungeScene;
		while (wait3)
		{
			time = Mathf.Min(time + Time.get_deltaTime(), 1f);
			if (viewMode == VIEW_MODE.NORMAL)
			{
				Vector3 val = default(Vector3);
				Vector3 val2 = default(Vector3);
				GetTargetAndNormalCameraPosition(ref val, ref val2);
				targetPos = val2;
				Quaternion val3 = Quaternion.LookRotation(val - val2);
				targetRot = val3;
			}
			if (targetCameraTransform.get_position() != targetPos)
			{
				targetCameraTransform.set_position(Vector3.Lerp(prevPos, targetPos, EaseOutCube(time) * loungeSceneParam.sittingCameraEaseCoef));
				wait3 = true;
			}
			else
			{
				wait3 = false;
			}
			if (Quaternion.Angle(targetCameraTransform.get_rotation(), targetRot) > 0.1f)
			{
				targetCameraTransform.set_rotation(Quaternion.Slerp(prevRot, targetRot, EaseOutCube(time) * loungeSceneParam.sittingCameraEaseCoef));
				wait3 = true;
			}
			else
			{
				wait3 = (wait3 ? true : false);
			}
			if (targetCamera.get_fieldOfView() != targetFov)
			{
				targetCamera.set_fieldOfView(Mathf.Lerp(prevFov, targetFov, time));
				wait3 = true;
			}
			else
			{
				wait3 = (wait3 ? true : false);
			}
			yield return null;
		}
	}

	private static float EaseOutCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}
}

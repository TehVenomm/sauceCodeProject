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
			StartCoroutine(DoSit(delegate
			{
				isChanging = false;
			}));
			break;
		case VIEW_MODE.NORMAL:
			this.viewMode = VIEW_MODE.NORMAL;
			if (viewMode == VIEW_MODE.SITTING)
			{
				StartCoroutine(DoStand(delegate
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
		if (chara == null)
		{
			chara = GetSelfCharacter();
		}
		if (IsValidCameraUpdate() && viewMode == VIEW_MODE.NORMAL)
		{
			targetPos = Vector3.zero;
			cameraPos = Vector3.zero;
			GetTargetAndNormalCameraPosition(ref targetPos, ref cameraPos);
			cameraPos = CheckCollision(targetPos, cameraPos);
			UpdateCameraTransform(targetPos, cameraPos);
		}
	}

	private void Start()
	{
		viewMode = VIEW_MODE.NORMAL;
		targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		targetCameraTransform = targetCamera.transform;
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
		Vector3 initPos = Vector3.zero;
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
		Vector3 vector = requestPos - charaPos;
		float cameraDistance = GetCameraDistance();
		float cameraDistanceMin = GetCameraDistanceMin();
		float lerpTime = GetLerpTime();
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = new Ray(charaPos, vector.normalized);
		if (Physics.Raycast(ray, out hitInfo, cameraDistance, 512))
		{
			float distance = hitInfo.distance;
			distance = Mathf.Max(distance, cameraDistanceMin);
			distance -= cameraDistanceMin;
			float num = cameraDistance - cameraDistanceMin;
			float num2 = 0f;
			if (num > 0f)
			{
				num2 = lerpTime * (distance / num);
			}
			float t = num2 / lerpTime;
			requestPos = charaPos + ray.direction * Mathf.Lerp(cameraDistanceMin, cameraDistance, t);
		}
		return requestPos;
	}

	private void GetNeededNormalCameraParam(ref Vector3 initPos, ref float cameraDistance, ref float cameraHeight, ref float targetHeight)
	{
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
		targetCameraTransform.position = cameraPos;
		targetCameraTransform.LookAt(targetPos);
		if (targetCamera.fieldOfView != defaultFov)
		{
			targetCamera.fieldOfView = defaultFov;
		}
	}

	private Vector3 GetNormalCameraPosition(Vector3 initPos, float cameraDistance, float cameraHeight, Vector3 targetPos)
	{
		Vector3 baseCameraPosition = GetBaseCameraPosition(initPos);
		baseCameraPosition.z -= 2f;
		Vector3 a = baseCameraPosition - targetPos;
		a.y = 0f;
		a.Normalize();
		return a * cameraDistance + targetPos;
	}

	private Vector3 GetTargetPositionForNormalCamera()
	{
		return chara._transform.position;
	}

	private HomeSelfCharacter GetSelfCharacter()
	{
		return GameSceneGlobalSettings.GetCurrentIHomeManager()?.IHomePeople.selfChara;
	}

	private Vector3 GetBaseCameraPosition(Vector3 target_pos)
	{
		Vector3 result = default(Vector3);
		result = Quaternion.AngleAxis(homeSceneParam.selfCameraAngleY, Vector3.up) * Vector3.forward * homeSceneParam.selfCameraDistanceMax + target_pos;
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
		beforeNormalCameraPos = targetCameraTransform.position;
		beforeNormalCameraRot = targetCameraTransform.rotation;
		beforeNormalCameraFov = targetCamera.fieldOfView;
		TablePoint tablePoint = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearTablePoint() : MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearTablePoint();
		Vector3 cameraPosition = tablePoint.cameraPosition;
		Quaternion targetRot = Quaternion.Euler(tablePoint.cameraRotation);
		float cameraFov = tablePoint.cameraFov;
		yield return StartCoroutine(LerpCamera(beforeNormalCameraPos, cameraPosition, beforeNormalCameraRot, targetRot, beforeNormalCameraFov, cameraFov));
		callback?.Invoke();
	}

	private IEnumerator DoStand(Action callback = null)
	{
		Vector3 position = targetCameraTransform.position;
		Quaternion rotation = targetCameraTransform.rotation;
		float fieldOfView = targetCamera.fieldOfView;
		Quaternion targetRot = beforeNormalCameraRot;
		Vector3 vector = beforeNormalCameraPos;
		float targetFov = beforeNormalCameraFov;
		yield return StartCoroutine(LerpCamera(position, vector, rotation, targetRot, fieldOfView, targetFov));
		callback?.Invoke();
	}

	private IEnumerator LerpCamera(Vector3 prevPos, Vector3 targetPos, Quaternion prevRot, Quaternion targetRot, float prevFov, float targetFov)
	{
		bool wait3 = true;
		float time = 0f;
		OutGameSettingsManager.LoungeScene loungeSceneParam = homeSceneParam as OutGameSettingsManager.LoungeScene;
		while (wait3)
		{
			time = Mathf.Min(time + Time.deltaTime, 1f);
			if (viewMode == VIEW_MODE.NORMAL)
			{
				Vector3 a = default(Vector3);
				Vector3 vector = default(Vector3);
				GetTargetAndNormalCameraPosition(ref a, ref vector);
				targetPos = vector;
				Quaternion quaternion = Quaternion.LookRotation(a - vector);
				targetRot = quaternion;
			}
			if (targetCameraTransform.position != targetPos)
			{
				targetCameraTransform.position = Vector3.Lerp(prevPos, targetPos, EaseOutCube(time) * loungeSceneParam.sittingCameraEaseCoef);
				wait3 = true;
			}
			else
			{
				wait3 = false;
			}
			if (Quaternion.Angle(targetCameraTransform.rotation, targetRot) > 0.1f)
			{
				targetCameraTransform.rotation = Quaternion.Slerp(prevRot, targetRot, EaseOutCube(time) * loungeSceneParam.sittingCameraEaseCoef);
				wait3 = true;
			}
			else
			{
				wait3 = (wait3 ? true : false);
			}
			if (targetCamera.fieldOfView != targetFov)
			{
				targetCamera.fieldOfView = Mathf.Lerp(prevFov, targetFov, time);
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

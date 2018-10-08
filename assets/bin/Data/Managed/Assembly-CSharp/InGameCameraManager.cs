using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameCameraManager : MonoBehaviourSingleton<InGameCameraManager>
{
	public enum CAMERA_MODE
	{
		DEFAULT,
		GRABBED,
		ARROW_AIM_BOSS,
		CANNON_AIM,
		CANNON_BEAM_CHARGE,
		CANNON_BEAM,
		STOP,
		CUT,
		MAX
	}

	[Serializable]
	public class Settings
	{
		[Serializable]
		public class ArrowAimSettings
		{
			[Tooltip("狙い中 ピッチ")]
			public float targetingPitch = 10f;

			[Tooltip("狙い中 距離")]
			public float targetingDistance = 4f;

			[Tooltip("狙い中(右側) オフセット")]
			public Vector3 targetingOffset = Vector3.zero;

			[Tooltip("狙い中(左側) オフセット")]
			public Vector3 targetingLeftOffset = Vector3.zero;

			[Tooltip("狙い中しゃがみ(右側) オフセット")]
			public Vector3 targetingAvoidShotRightOffset = Vector3.zero;

			[Tooltip("狙い中しゃがみ(左側) オフセット")]
			public Vector3 targetingAvoidShotLeftOffset = Vector3.zero;

			[Tooltip("狙い中 画角（0で変化無し")]
			public float fieldOfView;

			[Tooltip("狙い時の補正レ\u30fcト")]
			public float smoothTargetingRate = 0.1f;
		}

		[Serializable]
		public class CameraTargetOffsetSettings
		{
			[Tooltip("カメラオフセット補正位置")]
			public Vector3 targetOffsetPos = Vector3.zero;

			[Tooltip("カメラオフセット補正回転")]
			public Vector3 targetOffsetRot = Vector3.zero;
		}

		[Serializable]
		public class CannonAimSettings
		{
			[Tooltip("魔弾砲狙い時のカメラオフセット")]
			public Vector3 aimCameraOffset = new Vector3(1.2f, 0.6f, -1f);

			[Tooltip("魔弾砲狙い時の見下ろし角度")]
			public float aimLookDownAngle = -5f;

			[Tooltip("魔弾砲狙い時のカメラとプレイヤ\u30fcとの距離")]
			public float aimDistanceToSelf = 2.6f;

			[Tooltip("波動砲のカメラオフセット")]
			public Vector3 beamChargeCameraOffset = Vector3.zero;

			[Tooltip("波動砲のカメラ見下ろし角度")]
			public float beamChargeCameraLookDownAngle;

			[Tooltip("波動砲のカメラと自分との距離")]
			public float beamChargeCameraDistanceToSelf;

			[Tooltip("波動砲発射時のカメラ位置")]
			public Vector3 beamCameraPosition = Vector3.zero;

			[Tooltip("波動砲発射時のカメラ")]
			public Vector3 beamCameraRotationEular = Vector3.zero;
		}

		[Tooltip("タ\u30fcゲットカメラ ピッチ")]
		public float targetingPitch = 10f;

		[Tooltip("タ\u30fcゲットカメラ ピッチ変化最短距離")]
		public float targetingPitchNearDistance;

		[Tooltip("タ\u30fcゲットカメラ ピッチ変化最長距離")]
		public float targetingPitchFarDistance;

		[Tooltip("タ\u30fcゲットカメラ ピッチ変化カ\u30fcブ最小角度")]
		public float targetingPitchMinAngle = 10f;

		[Tooltip("タ\u30fcゲットカメラ ピッチ変化カ\u30fcブ最大角度")]
		public float targetingPitchMaxAngle = 10f;

		[Tooltip("タ\u30fcゲットカメラ ピッチ変化カ\u30fcブ")]
		public AnimationCurve targetingPitchCurve;

		[Tooltip("タ\u30fcゲットカメラ 距離")]
		public float targetingDistance = 4f;

		[Tooltip("タ\u30fcゲットカメラ オフセット")]
		public Vector3 targetingOffset = Vector3.zero;

		[Tooltip("タ\u30fcゲットカメラ カメラ移動最大速度")]
		public float targetingMaxSpeed = 999f;

		[Tooltip("フリ\u30fcカメラ ピッチ")]
		public float freePitch = 20f;

		[Tooltip("フリ\u30fcカメラ 距離")]
		public float freeDistance = 10f;

		[Tooltip("フリ\u30fcカメラ オフセット")]
		public Vector3 freeOffset = Vector3.zero;

		[Tooltip("フリ\u30fcカメラ カメラ移動最大速度")]
		public float freeMaxSpeed = 999f;

		[Tooltip("true:タ\u30fcゲットカメラ抜け時方向固定")]
		public bool normalEnable = true;

		[Tooltip("true:タ\u30fcゲットカメラ有効")]
		public bool targetEnable = true;

		[Tooltip("敵可動領域角度")]
		public float moveableTargetAngle = 10f;

		[Tooltip("タ\u30fcゲットカメラの補正レ\u30fcト")]
		public float smoothTargetingRate = 0.1f;

		[Tooltip("フリ\u30fcカメラの補正レ\u30fcト")]
		public float smoothFreeRate = 0.1f;

		[Tooltip("カメラ切り替え時間")]
		public float modeSwitchTime = 0.3f;

		[Tooltip("カメラが寄る限界値")]
		public float distanceLimit = 2f;

		[Tooltip("カメラの距離が寄せてから元に戻るまでの時間")]
		public float distanceLerpTime = 2f;

		[Tooltip("タ\u30fcゲット追尾の後方オフセット")]
		public float followBackOffset = 3.5f;

		[Tooltip("タ\u30fcゲット追尾の左右割合")]
		public float followSidePercent = 0.8f;

		[Tooltip("タ\u30fcゲット追尾の補正レ\u30fcト")]
		public float followRate = 0.1f;

		[Tooltip("弓狙い設定")]
		public ArrowAimSettings arrowAimSettings = new ArrowAimSettings();

		[Tooltip("大型モンスタ\u30fc戦のカメラ補正設定")]
		public CameraTargetOffsetSettings cameraTargetOffsetSettings = new CameraTargetOffsetSettings();

		[Tooltip("フィ\u30fcルドのカメラ補正設定")]
		public CameraTargetOffsetSettings cameraFieldOffsetSettings = new CameraTargetOffsetSettings();

		[Tooltip("魔弾砲関連のカメラ補正設定")]
		public CannonAimSettings cannonAimSettings = new CannonAimSettings();
	}

	protected class ShakeParam
	{
		public float shakeTime;

		public float shakeLength;

		public float shakeCycleTime;
	}

	public enum CAM_HIT_OBJ_TYPE
	{
		NONE,
		ZOOM
	}

	public class GrabInfo
	{
		public bool enabled;

		public Transform enemyRoot;

		public Vector3 dir;

		public float distance;
	}

	public class TargetOffset
	{
		public Vector3 pos = Vector3.zero;

		public Vector3 rot = Vector3.zero;
	}

	private Camera ctrlCamera;

	public Transform cameraTransform;

	private StageObject targetObject;

	private Player targetPlayer;

	private Self targetSelf;

	private CAMERA_MODE cameraMode;

	[Tooltip("縦画面設定")]
	public Settings portraitSettings = new Settings();

	[Tooltip("横画面設定")]
	public Settings landscapeSettings = new Settings();

	private Vector3 requestPos = Vector3.zero;

	private Quaternion requestRot = Quaternion.identity;

	private Vector3 normalForward = Vector3.back;

	private bool isBossExistsPast;

	private bool switching;

	private float switchTimer;

	private Vector3 posVelocity = Vector3.zero;

	private Vector3 rotVelocity = Vector3.zero;

	private float distanceElapsedTime;

	private float modeChangeTime;

	private float ingameFieldOfView;

	private float fieldOfViewVelocity;

	private Vector3 stopPos = Vector3.zero;

	private Vector3 stopRotEular = Vector3.zero;

	private Vector3 cutPos = Vector3.zero;

	private Quaternion cutRot = Quaternion.identity;

	protected bool adjustCamera;

	[Tooltip("カメラ揺れ 基本周期（秒）")]
	public float shakeCycleTime = 0.2f;

	[Tooltip("カメラ揺れ 減衰率")]
	public float shakeAttenuationPercent = 0.25f;

	[Tooltip("カメラ揺れ 基本振幅")]
	public float shakeAmplitude = 0.5f;

	[Tooltip("カメラ揺れ 最大発生距離")]
	public float shakeMaxFocusLength = 30f;

	[Tooltip("カメラ揺れ 最大同時発生数（0で無制限")]
	public int shakeMaxNum;

	protected List<ShakeParam> shakeParams = new List<ShakeParam>();

	[Tooltip("カメラがオブジェクトに当たった際の挙動")]
	public CAM_HIT_OBJ_TYPE hitObjectType;

	private RadialBlurFilter radialBlurFilter;

	private float radialBlurStrengthPerTime;

	private float radialBlurStrengthValue;

	private Transform radialBlurCenterTransform;

	private Vector3 radialBlurCenterPos = Vector3.zero;

	private Vector3 beforeFollowVec = Vector3.zero;

	private GrabInfo _grabInfo = new GrabInfo();

	private TargetOffset validAnimEventTargetOffset;

	private TargetOffset targetOffsetByPlayer;

	private TargetOffset targetOffsetByEnemy;

	public Vector3 movePosition
	{
		get;
		protected set;
	}

	public Quaternion moveRotation
	{
		get;
		protected set;
	}

	public Transform target
	{
		get;
		set;
	}

	public Settings validSettings
	{
		get;
		private set;
	}

	public int arrowCameraMode
	{
		get;
		private set;
	}

	public bool isArrowAimBossMode
	{
		get;
		protected set;
	}

	public bool isMotionCameraMode
	{
		get;
		protected set;
	}

	public bool isFixedCameraMode
	{
		get;
		protected set;
	}

	public Transform[] motionCameraTransforms
	{
		get;
		protected set;
	}

	public Transform motionCameraParent
	{
		get;
		protected set;
	}

	public GrabInfo grabInfo => _grabInfo;

	public InGameCameraManager()
	{
		isMotionCameraMode = false;
		motionCameraTransforms = null;
		motionCameraParent = null;
		cameraMode = CAMERA_MODE.DEFAULT;
		arrowCameraMode = InGameManager.GetArrowCameraType(GameSaveData.instance.arrowCameraKey);
	}

	public void SetCameraMode(CAMERA_MODE cameraMode)
	{
		this.cameraMode = cameraMode;
	}

	public bool IsCameraModeBeam()
	{
		return cameraMode == CAMERA_MODE.CANNON_BEAM;
	}

	public bool IsCameraMode(CAMERA_MODE mode)
	{
		return cameraMode == mode;
	}

	public void SetStopPos(Vector3 pos)
	{
		stopPos = pos;
	}

	public void SetStopRotEular(Vector3 rotEular)
	{
		stopRotEular = rotEular;
	}

	public void SetCutPos(Vector3 cutPos)
	{
		this.cutPos = cutPos;
	}

	public void SetCutRot(Quaternion cutRot)
	{
		this.cutRot = cutRot;
	}

	public void SetArrowCameraMode(int mode)
	{
		arrowCameraMode = mode;
	}

	public void SetAnimEventTargetOffsetByPlayer(TargetOffset targetOffset)
	{
		if (targetOffset != null)
		{
			targetOffsetByPlayer = targetOffset;
		}
	}

	public void SetAnimEventTargetOffsetByEnemy(TargetOffset targetOffset)
	{
		if (targetOffset != null)
		{
			targetOffsetByEnemy = targetOffset;
		}
	}

	public void ClearAnimEventTargetOffsetByPlayer()
	{
		validAnimEventTargetOffset = null;
		targetOffsetByPlayer = null;
	}

	public void ClearAnimEventTargetOffsetByEnemy()
	{
		validAnimEventTargetOffset = null;
		targetOffsetByEnemy = null;
	}

	public void AllClearAnimEventTargetOffset()
	{
		validAnimEventTargetOffset = null;
		targetOffsetByPlayer = null;
		targetOffsetByEnemy = null;
	}

	public void ClearStopCameraMode()
	{
		if (IsCameraMode(CAMERA_MODE.STOP))
		{
			SetCameraMode(CAMERA_MODE.DEFAULT);
		}
	}

	public void ClearCutCameraMode()
	{
		if (IsCameraMode(CAMERA_MODE.CUT))
		{
			SetCameraMode(CAMERA_MODE.DEFAULT);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		distanceElapsedTime = validSettings.distanceLerpTime;
	}

	private void OnEnable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = false;
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		EndRadialBlurFilter(0f);
		MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(GameSceneGlobalSettings.GetDefaultMainCameraCullingMask());
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = true;
		}
	}

	private void Start()
	{
		if ((UnityEngine.Object)ctrlCamera == (UnityEngine.Object)null)
		{
			ctrlCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
		cameraTransform = ctrlCamera.transform;
		movePosition = cameraTransform.position;
		moveRotation = cameraTransform.rotation;
		radialBlurFilter = ctrlCamera.GetComponent<RadialBlurFilter>();
		if ((UnityEngine.Object)radialBlurFilter != (UnityEngine.Object)null)
		{
			radialBlurFilter.enabled = false;
		}
	}

	private void UpdateRadialBlur()
	{
		Vector3 zero = Vector3.zero;
		zero = ((!((UnityEngine.Object)radialBlurCenterTransform != (UnityEngine.Object)null)) ? radialBlurCenterPos : radialBlurCenterTransform.position);
		Vector2 center = WorldToScreenPoint(zero).ToVector2XY();
		center.x /= (float)Screen.width;
		center.y /= (float)Screen.height;
		radialBlurFilter.SetCenter(center);
		if (radialBlurStrengthPerTime != 0f)
		{
			float strength = radialBlurFilter.strength;
			strength += radialBlurStrengthPerTime * Time.deltaTime;
			if (radialBlurStrengthPerTime > 0f)
			{
				if (strength >= radialBlurStrengthValue)
				{
					strength = radialBlurStrengthValue;
					radialBlurStrengthPerTime = 0f;
				}
				radialBlurFilter.strength = strength;
			}
			else
			{
				if (strength <= radialBlurStrengthValue)
				{
					strength = radialBlurStrengthValue;
					if (strength <= 0f)
					{
						EndRadialBlurFilter(0f);
					}
				}
				radialBlurFilter.strength = strength;
			}
		}
	}

	private void UpdatePlayerCamera()
	{
		Settings validSettings = this.validSettings;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		float fieldOfView = ctrlCamera.fieldOfView;
		Vector3 cameraTargetPos = targetObject.GetCameraTargetPos();
		if (!((UnityEngine.Object)targetPlayer == (UnityEngine.Object)null))
		{
			StageObject stageObject = null;
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				stageObject = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			}
			bool flag = (UnityEngine.Object)stageObject != (UnityEngine.Object)null;
			float num = 0f;
			float maxSpeed = 999f;
			bool flag2 = false;
			if (isBossExistsPast != flag)
			{
				isBossExistsPast = flag;
				switching = true;
				switchTimer = validSettings.modeSwitchTime;
				flag2 = true;
			}
			distanceElapsedTime += Time.deltaTime;
			if (distanceElapsedTime > validSettings.distanceLerpTime)
			{
				distanceElapsedTime = validSettings.distanceLerpTime;
			}
			modeChangeTime -= Time.deltaTime;
			if (modeChangeTime < 0f)
			{
				modeChangeTime = 0f;
			}
			float num2 = distanceElapsedTime / validSettings.distanceLerpTime;
			float num3 = 0f;
			fieldOfView = ingameFieldOfView;
			if (!isFixedCameraMode)
			{
				if (isMotionCameraMode)
				{
					num = 0f;
					if (motionCameraTransforms != null)
					{
						int num4 = 0;
						if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
						{
							num4 = ((!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? 1 : 0);
						}
						requestPos = motionCameraTransforms[num4].position;
						requestRot = motionCameraTransforms[num4].rotation;
						Vector3 localScale = motionCameraTransforms[num4].localScale;
						float num5 = localScale.x;
						if (num5 > 0f)
						{
							num5 = Utility.HorizontalToVerticalFOV(num5);
						}
						fieldOfView = num5;
					}
				}
				else if (!flag || !validSettings.targetEnable)
				{
					num = validSettings.smoothFreeRate;
					num3 = validSettings.freeDistance;
					maxSpeed = validSettings.freeMaxSpeed;
					Vector3 back = Vector3.back;
					if (validSettings.normalEnable)
					{
						back = normalForward;
					}
					Vector3 vector = cameraTargetPos;
					float num6 = 1f;
					Vector3 axis = Vector3.Cross(back, Vector3.up);
					Vector3 vector2 = Quaternion.AngleAxis(0f - validSettings.freePitch, axis) * back * Mathf.Lerp(validSettings.distanceLimit, num3, num2) * num6;
					Vector3 b = Quaternion.LookRotation(back) * validSettings.freeOffset * num6;
					Vector3 b2 = Vector3.zero;
					if (targetPlayer.actionID == Character.ACTION_ID.ATTACK && !targetPlayer.isArrowAimLesserMode && (UnityEngine.Object)targetPlayer.attackStartTarget != (UnityEngine.Object)null && (UnityEngine.Object)targetPlayer.attackStartTarget == (UnityEngine.Object)targetPlayer.actionTarget && MonoBehaviourSingleton<TargetMarkerManager>.I.isTargetLock)
					{
						Vector3 a = targetPlayer.attackStartTarget._position - targetPlayer._position;
						Quaternion rotation = Quaternion.identity;
						Vector3 vector3 = vector2;
						Vector3 vector4 = a - b;
						Vector3 zero = Vector3.zero;
						if (back != Vector3.forward)
						{
							rotation = Quaternion.FromToRotation(back, Vector3.forward);
							vector3 = rotation * vector3;
							vector4 = rotation * vector4;
						}
						if (vector4.z < (0f - validSettings.followBackOffset) * num6)
						{
							zero.z = vector4.z + validSettings.followBackOffset * num6;
						}
						Vector3 vector5 = Quaternion.FromToRotation(vector3, Vector3.forward) * (vector3 + vector4);
						float num7 = Mathf.Tan(0.0174532924f * fieldOfView * 0.5f) * ((float)Screen.width / (float)Screen.height);
						float num8 = Mathf.Abs(vector5.x / vector5.z);
						float num9 = num8 / num7;
						if (num9 > validSettings.followSidePercent)
						{
							zero.x = vector4.x * (num9 - validSettings.followSidePercent) / num9;
						}
						if (back != Vector3.forward)
						{
							b2 = Quaternion.Inverse(rotation) * zero;
						}
					}
					requestPos = cameraTargetPos + (beforeFollowVec = Vector3.Lerp(beforeFollowVec, b2, validSettings.followRate)) - vector2 + b;
					requestRot = Quaternion.LookRotation(vector2.normalized);
					requestPos += Quaternion.LookRotation(normalForward) * this.validSettings.cameraFieldOffsetSettings.targetOffsetPos;
					requestRot *= Quaternion.Euler(this.validSettings.cameraFieldOffsetSettings.targetOffsetRot);
					if (MonoBehaviourSingleton<FieldManager>.IsValid())
					{
						requestPos += Quaternion.LookRotation(normalForward) * MonoBehaviourSingleton<FieldManager>.I.cameraOffsetPos_Vec;
						requestRot *= MonoBehaviourSingleton<FieldManager>.I.cameraOffsetRot_Quat;
					}
				}
				else
				{
					num = validSettings.smoothTargetingRate;
					num3 = validSettings.targetingDistance;
					maxSpeed = validSettings.targetingMaxSpeed;
					Vector3 vector = stageObject.GetCameraTargetPos();
					Vector3 vector6 = vector - requestPos;
					vector6.y = 0f;
					vector6.Normalize();
					Vector3 vector7 = cameraTargetPos - requestPos;
					vector7.y = 0f;
					vector7.Normalize();
					if (flag2)
					{
						normalForward = vector - cameraTargetPos;
						normalForward.y = 0f;
						normalForward.Normalize();
					}
					else
					{
						float num10 = Vector3.Angle(vector6, vector7);
						if (num10 > validSettings.moveableTargetAngle)
						{
							Vector3 vector8 = Vector3.Cross(vector7, vector6);
							float num11 = (!(vector8.y >= 0f)) ? (-1f) : 1f;
							normalForward = Quaternion.AngleAxis((num10 - validSettings.moveableTargetAngle) * num11, Vector3.up) * vector7;
						}
					}
					if (normalForward == Vector3.zero)
					{
						normalForward = Vector3.back;
					}
					float num12 = validSettings.targetingPitch;
					if (validSettings.targetingPitchNearDistance != 0f || validSettings.targetingPitchFarDistance != 0f)
					{
						float magnitude = (vector - cameraTargetPos).magnitude;
						float time = Mathf.Clamp01((magnitude - validSettings.targetingPitchNearDistance) / (validSettings.targetingPitchFarDistance - validSettings.targetingPitchNearDistance));
						time = validSettings.targetingPitchCurve.Evaluate(time);
						num12 = validSettings.targetingPitchMinAngle * (1f - time) + validSettings.targetingPitchMaxAngle * time;
					}
					Vector3 axis2 = Vector3.Cross(normalForward, Vector3.up);
					Vector3 b3 = Quaternion.AngleAxis(0f - num12, axis2) * normalForward * Mathf.Lerp(validSettings.distanceLimit, num3, num2);
					Vector3 b4 = Quaternion.LookRotation(normalForward) * validSettings.targetingOffset;
					requestPos = cameraTargetPos - b3 + b4;
					requestRot = Quaternion.LookRotation(b3.normalized);
					requestPos += Quaternion.LookRotation(normalForward) * this.validSettings.cameraTargetOffsetSettings.targetOffsetPos;
					requestRot *= Quaternion.Euler(this.validSettings.cameraTargetOffsetSettings.targetOffsetRot);
					if (MonoBehaviourSingleton<FieldManager>.IsValid())
					{
						requestPos += Quaternion.LookRotation(normalForward) * MonoBehaviourSingleton<FieldManager>.I.cameraOffsetPos_Vec;
						requestRot *= MonoBehaviourSingleton<FieldManager>.I.cameraOffsetRot_Quat;
					}
					if (targetOffsetByPlayer != null)
					{
						validAnimEventTargetOffset = targetOffsetByPlayer;
					}
					else if (targetOffsetByEnemy != null)
					{
						validAnimEventTargetOffset = targetOffsetByEnemy;
					}
					else
					{
						validAnimEventTargetOffset = null;
					}
					if (validAnimEventTargetOffset != null)
					{
						requestPos += Quaternion.LookRotation(normalForward) * validAnimEventTargetOffset.pos;
						requestRot *= Quaternion.Euler(validAnimEventTargetOffset.rot);
					}
				}
			}
			if (hitObjectType != 0 && !isMotionCameraMode)
			{
				Vector3 vector9 = requestPos - cameraTargetPos;
				RaycastHit hitInfo = default(RaycastHit);
				Ray ray = new Ray(cameraTargetPos, vector9.normalized);
				if (Physics.Raycast(ray, out hitInfo, num3, 2359808) && hitObjectType == CAM_HIT_OBJ_TYPE.ZOOM)
				{
					float num13 = hitInfo.distance;
					if (num13 < validSettings.distanceLimit)
					{
						num13 = validSettings.distanceLimit;
					}
					num13 -= validSettings.distanceLimit;
					float num14 = num3 - validSettings.distanceLimit;
					float num15 = 0f;
					if (num14 > 0f)
					{
						num15 = validSettings.distanceLerpTime * (num13 / num14);
					}
					if (modeChangeTime <= 0f)
					{
						distanceElapsedTime -= Time.deltaTime * 5f;
						if (distanceElapsedTime < num15)
						{
							distanceElapsedTime = num15;
						}
					}
					else
					{
						distanceElapsedTime = num15;
					}
					num2 = distanceElapsedTime / validSettings.distanceLerpTime;
					requestPos = cameraTargetPos + ray.direction * Mathf.Lerp(validSettings.distanceLimit, num3, num2);
				}
			}
			if (num != 0f && modeChangeTime <= 0f)
			{
				num *= ((!(num2 < 0.1f)) ? num2 : 0.1f);
			}
			if (switching)
			{
				switchTimer -= Time.deltaTime;
				if (switchTimer <= 0f)
				{
					switching = false;
					switchTimer = 0f;
				}
				num = Mathf.Lerp(validSettings.modeSwitchTime, num, 1f - switchTimer / validSettings.modeSwitchTime);
			}
			if (adjustCamera || isMotionCameraMode || isFixedCameraMode)
			{
				if (adjustCamera)
				{
					adjustCamera = false;
				}
				this.movePosition = requestPos;
				this.moveRotation = requestRot;
			}
			else
			{
				Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, num, maxSpeed, Time.deltaTime);
				Vector3 zero2 = Vector3.zero;
				Vector3 eulerAngles = moveRotation.eulerAngles;
				Vector3 eulerAngles2 = requestRot.eulerAngles;
				zero2.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, num, 1000f, Time.deltaTime);
				zero2.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, num, 1000f, Time.deltaTime);
				zero2.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, num, 1000f, Time.deltaTime);
				Quaternion identity = Quaternion.identity;
				identity.eulerAngles = zero2;
				this.movePosition = movePosition2;
				this.moveRotation = identity;
				fieldOfView = Mathf.SmoothDamp(ctrlCamera.fieldOfView, fieldOfView, ref fieldOfViewVelocity, num, 1000f, Time.deltaTime);
			}
			ctrlCamera.fieldOfView = fieldOfView;
		}
	}

	private void UpdateArrowAimBossModeCamera()
	{
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		float target = ingameFieldOfView;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		StageObject x = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			x = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		}
		bool flag = (UnityEngine.Object)x != (UnityEngine.Object)null;
		if (isBossExistsPast != flag)
		{
			isBossExistsPast = flag;
			switching = true;
			switchTimer = validSettings.modeSwitchTime;
		}
		distanceElapsedTime += Time.deltaTime;
		if (distanceElapsedTime > validSettings.distanceLerpTime)
		{
			distanceElapsedTime = validSettings.distanceLerpTime;
		}
		modeChangeTime -= Time.deltaTime;
		if (modeChangeTime < 0f)
		{
			modeChangeTime = 0f;
		}
		Settings.ArrowAimSettings arrowAimSettings = validSettings.arrowAimSettings;
		float num = arrowAimSettings.smoothTargetingRate;
		float targetingMaxSpeed = validSettings.targetingMaxSpeed;
		float num2 = distanceElapsedTime / validSettings.distanceLerpTime;
		float targetingDistance = arrowAimSettings.targetingDistance;
		normalForward = targetSelf.arrowAimForward.normalized;
		int arrowAimStartSign = targetSelf.arrowAimStartSign;
		Vector3 axis = Vector3.Cross(normalForward, Vector3.up);
		Vector3 b = Quaternion.AngleAxis(0f - arrowAimSettings.targetingPitch, axis) * normalForward * Mathf.Lerp(validSettings.distanceLimit, targetingDistance, num2);
		Vector3 point = arrowAimSettings.targetingOffset;
		if (arrowCameraMode == 1)
		{
			point = ((!targetSelf.IsAbleArrowSitShot()) ? ((arrowAimStartSign > 0) ? arrowAimSettings.targetingLeftOffset : arrowAimSettings.targetingOffset) : ((arrowAimStartSign > 0) ? arrowAimSettings.targetingAvoidShotLeftOffset : arrowAimSettings.targetingAvoidShotRightOffset));
		}
		Vector3 b2 = Quaternion.LookRotation(normalForward) * point;
		requestPos = cameraTargetPos - b + b2;
		requestRot = Quaternion.LookRotation(b.normalized);
		if (!flag || !validSettings.targetEnable)
		{
			modeChangeTime = validSettings.smoothFreeRate;
		}
		else
		{
			modeChangeTime = validSettings.smoothTargetingRate;
		}
		distanceElapsedTime = validSettings.distanceLerpTime;
		if (hitObjectType != 0 && !isMotionCameraMode)
		{
			Vector3 vector = requestPos - cameraTargetPos;
			RaycastHit hitInfo = default(RaycastHit);
			Ray ray = new Ray(cameraTargetPos, vector.normalized);
			if (Physics.Raycast(ray, out hitInfo, targetingDistance, 2359808) && hitObjectType == CAM_HIT_OBJ_TYPE.ZOOM)
			{
				float num3 = hitInfo.distance;
				if (num3 < validSettings.distanceLimit)
				{
					num3 = validSettings.distanceLimit;
				}
				num3 -= validSettings.distanceLimit;
				float num4 = targetingDistance - validSettings.distanceLimit;
				float num5 = 0f;
				if (num4 > 0f)
				{
					num5 = validSettings.distanceLerpTime * (num3 / num4);
				}
				if (modeChangeTime <= 0f)
				{
					distanceElapsedTime -= Time.deltaTime * 5f;
					if (distanceElapsedTime < num5)
					{
						distanceElapsedTime = num5;
					}
				}
				else
				{
					distanceElapsedTime = num5;
				}
				num2 = distanceElapsedTime / validSettings.distanceLerpTime;
				requestPos = cameraTargetPos + ray.direction * Mathf.Lerp(validSettings.distanceLimit, targetingDistance, num2);
			}
		}
		if (num != 0f && modeChangeTime <= 0f)
		{
			num *= ((!(num2 < 0.1f)) ? num2 : 0.1f);
		}
		if (switching)
		{
			switchTimer -= Time.deltaTime;
			if (switchTimer <= 0f)
			{
				switching = false;
				switchTimer = 0f;
			}
			num = Mathf.Lerp(validSettings.modeSwitchTime, num, 1f - switchTimer / validSettings.modeSwitchTime);
		}
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, num, targetingMaxSpeed, Time.deltaTime);
		Vector3 zero = Vector3.zero;
		Vector3 eulerAngles = moveRotation.eulerAngles;
		Vector3 eulerAngles2 = requestRot.eulerAngles;
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, num, 1000f, Time.deltaTime);
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, num, 1000f, Time.deltaTime);
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, num, 1000f, Time.deltaTime);
		Quaternion identity = Quaternion.identity;
		identity.eulerAngles = zero;
		this.movePosition = movePosition2;
		this.moveRotation = identity;
		target = Mathf.SmoothDamp(ctrlCamera.fieldOfView, target, ref fieldOfViewVelocity, num, 1000f, Time.deltaTime);
		ctrlCamera.fieldOfView = target;
	}

	private void UpdateGrabCamera()
	{
		Vector3 position = target.position;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		normalForward = grabInfo.dir;
		Vector3 b = grabInfo.enemyRoot.rotation * normalForward * grabInfo.distance;
		requestPos = position + b;
		requestRot = Quaternion.LookRotation(-b.normalized);
		float smoothTargetingRate = validSettings.smoothTargetingRate;
		float targetingDistance = validSettings.targetingDistance;
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, smoothTargetingRate, targetingDistance, Time.deltaTime);
		Vector3 zero = Vector3.zero;
		Vector3 eulerAngles = moveRotation.eulerAngles;
		Vector3 eulerAngles2 = requestRot.eulerAngles;
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, smoothTargetingRate, 1000f, Time.deltaTime);
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, smoothTargetingRate, 1000f, Time.deltaTime);
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, smoothTargetingRate, 1000f, Time.deltaTime);
		Quaternion identity = Quaternion.identity;
		identity.eulerAngles = zero;
		this.movePosition = movePosition2;
		this.moveRotation = identity;
	}

	private void UpdateCannonAimCamera()
	{
		Settings validSettings = this.validSettings;
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		normalForward = targetSelf.cannonAimForward.normalized;
		Vector3 axis = Vector3.Cross(normalForward, Vector3.up);
		Vector3 b = Quaternion.AngleAxis(validSettings.cannonAimSettings.aimLookDownAngle, axis) * normalForward * validSettings.cannonAimSettings.aimDistanceToSelf;
		Vector3 b2 = Quaternion.LookRotation(normalForward) * validSettings.cannonAimSettings.aimCameraOffset;
		movePosition = cameraTargetPos - b + b2;
		moveRotation = Quaternion.LookRotation(b.normalized);
	}

	private void UpdateCannonBeamChargeCamera()
	{
		Settings validSettings = this.validSettings;
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		normalForward = targetSelf.cannonAimForward.normalized;
		Vector3 axis = Vector3.Cross(normalForward, Vector3.up);
		Vector3 b = Quaternion.AngleAxis(validSettings.cannonAimSettings.beamChargeCameraLookDownAngle, axis) * normalForward * validSettings.cannonAimSettings.beamChargeCameraDistanceToSelf;
		Vector3 b2 = Quaternion.LookRotation(normalForward) * validSettings.cannonAimSettings.beamChargeCameraOffset;
		movePosition = cameraTargetPos - b + b2;
		moveRotation = Quaternion.LookRotation(b.normalized);
	}

	private void UpdateCannonBeamCamera()
	{
		Settings validSettings = this.validSettings;
		movePosition = validSettings.cannonAimSettings.beamCameraPosition;
		moveRotation = Quaternion.Euler(validSettings.cannonAimSettings.beamCameraRotationEular);
	}

	private void UpdateStopCamera()
	{
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		float smoothTargetingRate = validSettings.smoothTargetingRate;
		float targetingDistance = validSettings.targetingDistance;
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, stopPos, ref posVelocity, smoothTargetingRate, targetingDistance, Time.deltaTime);
		Vector3 zero = Vector3.zero;
		Vector3 eulerAngles = moveRotation.eulerAngles;
		Vector3 vector = stopRotEular;
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, vector.x, ref rotVelocity.x, smoothTargetingRate, 1000f, Time.deltaTime);
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, vector.y, ref rotVelocity.y, smoothTargetingRate, 1000f, Time.deltaTime);
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, vector.z, ref rotVelocity.z, smoothTargetingRate, 1000f, Time.deltaTime);
		Quaternion identity = Quaternion.identity;
		identity.eulerAngles = zero;
		this.movePosition = movePosition2;
		this.moveRotation = identity;
	}

	private void UpdateCutCamera()
	{
		movePosition = cutPos;
		moveRotation = cutRot;
	}

	private void UpdateShake()
	{
		Vector3 vector = Vector3.zero;
		int num = 0;
		while (num < shakeParams.Count)
		{
			ShakeParam shakeParam = shakeParams[num];
			float num2 = shakeParam.shakeCycleTime;
			if (num2 <= 0f)
			{
				Log.Warning(LOG.INGAME, "カメラ揺れの振動周期が0");
				shakeParams.RemoveAt(num);
			}
			else
			{
				int num3 = (int)(shakeParam.shakeTime * 2f / num2);
				shakeParam.shakeTime += Time.deltaTime;
				int num4 = (int)(shakeParam.shakeTime * 2f / num2);
				bool flag = false;
				if (num3 != num4)
				{
					shakeParam.shakeLength *= Mathf.Pow(shakeAttenuationPercent, (float)(num4 - num3));
					if (shakeParam.shakeLength < 0.01f)
					{
						flag = true;
					}
				}
				if (flag)
				{
					shakeParams.RemoveAt(num);
				}
				else
				{
					vector += Vector3.up * (shakeParam.shakeLength * Mathf.Sin(shakeParam.shakeTime * 3.14159274f * 2f / num2));
					num++;
				}
			}
		}
		cameraTransform.position = movePosition + vector;
		cameraTransform.rotation = moveRotation;
	}

	private void LateUpdate()
	{
		if ((UnityEngine.Object)radialBlurFilter != (UnityEngine.Object)null && radialBlurFilter.enabled)
		{
			UpdateRadialBlur();
		}
		if (!((UnityEngine.Object)target == (UnityEngine.Object)null))
		{
			if ((UnityEngine.Object)targetObject == (UnityEngine.Object)null || (UnityEngine.Object)targetObject._transform != (UnityEngine.Object)target)
			{
				targetObject = target.GetComponent<StageObject>();
				targetPlayer = (targetObject as Player);
				targetSelf = (targetObject as Self);
			}
			switch (cameraMode)
			{
			case CAMERA_MODE.GRABBED:
				UpdateGrabCamera();
				break;
			case CAMERA_MODE.ARROW_AIM_BOSS:
				UpdateArrowAimBossModeCamera();
				break;
			case CAMERA_MODE.CANNON_AIM:
				UpdateCannonAimCamera();
				break;
			case CAMERA_MODE.CANNON_BEAM_CHARGE:
				UpdateCannonBeamChargeCamera();
				break;
			case CAMERA_MODE.CANNON_BEAM:
				UpdateCannonBeamCamera();
				break;
			case CAMERA_MODE.STOP:
				UpdateStopCamera();
				break;
			case CAMERA_MODE.CUT:
				UpdateCutCamera();
				break;
			default:
				UpdatePlayerCamera();
				break;
			}
			UpdateShake();
		}
	}

	public Vector3 WorldToScreenPoint(Vector3 pos)
	{
		return ctrlCamera.WorldToScreenPoint(pos);
	}

	public Vector3 WorldToViewportPoint(Vector3 pos)
	{
		return ctrlCamera.WorldToViewportPoint(pos);
	}

	public Vector3 ScreenToWorldPoint(Vector3 pos)
	{
		return ctrlCamera.ScreenToWorldPoint(pos);
	}

	public float GetPixelHeight()
	{
		return (float)ctrlCamera.pixelHeight;
	}

	public void AdjustCameraPosition()
	{
		adjustCamera = true;
		posVelocity = Vector3.zero;
		rotVelocity = Vector3.zero;
		fieldOfViewVelocity = 0f;
		switching = false;
		switchTimer = 0f;
		modeChangeTime = 0f;
	}

	public void SetShakeCamera(Vector3 pos, float percent, float cycle_time = 0f)
	{
		float magnitude = (pos - movePosition).magnitude;
		float num = (shakeMaxFocusLength - magnitude) / shakeMaxFocusLength;
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = shakeAmplitude * percent * num;
		if (num2 > 0.01f)
		{
			ShakeParam shakeParam = new ShakeParam();
			shakeParam.shakeTime = 0f;
			shakeParam.shakeLength = num2;
			shakeParam.shakeCycleTime = cycle_time;
			if (shakeParam.shakeCycleTime <= 0f)
			{
				shakeParam.shakeCycleTime = shakeCycleTime;
			}
			shakeParams.Add(shakeParam);
			if (shakeMaxNum > 0 && shakeParams.Count > shakeMaxNum)
			{
				shakeParams.RemoveAt(0);
			}
		}
	}

	public void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			validSettings = portraitSettings;
			ingameFieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.inGamePortraitFieldOfView;
		}
		else
		{
			validSettings = landscapeSettings;
			ingameFieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.inGameLandscapeFieldOfView;
		}
		if (QuestManager.IsValidInGameWaveMatch(false) && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			validSettings.cameraFieldOffsetSettings.targetOffsetPos.y = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam().cameraFieldOffsetY;
		}
	}

	public bool IsEndMotionCamera()
	{
		if (!isMotionCameraMode || motionCameraTransforms == null)
		{
			return true;
		}
		int num = 0;
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			num = ((!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? 1 : 0);
		}
		Animation component = motionCameraTransforms[num].gameObject.GetComponent<Animation>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
		{
			return true;
		}
		return !component.isPlaying;
	}

	public void OnHappenQuestDirection(bool enable, Transform boss_transform = null, UnityEngine.Object[] cameras = null, Vector3[] camera_offsets = null)
	{
		if (enable)
		{
			EndRadialBlurFilter(0f);
			int mainCameraCullingMask = 262144;
			MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(mainCameraCullingMask);
			if ((UnityEngine.Object)boss_transform != (UnityEngine.Object)null && cameras != null)
			{
				SetMotionCamera(true, boss_transform, cameras, camera_offsets);
			}
			if (MonoBehaviourSingleton<AudioListenerManager>.IsValid())
			{
				MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, true);
			}
		}
		else
		{
			SetMotionCamera(false, null, null, null);
			MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(GameSceneGlobalSettings.GetDefaultMainCameraCullingMask());
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
			{
				OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
			}
			if (MonoBehaviourSingleton<AudioListenerManager>.IsValid())
			{
				MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, false);
			}
			AdjustCameraPosition();
		}
	}

	public void SetMotionCamera(bool enable, Transform target_transform = null, UnityEngine.Object[] cameras = null, Vector3[] camera_offsets = null)
	{
		if (enable)
		{
			motionCameraTransforms = new Transform[2];
			Transform transform = Utility.CreateGameObject("FieldQuestCamera", base._transform, -1);
			transform.position = target_transform.position;
			transform.rotation = target_transform.rotation;
			Vector3 one = Vector3.one;
			Vector3 lossyScale = target_transform.lossyScale;
			float x = lossyScale.x;
			Vector3 lossyScale2 = base._transform.lossyScale;
			one.x = x / lossyScale2.x;
			Vector3 lossyScale3 = target_transform.lossyScale;
			float y = lossyScale3.y;
			Vector3 lossyScale4 = base._transform.lossyScale;
			one.y = y / lossyScale4.y;
			Vector3 lossyScale5 = target_transform.lossyScale;
			float z = lossyScale5.z;
			Vector3 lossyScale6 = base._transform.lossyScale;
			one.z = z / lossyScale6.z;
			transform.localScale = one;
			motionCameraParent = transform;
			for (int i = 0; i < 2; i++)
			{
				Transform transform2 = Utility.CreateGameObject("offset_" + i.ToString(), transform, -1);
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
				if (camera_offsets != null)
				{
					transform2.localPosition = camera_offsets[i];
				}
				Transform transform3 = ResourceUtility.Realizes(cameras[i], transform2, -1);
				if ((UnityEngine.Object)transform3 == (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
					return;
				}
				transform3.localPosition = Vector3.zero;
				transform3.localRotation = Quaternion.identity;
				transform3.localScale = Vector3.zero;
				motionCameraTransforms[i] = transform3;
			}
			isMotionCameraMode = true;
		}
		else
		{
			isMotionCameraMode = false;
			if ((UnityEngine.Object)motionCameraParent != (UnityEngine.Object)null)
			{
				UnityEngine.Object.Destroy(motionCameraParent.gameObject);
				motionCameraParent = null;
			}
			if (motionCameraTransforms != null)
			{
				motionCameraTransforms = null;
			}
		}
	}

	public void StartRadialBlurFilter(float time, float strength, Vector3 center_pos)
	{
		if (!((UnityEngine.Object)radialBlurFilter == (UnityEngine.Object)null))
		{
			_StartRadialBlurFilter(time, strength);
			radialBlurCenterPos = center_pos;
			radialBlurCenterTransform = null;
		}
	}

	public void StartRadialBlurFilter(float time, float strength, Transform center_transform)
	{
		if (!((UnityEngine.Object)radialBlurFilter == (UnityEngine.Object)null))
		{
			_StartRadialBlurFilter(time, strength);
			radialBlurCenterPos = Vector3.zero;
			radialBlurCenterTransform = center_transform;
		}
	}

	private void _StartRadialBlurFilter(float time, float strength)
	{
		if (!((UnityEngine.Object)radialBlurFilter == (UnityEngine.Object)null))
		{
			radialBlurFilter.enabled = true;
			radialBlurFilter.StartFilter();
			radialBlurStrengthValue = strength;
			if (time <= 0f)
			{
				radialBlurFilter.strength = strength;
			}
			else
			{
				radialBlurStrengthPerTime = strength / time;
			}
		}
	}

	public void ChangeRadialBlurFilter(float time, float strength)
	{
		if (!((UnityEngine.Object)radialBlurFilter == (UnityEngine.Object)null) && radialBlurFilter.enabled)
		{
			if (time <= 0f)
			{
				radialBlurStrengthValue = strength;
				if (strength <= 0f)
				{
					radialBlurFilter.strength = 0f;
					radialBlurFilter.StopFilter();
					radialBlurFilter.enabled = false;
				}
				else
				{
					radialBlurFilter.strength = strength;
				}
			}
			else
			{
				radialBlurStrengthPerTime = (0f - (radialBlurStrengthValue - strength)) / time;
				radialBlurStrengthValue = strength;
			}
		}
	}

	public void EndRadialBlurFilter(float time = 0f)
	{
		ChangeRadialBlurFilter(time, 0f);
	}

	public void ResetMovePositionAndRotaion()
	{
		movePosition = ctrlCamera.transform.position;
		moveRotation = ctrlCamera.transform.rotation;
	}
}

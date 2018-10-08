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
			public Vector3 targetingOffset = Vector3.get_zero();

			[Tooltip("狙い中(左側) オフセット")]
			public Vector3 targetingLeftOffset = Vector3.get_zero();

			[Tooltip("狙い中しゃがみ(右側) オフセット")]
			public Vector3 targetingAvoidShotRightOffset = Vector3.get_zero();

			[Tooltip("狙い中しゃがみ(左側) オフセット")]
			public Vector3 targetingAvoidShotLeftOffset = Vector3.get_zero();

			[Tooltip("狙い中 画角（0で変化無し")]
			public float fieldOfView;

			[Tooltip("狙い時の補正レ\u30fcト")]
			public float smoothTargetingRate = 0.1f;
		}

		[Serializable]
		public class CameraTargetOffsetSettings
		{
			[Tooltip("カメラオフセット補正位置")]
			public Vector3 targetOffsetPos = Vector3.get_zero();

			[Tooltip("カメラオフセット補正回転")]
			public Vector3 targetOffsetRot = Vector3.get_zero();
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
			public Vector3 beamChargeCameraOffset = Vector3.get_zero();

			[Tooltip("波動砲のカメラ見下ろし角度")]
			public float beamChargeCameraLookDownAngle;

			[Tooltip("波動砲のカメラと自分との距離")]
			public float beamChargeCameraDistanceToSelf;

			[Tooltip("波動砲発射時のカメラ位置")]
			public Vector3 beamCameraPosition = Vector3.get_zero();

			[Tooltip("波動砲発射時のカメラ")]
			public Vector3 beamCameraRotationEular = Vector3.get_zero();
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
		public Vector3 targetingOffset = Vector3.get_zero();

		[Tooltip("タ\u30fcゲットカメラ カメラ移動最大速度")]
		public float targetingMaxSpeed = 999f;

		[Tooltip("フリ\u30fcカメラ ピッチ")]
		public float freePitch = 20f;

		[Tooltip("フリ\u30fcカメラ 距離")]
		public float freeDistance = 10f;

		[Tooltip("フリ\u30fcカメラ オフセット")]
		public Vector3 freeOffset = Vector3.get_zero();

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
		public Vector3 pos = Vector3.get_zero();

		public Vector3 rot = Vector3.get_zero();
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

	private Vector3 requestPos = Vector3.get_zero();

	private Quaternion requestRot = Quaternion.get_identity();

	private Vector3 normalForward = Vector3.get_back();

	private bool isBossExistsPast;

	private bool switching;

	private float switchTimer;

	private Vector3 posVelocity = Vector3.get_zero();

	private Vector3 rotVelocity = Vector3.get_zero();

	private float distanceElapsedTime;

	private float modeChangeTime;

	private float ingameFieldOfView;

	private float fieldOfViewVelocity;

	private Vector3 stopPos = Vector3.get_zero();

	private Vector3 stopRotEular = Vector3.get_zero();

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

	private Vector3 radialBlurCenterPos = Vector3.get_zero();

	private Vector3 beforeFollowVec = Vector3.get_zero();

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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		stopPos = pos;
	}

	public void SetStopRotEular(Vector3 rotEular)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		stopRotEular = rotEular;
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
		if (component != null)
		{
			component.set_enabled(false);
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
		if (component != null)
		{
			component.set_enabled(true);
		}
	}

	private void Start()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (ctrlCamera == null)
		{
			ctrlCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
		cameraTransform = ctrlCamera.get_transform();
		movePosition = cameraTransform.get_position();
		moveRotation = cameraTransform.get_rotation();
		radialBlurFilter = ctrlCamera.GetComponent<RadialBlurFilter>();
		if (radialBlurFilter != null)
		{
			radialBlurFilter.set_enabled(false);
		}
	}

	private void UpdateRadialBlur()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 zero = Vector3.get_zero();
		zero = ((!(radialBlurCenterTransform != null)) ? radialBlurCenterPos : radialBlurCenterTransform.get_position());
		Vector2 center = WorldToScreenPoint(zero).ToVector2XY();
		center.x /= (float)Screen.get_width();
		center.y /= (float)Screen.get_height();
		radialBlurFilter.SetCenter(center);
		if (radialBlurStrengthPerTime != 0f)
		{
			float strength = radialBlurFilter.strength;
			strength += radialBlurStrengthPerTime * Time.get_deltaTime();
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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		Settings validSettings = this.validSettings;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		float fieldOfView = ctrlCamera.get_fieldOfView();
		Vector3 cameraTargetPos = targetObject.GetCameraTargetPos();
		if (!(targetPlayer == null))
		{
			StageObject stageObject = null;
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				stageObject = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			}
			bool flag = stageObject != null;
			float num = 0f;
			float num2 = 999f;
			bool flag2 = false;
			if (isBossExistsPast != flag)
			{
				isBossExistsPast = flag;
				switching = true;
				switchTimer = validSettings.modeSwitchTime;
				flag2 = true;
			}
			distanceElapsedTime += Time.get_deltaTime();
			if (distanceElapsedTime > validSettings.distanceLerpTime)
			{
				distanceElapsedTime = validSettings.distanceLerpTime;
			}
			modeChangeTime -= Time.get_deltaTime();
			if (modeChangeTime < 0f)
			{
				modeChangeTime = 0f;
			}
			float num3 = distanceElapsedTime / validSettings.distanceLerpTime;
			float num4 = 0f;
			fieldOfView = ingameFieldOfView;
			if (!isFixedCameraMode)
			{
				if (isMotionCameraMode)
				{
					num = 0f;
					if (motionCameraTransforms != null)
					{
						int num5 = 0;
						if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
						{
							num5 = ((!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? 1 : 0);
						}
						requestPos = motionCameraTransforms[num5].get_position();
						requestRot = motionCameraTransforms[num5].get_rotation();
						Vector3 localScale = motionCameraTransforms[num5].get_localScale();
						float num6 = localScale.x;
						if (num6 > 0f)
						{
							num6 = Utility.HorizontalToVerticalFOV(num6);
						}
						fieldOfView = num6;
					}
				}
				else if (!flag || !validSettings.targetEnable)
				{
					num = validSettings.smoothFreeRate;
					num4 = validSettings.freeDistance;
					num2 = validSettings.freeMaxSpeed;
					Vector3 back = Vector3.get_back();
					if (validSettings.normalEnable)
					{
						back = normalForward;
					}
					Vector3 val = cameraTargetPos;
					float num7 = 1f;
					Vector3 val2 = Vector3.Cross(back, Vector3.get_up());
					Vector3 val3 = Quaternion.AngleAxis(0f - validSettings.freePitch, val2) * back * Mathf.Lerp(validSettings.distanceLimit, num4, num3) * num7;
					Vector3 val4 = Quaternion.LookRotation(back) * validSettings.freeOffset * num7;
					Vector3 val5 = Vector3.get_zero();
					if (targetPlayer.actionID == Character.ACTION_ID.ATTACK && !targetPlayer.isArrowAimLesserMode && targetPlayer.attackStartTarget != null && targetPlayer.attackStartTarget == targetPlayer.actionTarget && MonoBehaviourSingleton<TargetMarkerManager>.I.isTargetLock)
					{
						Vector3 val6 = targetPlayer.attackStartTarget._position - targetPlayer._position;
						Quaternion val7 = Quaternion.get_identity();
						Vector3 val8 = val3;
						Vector3 val9 = val6 - val4;
						Vector3 zero = Vector3.get_zero();
						if (back != Vector3.get_forward())
						{
							val7 = Quaternion.FromToRotation(back, Vector3.get_forward());
							val8 = val7 * val8;
							val9 = val7 * val9;
						}
						if (val9.z < (0f - validSettings.followBackOffset) * num7)
						{
							zero.z = val9.z + validSettings.followBackOffset * num7;
						}
						Vector3 val10 = Quaternion.FromToRotation(val8, Vector3.get_forward()) * (val8 + val9);
						float num8 = Mathf.Tan(0.0174532924f * fieldOfView * 0.5f) * ((float)Screen.get_width() / (float)Screen.get_height());
						float num9 = Mathf.Abs(val10.x / val10.z);
						float num10 = num9 / num8;
						if (num10 > validSettings.followSidePercent)
						{
							zero.x = val9.x * (num10 - validSettings.followSidePercent) / num10;
						}
						if (back != Vector3.get_forward())
						{
							val5 = Quaternion.Inverse(val7) * zero;
						}
					}
					requestPos = cameraTargetPos + (beforeFollowVec = Vector3.Lerp(beforeFollowVec, val5, validSettings.followRate)) - val3 + val4;
					requestRot = Quaternion.LookRotation(val3.get_normalized());
					requestPos += Quaternion.LookRotation(normalForward) * this.validSettings.cameraFieldOffsetSettings.targetOffsetPos;
					requestRot *= Quaternion.Euler(this.validSettings.cameraFieldOffsetSettings.targetOffsetRot);
				}
				else
				{
					num = validSettings.smoothTargetingRate;
					num4 = validSettings.targetingDistance;
					num2 = validSettings.targetingMaxSpeed;
					Vector3 val = stageObject.GetCameraTargetPos();
					Vector3 val11 = val - requestPos;
					val11.y = 0f;
					val11.Normalize();
					Vector3 val12 = cameraTargetPos - requestPos;
					val12.y = 0f;
					val12.Normalize();
					if (flag2)
					{
						normalForward = val - cameraTargetPos;
						normalForward.y = 0f;
						normalForward.Normalize();
					}
					else
					{
						float num11 = Vector3.Angle(val11, val12);
						if (num11 > validSettings.moveableTargetAngle)
						{
							Vector3 val13 = Vector3.Cross(val12, val11);
							float num12 = (!(val13.y >= 0f)) ? (-1f) : 1f;
							normalForward = Quaternion.AngleAxis((num11 - validSettings.moveableTargetAngle) * num12, Vector3.get_up()) * val12;
						}
					}
					if (normalForward == Vector3.get_zero())
					{
						normalForward = Vector3.get_back();
					}
					float num13 = validSettings.targetingPitch;
					if (validSettings.targetingPitchNearDistance != 0f || validSettings.targetingPitchFarDistance != 0f)
					{
						Vector3 val14 = val - cameraTargetPos;
						float magnitude = val14.get_magnitude();
						float num14 = Mathf.Clamp01((magnitude - validSettings.targetingPitchNearDistance) / (validSettings.targetingPitchFarDistance - validSettings.targetingPitchNearDistance));
						num14 = validSettings.targetingPitchCurve.Evaluate(num14);
						num13 = validSettings.targetingPitchMinAngle * (1f - num14) + validSettings.targetingPitchMaxAngle * num14;
					}
					Vector3 val15 = Vector3.Cross(normalForward, Vector3.get_up());
					Vector3 val16 = Quaternion.AngleAxis(0f - num13, val15) * normalForward * Mathf.Lerp(validSettings.distanceLimit, num4, num3);
					Vector3 val17 = Quaternion.LookRotation(normalForward) * validSettings.targetingOffset;
					requestPos = cameraTargetPos - val16 + val17;
					requestRot = Quaternion.LookRotation(val16.get_normalized());
					requestPos += Quaternion.LookRotation(normalForward) * this.validSettings.cameraTargetOffsetSettings.targetOffsetPos;
					requestRot *= Quaternion.Euler(this.validSettings.cameraTargetOffsetSettings.targetOffsetRot);
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
				Vector3 val18 = requestPos - cameraTargetPos;
				RaycastHit val19 = default(RaycastHit);
				Ray val20 = default(Ray);
				val20._002Ector(cameraTargetPos, val18.get_normalized());
				if (Physics.Raycast(val20, ref val19, num4, 2359808) && hitObjectType == CAM_HIT_OBJ_TYPE.ZOOM)
				{
					float num15 = val19.get_distance();
					if (num15 < validSettings.distanceLimit)
					{
						num15 = validSettings.distanceLimit;
					}
					num15 -= validSettings.distanceLimit;
					float num16 = num4 - validSettings.distanceLimit;
					float num17 = 0f;
					if (num16 > 0f)
					{
						num17 = validSettings.distanceLerpTime * (num15 / num16);
					}
					if (modeChangeTime <= 0f)
					{
						distanceElapsedTime -= Time.get_deltaTime() * 5f;
						if (distanceElapsedTime < num17)
						{
							distanceElapsedTime = num17;
						}
					}
					else
					{
						distanceElapsedTime = num17;
					}
					num3 = distanceElapsedTime / validSettings.distanceLerpTime;
					requestPos = cameraTargetPos + val20.get_direction() * Mathf.Lerp(validSettings.distanceLimit, num4, num3);
				}
			}
			if (num != 0f && modeChangeTime <= 0f)
			{
				num *= ((!(num3 < 0.1f)) ? num3 : 0.1f);
			}
			if (switching)
			{
				switchTimer -= Time.get_deltaTime();
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
				Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, num, num2, Time.get_deltaTime());
				Vector3 zero2 = Vector3.get_zero();
				Vector3 eulerAngles = moveRotation.get_eulerAngles();
				Vector3 eulerAngles2 = requestRot.get_eulerAngles();
				zero2.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, num, 1000f, Time.get_deltaTime());
				zero2.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, num, 1000f, Time.get_deltaTime());
				zero2.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, num, 1000f, Time.get_deltaTime());
				Quaternion identity = Quaternion.get_identity();
				identity.set_eulerAngles(zero2);
				this.movePosition = movePosition2;
				this.moveRotation = identity;
				fieldOfView = Mathf.SmoothDamp(ctrlCamera.get_fieldOfView(), fieldOfView, ref fieldOfViewVelocity, num, 1000f, Time.get_deltaTime());
			}
			ctrlCamera.set_fieldOfView(fieldOfView);
		}
	}

	private void UpdateArrowAimBossModeCamera()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		float num = ingameFieldOfView;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		StageObject stageObject = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			stageObject = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		}
		bool flag = stageObject != null;
		if (isBossExistsPast != flag)
		{
			isBossExistsPast = flag;
			switching = true;
			switchTimer = validSettings.modeSwitchTime;
		}
		distanceElapsedTime += Time.get_deltaTime();
		if (distanceElapsedTime > validSettings.distanceLerpTime)
		{
			distanceElapsedTime = validSettings.distanceLerpTime;
		}
		modeChangeTime -= Time.get_deltaTime();
		if (modeChangeTime < 0f)
		{
			modeChangeTime = 0f;
		}
		Settings.ArrowAimSettings arrowAimSettings = validSettings.arrowAimSettings;
		float num2 = arrowAimSettings.smoothTargetingRate;
		float targetingMaxSpeed = validSettings.targetingMaxSpeed;
		float num3 = distanceElapsedTime / validSettings.distanceLerpTime;
		float targetingDistance = arrowAimSettings.targetingDistance;
		Vector3 arrowAimForward = targetSelf.arrowAimForward;
		normalForward = arrowAimForward.get_normalized();
		int arrowAimStartSign = targetSelf.arrowAimStartSign;
		Vector3 val = Vector3.Cross(normalForward, Vector3.get_up());
		Vector3 val2 = Quaternion.AngleAxis(0f - arrowAimSettings.targetingPitch, val) * normalForward * Mathf.Lerp(validSettings.distanceLimit, targetingDistance, num3);
		Vector3 val3 = arrowAimSettings.targetingOffset;
		if (arrowCameraMode == 1)
		{
			val3 = ((!targetSelf.IsAbleArrowSitShot()) ? ((arrowAimStartSign > 0) ? arrowAimSettings.targetingLeftOffset : arrowAimSettings.targetingOffset) : ((arrowAimStartSign > 0) ? arrowAimSettings.targetingAvoidShotLeftOffset : arrowAimSettings.targetingAvoidShotRightOffset));
		}
		Vector3 val4 = Quaternion.LookRotation(normalForward) * val3;
		requestPos = cameraTargetPos - val2 + val4;
		requestRot = Quaternion.LookRotation(val2.get_normalized());
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
			Vector3 val5 = requestPos - cameraTargetPos;
			RaycastHit val6 = default(RaycastHit);
			Ray val7 = default(Ray);
			val7._002Ector(cameraTargetPos, val5.get_normalized());
			if (Physics.Raycast(val7, ref val6, targetingDistance, 2359808) && hitObjectType == CAM_HIT_OBJ_TYPE.ZOOM)
			{
				float num4 = val6.get_distance();
				if (num4 < validSettings.distanceLimit)
				{
					num4 = validSettings.distanceLimit;
				}
				num4 -= validSettings.distanceLimit;
				float num5 = targetingDistance - validSettings.distanceLimit;
				float num6 = 0f;
				if (num5 > 0f)
				{
					num6 = validSettings.distanceLerpTime * (num4 / num5);
				}
				if (modeChangeTime <= 0f)
				{
					distanceElapsedTime -= Time.get_deltaTime() * 5f;
					if (distanceElapsedTime < num6)
					{
						distanceElapsedTime = num6;
					}
				}
				else
				{
					distanceElapsedTime = num6;
				}
				num3 = distanceElapsedTime / validSettings.distanceLerpTime;
				requestPos = cameraTargetPos + val7.get_direction() * Mathf.Lerp(validSettings.distanceLimit, targetingDistance, num3);
			}
		}
		if (num2 != 0f && modeChangeTime <= 0f)
		{
			num2 *= ((!(num3 < 0.1f)) ? num3 : 0.1f);
		}
		if (switching)
		{
			switchTimer -= Time.get_deltaTime();
			if (switchTimer <= 0f)
			{
				switching = false;
				switchTimer = 0f;
			}
			num2 = Mathf.Lerp(validSettings.modeSwitchTime, num2, 1f - switchTimer / validSettings.modeSwitchTime);
		}
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, num2, targetingMaxSpeed, Time.get_deltaTime());
		Vector3 zero = Vector3.get_zero();
		Vector3 eulerAngles = moveRotation.get_eulerAngles();
		Vector3 eulerAngles2 = requestRot.get_eulerAngles();
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, num2, 1000f, Time.get_deltaTime());
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, num2, 1000f, Time.get_deltaTime());
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, num2, 1000f, Time.get_deltaTime());
		Quaternion identity = Quaternion.get_identity();
		identity.set_eulerAngles(zero);
		this.movePosition = movePosition2;
		this.moveRotation = identity;
		num = Mathf.SmoothDamp(ctrlCamera.get_fieldOfView(), num, ref fieldOfViewVelocity, num2, 1000f, Time.get_deltaTime());
		ctrlCamera.set_fieldOfView(num);
	}

	private void UpdateGrabCamera()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = target.get_position();
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		normalForward = grabInfo.dir;
		Vector3 val = grabInfo.enemyRoot.get_rotation() * normalForward * grabInfo.distance;
		requestPos = position + val;
		requestRot = Quaternion.LookRotation(-val.get_normalized());
		float smoothTargetingRate = validSettings.smoothTargetingRate;
		float targetingDistance = validSettings.targetingDistance;
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, smoothTargetingRate, targetingDistance, Time.get_deltaTime());
		Vector3 zero = Vector3.get_zero();
		Vector3 eulerAngles = moveRotation.get_eulerAngles();
		Vector3 eulerAngles2 = requestRot.get_eulerAngles();
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, smoothTargetingRate, 1000f, Time.get_deltaTime());
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, smoothTargetingRate, 1000f, Time.get_deltaTime());
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, smoothTargetingRate, 1000f, Time.get_deltaTime());
		Quaternion identity = Quaternion.get_identity();
		identity.set_eulerAngles(zero);
		this.movePosition = movePosition2;
		this.moveRotation = identity;
	}

	private void UpdateCannonAimCamera()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Settings validSettings = this.validSettings;
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		normalForward = targetSelf.cannonAimForward.get_normalized();
		Vector3 val = Vector3.Cross(normalForward, Vector3.get_up());
		Vector3 val2 = Quaternion.AngleAxis(validSettings.cannonAimSettings.aimLookDownAngle, val) * normalForward * validSettings.cannonAimSettings.aimDistanceToSelf;
		Vector3 val3 = Quaternion.LookRotation(normalForward) * validSettings.cannonAimSettings.aimCameraOffset;
		movePosition = cameraTargetPos - val2 + val3;
		moveRotation = Quaternion.LookRotation(val2.get_normalized());
	}

	private void UpdateCannonBeamChargeCamera()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Settings validSettings = this.validSettings;
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		normalForward = targetSelf.cannonAimForward.get_normalized();
		Vector3 val = Vector3.Cross(normalForward, Vector3.get_up());
		Vector3 val2 = Quaternion.AngleAxis(validSettings.cannonAimSettings.beamChargeCameraLookDownAngle, val) * normalForward * validSettings.cannonAimSettings.beamChargeCameraDistanceToSelf;
		Vector3 val3 = Quaternion.LookRotation(normalForward) * validSettings.cannonAimSettings.beamChargeCameraOffset;
		movePosition = cameraTargetPos - val2 + val3;
		moveRotation = Quaternion.LookRotation(val2.get_normalized());
	}

	private void UpdateCannonBeamCamera()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Settings validSettings = this.validSettings;
		movePosition = validSettings.cannonAimSettings.beamCameraPosition;
		moveRotation = Quaternion.Euler(validSettings.cannonAimSettings.beamCameraRotationEular);
	}

	private void UpdateStopCamera()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		float smoothTargetingRate = validSettings.smoothTargetingRate;
		float targetingDistance = validSettings.targetingDistance;
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, stopPos, ref posVelocity, smoothTargetingRate, targetingDistance, Time.get_deltaTime());
		Vector3 zero = Vector3.get_zero();
		Vector3 eulerAngles = moveRotation.get_eulerAngles();
		Vector3 val = stopRotEular;
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, val.x, ref rotVelocity.x, smoothTargetingRate, 1000f, Time.get_deltaTime());
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, val.y, ref rotVelocity.y, smoothTargetingRate, 1000f, Time.get_deltaTime());
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, val.z, ref rotVelocity.z, smoothTargetingRate, 1000f, Time.get_deltaTime());
		Quaternion identity = Quaternion.get_identity();
		identity.set_eulerAngles(zero);
		this.movePosition = movePosition2;
		this.moveRotation = identity;
	}

	private void UpdateShake()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Vector3.get_zero();
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
				shakeParam.shakeTime += Time.get_deltaTime();
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
					val += Vector3.get_up() * (shakeParam.shakeLength * Mathf.Sin(shakeParam.shakeTime * 3.14159274f * 2f / num2));
					num++;
				}
			}
		}
		cameraTransform.set_position(movePosition + val);
		cameraTransform.set_rotation(moveRotation);
	}

	private void LateUpdate()
	{
		if (radialBlurFilter != null && radialBlurFilter.get_enabled())
		{
			UpdateRadialBlur();
		}
		if (!(target == null))
		{
			if (targetObject == null || targetObject._transform != target)
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
			default:
				UpdatePlayerCamera();
				break;
			}
			UpdateShake();
		}
	}

	public Vector3 WorldToScreenPoint(Vector3 pos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return ctrlCamera.WorldToScreenPoint(pos);
	}

	public Vector3 WorldToViewportPoint(Vector3 pos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return ctrlCamera.WorldToViewportPoint(pos);
	}

	public Vector3 ScreenToWorldPoint(Vector3 pos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return ctrlCamera.ScreenToWorldPoint(pos);
	}

	public float GetPixelHeight()
	{
		return (float)ctrlCamera.get_pixelHeight();
	}

	public void AdjustCameraPosition()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		adjustCamera = true;
		posVelocity = Vector3.get_zero();
		rotVelocity = Vector3.get_zero();
		fieldOfViewVelocity = 0f;
		switching = false;
		switchTimer = 0f;
		modeChangeTime = 0f;
	}

	public void SetShakeCamera(Vector3 pos, float percent, float cycle_time = 0f)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = pos - movePosition;
		float magnitude = val.get_magnitude();
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
	}

	public bool IsEndMotionCamera()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!isMotionCameraMode || motionCameraTransforms == null)
		{
			return true;
		}
		int num = 0;
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			num = ((!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? 1 : 0);
		}
		Animation component = motionCameraTransforms[num].get_gameObject().GetComponent<Animation>();
		if (component == null)
		{
			return true;
		}
		return !component.get_isPlaying();
	}

	public void OnHappenQuestDirection(bool enable, Transform boss_transform = null, Object[] cameras = null, Vector3[] camera_offsets = null)
	{
		if (enable)
		{
			EndRadialBlurFilter(0f);
			int mainCameraCullingMask = 262144;
			MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(mainCameraCullingMask);
			if (boss_transform != null && cameras != null)
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

	public void SetMotionCamera(bool enable, Transform target_transform = null, Object[] cameras = null, Vector3[] camera_offsets = null)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			motionCameraTransforms = (Transform[])new Transform[2];
			Transform val = Utility.CreateGameObject("FieldQuestCamera", base._transform, -1);
			val.set_position(target_transform.get_position());
			val.set_rotation(target_transform.get_rotation());
			Vector3 one = Vector3.get_one();
			Vector3 lossyScale = target_transform.get_lossyScale();
			float x = lossyScale.x;
			Vector3 lossyScale2 = base._transform.get_lossyScale();
			one.x = x / lossyScale2.x;
			Vector3 lossyScale3 = target_transform.get_lossyScale();
			float y = lossyScale3.y;
			Vector3 lossyScale4 = base._transform.get_lossyScale();
			one.y = y / lossyScale4.y;
			Vector3 lossyScale5 = target_transform.get_lossyScale();
			float z = lossyScale5.z;
			Vector3 lossyScale6 = base._transform.get_lossyScale();
			one.z = z / lossyScale6.z;
			val.set_localScale(one);
			motionCameraParent = val;
			for (int i = 0; i < 2; i++)
			{
				Transform val2 = Utility.CreateGameObject("offset_" + i.ToString(), val, -1);
				val2.set_localPosition(Vector3.get_zero());
				val2.set_localRotation(Quaternion.get_identity());
				val2.set_localScale(Vector3.get_one());
				if (camera_offsets != null)
				{
					val2.set_localPosition(camera_offsets[i]);
				}
				Transform val3 = ResourceUtility.Realizes(cameras[i], val2, -1);
				if (val3 == null)
				{
					Object.Destroy(val.get_gameObject());
					return;
				}
				val3.set_localPosition(Vector3.get_zero());
				val3.set_localRotation(Quaternion.get_identity());
				val3.set_localScale(Vector3.get_zero());
				motionCameraTransforms[i] = val3;
			}
			isMotionCameraMode = true;
		}
		else
		{
			isMotionCameraMode = false;
			if (motionCameraParent != null)
			{
				Object.Destroy(motionCameraParent.get_gameObject());
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
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (!(radialBlurFilter == null))
		{
			_StartRadialBlurFilter(time, strength);
			radialBlurCenterPos = center_pos;
			radialBlurCenterTransform = null;
		}
	}

	public void StartRadialBlurFilter(float time, float strength, Transform center_transform)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (!(radialBlurFilter == null))
		{
			_StartRadialBlurFilter(time, strength);
			radialBlurCenterPos = Vector3.get_zero();
			radialBlurCenterTransform = center_transform;
		}
	}

	private void _StartRadialBlurFilter(float time, float strength)
	{
		if (!(radialBlurFilter == null))
		{
			radialBlurFilter.set_enabled(true);
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
		if (!(radialBlurFilter == null) && radialBlurFilter.get_enabled())
		{
			if (time <= 0f)
			{
				radialBlurStrengthValue = strength;
				if (strength <= 0f)
				{
					radialBlurFilter.strength = 0f;
					radialBlurFilter.StopFilter();
					radialBlurFilter.set_enabled(false);
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		movePosition = ctrlCamera.get_transform().get_position();
		moveRotation = ctrlCamera.get_transform().get_rotation();
	}
}

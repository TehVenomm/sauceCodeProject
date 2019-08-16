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

		[Tooltip("タ\u30fcゲットカメラ\u3000カメラ回転最大速度")]
		public float targetingMaxRotateSpeed = 999f;

		[Tooltip("フリ\u30fcカメラ ピッチ")]
		public float freePitch = 20f;

		[Tooltip("フリ\u30fcカメラ 距離")]
		public float freeDistance = 10f;

		[Tooltip("フリ\u30fcカメラ オフセット")]
		public Vector3 freeOffset = Vector3.get_zero();

		[Tooltip("フリ\u30fcカメラ カメラ移動最大速度")]
		public float freeMaxSpeed = 999f;

		[Tooltip("フリ\u30fcカメラ カメラ回転最大速度")]
		public float freeMaxRotateSpeed = 999f;

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

		public float smoothMaxSpeed;
	}

	public class TargetOffset
	{
		public Vector3 pos = Vector3.get_zero();

		public Vector3 rot = Vector3.get_zero();

		public float smoothMaxSpeed;
	}

	public class TargetPosition
	{
		public Vector3 pos = Vector3.get_zero();

		public float smoothMaxSpeed;
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

	private float stopMaxSpeed;

	private float stopMaxRotSpeed;

	private Vector3 cutPos = Vector3.get_zero();

	private Quaternion cutRot = Quaternion.get_identity();

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

	private TargetPosition validAnimEventTargetPosition;

	private TargetPosition targetPositionByPlayer;

	private TargetPosition targetPositionByEnemy;

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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
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

	public void SetStopMaxSpeed(float speed)
	{
		stopMaxSpeed = speed;
	}

	public void SetStopMaxRotSpeed(float speed)
	{
		stopMaxRotSpeed = speed;
	}

	public void SetCutPos(Vector3 cutPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		this.cutPos = cutPos;
	}

	public void SetCutRot(Quaternion cutRot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
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

	public void SetAnimEventTargetPositionByPlayer(TargetPosition targetPosition)
	{
		if (targetPosition != null)
		{
			targetPositionByPlayer = targetPosition;
		}
	}

	public void SetAnimEventTargetPositionByEnemy(TargetPosition targetPosition)
	{
		if (targetPosition != null)
		{
			targetPositionByEnemy = targetPosition;
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

	public void ClearAnimEventTargetPositionByPlayer()
	{
		validAnimEventTargetPosition = null;
		targetPositionByPlayer = null;
	}

	public void ClearAnimEventTargetPositionByEnemy()
	{
		validAnimEventTargetPosition = null;
		targetPositionByEnemy = null;
	}

	public void ClearCameraMode(CAMERA_MODE mode = CAMERA_MODE.DEFAULT)
	{
		if (mode == CAMERA_MODE.DEFAULT || IsCameraMode(mode))
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
		EndRadialBlurFilter();
		MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(GameSceneGlobalSettings.GetDefaultMainCameraCullingMask());
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if (component != null)
		{
			component.set_enabled(true);
		}
	}

	private void Start()
	{
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
		if (radialBlurStrengthPerTime == 0f)
		{
			return;
		}
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
			return;
		}
		if (strength <= radialBlurStrengthValue)
		{
			strength = radialBlurStrengthValue;
			if (strength <= 0f)
			{
				EndRadialBlurFilter();
			}
		}
		radialBlurFilter.strength = strength;
	}

	private void UpdatePlayerCamera()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		Settings validSettings = this.validSettings;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		float fieldOfView = ctrlCamera.get_fieldOfView();
		Vector3 cameraTargetPos = targetObject.GetCameraTargetPos();
		if (targetPlayer == null)
		{
			return;
		}
		Enemy enemy = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		}
		bool flag = enemy != null && !enemy.enableAssimilation;
		float num = 0f;
		float num2 = 999f;
		float num3 = 999f;
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
		float num4 = distanceElapsedTime / validSettings.distanceLerpTime;
		float num5 = 0f;
		fieldOfView = ingameFieldOfView;
		if (!isFixedCameraMode)
		{
			if (isMotionCameraMode)
			{
				num = 0f;
				if (motionCameraTransforms != null)
				{
					int num6 = 0;
					if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
					{
						num6 = ((!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? 1 : 0);
					}
					requestPos = motionCameraTransforms[num6].get_position();
					requestRot = motionCameraTransforms[num6].get_rotation();
					Vector3 localScale = motionCameraTransforms[num6].get_localScale();
					float num7 = localScale.x;
					if (num7 > 0f)
					{
						num7 = Utility.HorizontalToVerticalFOV(num7);
					}
					fieldOfView = num7;
				}
			}
			else if (!flag || !validSettings.targetEnable)
			{
				num = validSettings.smoothFreeRate;
				num5 = validSettings.freeDistance;
				num2 = validSettings.freeMaxSpeed;
				num3 = validSettings.freeMaxRotateSpeed;
				Vector3 back = Vector3.get_back();
				if (validSettings.normalEnable)
				{
					back = normalForward;
				}
				Vector3 val = cameraTargetPos;
				float num8 = 1f;
				Vector3 val2 = Vector3.Cross(back, Vector3.get_up());
				Vector3 val3 = Quaternion.AngleAxis(0f - validSettings.freePitch, val2) * back * Mathf.Lerp(validSettings.distanceLimit, num5, num4) * num8;
				Vector3 val4 = Quaternion.LookRotation(back) * validSettings.freeOffset * num8;
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
					if (val9.z < (0f - validSettings.followBackOffset) * num8)
					{
						zero.z = val9.z + validSettings.followBackOffset * num8;
					}
					Vector3 val10 = Quaternion.FromToRotation(val8, Vector3.get_forward()) * (val8 + val9);
					float num9 = Mathf.Tan((float)Math.PI / 180f * fieldOfView * 0.5f) * ((float)Screen.get_width() / (float)Screen.get_height());
					float num10 = Mathf.Abs(val10.x / val10.z);
					float num11 = num10 / num9;
					if (num11 > validSettings.followSidePercent)
					{
						zero.x = val9.x * (num11 - validSettings.followSidePercent) / num11;
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
				if (MonoBehaviourSingleton<FieldManager>.IsValid())
				{
					requestPos += Quaternion.LookRotation(normalForward) * MonoBehaviourSingleton<FieldManager>.I.cameraOffsetPos_Vec;
					requestRot *= MonoBehaviourSingleton<FieldManager>.I.cameraOffsetRot_Quat;
				}
			}
			else
			{
				num = validSettings.smoothTargetingRate;
				num5 = validSettings.targetingDistance;
				num2 = validSettings.targetingMaxSpeed;
				num3 = validSettings.targetingMaxRotateSpeed;
				Vector3 val = enemy.GetCameraTargetPos();
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
					float num12 = Vector3.Angle(val11, val12);
					if (num12 > validSettings.moveableTargetAngle)
					{
						Vector3 val13 = Vector3.Cross(val12, val11);
						float num13 = (!(val13.y >= 0f)) ? (-1f) : 1f;
						normalForward = Quaternion.AngleAxis((num12 - validSettings.moveableTargetAngle) * num13, Vector3.get_up()) * val12;
					}
				}
				if (normalForward == Vector3.get_zero())
				{
					normalForward = Vector3.get_back();
				}
				float num14 = validSettings.targetingPitch;
				if (validSettings.targetingPitchNearDistance != 0f || validSettings.targetingPitchFarDistance != 0f)
				{
					Vector3 val14 = val - cameraTargetPos;
					float magnitude = val14.get_magnitude();
					float num15 = Mathf.Clamp01((magnitude - validSettings.targetingPitchNearDistance) / (validSettings.targetingPitchFarDistance - validSettings.targetingPitchNearDistance));
					num15 = validSettings.targetingPitchCurve.Evaluate(num15);
					num14 = validSettings.targetingPitchMinAngle * (1f - num15) + validSettings.targetingPitchMaxAngle * num15;
				}
				Vector3 val15 = Vector3.Cross(normalForward, Vector3.get_up());
				Vector3 val16 = Quaternion.AngleAxis(0f - num14, val15) * normalForward * Mathf.Lerp(validSettings.distanceLimit, num5, num4);
				Vector3 val17 = Quaternion.LookRotation(normalForward) * validSettings.targetingOffset;
				requestPos = cameraTargetPos - val16 + val17;
				requestRot = Quaternion.LookRotation(val16.get_normalized());
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
					num2 = ((!(validAnimEventTargetOffset.smoothMaxSpeed > 0f)) ? num2 : validAnimEventTargetOffset.smoothMaxSpeed);
				}
				if (targetPositionByPlayer != null)
				{
					validAnimEventTargetPosition = targetPositionByPlayer;
				}
				else if (targetPositionByEnemy != null)
				{
					validAnimEventTargetPosition = targetPositionByEnemy;
				}
				else
				{
					validAnimEventTargetPosition = null;
				}
				if (validAnimEventTargetPosition != null)
				{
					requestRot = Quaternion.LookRotation(validAnimEventTargetPosition.pos - cameraTransform.get_position());
					num3 = ((!(validAnimEventTargetPosition.smoothMaxSpeed > 0f)) ? num3 : validAnimEventTargetPosition.smoothMaxSpeed);
				}
			}
		}
		if (hitObjectType != 0 && !isMotionCameraMode)
		{
			Vector3 val18 = requestPos - cameraTargetPos;
			RaycastHit val19 = default(RaycastHit);
			Ray val20 = default(Ray);
			val20._002Ector(cameraTargetPos, val18.get_normalized());
			if (Physics.Raycast(val20, ref val19, num5, 2359808) && hitObjectType == CAM_HIT_OBJ_TYPE.ZOOM)
			{
				float num16 = val19.get_distance();
				if (num16 < validSettings.distanceLimit)
				{
					num16 = validSettings.distanceLimit;
				}
				num16 -= validSettings.distanceLimit;
				float num17 = num5 - validSettings.distanceLimit;
				float num18 = 0f;
				if (num17 > 0f)
				{
					num18 = validSettings.distanceLerpTime * (num16 / num17);
				}
				if (modeChangeTime <= 0f)
				{
					distanceElapsedTime -= Time.get_deltaTime() * 5f;
					if (distanceElapsedTime < num18)
					{
						distanceElapsedTime = num18;
					}
				}
				else
				{
					distanceElapsedTime = num18;
				}
				num4 = distanceElapsedTime / validSettings.distanceLerpTime;
				requestPos = cameraTargetPos + val20.get_direction() * Mathf.Lerp(validSettings.distanceLimit, num5, num4);
			}
		}
		if (num != 0f && modeChangeTime <= 0f)
		{
			num *= ((!(num4 < 0.1f)) ? num4 : 0.1f);
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
			zero2.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref rotVelocity.x, num, num3, Time.get_deltaTime());
			zero2.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref rotVelocity.y, num, num3, Time.get_deltaTime());
			zero2.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref rotVelocity.z, num, num3, Time.get_deltaTime());
			Quaternion identity = Quaternion.get_identity();
			identity.set_eulerAngles(zero2);
			this.movePosition = movePosition2;
			this.moveRotation = identity;
			fieldOfView = Mathf.SmoothDamp(ctrlCamera.get_fieldOfView(), fieldOfView, ref fieldOfViewVelocity, num, num3, Time.get_deltaTime());
		}
		ctrlCamera.set_fieldOfView(fieldOfView);
	}

	private void UpdateArrowAimBossModeCamera()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		Vector3 cameraTargetPos = targetSelf.GetCameraTargetPos();
		float num = ingameFieldOfView;
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		Enemy enemy = null;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		}
		bool flag = enemy != null && !enemy.enableAssimilation;
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
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = target.get_position();
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		normalForward = grabInfo.dir;
		Vector3 val = grabInfo.enemyRoot.get_rotation() * normalForward * grabInfo.distance;
		requestPos = position + val;
		requestRot = Quaternion.LookRotation(-val.get_normalized());
		float smoothTargetingRate = validSettings.smoothTargetingRate;
		float num = (!(grabInfo.smoothMaxSpeed > 0f)) ? validSettings.targetingDistance : grabInfo.smoothMaxSpeed;
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, requestPos, ref posVelocity, smoothTargetingRate, num, Time.get_deltaTime());
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
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		Vector3 movePosition = this.movePosition;
		Quaternion moveRotation = this.moveRotation;
		float smoothTargetingRate = validSettings.smoothTargetingRate;
		float targetingDistance = validSettings.targetingDistance;
		float num = 1000f;
		if (stopMaxSpeed > 0f)
		{
			targetingDistance = stopMaxSpeed;
			num = stopMaxSpeed * (num / validSettings.targetingDistance);
		}
		if (stopMaxRotSpeed > 0f)
		{
			num = stopMaxRotSpeed;
		}
		Vector3 movePosition2 = Vector3.SmoothDamp(movePosition, stopPos, ref posVelocity, smoothTargetingRate, targetingDistance, Time.get_deltaTime());
		Vector3 zero = Vector3.get_zero();
		Vector3 eulerAngles = moveRotation.get_eulerAngles();
		Vector3 val = stopRotEular;
		zero.x = Mathf.SmoothDampAngle(eulerAngles.x, val.x, ref rotVelocity.x, smoothTargetingRate, num, Time.get_deltaTime());
		zero.y = Mathf.SmoothDampAngle(eulerAngles.y, val.y, ref rotVelocity.y, smoothTargetingRate, num, Time.get_deltaTime());
		zero.z = Mathf.SmoothDampAngle(eulerAngles.z, val.z, ref rotVelocity.z, smoothTargetingRate, num, Time.get_deltaTime());
		Quaternion identity = Quaternion.get_identity();
		identity.set_eulerAngles(zero);
		this.movePosition = movePosition2;
		this.moveRotation = identity;
	}

	private void UpdateCutCamera()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		movePosition = cutPos;
		moveRotation = cutRot;
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
				continue;
			}
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
				continue;
			}
			val += Vector3.get_up() * (shakeParam.shakeLength * Mathf.Sin(shakeParam.shakeTime * (float)Math.PI * 2f / num2));
			num++;
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
		return ctrlCamera.get_pixelHeight();
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
		if (QuestManager.IsValidInGameWaveMatch() && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
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
			EndRadialBlurFilter();
			int mainCameraCullingMask = 262144;
			MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(mainCameraCullingMask);
			if (boss_transform != null && cameras != null)
			{
				SetMotionCamera(enable: true, boss_transform, cameras, camera_offsets);
			}
			if (MonoBehaviourSingleton<AudioListenerManager>.IsValid())
			{
				MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, isEnable: true);
			}
		}
		else
		{
			SetMotionCamera(enable: false);
			MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(GameSceneGlobalSettings.GetDefaultMainCameraCullingMask());
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
			{
				OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
			}
			if (MonoBehaviourSingleton<AudioListenerManager>.IsValid())
			{
				MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, isEnable: false);
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
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			motionCameraTransforms = (Transform[])new Transform[2];
			Transform val = Utility.CreateGameObject("FieldQuestCamera", base._transform);
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
				Transform val2 = Utility.CreateGameObject("offset_" + i.ToString(), val);
				val2.set_localPosition(Vector3.get_zero());
				val2.set_localRotation(Quaternion.get_identity());
				val2.set_localScale(Vector3.get_one());
				if (camera_offsets != null)
				{
					val2.set_localPosition(camera_offsets[i]);
				}
				Transform val3 = ResourceUtility.Realizes(cameras[i], val2);
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
		if (!(radialBlurFilter == null) && !radialBlurFilter.get_enabled())
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
		if (radialBlurFilter == null || !radialBlurFilter.get_enabled())
		{
			return;
		}
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

	public void EndRadialBlurFilter(float time = 0f)
	{
		ChangeRadialBlurFilter(time, 0f);
	}

	public void ResetMovePositionAndRotaion()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		movePosition = ctrlCamera.get_transform().get_position();
		moveRotation = ctrlCamera.get_transform().get_rotation();
	}
}

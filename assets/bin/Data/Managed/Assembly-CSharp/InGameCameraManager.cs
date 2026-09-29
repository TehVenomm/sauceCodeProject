// Decompiled with JetBrains decompiler
// Type: InGameCameraManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameCameraManager : MonoBehaviourSingleton<InGameCameraManager>
{
  private Camera ctrlCamera;
  public Transform cameraTransform;
  private StageObject targetObject;
  private Player targetPlayer;
  private Self targetSelf;
  private InGameCameraManager.CAMERA_MODE cameraMode;
  [Tooltip("縦画面設定")]
  public InGameCameraManager.Settings portraitSettings = new InGameCameraManager.Settings();
  [Tooltip("横画面設定")]
  public InGameCameraManager.Settings landscapeSettings = new InGameCameraManager.Settings();
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
  private float stopMaxSpeed;
  private float stopMaxRotSpeed;
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
  protected List<InGameCameraManager.ShakeParam> shakeParams = new List<InGameCameraManager.ShakeParam>();
  [Tooltip("カメラがオブジェクトに当たった際の挙動")]
  public InGameCameraManager.CAM_HIT_OBJ_TYPE hitObjectType;
  private RadialBlurFilter radialBlurFilter;
  private float radialBlurStrengthPerTime;
  private float radialBlurStrengthValue;
  private Transform radialBlurCenterTransform;
  private Vector3 radialBlurCenterPos = Vector3.zero;
  private Vector3 beforeFollowVec = Vector3.zero;
  private InGameCameraManager.GrabInfo _grabInfo = new InGameCameraManager.GrabInfo();
  private InGameCameraManager.TargetOffset validAnimEventTargetOffset;
  private InGameCameraManager.TargetOffset targetOffsetByPlayer;
  private InGameCameraManager.TargetOffset targetOffsetByEnemy;
  private InGameCameraManager.TargetPosition validAnimEventTargetPosition;
  private InGameCameraManager.TargetPosition targetPositionByPlayer;
  private InGameCameraManager.TargetPosition targetPositionByEnemy;

  public Vector3 movePosition { get; protected set; }

  public Quaternion moveRotation { get; protected set; }

  public Transform target { set; get; }

  public void SetCameraMode(InGameCameraManager.CAMERA_MODE cameraMode)
  {
    this.cameraMode = cameraMode;
  }

  public bool IsCameraModeBeam() => this.cameraMode == InGameCameraManager.CAMERA_MODE.CANNON_BEAM;

  public bool IsCameraMode(InGameCameraManager.CAMERA_MODE mode) => this.cameraMode == mode;

  public InGameCameraManager.Settings validSettings { get; private set; }

  public void SetStopPos(Vector3 pos) => this.stopPos = pos;

  public void SetStopRotEular(Vector3 rotEular) => this.stopRotEular = rotEular;

  public void SetStopMaxSpeed(float speed) => this.stopMaxSpeed = speed;

  public void SetStopMaxRotSpeed(float speed) => this.stopMaxRotSpeed = speed;

  public void SetCutPos(Vector3 cutPos) => this.cutPos = cutPos;

  public void SetCutRot(Quaternion cutRot) => this.cutRot = cutRot;

  public int arrowCameraMode { get; private set; }

  public void SetArrowCameraMode(int mode) => this.arrowCameraMode = mode;

  public bool isArrowAimBossMode { get; protected set; }

  public bool isMotionCameraMode { get; protected set; }

  public bool isFixedCameraMode { get; protected set; }

  public Transform[] motionCameraTransforms { get; protected set; }

  public Transform motionCameraParent { get; protected set; }

  public InGameCameraManager.GrabInfo grabInfo => this._grabInfo;

  public void SetAnimEventTargetOffsetByPlayer(InGameCameraManager.TargetOffset targetOffset)
  {
    if (targetOffset == null)
      return;
    this.targetOffsetByPlayer = targetOffset;
  }

  public void SetAnimEventTargetOffsetByEnemy(InGameCameraManager.TargetOffset targetOffset)
  {
    if (targetOffset == null)
      return;
    this.targetOffsetByEnemy = targetOffset;
  }

  public void SetAnimEventTargetPositionByPlayer(InGameCameraManager.TargetPosition targetPosition)
  {
    if (targetPosition == null)
      return;
    this.targetPositionByPlayer = targetPosition;
  }

  public void SetAnimEventTargetPositionByEnemy(InGameCameraManager.TargetPosition targetPosition)
  {
    if (targetPosition == null)
      return;
    this.targetPositionByEnemy = targetPosition;
  }

  public void ClearAnimEventTargetOffsetByPlayer()
  {
    this.validAnimEventTargetOffset = (InGameCameraManager.TargetOffset) null;
    this.targetOffsetByPlayer = (InGameCameraManager.TargetOffset) null;
  }

  public void ClearAnimEventTargetOffsetByEnemy()
  {
    this.validAnimEventTargetOffset = (InGameCameraManager.TargetOffset) null;
    this.targetOffsetByEnemy = (InGameCameraManager.TargetOffset) null;
  }

  public void ClearAnimEventTargetPositionByPlayer()
  {
    this.validAnimEventTargetPosition = (InGameCameraManager.TargetPosition) null;
    this.targetPositionByPlayer = (InGameCameraManager.TargetPosition) null;
  }

  public void ClearAnimEventTargetPositionByEnemy()
  {
    this.validAnimEventTargetPosition = (InGameCameraManager.TargetPosition) null;
    this.targetPositionByEnemy = (InGameCameraManager.TargetPosition) null;
  }

  public void ClearCameraMode(InGameCameraManager.CAMERA_MODE mode = InGameCameraManager.CAMERA_MODE.DEFAULT)
  {
    if (mode != InGameCameraManager.CAMERA_MODE.DEFAULT && !this.IsCameraMode(mode))
      return;
    this.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
  }

  public InGameCameraManager()
  {
    this.isMotionCameraMode = false;
    this.motionCameraTransforms = (Transform[]) null;
    this.motionCameraParent = (Transform) null;
    this.cameraMode = InGameCameraManager.CAMERA_MODE.DEFAULT;
    this.arrowCameraMode = InGameManager.GetArrowCameraType(GameSaveData.instance.arrowCameraKey);
  }

  protected override void Awake()
  {
    base.Awake();
    this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.distanceElapsedTime = this.validSettings.distanceLerpTime;
  }

  private void OnEnable()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    RenderTargetCacher component = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    ((Behaviour) component).enabled = false;
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.EndRadialBlurFilter();
    MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(GameSceneGlobalSettings.GetDefaultMainCameraCullingMask());
    RenderTargetCacher component = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    ((Behaviour) component).enabled = true;
  }

  private void Start()
  {
    if (Object.op_Equality((Object) this.ctrlCamera, (Object) null))
      this.ctrlCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    this.cameraTransform = ((Component) this.ctrlCamera).transform;
    this.movePosition = this.cameraTransform.position;
    this.moveRotation = this.cameraTransform.rotation;
    this.radialBlurFilter = ((Component) this.ctrlCamera).GetComponent<RadialBlurFilter>();
    if (Object.op_Inequality((Object) this.radialBlurFilter, (Object) null))
      ((Behaviour) this.radialBlurFilter).enabled = false;
    if (!Object.op_Equality((Object) ((Component) this.ctrlCamera).gameObject.GetComponent<InGameCameraCuller>(), (Object) null))
      return;
    ((Component) this.ctrlCamera).gameObject.AddComponent<InGameCameraCuller>();
  }

  private void UpdateRadialBlur()
  {
    Vector3 zero = Vector3.zero;
    Vector2 vector2Xy = this.WorldToScreenPoint(!Object.op_Inequality((Object) this.radialBlurCenterTransform, (Object) null) ? this.radialBlurCenterPos : this.radialBlurCenterTransform.position).ToVector2XY();
    vector2Xy.x /= (float) Screen.width;
    vector2Xy.y /= (float) Screen.height;
    this.radialBlurFilter.SetCenter(vector2Xy);
    if ((double) this.radialBlurStrengthPerTime == 0.0)
      return;
    float num = this.radialBlurFilter.strength + this.radialBlurStrengthPerTime * Time.deltaTime;
    if ((double) this.radialBlurStrengthPerTime > 0.0)
    {
      if ((double) num >= (double) this.radialBlurStrengthValue)
      {
        num = this.radialBlurStrengthValue;
        this.radialBlurStrengthPerTime = 0.0f;
      }
      this.radialBlurFilter.strength = num;
    }
    else
    {
      if ((double) num <= (double) this.radialBlurStrengthValue)
      {
        num = this.radialBlurStrengthValue;
        if ((double) num <= 0.0)
          this.EndRadialBlurFilter();
      }
      this.radialBlurFilter.strength = num;
    }
  }

  private void UpdatePlayerCamera()
  {
    InGameCameraManager.Settings validSettings = this.validSettings;
    Vector3 movePosition = this.movePosition;
    Quaternion moveRotation = this.moveRotation;
    float fieldOfView = this.ctrlCamera.fieldOfView;
    Vector3 cameraTargetPos1 = this.targetObject.GetCameraTargetPos();
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null))
      return;
    Enemy enemy = (Enemy) null;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    bool flag1 = Object.op_Inequality((Object) enemy, (Object) null) && !enemy.enableAssimilation;
    float num1 = 0.0f;
    float num2 = 999f;
    float num3 = 999f;
    bool flag2 = false;
    if (this.isBossExistsPast != flag1)
    {
      this.isBossExistsPast = flag1;
      this.switching = true;
      this.switchTimer = validSettings.modeSwitchTime;
      flag2 = true;
    }
    this.distanceElapsedTime += Time.deltaTime;
    if ((double) this.distanceElapsedTime > (double) validSettings.distanceLerpTime)
      this.distanceElapsedTime = validSettings.distanceLerpTime;
    this.modeChangeTime -= Time.deltaTime;
    if ((double) this.modeChangeTime < 0.0)
      this.modeChangeTime = 0.0f;
    float num4 = this.distanceElapsedTime / validSettings.distanceLerpTime;
    float num5 = 0.0f;
    float num6 = this.ingameFieldOfView;
    if (!this.isFixedCameraMode)
    {
      if (this.isMotionCameraMode)
      {
        num1 = 0.0f;
        if (this.motionCameraTransforms != null)
        {
          int index = 0;
          if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
            index = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? 0 : 1;
          this.requestPos = this.motionCameraTransforms[index].position;
          this.requestRot = this.motionCameraTransforms[index].rotation;
          float Horizontal_fov = this.motionCameraTransforms[index].localScale.x;
          if ((double) Horizontal_fov > 0.0)
            Horizontal_fov = Utility.HorizontalToVerticalFOV(Horizontal_fov);
          num6 = Horizontal_fov;
        }
      }
      else if (!flag1 || !validSettings.targetEnable)
      {
        num1 = validSettings.smoothFreeRate;
        num5 = validSettings.freeDistance;
        num2 = validSettings.freeMaxSpeed;
        num3 = validSettings.freeMaxRotateSpeed;
        Vector3 vector3_1 = Vector3.back;
        if (validSettings.normalEnable)
          vector3_1 = this.normalForward;
        float num7 = 1f;
        Vector3 vector3_2 = Vector3.Cross(vector3_1, Vector3.up);
        Vector3 vector3_3 = Vector3.op_Multiply(Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(-validSettings.freePitch, vector3_2), vector3_1), Mathf.Lerp(validSettings.distanceLimit, num5, num4)), num7);
        Vector3 vector3_4 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.LookRotation(vector3_1), validSettings.freeOffset), num7);
        Vector3 vector3_5 = Vector3.zero;
        if (this.targetPlayer.actionID == Character.ACTION_ID.ATTACK && !this.targetPlayer.isArrowAimLesserMode && Object.op_Inequality((Object) this.targetPlayer.attackStartTarget, (Object) null) && Object.op_Equality((Object) this.targetPlayer.attackStartTarget, (Object) this.targetPlayer.actionTarget) && MonoBehaviourSingleton<TargetMarkerManager>.I.isTargetLock)
        {
          Vector3 vector3_6 = Vector3.op_Subtraction(this.targetPlayer.attackStartTarget._position, this.targetPlayer._position);
          Quaternion quaternion = Quaternion.identity;
          Vector3 vector3_7 = vector3_3;
          Vector3 vector3_8 = vector3_4;
          Vector3 vector3_9 = Vector3.op_Subtraction(vector3_6, vector3_8);
          Vector3 zero = Vector3.zero;
          if (Vector3.op_Inequality(vector3_1, Vector3.forward))
          {
            quaternion = Quaternion.FromToRotation(vector3_1, Vector3.forward);
            vector3_7 = Quaternion.op_Multiply(quaternion, vector3_7);
            vector3_9 = Quaternion.op_Multiply(quaternion, vector3_9);
          }
          if ((double) vector3_9.z < -(double) validSettings.followBackOffset * (double) num7)
            zero.z = vector3_9.z + validSettings.followBackOffset * num7;
          Vector3 vector3_10 = Quaternion.op_Multiply(Quaternion.FromToRotation(vector3_7, Vector3.forward), Vector3.op_Addition(vector3_7, vector3_9));
          float num8 = Mathf.Tan((float) (Math.PI / 180.0 * (double) num6 * 0.5)) * ((float) Screen.width / (float) Screen.height);
          float num9 = Mathf.Abs(vector3_10.x / vector3_10.z) / num8;
          if ((double) num9 > (double) validSettings.followSidePercent)
            zero.x = vector3_9.x * (num9 - validSettings.followSidePercent) / num9;
          if (Vector3.op_Inequality(vector3_1, Vector3.forward))
            vector3_5 = Quaternion.op_Multiply(Quaternion.Inverse(quaternion), zero);
        }
        Vector3 vector3_11 = Vector3.Lerp(this.beforeFollowVec, vector3_5, validSettings.followRate);
        this.beforeFollowVec = vector3_11;
        this.requestPos = Vector3.op_Addition(Vector3.op_Subtraction(Vector3.op_Addition(cameraTargetPos1, vector3_11), vector3_3), vector3_4);
        this.requestRot = Quaternion.LookRotation(((Vector3) ref vector3_3).normalized);
        this.requestPos = Vector3.op_Addition(this.requestPos, Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), this.validSettings.cameraFieldOffsetSettings.targetOffsetPos));
        this.requestRot = Quaternion.op_Multiply(this.requestRot, Quaternion.Euler(this.validSettings.cameraFieldOffsetSettings.targetOffsetRot));
        if (MonoBehaviourSingleton<FieldManager>.IsValid())
        {
          this.requestPos = Vector3.op_Addition(this.requestPos, Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), MonoBehaviourSingleton<FieldManager>.I.cameraOffsetPos_Vec));
          this.requestRot = Quaternion.op_Multiply(this.requestRot, MonoBehaviourSingleton<FieldManager>.I.cameraOffsetRot_Quat);
        }
      }
      else
      {
        num1 = validSettings.smoothTargetingRate;
        num5 = validSettings.targetingDistance;
        num2 = validSettings.targetingMaxSpeed;
        num3 = validSettings.targetingMaxRotateSpeed;
        Vector3 cameraTargetPos2 = enemy.GetCameraTargetPos();
        Vector3 vector3_12 = Vector3.op_Subtraction(cameraTargetPos2, this.requestPos);
        vector3_12.y = 0.0f;
        ((Vector3) ref vector3_12).Normalize();
        Vector3 vector3_13 = Vector3.op_Subtraction(cameraTargetPos1, this.requestPos);
        vector3_13.y = 0.0f;
        ((Vector3) ref vector3_13).Normalize();
        if (flag2)
        {
          this.normalForward = Vector3.op_Subtraction(cameraTargetPos2, cameraTargetPos1);
          this.normalForward.y = 0.0f;
          ((Vector3) ref this.normalForward).Normalize();
        }
        else
        {
          float num10 = Vector3.Angle(vector3_12, vector3_13);
          if ((double) num10 > (double) validSettings.moveableTargetAngle)
          {
            float num11 = (double) Vector3.Cross(vector3_13, vector3_12).y >= 0.0 ? 1f : -1f;
            this.normalForward = Quaternion.op_Multiply(Quaternion.AngleAxis((num10 - validSettings.moveableTargetAngle) * num11, Vector3.up), vector3_13);
          }
        }
        if (Vector3.op_Equality(this.normalForward, Vector3.zero))
          this.normalForward = Vector3.back;
        float num12 = validSettings.targetingPitch;
        if ((double) validSettings.targetingPitchNearDistance != 0.0 || (double) validSettings.targetingPitchFarDistance != 0.0)
        {
          Vector3 vector3_14 = Vector3.op_Subtraction(cameraTargetPos2, cameraTargetPos1);
          float num13 = Mathf.Clamp01((float) (((double) ((Vector3) ref vector3_14).magnitude - (double) validSettings.targetingPitchNearDistance) / ((double) validSettings.targetingPitchFarDistance - (double) validSettings.targetingPitchNearDistance)));
          float num14 = validSettings.targetingPitchCurve.Evaluate(num13);
          num12 = (float) ((double) validSettings.targetingPitchMinAngle * (1.0 - (double) num14) + (double) validSettings.targetingPitchMaxAngle * (double) num14);
        }
        Vector3 vector3_15 = Vector3.Cross(this.normalForward, Vector3.up);
        Vector3 vector3_16 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(-num12, vector3_15), this.normalForward), Mathf.Lerp(validSettings.distanceLimit, num5, num4));
        Vector3 vector3_17 = Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), validSettings.targetingOffset);
        this.requestPos = Vector3.op_Addition(Vector3.op_Subtraction(cameraTargetPos1, vector3_16), vector3_17);
        this.requestRot = Quaternion.LookRotation(((Vector3) ref vector3_16).normalized);
        this.requestPos = Vector3.op_Addition(this.requestPos, Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), this.validSettings.cameraTargetOffsetSettings.targetOffsetPos));
        this.requestRot = Quaternion.op_Multiply(this.requestRot, Quaternion.Euler(this.validSettings.cameraTargetOffsetSettings.targetOffsetRot));
        if (MonoBehaviourSingleton<FieldManager>.IsValid())
        {
          this.requestPos = Vector3.op_Addition(this.requestPos, Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), MonoBehaviourSingleton<FieldManager>.I.cameraOffsetPos_Vec));
          this.requestRot = Quaternion.op_Multiply(this.requestRot, MonoBehaviourSingleton<FieldManager>.I.cameraOffsetRot_Quat);
        }
        this.validAnimEventTargetOffset = this.targetOffsetByPlayer == null ? (this.targetOffsetByEnemy == null ? (InGameCameraManager.TargetOffset) null : this.targetOffsetByEnemy) : this.targetOffsetByPlayer;
        if (this.validAnimEventTargetOffset != null)
        {
          this.requestPos = Vector3.op_Addition(this.requestPos, Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), this.validAnimEventTargetOffset.pos));
          this.requestRot = Quaternion.op_Multiply(this.requestRot, Quaternion.Euler(this.validAnimEventTargetOffset.rot));
          num2 = (double) this.validAnimEventTargetOffset.smoothMaxSpeed > 0.0 ? this.validAnimEventTargetOffset.smoothMaxSpeed : num2;
        }
        this.validAnimEventTargetPosition = this.targetPositionByPlayer == null ? (this.targetPositionByEnemy == null ? (InGameCameraManager.TargetPosition) null : this.targetPositionByEnemy) : this.targetPositionByPlayer;
        if (this.validAnimEventTargetPosition != null)
        {
          this.requestRot = Quaternion.LookRotation(Vector3.op_Subtraction(this.validAnimEventTargetPosition.pos, this.cameraTransform.position));
          num3 = (double) this.validAnimEventTargetPosition.smoothMaxSpeed > 0.0 ? this.validAnimEventTargetPosition.smoothMaxSpeed : num3;
        }
      }
    }
    if (this.hitObjectType != InGameCameraManager.CAM_HIT_OBJ_TYPE.NONE && !this.isMotionCameraMode)
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.requestPos, cameraTargetPos1);
      RaycastHit raycastHit = new RaycastHit();
      Ray ray;
      // ISSUE: explicit constructor call
      ((Ray) ref ray).\u002Ector(cameraTargetPos1, ((Vector3) ref vector3).normalized);
      if (Physics.Raycast(ray, ref raycastHit, num5, 2359808) && this.hitObjectType == InGameCameraManager.CAM_HIT_OBJ_TYPE.ZOOM)
      {
        float num15 = ((RaycastHit) ref raycastHit).distance;
        if ((double) num15 < (double) validSettings.distanceLimit)
          num15 = validSettings.distanceLimit;
        float num16 = num15 - validSettings.distanceLimit;
        float num17 = num5 - validSettings.distanceLimit;
        float num18 = 0.0f;
        if ((double) num17 > 0.0)
          num18 = validSettings.distanceLerpTime * (num16 / num17);
        if ((double) this.modeChangeTime <= 0.0)
        {
          this.distanceElapsedTime -= Time.deltaTime * 5f;
          if ((double) this.distanceElapsedTime < (double) num18)
            this.distanceElapsedTime = num18;
        }
        else
          this.distanceElapsedTime = num18;
        num4 = this.distanceElapsedTime / validSettings.distanceLerpTime;
        this.requestPos = Vector3.op_Addition(cameraTargetPos1, Vector3.op_Multiply(((Ray) ref ray).direction, Mathf.Lerp(validSettings.distanceLimit, num5, num4)));
      }
    }
    if ((double) num1 != 0.0 && (double) this.modeChangeTime <= 0.0)
      num1 *= (double) num4 < 0.10000000149011612 ? 0.1f : num4;
    if (this.switching)
    {
      this.switchTimer -= Time.deltaTime;
      if ((double) this.switchTimer <= 0.0)
      {
        this.switching = false;
        this.switchTimer = 0.0f;
      }
      num1 = Mathf.Lerp(validSettings.modeSwitchTime, num1, (float) (1.0 - (double) this.switchTimer / (double) validSettings.modeSwitchTime));
    }
    if (this.adjustCamera || this.isMotionCameraMode || this.isFixedCameraMode)
    {
      if (this.adjustCamera)
        this.adjustCamera = false;
      this.movePosition = this.requestPos;
      this.moveRotation = this.requestRot;
    }
    else
    {
      Vector3 vector3 = Vector3.SmoothDamp(movePosition, this.requestPos, ref this.posVelocity, num1, num2, Time.deltaTime);
      Vector3 zero = Vector3.zero;
      Vector3 eulerAngles1 = ((Quaternion) ref moveRotation).eulerAngles;
      Vector3 eulerAngles2 = ((Quaternion) ref this.requestRot).eulerAngles;
      zero.x = Mathf.SmoothDampAngle(eulerAngles1.x, eulerAngles2.x, ref this.rotVelocity.x, num1, num3, Time.deltaTime);
      zero.y = Mathf.SmoothDampAngle(eulerAngles1.y, eulerAngles2.y, ref this.rotVelocity.y, num1, num3, Time.deltaTime);
      zero.z = Mathf.SmoothDampAngle(eulerAngles1.z, eulerAngles2.z, ref this.rotVelocity.z, num1, num3, Time.deltaTime);
      Quaternion identity = Quaternion.identity;
      ((Quaternion) ref identity).eulerAngles = zero;
      this.movePosition = vector3;
      this.moveRotation = identity;
      num6 = Mathf.SmoothDamp(this.ctrlCamera.fieldOfView, num6, ref this.fieldOfViewVelocity, num1, num3, Time.deltaTime);
    }
    this.ctrlCamera.fieldOfView = num6;
  }

  private void UpdateArrowAimBossModeCamera()
  {
    Vector3 cameraTargetPos = this.targetSelf.GetCameraTargetPos();
    float ingameFieldOfView = this.ingameFieldOfView;
    Vector3 movePosition = this.movePosition;
    Quaternion moveRotation = this.moveRotation;
    Enemy enemy = (Enemy) null;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    bool flag = Object.op_Inequality((Object) enemy, (Object) null) && !enemy.enableAssimilation;
    if (this.isBossExistsPast != flag)
    {
      this.isBossExistsPast = flag;
      this.switching = true;
      this.switchTimer = this.validSettings.modeSwitchTime;
    }
    this.distanceElapsedTime += Time.deltaTime;
    if ((double) this.distanceElapsedTime > (double) this.validSettings.distanceLerpTime)
      this.distanceElapsedTime = this.validSettings.distanceLerpTime;
    this.modeChangeTime -= Time.deltaTime;
    if ((double) this.modeChangeTime < 0.0)
      this.modeChangeTime = 0.0f;
    InGameCameraManager.Settings.ArrowAimSettings arrowAimSettings = this.validSettings.arrowAimSettings;
    float num1 = arrowAimSettings.smoothTargetingRate;
    float targetingMaxSpeed = this.validSettings.targetingMaxSpeed;
    float num2 = this.distanceElapsedTime / this.validSettings.distanceLerpTime;
    float targetingDistance = arrowAimSettings.targetingDistance;
    Vector3 arrowAimForward = this.targetSelf.arrowAimForward;
    this.normalForward = ((Vector3) ref arrowAimForward).normalized;
    int arrowAimStartSign = this.targetSelf.arrowAimStartSign;
    Vector3 vector3_1 = Vector3.Cross(this.normalForward, Vector3.up);
    Vector3 vector3_2 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(-arrowAimSettings.targetingPitch, vector3_1), this.normalForward), Mathf.Lerp(this.validSettings.distanceLimit, targetingDistance, num2));
    Vector3 vector3_3 = arrowAimSettings.targetingOffset;
    if (this.arrowCameraMode == 1)
      vector3_3 = !this.targetSelf.IsAbleArrowSitShot() ? (arrowAimStartSign <= 0 ? arrowAimSettings.targetingOffset : arrowAimSettings.targetingLeftOffset) : (arrowAimStartSign <= 0 ? arrowAimSettings.targetingAvoidShotRightOffset : arrowAimSettings.targetingAvoidShotLeftOffset);
    Vector3 vector3_4 = Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), vector3_3);
    this.requestPos = Vector3.op_Addition(Vector3.op_Subtraction(cameraTargetPos, vector3_2), vector3_4);
    this.requestRot = Quaternion.LookRotation(((Vector3) ref vector3_2).normalized);
    this.modeChangeTime = !flag || !this.validSettings.targetEnable ? this.validSettings.smoothFreeRate : this.validSettings.smoothTargetingRate;
    this.distanceElapsedTime = this.validSettings.distanceLerpTime;
    if (this.hitObjectType != InGameCameraManager.CAM_HIT_OBJ_TYPE.NONE && !this.isMotionCameraMode)
    {
      Vector3 vector3_5 = Vector3.op_Subtraction(this.requestPos, cameraTargetPos);
      RaycastHit raycastHit = new RaycastHit();
      Ray ray;
      // ISSUE: explicit constructor call
      ((Ray) ref ray).\u002Ector(cameraTargetPos, ((Vector3) ref vector3_5).normalized);
      if (Physics.Raycast(ray, ref raycastHit, targetingDistance, 2359808) && this.hitObjectType == InGameCameraManager.CAM_HIT_OBJ_TYPE.ZOOM)
      {
        float num3 = ((RaycastHit) ref raycastHit).distance;
        if ((double) num3 < (double) this.validSettings.distanceLimit)
          num3 = this.validSettings.distanceLimit;
        float num4 = num3 - this.validSettings.distanceLimit;
        float num5 = targetingDistance - this.validSettings.distanceLimit;
        float num6 = 0.0f;
        if ((double) num5 > 0.0)
          num6 = this.validSettings.distanceLerpTime * (num4 / num5);
        if ((double) this.modeChangeTime <= 0.0)
        {
          this.distanceElapsedTime -= Time.deltaTime * 5f;
          if ((double) this.distanceElapsedTime < (double) num6)
            this.distanceElapsedTime = num6;
        }
        else
          this.distanceElapsedTime = num6;
        num2 = this.distanceElapsedTime / this.validSettings.distanceLerpTime;
        this.requestPos = Vector3.op_Addition(cameraTargetPos, Vector3.op_Multiply(((Ray) ref ray).direction, Mathf.Lerp(this.validSettings.distanceLimit, targetingDistance, num2)));
      }
    }
    if ((double) num1 != 0.0 && (double) this.modeChangeTime <= 0.0)
      num1 *= (double) num2 < 0.10000000149011612 ? 0.1f : num2;
    if (this.switching)
    {
      this.switchTimer -= Time.deltaTime;
      if ((double) this.switchTimer <= 0.0)
      {
        this.switching = false;
        this.switchTimer = 0.0f;
      }
      num1 = Mathf.Lerp(this.validSettings.modeSwitchTime, num1, (float) (1.0 - (double) this.switchTimer / (double) this.validSettings.modeSwitchTime));
    }
    Vector3 requestPos = this.requestPos;
    ref Vector3 local = ref this.posVelocity;
    double num7 = (double) num1;
    double num8 = (double) targetingMaxSpeed;
    double deltaTime = (double) Time.deltaTime;
    Vector3 vector3_6 = Vector3.SmoothDamp(movePosition, requestPos, ref local, (float) num7, (float) num8, (float) deltaTime);
    Vector3 zero = Vector3.zero;
    Vector3 eulerAngles1 = ((Quaternion) ref moveRotation).eulerAngles;
    Vector3 eulerAngles2 = ((Quaternion) ref this.requestRot).eulerAngles;
    zero.x = Mathf.SmoothDampAngle(eulerAngles1.x, eulerAngles2.x, ref this.rotVelocity.x, num1, 1000f, Time.deltaTime);
    zero.y = Mathf.SmoothDampAngle(eulerAngles1.y, eulerAngles2.y, ref this.rotVelocity.y, num1, 1000f, Time.deltaTime);
    zero.z = Mathf.SmoothDampAngle(eulerAngles1.z, eulerAngles2.z, ref this.rotVelocity.z, num1, 1000f, Time.deltaTime);
    Quaternion identity = Quaternion.identity;
    ((Quaternion) ref identity).eulerAngles = zero;
    this.movePosition = vector3_6;
    this.moveRotation = identity;
    this.ctrlCamera.fieldOfView = Mathf.SmoothDamp(this.ctrlCamera.fieldOfView, ingameFieldOfView, ref this.fieldOfViewVelocity, num1, 1000f, Time.deltaTime);
  }

  private void UpdateGrabCamera()
  {
    Vector3 position = this.target.position;
    Vector3 movePosition = this.movePosition;
    Quaternion moveRotation = this.moveRotation;
    this.normalForward = this.grabInfo.dir;
    Vector3 vector3_1 = Vector3.op_Multiply(Quaternion.op_Multiply(this.grabInfo.enemyRoot.rotation, this.normalForward), this.grabInfo.distance);
    this.requestPos = Vector3.op_Addition(position, vector3_1);
    this.requestRot = Quaternion.LookRotation(Vector3.op_UnaryNegation(((Vector3) ref vector3_1).normalized));
    float smoothTargetingRate = this.validSettings.smoothTargetingRate;
    float num1 = (double) this.grabInfo.smoothMaxSpeed > 0.0 ? this.grabInfo.smoothMaxSpeed : this.validSettings.targetingDistance;
    Vector3 requestPos = this.requestPos;
    ref Vector3 local = ref this.posVelocity;
    double num2 = (double) smoothTargetingRate;
    double num3 = (double) num1;
    double deltaTime = (double) Time.deltaTime;
    Vector3 vector3_2 = Vector3.SmoothDamp(movePosition, requestPos, ref local, (float) num2, (float) num3, (float) deltaTime);
    Vector3 zero = Vector3.zero;
    Vector3 eulerAngles1 = ((Quaternion) ref moveRotation).eulerAngles;
    Vector3 eulerAngles2 = ((Quaternion) ref this.requestRot).eulerAngles;
    zero.x = Mathf.SmoothDampAngle(eulerAngles1.x, eulerAngles2.x, ref this.rotVelocity.x, smoothTargetingRate, 1000f, Time.deltaTime);
    zero.y = Mathf.SmoothDampAngle(eulerAngles1.y, eulerAngles2.y, ref this.rotVelocity.y, smoothTargetingRate, 1000f, Time.deltaTime);
    zero.z = Mathf.SmoothDampAngle(eulerAngles1.z, eulerAngles2.z, ref this.rotVelocity.z, smoothTargetingRate, 1000f, Time.deltaTime);
    Quaternion identity = Quaternion.identity;
    ((Quaternion) ref identity).eulerAngles = zero;
    this.movePosition = vector3_2;
    this.moveRotation = identity;
  }

  private void UpdateCannonAimCamera()
  {
    InGameCameraManager.Settings validSettings = this.validSettings;
    Vector3 cameraTargetPos = this.targetSelf.GetCameraTargetPos();
    this.normalForward = ((Vector3) ref this.targetSelf.cannonAimForward).normalized;
    Vector3 vector3_1 = Vector3.Cross(this.normalForward, Vector3.up);
    Vector3 vector3_2 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(validSettings.cannonAimSettings.aimLookDownAngle, vector3_1), this.normalForward), validSettings.cannonAimSettings.aimDistanceToSelf);
    Vector3 vector3_3 = Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), validSettings.cannonAimSettings.aimCameraOffset);
    this.movePosition = Vector3.op_Addition(Vector3.op_Subtraction(cameraTargetPos, vector3_2), vector3_3);
    this.moveRotation = Quaternion.LookRotation(((Vector3) ref vector3_2).normalized);
  }

  private void UpdateCannonBeamChargeCamera()
  {
    InGameCameraManager.Settings validSettings = this.validSettings;
    Vector3 cameraTargetPos = this.targetSelf.GetCameraTargetPos();
    this.normalForward = ((Vector3) ref this.targetSelf.cannonAimForward).normalized;
    Vector3 vector3_1 = Vector3.Cross(this.normalForward, Vector3.up);
    Vector3 vector3_2 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(validSettings.cannonAimSettings.beamChargeCameraLookDownAngle, vector3_1), this.normalForward), validSettings.cannonAimSettings.beamChargeCameraDistanceToSelf);
    Vector3 vector3_3 = Quaternion.op_Multiply(Quaternion.LookRotation(this.normalForward), validSettings.cannonAimSettings.beamChargeCameraOffset);
    this.movePosition = Vector3.op_Addition(Vector3.op_Subtraction(cameraTargetPos, vector3_2), vector3_3);
    this.moveRotation = Quaternion.LookRotation(((Vector3) ref vector3_2).normalized);
  }

  private void UpdateCannonBeamCamera()
  {
    InGameCameraManager.Settings validSettings = this.validSettings;
    this.movePosition = validSettings.cannonAimSettings.beamCameraPosition;
    this.moveRotation = Quaternion.Euler(validSettings.cannonAimSettings.beamCameraRotationEular);
  }

  private void UpdateStopCamera()
  {
    Vector3 movePosition = this.movePosition;
    Quaternion moveRotation = this.moveRotation;
    float smoothTargetingRate = this.validSettings.smoothTargetingRate;
    float num1 = this.validSettings.targetingDistance;
    float num2 = 1000f;
    if ((double) this.stopMaxSpeed > 0.0)
    {
      num1 = this.stopMaxSpeed;
      num2 = this.stopMaxSpeed * (num2 / this.validSettings.targetingDistance);
    }
    if ((double) this.stopMaxRotSpeed > 0.0)
      num2 = this.stopMaxRotSpeed;
    Vector3 stopPos = this.stopPos;
    ref Vector3 local = ref this.posVelocity;
    double num3 = (double) smoothTargetingRate;
    double num4 = (double) num1;
    double deltaTime = (double) Time.deltaTime;
    Vector3 vector3 = Vector3.SmoothDamp(movePosition, stopPos, ref local, (float) num3, (float) num4, (float) deltaTime);
    Vector3 zero = Vector3.zero;
    Vector3 eulerAngles = ((Quaternion) ref moveRotation).eulerAngles;
    Vector3 stopRotEular = this.stopRotEular;
    zero.x = Mathf.SmoothDampAngle(eulerAngles.x, stopRotEular.x, ref this.rotVelocity.x, smoothTargetingRate, num2, Time.deltaTime);
    zero.y = Mathf.SmoothDampAngle(eulerAngles.y, stopRotEular.y, ref this.rotVelocity.y, smoothTargetingRate, num2, Time.deltaTime);
    zero.z = Mathf.SmoothDampAngle(eulerAngles.z, stopRotEular.z, ref this.rotVelocity.z, smoothTargetingRate, num2, Time.deltaTime);
    Quaternion identity = Quaternion.identity;
    ((Quaternion) ref identity).eulerAngles = zero;
    this.movePosition = vector3;
    this.moveRotation = identity;
  }

  private void UpdateCutCamera()
  {
    this.movePosition = this.cutPos;
    this.moveRotation = this.cutRot;
  }

  private void UpdateShake()
  {
    Vector3 vector3 = Vector3.zero;
    int index = 0;
    while (index < this.shakeParams.Count)
    {
      InGameCameraManager.ShakeParam shakeParam = this.shakeParams[index];
      float shakeCycleTime = shakeParam.shakeCycleTime;
      if ((double) shakeCycleTime <= 0.0)
      {
        Log.Warning(LOG.INGAME, "カメラ揺れの振動周期が0");
        this.shakeParams.RemoveAt(index);
      }
      else
      {
        int num1 = (int) ((double) shakeParam.shakeTime * 2.0 / (double) shakeCycleTime);
        shakeParam.shakeTime += Time.deltaTime;
        int num2 = (int) ((double) shakeParam.shakeTime * 2.0 / (double) shakeCycleTime);
        bool flag = false;
        if (num1 != num2)
        {
          shakeParam.shakeLength *= Mathf.Pow(this.shakeAttenuationPercent, (float) (num2 - num1));
          if ((double) shakeParam.shakeLength < 0.0099999997764825821)
            flag = true;
        }
        if (flag)
        {
          this.shakeParams.RemoveAt(index);
        }
        else
        {
          vector3 = Vector3.op_Addition(vector3, Vector3.op_Multiply(Vector3.up, shakeParam.shakeLength * Mathf.Sin((float) ((double) shakeParam.shakeTime * 3.1415927410125732 * 2.0) / shakeCycleTime)));
          ++index;
        }
      }
    }
    this.cameraTransform.position = Vector3.op_Addition(this.movePosition, vector3);
    this.cameraTransform.rotation = this.moveRotation;
  }

  private void LateUpdate()
  {
    if (Object.op_Inequality((Object) this.radialBlurFilter, (Object) null) && ((Behaviour) this.radialBlurFilter).enabled)
      this.UpdateRadialBlur();
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    if (Object.op_Equality((Object) this.targetObject, (Object) null) || Object.op_Inequality((Object) this.targetObject._transform, (Object) this.target))
    {
      this.targetObject = ((Component) this.target).GetComponent<StageObject>();
      this.targetPlayer = this.targetObject as Player;
      this.targetSelf = this.targetObject as Self;
    }
    switch (this.cameraMode)
    {
      case InGameCameraManager.CAMERA_MODE.GRABBED:
        this.UpdateGrabCamera();
        break;
      case InGameCameraManager.CAMERA_MODE.ARROW_AIM_BOSS:
        this.UpdateArrowAimBossModeCamera();
        break;
      case InGameCameraManager.CAMERA_MODE.CANNON_AIM:
        this.UpdateCannonAimCamera();
        break;
      case InGameCameraManager.CAMERA_MODE.CANNON_BEAM_CHARGE:
        this.UpdateCannonBeamChargeCamera();
        break;
      case InGameCameraManager.CAMERA_MODE.CANNON_BEAM:
        this.UpdateCannonBeamCamera();
        break;
      case InGameCameraManager.CAMERA_MODE.STOP:
        this.UpdateStopCamera();
        break;
      case InGameCameraManager.CAMERA_MODE.CUT:
        this.UpdateCutCamera();
        break;
      default:
        this.UpdatePlayerCamera();
        break;
    }
    this.UpdateShake();
  }

  public Vector3 WorldToScreenPoint(Vector3 pos) => this.ctrlCamera.WorldToScreenPoint(pos);

  public Vector3 WorldToViewportPoint(Vector3 pos) => this.ctrlCamera.WorldToViewportPoint(pos);

  public Vector3 ScreenToWorldPoint(Vector3 pos) => this.ctrlCamera.ScreenToWorldPoint(pos);

  public float GetPixelHeight() => (float) this.ctrlCamera.pixelHeight;

  public void AdjustCameraPosition()
  {
    this.adjustCamera = true;
    this.posVelocity = Vector3.zero;
    this.rotVelocity = Vector3.zero;
    this.fieldOfViewVelocity = 0.0f;
    this.switching = false;
    this.switchTimer = 0.0f;
    this.modeChangeTime = 0.0f;
  }

  public void SetShakeCamera(Vector3 pos, float percent, float cycle_time = 0.0f)
  {
    Vector3 vector3 = Vector3.op_Subtraction(pos, this.movePosition);
    float num1 = (this.shakeMaxFocusLength - ((Vector3) ref vector3).magnitude) / this.shakeMaxFocusLength;
    if ((double) num1 < 0.0)
      num1 = 0.0f;
    float num2 = this.shakeAmplitude * percent * num1;
    if ((double) num2 <= 0.0099999997764825821)
      return;
    InGameCameraManager.ShakeParam shakeParam = new InGameCameraManager.ShakeParam();
    shakeParam.shakeTime = 0.0f;
    shakeParam.shakeLength = num2;
    shakeParam.shakeCycleTime = cycle_time;
    if ((double) shakeParam.shakeCycleTime <= 0.0)
      shakeParam.shakeCycleTime = this.shakeCycleTime;
    this.shakeParams.Add(shakeParam);
    if (this.shakeMaxNum <= 0 || this.shakeParams.Count <= this.shakeMaxNum)
      return;
    this.shakeParams.RemoveAt(0);
  }

  public void OnScreenRotate(bool is_portrait)
  {
    if (is_portrait)
    {
      this.validSettings = this.portraitSettings;
      this.ingameFieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.inGamePortraitFieldOfView;
    }
    else
    {
      this.validSettings = this.landscapeSettings;
      this.ingameFieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.inGameLandscapeFieldOfView;
    }
    if (!QuestManager.IsValidInGameWaveMatch() || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.validSettings.cameraFieldOffsetSettings.targetOffsetPos.y = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam().cameraFieldOffsetY;
  }

  public bool IsEndMotionCamera()
  {
    if (!this.isMotionCameraMode || this.motionCameraTransforms == null)
      return true;
    int index = 0;
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      index = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? 0 : 1;
    Animation component = ((Component) this.motionCameraTransforms[index]).gameObject.GetComponent<Animation>();
    return Object.op_Equality((Object) component, (Object) null) || !component.isPlaying;
  }

  public void OnHappenQuestDirection(
    bool enable,
    Transform boss_transform = null,
    Object[] cameras = null,
    Vector3[] camera_offsets = null)
  {
    if (enable)
    {
      this.EndRadialBlurFilter();
      MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(262144 /*0x040000*/);
      if (Object.op_Inequality((Object) boss_transform, (Object) null) && cameras != null)
        this.SetMotionCamera(true, boss_transform, cameras, camera_offsets);
      if (!MonoBehaviourSingleton<AudioListenerManager>.IsValid())
        return;
      MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, true);
    }
    else
    {
      this.SetMotionCamera(false);
      MonoBehaviourSingleton<GameSceneManager>.I.SetMainCameraCullingMask(GameSceneGlobalSettings.GetDefaultMainCameraCullingMask());
      if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
        this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
      if (MonoBehaviourSingleton<AudioListenerManager>.IsValid())
        MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, false);
      this.AdjustCameraPosition();
    }
  }

  public void SetMotionCamera(
    bool enable,
    Transform target_transform = null,
    Object[] cameras = null,
    Vector3[] camera_offsets = null)
  {
    if (enable)
    {
      this.motionCameraTransforms = new Transform[2];
      Transform gameObject1 = Utility.CreateGameObject("FieldQuestCamera", this._transform);
      gameObject1.position = target_transform.position;
      gameObject1.rotation = target_transform.rotation;
      Vector3 one = Vector3.one;
      one.x = target_transform.lossyScale.x / this._transform.lossyScale.x;
      one.y = target_transform.lossyScale.y / this._transform.lossyScale.y;
      one.z = target_transform.lossyScale.z / this._transform.lossyScale.z;
      gameObject1.localScale = one;
      this.motionCameraParent = gameObject1;
      for (int index = 0; index < 2; ++index)
      {
        Transform gameObject2 = Utility.CreateGameObject("offset_" + index.ToString(), gameObject1);
        gameObject2.localPosition = Vector3.zero;
        gameObject2.localRotation = Quaternion.identity;
        gameObject2.localScale = Vector3.one;
        if (camera_offsets != null)
          gameObject2.localPosition = camera_offsets[index];
        Transform transform = ResourceUtility.Realizes(cameras[index], gameObject2);
        if (Object.op_Equality((Object) transform, (Object) null))
        {
          Object.Destroy((Object) ((Component) gameObject1).gameObject);
          return;
        }
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
        this.motionCameraTransforms[index] = transform;
      }
      this.isMotionCameraMode = true;
    }
    else
    {
      this.isMotionCameraMode = false;
      if (Object.op_Inequality((Object) this.motionCameraParent, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.motionCameraParent).gameObject);
        this.motionCameraParent = (Transform) null;
      }
      if (this.motionCameraTransforms == null)
        return;
      this.motionCameraTransforms = (Transform[]) null;
    }
  }

  public void StartRadialBlurFilter(float time, float strength, Vector3 center_pos)
  {
    if (Object.op_Equality((Object) this.radialBlurFilter, (Object) null))
      return;
    this._StartRadialBlurFilter(time, strength);
    this.radialBlurCenterPos = center_pos;
    this.radialBlurCenterTransform = (Transform) null;
  }

  public void StartRadialBlurFilter(float time, float strength, Transform center_transform)
  {
    if (Object.op_Equality((Object) this.radialBlurFilter, (Object) null))
      return;
    this._StartRadialBlurFilter(time, strength);
    this.radialBlurCenterPos = Vector3.zero;
    this.radialBlurCenterTransform = center_transform;
  }

  private void _StartRadialBlurFilter(float time, float strength)
  {
    if (Object.op_Equality((Object) this.radialBlurFilter, (Object) null) || ((Behaviour) this.radialBlurFilter).enabled)
      return;
    ((Behaviour) this.radialBlurFilter).enabled = true;
    this.radialBlurFilter.StartFilter();
    this.radialBlurStrengthValue = strength;
    if ((double) time <= 0.0)
      this.radialBlurFilter.strength = strength;
    else
      this.radialBlurStrengthPerTime = strength / time;
  }

  public void ChangeRadialBlurFilter(float time, float strength)
  {
    if (Object.op_Equality((Object) this.radialBlurFilter, (Object) null) || !((Behaviour) this.radialBlurFilter).enabled)
      return;
    if ((double) time <= 0.0)
    {
      this.radialBlurStrengthValue = strength;
      if ((double) strength <= 0.0)
      {
        this.radialBlurFilter.strength = 0.0f;
        this.radialBlurFilter.StopFilter();
        ((Behaviour) this.radialBlurFilter).enabled = false;
      }
      else
        this.radialBlurFilter.strength = strength;
    }
    else
    {
      this.radialBlurStrengthPerTime = (float) -((double) this.radialBlurStrengthValue - (double) strength) / time;
      this.radialBlurStrengthValue = strength;
    }
  }

  public void EndRadialBlurFilter(float time = 0.0f) => this.ChangeRadialBlurFilter(time, 0.0f);

  public void ResetMovePositionAndRotaion()
  {
    this.movePosition = ((Component) this.ctrlCamera).transform.position;
    this.moveRotation = ((Component) this.ctrlCamera).transform.rotation;
  }

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
    MAX,
  }

  [Serializable]
  public class Settings
  {
    [Tooltip("ターゲットカメラ ピッチ")]
    public float targetingPitch = 10f;
    [Tooltip("ターゲットカメラ ピッチ変化最短距離")]
    public float targetingPitchNearDistance;
    [Tooltip("ターゲットカメラ ピッチ変化最長距離")]
    public float targetingPitchFarDistance;
    [Tooltip("ターゲットカメラ ピッチ変化カーブ最小角度")]
    public float targetingPitchMinAngle = 10f;
    [Tooltip("ターゲットカメラ ピッチ変化カーブ最大角度")]
    public float targetingPitchMaxAngle = 10f;
    [Tooltip("ターゲットカメラ ピッチ変化カーブ")]
    public AnimationCurve targetingPitchCurve;
    [Tooltip("ターゲットカメラ 距離")]
    public float targetingDistance = 4f;
    [Tooltip("ターゲットカメラ オフセット")]
    public Vector3 targetingOffset = Vector3.zero;
    [Tooltip("ターゲットカメラ カメラ移動最大速度")]
    public float targetingMaxSpeed = 999f;
    [Tooltip("ターゲットカメラ　カメラ回転最大速度")]
    public float targetingMaxRotateSpeed = 999f;
    [Tooltip("フリーカメラ ピッチ")]
    public float freePitch = 20f;
    [Tooltip("フリーカメラ 距離")]
    public float freeDistance = 10f;
    [Tooltip("フリーカメラ オフセット")]
    public Vector3 freeOffset = Vector3.zero;
    [Tooltip("フリーカメラ カメラ移動最大速度")]
    public float freeMaxSpeed = 999f;
    [Tooltip("フリーカメラ カメラ回転最大速度")]
    public float freeMaxRotateSpeed = 999f;
    [Tooltip("true:ターゲットカメラ抜け時方向固定")]
    public bool normalEnable = true;
    [Tooltip("true:ターゲットカメラ有効")]
    public bool targetEnable = true;
    [Tooltip("敵可動領域角度")]
    public float moveableTargetAngle = 10f;
    [Tooltip("ターゲットカメラの補正レート")]
    public float smoothTargetingRate = 0.1f;
    [Tooltip("フリーカメラの補正レート")]
    public float smoothFreeRate = 0.1f;
    [Tooltip("カメラ切り替え時間")]
    public float modeSwitchTime = 0.3f;
    [Tooltip("カメラが寄る限界値")]
    public float distanceLimit = 2f;
    [Tooltip("カメラの距離が寄せてから元に戻るまでの時間")]
    public float distanceLerpTime = 2f;
    [Tooltip("ターゲット追尾の後方オフセット")]
    public float followBackOffset = 3.5f;
    [Tooltip("ターゲット追尾の左右割合")]
    public float followSidePercent = 0.8f;
    [Tooltip("ターゲット追尾の補正レート")]
    public float followRate = 0.1f;
    [Tooltip("弓狙い設定")]
    public InGameCameraManager.Settings.ArrowAimSettings arrowAimSettings = new InGameCameraManager.Settings.ArrowAimSettings();
    [Tooltip("大型モンスター戦のカメラ補正設定")]
    public InGameCameraManager.Settings.CameraTargetOffsetSettings cameraTargetOffsetSettings = new InGameCameraManager.Settings.CameraTargetOffsetSettings();
    [Tooltip("フィールドのカメラ補正設定")]
    public InGameCameraManager.Settings.CameraTargetOffsetSettings cameraFieldOffsetSettings = new InGameCameraManager.Settings.CameraTargetOffsetSettings();
    [Tooltip("魔弾砲関連のカメラ補正設定")]
    public InGameCameraManager.Settings.CannonAimSettings cannonAimSettings = new InGameCameraManager.Settings.CannonAimSettings();

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
      [Tooltip("狙い時の補正レート")]
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
      [Tooltip("魔弾砲狙い時のカメラとプレイヤーとの距離")]
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
    ZOOM,
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
    public Vector3 pos = Vector3.zero;
    public Vector3 rot = Vector3.zero;
    public float smoothMaxSpeed;
  }

  public class TargetPosition
  {
    public Vector3 pos = Vector3.zero;
    public float smoothMaxSpeed;
  }
}

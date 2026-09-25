// Decompiled with JetBrains decompiler
// Type: HomeCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class HomeCamera : MonoBehaviour
{
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

  public Camera targetCamera { get; private set; }

  public Transform targetCameraTransform { get; private set; }

  public bool isInitialized { get; private set; }

  public bool isChanging { get; private set; }

  public HomeCamera.VIEW_MODE viewMode { get; private set; }

  public void ChangeView(HomeCamera.VIEW_MODE view)
  {
    if (!this.IsValidChangeView(view))
      return;
    this.isChanging = true;
    HomeCamera.VIEW_MODE viewMode = this.viewMode;
    switch (view)
    {
      case HomeCamera.VIEW_MODE.NORMAL:
        this.viewMode = HomeCamera.VIEW_MODE.NORMAL;
        if (viewMode == HomeCamera.VIEW_MODE.SITTING)
        {
          this.StartCoroutine(this.DoStand((System.Action) (() => this.isChanging = false)));
          break;
        }
        this.isChanging = false;
        break;
      case HomeCamera.VIEW_MODE.SITTING:
        this.viewMode = HomeCamera.VIEW_MODE.SITTING;
        this.StartCoroutine(this.DoSit((System.Action) (() => this.isChanging = false)));
        break;
      default:
        this.isChanging = false;
        break;
    }
  }

  public void LateUpdate()
  {
    if (Object.op_Equality((Object) this.chara, (Object) null))
      this.chara = this.GetSelfCharacter();
    if (!this.IsValidCameraUpdate() || this.viewMode != HomeCamera.VIEW_MODE.NORMAL)
      return;
    this.targetPos = Vector3.zero;
    this.cameraPos = Vector3.zero;
    this.GetTargetAndNormalCameraPosition(ref this.targetPos, ref this.cameraPos);
    this.cameraPos = this.CheckCollision(this.targetPos, this.cameraPos);
    this.UpdateCameraTransform(this.targetPos, this.cameraPos);
  }

  private void Start()
  {
    this.viewMode = HomeCamera.VIEW_MODE.NORMAL;
    this.targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    this.targetCameraTransform = ((Component) this.targetCamera).transform;
    this.defaultFov = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
    this.homeSceneParam = GameSceneGlobalSettings.GetCurrentIHomeManager().GetSceneSetting();
    this.isInitialized = true;
  }

  private bool IsValidChangeView(HomeCamera.VIEW_MODE view)
  {
    return this.viewMode != view && !this.isChanging && !Object.op_Equality((Object) this.chara, (Object) null);
  }

  private bool IsValidCameraUpdate()
  {
    return HomeSelfCharacter.CTRL && !Object.op_Equality((Object) this.chara, (Object) null) && (!this.chara.InitedAnimation || !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "GuildTop")) && !this.isChanging;
  }

  private void GetTargetAndNormalCameraPosition(ref Vector3 targetPos, ref Vector3 cameraPos)
  {
    Vector3 zero = Vector3.zero;
    float cameraDistance = 0.0f;
    float cameraHeight = 0.0f;
    float targetHeight = 0.0f;
    this.GetNeededNormalCameraParam(ref zero, ref cameraDistance, ref cameraHeight, ref targetHeight);
    targetPos = this.GetTargetPositionForNormalCamera();
    cameraPos = this.GetNormalCameraPosition(zero, cameraDistance, cameraHeight, targetPos);
    targetPos.y += targetHeight;
    cameraPos.y += cameraHeight;
  }

  private Vector3 CheckCollision(Vector3 charaPos, Vector3 requestPos)
  {
    Vector3 vector3 = Vector3.op_Subtraction(requestPos, charaPos);
    float cameraDistance = this.GetCameraDistance();
    float cameraDistanceMin = this.GetCameraDistanceMin();
    float lerpTime = this.GetLerpTime();
    RaycastHit raycastHit = new RaycastHit();
    Ray ray;
    // ISSUE: explicit constructor call
    ((Ray) ref ray).\u002Ector(charaPos, ((Vector3) ref vector3).normalized);
    if (Physics.Raycast(ray, ref raycastHit, cameraDistance, 512 /*0x0200*/))
    {
      float num1 = Mathf.Max(((RaycastHit) ref raycastHit).distance, cameraDistanceMin) - cameraDistanceMin;
      float num2 = cameraDistance - cameraDistanceMin;
      float num3 = 0.0f;
      if ((double) num2 > 0.0)
        num3 = lerpTime * (num1 / num2);
      float num4 = num3 / lerpTime;
      requestPos = Vector3.op_Addition(charaPos, Vector3.op_Multiply(((Ray) ref ray).direction, Mathf.Lerp(cameraDistanceMin, cameraDistance, num4)));
    }
    return requestPos;
  }

  private void GetNeededNormalCameraParam(
    ref Vector3 initPos,
    ref float cameraDistance,
    ref float cameraHeight,
    ref float targetHeight)
  {
    initPos = this.homeSceneParam.selfInitPos;
    cameraHeight = this.homeSceneParam.GetSelfCameraHeight();
    targetHeight = this.homeSceneParam.selfCameraTagetHeight;
    cameraDistance = this.GetCameraDistance();
  }

  private float GetCameraDistance() => this.homeSceneParam.GetSelfCameraDistance();

  private float GetCameraDistanceMin() => this.homeSceneParam.selfCameraDistanceMin;

  private float GetLerpTime() => 1.5f;

  private void UpdateCameraTransform(Vector3 targetPos, Vector3 cameraPos)
  {
    this.targetCameraTransform.position = cameraPos;
    this.targetCameraTransform.LookAt(targetPos);
    if ((double) this.targetCamera.fieldOfView == (double) this.defaultFov)
      return;
    this.targetCamera.fieldOfView = this.defaultFov;
  }

  private Vector3 GetNormalCameraPosition(
    Vector3 initPos,
    float cameraDistance,
    float cameraHeight,
    Vector3 targetPos)
  {
    Vector3 baseCameraPosition = this.GetBaseCameraPosition(initPos);
    baseCameraPosition.z -= 2f;
    Vector3 vector3 = Vector3.op_Subtraction(baseCameraPosition, targetPos);
    vector3.y = 0.0f;
    ((Vector3) ref vector3).Normalize();
    return Vector3.op_Addition(Vector3.op_Multiply(vector3, cameraDistance), targetPos);
  }

  private Vector3 GetTargetPositionForNormalCamera() => this.chara._transform.position;

  private HomeSelfCharacter GetSelfCharacter()
  {
    return GameSceneGlobalSettings.GetCurrentIHomeManager()?.IHomePeople.selfChara;
  }

  private Vector3 GetBaseCameraPosition(Vector3 target_pos)
  {
    Vector3 vector3 = new Vector3();
    Vector3 baseCameraPosition = Vector3.op_Addition(Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(this.homeSceneParam.selfCameraAngleY, Vector3.up), Vector3.forward), this.homeSceneParam.selfCameraDistanceMax), target_pos);
    baseCameraPosition.y += this.homeSceneParam.GetSelfCameraHeight();
    return baseCameraPosition;
  }

  private void OnPinch(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1,
    float pinch_length)
  {
    if (touch_info0 == null || touch_info1 == null || MonoBehaviourSingleton<UIManager>.I.IsDisable() || !this.GetSelfCharacter().IsEnableControl() || this.viewMode != HomeCamera.VIEW_MODE.NORMAL)
      return;
    this.homeSceneParam.selfCameraZoomRate = Mathf.Clamp01(this.homeSceneParam.selfCameraZoomRate + pinch_length * this.homeSceneParam.selfCameraZoomCoef);
  }

  private void OnEnable()
  {
    if (!HomeSelfCharacter.CTRL)
      return;
    InputManager.OnPinch += new InputManager.OnPinchDelegate(this.OnPinch);
  }

  private void OnDisable()
  {
    if (!HomeSelfCharacter.CTRL)
      return;
    InputManager.OnPinch -= new InputManager.OnPinchDelegate(this.OnPinch);
  }

  private IEnumerator DoSit(System.Action callback = null)
  {
    this.beforeNormalCameraPos = this.targetCameraTransform.position;
    this.beforeNormalCameraRot = this.targetCameraTransform.rotation;
    this.beforeNormalCameraFov = this.targetCamera.fieldOfView;
    TablePoint tablePoint = !MonoBehaviourSingleton<LoungeManager>.IsValid() ? MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearTablePoint() : MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearTablePoint();
    yield return (object) this.StartCoroutine(this.LerpCamera(this.beforeNormalCameraPos, tablePoint.cameraPosition, this.beforeNormalCameraRot, Quaternion.Euler(tablePoint.cameraRotation), this.beforeNormalCameraFov, tablePoint.cameraFov));
    if (callback != null)
      callback();
  }

  private IEnumerator DoStand(System.Action callback = null)
  {
    Vector3 position = this.targetCameraTransform.position;
    Quaternion rotation = this.targetCameraTransform.rotation;
    float fieldOfView = this.targetCamera.fieldOfView;
    Quaternion beforeNormalCameraRot = this.beforeNormalCameraRot;
    Vector3 beforeNormalCameraPos = this.beforeNormalCameraPos;
    float beforeNormalCameraFov = this.beforeNormalCameraFov;
    yield return (object) this.StartCoroutine(this.LerpCamera(position, beforeNormalCameraPos, rotation, beforeNormalCameraRot, fieldOfView, beforeNormalCameraFov));
    if (callback != null)
      callback();
  }

  private IEnumerator LerpCamera(
    Vector3 prevPos,
    Vector3 targetPos,
    Quaternion prevRot,
    Quaternion targetRot,
    float prevFov,
    float targetFov)
  {
    bool wait = true;
    float time = 0.0f;
    OutGameSettingsManager.LoungeScene loungeSceneParam = this.homeSceneParam as OutGameSettingsManager.LoungeScene;
    while (wait)
    {
      time = Mathf.Min(time + Time.deltaTime, 1f);
      if (this.viewMode == HomeCamera.VIEW_MODE.NORMAL)
      {
        Vector3 targetPos1 = new Vector3();
        Vector3 cameraPos = new Vector3();
        this.GetTargetAndNormalCameraPosition(ref targetPos1, ref cameraPos);
        targetPos = cameraPos;
        targetRot = Quaternion.LookRotation(Vector3.op_Subtraction(targetPos1, cameraPos));
      }
      if (Vector3.op_Inequality(this.targetCameraTransform.position, targetPos))
      {
        this.targetCameraTransform.position = Vector3.Lerp(prevPos, targetPos, HomeCamera.EaseOutCube(time) * loungeSceneParam.sittingCameraEaseCoef);
        wait = true;
      }
      else
        wait = false;
      if ((double) Quaternion.Angle(this.targetCameraTransform.rotation, targetRot) > 0.10000000149011612)
      {
        this.targetCameraTransform.rotation = Quaternion.Slerp(prevRot, targetRot, HomeCamera.EaseOutCube(time) * loungeSceneParam.sittingCameraEaseCoef);
        wait = true;
      }
      else
        wait = wait;
      if ((double) this.targetCamera.fieldOfView != (double) targetFov)
      {
        this.targetCamera.fieldOfView = Mathf.Lerp(prevFov, targetFov, time);
        wait = true;
      }
      else
        wait = wait;
      yield return (object) null;
    }
  }

  private static float EaseOutCube(float currentValue) => currentValue * (2f - currentValue);

  public enum VIEW_MODE
  {
    NORMAL,
    SITTING,
  }
}

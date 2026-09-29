// Decompiled with JetBrains decompiler
// Type: WorldMapCameraController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class WorldMapCameraController : DraggableCamera
{
  protected bool isInteractive_ = true;
  private float fov = 60f;

  public bool isInteractive
  {
    get => this.isInteractive_;
    set => this.isInteractive_ = value;
  }

  public override Camera _camera
  {
    get
    {
      if (Object.op_Equality((Object) this.__camera, (Object) null))
        this.__camera = ((Component) this).GetComponent<Camera>();
      return this.__camera;
    }
  }

  protected float cameraFovMin
  {
    get => MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraFovMin;
  }

  protected float cameraFovMax
  {
    get => MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraFovMax;
  }

  protected override Plane hitPlane => new Plane(Vector3.back, 1f);

  private void Awake()
  {
    this.distance = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraManualDistance;
    this.distanceManual = this.distance;
    this.CreateRenderTexture(false);
    this._camera.fieldOfView = this.cameraFovMin;
    this.isInteractive_ = true;
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene"))
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.CreateRenderTexture);
  }

  private void CreateRenderTexture(bool isPortrait) => this.Restore();

  private void OnDestroy()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.CreateRenderTexture);
    if (!Object.op_Inequality((Object) this._camera.targetTexture, (Object) null))
      return;
    RenderTexture.ReleaseTemporary(this._camera.targetTexture);
    this._camera.targetTexture = (RenderTexture) null;
  }

  protected override Vector3 GetCameraMove(Vector2 old_screen_pos, Vector2 now_screen_pos)
  {
    Plane hitPlane = this.hitPlane;
    Ray ray1 = this._camera.ScreenPointToRay(old_screen_pos.ToVector3XY());
    Ray ray2 = this._camera.ScreenPointToRay(now_screen_pos.ToVector3XY());
    float num1;
    float num2;
    if (!((Plane) ref hitPlane).Raycast(ray1, ref num1) || !((Plane) ref hitPlane).Raycast(ray2, ref num2))
      return Vector3.zero;
    Vector3 point = ((Ray) ref ray1).GetPoint(num1);
    Vector3 cameraMove = Vector3.op_Subtraction(((Ray) ref ray2).GetPoint(num2), point);
    cameraMove.x = -cameraMove.x;
    cameraMove.y = -cameraMove.y;
    cameraMove.z = 0.0f;
    return cameraMove;
  }

  protected override Vector3 ClampEnableMapArea(Vector3 pos)
  {
    float num1 = -MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampLeft;
    float cameraMoveClampRight = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampRight;
    float cameraMoveClampUpper = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampUpper;
    float num2 = -MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampLower;
    if ((double) pos.x < (double) num1)
      pos.x = num1;
    else if ((double) pos.x > (double) cameraMoveClampRight)
      pos.x = cameraMoveClampRight;
    if ((double) pos.y < (double) num2)
      pos.y = num2;
    else if ((double) pos.y > (double) cameraMoveClampUpper)
      pos.y = cameraMoveClampUpper;
    return pos;
  }

  protected override void OnPinch(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1,
    float pinch_length)
  {
    if (!this.IsInteractive() || touch_info0 == null || touch_info1 == null)
      return;
    Plane hitPlane = this.hitPlane;
    this.cameraMove = Vector3.zero;
    Ray ray = this._camera.ScreenPointToRay(Vector2.op_Multiply(Vector2.op_Addition(touch_info0.position, touch_info1.position), 0.5f).ToVector3XY());
    float num;
    if (!((Plane) ref hitPlane).Raycast(ray, ref num))
      return;
    GlobalSettingsManager.WorldMapParam worldMapParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam;
    this.fov -= pinch_length * worldMapParam.cameraPinchSpeed;
    this.fov = Mathf.Clamp(this.fov, worldMapParam.cameraFovMin, worldMapParam.cameraFovMax);
    this._camera.fieldOfView = this.fov;
    this.UpdateCameraTransform();
  }

  protected override bool IsInteractive() => this.isInteractive_;

  protected override void UpdateCameraTransform()
  {
    this.targetPos = this.ClampEnableMapArea(this.targetPos);
    this.cameraTransform.position = Vector3.op_Subtraction(this.targetPos, Vector3.op_Multiply(this.cameraTransform.forward, this.distance));
  }

  public void Restore()
  {
    if (Object.op_Inequality((Object) null, (Object) this._camera.targetTexture))
    {
      RenderTexture.ReleaseTemporary(this._camera.targetTexture);
      this._camera.targetTexture = (RenderTexture) null;
    }
    this._camera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
  }
}

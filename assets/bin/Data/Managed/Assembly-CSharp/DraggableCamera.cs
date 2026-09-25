// Decompiled with JetBrains decompiler
// Type: DraggableCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class DraggableCamera : MonoBehaviour
{
  protected Camera __camera;
  protected Transform _cameraTransform;
  protected Vector3 cameraMove = Vector3.zero;

  public virtual Camera _camera
  {
    get
    {
      if (Object.op_Equality((Object) this.__camera, (Object) null))
        this.__camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
      return this.__camera;
    }
  }

  protected virtual Transform cameraTransform
  {
    get
    {
      if (Object.op_Equality((Object) this._cameraTransform, (Object) null))
        this._cameraTransform = ((Component) this._camera).transform;
      return this._cameraTransform;
    }
  }

  protected virtual Plane hitPlane => new Plane(Vector3.up, 0.0f);

  public bool isChanging { get; protected set; }

  public float distanceManual { get; protected set; }

  public Vector3 targetPos { get; set; }

  public Vector3 angle { get; protected set; }

  public float distance { get; protected set; }

  protected virtual float cameraManualDistanceMin => 0.0f;

  protected virtual float cameraManualDistanceMax => 0.0f;

  private void OnEnable()
  {
    InputManager.OnTouchOn += new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTouchOff += new InputManager.OnTouchDelegate(this.OnTouchOff);
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
    InputManager.OnPinch += new InputManager.OnPinchDelegate(this.OnPinch);
    InputManager.OnDoubleDrag += new InputManager.OnDoubleTouchDelegate(this.OnDoubleDrag);
  }

  private void OnDisable()
  {
    InputManager.OnTouchOn -= new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTouchOff -= new InputManager.OnTouchDelegate(this.OnTouchOff);
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
    InputManager.OnPinch -= new InputManager.OnPinchDelegate(this.OnPinch);
    InputManager.OnDoubleDrag -= new InputManager.OnDoubleTouchDelegate(this.OnDoubleDrag);
  }

  protected virtual void OnTouchOn(InputManager.TouchInfo info)
  {
    if (!this.IsInteractive())
      return;
    this.cameraMove = Vector3.zero;
  }

  protected virtual void OnTouchOff(InputManager.TouchInfo info)
  {
    if (!this.IsInteractive())
      return;
    this.cameraMove = this.GetCameraMove(info);
  }

  protected virtual void OnDrag(InputManager.TouchInfo info)
  {
    if (!this.IsInteractive())
      return;
    this.cameraMove = Vector3.zero;
    this.targetPos = Vector3.op_Addition(this.targetPos, this.GetCameraMove(info));
  }

  protected virtual void OnDoubleDrag(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1)
  {
    if (!this.IsInteractive())
      return;
    this.cameraMove = Vector3.zero;
    this.targetPos = Vector3.op_Addition(this.targetPos, this.GetCameraMove(touch_info0, touch_info1));
  }

  protected virtual void OnPinch(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1,
    float pinch_length)
  {
    if (!this.IsInteractive() || touch_info0 == null || touch_info1 == null)
      return;
    Plane hitPlane = this.hitPlane;
    this.cameraMove = Vector3.zero;
    Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Addition(touch_info0.position, touch_info1.position), 0.5f);
    Ray ray1 = this._camera.ScreenPointToRay(vector2.ToVector3XY());
    float num;
    if (!((Plane) ref hitPlane).Raycast(ray1, ref num))
      return;
    Vector3 point1 = ((Ray) ref ray1).GetPoint(num);
    this.distance -= pinch_length * 0.01f;
    this.distance = Mathf.Clamp(this.distance, this.cameraManualDistanceMin, this.cameraManualDistanceMax);
    this.distanceManual = this.distance;
    this.UpdateCameraTransform();
    Vector2 vector2Xy = this._camera.WorldToScreenPoint(point1).ToVector2XY();
    Ray ray2 = this._camera.ScreenPointToRay(Vector2.op_Addition(this._camera.WorldToScreenPoint(this.targetPos).ToVector2XY(), Vector2.op_Subtraction(vector2Xy, vector2)).ToVector3XY());
    if (!((Plane) ref hitPlane).Raycast(ray2, ref num))
      return;
    Vector3 point2 = ((Ray) ref ray2).GetPoint(num);
    point2.y = 0.0f;
    Vector3 vector3 = Vector3.op_Subtraction(point2, this.targetPos);
    if ((double) ((Vector3) ref vector3).magnitude < 1.4012984643248171E-45)
      return;
    this.targetPos = point2;
  }

  protected virtual bool IsInteractive() => true;

  protected Vector3 GetCameraMove(InputManager.TouchInfo info)
  {
    return this.GetCameraMove(Vector2.op_Subtraction(info.position, info.move), info.position);
  }

  protected Vector3 GetCameraMove(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1)
  {
    Vector2 vector2_1 = Vector2.op_Subtraction(touch_info0.position, touch_info0.move);
    Vector2 position1 = touch_info0.position;
    Vector2 vector2_2 = Vector2.op_Subtraction(touch_info1.position, touch_info1.move);
    Vector2 position2 = touch_info1.position;
    return this.GetCameraMove(Vector2.op_Multiply(Vector2.op_Addition(vector2_1, vector2_2), 0.5f), Vector2.op_Multiply(Vector2.op_Addition(position1, position2), 0.5f));
  }

  protected virtual Vector3 GetCameraMove(Vector2 old_screen_pos, Vector2 now_screen_pos)
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
    cameraMove.y = 0.0f;
    cameraMove.z = -cameraMove.z;
    return cameraMove;
  }

  protected virtual Vector3 ClampEnableMapArea(Vector3 pos)
  {
    float num = 5f;
    if ((double) pos.x < -(double) num)
      pos.x = -num;
    else if ((double) pos.x > (double) num)
      pos.x = num;
    if ((double) pos.z < -(double) num)
      pos.z = -num;
    else if ((double) pos.z > (double) num)
      pos.z = num;
    return pos;
  }

  protected virtual void UpdateCameraTransform()
  {
    this.cameraTransform.eulerAngles = this.angle;
    this.targetPos = this.ClampEnableMapArea(this.targetPos);
    this.cameraTransform.position = Vector3.op_Subtraction(this.targetPos, Vector3.op_Multiply(this.cameraTransform.forward, this.distance));
  }

  private void FixedUpdate()
  {
    if (!Vector3.op_Inequality(this.cameraMove, Vector3.zero))
      return;
    this.cameraMove = Vector3.op_Multiply(this.cameraMove, 0.75f);
    this.targetPos = Vector3.op_Addition(this.targetPos, this.cameraMove);
  }

  private void Update() => this.UpdateCameraTransform();
}

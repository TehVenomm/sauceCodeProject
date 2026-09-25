// Decompiled with JetBrains decompiler
// Type: RailCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RailCamera : MonoBehaviour
{
  public float moveCoef = -0.3f;
  public float easeCoef = 0.9f;
  private float moveRate;

  public bool enableInput { get; set; }

  public Camera targetCamera { get; private set; }

  public Transform targetCameraTransform { get; private set; }

  public RailAnimation railAnim { get; private set; }

  private void Awake()
  {
    this.targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    this.targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    this.railAnim = ((Component) this.targetCameraTransform).gameObject.AddComponent<RailAnimation>();
    this.enableInput = true;
  }

  private void FixedUpdate()
  {
    if ((double) this.moveRate == 0.0)
      return;
    this.moveRate *= this.easeCoef;
    this.railAnim.rate += this.moveRate;
  }

  private void OnEnable()
  {
    InputManager.OnTouchOn += new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTouchOff += new InputManager.OnTouchDelegate(this.OnTouchOff);
    InputManager.OnDrag += new InputManager.OnTouchDelegate(this.OnDrag);
    this.railAnim.enabledRail = true;
  }

  private void OnDisable()
  {
    InputManager.OnTouchOn -= new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTouchOff -= new InputManager.OnTouchDelegate(this.OnTouchOff);
    InputManager.OnDrag -= new InputManager.OnTouchDelegate(this.OnDrag);
    this.railAnim.enabledRail = false;
    this.Stop();
  }

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    Object.DestroyImmediate((Object) this.railAnim);
  }

  private void OnTouchOn(InputManager.TouchInfo info)
  {
    if (!this.enableInput)
      return;
    this.Stop();
  }

  private void OnTouchOff(InputManager.TouchInfo info)
  {
    if (!this.enableInput)
      return;
    this.moveRate = this.GetMove(info);
  }

  private void OnDrag(InputManager.TouchInfo info)
  {
    if (!this.enableInput)
      return;
    this.railAnim.rate += this.GetMove(info);
  }

  protected virtual float GetMove(InputManager.TouchInfo info)
  {
    Vector2 vector2 = Vector2.op_Subtraction(info.position, info.move);
    Vector2 position = info.position;
    vector2.y = 0.0f;
    position.y = 0.0f;
    float y = this.targetCameraTransform.position.y;
    Vector3 worldPoint = this.targetCamera.ScreenToWorldPoint(vector2.ToVector3XY(y));
    Vector3 vector3 = Vector3.op_Subtraction(this.targetCamera.ScreenToWorldPoint(position.ToVector3XY(y)), worldPoint);
    vector3.y = 0.0f;
    float num = ((Vector3) ref vector3).magnitude;
    if ((double) info.move.x < 0.0)
      num = -num;
    return num * this.moveCoef;
  }

  public void Stop() => this.moveRate = 0.0f;
}

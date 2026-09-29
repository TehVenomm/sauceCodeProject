// Decompiled with JetBrains decompiler
// Type: PuniConManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PuniConManager : MonoBehaviourSingleton<PuniConManager>
{
  [SerializeField]
  private PuniController punicon;
  protected InputManager.TouchInfo link_info;

  public bool enableMultiTouch { get; set; }

  protected override void Awake()
  {
    base.Awake();
    Camera camera = !MonoBehaviourSingleton<UIManager>.IsValid() ? MonoBehaviourSingleton<AppMain>.I.mainCamera : MonoBehaviourSingleton<UIManager>.I.uiCamera;
    ((Component) this.punicon).transform.position = ((Component) camera).transform.position;
    this.punicon.uiCamera = camera;
    this.link_info = (InputManager.TouchInfo) null;
    InputManager.OnTouchOn += new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTouchOff += new InputManager.OnTouchDelegate(this.OnTouchOff);
    InputManager.OnDrag += new InputManager.OnTouchDelegate(this.OnDrag);
    InputManager.OnLongTouch += new InputManager.OnTouchDelegate(this.OnLongTouch);
    InputManager.OnDoubleDrag += new InputManager.OnDoubleTouchDelegate(this.OnDoubleDrag);
  }

  protected override void _OnDestroy()
  {
    base._OnDestroy();
    InputManager.OnTouchOn -= new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTouchOff -= new InputManager.OnTouchDelegate(this.OnTouchOff);
    InputManager.OnDrag -= new InputManager.OnTouchDelegate(this.OnDrag);
    InputManager.OnLongTouch -= new InputManager.OnTouchDelegate(this.OnLongTouch);
    InputManager.OnDoubleDrag -= new InputManager.OnDoubleTouchDelegate(this.OnDoubleDrag);
  }

  public void Update()
  {
    bool flag = !this.enableMultiTouch && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() >= 2;
    if (this.link_info != null && (this.link_info.id == -1 || flag))
    {
      this.punicon.Reset();
      this.link_info = (InputManager.TouchInfo) null;
    }
    if (this.link_info != null || flag)
      return;
    InputManager.TouchInfo activeInfo = MonoBehaviourSingleton<InputManager>.I.GetActiveInfo(true);
    if (activeInfo == null)
      return;
    this.link_info = activeInfo;
    this.punicon.SetStartPosition(Vector2.op_Implicit(this.link_info.beginPosition));
    if (!this.link_info.activeAxis)
      return;
    this.punicon.SetEndPosition(Vector2.op_Implicit(this.link_info.position));
  }

  public void OnTouchOn(InputManager.TouchInfo touch_info)
  {
    if (this.link_info != null || !this.enableMultiTouch)
      return;
    this.punicon.SetStartPosition(Vector2.op_Implicit(touch_info.position));
    this.link_info = touch_info;
  }

  public void OnTouchOff(InputManager.TouchInfo touch_info)
  {
    if (touch_info != this.link_info)
      return;
    this.punicon.Reset();
    this.link_info = (InputManager.TouchInfo) null;
  }

  public void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (touch_info != this.link_info || !this.enableMultiTouch && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() >= 2)
      return;
    this.punicon.SetEndPosition(Vector2.op_Implicit(touch_info.position));
  }

  public void OnLongTouch(InputManager.TouchInfo touch_info)
  {
  }

  public void OnDoubleDrag(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
  {
    if (!this.enableMultiTouch)
      return;
    if (touch_info0 == this.link_info && touch_info0.enable)
    {
      this.punicon.SetEndPosition(Vector2.op_Implicit(touch_info0.position));
    }
    else
    {
      if (touch_info1 != this.link_info || !touch_info1.enable)
        return;
      this.punicon.SetEndPosition(Vector2.op_Implicit(touch_info1.position));
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: InputManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InputManager : MonoBehaviourSingleton<InputManager>
{
  private const int PREV_POSITIONS_NUM = 3;
  public static float pxRate = 1f;
  public static InputManager.OnTouchDelegate OnTap;
  public static InputManager.OnTouchDelegate OnDoubleTap;
  public static InputManager.OnStickDelegate OnStick;
  public static InputManager.OnTouchDelegate OnLongTouch;
  public static InputManager.OnFlickDelegate OnFlick;
  public static InputManager.OnTouchDelegate OnTouchOn;
  public static InputManager.OnTouchDelegate OnTouchOff;
  public static InputManager.OnTouchDelegate OnDrag;
  public static InputManager.OnDoubleTouchDelegate OnDoubleDrag;
  public static InputManager.OnPinchDelegate OnPinch;
  public static InputManager.OnTouchDelegate OnTouchOnAlways;
  public static InputManager.OnTouchDelegate OnTouchOffAlways;
  public static InputManager.OnTouchDelegate OnDragAlways;
  public int enableTouchCount = 2;
  public int enableStickCount = 1;
  [Tooltip("ドラッグやピンチ判定のタッチ移動距離")]
  public float dragThresholdLength = 1f;
  [Tooltip("バーチャルパッドの最大距離")]
  public float stickMaxLength = 80f;
  [Tooltip("バーチャルパッド判定のタッチ移動距離")]
  public float stickThresholdLength = 20f;
  [Tooltip("フリック判定のタッチ移動距離、感度高")]
  public float flickThresholdLengthHigh = 8f;
  [Tooltip("フリック判定のタッチ移動距離、感度低")]
  public float flickThresholdLengthLow = 24f;
  [Tooltip("フリック判定の速度、感度高")]
  public float flickThresholdSpeedHigh = 200f;
  [Tooltip("フリック判定の速度、感度低")]
  public float flickThresholdSpeedLow = 600f;
  [Tooltip("フリックを距離と速度のどちらで判定するか分ける時間")]
  public float flickLimitTime = 0.2f;
  [Tooltip("長押し判定の時間")]
  public float longTouchTime = 0.25f;
  [Tooltip("ダブルタップの各タップの許容時間")]
  public float doubleTapSingleTime = 0.15f;
  [Tooltip("ダブルタップの全体の許容時間")]
  public float doubleTapEnableTime = 0.4f;
  private float doubleTapBeginTime;
  private int doubleTapCount;
  private float scaledDragThresholdLength;
  private float scaledStickMaxLength;
  private float scaledStickThresholdLength;
  private float scaledFlickThresholdLength;
  private float scaledFlickThresholdSpeed;
  private InputManager.TouchInfo[] touchInfos;
  private int unique;
  private bool _untouch;

  protected override void OnDestroySingleton()
  {
    InputManager.OnTap = (InputManager.OnTouchDelegate) null;
    InputManager.OnDoubleTap = (InputManager.OnTouchDelegate) null;
    InputManager.OnStick = (InputManager.OnStickDelegate) null;
    InputManager.OnLongTouch = (InputManager.OnTouchDelegate) null;
    InputManager.OnFlick = (InputManager.OnFlickDelegate) null;
    InputManager.OnTouchOn = (InputManager.OnTouchDelegate) null;
    InputManager.OnTouchOff = (InputManager.OnTouchDelegate) null;
    InputManager.OnDrag = (InputManager.OnTouchDelegate) null;
    InputManager.OnDoubleDrag = (InputManager.OnDoubleTouchDelegate) null;
    InputManager.OnPinch = (InputManager.OnPinchDelegate) null;
    InputManager.OnTouchOnAlways = (InputManager.OnTouchDelegate) null;
    InputManager.OnTouchOffAlways = (InputManager.OnTouchDelegate) null;
    InputManager.OnDragAlways = (InputManager.OnTouchDelegate) null;
  }

  public INPUT_DISABLE_FACTOR disableFlags { get; private set; }

  public bool IsDisable() => this.disableFlags != 0;

  public void SetDisable(INPUT_DISABLE_FACTOR factor, bool disable)
  {
    if (disable)
      this.disableFlags |= factor;
    else
      this.disableFlags &= ~factor;
  }

  public bool IsActiveStick()
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
        return true;
    }
    return false;
  }

  public Vector2 GetStickVector()
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
        return touchInfo.axis;
    }
    return Vector2.zero;
  }

  public InputManager.TouchInfo GetStickInfo()
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
        return touchInfo;
    }
    return (InputManager.TouchInfo) null;
  }

  public bool IsTouch()
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id != -1 && touchInfo.enable)
        return true;
    }
    return false;
  }

  public bool IsTouchIgnoreHit()
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      if (this.touchInfos[index].id != -1)
        return true;
    }
    return false;
  }

  public int GetActiveInfoCount()
  {
    int activeInfoCount = 0;
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      if (this.touchInfos[index].id != -1)
        ++activeInfoCount;
    }
    return activeInfoCount;
  }

  public InputManager.TouchInfo GetActiveInfo(bool check_enable = false)
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id != -1 && (!check_enable || touchInfo.enable))
        return touchInfo;
    }
    return (InputManager.TouchInfo) null;
  }

  private InputManager.TouchInfo GetInfo(int id)
  {
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id == id)
        return touchInfo;
    }
    return (InputManager.TouchInfo) null;
  }

  private void Start()
  {
    if ((double) Screen.dpi > 0.0)
      InputManager.pxRate = Screen.dpi / 160f;
    this.scaledDragThresholdLength = this.dragThresholdLength * InputManager.pxRate;
    this.scaledStickMaxLength = this.stickMaxLength * InputManager.pxRate;
    this.scaledStickThresholdLength = this.stickThresholdLength * InputManager.pxRate;
    this.UpdateConfigInput();
    this.touchInfos = new InputManager.TouchInfo[this.enableTouchCount];
    for (int index = 0; index < this.enableTouchCount; ++index)
      this.touchInfos[index] = new InputManager.TouchInfo();
    this.Untouch();
  }

  public void UpdateConfigInput()
  {
    float num = 0.5f;
    if (GameSaveData.instance != null)
      num = GameSaveData.instance.touchInGameFlick;
    this.scaledFlickThresholdLength = (this.flickThresholdLengthLow + (this.flickThresholdLengthHigh - this.flickThresholdLengthLow) * num) * InputManager.pxRate;
    this.scaledFlickThresholdSpeed = (this.flickThresholdSpeedLow + (this.flickThresholdSpeedHigh - this.flickThresholdSpeedLow) * num) * InputManager.pxRate;
  }

  private void Update()
  {
    int touchCount = Input.touchCount;
    if (touchCount > 0 && this.disableFlags == (INPUT_DISABLE_FACTOR) 0)
    {
      for (int index = 0; index < touchCount; ++index)
      {
        UnityEngine.Touch touch = Input.GetTouch(index);
        this.Touch(((UnityEngine.Touch) ref touch).fingerId, ((UnityEngine.Touch) ref touch).phase, ((UnityEngine.Touch) ref touch).position);
      }
    }
    else
      this.Untouch();
    if (InputManager.OnDrag == null && InputManager.OnDoubleDrag == null && InputManager.OnPinch == null && InputManager.OnDragAlways == null)
      return;
    InputManager.TouchInfo touchInfo1 = (InputManager.TouchInfo) null;
    InputManager.TouchInfo touch_info1 = (InputManager.TouchInfo) null;
    int num = 0;
    int index1 = 0;
    for (int length = this.touchInfos.Length; index1 < length; ++index1)
    {
      InputManager.TouchInfo touchInfo2 = this.touchInfos[index1];
      if (touchInfo2.id != -1)
      {
        ++num;
        if (touchInfo1 == null)
          touchInfo1 = touchInfo2;
        else if (touch_info1 == null)
          touch_info1 = touchInfo2;
      }
    }
    switch (num)
    {
      case 1:
        if ((double) touchInfo1.moveLengthTotal <= (double) this.scaledDragThresholdLength || (double) touchInfo1.moveLength <= 0.0)
          break;
        if (touchInfo1.enable && InputManager.OnDrag != null)
          InputManager.OnDrag(touchInfo1);
        if (InputManager.OnDragAlways == null)
          break;
        InputManager.OnDragAlways(touchInfo1);
        break;
      case 2:
        if ((double) touchInfo1.moveLength <= 0.0 && (double) touch_info1.moveLength <= 0.0)
          break;
        bool flag = false;
        if (InputManager.OnPinch != null && ((double) touchInfo1.moveLengthTotal > (double) this.scaledDragThresholdLength || (double) touch_info1.moveLengthTotal > (double) this.scaledDragThresholdLength) && (Vector2.op_Equality(touchInfo1.move, Vector2.zero) || Vector2.op_Equality(touch_info1.move, Vector2.zero) || (double) Vector2.Angle(((Vector2) ref touchInfo1.move).normalized, ((Vector2) ref touch_info1.move).normalized) > 90.0))
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(Vector2.op_Subtraction(touchInfo1.position, touchInfo1.move), Vector2.op_Subtraction(touch_info1.position, touch_info1.move));
          float magnitude = ((Vector2) ref vector2_1).magnitude;
          Vector2 vector2_2 = Vector2.op_Subtraction(touchInfo1.position, touch_info1.position);
          float pinch_length = ((Vector2) ref vector2_2).magnitude - magnitude;
          if ((double) Mathf.Abs(pinch_length) > (double) this.scaledDragThresholdLength)
          {
            this.SetEnableUIInput(false);
            InputManager.OnPinch(touchInfo1, touch_info1, pinch_length);
            flag = true;
          }
        }
        if (InputManager.OnDoubleDrag == null || flag || (double) touchInfo1.moveLengthTotal <= (double) this.scaledDragThresholdLength || (double) touch_info1.moveLengthTotal <= (double) this.scaledDragThresholdLength)
          break;
        this.SetEnableUIInput(false);
        InputManager.OnDoubleDrag(touchInfo1, touch_info1);
        break;
    }
  }

  private void Touch(int id, TouchPhase phase, Vector2 pos)
  {
    switch ((int) phase)
    {
      case 0:
        if (this.GetInfo(id) != null)
          break;
        InputManager.TouchInfo info1 = this.GetInfo(-1);
        if (info1 == null)
          break;
        info1.id = id;
        info1.unique = ++this.unique;
        info1.hit = this.GetHitTransform(Vector2.op_Implicit(pos));
        info1.touchCount = this.GetActiveInfoCount();
        info1.position = pos;
        info1.beginPosition = pos;
        if (info1.prevPositions == null)
          info1.prevPositions = new List<Vector2>();
        else
          info1.prevPositions.Clear();
        info1.move = Vector2.zero;
        info1.moveLength = 0.0f;
        info1.moveLengthTotal = 0.0f;
        info1.beginTime = Time.time;
        info1.endTime = 0.0f;
        info1.activeAxis = false;
        info1.axis = Vector2.zero;
        info1.axisNoLimit = Vector2.zero;
        info1.calledTap = false;
        info1.calledFlick = false;
        info1.calledLongTouch = false;
        if (info1.enable && InputManager.OnTouchOn != null)
          InputManager.OnTouchOn(info1);
        if (InputManager.OnTouchOnAlways != null)
          InputManager.OnTouchOnAlways(info1);
        if (info1.touchCount <= 1)
          break;
        this.doubleTapCount = 0;
        break;
      case 1:
      case 2:
        InputManager.TouchInfo info2 = this.GetInfo(id);
        if (info2 == null)
          break;
        info2.move = Vector2.op_Subtraction(pos, info2.position);
        if (Vector2.op_Inequality(pos, info2.position))
        {
          info2.move = Vector2.op_Subtraction(pos, info2.position);
          info2.moveLength = ((Vector2) ref info2.move).magnitude;
          info2.moveLengthTotal += info2.moveLength;
        }
        else
        {
          info2.move = Vector2.op_Implicit(Vector3.zero);
          info2.moveLength = 0.0f;
        }
        info2.position = pos;
        info2.prevPositions.Insert(0, pos);
        if (info2.prevPositions.Count > 3)
          info2.prevPositions.RemoveRange(3, info2.prevPositions.Count - 3);
        Vector2 vector2 = Vector2.op_Subtraction(info2.position, info2.beginPosition);
        float magnitude = ((Vector2) ref vector2).magnitude;
        if (!info2.activeAxis && (double) magnitude >= (double) this.scaledStickThresholdLength)
          info2.activeAxis = true;
        ((Vector2) ref vector2).Normalize();
        info2.axis = vector2;
        info2.axisNoLimit = vector2;
        if ((double) magnitude < (double) this.scaledStickMaxLength)
        {
          InputManager.TouchInfo touchInfo = info2;
          touchInfo.axis = Vector2.op_Multiply(touchInfo.axis, magnitude / this.scaledStickMaxLength);
        }
        InputManager.TouchInfo touchInfo1 = info2;
        touchInfo1.axisNoLimit = Vector2.op_Multiply(touchInfo1.axisNoLimit, magnitude / this.scaledStickMaxLength);
        if (info2.activeAxis)
        {
          if (!info2.enable || InputManager.OnStick == null)
            break;
          InputManager.OnStick(info2.axis);
          break;
        }
        if ((double) this.longTouchTime <= 0.0 || info2.calledLongTouch || (double) Time.time - (double) info2.beginTime < (double) this.longTouchTime || !info2.enable || InputManager.OnLongTouch == null)
          break;
        InputManager.OnLongTouch(info2);
        info2.calledLongTouch = true;
        break;
      case 3:
      case 4:
        InputManager.TouchInfo info3 = this.GetInfo(id);
        if (info3 == null)
          break;
        info3.position = pos;
        info3.prevPositions.Insert(0, pos);
        if (info3.prevPositions.Count > 3)
          info3.prevPositions.RemoveRange(3, info3.prevPositions.Count - 3);
        info3.endTime = Time.time;
        if ((double) info3.endTime - (double) info3.beginTime <= (double) this.flickLimitTime)
        {
          Vector3 vector3 = Vector2.op_Implicit(Vector2.op_Subtraction(pos, info3.beginPosition));
          if ((double) ((Vector3) ref vector3).magnitude >= (double) this.scaledFlickThresholdLength && info3.enable && InputManager.OnFlick != null)
          {
            InputManager.OnFlick(Vector2.op_Implicit(((Vector3) ref vector3).normalized));
            info3.calledFlick = true;
          }
        }
        else
        {
          int index = info3.prevPositions.Count > 1 ? 1 : 0;
          Vector3 vector3 = Vector2.op_Implicit(Vector2.op_Subtraction(info3.prevPositions[index], info3.prevPositions[info3.prevPositions.Count - 1]));
          if (((double) Time.deltaTime > 0.0 ? (double) ((Vector3) ref vector3).magnitude / (double) Time.deltaTime : 0.0) >= (double) this.scaledFlickThresholdSpeed && info3.enable && InputManager.OnFlick != null)
          {
            InputManager.OnFlick(Vector2.op_Implicit(((Vector3) ref vector3).normalized));
            info3.calledFlick = true;
          }
        }
        if (!info3.activeAxis && !info3.calledFlick && !info3.calledLongTouch && info3.enable && InputManager.OnTap != null)
        {
          InputManager.OnTap(info3);
          info3.calledTap = true;
        }
        if ((double) Time.time - (double) info3.beginTime <= (double) this.doubleTapSingleTime && !info3.calledFlick)
        {
          if (this.doubleTapCount == 0)
          {
            this.doubleTapCount = 1;
            this.doubleTapBeginTime = info3.beginTime;
          }
          else if (this.doubleTapCount == 1)
          {
            if ((double) Time.time - (double) this.doubleTapBeginTime <= (double) this.doubleTapEnableTime)
            {
              if (info3.enable && InputManager.OnDoubleTap != null)
                InputManager.OnDoubleTap(info3);
            }
            else
            {
              this.doubleTapCount = 1;
              this.doubleTapBeginTime = info3.beginTime;
            }
          }
        }
        else
          this.doubleTapCount = 0;
        if (info3.enable && InputManager.OnTouchOff != null)
          InputManager.OnTouchOff(info3);
        if (InputManager.OnTouchOffAlways != null)
          InputManager.OnTouchOffAlways(info3);
        if (info3.touchCount == 1 && !info3.enable)
        {
          info3.id = -1;
          this.Untouch();
        }
        info3.Clear();
        if (this.IsEnableUIInput())
          break;
        int num = 0;
        int index1 = 0;
        for (int length = this.touchInfos.Length; index1 < length; ++index1)
        {
          if (this.touchInfos[index1].enable)
            ++num;
        }
        if (num > 1)
          break;
        this.SetEnableUIInput(true);
        break;
    }
  }

  public void Untouch()
  {
    if (this.touchInfos == null || this._untouch)
      return;
    this._untouch = true;
    int index = 0;
    for (int length = this.touchInfos.Length; index < length; ++index)
    {
      InputManager.TouchInfo touchInfo = this.touchInfos[index];
      if (touchInfo.id != -1)
        this.Touch(touchInfo.id, (TouchPhase) 3, touchInfo.position);
      touchInfo.Clear();
    }
    this.SetEnableUIInput(true);
    this._untouch = false;
  }

  private void SetEnableUIInput(bool is_enable)
  {
    MonoBehaviourSingleton<UIManager>.I.SetEnableUIInput(is_enable);
  }

  private bool IsEnableUIInput() => MonoBehaviourSingleton<UIManager>.I.IsEnableUIInput();

  private Transform GetHitTransform(Vector3 pos)
  {
    RaycastHit raycastHit;
    return !Physics.Raycast(MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenPointToRay(pos), ref raycastHit, 32f) ? (Transform) null : ((Component) ((RaycastHit) ref raycastHit).collider).transform;
  }

  public delegate void OnTouchDelegate(InputManager.TouchInfo touch_info);

  public delegate void OnDoubleTouchDelegate(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1);

  public delegate void OnPinchDelegate(
    InputManager.TouchInfo touch_info0,
    InputManager.TouchInfo touch_info1,
    float pinch_length);

  public delegate void OnStickDelegate(Vector2 stick_vec);

  public delegate void OnFlickDelegate(Vector2 flick_vec);

  public class TouchInfo
  {
    public int id = -1;
    public int unique;
    public Transform hit;
    public int touchCount;
    public Vector2 position;
    public Vector2 beginPosition;
    public List<Vector2> prevPositions;
    public Vector2 move;
    public float moveLength;
    public float moveLengthTotal;
    public float beginTime;
    public float endTime;
    public bool activeAxis;
    public Vector2 axis;
    public Vector2 axisNoLimit;
    public bool calledTap;
    public bool calledFlick;
    public bool calledLongTouch;

    public bool enable => Object.op_Equality((Object) this.hit, (Object) null) && this.id != -1;

    public void Clear()
    {
      this.id = -1;
      this.hit = (Transform) null;
    }
  }
}

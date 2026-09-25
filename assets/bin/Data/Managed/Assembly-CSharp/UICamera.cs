// Decompiled with JetBrains decompiler
// Type: UICamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
[RequireComponent(typeof (Camera))]
public class UICamera : MonoBehaviour
{
  public static BetterList<UICamera> list = new BetterList<UICamera>();
  public static UICamera.GetKeyStateFunc GetKeyDown = new UICamera.GetKeyStateFunc(Input.GetKeyDown);
  public static UICamera.GetKeyStateFunc GetKeyUp = new UICamera.GetKeyStateFunc(Input.GetKeyUp);
  public static UICamera.GetKeyStateFunc GetKey = new UICamera.GetKeyStateFunc(Input.GetKey);
  public static UICamera.GetAxisFunc GetAxis = new UICamera.GetAxisFunc(Input.GetAxis);
  public static UICamera.GetAnyKeyFunc GetAnyKeyDown;
  public static UICamera.OnScreenResize onScreenResize;
  public UICamera.EventType eventType = UICamera.EventType.UI_3D;
  public bool eventsGoToColliders;
  public LayerMask eventReceiverMask = LayerMask.op_Implicit(-1);
  public bool debug;
  public bool useMouse = true;
  public bool useTouch = true;
  public bool allowMultiTouch = true;
  public bool useKeyboard = true;
  public bool useController = true;
  public bool stickyTooltip = true;
  public float tooltipDelay = 1f;
  public bool longPressTooltip;
  public float mouseDragThreshold = 4f;
  public float mouseClickThreshold = 10f;
  public float touchDragThreshold = 40f;
  public float touchClickThreshold = 40f;
  public float rangeDistance = -1f;
  public string horizontalAxisName = "Horizontal";
  public string verticalAxisName = "Vertical";
  public string horizontalPanAxisName;
  public string verticalPanAxisName;
  public string scrollAxisName = "Mouse ScrollWheel";
  public bool commandClick = true;
  public KeyCode submitKey0 = (KeyCode) 13;
  public KeyCode submitKey1 = (KeyCode) 330;
  public KeyCode cancelKey0 = (KeyCode) 27;
  public KeyCode cancelKey1 = (KeyCode) 331;
  public static UICamera.OnCustomInput onCustomInput;
  public static bool showTooltips = true;
  private static bool mDisableController = false;
  private static Vector2 mLastPos = Vector2.zero;
  public static Vector3 lastWorldPosition = Vector3.zero;
  public static RaycastHit lastHit;
  public static UICamera current = (UICamera) null;
  public static Camera currentCamera = (Camera) null;
  public static UICamera.OnSchemeChange onSchemeChange;
  public static int currentTouchID = -100;
  private static KeyCode mCurrentKey = (KeyCode) 48 /*0x30*/;
  public static UICamera.MouseOrTouch currentTouch = (UICamera.MouseOrTouch) null;
  private static bool mInputFocus = false;
  private static GameObject mGenericHandler;
  public static GameObject fallThrough;
  public static UICamera.VoidDelegate onClick;
  public static UICamera.VoidDelegate onDoubleClick;
  public static UICamera.BoolDelegate onHover;
  public static UICamera.BoolDelegate onPress;
  public static UICamera.BoolDelegate onSelect;
  public static UICamera.FloatDelegate onScroll;
  public static UICamera.VectorDelegate onDrag;
  public static UICamera.VoidDelegate onDragStart;
  public static UICamera.ObjectDelegate onDragOver;
  public static UICamera.ObjectDelegate onDragOut;
  public static UICamera.VoidDelegate onDragEnd;
  public static UICamera.ObjectDelegate onDrop;
  public static UICamera.KeyCodeDelegate onKey;
  public static UICamera.KeyCodeDelegate onNavigate;
  public static UICamera.VectorDelegate onPan;
  public static UICamera.BoolDelegate onTooltip;
  public static UICamera.MoveDelegate onMouseMove;
  private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[3]
  {
    new UICamera.MouseOrTouch(),
    new UICamera.MouseOrTouch(),
    new UICamera.MouseOrTouch()
  };
  public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();
  public static List<UICamera.MouseOrTouch> activeTouches = new List<UICamera.MouseOrTouch>();
  private static List<int> mTouchIDs = new List<int>();
  private static int mWidth = 0;
  private static int mHeight = 0;
  private static GameObject mTooltip = (GameObject) null;
  private Camera mCam;
  private static float mTooltipTime = 0.0f;
  private float mNextRaycast;
  public static bool isDragging = false;
  private static GameObject mRayHitObject;
  private static GameObject mHover;
  private static GameObject mSelected;
  private static UICamera.DepthEntry mHit = new UICamera.DepthEntry();
  private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();
  private static Plane m2DPlane = new Plane(Vector3.back, 0.0f);
  private static float mNextEvent = 0.0f;
  private static int mNotifying = 0;
  private static bool mUsingTouchEvents = true;
  public static UICamera.GetTouchCountCallback GetInputTouchCount;
  public static UICamera.GetTouchCallback GetInputTouch;

  [Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
  public bool stickyPress => true;

  public static bool disableController
  {
    get
    {
      return UICamera.mDisableController && Object.op_Equality((Object) UIPopupList.current, (Object) null);
    }
    set => UICamera.mDisableController = value;
  }

  [Obsolete("Use lastEventPosition instead. It handles controller input properly.")]
  public static Vector2 lastTouchPosition
  {
    get => UICamera.mLastPos;
    set => UICamera.mLastPos = value;
  }

  public static Vector2 lastEventPosition
  {
    get
    {
      if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
      {
        GameObject hoveredObject = UICamera.hoveredObject;
        if (Object.op_Inequality((Object) hoveredObject, (Object) null))
        {
          Bounds absoluteWidgetBounds = NGUIMath.CalculateAbsoluteWidgetBounds(hoveredObject.transform);
          return Vector2.op_Implicit(NGUITools.FindCameraForLayer(hoveredObject.layer).WorldToScreenPoint(((Bounds) ref absoluteWidgetBounds).center));
        }
      }
      return UICamera.mLastPos;
    }
    set => UICamera.mLastPos = value;
  }

  public static UICamera.ControlScheme currentScheme
  {
    get
    {
      if (UICamera.mCurrentKey == null)
        return UICamera.ControlScheme.Touch;
      return UICamera.mCurrentKey >= 330 ? UICamera.ControlScheme.Controller : UICamera.ControlScheme.Mouse;
    }
    set
    {
      switch (value)
      {
        case UICamera.ControlScheme.Mouse:
          UICamera.currentKey = (KeyCode) 323;
          break;
        case UICamera.ControlScheme.Touch:
          UICamera.currentKey = (KeyCode) 0;
          break;
        case UICamera.ControlScheme.Controller:
          UICamera.currentKey = (KeyCode) 330;
          break;
        default:
          UICamera.currentKey = (KeyCode) 48 /*0x30*/;
          break;
      }
    }
  }

  public static KeyCode currentKey
  {
    get => UICamera.mCurrentKey;
    set
    {
      if (UICamera.mCurrentKey == value)
        return;
      int currentScheme1 = (int) UICamera.currentScheme;
      UICamera.mCurrentKey = value;
      UICamera.ControlScheme currentScheme2 = UICamera.currentScheme;
      int num = (int) currentScheme2;
      if (currentScheme1 == num)
        return;
      UICamera.HideTooltip();
      if (currentScheme2 == UICamera.ControlScheme.Mouse)
      {
        Cursor.lockState = (CursorLockMode) 1;
        Cursor.visible = true;
      }
      else
      {
        Cursor.visible = false;
        Cursor.lockState = (CursorLockMode) 0;
        UICamera.mMouse[0].ignoreDelta = 2;
      }
      if (UICamera.onSchemeChange == null)
        return;
      UICamera.onSchemeChange();
    }
  }

  public static Ray currentRay
  {
    get
    {
      return !Object.op_Inequality((Object) UICamera.currentCamera, (Object) null) || UICamera.currentTouch == null ? new Ray() : UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos));
    }
  }

  public static bool inputHasFocus
  {
    get
    {
      if (UICamera.mInputFocus)
      {
        if (Object.op_Implicit((Object) UICamera.mSelected) && UICamera.mSelected.activeInHierarchy)
          return true;
        UICamera.mInputFocus = false;
      }
      return false;
    }
  }

  [Obsolete("Use delegates instead such as UICamera.onClick, UICamera.onHover, etc.")]
  public static GameObject genericEventHandler
  {
    get => UICamera.mGenericHandler;
    set => UICamera.mGenericHandler = value;
  }

  private bool handlesEvents => Object.op_Equality((Object) UICamera.eventHandler, (Object) this);

  public Camera cachedCamera
  {
    get
    {
      if (Object.op_Equality((Object) this.mCam, (Object) null))
        this.mCam = ((Component) this).GetComponent<Camera>();
      return this.mCam;
    }
  }

  public static GameObject tooltipObject => UICamera.mTooltip;

  public static bool isOverUI
  {
    get
    {
      if (UICamera.currentTouch != null)
        return UICamera.currentTouch.isOverUI;
      return !Object.op_Equality((Object) UICamera.mHover, (Object) null) && !Object.op_Equality((Object) UICamera.mHover, (Object) UICamera.fallThrough) && Object.op_Inequality((Object) NGUITools.FindInParents<UIRoot>(UICamera.mHover), (Object) null);
    }
  }

  public static GameObject hoveredObject
  {
    get
    {
      if (UICamera.currentTouch != null && UICamera.currentTouch.dragStarted)
        return UICamera.currentTouch.current;
      if (Object.op_Implicit((Object) UICamera.mHover) && UICamera.mHover.activeInHierarchy)
        return UICamera.mHover;
      UICamera.mHover = (GameObject) null;
      return (GameObject) null;
    }
    set
    {
      if (Object.op_Equality((Object) UICamera.mHover, (Object) value))
        return;
      bool flag = false;
      UICamera current = UICamera.current;
      if (UICamera.currentTouch == null)
      {
        flag = true;
        UICamera.currentTouchID = -100;
        UICamera.currentTouch = UICamera.controller;
      }
      UICamera.ShowTooltip((GameObject) null);
      if (Object.op_Implicit((Object) UICamera.mSelected) && UICamera.currentScheme == UICamera.ControlScheme.Controller)
      {
        UICamera.Notify(UICamera.mSelected, "OnSelect", (object) false);
        if (UICamera.onSelect != null)
          UICamera.onSelect(UICamera.mSelected, false);
        UICamera.mSelected = (GameObject) null;
      }
      if (Object.op_Implicit((Object) UICamera.mHover))
      {
        UICamera.Notify(UICamera.mHover, "OnHover", (object) false);
        if (UICamera.onHover != null)
          UICamera.onHover(UICamera.mHover, false);
      }
      UICamera.mHover = value;
      UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
      if (Object.op_Implicit((Object) UICamera.mHover))
      {
        if (Object.op_Inequality((Object) UICamera.mHover, (Object) UICamera.controller.current) && Object.op_Inequality((Object) UICamera.mHover.GetComponent<UIKeyNavigation>(), (Object) null))
          UICamera.controller.current = UICamera.mHover;
        if (flag)
        {
          UICamera uiCamera = Object.op_Inequality((Object) UICamera.mHover, (Object) null) ? UICamera.FindCameraForLayer(UICamera.mHover.layer) : UICamera.list[0];
          if (Object.op_Inequality((Object) uiCamera, (Object) null))
          {
            UICamera.current = uiCamera;
            UICamera.currentCamera = uiCamera.cachedCamera;
          }
        }
        if (UICamera.onHover != null)
          UICamera.onHover(UICamera.mHover, true);
        UICamera.Notify(UICamera.mHover, "OnHover", (object) true);
      }
      if (!flag)
        return;
      UICamera.current = current;
      UICamera.currentCamera = Object.op_Inequality((Object) current, (Object) null) ? current.cachedCamera : (Camera) null;
      UICamera.currentTouch = (UICamera.MouseOrTouch) null;
      UICamera.currentTouchID = -100;
    }
  }

  public static GameObject controllerNavigationObject
  {
    get
    {
      if (Object.op_Implicit((Object) UICamera.controller.current) && UICamera.controller.current.activeInHierarchy)
        return UICamera.controller.current;
      if (UICamera.currentScheme == UICamera.ControlScheme.Controller && Object.op_Inequality((Object) UICamera.current, (Object) null) && UICamera.current.useController && UIKeyNavigation.list.size > 0)
      {
        for (int i = 0; i < UIKeyNavigation.list.size; ++i)
        {
          UIKeyNavigation uiKeyNavigation = UIKeyNavigation.list[i];
          if (Object.op_Implicit((Object) uiKeyNavigation) && uiKeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uiKeyNavigation.startsSelected)
          {
            UICamera.hoveredObject = ((Component) uiKeyNavigation).gameObject;
            UICamera.controller.current = UICamera.mHover;
            return UICamera.mHover;
          }
        }
        if (Object.op_Equality((Object) UICamera.mHover, (Object) null))
        {
          for (int i = 0; i < UIKeyNavigation.list.size; ++i)
          {
            UIKeyNavigation uiKeyNavigation = UIKeyNavigation.list[i];
            if (Object.op_Implicit((Object) uiKeyNavigation) && uiKeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit)
            {
              UICamera.hoveredObject = ((Component) uiKeyNavigation).gameObject;
              UICamera.controller.current = UICamera.mHover;
              return UICamera.mHover;
            }
          }
        }
      }
      UICamera.controller.current = (GameObject) null;
      return (GameObject) null;
    }
    set
    {
      if (Object.op_Inequality((Object) UICamera.controller.current, (Object) value) && Object.op_Implicit((Object) UICamera.controller.current))
      {
        UICamera.Notify(UICamera.controller.current, "OnHover", (object) false);
        if (UICamera.onHover != null)
          UICamera.onHover(UICamera.controller.current, false);
        UICamera.controller.current = (GameObject) null;
      }
      UICamera.hoveredObject = value;
    }
  }

  public static GameObject selectedObject
  {
    get
    {
      if (Object.op_Implicit((Object) UICamera.mSelected) && UICamera.mSelected.activeInHierarchy)
        return UICamera.mSelected;
      UICamera.mSelected = (GameObject) null;
      return (GameObject) null;
    }
    set
    {
      if (Object.op_Equality((Object) UICamera.mSelected, (Object) value))
      {
        UICamera.hoveredObject = value;
        UICamera.controller.current = value;
      }
      else
      {
        UICamera.ShowTooltip((GameObject) null);
        bool flag = false;
        UICamera current = UICamera.current;
        if (UICamera.currentTouch == null)
        {
          flag = true;
          UICamera.currentTouchID = -100;
          UICamera.currentTouch = UICamera.controller;
        }
        UICamera.mInputFocus = false;
        if (Object.op_Implicit((Object) UICamera.mSelected))
        {
          UICamera.Notify(UICamera.mSelected, "OnSelect", (object) false);
          if (UICamera.onSelect != null)
            UICamera.onSelect(UICamera.mSelected, false);
        }
        UICamera.mSelected = value;
        UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
        if (Object.op_Inequality((Object) value, (Object) null) && Object.op_Inequality((Object) value.GetComponent<UIKeyNavigation>(), (Object) null))
          UICamera.controller.current = value;
        if (Object.op_Implicit((Object) UICamera.mSelected) & flag)
        {
          UICamera uiCamera = Object.op_Inequality((Object) UICamera.mSelected, (Object) null) ? UICamera.FindCameraForLayer(UICamera.mSelected.layer) : UICamera.list[0];
          if (Object.op_Inequality((Object) uiCamera, (Object) null))
          {
            UICamera.current = uiCamera;
            UICamera.currentCamera = uiCamera.cachedCamera;
          }
        }
        if (Object.op_Implicit((Object) UICamera.mSelected))
        {
          UICamera.mInputFocus = UICamera.mSelected.activeInHierarchy && Object.op_Inequality((Object) UICamera.mSelected.GetComponent<UIInput>(), (Object) null);
          if (UICamera.onSelect != null)
            UICamera.onSelect(UICamera.mSelected, true);
          UICamera.Notify(UICamera.mSelected, "OnSelect", (object) true);
        }
        if (!flag)
          return;
        UICamera.current = current;
        UICamera.currentCamera = Object.op_Inequality((Object) current, (Object) null) ? current.cachedCamera : (Camera) null;
        UICamera.currentTouch = (UICamera.MouseOrTouch) null;
        UICamera.currentTouchID = -100;
      }
    }
  }

  public static bool IsPressed(GameObject go)
  {
    for (int index = 0; index < 3; ++index)
    {
      if (Object.op_Equality((Object) UICamera.mMouse[index].pressed, (Object) go))
        return true;
    }
    int index1 = 0;
    for (int count = UICamera.activeTouches.Count; index1 < count; ++index1)
    {
      if (Object.op_Equality((Object) UICamera.activeTouches[index1].pressed, (Object) go))
        return true;
    }
    return Object.op_Equality((Object) UICamera.controller.pressed, (Object) go);
  }

  [Obsolete("Use either 'CountInputSources()' or 'activeTouches.Count'")]
  public static int touchCount => UICamera.CountInputSources();

  public static int CountInputSources()
  {
    int num = 0;
    int index1 = 0;
    for (int count = UICamera.activeTouches.Count; index1 < count; ++index1)
    {
      if (Object.op_Inequality((Object) UICamera.activeTouches[index1].pressed, (Object) null))
        ++num;
    }
    for (int index2 = 0; index2 < UICamera.mMouse.Length; ++index2)
    {
      if (Object.op_Inequality((Object) UICamera.mMouse[index2].pressed, (Object) null))
        ++num;
    }
    if (Object.op_Inequality((Object) UICamera.controller.pressed, (Object) null))
      ++num;
    return num;
  }

  public static int dragCount
  {
    get
    {
      int dragCount = 0;
      int index1 = 0;
      for (int count = UICamera.activeTouches.Count; index1 < count; ++index1)
      {
        if (Object.op_Inequality((Object) UICamera.activeTouches[index1].dragged, (Object) null))
          ++dragCount;
      }
      for (int index2 = 0; index2 < UICamera.mMouse.Length; ++index2)
      {
        if (Object.op_Inequality((Object) UICamera.mMouse[index2].dragged, (Object) null))
          ++dragCount;
      }
      if (Object.op_Inequality((Object) UICamera.controller.dragged, (Object) null))
        ++dragCount;
      return dragCount;
    }
  }

  public static Camera mainCamera
  {
    get
    {
      UICamera eventHandler = UICamera.eventHandler;
      return !Object.op_Inequality((Object) eventHandler, (Object) null) ? (Camera) null : eventHandler.cachedCamera;
    }
  }

  public static UICamera eventHandler
  {
    get
    {
      for (int index = 0; index < UICamera.list.size; ++index)
      {
        UICamera eventHandler = UICamera.list.buffer[index];
        if (!Object.op_Equality((Object) eventHandler, (Object) null) && ((Behaviour) eventHandler).enabled && NGUITools.GetActive(((Component) eventHandler).gameObject))
          return eventHandler;
      }
      return (UICamera) null;
    }
  }

  private static int CompareFunc(UICamera a, UICamera b)
  {
    if ((double) a.cachedCamera.depth < (double) b.cachedCamera.depth)
      return 1;
    return (double) a.cachedCamera.depth > (double) b.cachedCamera.depth ? -1 : 0;
  }

  private static Rigidbody FindRootRigidbody(Transform trans)
  {
    for (; Object.op_Inequality((Object) trans, (Object) null); trans = trans.parent)
    {
      if (Object.op_Inequality((Object) ((Component) trans).GetComponent<UIPanel>(), (Object) null))
        return (Rigidbody) null;
      Rigidbody component = ((Component) trans).GetComponent<Rigidbody>();
      if (Object.op_Inequality((Object) component, (Object) null))
        return component;
    }
    return (Rigidbody) null;
  }

  private static Rigidbody2D FindRootRigidbody2D(Transform trans)
  {
    for (; Object.op_Inequality((Object) trans, (Object) null); trans = trans.parent)
    {
      if (Object.op_Inequality((Object) ((Component) trans).GetComponent<UIPanel>(), (Object) null))
        return (Rigidbody2D) null;
      Rigidbody2D component = ((Component) trans).GetComponent<Rigidbody2D>();
      if (Object.op_Inequality((Object) component, (Object) null))
        return component;
    }
    return (Rigidbody2D) null;
  }

  public static void Raycast(UICamera.MouseOrTouch touch)
  {
    if (!UICamera.Raycast(Vector2.op_Implicit(touch.pos)))
      UICamera.mRayHitObject = UICamera.fallThrough;
    if (Object.op_Equality((Object) UICamera.mRayHitObject, (Object) null))
      UICamera.mRayHitObject = UICamera.mGenericHandler;
    touch.last = touch.current;
    touch.current = UICamera.mRayHitObject;
    UICamera.mLastPos = touch.pos;
  }

  public static bool Raycast(Vector3 inPos)
  {
    for (int index1 = 0; index1 < UICamera.list.size; ++index1)
    {
      UICamera uiCamera = UICamera.list.buffer[index1];
      if (((Behaviour) uiCamera).enabled && NGUITools.GetActive(((Component) uiCamera).gameObject))
      {
        UICamera.currentCamera = uiCamera.cachedCamera;
        Vector3 viewportPoint = UICamera.currentCamera.ScreenToViewportPoint(inPos);
        if (!float.IsNaN(viewportPoint.x) && !float.IsNaN(viewportPoint.y) && (double) viewportPoint.x >= 0.0 && (double) viewportPoint.x <= 1.0 && (double) viewportPoint.y >= 0.0 && (double) viewportPoint.y <= 1.0)
        {
          Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
          int num1 = UICamera.currentCamera.cullingMask & LayerMask.op_Implicit(uiCamera.eventReceiverMask);
          float num2 = (double) uiCamera.rangeDistance > 0.0 ? uiCamera.rangeDistance : UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane;
          if (uiCamera.eventType == UICamera.EventType.World_3D)
          {
            if (Physics.Raycast(ray, ref UICamera.lastHit, num2, num1))
            {
              UICamera.lastWorldPosition = ((RaycastHit) ref UICamera.lastHit).point;
              UICamera.mRayHitObject = ((Component) ((RaycastHit) ref UICamera.lastHit).collider).gameObject;
              if (!UICamera.list[0].eventsGoToColliders)
              {
                Rigidbody rootRigidbody = UICamera.FindRootRigidbody(UICamera.mRayHitObject.transform);
                if (Object.op_Inequality((Object) rootRigidbody, (Object) null))
                  UICamera.mRayHitObject = ((Component) rootRigidbody).gameObject;
              }
              return true;
            }
          }
          else if (uiCamera.eventType == UICamera.EventType.UI_3D)
          {
            RaycastHit[] raycastHitArray = Physics.RaycastAll(ray, num2, num1);
            if (raycastHitArray.Length > 1)
            {
              for (int index2 = 0; index2 < raycastHitArray.Length; ++index2)
              {
                GameObject gameObject = ((Component) ((RaycastHit) ref raycastHitArray[index2]).collider).gameObject;
                UIWidget component = gameObject.GetComponent<UIWidget>();
                if (Object.op_Inequality((Object) component, (Object) null))
                {
                  if (!component.isVisible || component.hitCheck != null && !component.hitCheck(((RaycastHit) ref raycastHitArray[index2]).point))
                    continue;
                }
                else
                {
                  UIRect inParents = NGUITools.FindInParents<UIRect>(gameObject);
                  if (Object.op_Inequality((Object) inParents, (Object) null) && (double) inParents.finalAlpha < 1.0 / 1000.0)
                    continue;
                }
                UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
                if (UICamera.mHit.depth != int.MaxValue)
                {
                  UICamera.mHit.hit = raycastHitArray[index2];
                  UICamera.mHit.point = ((RaycastHit) ref raycastHitArray[index2]).point;
                  UICamera.mHit.go = ((Component) ((RaycastHit) ref raycastHitArray[index2]).collider).gameObject;
                  UICamera.mHits.Add(UICamera.mHit);
                }
              }
              UICamera.mHits.Sort((BetterList<UICamera.DepthEntry>.CompareFunc) ((r1, r2) => r2.depth.CompareTo(r1.depth)));
              for (int i = 0; i < UICamera.mHits.size; ++i)
              {
                if (UICamera.IsVisible(ref UICamera.mHits.buffer[i]))
                {
                  UICamera.lastHit = UICamera.mHits[i].hit;
                  UICamera.mRayHitObject = UICamera.mHits[i].go;
                  UICamera.lastWorldPosition = UICamera.mHits[i].point;
                  UICamera.mHits.Clear();
                  return true;
                }
              }
              UICamera.mHits.Clear();
            }
            else if (raycastHitArray.Length == 1)
            {
              GameObject gameObject = ((Component) ((RaycastHit) ref raycastHitArray[0]).collider).gameObject;
              UIWidget component = gameObject.GetComponent<UIWidget>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                if (!component.isVisible || component.hitCheck != null && !component.hitCheck(((RaycastHit) ref raycastHitArray[0]).point))
                  continue;
              }
              else
              {
                UIRect inParents = NGUITools.FindInParents<UIRect>(gameObject);
                if (Object.op_Inequality((Object) inParents, (Object) null) && (double) inParents.finalAlpha < 1.0 / 1000.0)
                  continue;
              }
              if (UICamera.IsVisible(((RaycastHit) ref raycastHitArray[0]).point, ((Component) ((RaycastHit) ref raycastHitArray[0]).collider).gameObject))
              {
                UICamera.lastHit = raycastHitArray[0];
                UICamera.lastWorldPosition = ((RaycastHit) ref raycastHitArray[0]).point;
                UICamera.mRayHitObject = ((Component) ((RaycastHit) ref UICamera.lastHit).collider).gameObject;
                return true;
              }
            }
          }
          else if (uiCamera.eventType == UICamera.EventType.World_2D)
          {
            if (((Plane) ref UICamera.m2DPlane).Raycast(ray, ref num2))
            {
              Vector3 point = ((Ray) ref ray).GetPoint(num2);
              Collider2D collider2D = Physics2D.OverlapPoint(Vector2.op_Implicit(point), num1);
              if (Object.op_Implicit((Object) collider2D))
              {
                UICamera.lastWorldPosition = point;
                UICamera.mRayHitObject = ((Component) collider2D).gameObject;
                if (!uiCamera.eventsGoToColliders)
                {
                  Rigidbody2D rootRigidbody2D = UICamera.FindRootRigidbody2D(UICamera.mRayHitObject.transform);
                  if (Object.op_Inequality((Object) rootRigidbody2D, (Object) null))
                    UICamera.mRayHitObject = ((Component) rootRigidbody2D).gameObject;
                }
                return true;
              }
            }
          }
          else if (uiCamera.eventType == UICamera.EventType.UI_2D && ((Plane) ref UICamera.m2DPlane).Raycast(ray, ref num2))
          {
            UICamera.lastWorldPosition = ((Ray) ref ray).GetPoint(num2);
            Collider2D[] collider2DArray = Physics2D.OverlapPointAll(Vector2.op_Implicit(UICamera.lastWorldPosition), num1);
            if (collider2DArray.Length > 1)
            {
              for (int index3 = 0; index3 < collider2DArray.Length; ++index3)
              {
                GameObject gameObject = ((Component) collider2DArray[index3]).gameObject;
                UIWidget component = gameObject.GetComponent<UIWidget>();
                if (Object.op_Inequality((Object) component, (Object) null))
                {
                  if (!component.isVisible || component.hitCheck != null && !component.hitCheck(UICamera.lastWorldPosition))
                    continue;
                }
                else
                {
                  UIRect inParents = NGUITools.FindInParents<UIRect>(gameObject);
                  if (Object.op_Inequality((Object) inParents, (Object) null) && (double) inParents.finalAlpha < 1.0 / 1000.0)
                    continue;
                }
                UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
                if (UICamera.mHit.depth != int.MaxValue)
                {
                  UICamera.mHit.go = gameObject;
                  UICamera.mHit.point = UICamera.lastWorldPosition;
                  UICamera.mHits.Add(UICamera.mHit);
                }
              }
              UICamera.mHits.Sort((BetterList<UICamera.DepthEntry>.CompareFunc) ((r1, r2) => r2.depth.CompareTo(r1.depth)));
              for (int i = 0; i < UICamera.mHits.size; ++i)
              {
                if (UICamera.IsVisible(ref UICamera.mHits.buffer[i]))
                {
                  UICamera.mRayHitObject = UICamera.mHits[i].go;
                  UICamera.mHits.Clear();
                  return true;
                }
              }
              UICamera.mHits.Clear();
            }
            else if (collider2DArray.Length == 1)
            {
              GameObject gameObject = ((Component) collider2DArray[0]).gameObject;
              UIWidget component = gameObject.GetComponent<UIWidget>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                if (!component.isVisible || component.hitCheck != null && !component.hitCheck(UICamera.lastWorldPosition))
                  continue;
              }
              else
              {
                UIRect inParents = NGUITools.FindInParents<UIRect>(gameObject);
                if (Object.op_Inequality((Object) inParents, (Object) null) && (double) inParents.finalAlpha < 1.0 / 1000.0)
                  continue;
              }
              if (UICamera.IsVisible(UICamera.lastWorldPosition, gameObject))
              {
                UICamera.mRayHitObject = gameObject;
                return true;
              }
            }
          }
        }
      }
    }
    return false;
  }

  private static bool IsVisible(Vector3 worldPoint, GameObject go)
  {
    for (UIPanel uiPanel = NGUITools.FindInParents<UIPanel>(go); Object.op_Inequality((Object) uiPanel, (Object) null); uiPanel = uiPanel.parentPanel)
    {
      if (!uiPanel.IsVisible(worldPoint))
        return false;
    }
    return true;
  }

  private static bool IsVisible(ref UICamera.DepthEntry de)
  {
    for (UIPanel uiPanel = NGUITools.FindInParents<UIPanel>(de.go); Object.op_Inequality((Object) uiPanel, (Object) null); uiPanel = uiPanel.parentPanel)
    {
      if (!uiPanel.IsVisible(de.point))
        return false;
    }
    return true;
  }

  public static bool IsHighlighted(GameObject go)
  {
    return Object.op_Equality((Object) UICamera.hoveredObject, (Object) go);
  }

  public static UICamera FindCameraForLayer(int layer)
  {
    int num = 1 << layer;
    for (int index = 0; index < UICamera.list.size; ++index)
    {
      UICamera cameraForLayer = UICamera.list.buffer[index];
      Camera cachedCamera = cameraForLayer.cachedCamera;
      if (Object.op_Inequality((Object) cachedCamera, (Object) null) && (cachedCamera.cullingMask & num) != 0)
        return cameraForLayer;
    }
    return (UICamera) null;
  }

  private static int GetDirection(KeyCode up, KeyCode down)
  {
    if (UICamera.GetKeyDown(up))
    {
      UICamera.currentKey = up;
      return 1;
    }
    if (!UICamera.GetKeyDown(down))
      return 0;
    UICamera.currentKey = down;
    return -1;
  }

  private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
  {
    if (UICamera.GetKeyDown(up0))
    {
      UICamera.currentKey = up0;
      return 1;
    }
    if (UICamera.GetKeyDown(up1))
    {
      UICamera.currentKey = up1;
      return 1;
    }
    if (UICamera.GetKeyDown(down0))
    {
      UICamera.currentKey = down0;
      return -1;
    }
    if (!UICamera.GetKeyDown(down1))
      return 0;
    UICamera.currentKey = down1;
    return -1;
  }

  private static int GetDirection(string axis)
  {
    float time = RealTime.time;
    if ((double) UICamera.mNextEvent < (double) time && !string.IsNullOrEmpty(axis))
    {
      float num = UICamera.GetAxis(axis);
      if ((double) num > 0.75)
      {
        UICamera.currentKey = (KeyCode) 330;
        UICamera.mNextEvent = time + 0.25f;
        return 1;
      }
      if ((double) num < -0.75)
      {
        UICamera.currentKey = (KeyCode) 330;
        UICamera.mNextEvent = time + 0.25f;
        return -1;
      }
    }
    return 0;
  }

  public static void Notify(GameObject go, string funcName, object obj)
  {
    if (UICamera.mNotifying > 10)
      return;
    if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UIPopupList.isOpen && Object.op_Equality((Object) UIPopupList.current.source, (Object) go) && UIPopupList.isOpen)
      go = ((Component) UIPopupList.current).gameObject;
    if (!Object.op_Implicit((Object) go) || !go.activeInHierarchy)
      return;
    ++UICamera.mNotifying;
    go.SendMessage(funcName, obj, (SendMessageOptions) 1);
    if (Object.op_Inequality((Object) UICamera.mGenericHandler, (Object) null) && Object.op_Inequality((Object) UICamera.mGenericHandler, (Object) go))
      UICamera.mGenericHandler.SendMessage(funcName, obj, (SendMessageOptions) 1);
    --UICamera.mNotifying;
  }

  public static UICamera.MouseOrTouch GetMouse(int button) => UICamera.mMouse[button];

  public static UICamera.MouseOrTouch GetTouch(int id, bool createIfMissing = false)
  {
    if (id < 0)
      return UICamera.GetMouse(-id - 1);
    int index = 0;
    for (int count = UICamera.mTouchIDs.Count; index < count; ++index)
    {
      if (UICamera.mTouchIDs[index] == id)
        return UICamera.activeTouches[index];
    }
    if (!createIfMissing)
      return (UICamera.MouseOrTouch) null;
    UICamera.MouseOrTouch touch = new UICamera.MouseOrTouch();
    touch.pressTime = RealTime.time;
    touch.touchBegan = true;
    UICamera.activeTouches.Add(touch);
    UICamera.mTouchIDs.Add(id);
    return touch;
  }

  public static void RemoveTouch(int id)
  {
    int index = 0;
    for (int count = UICamera.mTouchIDs.Count; index < count; ++index)
    {
      if (UICamera.mTouchIDs[index] == id)
      {
        UICamera.mTouchIDs.RemoveAt(index);
        UICamera.activeTouches.RemoveAt(index);
        break;
      }
    }
  }

  private void Awake()
  {
    UICamera.mWidth = Screen.width;
    UICamera.mHeight = Screen.height;
    UICamera.mMouse[0].pos = Vector2.op_Implicit(Input.mousePosition);
    for (int index = 1; index < 3; ++index)
    {
      UICamera.mMouse[index].pos = UICamera.mMouse[0].pos;
      UICamera.mMouse[index].lastPos = UICamera.mMouse[0].pos;
    }
    UICamera.mLastPos = UICamera.mMouse[0].pos;
  }

  private void OnEnable()
  {
    UICamera.list.Add(this);
    UICamera.list.Sort(new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc));
  }

  private void OnDisable() => UICamera.list.Remove(this);

  private void Start()
  {
    if (this.eventType != UICamera.EventType.World_3D && this.cachedCamera.transparencySortMode != 2)
      this.cachedCamera.transparencySortMode = (TransparencySortMode) 2;
    if (!Application.isPlaying)
      return;
    if (Object.op_Equality((Object) UICamera.fallThrough, (Object) null))
    {
      UIRoot inParents = NGUITools.FindInParents<UIRoot>(((Component) this).gameObject);
      if (Object.op_Inequality((Object) inParents, (Object) null))
      {
        UICamera.fallThrough = ((Component) inParents).gameObject;
      }
      else
      {
        Transform transform = ((Component) this).transform;
        UICamera.fallThrough = Object.op_Inequality((Object) transform.parent, (Object) null) ? ((Component) transform.parent).gameObject : ((Component) this).gameObject;
      }
    }
    this.cachedCamera.eventMask = 0;
  }

  private void Update()
  {
    if (!this.handlesEvents)
      return;
    UICamera.current = this;
    NGUIDebug.debugRaycast = this.debug;
    if (this.useTouch)
      this.ProcessTouches();
    else if (this.useMouse)
      this.ProcessMouse();
    if (UICamera.onCustomInput != null)
      UICamera.onCustomInput();
    if ((this.useKeyboard || this.useController) && !UICamera.disableController)
      this.ProcessOthers();
    if (this.useMouse && Object.op_Inequality((Object) UICamera.mHover, (Object) null))
    {
      float delta = !string.IsNullOrEmpty(this.scrollAxisName) ? UICamera.GetAxis(this.scrollAxisName) : 0.0f;
      if ((double) delta != 0.0)
      {
        if (UICamera.onScroll != null)
          UICamera.onScroll(UICamera.mHover, delta);
        UICamera.Notify(UICamera.mHover, "OnScroll", (object) delta);
      }
      if (UICamera.showTooltips && (double) UICamera.mTooltipTime != 0.0 && !UIPopupList.isOpen && ((double) UICamera.mTooltipTime < (double) RealTime.time || UICamera.GetKey((KeyCode) 304) || UICamera.GetKey((KeyCode) 303)))
      {
        UICamera.currentTouch = UICamera.mMouse[0];
        UICamera.currentTouchID = -1;
        UICamera.ShowTooltip(UICamera.mHover);
      }
    }
    if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null) && !NGUITools.GetActive(UICamera.mTooltip))
      UICamera.ShowTooltip((GameObject) null);
    UICamera.current = (UICamera) null;
    UICamera.currentTouchID = -100;
  }

  private void LateUpdate()
  {
    if (!this.handlesEvents)
      return;
    int width = Screen.width;
    int height = Screen.height;
    if (width == UICamera.mWidth && height == UICamera.mHeight)
      return;
    UICamera.mWidth = width;
    UICamera.mHeight = height;
    UIRoot.Broadcast("UpdateAnchors");
    if (UICamera.onScreenResize == null)
      return;
    UICamera.onScreenResize();
  }

  public void ProcessMouse()
  {
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < 3; ++index)
    {
      if (Input.GetMouseButtonDown(index))
      {
        UICamera.currentKey = (KeyCode) (323 + index);
        flag2 = true;
        flag1 = true;
      }
      else if (Input.GetMouseButton(index))
      {
        UICamera.currentKey = (KeyCode) (323 + index);
        flag1 = true;
      }
    }
    if (UICamera.currentScheme == UICamera.ControlScheme.Touch)
      return;
    UICamera.currentTouch = UICamera.mMouse[0];
    Vector2 vector2 = Vector2.op_Implicit(Input.mousePosition);
    if (UICamera.currentTouch.ignoreDelta == 0)
    {
      UICamera.currentTouch.delta = Vector2.op_Subtraction(vector2, UICamera.currentTouch.pos);
    }
    else
    {
      --UICamera.currentTouch.ignoreDelta;
      UICamera.currentTouch.delta.x = 0.0f;
      UICamera.currentTouch.delta.y = 0.0f;
    }
    float sqrMagnitude = ((Vector2) ref UICamera.currentTouch.delta).sqrMagnitude;
    UICamera.currentTouch.pos = vector2;
    UICamera.mLastPos = vector2;
    bool flag3 = false;
    if (UICamera.currentScheme != UICamera.ControlScheme.Mouse)
    {
      if ((double) sqrMagnitude < 1.0 / 1000.0)
        return;
      UICamera.currentKey = (KeyCode) 323;
      flag3 = true;
    }
    else if ((double) sqrMagnitude > 1.0 / 1000.0)
      flag3 = true;
    for (int index = 1; index < 3; ++index)
    {
      UICamera.mMouse[index].pos = UICamera.currentTouch.pos;
      UICamera.mMouse[index].delta = UICamera.currentTouch.delta;
    }
    if (flag1 | flag3 || (double) this.mNextRaycast < (double) RealTime.time)
    {
      this.mNextRaycast = RealTime.time + 0.02f;
      UICamera.Raycast(UICamera.currentTouch);
      for (int index = 0; index < 3; ++index)
        UICamera.mMouse[index].current = UICamera.currentTouch.current;
    }
    bool flag4 = Object.op_Inequality((Object) UICamera.currentTouch.last, (Object) UICamera.currentTouch.current);
    bool flag5 = Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null);
    if (!flag5)
      UICamera.hoveredObject = UICamera.currentTouch.current;
    UICamera.currentTouchID = -1;
    if (flag4)
      UICamera.currentKey = (KeyCode) 323;
    if (!flag1 & flag3 && !this.stickyTooltip | flag4)
    {
      if ((double) UICamera.mTooltipTime != 0.0)
        UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
      else if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null))
        UICamera.ShowTooltip((GameObject) null);
    }
    if (flag3 && UICamera.onMouseMove != null)
    {
      UICamera.onMouseMove(UICamera.currentTouch.delta);
      UICamera.currentTouch = (UICamera.MouseOrTouch) null;
    }
    if (flag4 && (flag2 || flag5 && !flag1))
      UICamera.hoveredObject = (GameObject) null;
    for (int index = 0; index < 3; ++index)
    {
      bool mouseButtonDown = Input.GetMouseButtonDown(index);
      bool mouseButtonUp = Input.GetMouseButtonUp(index);
      if (mouseButtonDown | mouseButtonUp)
        UICamera.currentKey = (KeyCode) (323 + index);
      UICamera.currentTouch = UICamera.mMouse[index];
      UICamera.currentTouchID = -1 - index;
      UICamera.currentKey = (KeyCode) (323 + index);
      if (mouseButtonDown)
      {
        UICamera.currentTouch.pressedCam = UICamera.currentCamera;
        UICamera.currentTouch.pressTime = RealTime.time;
      }
      else if (Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null))
        UICamera.currentCamera = UICamera.currentTouch.pressedCam;
      this.ProcessTouch(mouseButtonDown, mouseButtonUp);
    }
    if (!flag1 & flag4)
    {
      UICamera.currentTouch = UICamera.mMouse[0];
      UICamera.mTooltipTime = RealTime.time + this.tooltipDelay;
      UICamera.currentTouchID = -1;
      UICamera.currentKey = (KeyCode) 323;
      UICamera.hoveredObject = UICamera.currentTouch.current;
    }
    UICamera.currentTouch = (UICamera.MouseOrTouch) null;
    UICamera.mMouse[0].last = UICamera.mMouse[0].current;
    for (int index = 1; index < 3; ++index)
      UICamera.mMouse[index].last = UICamera.mMouse[0].last;
  }

  public void ProcessTouches()
  {
    int num = UICamera.GetInputTouchCount == null ? Input.touchCount : UICamera.GetInputTouchCount();
    for (int index = 0; index < num; ++index)
    {
      TouchPhase phase;
      int fingerId;
      Vector2 position;
      int tapCount;
      if (UICamera.GetInputTouch == null)
      {
        UnityEngine.Touch touch = Input.GetTouch(index);
        phase = ((UnityEngine.Touch) ref touch).phase;
        fingerId = ((UnityEngine.Touch) ref touch).fingerId;
        position = ((UnityEngine.Touch) ref touch).position;
        tapCount = ((UnityEngine.Touch) ref touch).tapCount;
      }
      else
      {
        UICamera.Touch touch = UICamera.GetInputTouch(index);
        phase = touch.phase;
        fingerId = touch.fingerId;
        position = touch.position;
        tapCount = touch.tapCount;
      }
      UICamera.currentTouchID = this.allowMultiTouch ? fingerId : 1;
      UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID, true);
      bool pressed = phase == null || UICamera.currentTouch.touchBegan;
      bool released = phase == 4 || phase == 3;
      UICamera.currentTouch.touchBegan = false;
      UICamera.currentTouch.delta = Vector2.op_Subtraction(position, UICamera.currentTouch.pos);
      UICamera.currentTouch.pos = position;
      UICamera.currentKey = (KeyCode) 0;
      UICamera.Raycast(UICamera.currentTouch);
      if (pressed)
        UICamera.currentTouch.pressedCam = UICamera.currentCamera;
      else if (Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null))
        UICamera.currentCamera = UICamera.currentTouch.pressedCam;
      if (tapCount > 1)
        UICamera.currentTouch.clickTime = RealTime.time;
      this.ProcessTouch(pressed, released);
      if (released)
        UICamera.RemoveTouch(UICamera.currentTouchID);
      UICamera.currentTouch.last = (GameObject) null;
      UICamera.currentTouch = (UICamera.MouseOrTouch) null;
      if (!this.allowMultiTouch)
        break;
    }
    if (num == 0)
    {
      if (UICamera.mUsingTouchEvents)
      {
        UICamera.mUsingTouchEvents = false;
      }
      else
      {
        if (!this.useMouse)
          return;
        this.ProcessMouse();
      }
    }
    else
      UICamera.mUsingTouchEvents = true;
  }

  private void ProcessFakeTouches()
  {
    bool mouseButtonDown = Input.GetMouseButtonDown(0);
    bool mouseButtonUp = Input.GetMouseButtonUp(0);
    bool mouseButton = Input.GetMouseButton(0);
    if (!(mouseButtonDown | mouseButtonUp | mouseButton))
      return;
    UICamera.currentTouchID = 1;
    UICamera.currentTouch = UICamera.mMouse[0];
    UICamera.currentTouch.touchBegan = mouseButtonDown;
    if (mouseButtonDown)
    {
      UICamera.currentTouch.pressTime = RealTime.time;
      UICamera.activeTouches.Add(UICamera.currentTouch);
    }
    Vector2 vector2 = Vector2.op_Implicit(Input.mousePosition);
    UICamera.currentTouch.delta = Vector2.op_Subtraction(vector2, UICamera.currentTouch.pos);
    UICamera.currentTouch.pos = vector2;
    UICamera.Raycast(UICamera.currentTouch);
    if (mouseButtonDown)
      UICamera.currentTouch.pressedCam = UICamera.currentCamera;
    else if (Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null))
      UICamera.currentCamera = UICamera.currentTouch.pressedCam;
    UICamera.currentKey = (KeyCode) 0;
    this.ProcessTouch(mouseButtonDown, mouseButtonUp);
    if (mouseButtonUp)
      UICamera.activeTouches.Remove(UICamera.currentTouch);
    UICamera.currentTouch.last = (GameObject) null;
    UICamera.currentTouch = (UICamera.MouseOrTouch) null;
  }

  public void ProcessOthers()
  {
    UICamera.currentTouchID = -100;
    UICamera.currentTouch = UICamera.controller;
    bool pressed = false;
    bool released = false;
    if (this.submitKey0 != null && UICamera.GetKeyDown(this.submitKey0))
    {
      UICamera.currentKey = this.submitKey0;
      pressed = true;
    }
    else if (this.submitKey1 != null && UICamera.GetKeyDown(this.submitKey1))
    {
      UICamera.currentKey = this.submitKey1;
      pressed = true;
    }
    else if ((this.submitKey0 == 13 || this.submitKey1 == 13) && UICamera.GetKeyDown((KeyCode) 271))
    {
      UICamera.currentKey = this.submitKey0;
      pressed = true;
    }
    if (this.submitKey0 != null && UICamera.GetKeyUp(this.submitKey0))
    {
      UICamera.currentKey = this.submitKey0;
      released = true;
    }
    else if (this.submitKey1 != null && UICamera.GetKeyUp(this.submitKey1))
    {
      UICamera.currentKey = this.submitKey1;
      released = true;
    }
    else if ((this.submitKey0 == 13 || this.submitKey1 == 13) && UICamera.GetKeyUp((KeyCode) 271))
    {
      UICamera.currentKey = this.submitKey0;
      released = true;
    }
    if (pressed)
      UICamera.currentTouch.pressTime = RealTime.time;
    if (pressed | released && UICamera.currentScheme == UICamera.ControlScheme.Controller)
    {
      UICamera.currentTouch.current = UICamera.controllerNavigationObject;
      this.ProcessTouch(pressed, released);
      UICamera.currentTouch.last = UICamera.currentTouch.current;
    }
    KeyCode key1 = (KeyCode) 0;
    if (this.useController)
    {
      if (!UICamera.disableController && UICamera.currentScheme == UICamera.ControlScheme.Controller && (Object.op_Equality((Object) UICamera.currentTouch.current, (Object) null) || !UICamera.currentTouch.current.activeInHierarchy))
        UICamera.currentTouch.current = UICamera.controllerNavigationObject;
      if (!string.IsNullOrEmpty(this.verticalAxisName))
      {
        int direction = UICamera.GetDirection(this.verticalAxisName);
        if (direction != 0)
        {
          UICamera.ShowTooltip((GameObject) null);
          UICamera.currentScheme = UICamera.ControlScheme.Controller;
          UICamera.currentTouch.current = UICamera.controllerNavigationObject;
          if (Object.op_Inequality((Object) UICamera.currentTouch.current, (Object) null))
          {
            key1 = direction > 0 ? (KeyCode) 273 : (KeyCode) 274;
            if (UICamera.onNavigate != null)
              UICamera.onNavigate(UICamera.currentTouch.current, key1);
            UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", (object) key1);
          }
        }
      }
      if (!string.IsNullOrEmpty(this.horizontalAxisName))
      {
        int direction = UICamera.GetDirection(this.horizontalAxisName);
        if (direction != 0)
        {
          UICamera.ShowTooltip((GameObject) null);
          UICamera.currentScheme = UICamera.ControlScheme.Controller;
          UICamera.currentTouch.current = UICamera.controllerNavigationObject;
          if (Object.op_Inequality((Object) UICamera.currentTouch.current, (Object) null))
          {
            key1 = direction > 0 ? (KeyCode) 275 : (KeyCode) 276;
            if (UICamera.onNavigate != null)
              UICamera.onNavigate(UICamera.currentTouch.current, key1);
            UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", (object) key1);
          }
        }
      }
      float num1 = !string.IsNullOrEmpty(this.horizontalPanAxisName) ? UICamera.GetAxis(this.horizontalPanAxisName) : 0.0f;
      float num2 = !string.IsNullOrEmpty(this.verticalPanAxisName) ? UICamera.GetAxis(this.verticalPanAxisName) : 0.0f;
      if ((double) num1 != 0.0 || (double) num2 != 0.0)
      {
        UICamera.ShowTooltip((GameObject) null);
        UICamera.currentScheme = UICamera.ControlScheme.Controller;
        UICamera.currentTouch.current = UICamera.controllerNavigationObject;
        if (Object.op_Inequality((Object) UICamera.currentTouch.current, (Object) null))
        {
          Vector2 vector2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2).\u002Ector(num1, num2);
          Vector2 delta = Vector2.op_Multiply(vector2, Time.unscaledDeltaTime);
          if (UICamera.onPan != null)
            UICamera.onPan(UICamera.currentTouch.current, delta);
          UICamera.Notify(UICamera.currentTouch.current, "OnPan", (object) delta);
        }
      }
    }
    if (Input.anyKeyDown)
    {
      int index = 0;
      for (int length = NGUITools.keys.Length; index < length; ++index)
      {
        KeyCode key2 = (KeyCode) (int) NGUITools.keys[index];
        if (key1 != key2 && UICamera.GetKeyDown(key2) && (this.useKeyboard || key2 >= 323) && (this.useController || key2 < 330) && (this.useMouse || key2 < 323 && key2 > 329))
        {
          UICamera.currentKey = key2;
          if (UICamera.onKey != null)
            UICamera.onKey(UICamera.currentTouch.current, key2);
          UICamera.Notify(UICamera.currentTouch.current, "OnKey", (object) key2);
        }
      }
    }
    UICamera.currentTouch = (UICamera.MouseOrTouch) null;
  }

  private void ProcessPress(bool pressed, float click, float drag)
  {
    if (pressed)
    {
      if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null))
        UICamera.ShowTooltip((GameObject) null);
      UICamera.currentTouch.pressStarted = true;
      if (UICamera.onPress != null && Object.op_Implicit((Object) UICamera.currentTouch.pressed))
        UICamera.onPress(UICamera.currentTouch.pressed, false);
      UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", (object) false);
      UICamera.currentTouch.pressed = UICamera.currentTouch.current;
      UICamera.currentTouch.dragged = UICamera.currentTouch.current;
      UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
      UICamera.currentTouch.totalDelta = Vector2.zero;
      UICamera.currentTouch.dragStarted = false;
      if (UICamera.onPress != null && Object.op_Implicit((Object) UICamera.currentTouch.pressed))
        UICamera.onPress(UICamera.currentTouch.pressed, true);
      UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", (object) true);
      if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null))
        UICamera.ShowTooltip((GameObject) null);
      if (!Object.op_Inequality((Object) UICamera.mSelected, (Object) UICamera.currentTouch.pressed))
        return;
      UICamera.mInputFocus = false;
      if (Object.op_Implicit((Object) UICamera.mSelected))
      {
        UICamera.Notify(UICamera.mSelected, "OnSelect", (object) false);
        if (UICamera.onSelect != null)
          UICamera.onSelect(UICamera.mSelected, false);
      }
      UICamera.mSelected = UICamera.currentTouch.pressed;
      if (Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null) && Object.op_Inequality((Object) UICamera.currentTouch.pressed.GetComponent<UIKeyNavigation>(), (Object) null))
        UICamera.controller.current = UICamera.currentTouch.pressed;
      if (!Object.op_Implicit((Object) UICamera.mSelected))
        return;
      UICamera.mInputFocus = UICamera.mSelected.activeInHierarchy && Object.op_Inequality((Object) UICamera.mSelected.GetComponent<UIInput>(), (Object) null);
      if (UICamera.onSelect != null)
        UICamera.onSelect(UICamera.mSelected, true);
      UICamera.Notify(UICamera.mSelected, "OnSelect", (object) true);
    }
    else
    {
      if (!Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null) || (double) ((Vector2) ref UICamera.currentTouch.delta).sqrMagnitude == 0.0 && !Object.op_Inequality((Object) UICamera.currentTouch.current, (Object) UICamera.currentTouch.last))
        return;
      UICamera.MouseOrTouch currentTouch = UICamera.currentTouch;
      currentTouch.totalDelta = Vector2.op_Addition(currentTouch.totalDelta, UICamera.currentTouch.delta);
      float sqrMagnitude = ((Vector2) ref UICamera.currentTouch.totalDelta).sqrMagnitude;
      bool flag = false;
      if (!UICamera.currentTouch.dragStarted && Object.op_Inequality((Object) UICamera.currentTouch.last, (Object) UICamera.currentTouch.current))
      {
        UICamera.currentTouch.dragStarted = true;
        UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
        UICamera.isDragging = true;
        if (UICamera.onDragStart != null)
          UICamera.onDragStart(UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", (object) null);
        if (UICamera.onDragOver != null)
          UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.last, "OnDragOver", (object) UICamera.currentTouch.dragged);
        UICamera.isDragging = false;
      }
      else if (!UICamera.currentTouch.dragStarted && (double) drag < (double) sqrMagnitude)
      {
        flag = true;
        UICamera.currentTouch.dragStarted = true;
        UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
      }
      if (!UICamera.currentTouch.dragStarted)
        return;
      if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null))
        UICamera.ShowTooltip((GameObject) null);
      UICamera.isDragging = true;
      int num = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None ? 1 : 0;
      if (flag)
      {
        if (UICamera.onDragStart != null)
          UICamera.onDragStart(UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", (object) null);
        if (UICamera.onDragOver != null)
          UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", (object) UICamera.currentTouch.dragged);
      }
      else if (Object.op_Inequality((Object) UICamera.currentTouch.last, (Object) UICamera.currentTouch.current))
      {
        if (UICamera.onDragOut != null)
          UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", (object) UICamera.currentTouch.dragged);
        if (UICamera.onDragOver != null)
          UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", (object) UICamera.currentTouch.dragged);
      }
      if (UICamera.onDrag != null)
        UICamera.onDrag(UICamera.currentTouch.dragged, UICamera.currentTouch.delta);
      UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", (object) UICamera.currentTouch.delta);
      UICamera.currentTouch.last = UICamera.currentTouch.current;
      UICamera.isDragging = false;
      if (num != 0)
      {
        UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
      }
      else
      {
        if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.BasedOnDelta || (double) click >= (double) sqrMagnitude)
          return;
        UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
      }
    }
  }

  private void ProcessRelease(bool isMouse, float drag)
  {
    if (UICamera.currentTouch == null)
      return;
    UICamera.currentTouch.pressStarted = false;
    if (Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null))
    {
      if (UICamera.currentTouch.dragStarted)
      {
        if (UICamera.onDragOut != null)
          UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", (object) UICamera.currentTouch.dragged);
        if (UICamera.onDragEnd != null)
          UICamera.onDragEnd(UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.dragged, "OnDragEnd", (object) null);
      }
      if (UICamera.onPress != null)
        UICamera.onPress(UICamera.currentTouch.pressed, false);
      UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", (object) false);
      if (isMouse && this.HasCollider(UICamera.currentTouch.pressed))
      {
        if (Object.op_Equality((Object) UICamera.mHover, (Object) UICamera.currentTouch.current))
        {
          if (UICamera.onHover != null)
            UICamera.onHover(UICamera.currentTouch.current, true);
          UICamera.Notify(UICamera.currentTouch.current, "OnHover", (object) true);
        }
        else
          UICamera.hoveredObject = UICamera.currentTouch.current;
      }
      if (Object.op_Equality((Object) UICamera.currentTouch.dragged, (Object) UICamera.currentTouch.current) || UICamera.currentScheme != UICamera.ControlScheme.Controller && UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && (double) ((Vector2) ref UICamera.currentTouch.totalDelta).sqrMagnitude < (double) drag)
      {
        if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && Object.op_Equality((Object) UICamera.currentTouch.pressed, (Object) UICamera.currentTouch.current))
        {
          UICamera.ShowTooltip((GameObject) null);
          float time = RealTime.time;
          if (TutorialMessage.IsActiveButton(UICamera.currentTouch.pressed))
          {
            if (UICamera.onClick != null)
              UICamera.onClick(UICamera.currentTouch.pressed);
            UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", (object) null);
          }
          if ((double) UICamera.currentTouch.clickTime + 0.34999999403953552 > (double) time)
          {
            if (UICamera.onDoubleClick != null)
              UICamera.onDoubleClick(UICamera.currentTouch.pressed);
            UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", (object) null);
          }
          UICamera.currentTouch.clickTime = time;
        }
      }
      else if (UICamera.currentTouch.dragStarted)
      {
        if (UICamera.onDrop != null)
          UICamera.onDrop(UICamera.currentTouch.current, UICamera.currentTouch.dragged);
        UICamera.Notify(UICamera.currentTouch.current, "OnDrop", (object) UICamera.currentTouch.dragged);
      }
    }
    UICamera.currentTouch.dragStarted = false;
    UICamera.currentTouch.pressed = (GameObject) null;
    UICamera.currentTouch.dragged = (GameObject) null;
  }

  private bool HasCollider(GameObject go)
  {
    if (Object.op_Equality((Object) go, (Object) null))
      return false;
    Collider component1 = go.GetComponent<Collider>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      return component1.enabled;
    Collider2D component2 = go.GetComponent<Collider2D>();
    return Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled;
  }

  public void ProcessTouch(bool pressed, bool released)
  {
    if (pressed)
      UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
    bool isMouse = UICamera.currentScheme == UICamera.ControlScheme.Mouse;
    float num1 = isMouse ? this.mouseDragThreshold : this.touchDragThreshold;
    float num2 = isMouse ? this.mouseClickThreshold : this.touchClickThreshold;
    float drag = num1 * num1;
    float click = num2 * num2;
    if (Object.op_Inequality((Object) UICamera.currentTouch.pressed, (Object) null))
    {
      if (released)
        this.ProcessRelease(isMouse, drag);
      this.ProcessPress(pressed, click, drag);
      if (!Object.op_Equality((Object) UICamera.currentTouch.pressed, (Object) UICamera.currentTouch.current) || (double) UICamera.mTooltipTime == 0.0 || UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None || UICamera.currentTouch.dragStarted || (double) UICamera.currentTouch.deltaTime <= (double) this.tooltipDelay)
        return;
      UICamera.mTooltipTime = 0.0f;
      UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
      if (this.longPressTooltip)
        UICamera.ShowTooltip(UICamera.currentTouch.pressed);
      UICamera.Notify(UICamera.currentTouch.current, "OnLongPress", (object) null);
    }
    else
    {
      if (!(isMouse | pressed | released))
        return;
      this.ProcessPress(pressed, click, drag);
      if (!released)
        return;
      this.ProcessRelease(isMouse, drag);
    }
  }

  public static bool ShowTooltip(GameObject go)
  {
    if (!Object.op_Inequality((Object) UICamera.mTooltip, (Object) go))
      return false;
    if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null))
    {
      if (UICamera.onTooltip != null)
        UICamera.onTooltip(UICamera.mTooltip, false);
      UICamera.Notify(UICamera.mTooltip, "OnTooltip", (object) false);
    }
    UICamera.mTooltip = go;
    UICamera.mTooltipTime = 0.0f;
    if (Object.op_Inequality((Object) UICamera.mTooltip, (Object) null))
    {
      if (UICamera.onTooltip != null)
        UICamera.onTooltip(UICamera.mTooltip, true);
      UICamera.Notify(UICamera.mTooltip, "OnTooltip", (object) true);
    }
    return true;
  }

  public static bool HideTooltip() => UICamera.ShowTooltip((GameObject) null);

  public enum ControlScheme
  {
    Mouse,
    Touch,
    Controller,
  }

  public enum ClickNotification
  {
    None,
    Always,
    BasedOnDelta,
  }

  public class MouseOrTouch
  {
    public KeyCode key;
    public Vector2 pos;
    public Vector2 lastPos;
    public Vector2 delta;
    public Vector2 totalDelta;
    public Camera pressedCam;
    public GameObject last;
    public GameObject current;
    public GameObject pressed;
    public GameObject dragged;
    public float pressTime;
    public float clickTime;
    public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;
    public bool touchBegan = true;
    public bool pressStarted;
    public bool dragStarted;
    public int ignoreDelta;

    public float deltaTime => RealTime.time - this.pressTime;

    public bool isOverUI
    {
      get
      {
        return Object.op_Inequality((Object) this.current, (Object) null) && Object.op_Inequality((Object) this.current, (Object) UICamera.fallThrough) && Object.op_Inequality((Object) NGUITools.FindInParents<UIRoot>(this.current), (Object) null);
      }
    }
  }

  public enum EventType
  {
    World_3D,
    UI_3D,
    World_2D,
    UI_2D,
  }

  public delegate bool GetKeyStateFunc(KeyCode key);

  public delegate float GetAxisFunc(string name);

  public delegate bool GetAnyKeyFunc();

  public delegate void OnScreenResize();

  public delegate void OnCustomInput();

  public delegate void OnSchemeChange();

  public delegate void MoveDelegate(Vector2 delta);

  public delegate void VoidDelegate(GameObject go);

  public delegate void BoolDelegate(GameObject go, bool state);

  public delegate void FloatDelegate(GameObject go, float delta);

  public delegate void VectorDelegate(GameObject go, Vector2 delta);

  public delegate void ObjectDelegate(GameObject go, GameObject obj);

  public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

  private struct DepthEntry
  {
    public int depth;
    public RaycastHit hit;
    public Vector3 point;
    public GameObject go;
  }

  public class Touch
  {
    public int fingerId;
    public TouchPhase phase;
    public Vector2 position;
    public int tapCount;
  }

  public delegate int GetTouchCountCallback();

  public delegate UICamera.Touch GetTouchCallback(int index);
}

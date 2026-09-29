// Decompiled with JetBrains decompiler
// Type: NGUITools
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

#nullable disable
public static class NGUITools
{
  private static AudioListener mListener;
  private static bool mLoaded;
  private static float mGlobalVolume;
  private static float mLastTimestamp;
  private static AudioClip mLastClip;
  private static Vector3[] mSides;
  public static KeyCode[] keys;

  public static float soundVolume
  {
    get
    {
      if (!NGUITools.mLoaded)
      {
        NGUITools.mLoaded = true;
        NGUITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
      }
      return NGUITools.mGlobalVolume;
    }
    set
    {
      if ((double) NGUITools.mGlobalVolume == (double) value)
        return;
      NGUITools.mLoaded = true;
      NGUITools.mGlobalVolume = value;
      PlayerPrefs.SetFloat("Sound", value);
    }
  }

  public static bool fileAccess => false;

  public static AudioSource PlaySound(AudioClip clip) => NGUITools.PlaySound(clip, 1f, 1f);

  public static AudioSource PlaySound(AudioClip clip, float volume)
  {
    return NGUITools.PlaySound(clip, volume, 1f);
  }

  public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
  {
    float time = RealTime.time;
    if (Object.op_Equality((Object) NGUITools.mLastClip, (Object) clip) && (double) NGUITools.mLastTimestamp + 0.10000000149011612 > (double) time)
      return (AudioSource) null;
    NGUITools.mLastClip = clip;
    NGUITools.mLastTimestamp = time;
    volume *= NGUITools.soundVolume;
    if (Object.op_Inequality((Object) clip, (Object) null) && (double) volume > 0.0099999997764825821)
    {
      if (Object.op_Equality((Object) NGUITools.mListener, (Object) null) || !NGUITools.GetActive((Behaviour) NGUITools.mListener))
      {
        if (Object.FindObjectsOfType(typeof (AudioListener)) is AudioListener[] objectsOfType)
        {
          for (int index = 0; index < objectsOfType.Length; ++index)
          {
            if (NGUITools.GetActive((Behaviour) objectsOfType[index]))
            {
              NGUITools.mListener = objectsOfType[index];
              break;
            }
          }
        }
        if (Object.op_Equality((Object) NGUITools.mListener, (Object) null))
        {
          Camera camera = Camera.main;
          if (Object.op_Equality((Object) camera, (Object) null))
            camera = Object.FindObjectOfType(typeof (Camera)) as Camera;
          if (Object.op_Inequality((Object) camera, (Object) null))
            NGUITools.mListener = ((Component) camera).gameObject.AddComponent<AudioListener>();
        }
      }
      if (Object.op_Inequality((Object) NGUITools.mListener, (Object) null) && ((Behaviour) NGUITools.mListener).enabled && NGUITools.GetActive(((Component) NGUITools.mListener).gameObject))
      {
        AudioSource audioSource = ((Component) NGUITools.mListener).GetComponent<AudioSource>();
        if (Object.op_Equality((Object) audioSource, (Object) null))
          audioSource = ((Component) NGUITools.mListener).gameObject.AddComponent<AudioSource>();
        audioSource.priority = 50;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip, volume);
        return audioSource;
      }
    }
    return (AudioSource) null;
  }

  public static int RandomRange(int min, int max) => min == max ? min : Random.Range(min, max + 1);

  public static string GetHierarchy(GameObject obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return "";
    string hierarchy = ((Object) obj).name;
    while (Object.op_Inequality((Object) obj.transform.parent, (Object) null))
    {
      obj = ((Component) obj.transform.parent).gameObject;
      hierarchy = $"{((Object) obj).name}\\{hierarchy}";
    }
    return hierarchy;
  }

  public static T[] FindActive<T>() where T : Component
  {
    return Object.FindObjectsOfType(typeof (T)) as T[];
  }

  public static Camera FindCameraForLayer(int layer)
  {
    int num = 1 << layer;
    for (int index = 0; index < UICamera.list.size; ++index)
    {
      Camera cachedCamera = UICamera.list.buffer[index].cachedCamera;
      if (Object.op_Implicit((Object) cachedCamera) && (cachedCamera.cullingMask & num) != 0)
        return cachedCamera;
    }
    Camera main = Camera.main;
    if (Object.op_Implicit((Object) main) && (main.cullingMask & num) != 0)
      return main;
    Camera[] cameraArray = new Camera[Camera.allCamerasCount];
    int allCameras = Camera.GetAllCameras(cameraArray);
    for (int index = 0; index < allCameras; ++index)
    {
      Camera cameraForLayer = cameraArray[index];
      if (Object.op_Implicit((Object) cameraForLayer) && ((Behaviour) cameraForLayer).enabled && (cameraForLayer.cullingMask & num) != 0)
        return cameraForLayer;
    }
    return (Camera) null;
  }

  public static void AddWidgetCollider(GameObject go) => NGUITools.AddWidgetCollider(go, false);

  public static void AddWidgetCollider(GameObject go, bool considerInactive)
  {
    if (!Object.op_Inequality((Object) go, (Object) null))
      return;
    Collider component1 = go.GetComponent<Collider>();
    BoxCollider box1 = component1 as BoxCollider;
    if (Object.op_Inequality((Object) box1, (Object) null))
    {
      NGUITools.UpdateWidgetCollider(box1, considerInactive);
    }
    else
    {
      if (Object.op_Inequality((Object) component1, (Object) null))
        return;
      BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
      if (Object.op_Inequality((Object) component2, (Object) null))
      {
        NGUITools.UpdateWidgetCollider(component2, considerInactive);
      }
      else
      {
        UICamera cameraForLayer = UICamera.FindCameraForLayer(go.layer);
        if (Object.op_Inequality((Object) cameraForLayer, (Object) null) && (cameraForLayer.eventType == UICamera.EventType.World_2D || cameraForLayer.eventType == UICamera.EventType.UI_2D))
        {
          BoxCollider2D box2 = go.AddComponent<BoxCollider2D>();
          ((Collider2D) box2).isTrigger = true;
          UIWidget component3 = go.GetComponent<UIWidget>();
          if (Object.op_Inequality((Object) component3, (Object) null))
            component3.autoResizeBoxCollider = true;
          NGUITools.UpdateWidgetCollider(box2, considerInactive);
        }
        else
        {
          BoxCollider box3 = go.AddComponent<BoxCollider>();
          ((Collider) box3).isTrigger = true;
          UIWidget component4 = go.GetComponent<UIWidget>();
          if (Object.op_Inequality((Object) component4, (Object) null))
            component4.autoResizeBoxCollider = true;
          NGUITools.UpdateWidgetCollider(box3, considerInactive);
        }
      }
    }
  }

  public static void UpdateWidgetCollider(GameObject go)
  {
    NGUITools.UpdateWidgetCollider(go, false);
  }

  public static void UpdateWidgetCollider(GameObject go, bool considerInactive)
  {
    if (!Object.op_Inequality((Object) go, (Object) null))
      return;
    BoxCollider component1 = go.GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) component1, (Object) null))
    {
      NGUITools.UpdateWidgetCollider(component1, considerInactive);
    }
    else
    {
      BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
        return;
      NGUITools.UpdateWidgetCollider(component2, considerInactive);
    }
  }

  public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive)
  {
    if (!Object.op_Inequality((Object) box, (Object) null))
      return;
    GameObject gameObject = ((Component) box).gameObject;
    UIWidget component = gameObject.GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      Vector4 drawRegion = component.drawRegion;
      if ((double) drawRegion.x != 0.0 || (double) drawRegion.y != 0.0 || (double) drawRegion.z != 1.0 || (double) drawRegion.w != 1.0)
      {
        Vector4 drawingDimensions = component.drawingDimensions;
        box.center = new Vector3((float) (((double) drawingDimensions.x + (double) drawingDimensions.z) * 0.5), (float) (((double) drawingDimensions.y + (double) drawingDimensions.w) * 0.5));
        box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
      }
      else
      {
        Vector3[] localCorners = component.localCorners;
        box.center = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
        box.size = Vector3.op_Subtraction(localCorners[2], localCorners[0]);
      }
    }
    else
    {
      Bounds relativeWidgetBounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
      box.center = ((Bounds) ref relativeWidgetBounds).center;
      box.size = new Vector3(((Bounds) ref relativeWidgetBounds).size.x, ((Bounds) ref relativeWidgetBounds).size.y, 0.0f);
    }
  }

  public static void UpdateWidgetCollider(BoxCollider2D box, bool considerInactive)
  {
    if (!Object.op_Inequality((Object) box, (Object) null))
      return;
    GameObject gameObject = ((Component) box).gameObject;
    UIWidget component = gameObject.GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      Vector3[] localCorners = component.localCorners;
      ((Collider2D) box).offset = Vector2.op_Implicit(Vector3.Lerp(localCorners[0], localCorners[2], 0.5f));
      box.size = Vector2.op_Implicit(Vector3.op_Subtraction(localCorners[2], localCorners[0]));
    }
    else
    {
      Bounds relativeWidgetBounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
      ((Collider2D) box).offset = Vector2.op_Implicit(((Bounds) ref relativeWidgetBounds).center);
      box.size = new Vector2(((Bounds) ref relativeWidgetBounds).size.x, ((Bounds) ref relativeWidgetBounds).size.y);
    }
  }

  public static string GetTypeName<T>()
  {
    string typeName = typeof (T).ToString();
    if (typeName.StartsWith("UI"))
      typeName = typeName.Substring(2);
    else if (typeName.StartsWith("UnityEngine."))
      typeName = typeName.Substring(12);
    return typeName;
  }

  public static string GetTypeName(Object obj)
  {
    if (Object.op_Equality(obj, (Object) null))
      return "Null";
    string typeName = obj.GetType().ToString();
    if (typeName.StartsWith("UI"))
      typeName = typeName.Substring(2);
    else if (typeName.StartsWith("UnityEngine."))
      typeName = typeName.Substring(12);
    return typeName;
  }

  public static void RegisterUndo(Object obj, string name)
  {
  }

  public static void SetDirty(Object obj)
  {
  }

  public static GameObject AddChild(GameObject parent) => NGUITools.AddChild(parent, true);

  public static GameObject AddChild(GameObject parent, bool undo)
  {
    GameObject gameObject = new GameObject();
    if (Object.op_Inequality((Object) parent, (Object) null))
    {
      Transform transform = gameObject.transform;
      transform.parent = parent.transform;
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
      gameObject.layer = parent.layer;
    }
    return gameObject;
  }

  public static GameObject AddChild(GameObject parent, GameObject prefab)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(prefab);
    if (Object.op_Inequality((Object) gameObject, (Object) null) && Object.op_Inequality((Object) parent, (Object) null))
    {
      Transform transform = gameObject.transform;
      transform.parent = parent.transform;
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
      gameObject.layer = parent.layer;
    }
    return gameObject;
  }

  public static int CalculateRaycastDepth(GameObject go)
  {
    UIWidget component = go.GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return component.raycastDepth;
    go.GetComponentsInChildren<UIWidget>(Temporary.uiWidgetList);
    if (Temporary.uiWidgetList.Count == 0)
      return 0;
    int raycastDepth = int.MaxValue;
    int index = 0;
    for (int count = Temporary.uiWidgetList.Count; index < count; ++index)
    {
      if (((Behaviour) Temporary.uiWidgetList[index]).enabled)
        raycastDepth = Mathf.Min(raycastDepth, Temporary.uiWidgetList[index].raycastDepth);
    }
    Temporary.uiWidgetList.Clear();
    return raycastDepth;
  }

  public static int CalculateNextDepth(GameObject go)
  {
    if (!Object.op_Implicit((Object) go))
      return 0;
    int num = -1;
    UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
      num = Mathf.Max(num, componentsInChildren[index].depth);
    return num + 1;
  }

  public static int CalculateNextDepth(GameObject go, bool ignoreChildrenWithColliders)
  {
    if (!(Object.op_Implicit((Object) go) & ignoreChildrenWithColliders))
      return NGUITools.CalculateNextDepth(go);
    int num = -1;
    UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
    {
      UIWidget uiWidget = componentsInChildren[index];
      if (!Object.op_Inequality((Object) uiWidget.cachedGameObject, (Object) go) || !Object.op_Inequality((Object) ((Component) uiWidget).GetComponent<Collider>(), (Object) null) && !Object.op_Inequality((Object) ((Component) uiWidget).GetComponent<Collider2D>(), (Object) null))
        num = Mathf.Max(num, uiWidget.depth);
    }
    return num + 1;
  }

  public static int AdjustDepth(GameObject go, int adjustment)
  {
    if (!Object.op_Inequality((Object) go, (Object) null))
      return 0;
    if (Object.op_Inequality((Object) go.GetComponent<UIPanel>(), (Object) null))
    {
      foreach (UIPanel componentsInChild in go.GetComponentsInChildren<UIPanel>(true))
        componentsInChild.depth += adjustment;
      return 1;
    }
    UIPanel inParents = NGUITools.FindInParents<UIPanel>(go);
    if (Object.op_Equality((Object) inParents, (Object) null))
      return 0;
    UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>(true);
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
    {
      UIWidget uiWidget = componentsInChildren[index];
      if (!Object.op_Inequality((Object) uiWidget.panel, (Object) inParents))
        uiWidget.depth += adjustment;
    }
    return 2;
  }

  public static void BringForward(GameObject go)
  {
    switch (NGUITools.AdjustDepth(go, 1000))
    {
      case 1:
        NGUITools.NormalizePanelDepths();
        break;
      case 2:
        NGUITools.NormalizeWidgetDepths();
        break;
    }
  }

  public static void PushBack(GameObject go)
  {
    switch (NGUITools.AdjustDepth(go, -1000))
    {
      case 1:
        NGUITools.NormalizePanelDepths();
        break;
      case 2:
        NGUITools.NormalizeWidgetDepths();
        break;
    }
  }

  public static void NormalizeDepths()
  {
    NGUITools.NormalizeWidgetDepths();
    NGUITools.NormalizePanelDepths();
  }

  public static void NormalizeWidgetDepths()
  {
    NGUITools.NormalizeWidgetDepths(NGUITools.FindActive<UIWidget>());
  }

  public static void NormalizeWidgetDepths(GameObject go)
  {
    NGUITools.NormalizeWidgetDepths(go.GetComponentsInChildren<UIWidget>());
  }

  public static void NormalizeWidgetDepths(UIWidget[] list)
  {
    int length = list.Length;
    if (length <= 0)
      return;
    Array.Sort<UIWidget>(list, new Comparison<UIWidget>(UIWidget.FullCompareFunc));
    int num = 0;
    int depth = list[0].depth;
    for (int index = 0; index < length; ++index)
    {
      UIWidget uiWidget = list[index];
      if (uiWidget.depth == depth)
      {
        uiWidget.depth = num;
      }
      else
      {
        depth = uiWidget.depth;
        uiWidget.depth = ++num;
      }
    }
  }

  public static void NormalizePanelDepths()
  {
    UIPanel[] active = NGUITools.FindActive<UIPanel>();
    int length = active.Length;
    if (length <= 0)
      return;
    Array.Sort<UIPanel>(active, new Comparison<UIPanel>(UIPanel.CompareFunc));
    int num = 0;
    int depth = active[0].depth;
    for (int index = 0; index < length; ++index)
    {
      UIPanel uiPanel = active[index];
      if (uiPanel.depth == depth)
      {
        uiPanel.depth = num;
      }
      else
      {
        depth = uiPanel.depth;
        uiPanel.depth = ++num;
      }
    }
  }

  public static UIPanel CreateUI(bool advanced3D)
  {
    return NGUITools.CreateUI((Transform) null, advanced3D, -1);
  }

  public static UIPanel CreateUI(bool advanced3D, int layer)
  {
    return NGUITools.CreateUI((Transform) null, advanced3D, layer);
  }

  public static UIPanel CreateUI(Transform trans, bool advanced3D, int layer)
  {
    UIRoot uiRoot1 = Object.op_Inequality((Object) trans, (Object) null) ? NGUITools.FindInParents<UIRoot>(((Component) trans).gameObject) : (UIRoot) null;
    if (Object.op_Equality((Object) uiRoot1, (Object) null) && UIRoot.list.Count > 0)
    {
      foreach (UIRoot uiRoot2 in UIRoot.list)
      {
        if (((Component) uiRoot2).gameObject.layer == layer)
        {
          uiRoot1 = uiRoot2;
          break;
        }
      }
    }
    if (Object.op_Equality((Object) uiRoot1, (Object) null))
    {
      int index = 0;
      for (int count = UIPanel.list.Count; index < count; ++index)
      {
        UIPanel ui = UIPanel.list[index];
        GameObject gameObject = ((Component) ui).gameObject;
        if (((Object) gameObject).hideFlags == null && gameObject.layer == layer)
        {
          trans.parent = ((Component) ui).transform;
          trans.localScale = Vector3.one;
          return ui;
        }
      }
    }
    if (Object.op_Inequality((Object) uiRoot1, (Object) null))
    {
      UICamera componentInChildren = ((Component) uiRoot1).GetComponentInChildren<UICamera>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && ((Component) componentInChildren).GetComponent<Camera>().orthographic == advanced3D)
      {
        trans = (Transform) null;
        uiRoot1 = (UIRoot) null;
      }
    }
    if (Object.op_Equality((Object) uiRoot1, (Object) null))
    {
      GameObject gameObject = NGUITools.AddChild((GameObject) null, false);
      uiRoot1 = gameObject.AddComponent<UIRoot>();
      if (layer == -1)
        layer = LayerMask.NameToLayer("UI");
      if (layer == -1)
        layer = LayerMask.NameToLayer("2D UI");
      gameObject.layer = layer;
      if (advanced3D)
      {
        ((Object) gameObject).name = "UI Root (3D)";
        uiRoot1.scalingStyle = UIRoot.Scaling.Constrained;
      }
      else
      {
        ((Object) gameObject).name = "UI Root";
        uiRoot1.scalingStyle = UIRoot.Scaling.Flexible;
      }
    }
    UIPanel ui1 = ((Component) uiRoot1).GetComponentInChildren<UIPanel>();
    if (Object.op_Equality((Object) ui1, (Object) null))
    {
      Camera[] active1 = NGUITools.FindActive<Camera>();
      float num1 = -1f;
      bool flag = false;
      int num2 = 1 << ((Component) uiRoot1).gameObject.layer;
      for (int index = 0; index < active1.Length; ++index)
      {
        Camera camera = active1[index];
        if (camera.clearFlags == 2 || camera.clearFlags == 1)
          flag = true;
        num1 = Mathf.Max(num1, camera.depth);
        camera.cullingMask &= ~num2;
      }
      Camera camera1 = NGUITools.AddChild<Camera>(((Component) uiRoot1).gameObject, false);
      ((Component) camera1).gameObject.AddComponent<UICamera>();
      camera1.clearFlags = flag ? (CameraClearFlags) 3 : (CameraClearFlags) 2;
      camera1.backgroundColor = Color.grey;
      camera1.cullingMask = num2;
      camera1.depth = num1 + 1f;
      if (advanced3D)
      {
        camera1.nearClipPlane = 0.1f;
        camera1.farClipPlane = 4f;
        ((Component) camera1).transform.localPosition = new Vector3(0.0f, 0.0f, -700f);
      }
      else
      {
        camera1.orthographic = true;
        camera1.orthographicSize = 1f;
        camera1.nearClipPlane = -10f;
        camera1.farClipPlane = 10f;
      }
      AudioListener[] active2 = NGUITools.FindActive<AudioListener>();
      if (active2 == null || active2.Length == 0)
        ((Component) camera1).gameObject.AddComponent<AudioListener>();
      ui1 = ((Component) uiRoot1).gameObject.AddComponent<UIPanel>();
    }
    if (Object.op_Inequality((Object) trans, (Object) null))
    {
      while (Object.op_Inequality((Object) trans.parent, (Object) null))
        trans = trans.parent;
      if (NGUITools.IsChild(trans, ((Component) ui1).transform))
      {
        ui1 = ((Component) trans).gameObject.AddComponent<UIPanel>();
      }
      else
      {
        trans.parent = ((Component) ui1).transform;
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        NGUITools.SetChildLayer(ui1.cachedTransform, ui1.cachedGameObject.layer);
      }
    }
    return ui1;
  }

  public static void SetChildLayer(Transform t, int layer)
  {
    for (int index = 0; index < t.childCount; ++index)
    {
      Transform child = t.GetChild(index);
      ((Component) child).gameObject.layer = layer;
      NGUITools.SetChildLayer(child, layer);
    }
  }

  public static T AddChild<T>(GameObject parent) where T : Component
  {
    GameObject gameObject = NGUITools.AddChild(parent);
    ((Object) gameObject).name = NGUITools.GetTypeName<T>();
    return gameObject.AddComponent<T>();
  }

  public static T AddChild<T>(GameObject parent, bool undo) where T : Component
  {
    GameObject gameObject = NGUITools.AddChild(parent, undo);
    ((Object) gameObject).name = NGUITools.GetTypeName<T>();
    return gameObject.AddComponent<T>();
  }

  public static T AddWidget<T>(GameObject go) where T : UIWidget
  {
    int nextDepth = NGUITools.CalculateNextDepth(go);
    T obj = NGUITools.AddChild<T>(go);
    obj.width = 100;
    obj.height = 100;
    obj.depth = nextDepth;
    return obj;
  }

  public static T AddWidget<T>(GameObject go, int depth) where T : UIWidget
  {
    T obj = NGUITools.AddChild<T>(go);
    obj.width = 100;
    obj.height = 100;
    obj.depth = depth;
    return obj;
  }

  public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
  {
    UISpriteData sprite = Object.op_Inequality((Object) atlas, (Object) null) ? atlas.GetSprite(spriteName) : (UISpriteData) null;
    UISprite uiSprite = NGUITools.AddWidget<UISprite>(go);
    uiSprite.type = sprite == null || !sprite.hasBorder ? UIBasicSprite.Type.Simple : UIBasicSprite.Type.Sliced;
    uiSprite.atlas = atlas;
    uiSprite.spriteName = spriteName;
    return uiSprite;
  }

  public static GameObject GetRoot(GameObject go)
  {
    Transform transform = go.transform;
    while (true)
    {
      Transform parent = transform.parent;
      if (!Object.op_Equality((Object) parent, (Object) null))
        transform = parent;
      else
        break;
    }
    return ((Component) transform).gameObject;
  }

  public static T FindInParents<T>(GameObject go) where T : Component
  {
    if (Object.op_Equality((Object) go, (Object) null))
      return default (T);
    T component = go.GetComponent<T>();
    if (Object.op_Equality((Object) (object) component, (Object) null))
    {
      for (Transform parent = go.transform.parent; Object.op_Inequality((Object) parent, (Object) null) && Object.op_Equality((Object) (object) component, (Object) null); parent = parent.parent)
        component = ((Component) parent).gameObject.GetComponent<T>();
    }
    return component;
  }

  public static T FindInParents<T>(Transform trans) where T : Component
  {
    return Object.op_Equality((Object) trans, (Object) null) ? default (T) : ((Component) trans).GetComponentInParent<T>();
  }

  public static void Destroy(Object obj)
  {
    if (!Object.op_Implicit(obj))
      return;
    if (obj is Transform)
    {
      Transform transform = obj as Transform;
      GameObject gameObject = ((Component) transform).gameObject;
      if (Application.isPlaying)
      {
        transform.parent = (Transform) null;
        Object.Destroy((Object) gameObject);
      }
      else
        Object.DestroyImmediate((Object) gameObject);
    }
    else if (obj is GameObject)
    {
      GameObject gameObject = obj as GameObject;
      Transform transform = gameObject.transform;
      if (Application.isPlaying)
      {
        transform.parent = (Transform) null;
        Object.Destroy((Object) gameObject);
      }
      else
        Object.DestroyImmediate((Object) gameObject);
    }
    else if (Application.isPlaying)
      Object.Destroy(obj);
    else
      Object.DestroyImmediate(obj);
  }

  public static void DestroyChildren(this Transform t)
  {
    bool isPlaying = Application.isPlaying;
    while (t.childCount != 0)
    {
      Transform child = t.GetChild(0);
      if (isPlaying)
      {
        child.parent = (Transform) null;
        Object.Destroy((Object) ((Component) child).gameObject);
      }
      else
        Object.DestroyImmediate((Object) ((Component) child).gameObject);
    }
  }

  public static void DestroyImmediate(Object obj)
  {
    if (!Object.op_Inequality(obj, (Object) null))
      return;
    if (Application.isEditor)
      Object.DestroyImmediate(obj);
    else
      Object.Destroy(obj);
  }

  public static void Broadcast(string funcName)
  {
    GameObject[] objectsOfType = Object.FindObjectsOfType(typeof (GameObject)) as GameObject[];
    int index = 0;
    for (int length = objectsOfType.Length; index < length; ++index)
      objectsOfType[index].SendMessage(funcName, (SendMessageOptions) 1);
  }

  public static void Broadcast(string funcName, object param)
  {
    GameObject[] objectsOfType = Object.FindObjectsOfType(typeof (GameObject)) as GameObject[];
    int index = 0;
    for (int length = objectsOfType.Length; index < length; ++index)
      objectsOfType[index].SendMessage(funcName, param, (SendMessageOptions) 1);
  }

  public static bool IsChild(Transform parent, Transform child)
  {
    if (Object.op_Equality((Object) parent, (Object) null) || Object.op_Equality((Object) child, (Object) null))
      return false;
    for (; Object.op_Inequality((Object) child, (Object) null); child = child.parent)
    {
      if (Object.op_Equality((Object) child, (Object) parent))
        return true;
    }
    return false;
  }

  private static void Activate(Transform t) => NGUITools.Activate(t, false);

  private static void Activate(Transform t, bool compatibilityMode)
  {
    NGUITools.SetActiveSelf(((Component) t).gameObject, true);
    if (!compatibilityMode)
      return;
    int num1 = 0;
    for (int childCount = t.childCount; num1 < childCount; ++num1)
    {
      if (((Component) t.GetChild(num1)).gameObject.activeSelf)
        return;
    }
    int num2 = 0;
    for (int childCount = t.childCount; num2 < childCount; ++num2)
      NGUITools.Activate(t.GetChild(num2), true);
  }

  private static void Deactivate(Transform t)
  {
    NGUITools.SetActiveSelf(((Component) t).gameObject, false);
  }

  public static void SetActive(GameObject go, bool state) => NGUITools.SetActive(go, state, true);

  public static void SetActive(GameObject go, bool state, bool compatibilityMode)
  {
    if (!Object.op_Implicit((Object) go))
      return;
    if (state)
    {
      NGUITools.Activate(go.transform, compatibilityMode);
      NGUITools.CallCreatePanel(go.transform);
    }
    else
      NGUITools.Deactivate(go.transform);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  private static void CallCreatePanel(Transform t)
  {
    UIWidget component = ((Component) t).GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.CreatePanel();
    int num = 0;
    for (int childCount = t.childCount; num < childCount; ++num)
      NGUITools.CallCreatePanel(t.GetChild(num));
  }

  public static void SetActiveChildren(GameObject go, bool state)
  {
    Transform transform = go.transform;
    if (state)
    {
      int num = 0;
      for (int childCount = transform.childCount; num < childCount; ++num)
        NGUITools.Activate(transform.GetChild(num));
    }
    else
    {
      int num = 0;
      for (int childCount = transform.childCount; num < childCount; ++num)
        NGUITools.Deactivate(transform.GetChild(num));
    }
  }

  [Obsolete("Use NGUITools.GetActive instead")]
  public static bool IsActive(Behaviour mb)
  {
    return Object.op_Inequality((Object) mb, (Object) null) && mb.enabled && ((Component) mb).gameObject.activeInHierarchy;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static bool GetActive(Behaviour mb)
  {
    return Object.op_Implicit((Object) mb) && mb.enabled && ((Component) mb).gameObject.activeInHierarchy;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static bool GetActive(GameObject go)
  {
    return Object.op_Implicit((Object) go) && go.activeInHierarchy;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static void SetActiveSelf(GameObject go, bool state) => go.SetActive(state);

  public static void SetLayer(GameObject go, int layer)
  {
    go.layer = layer;
    Transform transform = go.transform;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
      NGUITools.SetLayer(((Component) transform.GetChild(num)).gameObject, layer);
  }

  public static Vector3 Round(Vector3 v)
  {
    v.x = Mathf.Round(v.x);
    v.y = Mathf.Round(v.y);
    v.z = Mathf.Round(v.z);
    return v;
  }

  public static void MakePixelPerfect(Transform t)
  {
    UIWidget component = ((Component) t).GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.MakePixelPerfect();
    if (Object.op_Equality((Object) ((Component) t).GetComponent<UIAnchor>(), (Object) null) && Object.op_Equality((Object) ((Component) t).GetComponent<UIRoot>(), (Object) null))
    {
      t.localPosition = NGUITools.Round(t.localPosition);
      t.localScale = NGUITools.Round(t.localScale);
    }
    int num = 0;
    for (int childCount = t.childCount; num < childCount; ++num)
      NGUITools.MakePixelPerfect(t.GetChild(num));
  }

  public static bool Save(string fileName, byte[] bytes)
  {
    if (!NGUITools.fileAccess)
      return false;
    string path = $"{Application.persistentDataPath}/{fileName}";
    if (bytes == null)
    {
      if (File.Exists(path))
        File.Delete(path);
      return true;
    }
    FileStream fileStream;
    try
    {
      fileStream = File.Create(path);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.Message);
      return false;
    }
    fileStream.Write(bytes, 0, bytes.Length);
    fileStream.Close();
    return true;
  }

  public static byte[] Load(string fileName)
  {
    if (!NGUITools.fileAccess)
      return (byte[]) null;
    string path = $"{Application.persistentDataPath}/{fileName}";
    return File.Exists(path) ? File.ReadAllBytes(path) : (byte[]) null;
  }

  public static Color ApplyPMA(Color c)
  {
    if ((double) c.a != 1.0)
    {
      c.r *= c.a;
      c.g *= c.a;
      c.b *= c.a;
    }
    return c;
  }

  public static void MarkParentAsChanged(GameObject go)
  {
    UIRect[] componentsInChildren = go.GetComponentsInChildren<UIRect>();
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
      componentsInChildren[index].ParentHasChanged();
  }

  public static string clipboard
  {
    get
    {
      TextEditor textEditor = new TextEditor();
      textEditor.Paste();
      return textEditor.content.text;
    }
    set
    {
      TextEditor textEditor = new TextEditor();
      textEditor.content = new GUIContent(value);
      textEditor.OnFocus();
      textEditor.Copy();
    }
  }

  [Obsolete("Use NGUIText.EncodeColor instead")]
  public static string EncodeColor(Color c) => NGUIText.EncodeColor24(c);

  [Obsolete("Use NGUIText.ParseColor instead")]
  public static Color ParseColor(string text, int offset) => NGUIText.ParseColor24(text, offset);

  [Obsolete("Use NGUIText.StripSymbols instead")]
  public static string StripSymbols(string text) => NGUIText.StripSymbols(text);

  public static T AddMissingComponent<T>(this GameObject go) where T : Component
  {
    T obj = go.GetComponent<T>();
    if (Object.op_Equality((Object) (object) obj, (Object) null))
      obj = go.AddComponent<T>();
    return obj;
  }

  public static Vector3[] GetSides(this Camera cam)
  {
    return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), (Transform) null);
  }

  public static Vector3[] GetSides(this Camera cam, float depth)
  {
    return cam.GetSides(depth, (Transform) null);
  }

  public static Vector3[] GetSides(this Camera cam, Transform relativeTo)
  {
    return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
  }

  public static Vector3[] GetSides(this Camera cam, float depth, Transform relativeTo)
  {
    if (cam.orthographic)
    {
      double orthographicSize = (double) cam.orthographicSize;
      float num1 = (float) -orthographicSize;
      float num2 = (float) orthographicSize;
      float num3 = (float) -orthographicSize;
      float num4 = (float) orthographicSize;
      Rect rect = cam.rect;
      Vector2 screenSize = NGUITools.screenSize;
      float num5 = screenSize.x / screenSize.y * (((Rect) ref rect).width / ((Rect) ref rect).height);
      float num6 = num1 * num5;
      float num7 = num2 * num5;
      Transform transform = ((Component) cam).transform;
      Quaternion rotation = transform.rotation;
      Vector3 position = transform.position;
      int num8 = Mathf.RoundToInt(screenSize.x);
      int num9 = Mathf.RoundToInt(screenSize.y);
      if ((num8 & 1) == 1)
        position.x -= 1f / screenSize.x;
      if ((num9 & 1) == 1)
        position.y += 1f / screenSize.y;
      NGUITools.mSides[0] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(num6, 0.0f, depth)), position);
      NGUITools.mSides[1] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(0.0f, num4, depth)), position);
      NGUITools.mSides[2] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(num7, 0.0f, depth)), position);
      NGUITools.mSides[3] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(0.0f, num3, depth)), position);
    }
    else
    {
      NGUITools.mSides[0] = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, depth));
      NGUITools.mSides[1] = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, depth));
      NGUITools.mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth));
      NGUITools.mSides[3] = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, depth));
    }
    if (Object.op_Inequality((Object) relativeTo, (Object) null))
    {
      for (int index = 0; index < 4; ++index)
        NGUITools.mSides[index] = relativeTo.InverseTransformPoint(NGUITools.mSides[index]);
    }
    return NGUITools.mSides;
  }

  public static Vector3[] GetWorldCorners(this Camera cam)
  {
    float depth = Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f);
    return cam.GetWorldCorners(depth, (Transform) null);
  }

  public static Vector3[] GetWorldCorners(this Camera cam, float depth)
  {
    return cam.GetWorldCorners(depth, (Transform) null);
  }

  public static Vector3[] GetWorldCorners(this Camera cam, Transform relativeTo)
  {
    return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
  }

  public static Vector3[] GetWorldCorners(this Camera cam, float depth, Transform relativeTo)
  {
    if (cam.orthographic)
    {
      double orthographicSize = (double) cam.orthographicSize;
      float num1 = (float) -orthographicSize;
      float num2 = (float) orthographicSize;
      float num3 = (float) -orthographicSize;
      float num4 = (float) orthographicSize;
      Rect rect = cam.rect;
      Vector2 screenSize = NGUITools.screenSize;
      float num5 = screenSize.x / screenSize.y * (((Rect) ref rect).width / ((Rect) ref rect).height);
      float num6 = num1 * num5;
      float num7 = num2 * num5;
      Transform transform = ((Component) cam).transform;
      Quaternion rotation = transform.rotation;
      Vector3 position = transform.position;
      NGUITools.mSides[0] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(num6, num3, depth)), position);
      NGUITools.mSides[1] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(num6, num4, depth)), position);
      NGUITools.mSides[2] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(num7, num4, depth)), position);
      NGUITools.mSides[3] = Vector3.op_Addition(Quaternion.op_Multiply(rotation, new Vector3(num7, num3, depth)), position);
    }
    else
    {
      NGUITools.mSides[0] = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, depth));
      NGUITools.mSides[1] = cam.ViewportToWorldPoint(new Vector3(0.0f, 1f, depth));
      NGUITools.mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 1f, depth));
      NGUITools.mSides[3] = cam.ViewportToWorldPoint(new Vector3(1f, 0.0f, depth));
    }
    if (Object.op_Inequality((Object) relativeTo, (Object) null))
    {
      for (int index = 0; index < 4; ++index)
        NGUITools.mSides[index] = relativeTo.InverseTransformPoint(NGUITools.mSides[index]);
    }
    return NGUITools.mSides;
  }

  public static string GetFuncName(object obj, string method)
  {
    if (obj == null)
      return "<null>";
    string str = obj.GetType().ToString();
    int num = str.LastIndexOf('/');
    if (num > 0)
      str = str.Substring(num + 1);
    return !string.IsNullOrEmpty(method) ? $"{str}/{method}" : str;
  }

  public static void Execute<T>(GameObject go, string funcName) where T : Component
  {
    foreach (T component in go.GetComponents<T>())
    {
      MethodInfo method = ((object) component).GetType().GetMethod(funcName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (method != (MethodInfo) null)
        method.Invoke((object) component, (object[]) null);
    }
  }

  public static void ExecuteAll<T>(GameObject root, string funcName) where T : Component
  {
    NGUITools.Execute<T>(root, funcName);
    Transform transform = root.transform;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
      NGUITools.ExecuteAll<T>(((Component) transform.GetChild(num)).gameObject, funcName);
  }

  public static void ImmediatelyCreateDrawCalls(GameObject root)
  {
    NGUITools.ExecuteAll<UIWidget>(root, "Start");
    NGUITools.ExecuteAll<UIPanel>(root, "Start");
    NGUITools.ExecuteAll<UIWidget>(root, "Update");
    NGUITools.ExecuteAll<UIPanel>(root, "Update");
    NGUITools.ExecuteAll<UIPanel>(root, "LateUpdate");
  }

  public static Vector2 screenSize => new Vector2((float) Screen.width, (float) Screen.height);

  public static string KeyToCaption(KeyCode key)
  {
    switch ((int) key)
    {
      case 0:
        return (string) null;
      case 1:
      case 2:
      case 3:
      case 4:
      case 5:
      case 6:
      case 7:
      case 10:
      case 11:
      case 14:
      case 15:
      case 16 /*0x10*/:
      case 17:
      case 18:
      case 20:
      case 21:
      case 22:
      case 23:
      case 24:
      case 25:
      case 26:
      case 28:
      case 29:
      case 30:
      case 31 /*0x1F*/:
      case 37:
      case 65:
      case 66:
      case 67:
      case 68:
      case 69:
      case 70:
      case 71:
      case 72:
      case 73:
      case 74:
      case 75:
      case 76:
      case 77:
      case 78:
      case 79:
      case 80 /*0x50*/:
      case 81:
      case 82:
      case 83:
      case 84:
      case 85:
      case 86:
      case 87:
      case 88:
      case 89:
      case 90:
      case 123:
      case 124:
      case 125:
      case 126:
label_151:
        return (string) null;
      case 8:
        return "BS";
      case 9:
        return "Tab";
      case 12:
        return "Clr";
      case 13:
        return "NT";
      case 19:
        return "PS";
      case 27:
        return "Esc";
      case 32 /*0x20*/:
        return "SP";
      case 33:
        return "!";
      case 34:
        return "\"";
      case 35:
        return "#";
      case 36:
        return "$";
      case 38:
        return "&";
      case 39:
        return "'";
      case 40:
        return "(";
      case 41:
        return ")";
      case 42:
        return "*";
      case 43:
        return "+";
      case 44:
        return ",";
      case 45:
        return "-";
      case 46:
        return ".";
      case 47:
        return "/";
      case 48 /*0x30*/:
        return "0";
      case 49:
        return "1";
      case 50:
        return "2";
      case 51:
        return "3";
      case 52:
        return "4";
      case 53:
        return "5";
      case 54:
        return "6";
      case 55:
        return "7";
      case 56:
        return "8";
      case 57:
        return "9";
      case 58:
        return ":";
      case 59:
        return ";";
      case 60:
        return "<";
      case 61:
        return "=";
      case 62:
        return ">";
      case 63 /*0x3F*/:
        return "?";
      case 64 /*0x40*/:
        return "@";
      case 91:
        return "[";
      case 92:
        return "\\";
      case 93:
        return "]";
      case 94:
        return "^";
      case 95:
        return "_";
      case 96 /*0x60*/:
        return "`";
      case 97:
        return "A";
      case 98:
        return "B";
      case 99:
        return "C";
      case 100:
        return "D";
      case 101:
        return "E";
      case 102:
        return "F";
      case 103:
        return "G";
      case 104:
        return "H";
      case 105:
        return "I";
      case 106:
        return "J";
      case 107:
        return "K";
      case 108:
        return "L";
      case 109:
        return "M";
      case 110:
        return "N0";
      case 111:
        return "O";
      case 112 /*0x70*/:
        return "P";
      case 113:
        return "Q";
      case 114:
        return "R";
      case 115:
        return "S";
      case 116:
        return "T";
      case 117:
        return "U";
      case 118:
        return "V";
      case 119:
        return "W";
      case 120:
        return "X";
      case 121:
        return "Y";
      case 122:
        return "Z";
      case (int) sbyte.MaxValue:
        return "Del";
      default:
        switch (key - 256 /*0x0100*/)
        {
          case 0:
            return "K0";
          case 1:
            return "K1";
          case 2:
            return "K2";
          case 3:
            return "K3";
          case 4:
            return "K4";
          case 5:
            return "K5";
          case 6:
            return "K6";
          case 7:
            return "K7";
          case 8:
            return "K8";
          case 9:
            return "K9";
          case 10:
            return ".";
          case 11:
            return "/";
          case 12:
            return "*";
          case 13:
            return "-";
          case 14:
            return "+";
          case 15:
            return "NT";
          case 16 /*0x10*/:
            return "=";
          case 17:
            return "UP";
          case 18:
            return "DN";
          case 19:
            return "LT";
          case 20:
            return "RT";
          case 21:
            return "Ins";
          case 22:
            return "Home";
          case 23:
            return "End";
          case 24:
            return "PU";
          case 25:
            return "PD";
          case 26:
            return "F1";
          case 27:
            return "F2";
          case 28:
            return "F3";
          case 29:
            return "F4";
          case 30:
            return "F5";
          case 31 /*0x1F*/:
            return "F6";
          case 32 /*0x20*/:
            return "F7";
          case 33:
            return "F8";
          case 34:
            return "F9";
          case 35:
            return "F10";
          case 36:
            return "F11";
          case 37:
            return "F12";
          case 38:
            return "F13";
          case 39:
            return "F14";
          case 40:
            return "F15";
          case 44:
            return "Num";
          case 45:
            return "Cap";
          case 46:
            return "Scr";
          case 47:
            return "RS";
          case 48 /*0x30*/:
            return "LS";
          case 49:
            return "RC";
          case 50:
            return "LC";
          case 51:
            return "RA";
          case 52:
            return "LA";
          case 67:
            return "M0";
          case 68:
            return "M1";
          case 69:
            return "M2";
          case 70:
            return "M3";
          case 71:
            return "M4";
          case 72:
            return "M5";
          case 73:
            return "M6";
          case 74:
            return "(A)";
          case 75:
            return "(B)";
          case 76:
            return "(X)";
          case 77:
            return "(Y)";
          case 78:
            return "(RB)";
          case 79:
            return "(LB)";
          case 80 /*0x50*/:
            return "(Back)";
          case 81:
            return "(Start)";
          case 82:
            return "(LS)";
          case 83:
            return "(RS)";
          case 84:
            return "J10";
          case 85:
            return "J11";
          case 86:
            return "J12";
          case 87:
            return "J13";
          case 88:
            return "J14";
          case 89:
            return "J15";
          case 90:
            return "J16";
          case 91:
            return "J17";
          case 92:
            return "J18";
          case 93:
            return "J19";
          default:
            goto label_151;
        }
    }
  }

  static NGUITools()
  {
    // ISSUE: unable to decompile the method.
  }
}

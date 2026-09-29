// Decompiled with JetBrains decompiler
// Type: UIKeyNavigation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{
  public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();
  public UIKeyNavigation.Constraint constraint;
  public GameObject onUp;
  public GameObject onDown;
  public GameObject onLeft;
  public GameObject onRight;
  public GameObject onClick;
  public GameObject onTab;
  public bool startsSelected;
  [NonSerialized]
  private bool mStarted;

  public static UIKeyNavigation current
  {
    get
    {
      GameObject hoveredObject = UICamera.hoveredObject;
      return Object.op_Equality((Object) hoveredObject, (Object) null) ? (UIKeyNavigation) null : hoveredObject.GetComponent<UIKeyNavigation>();
    }
  }

  public bool isColliderEnabled
  {
    get
    {
      if (!((Behaviour) this).enabled || !((Component) this).gameObject.activeInHierarchy)
        return false;
      Collider component1 = ((Component) this).GetComponent<Collider>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        return component1.enabled;
      Collider2D component2 = ((Component) this).GetComponent<Collider2D>();
      return Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled;
    }
  }

  protected virtual void OnEnable()
  {
    UIKeyNavigation.list.Add(this);
    if (!this.mStarted)
      return;
    this.Start();
  }

  private void Start()
  {
    this.mStarted = true;
    if (!this.startsSelected || !this.isColliderEnabled)
      return;
    UICamera.hoveredObject = ((Component) this).gameObject;
  }

  protected virtual void OnDisable() => UIKeyNavigation.list.Remove(this);

  private static bool IsActive(GameObject go)
  {
    if (!Object.op_Implicit((Object) go) || !go.activeInHierarchy)
      return false;
    Collider component1 = go.GetComponent<Collider>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      return component1.enabled;
    Collider2D component2 = go.GetComponent<Collider2D>();
    return Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled;
  }

  public GameObject GetLeft()
  {
    if (UIKeyNavigation.IsActive(this.onLeft))
      return this.onLeft;
    return this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit ? (GameObject) null : this.Get(Vector3.left, y: 2f);
  }

  public GameObject GetRight()
  {
    if (UIKeyNavigation.IsActive(this.onRight))
      return this.onRight;
    return this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit ? (GameObject) null : this.Get(Vector3.right, y: 2f);
  }

  public GameObject GetUp()
  {
    if (UIKeyNavigation.IsActive(this.onUp))
      return this.onUp;
    return this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit ? (GameObject) null : this.Get(Vector3.up, 2f);
  }

  public GameObject GetDown()
  {
    if (UIKeyNavigation.IsActive(this.onDown))
      return this.onDown;
    return this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit ? (GameObject) null : this.Get(Vector3.down, 2f);
  }

  public GameObject Get(Vector3 myDir, float x = 1f, float y = 1f)
  {
    Transform transform = ((Component) this).transform;
    myDir = transform.TransformDirection(myDir);
    Vector3 center = UIKeyNavigation.GetCenter(((Component) this).gameObject);
    float num = float.MaxValue;
    GameObject gameObject = (GameObject) null;
    for (int i = 0; i < UIKeyNavigation.list.size; ++i)
    {
      UIKeyNavigation uiKeyNavigation = UIKeyNavigation.list[i];
      if (!Object.op_Equality((Object) uiKeyNavigation, (Object) this) && uiKeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uiKeyNavigation.isColliderEnabled)
      {
        UIWidget component = ((Component) uiKeyNavigation).GetComponent<UIWidget>();
        if (!Object.op_Inequality((Object) component, (Object) null) || (double) component.alpha != 0.0)
        {
          Vector3 vector3_1 = Vector3.op_Subtraction(UIKeyNavigation.GetCenter(((Component) uiKeyNavigation).gameObject), center);
          if ((double) Vector3.Dot(myDir, ((Vector3) ref vector3_1).normalized) >= 0.7070000171661377)
          {
            Vector3 vector3_2 = transform.InverseTransformDirection(vector3_1);
            vector3_2.x *= x;
            vector3_2.y *= y;
            float sqrMagnitude = ((Vector3) ref vector3_2).sqrMagnitude;
            if ((double) sqrMagnitude <= (double) num)
            {
              gameObject = ((Component) uiKeyNavigation).gameObject;
              num = sqrMagnitude;
            }
          }
        }
      }
    }
    return gameObject;
  }

  protected static Vector3 GetCenter(GameObject go)
  {
    UIWidget component = go.GetComponent<UIWidget>();
    UICamera cameraForLayer = UICamera.FindCameraForLayer(go.layer);
    if (Object.op_Inequality((Object) cameraForLayer, (Object) null))
    {
      Vector3 vector3 = go.transform.position;
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        Vector3[] worldCorners = component.worldCorners;
        vector3 = Vector3.op_Multiply(Vector3.op_Addition(worldCorners[0], worldCorners[2]), 0.5f);
      }
      Vector3 screenPoint = cameraForLayer.cachedCamera.WorldToScreenPoint(vector3);
      screenPoint.z = 0.0f;
      return screenPoint;
    }
    if (!Object.op_Inequality((Object) component, (Object) null))
      return go.transform.position;
    Vector3[] worldCorners1 = component.worldCorners;
    return Vector3.op_Multiply(Vector3.op_Addition(worldCorners1[0], worldCorners1[2]), 0.5f);
  }

  public virtual void OnNavigate(KeyCode key)
  {
    if (UIPopupList.isOpen)
      return;
    GameObject gameObject = (GameObject) null;
    switch (key - 273)
    {
      case 0:
        gameObject = this.GetUp();
        break;
      case 1:
        gameObject = this.GetDown();
        break;
      case 2:
        gameObject = this.GetRight();
        break;
      case 3:
        gameObject = this.GetLeft();
        break;
    }
    if (!Object.op_Inequality((Object) gameObject, (Object) null))
      return;
    UICamera.hoveredObject = gameObject;
  }

  public virtual void OnKey(KeyCode key)
  {
    if (key != 9)
      return;
    GameObject gameObject = this.onTab;
    if (Object.op_Equality((Object) gameObject, (Object) null))
    {
      if (UICamera.GetKey((KeyCode) 304) || UICamera.GetKey((KeyCode) 303))
      {
        gameObject = this.GetLeft();
        if (Object.op_Equality((Object) gameObject, (Object) null))
          gameObject = this.GetUp();
        if (Object.op_Equality((Object) gameObject, (Object) null))
          gameObject = this.GetDown();
        if (Object.op_Equality((Object) gameObject, (Object) null))
          gameObject = this.GetRight();
      }
      else
      {
        gameObject = this.GetRight();
        if (Object.op_Equality((Object) gameObject, (Object) null))
          gameObject = this.GetDown();
        if (Object.op_Equality((Object) gameObject, (Object) null))
          gameObject = this.GetUp();
        if (Object.op_Equality((Object) gameObject, (Object) null))
          gameObject = this.GetLeft();
      }
    }
    if (!Object.op_Inequality((Object) gameObject, (Object) null))
      return;
    UICamera.selectedObject = gameObject;
  }

  protected virtual void OnClick()
  {
    if (!NGUITools.GetActive(this.onClick))
      return;
    UICamera.hoveredObject = this.onClick;
  }

  public enum Constraint
  {
    None,
    Vertical,
    Horizontal,
    Explicit,
  }
}

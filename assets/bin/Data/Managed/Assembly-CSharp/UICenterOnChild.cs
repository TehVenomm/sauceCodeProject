// Decompiled with JetBrains decompiler
// Type: UICenterOnChild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild : MonoBehaviour
{
  public float springStrength = 8f;
  public float nextPageThreshold;
  public SpringPanel.OnFinished onFinished;
  public UICenterOnChild.OnCenterCallback onCenter;
  private UIScrollView mScrollView;
  private GameObject mCenteredObject;

  public GameObject centeredObject => this.mCenteredObject;

  private void Start() => this.Recenter();

  private void OnEnable()
  {
    if (!Object.op_Implicit((Object) this.mScrollView))
      return;
    this.mScrollView.centerOnChild = this;
    this.Recenter();
  }

  private void OnDisable()
  {
    if (!Object.op_Implicit((Object) this.mScrollView))
      return;
    this.mScrollView.centerOnChild = (UICenterOnChild) null;
  }

  private void OnDragFinished()
  {
    if (!((Behaviour) this).enabled)
      return;
    this.Recenter();
  }

  private void OnValidate() => this.nextPageThreshold = Mathf.Abs(this.nextPageThreshold);

  [ContextMenu("Execute")]
  public void Recenter()
  {
    if (Object.op_Equality((Object) this.mScrollView, (Object) null))
    {
      this.mScrollView = NGUITools.FindInParents<UIScrollView>(((Component) this).gameObject);
      if (Object.op_Equality((Object) this.mScrollView, (Object) null))
      {
        Debug.LogWarning((object) $"{(object) ((object) this).GetType()} requires {(object) typeof (UIScrollView)} on a parent object in order to work", (Object) this);
        ((Behaviour) this).enabled = false;
        return;
      }
      if (Object.op_Implicit((Object) this.mScrollView))
      {
        this.mScrollView.centerOnChild = this;
        this.mScrollView.onDragFinished += new UIScrollView.OnDragNotification(this.OnDragFinished);
      }
      if (Object.op_Inequality((Object) this.mScrollView.horizontalScrollBar, (Object) null))
        this.mScrollView.horizontalScrollBar.onDragFinished += new UIProgressBar.OnDragFinished(this.OnDragFinished);
      if (Object.op_Inequality((Object) this.mScrollView.verticalScrollBar, (Object) null))
        this.mScrollView.verticalScrollBar.onDragFinished += new UIProgressBar.OnDragFinished(this.OnDragFinished);
    }
    if (Object.op_Equality((Object) this.mScrollView.panel, (Object) null))
      return;
    Transform transform1 = ((Component) this).transform;
    if (transform1.childCount == 0)
      return;
    Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
    Vector3 panelCenter = Vector3.op_Multiply(Vector3.op_Addition(worldCorners[2], worldCorners[0]), 0.5f);
    Vector3 velocity = Vector3.op_Multiply(this.mScrollView.currentMomentum, this.mScrollView.momentumAmount);
    Vector3 vector3_1 = NGUIMath.SpringDampen(ref velocity, 9f, 2f);
    Vector3 vector3_2 = Vector3.op_Subtraction(panelCenter, Vector3.op_Multiply(vector3_1, 0.01f));
    float num1 = float.MaxValue;
    Transform target = (Transform) null;
    int index1 = 0;
    int num2 = 0;
    UIGrid component = ((Component) this).GetComponent<UIGrid>();
    List<Transform> transformList = (List<Transform>) null;
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      transformList = component.GetChildList();
      int index2 = 0;
      int count = transformList.Count;
      int num3 = 0;
      for (; index2 < count; ++index2)
      {
        Transform transform2 = transformList[index2];
        if (((Component) transform2).gameObject.activeInHierarchy)
        {
          float num4 = Vector3.SqrMagnitude(Vector3.op_Subtraction(transform2.position, vector3_2));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            target = transform2;
            index1 = index2;
            num2 = num3;
          }
          ++num3;
        }
      }
    }
    else
    {
      int num5 = 0;
      int childCount = transform1.childCount;
      int num6 = 0;
      for (; num5 < childCount; ++num5)
      {
        Transform child = transform1.GetChild(num5);
        if (((Component) child).gameObject.activeInHierarchy)
        {
          float num7 = Vector3.SqrMagnitude(Vector3.op_Subtraction(child.position, vector3_2));
          if ((double) num7 < (double) num1)
          {
            num1 = num7;
            target = child;
            index1 = num5;
            num2 = num6;
          }
          ++num6;
        }
      }
    }
    if ((double) this.nextPageThreshold > 0.0 && UICamera.currentTouch != null && Object.op_Inequality((Object) this.mCenteredObject, (Object) null) && Object.op_Equality((Object) this.mCenteredObject.transform, transformList != null ? (Object) transformList[index1] : (Object) transform1.GetChild(index1)))
    {
      Vector3 vector3_3 = Quaternion.op_Multiply(((Component) this).transform.rotation, Vector2.op_Implicit(UICamera.currentTouch.totalDelta));
      float num8;
      switch (this.mScrollView.movement)
      {
        case UIScrollView.Movement.Horizontal:
          num8 = vector3_3.x;
          break;
        case UIScrollView.Movement.Vertical:
          num8 = vector3_3.y;
          break;
        default:
          num8 = ((Vector3) ref vector3_3).magnitude;
          break;
      }
      if ((double) Mathf.Abs(num8) > (double) this.nextPageThreshold)
      {
        if ((double) num8 > (double) this.nextPageThreshold)
          target = transformList == null ? (num2 <= 0 ? (Object.op_Equality((Object) ((Component) this).GetComponent<UIWrapContent>(), (Object) null) ? transform1.GetChild(0) : transform1.GetChild(transform1.childCount - 1)) : transform1.GetChild(num2 - 1)) : (num2 <= 0 ? (Object.op_Equality((Object) ((Component) this).GetComponent<UIWrapContent>(), (Object) null) ? transformList[0] : transformList[transformList.Count - 1]) : transformList[num2 - 1]);
        else if ((double) num8 < -(double) this.nextPageThreshold)
          target = transformList == null ? (num2 >= transform1.childCount - 1 ? (Object.op_Equality((Object) ((Component) this).GetComponent<UIWrapContent>(), (Object) null) ? transform1.GetChild(transform1.childCount - 1) : transform1.GetChild(0)) : transform1.GetChild(num2 + 1)) : (num2 >= transformList.Count - 1 ? (Object.op_Equality((Object) ((Component) this).GetComponent<UIWrapContent>(), (Object) null) ? transformList[transformList.Count - 1] : transformList[0]) : transformList[num2 + 1]);
      }
    }
    this.CenterOn(target, panelCenter);
  }

  private void CenterOn(Transform target, Vector3 panelCenter)
  {
    if (Object.op_Inequality((Object) target, (Object) null) && Object.op_Inequality((Object) this.mScrollView, (Object) null) && Object.op_Inequality((Object) this.mScrollView.panel, (Object) null))
    {
      Transform cachedTransform = this.mScrollView.panel.cachedTransform;
      this.mCenteredObject = ((Component) target).gameObject;
      Vector3 vector3 = Vector3.op_Subtraction(cachedTransform.InverseTransformPoint(target.position), cachedTransform.InverseTransformPoint(panelCenter));
      if (!this.mScrollView.canMoveHorizontally)
        vector3.x = 0.0f;
      if (!this.mScrollView.canMoveVertically)
        vector3.y = 0.0f;
      vector3.z = 0.0f;
      SpringPanel.Begin(this.mScrollView.panel.cachedGameObject, Vector3.op_Subtraction(cachedTransform.localPosition, vector3), this.springStrength).onFinished = this.onFinished;
    }
    else
      this.mCenteredObject = (GameObject) null;
    if (this.onCenter == null)
      return;
    this.onCenter(this.mCenteredObject);
  }

  public void CenterOn(Transform target)
  {
    if (!Object.op_Inequality((Object) this.mScrollView, (Object) null) || !Object.op_Inequality((Object) this.mScrollView.panel, (Object) null))
      return;
    Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
    Vector3 panelCenter = Vector3.op_Multiply(Vector3.op_Addition(worldCorners[2], worldCorners[0]), 0.5f);
    this.CenterOn(target, panelCenter);
  }

  public delegate void OnCenterCallback(GameObject centeredObject);
}

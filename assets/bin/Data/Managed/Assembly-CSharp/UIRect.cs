// Decompiled with JetBrains decompiler
// Type: UIRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class UIRect : MonoBehaviour
{
  public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();
  public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);
  public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();
  public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);
  public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnStart;
  protected GameObject mGo;
  protected Transform mTrans;
  protected BetterList<UIRect> mChildren = new BetterList<UIRect>();
  protected bool mChanged = true;
  protected bool mStarted;
  protected bool mParentFound;
  [NonSerialized]
  private bool mUpdateAnchors = true;
  [NonSerialized]
  private int mUpdateFrame = -1;
  [NonSerialized]
  private bool mAnchorsCached;
  [NonSerialized]
  private UIRoot mRoot;
  [NonSerialized]
  private UIRect mParent;
  [NonSerialized]
  private bool mRootSet;
  [NonSerialized]
  protected Camera mCam;
  [NonSerialized]
  public float finalAlpha = 1f;
  protected static Vector3[] mSides = new Vector3[4];

  public GameObject cachedGameObject
  {
    get
    {
      if (Object.op_Equality((Object) this.mGo, (Object) null))
        this.mGo = ((Component) this).gameObject;
      return this.mGo;
    }
  }

  public Transform cachedTransform
  {
    get
    {
      if (Object.op_Equality((Object) this.mTrans, (Object) null))
        this.mTrans = ((Component) this).transform;
      return this.mTrans;
    }
  }

  public Camera anchorCamera
  {
    get
    {
      if (!this.mAnchorsCached)
        this.ResetAnchors();
      return this.mCam;
    }
  }

  public bool isFullyAnchored
  {
    get
    {
      return Object.op_Implicit((Object) this.leftAnchor.target) && Object.op_Implicit((Object) this.rightAnchor.target) && Object.op_Implicit((Object) this.topAnchor.target) && Object.op_Implicit((Object) this.bottomAnchor.target);
    }
  }

  public virtual bool isAnchoredHorizontally
  {
    get
    {
      return Object.op_Implicit((Object) this.leftAnchor.target) || Object.op_Implicit((Object) this.rightAnchor.target);
    }
  }

  public virtual bool isAnchoredVertically
  {
    get
    {
      return Object.op_Implicit((Object) this.bottomAnchor.target) || Object.op_Implicit((Object) this.topAnchor.target);
    }
  }

  public virtual bool canBeAnchored => true;

  public UIRect parent
  {
    get
    {
      if (!this.mParentFound)
      {
        this.mParentFound = true;
        this.mParent = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
      }
      return this.mParent;
    }
  }

  public UIRoot root
  {
    get
    {
      if (Object.op_Inequality((Object) this.parent, (Object) null))
        return this.mParent.root;
      if (!this.mRootSet)
      {
        this.mRootSet = true;
        this.mRoot = NGUITools.FindInParents<UIRoot>(this.cachedTransform);
      }
      return this.mRoot;
    }
  }

  public bool isAnchored
  {
    get
    {
      return (Object.op_Implicit((Object) this.leftAnchor.target) || Object.op_Implicit((Object) this.rightAnchor.target) || Object.op_Implicit((Object) this.topAnchor.target) || Object.op_Implicit((Object) this.bottomAnchor.target)) && this.canBeAnchored;
    }
  }

  public abstract float alpha { get; set; }

  public abstract float CalculateFinalAlpha(int frameID);

  public abstract Vector3[] localCorners { get; }

  public abstract Vector3[] worldCorners { get; }

  protected float cameraRayDistance
  {
    get
    {
      if (Object.op_Equality((Object) this.anchorCamera, (Object) null))
        return 0.0f;
      if (!this.mCam.orthographic)
      {
        Transform cachedTransform = this.cachedTransform;
        Transform transform = ((Component) this.mCam).transform;
        Plane plane;
        ((Plane) ref plane).\u002Ector(Quaternion.op_Multiply(cachedTransform.rotation, Vector3.back), cachedTransform.position);
        Ray ray;
        ((Ray) ref ray).\u002Ector(transform.position, Quaternion.op_Multiply(transform.rotation, Vector3.forward));
        float cameraRayDistance;
        if (((Plane) ref plane).Raycast(ray, ref cameraRayDistance))
          return cameraRayDistance;
      }
      return Mathf.Lerp(this.mCam.nearClipPlane, this.mCam.farClipPlane, 0.5f);
    }
  }

  public virtual void Invalidate(bool includeChildren)
  {
    this.mChanged = true;
    if (!includeChildren)
      return;
    for (int index = 0; index < this.mChildren.size; ++index)
      this.mChildren.buffer[index].Invalidate(true);
  }

  public virtual Vector3[] GetSides(Transform relativeTo)
  {
    if (Object.op_Inequality((Object) this.anchorCamera, (Object) null))
      return this.mCam.GetSides(this.cameraRayDistance, relativeTo);
    Vector3 position = this.cachedTransform.position;
    for (int index = 0; index < 4; ++index)
      UIRect.mSides[index] = position;
    if (Object.op_Inequality((Object) relativeTo, (Object) null))
    {
      for (int index = 0; index < 4; ++index)
        UIRect.mSides[index] = relativeTo.InverseTransformPoint(UIRect.mSides[index]);
    }
    return UIRect.mSides;
  }

  protected Vector3 GetLocalPos(UIRect.AnchorPoint ac, Transform trans)
  {
    if (Object.op_Equality((Object) this.anchorCamera, (Object) null) || Object.op_Equality((Object) ac.targetCam, (Object) null))
      return this.cachedTransform.localPosition;
    Rect rect = ac.targetCam.rect;
    Vector3 viewportPoint = ac.targetCam.WorldToViewportPoint(ac.target.position);
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(viewportPoint.x * ((Rect) ref rect).width + ((Rect) ref rect).x, viewportPoint.y * ((Rect) ref rect).height + ((Rect) ref rect).y, viewportPoint.z);
    Vector3 localPos = this.mCam.ViewportToWorldPoint(vector3);
    if (Object.op_Inequality((Object) trans, (Object) null))
      localPos = trans.InverseTransformPoint(localPos);
    localPos.x = Mathf.Floor(localPos.x + 0.5f);
    localPos.y = Mathf.Floor(localPos.y + 0.5f);
    return localPos;
  }

  protected virtual void OnEnable()
  {
    UpdaterBase.Add(new System.Action(this._Update));
    this.mUpdateFrame = -1;
    if (this.updateAnchors == UIRect.AnchorUpdate.OnEnable)
    {
      this.mAnchorsCached = false;
      this.mUpdateAnchors = true;
    }
    if (this.mStarted)
      this.OnInit();
    this.mUpdateFrame = -1;
  }

  protected virtual void OnInit()
  {
    this.mChanged = true;
    this.mRootSet = false;
    this.mParentFound = false;
    if (!Object.op_Inequality((Object) this.parent, (Object) null))
      return;
    this.mParent.mChildren.Add(this);
  }

  protected virtual void OnDisable()
  {
    if (Object.op_Implicit((Object) this.mParent))
      this.mParent.mChildren.Remove(this);
    this.mParent = (UIRect) null;
    this.mRoot = (UIRoot) null;
    this.mRootSet = false;
    this.mParentFound = false;
    UpdaterBase.Remove(new System.Action(this._Update));
  }

  protected void Start()
  {
    this.mStarted = true;
    this.OnInit();
    this.OnStart();
  }

  public void _Update()
  {
    if (!this.mAnchorsCached)
      this.ResetAnchors();
    int frameCount = Time.frameCount;
    if (this.mUpdateFrame == frameCount)
      return;
    if (this.updateAnchors == UIRect.AnchorUpdate.OnUpdate || this.mUpdateAnchors)
      this.UpdateAnchorsInternal(frameCount);
    this.OnUpdate();
  }

  protected void UpdateAnchorsInternal(int frame)
  {
    this.mUpdateFrame = frame;
    this.mUpdateAnchors = false;
    bool flag = false;
    if (Object.op_Implicit((Object) this.leftAnchor.target))
    {
      flag = true;
      if (Object.op_Inequality((Object) this.leftAnchor.rect, (Object) null) && this.leftAnchor.rect.mUpdateFrame != frame)
        this.leftAnchor.rect._Update();
    }
    if (Object.op_Implicit((Object) this.bottomAnchor.target))
    {
      flag = true;
      if (Object.op_Inequality((Object) this.bottomAnchor.rect, (Object) null) && this.bottomAnchor.rect.mUpdateFrame != frame)
        this.bottomAnchor.rect._Update();
    }
    if (Object.op_Implicit((Object) this.rightAnchor.target))
    {
      flag = true;
      if (Object.op_Inequality((Object) this.rightAnchor.rect, (Object) null) && this.rightAnchor.rect.mUpdateFrame != frame)
        this.rightAnchor.rect._Update();
    }
    if (Object.op_Implicit((Object) this.topAnchor.target))
    {
      flag = true;
      if (Object.op_Inequality((Object) this.topAnchor.rect, (Object) null) && this.topAnchor.rect.mUpdateFrame != frame)
        this.topAnchor.rect._Update();
    }
    if (!flag)
      return;
    this.OnAnchor();
  }

  public void UpdateAnchors()
  {
    if (!this.isAnchored)
      return;
    this.mUpdateFrame = -1;
    this.mUpdateAnchors = true;
    if (Object.op_Inequality((Object) this.mGo, (Object) null) && !this.mGo.activeInHierarchy)
      return;
    this.UpdateAnchorsInternal(Time.frameCount);
  }

  protected abstract void OnAnchor();

  public void SetAnchor(Transform t)
  {
    this.leftAnchor.target = t;
    this.rightAnchor.target = t;
    this.topAnchor.target = t;
    this.bottomAnchor.target = t;
    this.ResetAnchors();
    this.UpdateAnchors();
  }

  public void SetAnchor(GameObject go)
  {
    Transform transform = Object.op_Inequality((Object) go, (Object) null) ? go.transform : (Transform) null;
    this.leftAnchor.target = transform;
    this.rightAnchor.target = transform;
    this.topAnchor.target = transform;
    this.bottomAnchor.target = transform;
    this.ResetAnchors();
    this.UpdateAnchors();
  }

  public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
  {
    Transform transform = Object.op_Inequality((Object) go, (Object) null) ? go.transform : (Transform) null;
    this.leftAnchor.target = transform;
    this.rightAnchor.target = transform;
    this.topAnchor.target = transform;
    this.bottomAnchor.target = transform;
    this.leftAnchor.relative = 0.0f;
    this.rightAnchor.relative = 1f;
    this.bottomAnchor.relative = 0.0f;
    this.topAnchor.relative = 1f;
    this.leftAnchor.absolute = left;
    this.rightAnchor.absolute = right;
    this.bottomAnchor.absolute = bottom;
    this.topAnchor.absolute = top;
    this.ResetAnchors();
    this.UpdateAnchors();
  }

  public void ResetAnchors()
  {
    this.mAnchorsCached = true;
    this.leftAnchor.rect = Object.op_Implicit((Object) this.leftAnchor.target) ? ((Component) this.leftAnchor.target).GetComponent<UIRect>() : (UIRect) null;
    this.bottomAnchor.rect = Object.op_Implicit((Object) this.bottomAnchor.target) ? ((Component) this.bottomAnchor.target).GetComponent<UIRect>() : (UIRect) null;
    this.rightAnchor.rect = Object.op_Implicit((Object) this.rightAnchor.target) ? ((Component) this.rightAnchor.target).GetComponent<UIRect>() : (UIRect) null;
    this.topAnchor.rect = Object.op_Implicit((Object) this.topAnchor.target) ? ((Component) this.topAnchor.target).GetComponent<UIRect>() : (UIRect) null;
    this.mCam = NGUITools.FindCameraForLayer(this.cachedGameObject.layer);
    this.FindCameraFor(this.leftAnchor);
    this.FindCameraFor(this.bottomAnchor);
    this.FindCameraFor(this.rightAnchor);
    this.FindCameraFor(this.topAnchor);
    this.mUpdateAnchors = true;
  }

  public void ResetAndUpdateAnchors()
  {
    this.ResetAnchors();
    this.UpdateAnchors();
  }

  public abstract void SetRect(float x, float y, float width, float height);

  private void FindCameraFor(UIRect.AnchorPoint ap)
  {
    if (Object.op_Equality((Object) ap.target, (Object) null) || Object.op_Inequality((Object) ap.rect, (Object) null))
      ap.targetCam = (Camera) null;
    else
      ap.targetCam = NGUITools.FindCameraForLayer(((Component) ap.target).gameObject.layer);
  }

  public virtual void ParentHasChanged()
  {
    this.mParentFound = false;
    UIRect inParents = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
    if (!Object.op_Inequality((Object) this.mParent, (Object) inParents))
      return;
    if (Object.op_Implicit((Object) this.mParent))
      this.mParent.mChildren.Remove(this);
    this.mParent = inParents;
    if (Object.op_Implicit((Object) this.mParent))
      this.mParent.mChildren.Add(this);
    this.mRootSet = false;
  }

  protected abstract void OnStart();

  protected virtual void OnUpdate()
  {
  }

  [Serializable]
  public class AnchorPoint
  {
    public Transform target;
    public float relative;
    public int absolute;
    [NonSerialized]
    public UIRect rect;
    [NonSerialized]
    public Camera targetCam;

    public AnchorPoint()
    {
    }

    public AnchorPoint(float relative) => this.relative = relative;

    public void Set(float relative, float absolute)
    {
      this.relative = relative;
      this.absolute = Mathf.FloorToInt(absolute + 0.5f);
    }

    public void Set(Transform target, float relative, float absolute)
    {
      this.target = target;
      this.relative = relative;
      this.absolute = Mathf.FloorToInt(absolute + 0.5f);
    }

    public void SetToNearest(float abs0, float abs1, float abs2)
    {
      this.SetToNearest(0.0f, 0.5f, 1f, abs0, abs1, abs2);
    }

    public void SetToNearest(
      float rel0,
      float rel1,
      float rel2,
      float abs0,
      float abs1,
      float abs2)
    {
      float num1 = Mathf.Abs(abs0);
      float num2 = Mathf.Abs(abs1);
      float num3 = Mathf.Abs(abs2);
      if ((double) num1 < (double) num2 && (double) num1 < (double) num3)
        this.Set(rel0, abs0);
      else if ((double) num2 < (double) num1 && (double) num2 < (double) num3)
        this.Set(rel1, abs1);
      else
        this.Set(rel2, abs2);
    }

    public void SetHorizontal(Transform parent, float localPos)
    {
      if (Object.op_Implicit((Object) this.rect))
      {
        Vector3[] sides = this.rect.GetSides(parent);
        float num = Mathf.Lerp(sides[0].x, sides[2].x, this.relative);
        this.absolute = Mathf.FloorToInt((float) ((double) localPos - (double) num + 0.5));
      }
      else
      {
        Vector3 vector3 = this.target.position;
        if (Object.op_Inequality((Object) parent, (Object) null))
          vector3 = parent.InverseTransformPoint(vector3);
        this.absolute = Mathf.FloorToInt((float) ((double) localPos - (double) vector3.x + 0.5));
      }
    }

    public void SetVertical(Transform parent, float localPos)
    {
      if (Object.op_Implicit((Object) this.rect))
      {
        Vector3[] sides = this.rect.GetSides(parent);
        float num = Mathf.Lerp(sides[3].y, sides[1].y, this.relative);
        this.absolute = Mathf.FloorToInt((float) ((double) localPos - (double) num + 0.5));
      }
      else
      {
        Vector3 vector3 = this.target.position;
        if (Object.op_Inequality((Object) parent, (Object) null))
          vector3 = parent.InverseTransformPoint(vector3);
        this.absolute = Mathf.FloorToInt((float) ((double) localPos - (double) vector3.y + 0.5));
      }
    }

    public Vector3[] GetSides(Transform relativeTo)
    {
      if (Object.op_Inequality((Object) this.target, (Object) null))
      {
        if (Object.op_Inequality((Object) this.rect, (Object) null))
          return this.rect.GetSides(relativeTo);
        if (Object.op_Inequality((Object) ((Component) this.target).GetComponent<Camera>(), (Object) null))
          return ((Component) this.target).GetComponent<Camera>().GetSides(relativeTo);
      }
      return (Vector3[]) null;
    }
  }

  public enum AnchorUpdate
  {
    OnEnable,
    OnStart,
    OnStart2,
    OnUpdate,
  }
}

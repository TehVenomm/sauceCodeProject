// Decompiled with JetBrains decompiler
// Type: UIGrid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
  public UIGrid.Arrangement arrangement;
  public UIGrid.Sorting sorting;
  public UIWidget.Pivot pivot;
  public int maxPerLine;
  public float cellWidth = 200f;
  public float cellHeight = 200f;
  public bool animateSmoothly;
  public bool hideInactive;
  public bool keepWithinPanel;
  public UIGrid.OnReposition onReposition;
  public Comparison<Transform> onCustomSort;
  [HideInInspector]
  [SerializeField]
  private bool sorted;
  protected bool mReposition;
  protected UIPanel mPanel;
  protected bool mInitDone;

  public bool repositionNow
  {
    set
    {
      if (!value)
        return;
      this.mReposition = true;
      ((Behaviour) this).enabled = true;
    }
  }

  public List<Transform> GetChildList()
  {
    Transform transform = ((Component) this).transform;
    List<Transform> list = new List<Transform>();
    for (int index = 0; index < transform.childCount; ++index)
    {
      Transform child = transform.GetChild(index);
      if (!this.hideInactive || Object.op_Implicit((Object) child) && NGUITools.GetActive(((Component) child).gameObject))
        list.Add(child);
    }
    if (this.sorting != UIGrid.Sorting.None && this.arrangement != UIGrid.Arrangement.CellSnap)
    {
      if (this.sorting == UIGrid.Sorting.Alphabetic)
        list.Sort(new Comparison<Transform>(UIGrid.SortByName));
      else if (this.sorting == UIGrid.Sorting.Horizontal)
        list.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
      else if (this.sorting == UIGrid.Sorting.Vertical)
        list.Sort(new Comparison<Transform>(UIGrid.SortVertical));
      else if (this.onCustomSort != null)
        list.Sort(this.onCustomSort);
      else
        this.Sort(list);
    }
    return list;
  }

  public Transform GetChild(int index)
  {
    List<Transform> childList = this.GetChildList();
    return index >= childList.Count ? (Transform) null : childList[index];
  }

  public int GetIndex(Transform trans) => this.GetChildList().IndexOf(trans);

  public void AddChild(Transform trans) => this.AddChild(trans, true);

  public void AddChild(Transform trans, bool sort)
  {
    if (!Object.op_Inequality((Object) trans, (Object) null))
      return;
    trans.parent = ((Component) this).transform;
    this.ResetPosition(this.GetChildList());
  }

  public bool RemoveChild(Transform t)
  {
    List<Transform> childList = this.GetChildList();
    if (!childList.Remove(t))
      return false;
    this.ResetPosition(childList);
    return true;
  }

  protected virtual void Init()
  {
    this.mInitDone = true;
    this.mPanel = NGUITools.FindInParents<UIPanel>(((Component) this).gameObject);
  }

  protected virtual void Start()
  {
    if (!this.mInitDone)
      this.Init();
    bool animateSmoothly = this.animateSmoothly;
    this.animateSmoothly = false;
    this.Reposition();
    this.animateSmoothly = animateSmoothly;
    ((Behaviour) this).enabled = false;
  }

  protected virtual void Update()
  {
    this.Reposition();
    ((Behaviour) this).enabled = false;
  }

  private void OnValidate()
  {
    if (Application.isPlaying || !NGUITools.GetActive((Behaviour) this))
      return;
    this.Reposition();
  }

  public static int SortByName(Transform a, Transform b)
  {
    return string.Compare(((Object) a).name, ((Object) b).name);
  }

  public static int SortHorizontal(Transform a, Transform b)
  {
    return a.localPosition.x.CompareTo(b.localPosition.x);
  }

  public static int SortVertical(Transform a, Transform b)
  {
    return b.localPosition.y.CompareTo(a.localPosition.y);
  }

  protected virtual void Sort(List<Transform> list)
  {
  }

  [ContextMenu("Execute")]
  public virtual void Reposition()
  {
    if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(((Component) this).gameObject))
      this.Init();
    if (this.sorted)
    {
      this.sorted = false;
      if (this.sorting == UIGrid.Sorting.None)
        this.sorting = UIGrid.Sorting.Alphabetic;
      NGUITools.SetDirty((Object) this);
    }
    this.ResetPosition(this.GetChildList());
    if (this.keepWithinPanel)
      this.ConstrainWithinPanel();
    if (this.onReposition == null)
      return;
    this.onReposition();
  }

  public void ConstrainWithinPanel()
  {
    if (!Object.op_Inequality((Object) this.mPanel, (Object) null))
      return;
    this.mPanel.ConstrainTargetToBounds(((Component) this).transform, true);
    UIScrollView component = ((Component) this.mPanel).GetComponent<UIScrollView>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.UpdateScrollbars(true);
  }

  protected virtual void ResetPosition(List<Transform> list)
  {
    this.mReposition = false;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    Transform transform1 = ((Component) this).transform;
    int index1 = 0;
    for (int count = list.Count; index1 < count; ++index1)
    {
      Transform transform2 = list[index1];
      Vector3 pos = transform2.localPosition;
      float z = pos.z;
      if (this.arrangement == UIGrid.Arrangement.CellSnap)
      {
        if ((double) this.cellWidth > 0.0)
          pos.x = Mathf.Round(pos.x / this.cellWidth) * this.cellWidth;
        if ((double) this.cellHeight > 0.0)
          pos.y = Mathf.Round(pos.y / this.cellHeight) * this.cellHeight;
      }
      else
        pos = this.arrangement == UIGrid.Arrangement.Horizontal ? new Vector3(this.cellWidth * (float) num1, -this.cellHeight * (float) num2, z) : new Vector3(this.cellWidth * (float) num2, -this.cellHeight * (float) num1, z);
      if (this.animateSmoothly && Application.isPlaying)
      {
        SpringPosition springPosition = SpringPosition.Begin(((Component) transform2).gameObject, pos, 15f);
        springPosition.updateScrollView = true;
        springPosition.ignoreTimeScale = true;
      }
      else
        transform2.localPosition = pos;
      num3 = Mathf.Max(num3, num1);
      num4 = Mathf.Max(num4, num2);
      if (++num1 >= this.maxPerLine && this.maxPerLine > 0)
      {
        num1 = 0;
        ++num2;
      }
    }
    if (this.pivot == UIWidget.Pivot.TopLeft)
      return;
    Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
    float num5;
    float num6;
    if (this.arrangement == UIGrid.Arrangement.Horizontal)
    {
      num5 = Mathf.Lerp(0.0f, (float) num3 * this.cellWidth, pivotOffset.x);
      num6 = Mathf.Lerp((float) -num4 * this.cellHeight, 0.0f, pivotOffset.y);
    }
    else
    {
      num5 = Mathf.Lerp(0.0f, (float) num4 * this.cellWidth, pivotOffset.x);
      num6 = Mathf.Lerp((float) -num3 * this.cellHeight, 0.0f, pivotOffset.y);
    }
    for (int index2 = 0; index2 < transform1.childCount; ++index2)
    {
      Transform child = transform1.GetChild(index2);
      SpringPosition component = ((Component) child).GetComponent<SpringPosition>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.target.x -= num5;
        component.target.y -= num6;
      }
      else
      {
        Vector3 localPosition = child.localPosition;
        localPosition.x -= num5;
        localPosition.y -= num6;
        child.localPosition = localPosition;
      }
    }
  }

  public delegate void OnReposition();

  public enum Arrangement
  {
    Horizontal,
    Vertical,
    CellSnap,
  }

  public enum Sorting
  {
    None,
    Alphabetic,
    Horizontal,
    Vertical,
    Custom,
  }
}

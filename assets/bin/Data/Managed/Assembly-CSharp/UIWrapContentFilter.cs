// Decompiled with JetBrains decompiler
// Type: UIWrapContentFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Wrap Content Filter")]
public class UIWrapContentFilter : MonoBehaviour
{
  public Func<int, string, bool> FilterItemFunc;
  private string _filter = string.Empty;
  public int itemSize = 100;
  public bool cullContent = true;
  public int minIndex;
  public int maxIndex;
  public UIWrapContentFilter.OnInitializeItem onInitializeItem;
  private Transform mTrans;
  private UIPanel mPanel;
  private UIScrollView mScroll;
  private bool mHorizontal;
  private bool mFirstTime = true;
  private List<Transform> mChildren = new List<Transform>();

  public string filter
  {
    get => this._filter;
    set
    {
      if (this._filter == value)
        return;
      this._filter = value;
      this.FilterList(this._filter);
      this.WrapContent();
    }
  }

  protected virtual void Start()
  {
    this.SortBasedOnScrollMovement();
    this.WrapContent();
    if (Object.op_Inequality((Object) this.mScroll, (Object) null))
      ((Component) this.mScroll).GetComponent<UIPanel>().onClipMove = new UIPanel.OnClippingMoved(this.OnMove);
    this.mFirstTime = false;
  }

  protected virtual void OnMove(UIPanel panel) => this.WrapContent();

  public virtual void Initialize(Func<int, string, bool> filter_item_func = null)
  {
    this.FilterItemFunc = filter_item_func;
    this.SortAlphabetically();
  }

  [ContextMenu("Sort Based on Scroll Movement")]
  public void SortBasedOnScrollMovement()
  {
    if (!this.CacheScrollView())
      return;
    this.mChildren.Clear();
    for (int index = 0; index < this.mTrans.childCount; ++index)
      this.mChildren.Add(this.mTrans.GetChild(index));
    if (this.mHorizontal)
      this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
    else
      this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortVertical));
    this.ResetChildPositions();
  }

  [ContextMenu("Sort Alphabetically")]
  public void SortAlphabetically()
  {
    if (!this.CacheScrollView())
      return;
    if (!((Behaviour) this.mScroll).enabled)
      ((Behaviour) this.mScroll).enabled = true;
    this.mChildren.Clear();
    for (int index = 0; index < this.mTrans.childCount; ++index)
      this.mChildren.Add(this.mTrans.GetChild(index));
    this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
    this.ResetChildPositions();
  }

  public void FilterList(string filterName = null)
  {
    this.mChildren.Clear();
    for (int index = 0; index < this.mTrans.childCount; ++index)
    {
      if (string.IsNullOrEmpty(filterName))
        this.mChildren.Add(this.mTrans.GetChild(index));
      else if (this.FilterItemFunc != null && this.FilterItemFunc(index, filterName))
      {
        Transform child = this.mTrans.GetChild(index);
        ((Component) child).gameObject.SetActive(true);
        this.mChildren.Add(child);
      }
      else
        ((Component) this.mTrans.GetChild(index)).gameObject.SetActive(false);
    }
    this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
    this.ResetChildPositions();
    this.mScroll.ResetPosition();
  }

  protected bool CacheScrollView()
  {
    this.mTrans = ((Component) this).transform;
    this.mPanel = NGUITools.FindInParents<UIPanel>(((Component) this).gameObject);
    this.mScroll = ((Component) this.mPanel).GetComponent<UIScrollView>();
    if (Object.op_Equality((Object) this.mScroll, (Object) null))
      return false;
    if (this.mScroll.movement == UIScrollView.Movement.Horizontal)
    {
      this.mHorizontal = true;
    }
    else
    {
      if (this.mScroll.movement != UIScrollView.Movement.Vertical)
        return false;
      this.mHorizontal = false;
    }
    return true;
  }

  private void ResetChildPositions()
  {
    int index = 0;
    for (int count = this.mChildren.Count; index < count; ++index)
    {
      Transform mChild = this.mChildren[index];
      mChild.localPosition = this.mHorizontal ? new Vector3((float) (index * this.itemSize), 0.0f, 0.0f) : new Vector3(0.0f, (float) (-index * this.itemSize), 0.0f);
      this.UpdateItem(mChild, index);
    }
  }

  public void WrapContent()
  {
    float num1 = (float) (this.itemSize * this.mChildren.Count) * 0.5f;
    Vector3[] worldCorners = this.mPanel.worldCorners;
    for (int index = 0; index < 4; ++index)
    {
      Vector3 vector3 = this.mTrans.InverseTransformPoint(worldCorners[index]);
      worldCorners[index] = vector3;
    }
    Vector3 vector3_1 = Vector3.Lerp(worldCorners[0], worldCorners[2], 0.5f);
    if (this.mHorizontal)
    {
      float num2 = worldCorners[0].x - (float) this.itemSize;
      float num3 = worldCorners[2].x + (float) this.itemSize;
      int index = 0;
      for (int count = this.mChildren.Count; index < count; ++index)
      {
        Transform mChild = this.mChildren[index];
        float num4 = mChild.localPosition.x - vector3_1.x;
        if (this.mFirstTime)
          this.UpdateItem(mChild, index);
        if (this.cullContent)
        {
          float num5 = num4 + (this.mPanel.clipOffset.x - this.mTrans.localPosition.x);
          if (!UICamera.IsPressed(((Component) mChild).gameObject))
            NGUITools.SetActive(((Component) mChild).gameObject, (double) num5 > (double) num2 && (double) num5 < (double) num3, false);
        }
      }
    }
    else
    {
      float num6 = worldCorners[0].y - (float) this.itemSize;
      float num7 = worldCorners[2].y + (float) this.itemSize;
      int index = 0;
      for (int count = this.mChildren.Count; index < count; ++index)
      {
        Transform mChild = this.mChildren[index];
        float num8 = mChild.localPosition.y - vector3_1.y;
        if (this.mFirstTime)
          this.UpdateItem(mChild, index);
        if (this.cullContent)
        {
          float num9 = num8 + (this.mPanel.clipOffset.y - this.mTrans.localPosition.y);
          if (!UICamera.IsPressed(((Component) mChild).gameObject))
            NGUITools.SetActive(((Component) mChild).gameObject, (double) num9 > (double) num6 && (double) num9 < (double) num7, false);
        }
      }
    }
  }

  private void OnValidate()
  {
    if (this.maxIndex < this.minIndex)
      this.maxIndex = this.minIndex;
    if (this.minIndex <= this.maxIndex)
      return;
    this.maxIndex = this.minIndex;
  }

  protected virtual void UpdateItem(Transform item, int index)
  {
    if (this.onInitializeItem == null)
      return;
    int realIndex = this.mScroll.movement == UIScrollView.Movement.Vertical ? Mathf.RoundToInt(item.localPosition.y / (float) this.itemSize) : Mathf.RoundToInt(item.localPosition.x / (float) this.itemSize);
    this.onInitializeItem(((Component) item).gameObject, index, realIndex);
  }

  public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);
}

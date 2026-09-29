// Decompiled with JetBrains decompiler
// Type: UIDynamicList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIDynamicList : MonoBehaviour
{
  private List<UIWidget> itemWidgets;
  private List<Transform> showItems;
  private List<Transform> hideItems;
  private UIScrollView scrollView;
  private Vector3 scrollViewPos;
  private GameObject itemPrefab;
  private bool needCenterOnClickChild;
  private Func<int, Transform, Transform> createItemFunc;
  private Action<int, Transform, bool> initItemFunc;

  public static UIDynamicList Set(
    UIScrollView scroll_view,
    int item_num,
    GameObject item_prefab,
    bool need_center_on_clickchild,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> init_item_func)
  {
    if (Object.op_Equality((Object) scroll_view, (Object) null) || Object.op_Equality((Object) scroll_view.panel, (Object) null))
      return (UIDynamicList) null;
    UIDynamicList uiDynamicList = ((Component) scroll_view).GetComponent<UIDynamicList>();
    if (Object.op_Equality((Object) uiDynamicList, (Object) null))
      uiDynamicList = ((Component) scroll_view).gameObject.AddComponent<UIDynamicList>();
    uiDynamicList.itemWidgets = new List<UIWidget>(item_num);
    uiDynamicList.showItems = new List<Transform>();
    uiDynamicList.hideItems = new List<Transform>();
    uiDynamicList.scrollView = scroll_view;
    uiDynamicList.itemPrefab = item_prefab;
    uiDynamicList.needCenterOnClickChild = need_center_on_clickchild;
    uiDynamicList.createItemFunc = create_item_func;
    uiDynamicList.initItemFunc = init_item_func;
    return uiDynamicList;
  }

  public void AddItemWidget(UIWidget w)
  {
    this.itemWidgets.Add(w);
    if (w.cachedTransform.childCount <= 0)
      return;
    Transform child = w.cachedTransform.GetChild(0);
    ((Component) child).gameObject.SetActive(false);
    this.hideItems.Add(child);
  }

  private void Update()
  {
    if (!Vector3.op_Inequality(((Component) this.scrollView).transform.localPosition, this.scrollViewPos))
      return;
    this.UpdateItems();
  }

  public void UpdateItems()
  {
    this.scrollViewPos = ((Component) this.scrollView).transform.localPosition;
    UIPanel panel = this.scrollView.panel;
    Transform cachedTransform = panel.cachedTransform;
    int index1 = 0;
    for (int count1 = this.itemWidgets.Count; index1 < count1; ++index1)
    {
      UIWidget itemWidget = this.itemWidgets[index1];
      if (panel.IsVisible(itemWidget))
      {
        int num = int.Parse(((Object) itemWidget).name);
        Transform transform;
        bool flag;
        if (itemWidget.cachedTransform.childCount > 0)
        {
          transform = itemWidget.cachedTransform.GetChild(0);
          if (!this.showItems.Contains(transform))
          {
            flag = true;
            this.hideItems.Remove(transform);
          }
          else
            continue;
        }
        else
        {
          int count2 = this.hideItems.Count;
          if (count2 > 0)
          {
            int index2 = count2 - 1;
            transform = this.hideItems[index2];
            this.hideItems.RemoveAt(index2);
            transform.SetParent(itemWidget.cachedTransform, false);
            flag = true;
          }
          else
          {
            transform = this.createItemFunc == null ? (!Object.op_Inequality((Object) this.itemPrefab, (Object) null) ? (Transform) null : ResourceUtility.Realizes((Object) this.itemPrefab, itemWidget.cachedTransform, 5)) : this.createItemFunc(num, itemWidget.cachedTransform);
            if (Object.op_Inequality((Object) transform, (Object) null))
            {
              UIPanel componentInChildren = ((Component) transform).GetComponentInChildren<UIPanel>();
              if (Object.op_Inequality((Object) componentInChildren, (Object) null))
                componentInChildren.depth = panel.depth + 1;
            }
            else
            {
              GameObject gameObject = new GameObject("item");
              gameObject.layer = 5;
              transform = gameObject.transform;
              transform.SetParent(itemWidget.cachedTransform, false);
              gameObject.AddComponent<UIDragScrollView>().scrollView = this.scrollView;
            }
            if (this.needCenterOnClickChild)
              UIUtility.AddCenterOnClickChild(transform);
            flag = false;
          }
        }
        this.showItems.Add(transform);
        ((Component) transform).gameObject.SetActive(true);
        this.initItemFunc(num, transform, flag);
        UIUtility.UpdateAnchors(transform);
      }
      else if (itemWidget.cachedTransform.childCount > 0)
      {
        Transform transform1 = itemWidget.cachedTransform.GetChild(0);
        if (this.showItems.Contains(transform1))
        {
          this.showItems.Remove(transform1);
          this.hideItems.Add(transform1);
          if (Object.op_Inequality((Object) UICamera.selectedObject, (Object) null))
          {
            for (Transform transform2 = UICamera.selectedObject.transform; Object.op_Inequality((Object) transform2, (Object) null) && Object.op_Inequality((Object) cachedTransform, (Object) transform2); transform2 = transform2.parent)
            {
              if (Object.op_Equality((Object) transform1, (Object) transform2))
              {
                transform1 = (Transform) null;
                break;
              }
            }
          }
          if (Object.op_Inequality((Object) transform1, (Object) null))
            ((Component) transform1).gameObject.SetActive(false);
        }
      }
    }
  }
}

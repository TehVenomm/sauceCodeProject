using System;
using System.Collections.Generic;
using UnityEngine;

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

	public static UIDynamicList Set(UIScrollView scroll_view, int item_num, GameObject item_prefab, bool need_center_on_clickchild, Func<int, Transform, Transform> create_item_func, Action<int, Transform, bool> init_item_func)
	{
		if (scroll_view == null || scroll_view.panel == null)
		{
			return null;
		}
		UIDynamicList uIDynamicList = scroll_view.GetComponent<UIDynamicList>();
		if (uIDynamicList == null)
		{
			uIDynamicList = scroll_view.gameObject.AddComponent<UIDynamicList>();
		}
		uIDynamicList.itemWidgets = new List<UIWidget>(item_num);
		uIDynamicList.showItems = new List<Transform>();
		uIDynamicList.hideItems = new List<Transform>();
		uIDynamicList.scrollView = scroll_view;
		uIDynamicList.itemPrefab = item_prefab;
		uIDynamicList.needCenterOnClickChild = need_center_on_clickchild;
		uIDynamicList.createItemFunc = create_item_func;
		uIDynamicList.initItemFunc = init_item_func;
		return uIDynamicList;
	}

	public void AddItemWidget(UIWidget w)
	{
		itemWidgets.Add(w);
		if (w.cachedTransform.childCount > 0)
		{
			Transform child = w.cachedTransform.GetChild(0);
			child.gameObject.SetActive(value: false);
			hideItems.Add(child);
		}
	}

	private void Update()
	{
		if (scrollView.transform.localPosition != scrollViewPos)
		{
			UpdateItems();
		}
	}

	public void UpdateItems()
	{
		scrollViewPos = scrollView.transform.localPosition;
		UIPanel panel = scrollView.panel;
		Transform cachedTransform = panel.cachedTransform;
		int i = 0;
		for (int count = itemWidgets.Count; i < count; i++)
		{
			UIWidget uIWidget = itemWidgets[i];
			if (panel.IsVisible(uIWidget))
			{
				int arg = int.Parse(uIWidget.name);
				Transform transform;
				bool arg2;
				if (uIWidget.cachedTransform.childCount > 0)
				{
					transform = uIWidget.cachedTransform.GetChild(0);
					if (showItems.Contains(transform))
					{
						continue;
					}
					arg2 = true;
					hideItems.Remove(transform);
				}
				else
				{
					int count2 = hideItems.Count;
					if (count2 > 0)
					{
						count2--;
						transform = hideItems[count2];
						hideItems.RemoveAt(count2);
						transform.SetParent(uIWidget.cachedTransform, worldPositionStays: false);
						arg2 = true;
					}
					else
					{
						transform = ((createItemFunc != null) ? createItemFunc(arg, uIWidget.cachedTransform) : ((!(itemPrefab != null)) ? null : ResourceUtility.Realizes(itemPrefab, uIWidget.cachedTransform, 5)));
						if (transform != null)
						{
							UIPanel componentInChildren = transform.GetComponentInChildren<UIPanel>();
							if (componentInChildren != null)
							{
								componentInChildren.depth = panel.depth + 1;
							}
						}
						else
						{
							GameObject obj = new GameObject("item")
							{
								layer = 5
							};
							transform = obj.transform;
							transform.SetParent(uIWidget.cachedTransform, worldPositionStays: false);
							obj.AddComponent<UIDragScrollView>().scrollView = scrollView;
						}
						if (needCenterOnClickChild)
						{
							UIUtility.AddCenterOnClickChild(transform);
						}
						arg2 = false;
					}
				}
				showItems.Add(transform);
				transform.gameObject.SetActive(value: true);
				initItemFunc(arg, transform, arg2);
				UIUtility.UpdateAnchors(transform);
			}
			else
			{
				if (uIWidget.cachedTransform.childCount <= 0)
				{
					continue;
				}
				Transform transform2 = uIWidget.cachedTransform.GetChild(0);
				if (!showItems.Contains(transform2))
				{
					continue;
				}
				showItems.Remove(transform2);
				hideItems.Add(transform2);
				if (UICamera.selectedObject != null)
				{
					Transform transform3 = UICamera.selectedObject.transform;
					while (transform3 != null && cachedTransform != transform3)
					{
						if (transform2 == transform3)
						{
							transform2 = null;
							break;
						}
						transform3 = transform3.parent;
					}
				}
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(value: false);
				}
			}
		}
	}
}

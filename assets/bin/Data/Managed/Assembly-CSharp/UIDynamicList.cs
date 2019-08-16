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

	public UIDynamicList()
		: this()
	{
	}

	public static UIDynamicList Set(UIScrollView scroll_view, int item_num, GameObject item_prefab, bool need_center_on_clickchild, Func<int, Transform, Transform> create_item_func, Action<int, Transform, bool> init_item_func)
	{
		if (scroll_view == null || scroll_view.panel == null)
		{
			return null;
		}
		UIDynamicList uIDynamicList = scroll_view.GetComponent<UIDynamicList>();
		if (uIDynamicList == null)
		{
			uIDynamicList = scroll_view.get_gameObject().AddComponent<UIDynamicList>();
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
		if (w.cachedTransform.get_childCount() > 0)
		{
			Transform child = w.cachedTransform.GetChild(0);
			child.get_gameObject().SetActive(false);
			hideItems.Add(child);
		}
	}

	private void Update()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (scrollView.get_transform().get_localPosition() != scrollViewPos)
		{
			UpdateItems();
		}
	}

	public void UpdateItems()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		scrollViewPos = scrollView.get_transform().get_localPosition();
		UIPanel panel = scrollView.panel;
		Transform cachedTransform = panel.cachedTransform;
		int i = 0;
		for (int count = itemWidgets.Count; i < count; i++)
		{
			UIWidget uIWidget = itemWidgets[i];
			if (panel.IsVisible(uIWidget))
			{
				int arg = int.Parse(uIWidget.get_name());
				Transform val;
				bool arg2;
				if (uIWidget.cachedTransform.get_childCount() > 0)
				{
					val = uIWidget.cachedTransform.GetChild(0);
					if (showItems.Contains(val))
					{
						continue;
					}
					arg2 = true;
					hideItems.Remove(val);
				}
				else
				{
					int count2 = hideItems.Count;
					if (count2 > 0)
					{
						count2--;
						val = hideItems[count2];
						hideItems.RemoveAt(count2);
						val.SetParent(uIWidget.cachedTransform, false);
						arg2 = true;
					}
					else
					{
						val = ((createItemFunc != null) ? createItemFunc(arg, uIWidget.cachedTransform) : ((!(itemPrefab != null)) ? null : ResourceUtility.Realizes(itemPrefab, uIWidget.cachedTransform, 5)));
						if (val != null)
						{
							UIPanel componentInChildren = val.GetComponentInChildren<UIPanel>();
							if (componentInChildren != null)
							{
								componentInChildren.depth = panel.depth + 1;
							}
						}
						else
						{
							GameObject val2 = new GameObject("item");
							val2.set_layer(5);
							val = val2.get_transform();
							val.SetParent(uIWidget.cachedTransform, false);
							val2.AddComponent<UIDragScrollView>().scrollView = scrollView;
						}
						if (needCenterOnClickChild)
						{
							UIUtility.AddCenterOnClickChild(val);
						}
						arg2 = false;
					}
				}
				showItems.Add(val);
				val.get_gameObject().SetActive(true);
				initItemFunc(arg, val, arg2);
				UIUtility.UpdateAnchors(val);
			}
			else
			{
				if (uIWidget.cachedTransform.get_childCount() <= 0)
				{
					continue;
				}
				Transform val3 = uIWidget.cachedTransform.GetChild(0);
				if (!showItems.Contains(val3))
				{
					continue;
				}
				showItems.Remove(val3);
				hideItems.Add(val3);
				if (UICamera.selectedObject != null)
				{
					Transform val4 = UICamera.selectedObject.get_transform();
					while (val4 != null && cachedTransform != val4)
					{
						if (val3 == val4)
						{
							val3 = null;
							break;
						}
						val4 = val4.get_parent();
					}
				}
				if (val3 != null)
				{
					val3.get_gameObject().SetActive(false);
				}
			}
		}
	}
}

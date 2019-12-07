using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Wrap Content Filter")]
public class UIWrapContentFilter : MonoBehaviour
{
	public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);

	public Func<int, string, bool> FilterItemFunc;

	private string _filter = string.Empty;

	public int itemSize = 100;

	public bool cullContent = true;

	public int minIndex;

	public int maxIndex;

	public OnInitializeItem onInitializeItem;

	private Transform mTrans;

	private UIPanel mPanel;

	private UIScrollView mScroll;

	private bool mHorizontal;

	private bool mFirstTime = true;

	private List<Transform> mChildren = new List<Transform>();

	public string filter
	{
		get
		{
			return _filter;
		}
		set
		{
			if (!(_filter == value))
			{
				_filter = value;
				FilterList(_filter);
				WrapContent();
			}
		}
	}

	protected virtual void Start()
	{
		SortBasedOnScrollMovement();
		WrapContent();
		if (mScroll != null)
		{
			mScroll.GetComponent<UIPanel>().onClipMove = OnMove;
		}
		mFirstTime = false;
	}

	protected virtual void OnMove(UIPanel panel)
	{
		WrapContent();
	}

	public virtual void Initialize(Func<int, string, bool> filter_item_func = null)
	{
		FilterItemFunc = filter_item_func;
		SortAlphabetically();
	}

	[ContextMenu("Sort Based on Scroll Movement")]
	public void SortBasedOnScrollMovement()
	{
		if (CacheScrollView())
		{
			mChildren.Clear();
			for (int i = 0; i < mTrans.childCount; i++)
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			if (mHorizontal)
			{
				mChildren.Sort(UIGrid.SortHorizontal);
			}
			else
			{
				mChildren.Sort(UIGrid.SortVertical);
			}
			ResetChildPositions();
		}
	}

	[ContextMenu("Sort Alphabetically")]
	public void SortAlphabetically()
	{
		if (CacheScrollView())
		{
			if (!mScroll.enabled)
			{
				mScroll.enabled = true;
			}
			mChildren.Clear();
			for (int i = 0; i < mTrans.childCount; i++)
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			mChildren.Sort(UIGrid.SortByName);
			ResetChildPositions();
		}
	}

	public void FilterList(string filterName = null)
	{
		mChildren.Clear();
		for (int i = 0; i < mTrans.childCount; i++)
		{
			if (string.IsNullOrEmpty(filterName))
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			else if (FilterItemFunc != null && FilterItemFunc(i, filterName))
			{
				Transform child = mTrans.GetChild(i);
				child.gameObject.SetActive(value: true);
				mChildren.Add(child);
			}
			else
			{
				mTrans.GetChild(i).gameObject.SetActive(value: false);
			}
		}
		mChildren.Sort(UIGrid.SortByName);
		ResetChildPositions();
		mScroll.ResetPosition();
	}

	protected bool CacheScrollView()
	{
		mTrans = base.transform;
		mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		mScroll = mPanel.GetComponent<UIScrollView>();
		if (mScroll == null)
		{
			return false;
		}
		if (mScroll.movement == UIScrollView.Movement.Horizontal)
		{
			mHorizontal = true;
		}
		else
		{
			if (mScroll.movement != UIScrollView.Movement.Vertical)
			{
				return false;
			}
			mHorizontal = false;
		}
		return true;
	}

	private void ResetChildPositions()
	{
		int i = 0;
		for (int count = mChildren.Count; i < count; i++)
		{
			Transform transform = mChildren[i];
			transform.localPosition = (mHorizontal ? new Vector3(i * itemSize, 0f, 0f) : new Vector3(0f, -i * itemSize, 0f));
			UpdateItem(transform, i);
		}
	}

	public void WrapContent()
	{
		float num = (float)(itemSize * mChildren.Count) * 0.5f;
		Vector3[] worldCorners = mPanel.worldCorners;
		for (int i = 0; i < 4; i++)
		{
			Vector3 position = worldCorners[i];
			position = (worldCorners[i] = mTrans.InverseTransformPoint(position));
		}
		Vector3 vector = Vector3.Lerp(worldCorners[0], worldCorners[2], 0.5f);
		if (mHorizontal)
		{
			float num2 = worldCorners[0].x - (float)itemSize;
			float num3 = worldCorners[2].x + (float)itemSize;
			int j = 0;
			for (int count = mChildren.Count; j < count; j++)
			{
				Transform transform = mChildren[j];
				float num4 = transform.localPosition.x - vector.x;
				if (mFirstTime)
				{
					UpdateItem(transform, j);
				}
				if (cullContent)
				{
					num4 += mPanel.clipOffset.x - mTrans.localPosition.x;
					if (!UICamera.IsPressed(transform.gameObject))
					{
						NGUITools.SetActive(transform.gameObject, num4 > num2 && num4 < num3, compatibilityMode: false);
					}
				}
			}
			return;
		}
		float num5 = worldCorners[0].y - (float)itemSize;
		float num6 = worldCorners[2].y + (float)itemSize;
		int k = 0;
		for (int count2 = mChildren.Count; k < count2; k++)
		{
			Transform transform2 = mChildren[k];
			float num7 = transform2.localPosition.y - vector.y;
			if (mFirstTime)
			{
				UpdateItem(transform2, k);
			}
			if (cullContent)
			{
				num7 += mPanel.clipOffset.y - mTrans.localPosition.y;
				if (!UICamera.IsPressed(transform2.gameObject))
				{
					NGUITools.SetActive(transform2.gameObject, num7 > num5 && num7 < num6, compatibilityMode: false);
				}
			}
		}
	}

	private void OnValidate()
	{
		if (maxIndex < minIndex)
		{
			maxIndex = minIndex;
		}
		if (minIndex > maxIndex)
		{
			maxIndex = minIndex;
		}
	}

	protected virtual void UpdateItem(Transform item, int index)
	{
		if (onInitializeItem != null)
		{
			int realIndex = (mScroll.movement == UIScrollView.Movement.Vertical) ? Mathf.RoundToInt(item.localPosition.y / (float)itemSize) : Mathf.RoundToInt(item.localPosition.x / (float)itemSize);
			onInitializeItem(item.gameObject, index, realIndex);
		}
	}
}

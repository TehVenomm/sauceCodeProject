using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Wrap Content Filter")]
public class UIWrapContentFilter
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

	public UIWrapContentFilter()
		: this()
	{
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (CacheScrollView())
		{
			mChildren.Clear();
			for (int i = 0; i < mTrans.get_childCount(); i++)
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		if (CacheScrollView())
		{
			if (!mScroll.get_enabled())
			{
				mScroll.set_enabled(true);
			}
			mChildren.Clear();
			for (int i = 0; i < mTrans.get_childCount(); i++)
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			mChildren.Sort(UIGrid.SortByName);
			ResetChildPositions();
		}
	}

	public void FilterList(string filterName = null)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		mChildren.Clear();
		for (int i = 0; i < mTrans.get_childCount(); i++)
		{
			if (string.IsNullOrEmpty(filterName))
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			else if (FilterItemFunc != null && FilterItemFunc.Invoke(i, filterName))
			{
				Transform val = mTrans.GetChild(i);
				val.get_gameObject().SetActive(true);
				mChildren.Add(val);
			}
			else
			{
				mTrans.GetChild(i).get_gameObject().SetActive(false);
			}
		}
		mChildren.Sort(UIGrid.SortByName);
		ResetChildPositions();
		mScroll.ResetPosition();
	}

	protected bool CacheScrollView()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		mTrans = this.get_transform();
		mPanel = NGUITools.FindInParents<UIPanel>(this.get_gameObject());
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int count = mChildren.Count; i < count; i++)
		{
			Transform val = mChildren[i];
			val.set_localPosition((!mHorizontal) ? new Vector3(0f, (float)(-i * itemSize), 0f) : new Vector3((float)(i * itemSize), 0f, 0f));
			UpdateItem(val, i);
		}
	}

	public void WrapContent()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		float num = (float)(itemSize * mChildren.Count) * 0.5f;
		Vector3[] worldCorners = mPanel.worldCorners;
		for (int i = 0; i < 4; i++)
		{
			Vector3 val = worldCorners[i];
			val = mTrans.InverseTransformPoint(val);
			worldCorners[i] = val;
		}
		Vector3 val2 = Vector3.Lerp(worldCorners[0], worldCorners[2], 0.5f);
		bool flag = true;
		float num2 = num * 2f;
		if (mHorizontal)
		{
			float num3 = worldCorners[0].x - (float)itemSize;
			float num4 = worldCorners[2].x + (float)itemSize;
			int j = 0;
			for (int count = mChildren.Count; j < count; j++)
			{
				Transform val3 = mChildren[j];
				Vector3 localPosition = val3.get_localPosition();
				float num5 = localPosition.x - val2.x;
				if (mFirstTime)
				{
					UpdateItem(val3, j);
				}
				if (cullContent)
				{
					float num6 = num5;
					Vector2 clipOffset = mPanel.clipOffset;
					float x = clipOffset.x;
					Vector3 localPosition2 = mTrans.get_localPosition();
					num5 = num6 + (x - localPosition2.x);
					if (!UICamera.IsPressed(val3.get_gameObject()))
					{
						NGUITools.SetActive(val3.get_gameObject(), num5 > num3 && num5 < num4, false);
					}
				}
			}
		}
		else
		{
			float num7 = worldCorners[0].y - (float)itemSize;
			float num8 = worldCorners[2].y + (float)itemSize;
			int k = 0;
			for (int count2 = mChildren.Count; k < count2; k++)
			{
				Transform val4 = mChildren[k];
				Vector3 localPosition3 = val4.get_localPosition();
				float num9 = localPosition3.y - val2.y;
				if (mFirstTime)
				{
					UpdateItem(val4, k);
				}
				if (cullContent)
				{
					float num10 = num9;
					Vector2 clipOffset2 = mPanel.clipOffset;
					float y = clipOffset2.y;
					Vector3 localPosition4 = mTrans.get_localPosition();
					num9 = num10 + (y - localPosition4.y);
					if (!UICamera.IsPressed(val4.get_gameObject()))
					{
						NGUITools.SetActive(val4.get_gameObject(), num9 > num7 && num9 < num8, false);
					}
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
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		if (onInitializeItem != null)
		{
			int num;
			if (mScroll.movement == UIScrollView.Movement.Vertical)
			{
				Vector3 localPosition = item.get_localPosition();
				num = Mathf.RoundToInt(localPosition.y / (float)itemSize);
			}
			else
			{
				Vector3 localPosition2 = item.get_localPosition();
				num = Mathf.RoundToInt(localPosition2.x / (float)itemSize);
			}
			int realIndex = num;
			onInitializeItem(item.get_gameObject(), index, realIndex);
		}
	}
}

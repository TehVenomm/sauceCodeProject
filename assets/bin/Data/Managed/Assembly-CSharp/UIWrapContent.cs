using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Wrap Content")]
public class UIWrapContent
{
	public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);

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

	public UIWrapContent()
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (CacheScrollView())
		{
			mChildren.Clear();
			for (int i = 0; i < mTrans.get_childCount(); i++)
			{
				mChildren.Add(mTrans.GetChild(i));
			}
			mChildren.Sort(UIGrid.SortByName);
			ResetChildPositions();
		}
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
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Expected O, but got Unknown
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
				if (num5 < 0f - num)
				{
					Vector3 localPosition2 = val3.get_localPosition();
					localPosition2.x += num2;
					num5 = localPosition2.x - val2.x;
					int num6 = Mathf.RoundToInt(localPosition2.x / (float)itemSize);
					if (minIndex == maxIndex || (minIndex <= num6 && num6 <= maxIndex))
					{
						val3.set_localPosition(localPosition2);
						UpdateItem(val3, j);
					}
					else
					{
						flag = false;
					}
				}
				else if (num5 > num)
				{
					Vector3 localPosition3 = val3.get_localPosition();
					localPosition3.x -= num2;
					num5 = localPosition3.x - val2.x;
					int num7 = Mathf.RoundToInt(localPosition3.x / (float)itemSize);
					if (minIndex == maxIndex || (minIndex <= num7 && num7 <= maxIndex))
					{
						val3.set_localPosition(localPosition3);
						UpdateItem(val3, j);
					}
					else
					{
						flag = false;
					}
				}
				else if (mFirstTime)
				{
					UpdateItem(val3, j);
				}
				if (cullContent)
				{
					float num8 = num5;
					Vector2 clipOffset = mPanel.clipOffset;
					float x = clipOffset.x;
					Vector3 localPosition4 = mTrans.get_localPosition();
					num5 = num8 + (x - localPosition4.x);
					if (!UICamera.IsPressed(val3.get_gameObject()))
					{
						NGUITools.SetActive(val3.get_gameObject(), num5 > num3 && num5 < num4, false);
					}
				}
			}
		}
		else
		{
			float num9 = worldCorners[0].y - (float)itemSize;
			float num10 = worldCorners[2].y + (float)itemSize;
			int k = 0;
			for (int count2 = mChildren.Count; k < count2; k++)
			{
				Transform val4 = mChildren[k];
				Vector3 localPosition5 = val4.get_localPosition();
				float num11 = localPosition5.y - val2.y;
				if (num11 < 0f - num)
				{
					Vector3 localPosition6 = val4.get_localPosition();
					localPosition6.y += num2;
					num11 = localPosition6.y - val2.y;
					int num12 = Mathf.RoundToInt(localPosition6.y / (float)itemSize);
					if (minIndex == maxIndex || (minIndex <= num12 && num12 <= maxIndex))
					{
						val4.set_localPosition(localPosition6);
						UpdateItem(val4, k);
					}
					else
					{
						flag = false;
					}
				}
				else if (num11 > num)
				{
					Vector3 localPosition7 = val4.get_localPosition();
					localPosition7.y -= num2;
					num11 = localPosition7.y - val2.y;
					int num13 = Mathf.RoundToInt(localPosition7.y / (float)itemSize);
					if (minIndex == maxIndex || (minIndex <= num13 && num13 <= maxIndex))
					{
						val4.set_localPosition(localPosition7);
						UpdateItem(val4, k);
					}
					else
					{
						flag = false;
					}
				}
				else if (mFirstTime)
				{
					UpdateItem(val4, k);
				}
				if (cullContent)
				{
					float num14 = num11;
					Vector2 clipOffset2 = mPanel.clipOffset;
					float y = clipOffset2.y;
					Vector3 localPosition8 = mTrans.get_localPosition();
					num11 = num14 + (y - localPosition8.y);
					if (!UICamera.IsPressed(val4.get_gameObject()))
					{
						NGUITools.SetActive(val4.get_gameObject(), num11 > num9 && num11 < num10, false);
					}
				}
			}
		}
		mScroll.restrictWithinPanel = !flag;
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

using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	public enum Direction
	{
		Down,
		Up
	}

	public enum Sorting
	{
		None,
		Alphabetic,
		Horizontal,
		Vertical,
		Custom
	}

	public delegate void OnReposition();

	public int columns;

	public Direction direction;

	public Sorting sorting;

	public UIWidget.Pivot pivot;

	public UIWidget.Pivot cellAlignment;

	public bool hideInactive = true;

	public bool keepWithinPanel;

	public Vector2 padding = Vector2.get_zero();

	public OnReposition onReposition;

	public Comparison<Transform> onCustomSort;

	protected UIPanel mPanel;

	protected bool mInitDone;

	protected bool mReposition;

	public bool repositionNow
	{
		set
		{
			if (value)
			{
				mReposition = true;
				this.set_enabled(true);
			}
		}
	}

	public List<Transform> GetChildList()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		Transform val = this.get_transform();
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < val.get_childCount(); i++)
		{
			Transform val2 = val.GetChild(i);
			if (!hideInactive || (Object.op_Implicit(val2) && NGUITools.GetActive(val2.get_gameObject())))
			{
				list.Add(val2);
			}
		}
		if (sorting != 0)
		{
			if (sorting == Sorting.Alphabetic)
			{
				list.Sort(UIGrid.SortByName);
			}
			else if (sorting == Sorting.Horizontal)
			{
				list.Sort(UIGrid.SortHorizontal);
			}
			else if (sorting == Sorting.Vertical)
			{
				list.Sort(UIGrid.SortVertical);
			}
			else if (onCustomSort != null)
			{
				list.Sort(onCustomSort);
			}
			else
			{
				Sort(list);
			}
		}
		return list;
	}

	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(UIGrid.SortByName);
	}

	protected virtual void Start()
	{
		Init();
		Reposition();
		this.set_enabled(false);
	}

	protected virtual void Init()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		mInitDone = true;
		mPanel = NGUITools.FindInParents<UIPanel>(this.get_gameObject());
	}

	protected virtual void LateUpdate()
	{
		if (mReposition)
		{
			Reposition();
		}
		this.set_enabled(false);
	}

	private void OnValidate()
	{
		if (!Application.get_isPlaying() && NGUITools.GetActive(this))
		{
			Reposition();
		}
	}

	protected void RepositionVariableSize(List<Transform> children)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Expected O, but got Unknown
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Expected O, but got Unknown
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Expected O, but got Unknown
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		float num2 = 0f;
		int num3 = (columns <= 0) ? 1 : (children.Count / columns + 1);
		int num4 = (columns <= 0) ? children.Count : columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = (Bounds[])new Bounds[num4];
		Bounds[] array3 = (Bounds[])new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		for (int count = children.Count; i < count; i++)
		{
			Transform val = children[i];
			Bounds val2 = NGUIMath.CalculateRelativeWidgetBounds(val, !hideInactive);
			Vector3 localScale = val.get_localScale();
			val2.set_min(Vector3.Scale(val2.get_min(), localScale));
			val2.set_max(Vector3.Scale(val2.get_max(), localScale));
			array[num6, num5] = val2;
			array2[num5].Encapsulate(val2);
			array3[num6].Encapsulate(val2);
			if (++num5 >= columns && columns > 0)
			{
				num5 = 0;
				num6++;
			}
		}
		num5 = 0;
		num6 = 0;
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(cellAlignment);
		int j = 0;
		for (int count2 = children.Count; j < count2; j++)
		{
			Transform val3 = children[j];
			Bounds val4 = array[num6, num5];
			Bounds val5 = array2[num5];
			Bounds val6 = array3[num6];
			Vector3 localPosition = val3.get_localPosition();
			float num7 = num;
			Vector3 extents = val4.get_extents();
			float num8 = num7 + extents.x;
			Vector3 center = val4.get_center();
			localPosition.x = num8 - center.x;
			float x = localPosition.x;
			Vector3 max = val4.get_max();
			float x2 = max.x;
			Vector3 min = val4.get_min();
			float num9 = x2 - min.x;
			Vector3 max2 = val5.get_max();
			float num10 = num9 - max2.x;
			Vector3 min2 = val5.get_min();
			localPosition.x = x - (Mathf.Lerp(0f, num10 + min2.x, pivotOffset.x) - padding.x);
			if (direction == Direction.Down)
			{
				float num11 = 0f - num2;
				Vector3 extents2 = val4.get_extents();
				float num12 = num11 - extents2.y;
				Vector3 center2 = val4.get_center();
				localPosition.y = num12 - center2.y;
				float y = localPosition.y;
				Vector3 max3 = val4.get_max();
				float y2 = max3.y;
				Vector3 min3 = val4.get_min();
				float num13 = y2 - min3.y;
				Vector3 max4 = val6.get_max();
				float num14 = num13 - max4.y;
				Vector3 min4 = val6.get_min();
				localPosition.y = y + (Mathf.Lerp(num14 + min4.y, 0f, pivotOffset.y) - padding.y);
			}
			else
			{
				float num15 = num2;
				Vector3 extents3 = val4.get_extents();
				float num16 = num15 + extents3.y;
				Vector3 center3 = val4.get_center();
				localPosition.y = num16 - center3.y;
				float y3 = localPosition.y;
				Vector3 max5 = val4.get_max();
				float y4 = max5.y;
				Vector3 min5 = val4.get_min();
				float num17 = y4 - min5.y;
				Vector3 max6 = val6.get_max();
				float num18 = num17 - max6.y;
				Vector3 min6 = val6.get_min();
				localPosition.y = y3 - (Mathf.Lerp(0f, num18 + min6.y, pivotOffset.y) - padding.y);
			}
			float num19 = num;
			Vector3 size = val5.get_size();
			num = num19 + (size.x + padding.x * 2f);
			val3.set_localPosition(localPosition);
			if (++num5 >= columns && columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				float num20 = num2;
				Vector3 size2 = val6.get_size();
				num2 = num20 + (size2.y + padding.y * 2f);
			}
		}
		if (pivot != 0)
		{
			pivotOffset = NGUIMath.GetPivotOffset(pivot);
			Bounds val7 = NGUIMath.CalculateRelativeWidgetBounds(this.get_transform());
			Vector3 size3 = val7.get_size();
			float num21 = Mathf.Lerp(0f, size3.x, pivotOffset.x);
			Vector3 size4 = val7.get_size();
			float num22 = Mathf.Lerp(0f - size4.y, 0f, pivotOffset.y);
			Transform val8 = this.get_transform();
			for (int k = 0; k < val8.get_childCount(); k++)
			{
				Transform val9 = val8.GetChild(k);
				SpringPosition component = val9.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.target.x -= num21;
					component.target.y -= num22;
				}
				else
				{
					Vector3 localPosition2 = val9.get_localPosition();
					localPosition2.x -= num21;
					localPosition2.y -= num22;
					val9.set_localPosition(localPosition2);
				}
			}
		}
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (Application.get_isPlaying() && !mInitDone && NGUITools.GetActive(this))
		{
			Init();
		}
		mReposition = false;
		Transform target = this.get_transform();
		List<Transform> childList = GetChildList();
		if (childList.Count > 0)
		{
			RepositionVariableSize(childList);
		}
		if (keepWithinPanel && mPanel != null)
		{
			mPanel.ConstrainTargetToBounds(target, true);
			UIScrollView component = mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (onReposition != null)
		{
			onReposition();
		}
	}
}

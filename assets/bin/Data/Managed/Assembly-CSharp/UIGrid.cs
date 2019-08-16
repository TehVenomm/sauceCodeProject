using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	public delegate void OnReposition();

	public enum Arrangement
	{
		Horizontal,
		Vertical,
		CellSnap
	}

	public enum Sorting
	{
		None,
		Alphabetic,
		Horizontal,
		Vertical,
		Custom
	}

	public Arrangement arrangement;

	public Sorting sorting;

	public UIWidget.Pivot pivot;

	public int maxPerLine;

	public float cellWidth = 200f;

	public float cellHeight = 200f;

	public bool animateSmoothly;

	public bool hideInactive;

	public bool keepWithinPanel;

	public OnReposition onReposition;

	public Comparison<Transform> onCustomSort;

	[HideInInspector]
	[SerializeField]
	private bool sorted;

	protected bool mReposition;

	protected UIPanel mPanel;

	protected bool mInitDone;

	[CompilerGenerated]
	private static Comparison<Transform> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static Comparison<Transform> _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static Comparison<Transform> _003C_003Ef__mg_0024cache2;

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
		Transform transform = this.get_transform();
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			Transform child = transform.GetChild(i);
			if (!hideInactive || (Object.op_Implicit(child) && NGUITools.GetActive(child.get_gameObject())))
			{
				list.Add(child);
			}
		}
		if (sorting != 0 && arrangement != Arrangement.CellSnap)
		{
			if (sorting == Sorting.Alphabetic)
			{
				list.Sort(SortByName);
			}
			else if (sorting == Sorting.Horizontal)
			{
				list.Sort(SortHorizontal);
			}
			else if (sorting == Sorting.Vertical)
			{
				list.Sort(SortVertical);
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

	public Transform GetChild(int index)
	{
		List<Transform> childList = GetChildList();
		return (index >= childList.Count) ? null : childList[index];
	}

	public int GetIndex(Transform trans)
	{
		return GetChildList().IndexOf(trans);
	}

	public void AddChild(Transform trans)
	{
		AddChild(trans, sort: true);
	}

	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			trans.set_parent(this.get_transform());
			ResetPosition(GetChildList());
		}
	}

	public bool RemoveChild(Transform t)
	{
		List<Transform> childList = GetChildList();
		if (childList.Remove(t))
		{
			ResetPosition(childList);
			return true;
		}
		return false;
	}

	protected virtual void Init()
	{
		mInitDone = true;
		mPanel = NGUITools.FindInParents<UIPanel>(this.get_gameObject());
	}

	protected virtual void Start()
	{
		if (!mInitDone)
		{
			Init();
		}
		bool flag = animateSmoothly;
		animateSmoothly = false;
		Reposition();
		animateSmoothly = flag;
		this.set_enabled(false);
	}

	protected virtual void Update()
	{
		Reposition();
		this.set_enabled(false);
	}

	private void OnValidate()
	{
		if (!Application.get_isPlaying() && NGUITools.GetActive(this))
		{
			Reposition();
		}
	}

	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.get_name(), b.get_name());
	}

	public static int SortHorizontal(Transform a, Transform b)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = a.get_localPosition();
		ref float x = ref localPosition.x;
		Vector3 localPosition2 = b.get_localPosition();
		return x.CompareTo(localPosition2.x);
	}

	public static int SortVertical(Transform a, Transform b)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = b.get_localPosition();
		ref float y = ref localPosition.y;
		Vector3 localPosition2 = a.get_localPosition();
		return y.CompareTo(localPosition2.y);
	}

	protected virtual void Sort(List<Transform> list)
	{
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.get_isPlaying() && !mInitDone && NGUITools.GetActive(this.get_gameObject()))
		{
			Init();
		}
		if (sorted)
		{
			sorted = false;
			if (sorting == Sorting.None)
			{
				sorting = Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		List<Transform> childList = GetChildList();
		ResetPosition(childList);
		if (keepWithinPanel)
		{
			ConstrainWithinPanel();
		}
		if (onReposition != null)
		{
			onReposition();
		}
	}

	public void ConstrainWithinPanel()
	{
		if (mPanel != null)
		{
			mPanel.ConstrainTargetToBounds(this.get_transform(), immediate: true);
			UIScrollView component = mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(recalculateBounds: true);
			}
		}
	}

	protected virtual void ResetPosition(List<Transform> list)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		mReposition = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = this.get_transform();
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			Transform val = list[i];
			Vector3 val2 = val.get_localPosition();
			float z = val2.z;
			if (arrangement == Arrangement.CellSnap)
			{
				if (cellWidth > 0f)
				{
					val2.x = Mathf.Round(val2.x / cellWidth) * cellWidth;
				}
				if (cellHeight > 0f)
				{
					val2.y = Mathf.Round(val2.y / cellHeight) * cellHeight;
				}
			}
			else
			{
				val2 = ((arrangement != 0) ? new Vector3(cellWidth * (float)num2, (0f - cellHeight) * (float)num, z) : new Vector3(cellWidth * (float)num, (0f - cellHeight) * (float)num2, z));
			}
			if (animateSmoothly && Application.get_isPlaying())
			{
				SpringPosition springPosition = SpringPosition.Begin(val.get_gameObject(), val2, 15f);
				springPosition.updateScrollView = true;
				springPosition.ignoreTimeScale = true;
			}
			else
			{
				val.set_localPosition(val2);
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= maxPerLine && maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
		}
		if (pivot == UIWidget.Pivot.TopLeft)
		{
			return;
		}
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(pivot);
		float num5;
		float num6;
		if (arrangement == Arrangement.Horizontal)
		{
			num5 = Mathf.Lerp(0f, (float)num3 * cellWidth, pivotOffset.x);
			num6 = Mathf.Lerp((float)(-num4) * cellHeight, 0f, pivotOffset.y);
		}
		else
		{
			num5 = Mathf.Lerp(0f, (float)num4 * cellWidth, pivotOffset.x);
			num6 = Mathf.Lerp((float)(-num3) * cellHeight, 0f, pivotOffset.y);
		}
		for (int j = 0; j < transform.get_childCount(); j++)
		{
			Transform child = transform.GetChild(j);
			SpringPosition component = child.GetComponent<SpringPosition>();
			if (component != null)
			{
				ref Vector3 target = ref component.target;
				target.x -= num5;
				ref Vector3 target2 = ref component.target;
				target2.y -= num6;
			}
			else
			{
				Vector3 localPosition = child.get_localPosition();
				localPosition.x -= num5;
				localPosition.y -= num6;
				child.set_localPosition(localPosition);
			}
		}
	}
}

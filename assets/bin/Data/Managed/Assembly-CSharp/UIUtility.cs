using System;
using System.Text;
using UnityEngine;

public static class UIUtility
{
	public static void GetGridSize(UIGrid grid, int item_num, out int grid_w, out int grid_h)
	{
		if (item_num == 0)
		{
			grid_w = 0;
			grid_h = 0;
		}
		else if (grid.maxPerLine == 0 || item_num < grid.maxPerLine)
		{
			grid_w = item_num;
			grid_h = 1;
		}
		else
		{
			grid_w = grid.maxPerLine;
			grid_h = (item_num + grid_w - 1) / grid_w;
		}
		if (grid.arrangement == UIGrid.Arrangement.Vertical)
		{
			int num = grid_w;
			grid_w = grid_h;
			grid_h = num;
		}
	}

	public static Vector2 GetGridItemsBoundSize(UIGrid grid, int item_num)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		GetGridSize(grid, item_num, out int grid_w, out int grid_h);
		int num = (int)grid.cellWidth;
		int num2 = (int)grid.cellHeight;
		return new Vector2((float)(num * grid_w), (float)(num2 * grid_h));
	}

	public static void SetGridItemsDraggableWidget(UIScrollView scroll_view, UIGrid grid, int item_num)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		Transform val = scroll_view.get_transform().FindChild("_DRAG_SCROLL_");
		UIWidget uIWidget;
		BoxCollider val2;
		if (val == null)
		{
			uIWidget = NGUITools.AddChild<UIWidget>(scroll_view.get_gameObject());
			uIWidget.get_gameObject().set_name("_DRAG_SCROLL_");
			uIWidget.get_gameObject().AddComponent<UIDragScrollView>();
			uIWidget.depth = -1;
			uIWidget.pivot = UIWidget.Pivot.TopLeft;
			val2 = uIWidget.get_gameObject().AddComponent<BoxCollider>();
			val = uIWidget.get_transform();
		}
		else
		{
			val2 = val.GetComponent<BoxCollider>();
			uIWidget = val.GetComponent<UIWidget>();
		}
		Vector3 size = Vector2.op_Implicit(GetGridItemsBoundSize(grid, item_num));
		val2.set_size(size);
		val2.set_center(new Vector3(size.x * 0.5f, (0f - size.y) * 0.5f));
		Vector3 localPosition = grid.get_transform().get_localPosition() + new Vector3((0f - grid.cellWidth) * 0.5f, grid.cellHeight * 0.5f);
		if (grid.pivot != 0)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(grid.pivot);
			localPosition.x -= Mathf.Lerp(0f, size.x - grid.cellWidth, pivotOffset.x);
			localPosition.y -= Mathf.Lerp(0f - (size.y - grid.cellHeight), 0f, pivotOffset.y);
		}
		val.set_localPosition(localPosition);
		uIWidget.SetDimensions((int)size.x, (int)size.y);
	}

	public static void AddCenterOnClickChild(Transform t)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		UICenterOnClick componentInChildren = t.GetComponentInChildren<UICenterOnClick>();
		if (componentInChildren != null)
		{
			Transform val = componentInChildren.get_transform();
			int i = 0;
			for (int childCount = val.get_childCount(); i < childCount; i++)
			{
				val.GetChild(i).GetComponentsInChildren<BoxCollider>(true, Temporary.boxColliderList);
				int j = 0;
				for (int count = Temporary.boxColliderList.Count; j < count; j++)
				{
					if (Temporary.boxColliderList[j].GetComponent<UICenterOnClickChild>() == null)
					{
						Temporary.boxColliderList[j].get_gameObject().AddComponent<UICenterOnClickChild>();
					}
				}
				Temporary.boxColliderList.Clear();
			}
		}
	}

	public static void SetActiveAndAlphaFade(GameObject go, bool is_active)
	{
		bool activeSelf = go.get_activeSelf();
		go.SetActive(true);
		TweenAlpha component = go.GetComponent<TweenAlpha>();
		if (component == null)
		{
			go.SetActive(false);
		}
		else if (is_active)
		{
			if (!activeSelf)
			{
				component.value = 0f;
			}
			component.SetOnFinished((EventDelegate.Callback)null);
			component.PlayForward();
		}
		else if (activeSelf && component.value > 0f)
		{
			component.SetOnFinished(delegate
			{
				go.SetActive(false);
			});
			component.PlayReverse();
		}
		else
		{
			component.value = 0f;
			go.SetActive(false);
		}
	}

	public static float GetWorldTopY(UIWidget w)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		object cachedTransform = (object)w.cachedTransform;
		Vector2 pivotOffset = w.pivotOffset;
		Vector3 val = cachedTransform.TransformPoint(0f, (0f - pivotOffset.y) * (float)w.height + (float)w.height, 0f);
		return val.y;
	}

	public static void UpdateAnchors(Transform root)
	{
		root.GetComponentsInChildren<UIRect>(true, Temporary.uiRectList);
		int i = 0;
		for (int count = Temporary.uiRectList.Count; i < count; i++)
		{
			Temporary.uiRectList[i].UpdateAnchors();
		}
		Temporary.uiRectList.Clear();
	}

	public static string GetColorText(string text, Color color)
	{
		if (text.IsNullOrWhiteSpace())
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		stringBuilder.Append(((int)(color.r * 255f)).ToString("x2"));
		stringBuilder.Append(((int)(color.g * 255f)).ToString("x2"));
		stringBuilder.Append(((int)(color.b * 255f)).ToString("x2"));
		stringBuilder.Append("]");
		stringBuilder.Append(text);
		stringBuilder.Append("[-]");
		return stringBuilder.ToString();
	}

	public static string TimeFormatWithUnit(int restTime)
	{
		TimeSpan restTime2 = TimeSpan.FromSeconds((double)restTime);
		return TimeFormatWithUnit(restTime2);
	}

	public static string TimeFormatWithUnit(TimeSpan restTime)
	{
		if (restTime.Days > 0)
		{
			return StringTable.Format(STRING_CATEGORY.TIME, 0u, restTime.Days);
		}
		if (restTime.Hours > 0)
		{
			return StringTable.Format(STRING_CATEGORY.TIME, 1u, restTime.Hours);
		}
		return StringTable.Format(STRING_CATEGORY.TIME, 2u, restTime.Minutes);
	}

	public static string TimeFormat(int restTime, bool isHours = false)
	{
		TimeSpan restTime2 = TimeSpan.FromSeconds((double)restTime);
		return TimeFormat(restTime2, isHours);
	}

	public static string TimeFormat(TimeSpan restTime, bool isHours = false)
	{
		if (!isHours)
		{
			int num = (int)restTime.TotalMinutes;
			int num2 = restTime.Seconds;
			if (num >= 100)
			{
				num = 99;
				num2 = 59;
			}
			else if (num2 < 0)
			{
				num = 0;
				num2 = 0;
			}
			return $"{num:D2}:{num2:D2}";
		}
		int num3 = (int)restTime.TotalHours;
		int num4 = restTime.Minutes;
		int num5 = restTime.Seconds;
		if (num3 >= 100)
		{
			num3 = 99;
			num4 = 59;
			num5 = 59;
		}
		else if (num5 < 0)
		{
			num3 = 0;
			num4 = 0;
			num5 = 0;
		}
		return $"{num3}:{num4:D2}:{num5:D2}";
	}
}

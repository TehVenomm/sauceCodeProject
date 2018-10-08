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
		GetGridSize(grid, item_num, out int grid_w, out int grid_h);
		int num = (int)grid.cellWidth;
		int num2 = (int)grid.cellHeight;
		return new Vector2((float)(num * grid_w), (float)(num2 * grid_h));
	}

	public static void SetGridItemsDraggableWidget(UIScrollView scroll_view, UIGrid grid, int item_num)
	{
		Transform transform = scroll_view.transform.FindChild("_DRAG_SCROLL_");
		UIWidget uIWidget;
		BoxCollider boxCollider;
		if ((UnityEngine.Object)transform == (UnityEngine.Object)null)
		{
			uIWidget = NGUITools.AddChild<UIWidget>(scroll_view.gameObject);
			uIWidget.gameObject.name = "_DRAG_SCROLL_";
			uIWidget.gameObject.AddComponent<UIDragScrollView>();
			uIWidget.depth = -1;
			uIWidget.pivot = UIWidget.Pivot.TopLeft;
			boxCollider = uIWidget.gameObject.AddComponent<BoxCollider>();
			transform = uIWidget.transform;
		}
		else
		{
			boxCollider = transform.GetComponent<BoxCollider>();
			uIWidget = transform.GetComponent<UIWidget>();
		}
		Vector3 vector2 = boxCollider.size = GetGridItemsBoundSize(grid, item_num);
		boxCollider.center = new Vector3(vector2.x * 0.5f, (0f - vector2.y) * 0.5f);
		Vector3 localPosition = grid.transform.localPosition + new Vector3((0f - grid.cellWidth) * 0.5f, grid.cellHeight * 0.5f);
		if (grid.pivot != 0)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(grid.pivot);
			localPosition.x -= Mathf.Lerp(0f, vector2.x - grid.cellWidth, pivotOffset.x);
			localPosition.y -= Mathf.Lerp(0f - (vector2.y - grid.cellHeight), 0f, pivotOffset.y);
		}
		transform.localPosition = localPosition;
		uIWidget.SetDimensions((int)vector2.x, (int)vector2.y);
	}

	public static void AddCenterOnClickChild(Transform t)
	{
		UICenterOnClick componentInChildren = t.GetComponentInChildren<UICenterOnClick>();
		if ((UnityEngine.Object)componentInChildren != (UnityEngine.Object)null)
		{
			Transform transform = componentInChildren.transform;
			int i = 0;
			for (int childCount = transform.childCount; i < childCount; i++)
			{
				transform.GetChild(i).GetComponentsInChildren(true, Temporary.boxColliderList);
				int j = 0;
				for (int count = Temporary.boxColliderList.Count; j < count; j++)
				{
					if ((UnityEngine.Object)Temporary.boxColliderList[j].GetComponent<UICenterOnClickChild>() == (UnityEngine.Object)null)
					{
						Temporary.boxColliderList[j].gameObject.AddComponent<UICenterOnClickChild>();
					}
				}
				Temporary.boxColliderList.Clear();
			}
		}
	}

	public static void SetActiveAndAlphaFade(GameObject go, bool is_active)
	{
		bool activeSelf = go.activeSelf;
		go.SetActive(true);
		TweenAlpha component = go.GetComponent<TweenAlpha>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
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
		Transform cachedTransform = w.cachedTransform;
		Vector2 pivotOffset = w.pivotOffset;
		Vector3 vector = cachedTransform.TransformPoint(0f, (0f - pivotOffset.y) * (float)w.height + (float)w.height, 0f);
		return vector.y;
	}

	public static void UpdateAnchors(Transform root)
	{
		root.GetComponentsInChildren(true, Temporary.uiRectList);
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

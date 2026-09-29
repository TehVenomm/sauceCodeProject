// Decompiled with JetBrains decompiler
// Type: UIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

#nullable disable
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
    if (grid.arrangement != UIGrid.Arrangement.Vertical)
      return;
    int num = grid_w;
    grid_w = grid_h;
    grid_h = num;
  }

  public static Vector2 GetGridItemsBoundSize(UIGrid grid, int item_num)
  {
    int grid_w;
    int grid_h;
    UIUtility.GetGridSize(grid, item_num, out grid_w, out grid_h);
    int cellWidth = (int) grid.cellWidth;
    int cellHeight = (int) grid.cellHeight;
    int num = grid_w;
    return new Vector2((float) (cellWidth * num), (float) (cellHeight * grid_h));
  }

  public static void SetGridItemsDraggableWidget(
    UIScrollView scroll_view,
    UIGrid grid,
    int item_num)
  {
    Transform transform = ((Component) scroll_view).transform.Find("_DRAG_SCROLL_");
    UIWidget uiWidget;
    BoxCollider boxCollider;
    if (Object.op_Equality((Object) transform, (Object) null))
    {
      uiWidget = NGUITools.AddChild<UIWidget>(((Component) scroll_view).gameObject);
      ((Object) ((Component) uiWidget).gameObject).name = "_DRAG_SCROLL_";
      ((Component) uiWidget).gameObject.AddComponent<UIDragScrollView>();
      uiWidget.depth = -1;
      uiWidget.pivot = UIWidget.Pivot.TopLeft;
      boxCollider = ((Component) uiWidget).gameObject.AddComponent<BoxCollider>();
      transform = ((Component) uiWidget).transform;
    }
    else
    {
      boxCollider = ((Component) transform).GetComponent<BoxCollider>();
      uiWidget = ((Component) transform).GetComponent<UIWidget>();
    }
    Vector3 vector3_1 = Vector2.op_Implicit(UIUtility.GetGridItemsBoundSize(grid, item_num));
    boxCollider.size = vector3_1;
    boxCollider.center = new Vector3(vector3_1.x * 0.5f, (float) (-(double) vector3_1.y * 0.5));
    Vector3 vector3_2 = Vector3.op_Addition(((Component) grid).transform.localPosition, new Vector3((float) (-(double) grid.cellWidth * 0.5), grid.cellHeight * 0.5f));
    if (grid.pivot != UIWidget.Pivot.TopLeft)
    {
      Vector2 pivotOffset = NGUIMath.GetPivotOffset(grid.pivot);
      vector3_2.x -= Mathf.Lerp(0.0f, vector3_1.x - grid.cellWidth, pivotOffset.x);
      vector3_2.y -= Mathf.Lerp((float) -((double) vector3_1.y - (double) grid.cellHeight), 0.0f, pivotOffset.y);
    }
    transform.localPosition = vector3_2;
    uiWidget.SetDimensions((int) vector3_1.x, (int) vector3_1.y);
  }

  public static void AddCenterOnClickChild(Transform t)
  {
    UICenterOnClick componentInChildren = ((Component) t).GetComponentInChildren<UICenterOnClick>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    Transform transform = ((Component) componentInChildren).transform;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
    {
      ((Component) transform.GetChild(num)).GetComponentsInChildren<BoxCollider>(true, Temporary.boxColliderList);
      int index = 0;
      for (int count = Temporary.boxColliderList.Count; index < count; ++index)
      {
        if (Object.op_Equality((Object) ((Component) Temporary.boxColliderList[index]).GetComponent<UICenterOnClickChild>(), (Object) null))
          ((Component) Temporary.boxColliderList[index]).gameObject.AddComponent<UICenterOnClickChild>();
      }
      Temporary.boxColliderList.Clear();
    }
  }

  public static void SetActiveAndAlphaFade(GameObject go, bool is_active)
  {
    bool activeSelf = go.activeSelf;
    go.SetActive(true);
    TweenAlpha component = go.GetComponent<TweenAlpha>();
    if (Object.op_Equality((Object) component, (Object) null))
      go.SetActive(false);
    else if (is_active)
    {
      if (!activeSelf)
        component.value = 0.0f;
      component.SetOnFinished((EventDelegate.Callback) null);
      component.PlayForward();
    }
    else if (activeSelf && (double) component.value > 0.0)
    {
      component.SetOnFinished((EventDelegate.Callback) (() => go.SetActive(false)));
      component.PlayReverse();
    }
    else
    {
      component.value = 0.0f;
      go.SetActive(false);
    }
  }

  public static float GetWorldTopY(UIWidget w)
  {
    return w.cachedTransform.TransformPoint(0.0f, -w.pivotOffset.y * (float) w.height + (float) w.height, 0.0f).y;
  }

  public static void UpdateAnchors(Transform root)
  {
    ((Component) root).GetComponentsInChildren<UIRect>(true, Temporary.uiRectList);
    int index = 0;
    for (int count = Temporary.uiRectList.Count; index < count; ++index)
      Temporary.uiRectList[index].UpdateAnchors();
    Temporary.uiRectList.Clear();
  }

  public static string GetColorText(string text, Color color)
  {
    if (text.IsNullOrWhiteSpace())
      return text;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("[");
    stringBuilder.Append(((int) ((double) color.r * (double) byte.MaxValue)).ToString("x2"));
    stringBuilder.Append(((int) ((double) color.g * (double) byte.MaxValue)).ToString("x2"));
    stringBuilder.Append(((int) ((double) color.b * (double) byte.MaxValue)).ToString("x2"));
    stringBuilder.Append("]");
    stringBuilder.Append(text);
    stringBuilder.Append("[-]");
    return stringBuilder.ToString();
  }

  public static string TimeFormatWithUnit(int restTime)
  {
    return UIUtility.TimeFormatWithUnit(TimeSpan.FromSeconds((double) restTime));
  }

  public static string TimeFormatWithUnit(TimeSpan restTime)
  {
    return restTime.Days > 0 ? StringTable.Format(STRING_CATEGORY.TIME, 0U, (object) restTime.Days) : (restTime.Hours > 0 ? StringTable.Format(STRING_CATEGORY.TIME, 1U, (object) restTime.Hours) : StringTable.Format(STRING_CATEGORY.TIME, 2U, (object) restTime.Minutes));
  }

  public static string TimeFormat(int restTime, bool isHours = false)
  {
    return UIUtility.TimeFormat(TimeSpan.FromSeconds((double) restTime), isHours);
  }

  public static string TimeFormat(TimeSpan restTime, bool isHours = false)
  {
    if (!isHours)
    {
      int num1 = (int) restTime.TotalMinutes;
      int num2 = restTime.Seconds;
      if (num1 >= 100)
      {
        num1 = 99;
        num2 = 59;
      }
      else if (num2 < 0)
      {
        num1 = 0;
        num2 = 0;
      }
      return $"{num1:D2}:{num2:D2}";
    }
    int num3 = (int) restTime.TotalHours;
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

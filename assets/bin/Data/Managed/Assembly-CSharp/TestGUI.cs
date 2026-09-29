// Decompiled with JetBrains decompiler
// Type: TestGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TestGUI
{
  public static GameObject sender;
  private static List<int> splitNum = new List<int>();
  private static bool isAreaSettings = false;
  private static bool isBegin = false;
  private static bool isBeginDialog = false;
  private static Rect dialogRect;
  private static float areaWidth;

  private static float SCREEN_WIDTH_RATE => (float) Screen.width / 480f;

  private static float SCREEN_HEIGHT_RATE => (float) Screen.height / 800f;

  private static float SMALL_BUTTON_HEIGHT => TestGUI.SCREEN_HEIGHT_RATE * 22f;

  private static float BUTTON_HEIGHT => TestGUI.SCREEN_HEIGHT_RATE * 60f;

  public static void Button(
    string text,
    string event_name,
    object user_data = null,
    int button_height_num = 1,
    TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._Button(text, event_name, place, TestGUI.areaWidth, TestGUI.BUTTON_HEIGHT * (float) button_height_num, (System.Action) null, user_data);
  }

  public static void SmallButton(
    string text,
    string event_name,
    object user_data = null,
    int button_height_num = 1,
    TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._Button(text, event_name, place, TestGUI.areaWidth, TestGUI.SMALL_BUTTON_HEIGHT * (float) button_height_num, (System.Action) null, user_data);
  }

  public static void TempButton(
    string text,
    System.Action call_back,
    object user_data = null,
    int button_height_num = 1,
    TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._Button(text, string.Empty, place, TestGUI.areaWidth, TestGUI.BUTTON_HEIGHT, call_back, user_data);
  }

  public static void TempSmallButton(
    string text,
    System.Action call_back,
    object user_data = null,
    int button_height_num = 1,
    TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._Button(text, string.Empty, place, TestGUI.areaWidth, TestGUI.SMALL_BUTTON_HEIGHT, call_back, user_data);
  }

  public static void Label(string text, TextAnchor alignment = 4, TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._Label(text, place, TestGUI.areaWidth, TestGUI.BUTTON_HEIGHT, alignment);
  }

  public static void SmallLabel(string text, TextAnchor alignment = 4, TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._Label(text, place, TestGUI.areaWidth, TestGUI.SMALL_BUTTON_HEIGHT, alignment);
  }

  public static void TextField(ref string text, TextAnchor alignment = 4, TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._TextField(ref text, place, TestGUI.areaWidth, TestGUI.BUTTON_HEIGHT, alignment);
  }

  public static void SmallTextField(ref string text, TextAnchor alignment = 4, TestGUI.PLACE place = TestGUI.PLACE.DEFAULT)
  {
    TestGUI._TextField(ref text, place, TestGUI.areaWidth, TestGUI.SMALL_BUTTON_HEIGHT, alignment);
  }

  public static void Begin(TestGUI.PLACE flag = TestGUI.PLACE.DEFAULT)
  {
    TestGUI.isBegin = true;
    TestGUI.BeginArea(TestGUI.AREA_TYPE.DEFAULT);
    TestGUI._Begin(flag);
  }

  public static void End(TestGUI.PLACE flag = TestGUI.PLACE.DEFAULT)
  {
    TestGUI.EndArea();
    TestGUI._End(flag);
    TestGUI.isBegin = false;
  }

  public static void BeginDialog(
    TestGUI.AREA_TYPE area_type,
    int vertical_item,
    int holizon_reduction_num = 0)
  {
    TestGUI.isBegin = true;
    float holizon_reduction_rate = (float) (1.0 - 0.10000000149011612 * (double) holizon_reduction_num);
    TestGUI.BeginArea(area_type, holizon_reduction_rate);
    TestGUI.GetAreaRect(area_type, out TestGUI.dialogRect);
    float num = ((Rect) ref TestGUI.dialogRect).width * 0.1f * (float) holizon_reduction_num;
    TestGUI.areaWidth = (float) ((double) ((Rect) ref TestGUI.dialogRect).width - (double) num - 20.0);
    TestGUI.isBeginDialog = true;
    GUILayout.BeginVertical(GUIStyle.op_Implicit("Window"), new GUILayoutOption[1]
    {
      GUILayout.Height((float) ((double) TestGUI.BUTTON_HEIGHT * (double) vertical_item + (double) TestGUI.BUTTON_HEIGHT * 0.5))
    });
  }

  public static void BeginModalDialog(
    TestGUI.AREA_TYPE area_type,
    int vertical_item,
    int holizon_reduction_num = 0)
  {
    GUILayout.BeginVertical(GUIStyle.op_Implicit("Box"), new GUILayoutOption[2]
    {
      GUILayout.Width((float) Screen.width),
      GUILayout.Height((float) Screen.height)
    });
    GUILayout.FlexibleSpace();
    GUILayout.EndVertical();
    TestGUI.EndArea();
    TestGUI.BeginDialog(area_type, vertical_item, holizon_reduction_num);
  }

  public static void EndDialog()
  {
    GUILayout.EndVertical();
    TestGUI.EndArea();
    TestGUI.isBegin = false;
    TestGUI.isBeginDialog = false;
  }

  public static void BeginSplitHolizon(int split_num, string style = null)
  {
    if (split_num < 1)
      split_num = 1;
    if (style != null)
      GUILayout.BeginHorizontal(GUIStyle.op_Implicit(style), Array.Empty<GUILayoutOption>());
    else
      GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
    TestGUI.splitNum.Add(split_num);
  }

  public static void EndSplit()
  {
    GUILayout.EndHorizontal();
    TestGUI.splitNum.RemoveAt(TestGUI.splitNum.Count - 1);
  }

  public static void Space() => GUILayout.Space(TestGUI.GetUIWidth(0.0f));

  public static void MenuSpace() => GUILayout.Space(TestGUI.BUTTON_HEIGHT);

  public static void ButtonHeightSpace() => GUILayout.Space(TestGUI.BUTTON_HEIGHT);

  public static void SmallButtonHeightSpace() => GUILayout.Space(TestGUI.SMALL_BUTTON_HEIGHT);

  private static void _Button(
    string text,
    string event_name,
    TestGUI.PLACE flag,
    float width,
    float height,
    System.Action call_back,
    object user_data)
  {
    float uiWidth = TestGUI.GetUIWidth(width);
    float num = (double) height == 0.0 ? TestGUI.BUTTON_HEIGHT : height;
    GUILayoutOption[] guiLayoutOptionArray = new GUILayoutOption[2]
    {
      GUILayout.Width(uiWidth),
      GUILayout.Height(num)
    };
    TestGUI._Begin(flag);
    if (GUILayout.Button(text, guiLayoutOptionArray) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
    {
      if (call_back != null)
        call_back();
      if (event_name.Length > 0)
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("TestGUI.Button", TestGUI.sender, event_name, user_data);
    }
    if (!TestGUI.isBegin)
      TestGUI.EndArea();
    TestGUI._End(flag);
  }

  private static void _Label(
    string text,
    TestGUI.PLACE flag,
    float width,
    float height,
    TextAnchor alignment)
  {
    float uiWidth = TestGUI.GetUIWidth(width);
    float num = (double) height == 0.0 ? TestGUI.BUTTON_HEIGHT : height;
    GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
    guiStyle.alignment = alignment;
    guiStyle.normal.textColor = Color.white;
    GUILayoutOption[] guiLayoutOptionArray = new GUILayoutOption[2]
    {
      GUILayout.Width(uiWidth),
      GUILayout.Height(num)
    };
    TestGUI._Begin(flag);
    GUILayout.Label(text, guiStyle, guiLayoutOptionArray);
    if (!TestGUI.isBegin)
      TestGUI.EndArea();
    TestGUI._End(flag);
  }

  private static void _TextField(
    ref string text,
    TestGUI.PLACE flag,
    float width,
    float height,
    TextAnchor alignment)
  {
    float uiWidth = TestGUI.GetUIWidth(width);
    float num = (double) height == 0.0 ? TestGUI.BUTTON_HEIGHT : height;
    GUIStyle guiStyle = new GUIStyle(GUI.skin.textField);
    guiStyle.alignment = alignment;
    guiStyle.normal.textColor = Color.white;
    GUILayoutOption[] guiLayoutOptionArray = new GUILayoutOption[2]
    {
      GUILayout.Width(uiWidth),
      GUILayout.Height(num)
    };
    TestGUI._Begin(flag);
    text = GUILayout.TextField(text, guiStyle, guiLayoutOptionArray);
    if (!TestGUI.isBegin)
      TestGUI.EndArea();
    TestGUI._End(flag);
  }

  private static void _Begin(TestGUI.PLACE flag)
  {
    if (!TestGUI.isAreaSettings)
      TestGUI.BeginArea(TestGUI.AREA_TYPE.DEFAULT);
    switch (flag)
    {
      case TestGUI.PLACE.LEFT_TOP:
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        break;
      case TestGUI.PLACE.LEFT_MID:
      case TestGUI.PLACE.LEFT_BOTTOM:
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        break;
      case TestGUI.PLACE.CENTER_TOP:
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        break;
      case TestGUI.PLACE.CENTER_MID:
      case TestGUI.PLACE.CENTER_BOTTOM:
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        break;
      case TestGUI.PLACE.RIGHT_TOP:
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        break;
      case TestGUI.PLACE.RIGHT_MID:
      case TestGUI.PLACE.RIGHT_BOTTOM:
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        GUILayout.FlexibleSpace();
        break;
    }
  }

  private static void _End(TestGUI.PLACE flag)
  {
    switch (flag)
    {
      case TestGUI.PLACE.LEFT_TOP:
      case TestGUI.PLACE.LEFT_MID:
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        break;
      case TestGUI.PLACE.LEFT_BOTTOM:
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        break;
      case TestGUI.PLACE.CENTER_TOP:
      case TestGUI.PLACE.CENTER_MID:
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        break;
      case TestGUI.PLACE.CENTER_BOTTOM:
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        break;
      case TestGUI.PLACE.RIGHT_TOP:
      case TestGUI.PLACE.RIGHT_MID:
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        break;
      case TestGUI.PLACE.RIGHT_BOTTOM:
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        break;
    }
  }

  private static void BeginArea(TestGUI.AREA_TYPE area_type, float holizon_reduction_rate = 1f)
  {
    TestGUI.isAreaSettings = true;
    Rect rect;
    TestGUI.GetAreaRect(area_type, out rect);
    double width1 = (double) ((Rect) ref rect).width;
    ref Rect local1 = ref rect;
    ((Rect) ref local1).width = ((Rect) ref local1).width * holizon_reduction_rate;
    double width2 = (double) ((Rect) ref rect).width;
    float num = (float) (width1 - width2);
    ref Rect local2 = ref rect;
    ((Rect) ref local2).x = ((Rect) ref local2).x + num * 0.5f;
    if (!TestGUI.isBeginDialog)
      TestGUI.areaWidth = ((Rect) ref rect).width;
    GUILayout.BeginArea(rect);
  }

  private static void GetAreaRect(TestGUI.AREA_TYPE area_type, out Rect rect)
  {
    float num1;
    float num2;
    float num3;
    float num4;
    switch (area_type)
    {
      case TestGUI.AREA_TYPE.TOP:
        num1 = (float) Screen.width * 0.1f;
        num2 = 0.0f;
        num3 = (float) Screen.width * 0.9f;
        num4 = (float) Screen.height * 0.5f;
        break;
      case TestGUI.AREA_TYPE.CENTER:
        num1 = (float) Screen.width * 0.1f;
        num2 = (float) Screen.height * 0.3f;
        num3 = (float) Screen.width * 0.9f;
        num4 = (float) Screen.height * 0.7f;
        break;
      case TestGUI.AREA_TYPE.CENTER_LARGE:
        num1 = (float) Screen.width * 0.1f;
        num2 = (float) Screen.height * 0.15f;
        num3 = (float) Screen.width * 0.9f;
        num4 = (float) Screen.height * 0.85f;
        break;
      case TestGUI.AREA_TYPE.CENTER_JUMBO:
        num1 = (float) Screen.width * 0.1f;
        num2 = (float) Screen.height * 0.05f;
        num3 = (float) Screen.width * 0.9f;
        num4 = (float) Screen.height * 0.95f;
        break;
      case TestGUI.AREA_TYPE.MAXIMUM:
        num1 = (float) Screen.width * 0.1f;
        num2 = 0.0f;
        num3 = (float) Screen.width * 0.9f;
        num4 = (float) Screen.height;
        break;
      case TestGUI.AREA_TYPE.BOTTOM:
        num1 = (float) Screen.width * 0.1f;
        num2 = (float) Screen.height * 0.6f;
        num3 = (float) Screen.width;
        num4 = (float) Screen.height;
        break;
      case TestGUI.AREA_TYPE.LEFT:
        num1 = 0.0f;
        num2 = (float) Screen.height * 0.1f;
        num3 = (float) Screen.width * 0.5f;
        num4 = (float) Screen.height * 0.9f;
        break;
      case TestGUI.AREA_TYPE.RIGHT:
        num1 = (float) Screen.width * 0.5f;
        num2 = (float) Screen.height * 0.1f;
        num3 = (float) Screen.width;
        num4 = (float) Screen.height * 0.9f;
        break;
      default:
        num1 = 0.0f;
        num2 = 0.0f;
        num3 = (float) Screen.width;
        num4 = (float) Screen.height;
        break;
    }
    rect = new Rect(num1, num2, num3 - num1, num4 - num2);
  }

  private static void EndArea()
  {
    if (!TestGUI.isAreaSettings)
      return;
    TestGUI.isAreaSettings = false;
    GUILayout.EndArea();
  }

  private static float GetUIWidth(float width)
  {
    float uiWidth;
    if (TestGUI.splitNum.Count > 0)
    {
      int num1 = TestGUI.splitNum[TestGUI.splitNum.Count - 1];
      int num2 = num1 > 2 ? num1 - 2 : num1 - 1;
      float num3 = num2 > 0 ? (float) (num2 * 5 / num1) : 0.0f;
      uiWidth = TestGUI.areaWidth / (float) num1 - num3;
    }
    else
      uiWidth = (double) width == 0.0 ? TestGUI.areaWidth : width;
    return uiWidth;
  }

  public enum PLACE
  {
    DEFAULT,
    LEFT_TOP,
    LEFT_MID,
    LEFT_BOTTOM,
    CENTER_TOP,
    CENTER_MID,
    CENTER_BOTTOM,
    RIGHT_TOP,
    RIGHT_MID,
    RIGHT_BOTTOM,
  }

  public enum AREA_TYPE
  {
    DEFAULT,
    TOP,
    CENTER,
    CENTER_LARGE,
    CENTER_JUMBO,
    MAXIMUM,
    BOTTOM,
    LEFT,
    RIGHT,
  }
}

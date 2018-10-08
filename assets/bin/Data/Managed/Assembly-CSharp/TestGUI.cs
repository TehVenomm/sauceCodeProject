using System;
using System.Collections.Generic;
using UnityEngine;

public class TestGUI
{
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
		RIGHT_BOTTOM
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
		RIGHT
	}

	public static GameObject sender;

	private static List<int> splitNum = new List<int>();

	private static bool isAreaSettings = false;

	private static bool isBegin = false;

	private static bool isBeginDialog = false;

	private static Rect dialogRect;

	private static float areaWidth;

	private static float SCREEN_WIDTH_RATE => (float)Screen.get_width() / 480f;

	private static float SCREEN_HEIGHT_RATE => (float)Screen.get_height() / 800f;

	private static float SMALL_BUTTON_HEIGHT => SCREEN_HEIGHT_RATE * 22f;

	private static float BUTTON_HEIGHT => SCREEN_HEIGHT_RATE * 60f;

	public static void Button(string text, string event_name, object user_data = null, int button_height_num = 1, PLACE place = PLACE.DEFAULT)
	{
		_Button(text, event_name, place, areaWidth, BUTTON_HEIGHT * (float)button_height_num, null, user_data);
	}

	public static void SmallButton(string text, string event_name, object user_data = null, int button_height_num = 1, PLACE place = PLACE.DEFAULT)
	{
		_Button(text, event_name, place, areaWidth, SMALL_BUTTON_HEIGHT * (float)button_height_num, null, user_data);
	}

	public static void TempButton(string text, Action call_back, object user_data = null, int button_height_num = 1, PLACE place = PLACE.DEFAULT)
	{
		_Button(text, string.Empty, place, areaWidth, BUTTON_HEIGHT, call_back, user_data);
	}

	public static void TempSmallButton(string text, Action call_back, object user_data = null, int button_height_num = 1, PLACE place = PLACE.DEFAULT)
	{
		_Button(text, string.Empty, place, areaWidth, SMALL_BUTTON_HEIGHT, call_back, user_data);
	}

	public static void Label(string text, TextAnchor alignment = 4, PLACE place = PLACE.DEFAULT)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		_Label(text, place, areaWidth, BUTTON_HEIGHT, alignment);
	}

	public static void SmallLabel(string text, TextAnchor alignment = 4, PLACE place = PLACE.DEFAULT)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		_Label(text, place, areaWidth, SMALL_BUTTON_HEIGHT, alignment);
	}

	public static void TextField(ref string text, TextAnchor alignment = 4, PLACE place = PLACE.DEFAULT)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		_TextField(ref text, place, areaWidth, BUTTON_HEIGHT, alignment);
	}

	public static void SmallTextField(ref string text, TextAnchor alignment = 4, PLACE place = PLACE.DEFAULT)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		_TextField(ref text, place, areaWidth, SMALL_BUTTON_HEIGHT, alignment);
	}

	public static void Begin(PLACE flag = PLACE.DEFAULT)
	{
		isBegin = true;
		BeginArea(AREA_TYPE.DEFAULT, 1f);
		_Begin(flag);
	}

	public static void End(PLACE flag = PLACE.DEFAULT)
	{
		EndArea();
		_End(flag);
		isBegin = false;
	}

	public static void BeginDialog(AREA_TYPE area_type, int vertical_item, int holizon_reduction_num = 0)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		isBegin = true;
		float holizon_reduction_rate = 1f - 0.1f * (float)holizon_reduction_num;
		BeginArea(area_type, holizon_reduction_rate);
		GetAreaRect(area_type, out dialogRect);
		float num = dialogRect.get_width() * 0.1f;
		float num2 = num * (float)holizon_reduction_num;
		areaWidth = dialogRect.get_width() - num2 - 20f;
		isBeginDialog = true;
		GUILayout.BeginVertical(GUIStyle.op_Implicit("Window"), (GUILayoutOption[])new GUILayoutOption[1]
		{
			GUILayout.Height(BUTTON_HEIGHT * (float)vertical_item + BUTTON_HEIGHT * 0.5f)
		});
	}

	public static void BeginModalDialog(AREA_TYPE area_type, int vertical_item, int holizon_reduction_num = 0)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		GUILayout.BeginVertical(GUIStyle.op_Implicit("Box"), (GUILayoutOption[])new GUILayoutOption[2]
		{
			GUILayout.Width((float)Screen.get_width()),
			GUILayout.Height((float)Screen.get_height())
		});
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		EndArea();
		BeginDialog(area_type, vertical_item, holizon_reduction_num);
	}

	public static void EndDialog()
	{
		GUILayout.EndVertical();
		EndArea();
		isBegin = false;
		isBeginDialog = false;
	}

	public static void BeginSplitHolizon(int split_num, string style = null)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (split_num < 1)
		{
			split_num = 1;
		}
		if (style != null)
		{
			GUILayout.BeginHorizontal(GUIStyle.op_Implicit(style), (GUILayoutOption[])new GUILayoutOption[0]);
		}
		else
		{
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
		}
		splitNum.Add(split_num);
	}

	public static void EndSplit()
	{
		GUILayout.EndHorizontal();
		splitNum.RemoveAt(splitNum.Count - 1);
	}

	public static void Space()
	{
		float uIWidth = GetUIWidth(0f);
		GUILayout.Space(uIWidth);
	}

	public static void MenuSpace()
	{
		GUILayout.Space(BUTTON_HEIGHT);
	}

	public static void ButtonHeightSpace()
	{
		GUILayout.Space(BUTTON_HEIGHT);
	}

	public static void SmallButtonHeightSpace()
	{
		GUILayout.Space(SMALL_BUTTON_HEIGHT);
	}

	private static void _Button(string text, string event_name, PLACE flag, float width, float height, Action call_back, object user_data)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		float uIWidth = GetUIWidth(width);
		float num = (height != 0f) ? height : BUTTON_HEIGHT;
		GUILayoutOption[] array = (GUILayoutOption[])new GUILayoutOption[2]
		{
			GUILayout.Width(uIWidth),
			GUILayout.Height(num)
		};
		_Begin(flag);
		if (GUILayout.Button(text, array) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			call_back?.Invoke();
			if (event_name.Length > 0)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("TestGUI.Button", sender, event_name, user_data, null, true);
			}
		}
		if (!isBegin)
		{
			EndArea();
		}
		_End(flag);
	}

	private static void _Label(string text, PLACE flag, float width, float height, TextAnchor alignment)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		float uIWidth = GetUIWidth(width);
		float num = (height != 0f) ? height : BUTTON_HEIGHT;
		GUIStyle val = new GUIStyle(GUI.get_skin().get_label());
		val.set_alignment(alignment);
		val.get_normal().set_textColor(Color.get_white());
		GUILayoutOption[] array = (GUILayoutOption[])new GUILayoutOption[2]
		{
			GUILayout.Width(uIWidth),
			GUILayout.Height(num)
		};
		_Begin(flag);
		GUILayout.Label(text, val, array);
		if (!isBegin)
		{
			EndArea();
		}
		_End(flag);
	}

	private static void _TextField(ref string text, PLACE flag, float width, float height, TextAnchor alignment)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		float uIWidth = GetUIWidth(width);
		float num = (height != 0f) ? height : BUTTON_HEIGHT;
		GUIStyle val = new GUIStyle(GUI.get_skin().get_textField());
		val.set_alignment(alignment);
		val.get_normal().set_textColor(Color.get_white());
		GUILayoutOption[] array = (GUILayoutOption[])new GUILayoutOption[2]
		{
			GUILayout.Width(uIWidth),
			GUILayout.Height(num)
		};
		_Begin(flag);
		text = GUILayout.TextField(text, val, array);
		if (!isBegin)
		{
			EndArea();
		}
		_End(flag);
	}

	private static void _Begin(PLACE flag)
	{
		if (!isAreaSettings)
		{
			BeginArea(AREA_TYPE.DEFAULT, 1f);
		}
		switch (flag)
		{
		case PLACE.LEFT_TOP:
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			break;
		case PLACE.LEFT_MID:
		case PLACE.LEFT_BOTTOM:
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			break;
		case PLACE.CENTER_TOP:
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			break;
		case PLACE.CENTER_MID:
		case PLACE.CENTER_BOTTOM:
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			break;
		case PLACE.RIGHT_TOP:
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			break;
		case PLACE.RIGHT_MID:
		case PLACE.RIGHT_BOTTOM:
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			break;
		}
	}

	private static void _End(PLACE flag)
	{
		switch (flag)
		{
		case PLACE.LEFT_TOP:
		case PLACE.LEFT_MID:
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			break;
		case PLACE.LEFT_BOTTOM:
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			break;
		case PLACE.CENTER_TOP:
		case PLACE.CENTER_MID:
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			break;
		case PLACE.CENTER_BOTTOM:
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			break;
		case PLACE.RIGHT_TOP:
		case PLACE.RIGHT_MID:
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			break;
		case PLACE.RIGHT_BOTTOM:
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			break;
		}
	}

	private static void BeginArea(AREA_TYPE area_type, float holizon_reduction_rate = 1f)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		isAreaSettings = true;
		GetAreaRect(area_type, out Rect rect);
		float width = rect.get_width();
		rect.set_width(rect.get_width() * holizon_reduction_rate);
		float num = width - rect.get_width();
		rect.set_x(rect.get_x() + num * 0.5f);
		if (!isBeginDialog)
		{
			areaWidth = rect.get_width();
		}
		GUILayout.BeginArea(rect);
	}

	private static void GetAreaRect(AREA_TYPE area_type, out Rect rect)
	{
		float num;
		float num2;
		float num3;
		float num4;
		switch (area_type)
		{
		default:
			num = 0f;
			num2 = 0f;
			num3 = (float)Screen.get_width();
			num4 = (float)Screen.get_height();
			break;
		case AREA_TYPE.CENTER:
			num = (float)Screen.get_width() * 0.1f;
			num2 = (float)Screen.get_height() * 0.3f;
			num3 = (float)Screen.get_width() * 0.9f;
			num4 = (float)Screen.get_height() * 0.7f;
			break;
		case AREA_TYPE.CENTER_LARGE:
			num = (float)Screen.get_width() * 0.1f;
			num2 = (float)Screen.get_height() * 0.15f;
			num3 = (float)Screen.get_width() * 0.9f;
			num4 = (float)Screen.get_height() * 0.85f;
			break;
		case AREA_TYPE.CENTER_JUMBO:
			num = (float)Screen.get_width() * 0.1f;
			num2 = (float)Screen.get_height() * 0.05f;
			num3 = (float)Screen.get_width() * 0.9f;
			num4 = (float)Screen.get_height() * 0.95f;
			break;
		case AREA_TYPE.MAXIMUM:
			num = (float)Screen.get_width() * 0.1f;
			num2 = 0f;
			num3 = (float)Screen.get_width() * 0.9f;
			num4 = (float)Screen.get_height();
			break;
		case AREA_TYPE.TOP:
			num = (float)Screen.get_width() * 0.1f;
			num2 = 0f;
			num3 = (float)Screen.get_width() * 0.9f;
			num4 = (float)Screen.get_height() * 0.5f;
			break;
		case AREA_TYPE.BOTTOM:
			num = (float)Screen.get_width() * 0.1f;
			num2 = (float)Screen.get_height() * 0.6f;
			num3 = (float)Screen.get_width();
			num4 = (float)Screen.get_height();
			break;
		case AREA_TYPE.LEFT:
			num = 0f;
			num2 = (float)Screen.get_height() * 0.1f;
			num3 = (float)Screen.get_width() * 0.5f;
			num4 = (float)Screen.get_height() * 0.9f;
			break;
		case AREA_TYPE.RIGHT:
			num = (float)Screen.get_width() * 0.5f;
			num2 = (float)Screen.get_height() * 0.1f;
			num3 = (float)Screen.get_width();
			num4 = (float)Screen.get_height() * 0.9f;
			break;
		}
		rect._002Ector(num, num2, num3 - num, num4 - num2);
	}

	private static void EndArea()
	{
		if (isAreaSettings)
		{
			isAreaSettings = false;
			GUILayout.EndArea();
		}
	}

	private static float GetUIWidth(float width)
	{
		if (splitNum.Count <= 0)
		{
			return (width != 0f) ? width : areaWidth;
		}
		int num = splitNum[splitNum.Count - 1];
		int num2 = (num <= 2) ? (num - 1) : (num - 2);
		float num3 = (float)((num2 > 0) ? (num2 * 5 / num) : 0);
		return areaWidth / (float)num - num3;
	}
}

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

	private static float SCREEN_WIDTH_RATE => (float)Screen.width / 480f;

	private static float SCREEN_HEIGHT_RATE => (float)Screen.height / 800f;

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

	public static void Label(string text, TextAnchor alignment = TextAnchor.MiddleCenter, PLACE place = PLACE.DEFAULT)
	{
		_Label(text, place, areaWidth, BUTTON_HEIGHT, alignment);
	}

	public static void SmallLabel(string text, TextAnchor alignment = TextAnchor.MiddleCenter, PLACE place = PLACE.DEFAULT)
	{
		_Label(text, place, areaWidth, SMALL_BUTTON_HEIGHT, alignment);
	}

	public static void TextField(ref string text, TextAnchor alignment = TextAnchor.MiddleCenter, PLACE place = PLACE.DEFAULT)
	{
		_TextField(ref text, place, areaWidth, BUTTON_HEIGHT, alignment);
	}

	public static void SmallTextField(ref string text, TextAnchor alignment = TextAnchor.MiddleCenter, PLACE place = PLACE.DEFAULT)
	{
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
		isBegin = true;
		float holizon_reduction_rate = 1f - 0.1f * (float)holizon_reduction_num;
		BeginArea(area_type, holizon_reduction_rate);
		GetAreaRect(area_type, out dialogRect);
		float num = dialogRect.width * 0.1f;
		float num2 = num * (float)holizon_reduction_num;
		areaWidth = dialogRect.width - num2 - 20f;
		isBeginDialog = true;
		GUILayout.BeginVertical("Window", GUILayout.Height(BUTTON_HEIGHT * (float)vertical_item + BUTTON_HEIGHT * 0.5f));
	}

	public static void BeginModalDialog(AREA_TYPE area_type, int vertical_item, int holizon_reduction_num = 0)
	{
		GUILayout.BeginVertical("Box", GUILayout.Width((float)Screen.width), GUILayout.Height((float)Screen.height));
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
		if (split_num < 1)
		{
			split_num = 1;
		}
		if (style != null)
		{
			GUILayout.BeginHorizontal(style);
		}
		else
		{
			GUILayout.BeginHorizontal();
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
		float uIWidth = GetUIWidth(width);
		float height2 = (height != 0f) ? height : BUTTON_HEIGHT;
		GUILayoutOption[] options = new GUILayoutOption[2]
		{
			GUILayout.Width(uIWidth),
			GUILayout.Height(height2)
		};
		_Begin(flag);
		if (GUILayout.Button(text, options) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
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
		float uIWidth = GetUIWidth(width);
		float height2 = (height != 0f) ? height : BUTTON_HEIGHT;
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.label);
		gUIStyle.alignment = alignment;
		gUIStyle.normal.textColor = Color.white;
		GUILayoutOption[] options = new GUILayoutOption[2]
		{
			GUILayout.Width(uIWidth),
			GUILayout.Height(height2)
		};
		_Begin(flag);
		GUILayout.Label(text, gUIStyle, options);
		if (!isBegin)
		{
			EndArea();
		}
		_End(flag);
	}

	private static void _TextField(ref string text, PLACE flag, float width, float height, TextAnchor alignment)
	{
		float uIWidth = GetUIWidth(width);
		float height2 = (height != 0f) ? height : BUTTON_HEIGHT;
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.textField);
		gUIStyle.alignment = alignment;
		gUIStyle.normal.textColor = Color.white;
		GUILayoutOption[] options = new GUILayoutOption[2]
		{
			GUILayout.Width(uIWidth),
			GUILayout.Height(height2)
		};
		_Begin(flag);
		text = GUILayout.TextField(text, gUIStyle, options);
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
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			break;
		case PLACE.LEFT_MID:
		case PLACE.LEFT_BOTTOM:
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			break;
		case PLACE.CENTER_TOP:
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			break;
		case PLACE.CENTER_MID:
		case PLACE.CENTER_BOTTOM:
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			break;
		case PLACE.RIGHT_TOP:
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			break;
		case PLACE.RIGHT_MID:
		case PLACE.RIGHT_BOTTOM:
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
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
		isAreaSettings = true;
		GetAreaRect(area_type, out Rect rect);
		float width = rect.width;
		rect.width *= holizon_reduction_rate;
		float num = width - rect.width;
		rect.x += num * 0.5f;
		if (!isBeginDialog)
		{
			areaWidth = rect.width;
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
			num3 = (float)Screen.width;
			num4 = (float)Screen.height;
			break;
		case AREA_TYPE.CENTER:
			num = (float)Screen.width * 0.1f;
			num2 = (float)Screen.height * 0.3f;
			num3 = (float)Screen.width * 0.9f;
			num4 = (float)Screen.height * 0.7f;
			break;
		case AREA_TYPE.CENTER_LARGE:
			num = (float)Screen.width * 0.1f;
			num2 = (float)Screen.height * 0.15f;
			num3 = (float)Screen.width * 0.9f;
			num4 = (float)Screen.height * 0.85f;
			break;
		case AREA_TYPE.CENTER_JUMBO:
			num = (float)Screen.width * 0.1f;
			num2 = (float)Screen.height * 0.05f;
			num3 = (float)Screen.width * 0.9f;
			num4 = (float)Screen.height * 0.95f;
			break;
		case AREA_TYPE.MAXIMUM:
			num = (float)Screen.width * 0.1f;
			num2 = 0f;
			num3 = (float)Screen.width * 0.9f;
			num4 = (float)Screen.height;
			break;
		case AREA_TYPE.TOP:
			num = (float)Screen.width * 0.1f;
			num2 = 0f;
			num3 = (float)Screen.width * 0.9f;
			num4 = (float)Screen.height * 0.5f;
			break;
		case AREA_TYPE.BOTTOM:
			num = (float)Screen.width * 0.1f;
			num2 = (float)Screen.height * 0.6f;
			num3 = (float)Screen.width;
			num4 = (float)Screen.height;
			break;
		case AREA_TYPE.LEFT:
			num = 0f;
			num2 = (float)Screen.height * 0.1f;
			num3 = (float)Screen.width * 0.5f;
			num4 = (float)Screen.height * 0.9f;
			break;
		case AREA_TYPE.RIGHT:
			num = (float)Screen.width * 0.5f;
			num2 = (float)Screen.height * 0.1f;
			num3 = (float)Screen.width;
			num4 = (float)Screen.height * 0.9f;
			break;
		}
		rect = new Rect(num, num2, num3 - num, num4 - num2);
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

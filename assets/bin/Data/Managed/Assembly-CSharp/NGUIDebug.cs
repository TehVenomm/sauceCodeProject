using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug
{
	private static bool mRayDebug = false;

	private static List<string> mLines = new List<string>();

	private static NGUIDebug mInstance = null;

	public static bool debugRaycast
	{
		get
		{
			return mRayDebug;
		}
		set
		{
			mRayDebug = value;
			if (value && Application.get_isPlaying())
			{
				CreateInstance();
			}
		}
	}

	public NGUIDebug()
		: this()
	{
	}

	public static void CreateInstance()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		if (mInstance == null)
		{
			GameObject val = new GameObject("_NGUI Debug");
			mInstance = val.AddComponent<NGUIDebug>();
			Object.DontDestroyOnLoad(val);
		}
	}

	private static void LogString(string text)
	{
		if (Application.get_isPlaying())
		{
			if (mLines.Count > 20)
			{
				mLines.RemoveAt(0);
			}
			mLines.Add(text);
			CreateInstance();
		}
		else
		{
			Debug.Log((object)text);
		}
	}

	public static void Log(params object[] objs)
	{
		string text = string.Empty;
		for (int i = 0; i < objs.Length; i++)
		{
			text = ((i != 0) ? (text + ", " + objs[i].ToString()) : (text + objs[i].ToString()));
		}
		LogString(text);
	}

	public static void Clear()
	{
		mLines.Clear();
	}

	public static void DrawBounds(Bounds b)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		Vector3 center = b.get_center();
		Vector3 val = b.get_center() - b.get_extents();
		Vector3 val2 = b.get_center() + b.get_extents();
		Debug.DrawLine(new Vector3(val.x, val.y, center.z), new Vector3(val2.x, val.y, center.z), Color.get_red());
		Debug.DrawLine(new Vector3(val.x, val.y, center.z), new Vector3(val.x, val2.y, center.z), Color.get_red());
		Debug.DrawLine(new Vector3(val2.x, val.y, center.z), new Vector3(val2.x, val2.y, center.z), Color.get_red());
		Debug.DrawLine(new Vector3(val.x, val2.y, center.z), new Vector3(val2.x, val2.y, center.z), Color.get_red());
	}

	private void OnGUI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		Rect val = default(Rect);
		val._002Ector(5f, 5f, 1000f, 18f);
		if (mRayDebug)
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			string text = "Scheme: " + currentScheme;
			GUI.set_color(Color.get_black());
			GUI.Label(val, text);
			val.set_y(val.get_y() - 1f);
			val.set_x(val.get_x() - 1f);
			GUI.set_color(Color.get_white());
			GUI.Label(val, text);
			val.set_y(val.get_y() + 18f);
			val.set_x(val.get_x() + 1f);
			text = "Hover: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", string.Empty);
			GUI.set_color(Color.get_black());
			GUI.Label(val, text);
			val.set_y(val.get_y() - 1f);
			val.set_x(val.get_x() - 1f);
			GUI.set_color(Color.get_white());
			GUI.Label(val, text);
			val.set_y(val.get_y() + 18f);
			val.set_x(val.get_x() + 1f);
			text = "Selection: " + NGUITools.GetHierarchy(UICamera.selectedObject).Replace("\"", string.Empty);
			GUI.set_color(Color.get_black());
			GUI.Label(val, text);
			val.set_y(val.get_y() - 1f);
			val.set_x(val.get_x() - 1f);
			GUI.set_color(Color.get_white());
			GUI.Label(val, text);
			val.set_y(val.get_y() + 18f);
			val.set_x(val.get_x() + 1f);
			text = "Controller: " + NGUITools.GetHierarchy(UICamera.controllerNavigationObject).Replace("\"", string.Empty);
			GUI.set_color(Color.get_black());
			GUI.Label(val, text);
			val.set_y(val.get_y() - 1f);
			val.set_x(val.get_x() - 1f);
			GUI.set_color(Color.get_white());
			GUI.Label(val, text);
			val.set_y(val.get_y() + 18f);
			val.set_x(val.get_x() + 1f);
			text = "Active events: " + UICamera.CountInputSources();
			if (UICamera.disableController)
			{
				text += ", disabled controller";
			}
			if (UICamera.inputHasFocus)
			{
				text += ", input focus";
			}
			GUI.set_color(Color.get_black());
			GUI.Label(val, text);
			val.set_y(val.get_y() - 1f);
			val.set_x(val.get_x() - 1f);
			GUI.set_color(Color.get_white());
			GUI.Label(val, text);
			val.set_y(val.get_y() + 18f);
			val.set_x(val.get_x() + 1f);
		}
		int i = 0;
		for (int count = mLines.Count; i < count; i++)
		{
			GUI.set_color(Color.get_black());
			GUI.Label(val, mLines[i]);
			val.set_y(val.get_y() - 1f);
			val.set_x(val.get_x() - 1f);
			GUI.set_color(Color.get_white());
			GUI.Label(val, mLines[i]);
			val.set_y(val.get_y() + 18f);
			val.set_x(val.get_x() + 1f);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public class Naka
{
	private class Msg
	{
		public Color color = Color.get_white();

		public string msg = string.Empty;

		public Msg(string m)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			msg = m;
		}

		public Msg(string m, Color c)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			color = c;
			msg = m;
		}
	}

	public bool isSpeedTestForEach;

	public bool isPacketTest = true;

	private static List<Msg> msg = new List<Msg>();

	public Vector2 scrollPosition;

	public Naka()
		: this()
	{
	}

	private void Awake()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (isSpeedTestForEach)
		{
			this.get_gameObject().AddComponent<SpeedTest_ForEach>();
		}
		if (isPacketTest)
		{
			this.get_gameObject().AddComponent<PacketTest>();
		}
	}

	private void Start()
	{
		Log("naka start");
	}

	private void Update()
	{
	}

	public static void Log(string str)
	{
		msg.Insert(0, new Msg(str));
		Debug.Log((object)str);
	}

	public static void LogError(string str)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		msg.Insert(0, new Msg(str, Color.get_red()));
		Debug.LogError((object)str);
	}

	private void OnGUI()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, (GUILayoutOption[])new GUILayoutOption[2]
		{
			GUILayout.Width((float)(Screen.get_width() - 10)),
			GUILayout.Height((float)Screen.get_height())
		});
		msg.ForEach(delegate(Msg m)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			GUI.set_color(m.color);
			GUILayout.Label(m.msg, (GUILayoutOption[])new GUILayoutOption[0]);
		});
		GUILayout.EndScrollView();
	}
}

using System.Collections.Generic;
using UnityEngine;

public class Naka : MonoBehaviour
{
	private class Msg
	{
		public Color color = Color.white;

		public string msg = string.Empty;

		public Msg(string m)
		{
			msg = m;
		}

		public Msg(string m, Color c)
		{
			color = c;
			msg = m;
		}
	}

	public bool isSpeedTestForEach;

	public bool isPacketTest = true;

	private static List<Msg> msg = new List<Msg>();

	public Vector2 scrollPosition;

	private void Awake()
	{
		if (isSpeedTestForEach)
		{
			base.gameObject.AddComponent<SpeedTest_ForEach>();
		}
		if (isPacketTest)
		{
			base.gameObject.AddComponent<PacketTest>();
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
		Debug.Log(str);
	}

	public static void LogError(string str)
	{
		msg.Insert(0, new Msg(str, Color.red));
		Debug.LogError(str);
	}

	private void OnGUI()
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width - 10), GUILayout.Height(Screen.height));
		msg.ForEach(delegate(Msg m)
		{
			GUI.color = m.color;
			GUILayout.Label(m.msg);
		});
		GUILayout.EndScrollView();
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class YamashitaTest
{
	[Serializable]
	public class SaveTest
	{
		public int a;

		public int b;

		public int c;

		public int d;

		public int e;

		public int f;

		public int g;

		public int h;

		public int i;

		public int j;

		public int k;

		public int l;

		public int m;

		public int n;

		public int o;

		public int p;

		public int q;

		public int r;

		public int s;

		public int t;

		public int u;

		public int v;

		public int w;

		public int x;

		public int y;

		public int z;
	}

	[Serializable]
	public class SaveTest2
	{
		public SaveTest test1;

		public SaveTest test2;
	}

	[Serializable]
	public class SaveTest3
	{
		public List<SaveTest> test;
	}

	[Serializable]
	public class SaveTest4 : SaveTest
	{
		public int y2;

		public int z2;
	}

	[Serializable]
	public class SaveTest5
	{
		public List<SaveTest3> test;
	}

	public YamashitaTest()
		: this()
	{
	}

	private void OnGUI()
	{
		if (GUILayout.Button("セ\u30fcブ", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			SaveTest saveTest = new SaveTest();
			saveTest.b = 1;
			saveTest.g = 2;
			string text = JsonUtility.ToJson((object)saveTest);
			PlayerPrefs.SetString("YamashitaTest", text);
		}
		if (GUILayout.Button("セ\u30fcブ2", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			SaveTest2 saveTest2 = new SaveTest2();
			string text2 = JsonUtility.ToJson((object)saveTest2);
			PlayerPrefs.SetString("YamashitaTest2", text2);
		}
		if (GUILayout.Button("セ\u30fcブ3", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			SaveTest3 saveTest3 = new SaveTest3();
			saveTest3.test = new List<SaveTest>();
			for (int i = 0; i < 5; i++)
			{
				saveTest3.test.Add(new SaveTest());
			}
			string text3 = JsonUtility.ToJson((object)saveTest3);
			PlayerPrefs.SetString("YamashitaTest3", text3);
		}
		if (GUILayout.Button("セ\u30fcブ4", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			SaveTest saveTest4 = new SaveTest4();
			string text4 = JsonUtility.ToJson((object)saveTest4);
			PlayerPrefs.SetString("YamashitaTest4", text4);
		}
		if (GUILayout.Button("セ\u30fcブ5", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			SaveTest5 saveTest5 = new SaveTest5();
			saveTest5.test = new List<SaveTest3>();
			for (int j = 0; j < 5; j++)
			{
				SaveTest3 saveTest6 = new SaveTest3();
				saveTest6.test = new List<SaveTest>();
				for (int k = 0; k < 5; k++)
				{
					saveTest6.test.Add(new SaveTest());
				}
				saveTest5.test.Add(saveTest6);
			}
			string text5 = JsonUtility.ToJson((object)saveTest5);
			PlayerPrefs.SetString("YamashitaTest5", text5);
		}
		if (GUILayout.Button("ロ\u30fcドテスト\u3000JSONSerializer", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			string @string = PlayerPrefs.GetString("YamashitaTest");
			for (int l = 0; l < 1000; l++)
			{
				SaveTest saveTest7 = JSONSerializer.Deserialize<SaveTest>(@string);
			}
		}
		if (GUILayout.Button("ロ\u30fcドテスト\u3000JsonUtility", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			string string2 = PlayerPrefs.GetString("YamashitaTest");
			for (int m = 0; m < 1000; m++)
			{
				SaveTest saveTest8 = JsonUtility.FromJson<SaveTest>(string2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class YamashitaTest : MonoBehaviour
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

	private void OnGUI()
	{
		if (GUILayout.Button("セ\u30fcブ"))
		{
			SaveTest saveTest = new SaveTest();
			saveTest.b = 1;
			saveTest.g = 2;
			string value = JsonUtility.ToJson(saveTest);
			PlayerPrefs.SetString("YamashitaTest", value);
		}
		if (GUILayout.Button("セ\u30fcブ2"))
		{
			SaveTest2 obj = new SaveTest2();
			string value2 = JsonUtility.ToJson(obj);
			PlayerPrefs.SetString("YamashitaTest2", value2);
		}
		if (GUILayout.Button("セ\u30fcブ3"))
		{
			SaveTest3 saveTest2 = new SaveTest3();
			saveTest2.test = new List<SaveTest>();
			for (int i = 0; i < 5; i++)
			{
				saveTest2.test.Add(new SaveTest());
			}
			string value3 = JsonUtility.ToJson(saveTest2);
			PlayerPrefs.SetString("YamashitaTest3", value3);
		}
		if (GUILayout.Button("セ\u30fcブ4"))
		{
			SaveTest obj2 = new SaveTest4();
			string value4 = JsonUtility.ToJson(obj2);
			PlayerPrefs.SetString("YamashitaTest4", value4);
		}
		if (GUILayout.Button("セ\u30fcブ5"))
		{
			SaveTest5 saveTest3 = new SaveTest5();
			saveTest3.test = new List<SaveTest3>();
			for (int j = 0; j < 5; j++)
			{
				SaveTest3 saveTest4 = new SaveTest3();
				saveTest4.test = new List<SaveTest>();
				for (int k = 0; k < 5; k++)
				{
					saveTest4.test.Add(new SaveTest());
				}
				saveTest3.test.Add(saveTest4);
			}
			string value5 = JsonUtility.ToJson(saveTest3);
			PlayerPrefs.SetString("YamashitaTest5", value5);
		}
		if (GUILayout.Button("ロ\u30fcドテスト\u3000JSONSerializer"))
		{
			string @string = PlayerPrefs.GetString("YamashitaTest");
			for (int l = 0; l < 1000; l++)
			{
				SaveTest saveTest5 = JSONSerializer.Deserialize<SaveTest>(@string);
			}
		}
		if (GUILayout.Button("ロ\u30fcドテスト\u3000JsonUtility"))
		{
			string string2 = PlayerPrefs.GetString("YamashitaTest");
			for (int m = 0; m < 1000; m++)
			{
				SaveTest saveTest6 = JsonUtility.FromJson<SaveTest>(string2);
			}
		}
	}
}

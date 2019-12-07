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
			string value = JsonUtility.ToJson(new SaveTest
			{
				b = 1,
				g = 2
			});
			PlayerPrefs.SetString("YamashitaTest", value);
		}
		if (GUILayout.Button("セ\u30fcブ2"))
		{
			string value2 = JsonUtility.ToJson(new SaveTest2());
			PlayerPrefs.SetString("YamashitaTest2", value2);
		}
		if (GUILayout.Button("セ\u30fcブ3"))
		{
			SaveTest3 saveTest = new SaveTest3();
			saveTest.test = new List<SaveTest>();
			for (int i = 0; i < 5; i++)
			{
				saveTest.test.Add(new SaveTest());
			}
			string value3 = JsonUtility.ToJson(saveTest);
			PlayerPrefs.SetString("YamashitaTest3", value3);
		}
		if (GUILayout.Button("セ\u30fcブ4"))
		{
			string value4 = JsonUtility.ToJson(new SaveTest4());
			PlayerPrefs.SetString("YamashitaTest4", value4);
		}
		if (GUILayout.Button("セ\u30fcブ5"))
		{
			SaveTest5 saveTest2 = new SaveTest5();
			saveTest2.test = new List<SaveTest3>();
			for (int j = 0; j < 5; j++)
			{
				SaveTest3 saveTest3 = new SaveTest3();
				saveTest3.test = new List<SaveTest>();
				for (int k = 0; k < 5; k++)
				{
					saveTest3.test.Add(new SaveTest());
				}
				saveTest2.test.Add(saveTest3);
			}
			string value5 = JsonUtility.ToJson(saveTest2);
			PlayerPrefs.SetString("YamashitaTest5", value5);
		}
		if (GUILayout.Button("ロ\u30fcドテスト\u3000JSONSerializer"))
		{
			string @string = PlayerPrefs.GetString("YamashitaTest");
			for (int l = 0; l < 1000; l++)
			{
				JSONSerializer.Deserialize<SaveTest>(@string);
			}
		}
		if (GUILayout.Button("ロ\u30fcドテスト\u3000JsonUtility"))
		{
			string string2 = PlayerPrefs.GetString("YamashitaTest");
			for (int m = 0; m < 1000; m++)
			{
				JsonUtility.FromJson<SaveTest>(string2);
			}
		}
	}
}

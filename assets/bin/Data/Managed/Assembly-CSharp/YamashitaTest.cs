// Decompiled with JetBrains decompiler
// Type: YamashitaTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class YamashitaTest : MonoBehaviour
{
  private void OnGUI()
  {
    if (GUILayout.Button("セーブ", Array.Empty<GUILayoutOption>()))
      PlayerPrefs.SetString(nameof (YamashitaTest), JsonUtility.ToJson((object) new YamashitaTest.SaveTest()
      {
        b = 1,
        g = 2
      }));
    if (GUILayout.Button("セーブ2", Array.Empty<GUILayoutOption>()))
      PlayerPrefs.SetString("YamashitaTest2", JsonUtility.ToJson((object) new YamashitaTest.SaveTest2()));
    if (GUILayout.Button("セーブ3", Array.Empty<GUILayoutOption>()))
    {
      YamashitaTest.SaveTest3 saveTest3 = new YamashitaTest.SaveTest3();
      saveTest3.test = new List<YamashitaTest.SaveTest>();
      for (int index = 0; index < 5; ++index)
        saveTest3.test.Add(new YamashitaTest.SaveTest());
      PlayerPrefs.SetString("YamashitaTest3", JsonUtility.ToJson((object) saveTest3));
    }
    if (GUILayout.Button("セーブ4", Array.Empty<GUILayoutOption>()))
      PlayerPrefs.SetString("YamashitaTest4", JsonUtility.ToJson((object) new YamashitaTest.SaveTest4()));
    if (GUILayout.Button("セーブ5", Array.Empty<GUILayoutOption>()))
    {
      YamashitaTest.SaveTest5 saveTest5 = new YamashitaTest.SaveTest5();
      saveTest5.test = new List<YamashitaTest.SaveTest3>();
      for (int index1 = 0; index1 < 5; ++index1)
      {
        YamashitaTest.SaveTest3 saveTest3 = new YamashitaTest.SaveTest3();
        saveTest3.test = new List<YamashitaTest.SaveTest>();
        for (int index2 = 0; index2 < 5; ++index2)
          saveTest3.test.Add(new YamashitaTest.SaveTest());
        saveTest5.test.Add(saveTest3);
      }
      PlayerPrefs.SetString("YamashitaTest5", JsonUtility.ToJson((object) saveTest5));
    }
    if (GUILayout.Button("ロードテスト　JSONSerializer", Array.Empty<GUILayoutOption>()))
    {
      string message = PlayerPrefs.GetString(nameof (YamashitaTest));
      for (int index = 0; index < 1000; ++index)
        JSONSerializer.Deserialize<YamashitaTest.SaveTest>(message);
    }
    if (!GUILayout.Button("ロードテスト　JsonUtility", Array.Empty<GUILayoutOption>()))
      return;
    string str = PlayerPrefs.GetString(nameof (YamashitaTest));
    for (int index = 0; index < 1000; ++index)
      JsonUtility.FromJson<YamashitaTest.SaveTest>(str);
  }

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
    public YamashitaTest.SaveTest test1;
    public YamashitaTest.SaveTest test2;
  }

  [Serializable]
  public class SaveTest3
  {
    public List<YamashitaTest.SaveTest> test;
  }

  [Serializable]
  public class SaveTest4 : YamashitaTest.SaveTest
  {
    public int y2;
    public int z2;
  }

  [Serializable]
  public class SaveTest5
  {
    public List<YamashitaTest.SaveTest3> test;
  }
}

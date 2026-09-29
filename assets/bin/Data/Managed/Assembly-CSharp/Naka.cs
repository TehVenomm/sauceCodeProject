// Decompiled with JetBrains decompiler
// Type: Naka
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Naka : MonoBehaviour
{
  public bool isSpeedTestForEach;
  public bool isPacketTest = true;
  private static List<Naka.Msg> msg = new List<Naka.Msg>();
  public Vector2 scrollPosition;

  private void Awake()
  {
    if (this.isSpeedTestForEach)
      ((Component) this).gameObject.AddComponent<SpeedTest_ForEach>();
    if (!this.isPacketTest)
      return;
    ((Component) this).gameObject.AddComponent<PacketTest>();
  }

  private void Start() => Naka.Log("naka start");

  private void Update()
  {
  }

  public static void Log(string str)
  {
    Naka.msg.Insert(0, new Naka.Msg(str));
    Debug.Log((object) str);
  }

  public static void LogError(string str)
  {
    Naka.msg.Insert(0, new Naka.Msg(str, Color.red));
    Debug.LogError((object) str);
  }

  private void OnGUI()
  {
    this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[2]
    {
      GUILayout.Width((float) (Screen.width - 10)),
      GUILayout.Height((float) Screen.height)
    });
    Naka.msg.ForEach((Action<Naka.Msg>) (m =>
    {
      GUI.color = m.color;
      GUILayout.Label(m.msg, Array.Empty<GUILayoutOption>());
    }));
    GUILayout.EndScrollView();
  }

  private class Msg
  {
    public Color color = Color.white;
    public string msg = string.Empty;

    public Msg(string m) => this.msg = m;

    public Msg(string m, Color c)
    {
      this.color = c;
      this.msg = m;
    }
  }
}

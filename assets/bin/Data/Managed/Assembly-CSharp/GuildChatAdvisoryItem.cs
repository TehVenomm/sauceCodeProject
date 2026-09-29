// Decompiled with JetBrains decompiler
// Type: GuildChatAdvisoryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GuildChatAdvisoryItem : MonoBehaviour
{
  public Transform close;
  [SerializeField]
  private UILabel full_title;
  private string title;
  private string content;

  public void Init(string t, string c)
  {
    this.title = t;
    this.content = c;
    this.full_title.text = this.content;
  }

  public static bool HasReadNew()
  {
    int num = PlayerPrefs.GetInt("Guild_Chat_Advisory_New", 0);
    return num != 0 && num == DateTime.Now.Day;
  }

  public static void SetReadNew()
  {
    PlayerPrefs.SetInt("Guild_Chat_Advisory_New", DateTime.Now.Day);
  }

  public static bool HasReadHomeNew()
  {
    int num = PlayerPrefs.GetInt("Home_Chat_Advisory_New", 0);
    return num != 0 && num == DateTime.Now.Day;
  }

  public static void SetReadHomeNew()
  {
    PlayerPrefs.SetInt("Home_Chat_Advisory_New", DateTime.Now.Day);
  }
}

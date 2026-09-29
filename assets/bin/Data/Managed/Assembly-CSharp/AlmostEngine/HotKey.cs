// Decompiled with JetBrains decompiler
// Type: AlmostEngine.HotKey
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace AlmostEngine;

[Serializable]
public class HotKey
{
  public bool m_Shift;
  public bool m_Control;
  public bool m_Alt;
  public KeyCode m_Key;

  public HotKey()
  {
  }

  public HotKey(bool shift, bool control, bool alt, KeyCode key)
  {
    this.m_Shift = shift;
    this.m_Control = control;
    this.m_Alt = alt;
    this.m_Key = key;
  }

  public bool IsPressed()
  {
    return this.m_Key != null && (!this.m_Shift || Input.GetKey((KeyCode) 304) || Input.GetKey((KeyCode) 303)) && (!this.m_Control || Input.GetKey((KeyCode) 306) || Input.GetKey((KeyCode) 305)) && (!this.m_Alt || Input.GetKey((KeyCode) 308) || Input.GetKey((KeyCode) 307)) && Input.GetKeyUp(this.m_Key);
  }
}

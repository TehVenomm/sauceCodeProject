// Decompiled with JetBrains decompiler
// Type: CheckAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CheckAnim : MonoBehaviour
{
  public Animation m_Target;

  private void OnGUI()
  {
    if (Object.op_Equality((Object) this.m_Target, (Object) null) || !GUILayout.Button("play", Array.Empty<GUILayoutOption>()))
      return;
    this.m_Target.Stop();
    this.m_Target.Play();
  }
}

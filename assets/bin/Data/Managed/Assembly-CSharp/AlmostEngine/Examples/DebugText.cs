// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.DebugText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace AlmostEngine.Examples;

public class DebugText : MonoBehaviour
{
  private Text m_Text;

  private void Start() => this.m_Text = ((Component) this).GetComponent<Text>();

  private void Update()
  {
    this.m_Text.text = $"{(object) Screen.width}x{(object) Screen.height}  Screen.dpi: {(object) Screen.dpi}";
  }
}

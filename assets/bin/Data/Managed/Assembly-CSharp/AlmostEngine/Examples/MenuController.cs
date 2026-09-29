// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.MenuController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Examples;

public class MenuController : MonoBehaviour
{
  public CameraController m_Player;

  private void Awake() => ((Behaviour) this.m_Player).enabled = false;

  public void OnButtonClickCallback()
  {
    ((Behaviour) this.m_Player).enabled = true;
    ((Component) this).gameObject.SetActive(false);
  }
}

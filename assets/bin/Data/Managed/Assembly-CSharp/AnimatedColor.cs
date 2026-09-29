// Decompiled with JetBrains decompiler
// Type: AnimatedColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[RequireComponent(typeof (UIWidget))]
public class AnimatedColor : MonoBehaviour
{
  public Color color = Color.white;
  private UIWidget mWidget;

  private void OnEnable()
  {
    this.mWidget = ((Component) this).GetComponent<UIWidget>();
    this.LateUpdate();
  }

  private void LateUpdate() => this.mWidget.color = this.color;
}

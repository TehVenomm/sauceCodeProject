// Decompiled with JetBrains decompiler
// Type: AnimatedWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
  public float width = 1f;
  public float height = 1f;
  private UIWidget mWidget;

  private void OnEnable()
  {
    this.mWidget = ((Component) this).GetComponent<UIWidget>();
    this.LateUpdate();
  }

  private void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.mWidget, (Object) null))
      return;
    this.mWidget.width = Mathf.RoundToInt(this.width);
    this.mWidget.height = Mathf.RoundToInt(this.height);
  }
}

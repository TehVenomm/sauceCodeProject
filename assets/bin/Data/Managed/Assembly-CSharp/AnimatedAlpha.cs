// Decompiled with JetBrains decompiler
// Type: AnimatedAlpha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
  [Range(0.0f, 1f)]
  public float alpha = 1f;
  private UIWidget mWidget;
  private UIPanel mPanel;

  private void OnEnable()
  {
    this.mWidget = ((Component) this).GetComponent<UIWidget>();
    this.mPanel = ((Component) this).GetComponent<UIPanel>();
    this.LateUpdate();
  }

  private void LateUpdate()
  {
    if (Object.op_Inequality((Object) this.mWidget, (Object) null))
      this.mWidget.alpha = this.alpha;
    if (!Object.op_Inequality((Object) this.mPanel, (Object) null))
      return;
    this.mPanel.alpha = this.alpha;
  }
}

// Decompiled with JetBrains decompiler
// Type: AbilityBGWrapContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AbilityBGWrapContent : UIWrapContent
{
  private UIScrollView scroll;

  protected override void OnMove(UIPanel panel)
  {
    if (Object.op_Equality((Object) this.scroll, (Object) null))
      this.scroll = ((Component) this).GetComponentInParent<UIScrollView>();
    base.OnMove(panel);
    this.scroll.restrictWithinPanel = true;
  }
}

// Decompiled with JetBrains decompiler
// Type: UIScreenRotationAnchor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIScreenRotationAnchor : UIScreenRotationHandler
{
  [SerializeField]
  private UIRect rect;
  [SerializeField]
  private UIScreenRotationAnchor.Anchors portrait;
  [SerializeField]
  private UIScreenRotationAnchor.Anchors landscape;

  protected override void OnScreenRotate(bool is_portrait)
  {
    if (is_portrait)
      this.portrait.Set(this.rect);
    else
      this.landscape.Set(this.rect);
  }

  [Serializable]
  private class Anchors
  {
    [SerializeField]
    private UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();
    [SerializeField]
    private UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);
    [SerializeField]
    private UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();
    [SerializeField]
    private UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

    public void Set(UIRect rect)
    {
      rect.leftAnchor.Set(this.leftAnchor.target, this.leftAnchor.relative, (float) this.leftAnchor.absolute);
      rect.rightAnchor.Set(this.rightAnchor.target, this.rightAnchor.relative, (float) this.rightAnchor.absolute);
      rect.bottomAnchor.Set(this.bottomAnchor.target, this.bottomAnchor.relative, (float) this.bottomAnchor.absolute);
      rect.topAnchor.Set(this.topAnchor.target, this.topAnchor.relative, (float) this.topAnchor.absolute);
    }
  }
}

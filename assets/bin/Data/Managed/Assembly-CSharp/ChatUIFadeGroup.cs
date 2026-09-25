// Decompiled with JetBrains decompiler
// Type: ChatUIFadeGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatUIFadeGroup(UIRect root) : ChatUITweenGroup<TweenAlpha>(root)
{
  protected override void InitTween(TweenAlpha tween, bool isOpenTween)
  {
    if (isOpenTween)
      this.InitTween(tween, 0.0f, 1f);
    else
      this.InitTween(tween, 1f, 0.0f);
  }

  private void InitTween(TweenAlpha tween, float from_alpha, float to_alpha)
  {
    tween.from = from_alpha;
    tween.to = to_alpha;
    tween.method = UITweener.Method.EaseOut;
    tween.duration = 0.25f;
    ((Behaviour) tween).enabled = false;
  }
}

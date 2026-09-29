// Decompiled with JetBrains decompiler
// Type: ChatUISlideGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatUISlideGroup : ChatUITweenGroup<TweenPosition>
{
  private Vector3 startPos;
  private Vector3 endPos;

  public ChatUISlideGroup(UIRect root, Vector3 start_pos, Vector3 end_pos)
    : base(root)
  {
    this.startPos = start_pos;
    this.endPos = end_pos;
  }

  protected override void InitTween(TweenPosition tween, bool isOpenTween)
  {
    if (isOpenTween)
      this.InitTween(tween, this.startPos, this.endPos);
    else
      this.InitTween(tween, this.endPos, this.startPos);
  }

  private void InitTween(TweenPosition tween, Vector3 from_position, Vector3 to_position)
  {
    tween.from = from_position;
    tween.to = to_position;
    tween.method = UITweener.Method.EaseOut;
    tween.duration = 0.25f;
    ((Behaviour) tween).enabled = false;
  }

  protected override void OnPreClose() => (this.rootRect as UIPanel).widgetsAreStatic = true;

  protected override void OnPostOpen() => (this.rootRect as UIPanel).widgetsAreStatic = false;
}

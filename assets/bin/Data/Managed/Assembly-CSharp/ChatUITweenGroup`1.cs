// Decompiled with JetBrains decompiler
// Type: ChatUITweenGroup`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class ChatUITweenGroup<T> : ChatUITweenGroup where T : UITweener
{
  public ChatUITweenGroup(UIRect root)
    : base(root)
  {
  }

  protected override UITweener CreateTween(bool isOpenTween)
  {
    if (Object.op_Equality((Object) this.rootRect, (Object) null))
      return (UITweener) null;
    T tween = ((Component) this.rootRect).gameObject.AddComponent<T>();
    this.InitTween(tween, isOpenTween);
    return (UITweener) tween;
  }

  protected abstract void InitTween(T tween, bool isOpenTween);
}

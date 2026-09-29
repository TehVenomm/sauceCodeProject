// Decompiled with JetBrains decompiler
// Type: UITweenAddToChildrenCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UITweenAddToChildrenCtrl : MonoBehaviour
{
  public UITweener baseTween;
  public float dispDuration;
  public float dispStartDelay;
  public int repetitionStartIndex = -1;

  private void Awake()
  {
    if (!Object.op_Equality((Object) this.baseTween, (Object) null))
      return;
    this.baseTween = ((Component) this).GetComponent<UITweener>();
  }

  [ContextMenu("TweenAdd")]
  public void TweenAdd()
  {
    if (!((Behaviour) this).enabled || Object.op_Equality((Object) this.baseTween, (Object) null))
      return;
    int childCount = ((Component) this).transform.childCount;
    Transform[] transformArray = new Transform[childCount];
    for (int index = 0; index < childCount; ++index)
      transformArray[index] = ((Component) this).transform.GetChild(index);
    for (int index = 0; index < childCount; ++index)
    {
      Transform transform = transformArray[index];
      if (Object.op_Equality((Object) transform, (Object) null))
        return;
      UITweenAddCtrlChild[] componentsInChildren = ((Component) transform).GetComponentsInChildren<UITweenAddCtrlChild>();
      if (componentsInChildren == null || componentsInChildren.Length == 0)
      {
        GameObject gameObject = new GameObject(((Object) transform).name);
        gameObject.layer = 5;
        gameObject.transform.parent = ((Component) transform).transform.parent;
        gameObject.transform.localPosition = ((Component) transform).transform.localPosition;
        gameObject.transform.localScale = Vector3.one;
        gameObject.AddComponent<UITweenAddCtrlChild>();
        UIWidget component = ((Component) transform).GetComponent<UIWidget>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          UIWidget uiWidget = gameObject.AddComponent<UIWidget>();
          uiWidget.width = component.width;
          uiWidget.height = component.height;
          uiWidget.keepAspectRatio = component.keepAspectRatio;
          uiWidget.pivot = component.pivot;
          uiWidget.depth = component.depth;
          uiWidget.alpha = component.alpha;
        }
        ((Component) transform).transform.parent = gameObject.transform;
        if (Object.op_Equality((Object) ((Component) transform).gameObject.GetComponent(((object) this.baseTween).GetType()), (Object) null))
          ((Component) transform).gameObject.AddComponent(((object) this.baseTween).GetType());
      }
    }
    this.InitTween();
  }

  public void SkipTween() => this._InitTween(true);

  public void InitTween() => this._InitTween(false);

  private void _InitTween(bool is_skip)
  {
    if (!((Behaviour) this).enabled)
      return;
    int childCount = ((Component) this).transform.childCount;
    for (int i = 0; i < childCount; ++i)
    {
      Transform child = ((Component) this).transform.GetChild(i);
      if (Object.op_Inequality((Object) child, (Object) null))
      {
        Component componentInChildren = ((Component) child).gameObject.GetComponentInChildren(((object) this.baseTween).GetType());
        switch (componentInChildren)
        {
          case TweenAlpha _:
            TweenAlpha new_tw1 = componentInChildren as TweenAlpha;
            TweenAlpha baseTween1 = this.baseTween as TweenAlpha;
            new_tw1.from = baseTween1.from;
            new_tw1.to = baseTween1.to;
            this.InitTween((UITweener) new_tw1, (UITweener) baseTween1, i, is_skip);
            continue;
          case TweenColor _:
            TweenColor new_tw2 = componentInChildren as TweenColor;
            TweenColor baseTween2 = this.baseTween as TweenColor;
            new_tw2.from = baseTween2.from;
            new_tw2.to = baseTween2.to;
            this.InitTween((UITweener) new_tw2, (UITweener) baseTween2, i, is_skip);
            continue;
          case TweenPosition _:
            TweenPosition new_tw3 = componentInChildren as TweenPosition;
            TweenPosition baseTween3 = this.baseTween as TweenPosition;
            new_tw3.from = baseTween3.from;
            new_tw3.to = baseTween3.to;
            this.InitTween((UITweener) new_tw3, (UITweener) baseTween3, i, is_skip);
            continue;
          case TweenRotation _:
            TweenRotation new_tw4 = componentInChildren as TweenRotation;
            TweenRotation baseTween4 = this.baseTween as TweenRotation;
            new_tw4.from = baseTween4.from;
            new_tw4.to = baseTween4.to;
            this.InitTween((UITweener) new_tw4, (UITweener) baseTween4, i, is_skip);
            continue;
          case TweenScale _:
            TweenScale new_tw5 = componentInChildren as TweenScale;
            TweenScale baseTween5 = this.baseTween as TweenScale;
            new_tw5.from = baseTween5.from;
            new_tw5.to = baseTween5.to;
            this.InitTween((UITweener) new_tw5, (UITweener) baseTween5, i, is_skip);
            continue;
          case TweenWidth _:
            TweenWidth new_tw6 = componentInChildren as TweenWidth;
            TweenWidth baseTween6 = this.baseTween as TweenWidth;
            new_tw6.from = baseTween6.from;
            new_tw6.to = baseTween6.to;
            this.InitTween((UITweener) new_tw6, (UITweener) baseTween6, i, is_skip);
            continue;
          case TweenHeight _:
            TweenHeight new_tw7 = componentInChildren as TweenHeight;
            TweenHeight baseTween7 = this.baseTween as TweenHeight;
            new_tw7.from = baseTween7.from;
            new_tw7.to = baseTween7.to;
            this.InitTween((UITweener) new_tw7, (UITweener) baseTween7, i, is_skip);
            continue;
          default:
            continue;
        }
      }
    }
  }

  private void InitTween(UITweener new_tw, UITweener base_tw, int i, bool is_skip)
  {
    int num = this.repetitionStartIndex > -1 ? Mathf.Min(this.repetitionStartIndex, i) : i;
    new_tw.animationCurve = base_tw.animationCurve;
    new_tw.style = base_tw.style;
    new_tw.duration = base_tw.duration + this.dispDuration * (float) num;
    new_tw.delay = base_tw.delay + this.dispStartDelay * (float) num;
    new_tw.ignoreTimeScale = base_tw.ignoreTimeScale;
    new_tw.tweenGroup = base_tw.tweenGroup;
    ((Behaviour) new_tw).enabled = true;
    if (!is_skip)
    {
      new_tw.ResetToBeginning();
    }
    else
    {
      new_tw.tweenFactor = 1f;
      new_tw.Sample(new_tw.tweenFactor, false);
    }
  }
}

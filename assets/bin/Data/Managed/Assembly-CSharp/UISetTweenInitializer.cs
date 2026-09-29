// Decompiled with JetBrains decompiler
// Type: UISetTweenInitializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIWidget))]
public class UISetTweenInitializer : MonoBehaviour
{
  private void Awake()
  {
    if (!Application.isPlaying)
      return;
    Transform transform = ((Component) this).transform;
    UIWidget component = ((Component) this).GetComponent<UIWidget>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    UITweener[] components = ((Component) this).GetComponents<UITweener>();
    if (components == null || components.Length == 0)
      return;
    int length = components.Length;
    for (int index = 0; index < length; ++index)
    {
      UITweener uiTweener = components[index];
      if (Object.op_Inequality((Object) uiTweener, (Object) null))
      {
        switch (uiTweener)
        {
          case TweenAlpha _:
            TweenAlpha tweenAlpha = uiTweener as TweenAlpha;
            component.alpha = tweenAlpha.from;
            continue;
          case TweenColor _:
            TweenColor tweenColor = uiTweener as TweenColor;
            component.color = tweenColor.from;
            continue;
          case TweenPosition _:
            TweenPosition tweenPosition = uiTweener as TweenPosition;
            transform.localPosition = tweenPosition.from;
            continue;
          case TweenRotation _:
            TweenRotation tweenRotation = uiTweener as TweenRotation;
            transform.localEulerAngles = tweenRotation.from;
            continue;
          case TweenScale _:
            TweenScale tweenScale = uiTweener as TweenScale;
            transform.localScale = tweenScale.from;
            continue;
          case TweenWidth _:
            TweenWidth tweenWidth = uiTweener as TweenWidth;
            component.width = tweenWidth.from;
            continue;
          case TweenHeight _:
            TweenHeight tweenHeight = uiTweener as TweenHeight;
            component.height = tweenHeight.from;
            continue;
          default:
            continue;
        }
      }
    }
  }
}

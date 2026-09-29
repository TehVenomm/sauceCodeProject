// Decompiled with JetBrains decompiler
// Type: Curves
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class Curves
{
  public static readonly AnimationCurve easeLinear = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
  public static readonly AnimationCurve easeInOut = AnimationCurve.EaseInOut(0.0f, 0.0f, 1f, 1f);
  public static readonly AnimationCurve easeIn = Curves.CreateEaseInCurve();
  public static readonly AnimationCurve sinCurve = Curves.CreateSinCurve();
  public static readonly AnimationCurve sinHalfCurve = Curves.CreateSinHalfCurve();
  public static readonly AnimationCurve arcHalfCurve = Curves.CreateArcHalfCurve();

  public static AnimationCurve CreateSinCurve()
  {
    AnimationCurve sinCurve = new AnimationCurve();
    sinCurve.AddKey(new Keyframe(0.25f, 1f, 0.0f, 0.0f));
    sinCurve.AddKey(new Keyframe(0.75f, -1f, 0.0f, 0.0f));
    sinCurve.preWrapMode = (WrapMode) 4;
    sinCurve.postWrapMode = (WrapMode) 4;
    return sinCurve;
  }

  public static AnimationCurve CreateSinHalfCurve()
  {
    AnimationCurve sinHalfCurve = new AnimationCurve();
    sinHalfCurve.AddKey(new Keyframe(0.0f, 0.0f, 0.0f, 0.0f));
    sinHalfCurve.AddKey(new Keyframe(0.5f, 1f, 0.0f, 0.0f));
    sinHalfCurve.preWrapMode = (WrapMode) 4;
    sinHalfCurve.postWrapMode = (WrapMode) 4;
    return sinHalfCurve;
  }

  public static AnimationCurve CreateEaseInCurve()
  {
    AnimationCurve easeInCurve = new AnimationCurve();
    easeInCurve.AddKey(new Keyframe(0.0f, 0.0f, 0.0f, 0.0f));
    easeInCurve.AddKey(new Keyframe(1f, 1f, 2f, 1f));
    return easeInCurve;
  }

  public static AnimationCurve CreateArcHalfCurve()
  {
    AnimationCurve arcHalfCurve = new AnimationCurve();
    arcHalfCurve.AddKey(new Keyframe(0.0f, 0.0f, 0.0f, 4f));
    arcHalfCurve.AddKey(new Keyframe(0.5f, 1f, 0.0f, 0.0f));
    arcHalfCurve.AddKey(new Keyframe(1f, 0.0f, -4f, 0.0f));
    return arcHalfCurve;
  }
}

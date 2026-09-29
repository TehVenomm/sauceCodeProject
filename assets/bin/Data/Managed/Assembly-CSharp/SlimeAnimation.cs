// Decompiled with JetBrains decompiler
// Type: SlimeAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SlimeAnimation
{
  private SlimeAnimation.SlimeParamAnimator<SlimePosAnim, Vector3> posAnimator;
  private SlimeAnimation.SlimeParamAnimator<SlimeScaleAnim, Vector3> scaleAnimator;
  private SlimeAnimation.SlimeParamAnimator<SlimeColorAnim, Color> colorAnimator;
  private bool isFadeOut;
  private SlimeController slime;
  private Transform slimeTransform;
  private Material slimeMaterial;
  private const float INTERPOLATION_TIME_POS = 0.2f;
  private const float INTERPOLATION_TIME_SCALE = 0.1f;
  private const float INTERPOLATION_TIME_COLOR = 0.5f;
  private const float FADEIN_ALPHA_MIN = 0.0f;
  private const float FADEIN_ALPHA_MAX = 0.5f;
  private const float FADEOUT_ALPHA_MIN = 0.0f;
  private const float FADEOUT_ALPHA_MAX = 0.5f;
  private const float CRUSH_ALPHA_MIN = 0.0f;
  private const float CRUSH_ALPHA_MAX = 0.5f;
  private const float NON_ANIM_TIME = 1f;
  private readonly AnimationCurve SLIME_ANIM_CURVE_ZERO = AnimationCurve.Linear(0.0f, 0.0f, 0.0f, 0.0f);
  private readonly AnimationCurve SLIME_ANIM_CURVE_ONE = AnimationCurve.Linear(0.0f, 1f, 0.0f, 1f);
  private readonly AnimationCurve SLIME_ANIM_CURVE_HALF = AnimationCurve.Linear(0.0f, 0.5f, 0.0f, 0.5f);

  public SlimeAnimation(SlimeController slime_controller)
  {
    this.slime = slime_controller;
    this.slimeTransform = ((Component) this.slime).transform;
    this.slimeMaterial = ((Component) this.slime).GetComponent<Renderer>().material;
    this.posAnimator = new SlimeAnimation.SlimeParamAnimator<SlimePosAnim, Vector3>(0.2f);
    this.scaleAnimator = new SlimeAnimation.SlimeParamAnimator<SlimeScaleAnim, Vector3>(0.1f);
    this.colorAnimator = new SlimeAnimation.SlimeParamAnimator<SlimeColorAnim, Color>(0.5f);
  }

  public void Update()
  {
    if (this.posAnimator.IsPlaying())
      this.slimeTransform.localPosition = this.posAnimator.Update();
    if (this.scaleAnimator.IsPlaying())
      this.slimeTransform.localScale = this.scaleAnimator.Update();
    if (this.colorAnimator.IsPlaying())
    {
      Color color = this.colorAnimator.Update();
      this.slimeMaterial.color = color;
      if ((double) color.a <= 0.0099999997764825821 && this.isFadeOut)
        this.slime.SetInvisible();
      else
        this.slime.SetVisible();
    }
    else
    {
      if (!this.slime.IsVisible() || (double) this.slimeMaterial.color.a <= 0.0 || !this.isFadeOut)
        return;
      this.slimeMaterial.color = this.colorAnimator.Update();
      this.slime.SetInvisible();
    }
  }

  public bool IsPlaying()
  {
    return this.posAnimator.IsPlaying() || this.scaleAnimator.IsPlaying() || this.colorAnimator.IsPlaying();
  }

  public void TouchOn(System.Action CallBackPos = null, System.Action CallBackScale = null, System.Action CallBackColor = null)
  {
    this.posAnimator.SetAnimation(this.SLIME_ANIM_CURVE_ZERO, 1f, true, this.slimeTransform.localPosition, CallBackPos);
    this.scaleAnimator.SetAnimation(this.slime.animFadeIn, this.slime.fadeInAnimTime, true, this.slimeTransform.localScale, CallBackScale);
    this.colorAnimator.SetAnimation(AnimationCurve.Linear(0.0f, 0.0f, 1f, 0.5f), this.slime.fadeInColorAnimTime, false, this.slimeMaterial.color, CallBackColor);
    this.isFadeOut = false;
  }

  public void TouchOff(System.Action CallBackPos = null, System.Action CallBackScale = null, System.Action CallBackColor = null)
  {
    this.posAnimator.SetAnimation(this.slime.animFadeOut, this.slime.fadeOutAnimTime, true, this.slimeTransform.localPosition, CallBackPos);
    this.scaleAnimator.SetAnimation(this.SLIME_ANIM_CURVE_ONE, 1f, true, this.slimeTransform.localScale, CallBackScale);
    this.colorAnimator.SetAnimation(AnimationCurve.Linear(0.0f, 0.5f, 1f, 0.0f), this.slime.fadeOutColorAnimTime, true, this.slimeMaterial.color, CallBackColor);
    this.isFadeOut = true;
  }

  public void Crush(System.Action CallBackPos = null, System.Action CallBackScale = null, System.Action CallBackColor = null)
  {
    this.posAnimator.SetAnimation(this.SLIME_ANIM_CURVE_ZERO, 1f, false, this.slimeTransform.localPosition, CallBackPos);
    this.scaleAnimator.SetAnimation(this.slime.animCrush, this.slime.crushAnimTime, true, this.slimeTransform.localScale, CallBackScale);
    this.colorAnimator.SetAnimation(AnimationCurve.Linear(0.0f, 0.5f, 1f, 0.0f), this.slime.crushColorAnimTime, false, this.slimeMaterial.color, CallBackColor);
    this.isFadeOut = true;
  }

  public void ScaleUp(System.Action CallBackPos = null, System.Action CallBackScale = null, System.Action CallBackColor = null)
  {
    this.posAnimator.SetAnimation(this.SLIME_ANIM_CURVE_ZERO, 1f, true, this.slimeTransform.localPosition, CallBackPos);
    this.scaleAnimator.SetAnimation(AnimationCurve.Linear(0.0f, 1f, this.slime.scaleupAnimTime, this.slime.scaleupAnimMaxScale), this.slime.scaleupAnimTime, true, this.slimeTransform.localScale, CallBackScale);
    this.colorAnimator.SetAnimation(this.SLIME_ANIM_CURVE_HALF, 1f, false, this.slimeMaterial.color, CallBackColor);
    this.isFadeOut = false;
  }

  public void ScaleUpDown(System.Action CallBackPos = null, System.Action CallBackScale = null, System.Action CallBackColor = null)
  {
    this.posAnimator.SetAnimation(this.SLIME_ANIM_CURVE_ZERO, 1f, true, this.slimeTransform.localPosition, CallBackPos);
    this.scaleAnimator.SetAnimation(this.slime.animScaleUpDown, this.slime.scaleUpDownAnimTime, true, this.slimeTransform.localScale, CallBackScale);
    this.colorAnimator.SetAnimation(this.SLIME_ANIM_CURVE_HALF, 1f, false, this.slimeMaterial.color, CallBackColor);
    this.isFadeOut = false;
  }

  private class SlimeParamAnimator<T, T2>
    where T : SlimeAnimBase<T2>, new()
    where T2 : new()
  {
    private T anim;

    public SlimeParamAnimator(float time, float start = 0.0f, float end = 1f)
    {
      this.anim = new T();
      this.anim.SetBlendParam(AnimationCurve.Linear(0.0f, start, time, end), time);
    }

    public T2 Update() => this.anim.Update();

    public void SetAnimation(
      AnimationCurve curve,
      float time,
      bool is_blend,
      T2 param,
      System.Action cb)
    {
      this.anim.InitAnim(curve, time, is_blend, param, cb);
    }

    public bool IsPlaying() => this.anim.isPlaying;

    public void Terminate() => this.anim.Terminate();

    public T GetAnimData() => this.anim;
  }
}

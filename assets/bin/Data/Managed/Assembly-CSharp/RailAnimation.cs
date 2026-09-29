// Decompiled with JetBrains decompiler
// Type: RailAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RailAnimation : MonoBehaviour
{
  public const string DEFAULT_RAIL_ANIM_NAME = "DefaultRailAnim";
  public AnimationClip railAnimClip;
  private AnimationState currentAnimState;
  private AnimationState nextAnimState;
  private FloatInterpolator changeInterp = new FloatInterpolator();
  private bool addAnim;
  private float _rate;

  public Animation _animation { get; private set; }

  public float rate
  {
    get => this._rate;
    set
    {
      this._rate = value;
      if ((double) this._rate < 0.0)
        this._rate = 0.0f;
      else if ((double) this._rate > 1.0)
        this._rate = 1f;
      if (TrackedReference.op_Equality((TrackedReference) this.currentAnimState, (TrackedReference) null))
      {
        if (Object.op_Equality((Object) this.railAnimClip, (Object) null))
          return;
        this.AddRailAnimClip(this.railAnimClip, "DefaultRailAnim");
      }
      this.currentAnimState.time = this.currentAnimState.length * this._rate;
    }
  }

  public bool enabledRail
  {
    get => ((Behaviour) this).enabled;
    set
    {
      ((Behaviour) this).enabled = value;
      ((Behaviour) this._animation).enabled = value;
    }
  }

  private void Awake()
  {
    this._animation = ((Component) this).GetComponent<Animation>();
    if (!Object.op_Equality((Object) this._animation, (Object) null))
      return;
    this._animation = ((Component) this).gameObject.AddComponent<Animation>();
    this._animation.playAutomatically = false;
    this.addAnim = true;
  }

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit || !this.addAnim)
      return;
    Object.DestroyImmediate((Object) this._animation);
    this._animation = (Animation) null;
    this.addAnim = false;
  }

  private void Update()
  {
    if (TrackedReference.op_Equality((TrackedReference) this.nextAnimState, (TrackedReference) null))
    {
      if (!TrackedReference.op_Inequality((TrackedReference) this.currentAnimState, (TrackedReference) null))
        return;
      this.currentAnimState.enabled = true;
      this.currentAnimState.weight = 1f;
    }
    else
    {
      double num = (double) this.changeInterp.Update();
      this.currentAnimState.weight = this.changeInterp.Get();
      this.nextAnimState.weight = 1f - this.currentAnimState.weight;
      if (this.changeInterp.IsPlaying())
        return;
      this.currentAnimState.enabled = false;
      this.currentAnimState.weight = 0.0f;
      this.currentAnimState = this.nextAnimState;
      this.nextAnimState = (AnimationState) null;
      this._rate = this.currentAnimState.time / this.currentAnimState.length;
    }
  }

  public void AddRailAnimClip(AnimationClip anim_clip, string name)
  {
    if (Object.op_Equality((Object) anim_clip, (Object) null))
      return;
    this._animation.AddClip(anim_clip, name);
    AnimationState animationState = this._animation[name];
    animationState.speed = 0.0f;
    animationState.enabled = false;
    animationState.blendMode = (AnimationBlendMode) 0;
    animationState.wrapMode = (WrapMode) 1;
    animationState.enabled = false;
    animationState.weight = 0.0f;
    if (!TrackedReference.op_Equality((TrackedReference) this.currentAnimState, (TrackedReference) null))
      return;
    this.currentAnimState = animationState;
    animationState.enabled = true;
    animationState.weight = 1f;
  }

  public void ChangeRail(string anim_clip_name, float time, float rate = -1f)
  {
    this.nextAnimState = this._animation[anim_clip_name];
    if (TrackedReference.op_Equality((TrackedReference) this.nextAnimState, (TrackedReference) null) || TrackedReference.op_Equality((TrackedReference) this.nextAnimState, (TrackedReference) this.currentAnimState))
      return;
    this.nextAnimState.weight = 1f - this.currentAnimState.weight;
    this.nextAnimState.enabled = true;
    if ((double) rate >= 0.0)
      this.nextAnimState.time = this.nextAnimState.length * rate;
    this.changeInterp.Set(time, this.currentAnimState.weight, 0.0f, (AnimationCurve) null, 0.0f, (AnimationCurve) null);
    this.changeInterp.Play();
  }

  public bool IsChanging() => this.changeInterp.IsPlaying();
}

// Decompiled with JetBrains decompiler
// Type: EffectCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EffectCtrl : MonoBehaviour
{
  [Header("-- Effect Settings --")]
  [Tooltip("ループエフェクトかどうか")]
  public bool loop;
  [Tooltip("管理対象のParticleSystemの配列")]
  public ParticleSystem[] particles;
  [Tooltip("particlesが空の場合、自動的に検索するかどうか")]
  public bool autoCollectParticles = true;
  [Tooltip("管理対象のAnimator\n空の場合はこのGameObjectにアタッチされたAnimatorが使用される")]
  public Animator animator;
  [Header("-- Loop End Behaviour --")]
  [Tooltip("ループを抜ける時にパーティクルを停止するかどうか")]
  public bool stopParticle = true;
  [Tooltip("ループを抜ける時にAnimatorのENDを再生するかどうか")]
  public bool changeStateToEND = true;
  [Tooltip("AnimatorのENDを再生する時のクロスフェード時間（秒）")]
  public float crossFadeTimeToEND = 0.1f;
  [Header("-- Wait Destroy --")]
  [Tooltip("指定時間を待ってから削除（秒）\n0に設定すると待たない")]
  public float waitTime = 0.2f;
  [Tooltip("パーティクルが全て消えてから削除")]
  public bool waitParticlePlaying = true;
  [Tooltip("アニメが最後まで再生されてから削除")]
  public bool waitAnimationPlaying = true;
  [Header("-- Audio Destroy --")]
  [Tooltip("同時に再生される可能性のあるAudioClip")]
  public AudioClip attachedAudioClip;
  [Tooltip("同時に再生される可能性のあるAudioClipのSE設定ID")]
  public int attachedAudioSettingID = 40000035;
  private Transform _transform;
  private bool loopEnd;
  private float timer;
  private int defaultStateHash;
  private int endStateHash;
  private AudioObject loopAudioObject;
  private AudioClip loopAudioClip;
  private bool isPause;
  private int pauseStateHash;
  private ParticleSystemRenderer[] particleRenders;
  private BetterList<ParticleSystemRenderer> hidePSRenders = new BetterList<ParticleSystemRenderer>();

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    if ((this.particles == null || this.particles.Length == 0) && this.autoCollectParticles)
    {
      this.particles = ((Component) this).GetComponentsInChildren<ParticleSystem>();
      this.particleRenders = ((Component) this).GetComponentsInChildren<ParticleSystemRenderer>();
    }
    if (Object.op_Equality((Object) this.animator, (Object) null))
    {
      Animator component = ((Component) this).GetComponent<Animator>();
      this.animator = !Object.op_Inequality((Object) component, (Object) null) ? (Animator) null : component;
    }
    if (!Object.op_Inequality((Object) this.animator, (Object) null))
      return;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    this.defaultStateHash = ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash;
    int hash = Animator.StringToHash("END");
    if (!this.animator.HasState(0, hash))
      return;
    this.endStateHash = hash;
  }

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    this.__FUNCTION__StopLoopSE();
  }

  private void Update()
  {
    if (this.loop && !this.loopEnd)
      return;
    if ((double) this.waitTime > 0.0)
    {
      this.timer += Time.deltaTime;
      if ((double) this.timer < (double) this.waitTime)
        return;
    }
    if (this.waitParticlePlaying)
    {
      int index = 0;
      for (int length = this.particles.Length; index < length; ++index)
      {
        ParticleSystem particle = this.particles[index];
        if (Object.op_Inequality((Object) particle, (Object) null) && particle.isPlaying)
        {
          particle.Stop(true);
          return;
        }
      }
    }
    if (this.waitAnimationPlaying && Object.op_Inequality((Object) this.animator, (Object) null))
    {
      if (this.animator.IsInTransition(0))
        return;
      AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo).loop && (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
        return;
    }
    this.DestroyGameObject();
  }

  public void EndLoop(bool isPlayEndAnimation = true)
  {
    this.loopEnd = true;
    if (this.stopParticle)
    {
      int index = 0;
      for (int length = this.particles.Length; index < length; ++index)
      {
        ParticleSystem particle = this.particles[index];
        if (Object.op_Inequality((Object) particle, (Object) null))
          particle.Stop(true);
      }
    }
    if (((!this.changeStateToEND || !Object.op_Inequality((Object) this.animator, (Object) null) ? 0 : (this.endStateHash != 0 ? 1 : 0)) & (isPlayEndAnimation ? 1 : 0)) == 0)
      return;
    if ((double) this.crossFadeTimeToEND > 0.0)
      this.animator.CrossFade(this.endStateHash, this.crossFadeTimeToEND, 0);
    else
      this.animator.Play(this.endStateHash, 0, 0.0f);
  }

  public void Play(string stateName) => this.Play(Animator.StringToHash(stateName));

  public void Play(int stateNameHash)
  {
    if (Object.op_Equality((Object) this.animator, (Object) null) || !this.animator.HasState(0, stateNameHash))
      return;
    this.animator.Play(stateNameHash);
  }

  public void CrossFade(int stateNameHash, float transitionDuration)
  {
    if (Object.op_Equality((Object) this.animator, (Object) null) || !this.animator.HasState(0, stateNameHash))
      return;
    this.animator.CrossFade(stateNameHash, transitionDuration);
  }

  public bool IsCurrentState(int stateNameHash)
  {
    if (Object.op_Equality((Object) this.animator, (Object) null))
      return false;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    return ((AnimatorStateInfo) ref animatorStateInfo).shortNameHash == stateNameHash;
  }

  public void Pause(bool pause)
  {
    if (this.isPause == pause)
      return;
    if (pause)
    {
      if (!((Component) this).gameObject.activeInHierarchy)
        return;
      if (this.animator != null)
      {
        AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        int shortNameHash = ((AnimatorStateInfo) ref animatorStateInfo).shortNameHash;
        if (shortNameHash == 0)
          return;
        this.pauseStateHash = shortNameHash;
        ((Behaviour) this.animator).enabled = false;
      }
      ((Component) this).gameObject.SetActive(false);
      this.isPause = true;
    }
    else
    {
      ((Component) this).gameObject.SetActive(true);
      if (this.animator != null)
      {
        ((Behaviour) this.animator).enabled = true;
        this.animator.Play(this.pauseStateHash);
        this.pauseStateHash = 0;
      }
      this.isPause = false;
    }
  }

  public bool CheckShow(Bounds bound)
  {
    return !MonoBehaviourSingleton<InGameCameraCuller>.IsValid() || MonoBehaviourSingleton<InGameCameraCuller>.I.IsVisible(bound);
  }

  public void SetRenderQueue(int renderQueue)
  {
    if (this.particles == null)
      return;
    if (this.particles.Length == 0)
    {
      this.particles = ((Component) this).GetComponentsInChildren<ParticleSystem>(true);
      this.particleRenders = ((Component) this).GetComponentsInChildren<ParticleSystemRenderer>(true);
    }
    for (int index = 0; index < this.particles.Length; ++index)
      ((Renderer) ((Component) this.particles[index]).GetComponent<ParticleSystemRenderer>()).sharedMaterial.renderQueue = renderQueue;
  }

  public void Reset()
  {
    this.timer = 0.0f;
    this.loopEnd = false;
    if (this.particles != null)
    {
      int index = 0;
      for (int length = this.particles.Length; index < length; ++index)
      {
        ParticleSystem particle = this.particles[index];
        if (Object.op_Inequality((Object) particle, (Object) null))
        {
          particle.Clear(true);
          particle.Play(true);
        }
      }
    }
    if (Object.op_Inequality((Object) this.animator, (Object) null) && this.defaultStateHash != 0)
    {
      this.animator.Rebind();
      this.animator.Play(this.defaultStateHash, 0, 0.0f);
    }
    this.isPause = false;
    this.pauseStateHash = 0;
  }

  public void DestroyGameObject()
  {
    if (Object.op_Equality((Object) ((Component) this).gameObject, (Object) null) || MonoBehaviourSingleton<EffectManager>.IsValid() && MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(((Component) this).gameObject, false))
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void UpdateCulling()
  {
    if (this.particleRenders == null || !MonoBehaviourSingleton<InGameCameraCuller>.IsValid())
      return;
    int index = 0;
    for (int length = this.particleRenders.Length; index < length; ++index)
    {
      ParticleSystem particle = this.particles[index];
      ParticleSystemRenderer particleRender = this.particleRenders[index];
      if (Object.op_Inequality((Object) particle, (Object) null) && particle.isPlaying && Object.op_Inequality((Object) particleRender, (Object) null))
      {
        if (this.CheckShow(((Renderer) particleRender).bounds))
        {
          if (this.hidePSRenders.Contains(particleRender))
          {
            ((Renderer) particleRender).enabled = true;
            this.hidePSRenders.Remove(particleRender);
          }
        }
        else
        {
          ((Renderer) particleRender).enabled = false;
          this.hidePSRenders.Add(particleRender);
        }
      }
    }
  }

  private void OnDisable()
  {
    if (!this.loopEnd)
      return;
    this.DestroyGameObject();
  }

  private void __FUNCTION__PlayOneShotSE(AudioClip clip)
  {
    SoundManager.PlaySE(clip, false, this._transform);
  }

  private void __FUNCTION__PlayLoopSE(AudioClip clip)
  {
    if (Object.op_Equality((Object) this.loopAudioClip, (Object) clip))
      return;
    if (Object.op_Inequality((Object) this.loopAudioObject, (Object) null))
      this.loopAudioObject.Stop();
    this.loopAudioObject = SoundManager.PlaySE(clip, true, this._transform);
    if (!Object.op_Inequality((Object) this.loopAudioObject, (Object) null))
      return;
    this.loopAudioClip = clip;
  }

  private void __FUNCTION__StopLoopSE()
  {
    if (Object.op_Inequality((Object) this.loopAudioObject, (Object) null))
      this.loopAudioObject.Stop();
    this.loopAudioObject = (AudioObject) null;
    this.loopAudioClip = (AudioClip) null;
  }
}

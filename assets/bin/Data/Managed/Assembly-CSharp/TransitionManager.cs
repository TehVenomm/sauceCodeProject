// Decompiled with JetBrains decompiler
// Type: TransitionManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class TransitionManager : MonoBehaviourSingleton<TransitionManager>
{
  private const float BLACK_FADE_OUT_TIME = 0.15f;
  private const float BLACK_FADE_IN_TIME = 0.15f;
  private const float WHITE_FADE_OUT_TIME = 0.25f;
  private const float WHITE_FADE_IN_TIME = 0.25f;
  private const float LOADING_FADE_OUT_TIME = 0.25f;
  private const float LOADING_FADE_IN_TIME = 0.25f;
  private const float AUTO_EVENT_FADE_OUT_TIME = 0.1f;
  private const float AUTO_EVENT_FADE_IN_TIME = 0.1f;
  private UIPanel faderPanel;
  private UITexture faderTexture;
  private UISprite faderSprite;
  private TweenAlpha faderTweenAlpha;
  private bool isOut;
  private TransitionManager.TYPE currentType;

  public bool isTransing { get; private set; }

  public bool isChanging { get; private set; }

  private IEnumerator Start()
  {
    while (!MonoBehaviourSingleton<UIManager>.IsValid() || MonoBehaviourSingleton<UIManager>.I.isLoading)
      yield return (object) null;
    this.faderPanel = MonoBehaviourSingleton<UIManager>.I.faderPanel;
    this.faderTexture = ((Component) MonoBehaviourSingleton<UIManager>.I.system.GetCtrl((Enum) UIManager.SYSTEM.FADER)).GetComponent<UITexture>();
    this.faderSprite = ((Component) MonoBehaviourSingleton<UIManager>.I.system.GetCtrl((Enum) UIManager.SYSTEM.FADER)).GetComponent<UISprite>();
    this.faderTweenAlpha = ((Component) MonoBehaviourSingleton<UIManager>.I.system.GetCtrl((Enum) UIManager.SYSTEM.FADER)).GetComponent<TweenAlpha>();
    this.faderTweenAlpha.SetOnFinished(new EventDelegate(new EventDelegate.Callback(this.OnFaderTweenFinised)));
    ((Component) this.faderTweenAlpha).gameObject.SetActive(false);
  }

  private void Update()
  {
  }

  private void OnFaderTweenFinised()
  {
    if (Object.op_Implicit((Object) this.faderTexture))
      this.faderTexture.alpha = this.faderTweenAlpha.to;
    if (Object.op_Implicit((Object) this.faderSprite))
      this.faderSprite.alpha = this.faderTweenAlpha.to;
    this.StartCoroutine(this.DoFaderTweenFinised());
  }

  private IEnumerator DoFaderTweenFinised()
  {
    yield return (object) null;
    this.isChanging = false;
    if (!this.isOut)
    {
      this.faderPanel.depth = 4000;
      ((Component) this.faderTweenAlpha).gameObject.SetActive(false);
      this.OnEnd();
    }
  }

  private void OnEnd()
  {
    this.isTransing = false;
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.TRANSITION, false);
  }

  private void FadeOut(Color color, float time, int depth)
  {
    color.a = 0.0f;
    if (Object.op_Implicit((Object) this.faderTexture))
      this.faderTexture.color = color;
    if (Object.op_Implicit((Object) this.faderSprite))
      this.faderSprite.color = color;
    this.faderPanel.depth = depth;
    this.SetFade(1f, time);
  }

  private void FadeIn(float time) => this.SetFade(0.0f, time);

  private void SetFade(float to, float time)
  {
    this.faderTweenAlpha.from = this.faderTweenAlpha.to;
    this.faderTweenAlpha.to = to;
    ((Component) this.faderTweenAlpha).gameObject.SetActive(true);
    this.faderTweenAlpha.duration = time;
    ((Behaviour) this.faderTweenAlpha).enabled = true;
    this.faderTweenAlpha.ResetToBeginning();
  }

  private void Begin(TransitionManager.TYPE type)
  {
    if (this.isTransing)
    {
      Log.Error("transing now.");
    }
    else
    {
      MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.TRANSITION, true);
      this.isOut = true;
      this.isChanging = true;
      this.isTransing = true;
      this.currentType = type;
      switch (type)
      {
        case TransitionManager.TYPE.BLACK:
          this.FadeOut(Color.black, 0.15f, 4000);
          break;
        case TransitionManager.TYPE.WHITE:
          this.FadeOut(Color.white, 0.25f, 4000);
          break;
        case TransitionManager.TYPE.LOADING:
          MonoBehaviourSingleton<UIManager>.I.loading.ShowTips(true);
          this.FadeOut(Color.black, 0.25f, 4000);
          break;
        case TransitionManager.TYPE.AUTO_EVENT:
          this.FadeOut(Color.black, 0.1f, 7000);
          break;
      }
      MonoBehaviourSingleton<UIManager>.I.loading.ShowRushUI(true);
      MonoBehaviourSingleton<UIManager>.I.loading.ShowArenaUI(true);
    }
  }

  private void End()
  {
    if (this.isChanging)
    {
      Log.Error("changing now.");
    }
    else
    {
      this.isOut = false;
      this.isChanging = true;
      switch (this.currentType)
      {
        case TransitionManager.TYPE.BLACK:
          this.FadeIn(0.15f);
          break;
        case TransitionManager.TYPE.WHITE:
          this.FadeIn(0.25f);
          break;
        case TransitionManager.TYPE.LOADING:
          MonoBehaviourSingleton<UIManager>.I.loading.ShowTips(false);
          if (MonoBehaviourSingleton<UIManager>.I.isShowingGGTutorialMessage)
            MonoBehaviourSingleton<UIManager>.I.HideGGTutorialMessage();
          this.FadeIn(0.25f);
          break;
        case TransitionManager.TYPE.AUTO_EVENT:
          this.FadeIn(0.1f);
          break;
      }
      MonoBehaviourSingleton<UIManager>.I.loading.ShowRushUI(false);
      MonoBehaviourSingleton<UIManager>.I.loading.ShowArenaUI(false);
      if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.TRANSITION_END);
    }
  }

  public Coroutine Out(TransitionManager.TYPE type = TransitionManager.TYPE.BLACK)
  {
    return type == TransitionManager.TYPE.NONE ? (Coroutine) null : this.StartCoroutine(this.DoOut(type));
  }

  private IEnumerator DoOut(TransitionManager.TYPE type)
  {
    while (this.isChanging)
      yield return (object) null;
    if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing)
    {
      MonoBehaviourSingleton<TransitionManager>.I.Begin(type);
      while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
        yield return (object) null;
    }
  }

  public Coroutine In()
  {
    return this.currentType == TransitionManager.TYPE.NONE ? (Coroutine) null : this.StartCoroutine(this.DoIn());
  }

  private IEnumerator DoIn()
  {
    while (this.isChanging)
      yield return (object) null;
    if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
    {
      MonoBehaviourSingleton<TransitionManager>.I.End();
      while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
        yield return (object) null;
    }
  }

  public enum TYPE
  {
    NONE,
    BLACK,
    WHITE,
    LOADING,
    NEW_FILEDOPEN,
    AUTO_EVENT,
  }
}

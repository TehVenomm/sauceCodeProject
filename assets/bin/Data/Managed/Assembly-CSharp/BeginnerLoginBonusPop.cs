// Decompiled with JetBrains decompiler
// Type: BeginnerLoginBonusPop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class BeginnerLoginBonusPop : GameSection
{
  private const string POP_IMAGE_NAME = "BLBP";
  private LoadingQueue loadQueue;
  private BeginnerLoginBonusPop.State currentState;
  private bool stateInitialized;
  private float showTimer;
  private bool skipRequest;

  public override void Initialize()
  {
    this.SetFullScreenButton((Enum) BeginnerLoginBonusPop.UI.BTN_SKIP_FULL_SCREEN);
    this.InitTween((Enum) BeginnerLoginBonusPop.UI.OBJ_IMG_ROOT);
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = this.loadQueue.Load(RESOURCE_CATEGORY.LOGINBONUS_IMAGE, "BLBP");
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (Object.op_Equality(lo_image.loadedObject, (Object) null))
      yield return (object) null;
    ((Component) this.GetCtrl((Enum) BeginnerLoginBonusPop.UI.TEX)).GetComponent<UITexture>().mainTexture = lo_image.loadedObject as Texture;
    base.Initialize();
  }

  private void Update()
  {
    if (this.stateInitialized)
      return;
    switch (this.currentState)
    {
      case BeginnerLoginBonusPop.State.START:
        this.StartCoroutine(this.StartAnimation());
        this.stateInitialized = true;
        break;
      case BeginnerLoginBonusPop.State.SHOW:
        this.showTimer = 0.0f;
        this.StartCoroutine(this.ShowCountdown());
        this.stateInitialized = true;
        break;
      case BeginnerLoginBonusPop.State.END:
        this.StartCoroutine(this.EndAnimation());
        this.stateInitialized = true;
        break;
    }
  }

  private void ChangeState(BeginnerLoginBonusPop.State nextState)
  {
    this.stateInitialized = false;
    this.currentState = nextState;
  }

  private IEnumerator StartAnimation()
  {
    this.SetActive((Enum) BeginnerLoginBonusPop.UI.BTN_SKIP_FULL_SCREEN, false);
    bool wait = true;
    this.PlayAudio((Enum) BeginnerLoginBonusPop.AUDIO.START, 1.3f);
    this.PlayTween((Enum) BeginnerLoginBonusPop.UI.OBJ_IMG_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
      yield return (object) 0;
    this.ChangeState(BeginnerLoginBonusPop.State.SHOW);
  }

  private IEnumerator ShowCountdown()
  {
    bool wait = true;
    Transform skip = this.GetCtrl((Enum) BeginnerLoginBonusPop.UI.BTN_SKIP_FULL_SCREEN);
    while (wait)
    {
      this.showTimer += Time.deltaTime;
      if (1.2000000476837158 < (double) this.showTimer && !((Component) skip).gameObject.activeSelf)
        this.SetActive((Enum) BeginnerLoginBonusPop.UI.BTN_SKIP_FULL_SCREEN, true);
      if (this.skipRequest && 1.2000000476837158 < (double) this.showTimer)
        wait = false;
      yield return (object) 0;
    }
    this.ChangeState(BeginnerLoginBonusPop.State.END);
  }

  private IEnumerator EndAnimation()
  {
    bool wait = true;
    this.PlayTween((Enum) BeginnerLoginBonusPop.UI.OBJ_IMG_ROOT, false, (EventDelegate.Callback) (() => wait = false));
    while (wait)
      yield return (object) 0;
    GameSection.BackSection();
  }

  private void OnQuery_SKIP() => this.skipRequest = true;

  private enum UI
  {
    OBJ_IMG_ROOT,
    TEX,
    BTN_SKIP_FULL_SCREEN,
  }

  private enum AUDIO
  {
    START = 40000388, // 0x02625B84
  }

  private enum State
  {
    START,
    SHOW,
    END,
  }
}

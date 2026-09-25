// Decompiled with JetBrains decompiler
// Type: HomeCountdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeCountdown : GameSection
{
  private bool ready;
  private LoadingQueue loadQueue;
  private int showID;
  private HomeCountdown.State currentState;
  private bool stateInitialized;
  private float showTimer;
  private bool skipRequest;

  public override void Initialize()
  {
    this.ready = false;
    this.showID = (int) GameSection.GetEventData();
    PlayerPrefs.SetInt("COUNTDOWN_SHOWED_REMAIN", this.showID);
    this.SetFullScreenButton((Enum) HomeCountdown.UI.BTN_SKIP_FULL_SCREEN);
    this.InitTween((Enum) HomeCountdown.UI.OBJ_COUNTDOWN_ROOT);
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = this.loadQueue.Load(RESOURCE_CATEGORY.COUNTDOWN_IMAGE, ResourceName.GetCountdownImage(this.showID));
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (Object.op_Equality(lo_image.loadedObject, (Object) null))
      yield return (object) null;
    ((Component) this.GetCtrl((Enum) HomeCountdown.UI.TEX_COUNTDOWN)).GetComponent<UITexture>().mainTexture = lo_image.loadedObject as Texture;
    this.ready = true;
    base.Initialize();
  }

  private void Update()
  {
    if (this.stateInitialized)
      return;
    switch (this.currentState)
    {
      case HomeCountdown.State.START:
        this.StartCoroutine(this.StartAnimation());
        this.stateInitialized = true;
        break;
      case HomeCountdown.State.SHOW:
        this.showTimer = 0.0f;
        this.StartCoroutine(this.ShowCountdown());
        this.stateInitialized = true;
        break;
      case HomeCountdown.State.END:
        this.StartCoroutine(this.EndAnimation());
        this.stateInitialized = true;
        break;
    }
  }

  private void ChangeState(HomeCountdown.State nextState)
  {
    this.stateInitialized = false;
    this.currentState = nextState;
  }

  private IEnumerator StartAnimation()
  {
    this.SetActive((Enum) HomeCountdown.UI.BTN_SKIP_FULL_SCREEN, false);
    bool wait = true;
    this.PlayAudio((Enum) HomeCountdown.AUDIO.START, 1.3f);
    this.PlayTween((Enum) HomeCountdown.UI.OBJ_COUNTDOWN_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
      yield return (object) 0;
    this.ChangeState(HomeCountdown.State.SHOW);
  }

  private IEnumerator ShowCountdown()
  {
    bool wait = true;
    Transform skip = this.GetCtrl((Enum) HomeCountdown.UI.BTN_SKIP_FULL_SCREEN);
    while (wait)
    {
      this.showTimer += Time.deltaTime;
      if (1.2000000476837158 < (double) this.showTimer && !((Component) skip).gameObject.activeSelf)
        this.SetActive((Enum) HomeCountdown.UI.BTN_SKIP_FULL_SCREEN, true);
      if (this.skipRequest && 1.2000000476837158 < (double) this.showTimer)
        wait = false;
      yield return (object) 0;
    }
    this.ChangeState(HomeCountdown.State.END);
  }

  private IEnumerator EndAnimation()
  {
    bool wait = true;
    this.PlayTween((Enum) HomeCountdown.UI.OBJ_COUNTDOWN_ROOT, false, (EventDelegate.Callback) (() => wait = false));
    while (wait)
      yield return (object) 0;
    this.DispatchEvent("BACK");
  }

  private void OnQuery_SKIP() => this.skipRequest = true;

  private enum UI
  {
    OBJ_COUNTDOWN_ROOT,
    TEX_COUNTDOWN,
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

// Decompiled with JetBrains decompiler
// Type: QuestResultDirection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultDirection : GameSection
{
  private QuestResultDirector director;
  private PlayerLoader[] players;
  private int winnder_voice_id;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
    yield return (object) new WaitForEndOfFrame();
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.players.Count > 0)
    {
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.UI, "QuestResultDirector");
      List<InGameRecorder.PlayerRecord> players = MonoBehaviourSingleton<InGameRecorder>.I.players;
      int index1 = 0;
      while (index1 < players.Count)
      {
        InGameRecorder.PlayerRecord playerRecord = players[index1];
        if (playerRecord == null || playerRecord.playerLoadInfo == null)
          players.RemoveAt(index1);
        else
          ++index1;
      }
      bool waitLoad = true;
      MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModelsAsync((Action<PlayerLoader[]>) (loaders =>
      {
        this.players = loaders;
        waitLoad = false;
      }));
      while (waitLoad)
        yield return (object) null;
      this.winnder_voice_id = 0;
      if (this.players != null)
      {
        this.winnder_voice_id = this.players[0].GetVoiceId(ACTION_VOICE_ID.HAPPY_01);
        load_queue.CacheActionVoice(this.winnder_voice_id);
      }
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      int index2 = 0;
      for (int length = this.players.Length; index2 < length; ++index2)
        this.players[index2].animator.applyRootMotion = false;
      this.director = ((Component) ResourceUtility.Realizes(lo.loadedObject, MonoBehaviourSingleton<StageManager>.I._transform)).GetComponent<QuestResultDirector>();
      this.director.players = this.players;
      load_queue = (LoadingQueue) null;
      lo = (LoadObject) null;
    }
    if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      MonoBehaviourSingleton<SceneSettingsManager>.I.DisableWaveTarget();
    GC.Collect();
    yield return (object) new WaitForEndOfFrame();
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
    if (QuestManager.IsValidTrial() && MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
    {
      MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
      MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
    }
    base.Initialize();
  }

  private void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.director, (Object) null) || !((Behaviour) this.director).enabled || !Object.op_Inequality((Object) this.director.targetAnim, (Object) null) || this.director.targetAnim.isPlaying)
      return;
    this.OnDirectionFinished();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (Object.op_Inequality((Object) this.director, (Object) null))
      Object.Destroy((Object) ((Component) this.director).gameObject);
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    MonoBehaviourSingleton<InGameRecorder>.I.DeletePlayerModels();
  }

  public override void StartSection()
  {
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.players.Count != 0)
      return;
    this.OnDirectionFinished();
  }

  private void OnDirectionFinished()
  {
    if (this.winnder_voice_id > 0)
      SoundManager.PlayActionVoice(this.winnder_voice_id);
    ((Behaviour) this.director).enabled = false;
    if (QuestManager.IsValidTrial())
      this.DispatchEvent("NEXT_TRIAL");
    else
      this.DispatchEvent("NEXT");
  }

  private void OnQuery_NEXT()
  {
    if (!((Behaviour) this.director).enabled)
      return;
    this.director.Skip();
    GameSection.StopEvent();
  }

  private void OnQuery_NEXT_TRIAL()
  {
    if (!((Behaviour) this.director).enabled)
      return;
    this.director.Skip();
    GameSection.StopEvent();
  }

  public override void UpdateUI()
  {
  }
}

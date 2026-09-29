// Decompiled with JetBrains decompiler
// Type: SmithPerformanceBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class SmithPerformanceBase : GameSection
{
  private object resultData;
  protected SmithEquipDirector director;

  public override void Initialize()
  {
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(false);
    this.resultData = GameSection.GetEventData();
    this.SetToggle((Enum) SmithPerformanceBase.UI.TGL_DIRECTION, true);
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_direction = loadingQueue.Load(RESOURCE_CATEGORY.UI, "SmithEquipDirection");
    int wait = 0;
    ++wait;
    int npc_id = StatusManager.IsUnique() ? 36 : 4;
    NPCTable.NPCData npcData1 = Singleton<NPCTable>.I.GetNPCData(npc_id);
    GameObject npcRoot004 = new GameObject("NPC");
    GameObject go1 = npcRoot004;
    Action<Animator> on_complete1 = (Action<Animator>) (animator => --wait);
    npcData1.LoadModel(go1, false, true, on_complete1, false);
    GameObject npcRoot003 = (GameObject) null;
    if ((this is SmithAbilityChangePerformance ? 1 : (this is SmithAbilityItemPerformance ? 1 : 0)) != 0)
    {
      ++wait;
      NPCTable.NPCData npcData2 = Singleton<NPCTable>.I.GetNPCData(3);
      npcRoot003 = new GameObject("NPC003");
      GameObject go2 = npcRoot003;
      Action<Animator> on_complete2 = (Action<Animator>) (animator => --wait);
      npcData2.LoadModel(go2, false, true, on_complete2, false);
    }
    foreach (int se_id in (int[]) Enum.GetValues(typeof (SmithEquipDirector.AUDIO)))
      loadingQueue.CacheSE(se_id);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (EquipResultBase.AUDIO)))
      loadingQueue.CacheSE(se_id);
    yield return (object) loadingQueue.Wait();
    while (wait > 0)
      yield return (object) null;
    this.director = ((Component) ResourceUtility.Realizes(lo_direction.loadedObject, MonoBehaviourSingleton<StageManager>.I.stageObject)).GetComponent<SmithEquipDirector>();
    this.director.SetNPC004(npcRoot004);
    this.director.SetNPC003(npcRoot003);
    base.Initialize();
  }

  public override void UpdateUI() => base.UpdateUI();

  public override void Exit()
  {
    base.Exit();
    if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
      return;
    MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(true);
  }

  protected void OnQuery_SKIP()
  {
    if (this.director.isPlaying)
      this.director.Skip();
    GameSection.SetEventData(this.resultData);
  }

  protected virtual void OnEndDirection() => this.StartCoroutine(this.DoEnd());

  protected void EndDirectionUI()
  {
    this.SetToggle((Enum) SmithPerformanceBase.UI.TGL_DIRECTION, false);
  }

  private IEnumerator DoEnd()
  {
    yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetEnableSmithCharacterActivate(true);
    this.EndDirectionUI();
    this.DispatchEvent("SKIP");
    if (Object.op_Implicit((Object) this.director))
    {
      this.director.Reset();
      Object.Destroy((Object) ((Component) this.director).gameObject);
    }
  }

  protected override void OnDestroy()
  {
    if (Object.op_Implicit((Object) this.director))
    {
      this.director.Reset();
      Object.Destroy((Object) ((Component) this.director).gameObject);
    }
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(true);
    base.OnDestroy();
  }

  private enum UI
  {
    TGL_DIRECTION,
  }
}

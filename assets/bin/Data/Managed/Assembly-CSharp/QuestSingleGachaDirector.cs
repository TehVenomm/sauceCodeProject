// Decompiled with JetBrains decompiler
// Type: QuestSingleGachaDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class QuestSingleGachaDirector : QuestGachaDirectorBase
{
  public Transform enemyPosition;

  protected override IEnumerator GetDirectionCoroutine() => this.DoQuestGacha();

  private IEnumerator DoQuestGacha()
  {
    this.Init();
    int display_rarity = 0;
    this.SetLinkCamera(true);
    int id = 0;
    EnemyTable.EnemyData enemy_data = (EnemyTable.EnemyData) null;
    if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null)
    {
      uint itemId = (uint) MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[0].itemId;
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(itemId);
      if (questData != null)
        enemy_data = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    }
    if (enemy_data == null)
    {
      if (id == 0)
        id = 1101001;
      enemy_data = Singleton<EnemyTable>.I.GetEnemyData((uint) id);
    }
    NPCLoader npc_loader = this.LoadNPC();
    EnemyLoader enemy_loader = (EnemyLoader) null;
    if (enemy_data != null)
    {
      int animId = enemy_data.animId;
      OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_data);
      int modelId = enemy_data.modelId;
      float displayScale = enemy_data.modelScale;
      if (enemyDisplayInfo != null)
      {
        animId = enemyDisplayInfo.animID;
        displayScale = enemyDisplayInfo.gachaScale;
      }
      enemy_loader = this.LoadEnemy(this.enemyPosition, modelId, animId, displayScale, enemy_data.baseEffectName, enemy_data.baseEffectNode);
      while (enemy_loader.isLoading)
        yield return (object) null;
      enemy_loader.ApplyGachaDisplayScaleToParentNode();
      this.CheckAndReplaceShader(enemy_loader);
      ((Component) enemy_loader).gameObject.SetActive(false);
    }
    while (npc_loader.isLoading)
      yield return (object) null;
    LoadingQueue lo_queue = new LoadingQueue((MonoBehaviour) this);
    this.CacheAudio(lo_queue);
    if (enemy_data != null)
      this.CacheEnemyAudio(enemy_data, lo_queue);
    while (lo_queue.IsLoading())
      yield return (object) null;
    PlayerAnimCtrl npc_anim = PlayerAnimCtrl.Get(npc_loader.animator, PLCA.IDLE_01);
    this.CreateNPCEffect(npc_loader.model);
    yield return (object) null;
    this.stageAnimator.cullingMode = (AnimatorCullingMode) 0;
    this.stageAnimator.Rebind();
    this.targetRarity = MonoBehaviourSingleton<GachaManager>.I.GetMaxRarity().ToRarityExpressionID() + 1;
    if (this.targetRarity > 4)
      this.targetRarity = 4;
    this.stageAnimator.Play("StageAnim_Main");
    this.Play("MainAnim_Start");
    this.PlayEffect(this.startEffectPrefabs[0]);
    npc_anim.Play(PLCA.QUEST_GACHA, true);
    bool rankup = this.UpdateDisplayRarity(ref display_rarity);
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.OPENING_01);
    while (this.Step(0.5f))
      yield return (object) null;
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.OPENING_02);
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.OPENING_03);
    while (this.Step(1.4f))
      yield return (object) null;
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.OPENING_04);
    while (this.Step(2.6f))
      yield return (object) null;
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.DOOR_01);
    while (this.Step(3.2f))
      yield return (object) null;
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.DOOR_02);
    while (this.Step(3.38f))
      yield return (object) null;
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.MAGI_INTRO_01);
    while (this.Step(6.5f))
      yield return (object) null;
    ((Component) npc_loader).gameObject.SetActive(false);
    this.PlayMeteorEffect(display_rarity);
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.METEOR_01);
    while (this.Step(7.5f))
      yield return (object) null;
    this.PlayMagicEffect(display_rarity, rankup);
    this.PlayMagicAudio(display_rarity);
    rankup = this.UpdateDisplayRarity(ref display_rarity);
    this.PlayMeteorEffect(display_rarity);
    while (this.Step(8.5f))
      yield return (object) null;
    this.PlayMagicEffect(display_rarity, rankup);
    this.PlayMagicAudio(display_rarity);
    rankup = this.UpdateDisplayRarity(ref display_rarity);
    this.PlayMeteorEffect(display_rarity);
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.METEOR_02);
    while (this.Step(9.5f))
      yield return (object) null;
    this.PlayMagicEffect(display_rarity, rankup);
    this.PlayMagicAudio(display_rarity);
    while (this.Step(10.5f))
      yield return (object) null;
    if (Object.op_Inequality((Object) enemy_loader, (Object) null))
      ((Component) enemy_loader).gameObject.SetActive(true);
    if (!this.skip)
      this.PlayEnemyAnimation(enemy_loader, "Base Layer.GACHA_SINGLE");
    this.Play("MainAnim_End");
    rankup = this.UpdateDisplayRarity(ref display_rarity);
    this.PlayEndEffect(display_rarity);
    this.PlayAppearAudio(MonoBehaviourSingleton<GachaManager>.I.GetMaxRarity(), false);
    if (enemy_data != null)
      this.PlayEnemyAudio(enemy_data);
    while (this.Step(11.5f))
      yield return (object) null;
    while (this.Step(13f))
      yield return (object) null;
    if (this.skip)
    {
      while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
        yield return (object) null;
      this.PlayEnemyAnimation(enemy_loader, "Base Layer.IDLE");
      Time.timeScale = 1f;
      if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
        yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
    }
    else
      this.skip = true;
    Time.timeScale = 1f;
    this.sectionCommandReceiver.OnEnd();
  }
}

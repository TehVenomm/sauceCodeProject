// Decompiled with JetBrains decompiler
// Type: QuestFeverGachaLegacyDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestFeverGachaLegacyDirector : QuestGachaDirectorBase
{
  public const string ENEMY_MOTION_BASE_LAYER = "Base Layer.";
  public const string ENEMY_MOTION_STATE_IDLE = "Base Layer.IDLE";
  public const string ENEMY_MOTION_STATE_GACHA = "Base Layer.GACHA_SINGLE";
  public const string ENEMY_MOTION_STATE_GACHA11 = "Base Layer.GACHA_11";
  public const float MAGIC_CIRCLE_EFFECT_FINISH_TIME = 13.5f;
  public const int DEFAULT_REAM_MAX = 11;
  public Transform[] magicCircles;
  public Transform[] enemyPositions;
  public float[] meteorTimings;
  public float[] magicTimings;
  public float[] meteorSETimings;
  public float[] magicSETimings;
  public float showRarityWaitTime = 1f;
  public float lastShowRarityWaitTime = 2.5f;
  private bool m_isSkipAll;

  protected override IEnumerator GetDirectionCoroutine() => this.DoQuestGacha();

  private IEnumerator DoQuestGacha()
  {
    this.m_isSkipAll = false;
    this.Init();
    this.SetLinkCamera(true);
    GachaResult gachaResultBonus = MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus;
    int reamMax = gachaResultBonus != null ? gachaResultBonus.reward.Count : 9;
    int[] idxList = new int[reamMax];
    for (int index = 0; index < reamMax - 1; ++index)
      idxList[index] = index;
    idxList[reamMax - 1] = 10;
    EnemyTable.EnemyData[] enemy_datas = new EnemyTable.EnemyData[reamMax];
    QuestTable.QuestTableData[] quest_datas = new QuestTable.QuestTableData[reamMax];
    EnemyLoader[] enemyLoaderList = new EnemyLoader[reamMax];
    if (MonoBehaviourSingleton<GachaManager>.IsValid() && gachaResultBonus != null)
    {
      int count = gachaResultBonus.reward.Count;
      int index1 = 0;
      for (int index2 = count; index1 < index2; ++index1)
      {
        GachaResult.GachaReward gachaReward = gachaResultBonus.reward[index1];
        uint itemId = (uint) gachaReward.itemId;
        quest_datas[index1] = Singleton<QuestTable>.I.GetQuestData(itemId);
        if (quest_datas[index1] != null)
        {
          enemy_datas[index1] = Singleton<EnemyTable>.I.GetEnemyData((uint) quest_datas[index1].GetMainEnemyID());
          if (enemy_datas[index1] == null)
            Log.Error("EnemyTable[{0}] == null", (object) quest_datas[index1].GetMainEnemyID());
        }
        else
        {
          Log.Error("QuestTable[{0}] == null", (object) gachaReward.itemId);
          quest_datas[index1] = new QuestTable.QuestTableData();
          enemy_datas[index1] = new EnemyTable.EnemyData();
        }
      }
    }
    NPCLoader npc_loader = this.LoadNPC();
    npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, false, true, SHADER_TYPE.NORMAL, (System.Action) null);
    int index3 = 0;
    for (int index4 = reamMax; index3 < index4; ++index3)
    {
      float displayScale = enemy_datas[index3].modelScale;
      int animId = enemy_datas[index3].animId;
      int modelId = enemy_datas[index3].modelId;
      OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_datas[index3]);
      if (enemyDisplayInfo != null)
      {
        animId = enemyDisplayInfo.animID;
        displayScale = enemyDisplayInfo.gachaScale;
      }
      enemyLoaderList[index3] = this.LoadEnemy(this.enemyPositions[idxList[index3]], modelId, animId, displayScale, enemy_datas[index3].baseEffectName, enemy_datas[index3].baseEffectNode);
    }
    int i = 0;
    int n;
    for (n = reamMax; i < n; ++i)
    {
      while (enemyLoaderList[i].isLoading)
        yield return (object) null;
      enemyLoaderList[i].ApplyGachaDisplayScaleToParentNode();
      this.CheckAndReplaceShader(enemyLoaderList[i]);
      ((Component) enemyLoaderList[i]).gameObject.SetActive(false);
    }
    LoadingQueue lo_queue = new LoadingQueue((MonoBehaviour) this);
    this.CacheAudio(lo_queue);
    for (int index5 = 0; index5 < reamMax; ++index5)
      this.CacheEnemyAudio(enemy_datas[index5], lo_queue);
    while (npc_loader.isLoading)
      yield return (object) null;
    while (lo_queue.IsLoading())
      yield return (object) null;
    PlayerAnimCtrl npc_anim = PlayerAnimCtrl.Get(npc_loader.animator, PLCA.IDLE_01);
    this.CreateNPCEffect(npc_loader.model);
    yield return (object) null;
    this.stageAnimator.cullingMode = (AnimatorCullingMode) 0;
    this.stageAnimator.Rebind();
    this.stageAnimator.Play("StageAnim_Main");
    this.Play("MainAnim_Start");
    this.PlayEffect(this.startEffectPrefabs[0]);
    npc_anim.Play(PLCA.QUEST_GACHA, true);
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
    while (this.Step(5.1f))
      yield return (object) null;
    this.PlayAudio(QuestGachaDirectorBase.AUDIO.MAGI_INTRO_02);
    while (this.Step(5.5f))
      yield return (object) null;
    this.PlayEffect(this.startEffectPrefabs[1]);
    Transform[] magic_effects = new Transform[reamMax];
    Transform[] end_effects = new Transform[reamMax];
    n = 0;
    i = 0;
    this.time -= Time.deltaTime;
    while (n < reamMax || i < reamMax)
    {
      this.time += Time.deltaTime;
      if (n < reamMax && (double) this.meteorTimings[idxList[n]] <= (double) this.time)
      {
        this.PlayEffect(this.magicCircles[idxList[n]], this.meteorEffectPrefabs[quest_datas[n].rarity.ToRarityExpressionID()]);
        if (n == reamMax - 1)
          this.PlayAudio(QuestGachaDirectorBase.AUDIO.METEOR_01);
        ++n;
      }
      if (i < reamMax && (double) this.magicTimings[idxList[i]] <= (double) this.time)
      {
        magic_effects[i] = this.PlayEffect(this.magicCircles[idxList[i]], this.magicEffectPrefabs[quest_datas[i].rarity.ToRarityExpressionID()]);
        this.PlayMagicAudio((int) quest_datas[i].rarity);
        ++i;
      }
      yield return (object) null;
    }
    int index6 = i - 1;
    if (index6 < reamMax && index6 >= 0)
      this.PlayMagicAudio((int) quest_datas[index6].rarity);
    while (this.Step(13.5f))
      yield return (object) null;
    if (this.skip && !this.m_isSkipAll)
    {
      if (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
        yield return (object) null;
      Time.timeScale = 1f;
      this.skip = false;
      this.time = 13.5f;
      yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
      this.sectionCommandReceiver.ActivateSkipButton();
    }
    this.ActivateFirstSkipFlag();
    i = 0;
    float end_time = 13.5f;
    float end_step_time = 1.5f;
    float end_step_time_last = 2f;
    RARITY_TYPE rarity_type;
    while (true)
    {
      this.sectionCommandReceiver.OnHideRarity();
      this.PlayAudio(QuestGachaDirectorBase.AUDIO.RARITY_EXPOSITION);
      if (i > 0)
      {
        int index7 = i - 1;
        if (Object.op_Inequality((Object) enemyLoaderList[index7], (Object) null))
          ((Component) enemyLoaderList[index7]).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) magic_effects[index7], (Object) null))
        {
          Object.Destroy((Object) ((Component) magic_effects[index7]).gameObject);
          magic_effects[index7] = (Transform) null;
        }
        if (Object.op_Inequality((Object) end_effects[index7], (Object) null))
        {
          Object.Destroy((Object) ((Component) end_effects[index7]).gameObject);
          end_effects[index7] = (Transform) null;
        }
      }
      EnemyLoader enemyLoader = enemyLoaderList[i];
      ((Component) enemyLoader).gameObject.SetActive(true);
      if (!this.skip)
      {
        string animStateName = "Base Layer.GACHA_11";
        if (i == enemyLoaderList.Length - 1)
          animStateName = "Base Layer.GACHA_SINGLE";
        this.PlayEnemyAnimation(enemyLoader, animStateName);
      }
      rarity_type = quest_datas[i].rarity;
      int index8 = rarity_type.ToRarityExpressionID();
      if (rarity_type == RARITY_TYPE.SS)
        index8 = 3;
      end_effects[i] = this.PlayEffect(this.magicCircles[idxList[i]], this.endEffectPrefabs[index8]);
      this.Play($"MainAnim_End_{idxList[i] + 1:D2}");
      ++i;
      bool is_short = i < reamMax;
      this.PlayAppearAudio(rarity_type, is_short);
      if (enemy_datas.Length >= i)
        this.PlayEnemyAudio(enemy_datas[i - 1], is_short);
      if (i < reamMax)
      {
        float waitTime = 0.0f;
        while ((double) waitTime < (double) this.showRarityWaitTime)
        {
          waitTime += Time.deltaTime;
          if (this.IsSkipAppearEnemy)
            waitTime = this.showRarityWaitTime;
          yield return (object) null;
        }
        if (!this.IsSkipAppearEnemy)
          this.sectionCommandReceiver.OnShowRarity(rarity_type);
        end_time += end_step_time;
        while (this.Step(end_time))
        {
          if (this.IsSkipAppearEnemy)
            this.time = end_time;
          yield return (object) null;
        }
        this.sectionCommandReceiver.ActivateSkipButton();
        this.ResetSkipAppearEnemyFlag();
      }
      else
        break;
    }
    yield return (object) new WaitForSeconds(this.lastShowRarityWaitTime);
    this.sectionCommandReceiver.OnShowRarity(rarity_type);
    end_time += end_step_time_last;
    while (this.Step(end_time))
      yield return (object) null;
    if (this.skip)
    {
      while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
        yield return (object) null;
      int index9 = enemyLoaderList.Length - 1;
      if (index9 >= 0)
        this.PlayEnemyAnimation(enemyLoaderList[index9], "Base Layer.IDLE");
      Time.timeScale = 1f;
      if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
        yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
    }
    else
      this.skip = true;
    this.sectionCommandReceiver.OnHideRarity();
    Time.timeScale = 1f;
    this.sectionCommandReceiver.OnEnd();
  }

  public override void SkipAll()
  {
    if (this.m_isSkipAll || this.skip)
      return;
    this.m_isSkipAll = true;
    this.skip = true;
    this.StartCoroutine(this.DoSkipAll());
  }

  private IEnumerator DoSkipAll()
  {
    yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out();
    Time.timeScale = 100f;
  }
}

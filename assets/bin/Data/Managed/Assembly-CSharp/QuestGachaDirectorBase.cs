// Decompiled with JetBrains decompiler
// Type: QuestGachaDirectorBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestGachaDirectorBase : AnimationDirector
{
  public const float DEFAULT_MODEL_LOAD_SCALE = 1f;
  public Animator stageAnimator;
  public Transform npcPosition;
  public GameObject npcEffect;
  public GameObject[] startEffectPrefabs;
  public GameObject[] meteorEffectPrefabs;
  public GameObject[] magicEffectPrefabs;
  public GameObject[] endEffectPrefabs;
  public GameObject[] demoEffectPrefabs;
  public GameObject[] uiRarityEffectPrefabs;
  protected QuestGachaDirectorBase.ISectionCommand sectionCommandReceiver;
  protected IEnumerator coroutine;
  protected float time;
  protected int targetRarity;
  protected List<Transform> effects = new List<Transform>();
  protected List<Component> objects = new List<Component>();
  protected Transform magicEffect;
  private IEnumerator m_skipCoroutine;
  private bool m_isFirstSkipped;
  private bool m_isSkipAppearEnemy;

  private void Start() => this.Init();

  protected void Init()
  {
    this.skip = false;
    this.m_isFirstSkipped = false;
    this.m_isSkipAppearEnemy = false;
    this.stageAnimator.Play("StageAnim_Init");
    this.Play("MainAnim_Init");
    this.time = 0.0f;
    this.Delete();
  }

  protected void Delete()
  {
    int index = 0;
    for (int count = this.objects.Count; index < count; ++index)
    {
      if (Object.op_Inequality((Object) this.objects[index], (Object) null))
      {
        Object.Destroy((Object) this.objects[index].gameObject);
        this.objects[index] = (Component) null;
      }
    }
    this.objects.Clear();
    this.effects.ForEach((Action<Transform>) (o =>
    {
      if (!Object.op_Inequality((Object) o, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) o).gameObject);
    }));
    this.effects.Clear();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    this.Delete();
  }

  public void StartDirection(
    QuestGachaDirectorBase.ISectionCommand command_receiver)
  {
    this.sectionCommandReceiver = command_receiver;
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    this.StartCoroutine(this.coroutine = this.GetDirectionCoroutine());
  }

  protected virtual IEnumerator GetDirectionCoroutine() => (IEnumerator) null;

  protected NPCLoader LoadNPC()
  {
    NPCLoader npcLoader = ((Component) Utility.CreateGameObject("NPC", this.npcPosition)).gameObject.AddComponent<NPCLoader>();
    npcLoader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, false, true, SHADER_TYPE.NORMAL, (System.Action) null);
    this.objects.Add((Component) npcLoader);
    return npcLoader;
  }

  protected void CreateNPCEffect(Transform parent)
  {
    Transform parent1 = Utility.Find(parent, "R_Finger02b");
    if (!Object.op_Inequality((Object) parent1, (Object) null))
      return;
    this.PlayEffect(parent1, this.npcEffect);
  }

  protected EnemyLoader LoadEnemy(
    Transform parent,
    int model_id,
    int anim_id,
    float displayScale,
    string base_effect,
    string base_effect_node)
  {
    EnemyLoader enemyLoader = ((Component) Utility.CreateGameObject("Enemy", parent)).gameObject.AddComponent<EnemyLoader>();
    enemyLoader.StartLoad(model_id, anim_id, 1f, base_effect, base_effect_node, false, true, false, SHADER_TYPE.NORMAL);
    enemyLoader.DisplayGachaScale = displayScale;
    this.objects.Add((Component) enemyLoader);
    return enemyLoader;
  }

  protected bool Step(float sec)
  {
    this.time += Time.deltaTime;
    return (double) this.time < (double) sec;
  }

  protected void PlayMeteorEffect(int rarity)
  {
    if (rarity > 3)
      rarity = 3;
    this.PlayEffect(this.meteorEffectPrefabs[rarity - 1]);
  }

  protected void PlayMagicEffect(int rarity, bool rankup)
  {
    if (rarity > 3)
      rarity = 3;
    if (rankup)
    {
      this.DeleteMagicEffect();
      this.magicEffect = this.PlayEffect(this.magicEffectPrefabs[rarity - 1]);
    }
    this.Play("MainAnim_MagicAngle0" + (object) rarity);
  }

  protected void DeleteMagicEffect()
  {
    if (!Object.op_Inequality((Object) this.magicEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(ref this.magicEffect);
  }

  protected void PlayEndEffect(int rarity)
  {
    if (rarity > 4)
      rarity = 4;
    this.PlayEffect(this.endEffectPrefabs[rarity - 1]);
  }

  protected Transform PlayEffect(GameObject prefab)
  {
    return this.PlayEffect(MonoBehaviourSingleton<StageManager>.I._transform, prefab);
  }

  protected Transform PlayEffect(Transform parent, GameObject prefab)
  {
    Transform transform = ResourceUtility.Realizes((Object) prefab, parent);
    this.effects.Add(transform);
    rymFX component = ((Component) transform).GetComponent<rymFX>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.Cameras = new Camera[1]
      {
        MonoBehaviourSingleton<AppMain>.I.mainCamera
      };
    return transform;
  }

  protected bool UpdateDisplayRarity(ref int rarity)
  {
    if (rarity >= this.targetRarity)
      return false;
    ++rarity;
    return true;
  }

  public override void Skip()
  {
    if (this.skip)
      return;
    if (this.m_isFirstSkipped)
    {
      this.m_isSkipAppearEnemy = true;
    }
    else
    {
      this.ActivateFirstSkipFlag();
      base.Skip();
      if (this.m_skipCoroutine != null)
        this.StopCoroutine(this.m_skipCoroutine);
      this.m_skipCoroutine = this.DoSkip();
      this.StartCoroutine(this.m_skipCoroutine);
    }
  }

  private IEnumerator DoSkip()
  {
    yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out();
    Time.timeScale = 100f;
  }

  protected void ActivateFirstSkipFlag() => this.m_isFirstSkipped = true;

  protected void ResetSkipAppearEnemyFlag() => this.m_isSkipAppearEnemy = false;

  protected bool IsSkipAppearEnemy => this.m_isSkipAppearEnemy;

  public void PlayUIRarityEffect(
    RARITY_TYPE rarity,
    Transform effect_parent_ui,
    Transform effect_target_ui)
  {
    this.PlayRarityAudio(rarity);
    GameObject gameObject = (GameObject) null;
    int rarityExpressionId2 = rarity.ToRarityExpressionID2();
    if (rarityExpressionId2 > 0)
      gameObject = this.uiRarityEffectPrefabs[rarityExpressionId2 - 1];
    if (Object.op_Equality((Object) gameObject, (Object) null))
      return;
    UIWidget componentInChildren = ((Component) effect_target_ui).GetComponentInChildren<UIWidget>();
    Transform effect = ResourceUtility.Realizes((Object) gameObject, effect_parent_ui, 5);
    effect.position = effect_parent_ui.position;
    EffectManager.SetUIEffectDepth(effect, effect_parent_ui, add_render_queue: 10, ref_render_queue: componentInChildren);
    this.effects.Add(effect);
  }

  protected void CacheAudio(LoadingQueue lo_queue)
  {
    foreach (int se_id in (int[]) Enum.GetValues(typeof (QuestGachaDirectorBase.AUDIO)))
      lo_queue.CacheSE(se_id);
  }

  protected void PlayAudio(QuestGachaDirectorBase.AUDIO audio) => this.PlayAudio(audio, false);

  protected void PlayAudio(QuestGachaDirectorBase.AUDIO audio, bool ignore_skip)
  {
    if (this.skip && !ignore_skip)
      return;
    SoundManager.PlayOneShotUISE((int) audio);
  }

  protected void PlayMagicAudio(int display_rarity)
  {
    if (display_rarity > 2)
      this.PlayAudio(QuestGachaDirectorBase.AUDIO.MAGICAL_02);
    else
      this.PlayAudio(QuestGachaDirectorBase.AUDIO.MAGICAL_01);
  }

  protected void PlayAppearAudio(RARITY_TYPE rarity_type, bool is_short)
  {
    this.PlayAudio(is_short ? QuestGachaDirectorBase.AUDIO.APEAR_SHORT : QuestGachaDirectorBase.AUDIO.APEAR_LONG);
  }

  public void PlayRarityAudio(RARITY_TYPE rarity_type, bool ignore_skip_check = false)
  {
    QuestGachaDirectorBase.AUDIO audio = QuestGachaDirectorBase.AUDIO.RARITY_LOW;
    switch (rarity_type)
    {
      case RARITY_TYPE.A:
        audio = QuestGachaDirectorBase.AUDIO.RARITY_MID;
        break;
      case RARITY_TYPE.S:
      case RARITY_TYPE.SS:
      case RARITY_TYPE.SSS:
        audio = QuestGachaDirectorBase.AUDIO.RARITY_HIGH_0;
        break;
    }
    this.PlayAudio(audio, ignore_skip_check);
  }

  protected void CacheEnemyAudio(EnemyTable.EnemyData enemyData, LoadingQueue lo_queue)
  {
    if (lo_queue == null)
      return;
    OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
    if (enemyDisplayInfo == null)
      return;
    if (enemyDisplayInfo.seIdGachaShort > 0)
      lo_queue.CacheSE(enemyDisplayInfo.seIdGachaShort);
    if (enemyDisplayInfo.seIdGachaLong <= 0)
      return;
    lo_queue.CacheSE(enemyDisplayInfo.seIdGachaLong);
  }

  protected void PlayEnemyAudio(EnemyTable.EnemyData enemyData, bool is_short = false)
  {
    if (this.skip)
      return;
    OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
    if (enemyDisplayInfo == null)
      return;
    int se_id = is_short ? enemyDisplayInfo.seIdGachaShort : enemyDisplayInfo.seIdGachaLong;
    if (se_id <= 0)
      return;
    SoundManager.PlayOneshotJingle(se_id);
  }

  protected void PlayEnemyAnimation(EnemyLoader enemyLoader, string animStateName)
  {
    Animator animator = (Animator) null;
    if (Object.op_Inequality((Object) enemyLoader, (Object) null))
      animator = enemyLoader.GetAnimator();
    if (!Object.op_Inequality((Object) animator, (Object) null))
      return;
    int hash = Animator.StringToHash(animStateName);
    if (!animator.HasState(0, hash))
      return;
    ((Behaviour) animator).enabled = true;
    animator.Play(hash, 0, 0.0f);
    animator.Update(0.0f);
  }

  public override void __FUNCTION__PlayCachedAudio(int se_id)
  {
    if (this.skip)
      return;
    base.__FUNCTION__PlayCachedAudio(se_id);
  }

  protected void CheckAndReplaceShader(EnemyLoader enemyLoader)
  {
    Shader shader = (Shader) null;
    if (enemyLoader.bodyID == 2023)
      shader = ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_reflective_simple");
    if (!Object.op_Inequality((Object) shader, (Object) null))
      return;
    ((Component) enemyLoader.body).GetComponentsInChildren<Renderer>(Temporary.rendererList);
    for (int index1 = 0; index1 < Temporary.rendererList.Count; ++index1)
    {
      Renderer renderer = Temporary.rendererList[index1];
      switch (renderer)
      {
        case MeshRenderer _:
        case SkinnedMeshRenderer _:
          for (int index2 = 0; index2 < renderer.materials.Length; ++index2)
            renderer.materials[index2].shader = shader;
          break;
      }
    }
  }

  public interface ISectionCommand
  {
    void OnShowRarity(RARITY_TYPE rarity);

    void OnHideRarity();

    void OnEnd();

    void ActivateSkipButton();
  }

  public enum AUDIO
  {
    RARITY_MID = 40000126, // 0x02625A7E
    RARITY_HIGH_0 = 40000127, // 0x02625A7F
    OPENING_01 = 40000128, // 0x02625A80
    OPENING_02 = 40000129, // 0x02625A81
    OPENING_03 = 40000130, // 0x02625A82
    OPENING_04 = 40000131, // 0x02625A83
    DOOR_01 = 40000132, // 0x02625A84
    DOOR_02 = 40000133, // 0x02625A85
    MAGI_INTRO_01 = 40000134, // 0x02625A86
    MAGI_INTRO_02 = 40000135, // 0x02625A87
    MAGICAL_01 = 40000136, // 0x02625A88
    MAGICAL_02 = 40000137, // 0x02625A89
    METEOR_01 = 40000138, // 0x02625A8A
    METEOR_02 = 40000139, // 0x02625A8B
    APEAR_LONG = 40000140, // 0x02625A8C
    APEAR_SHORT = 40000141, // 0x02625A8D
    RARITY_LOW = 40000164, // 0x02625AA4
    RARITY_EXPOSITION = 40000165, // 0x02625AA5
  }
}

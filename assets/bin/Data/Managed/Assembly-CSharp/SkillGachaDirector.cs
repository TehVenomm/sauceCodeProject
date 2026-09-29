// Decompiled with JetBrains decompiler
// Type: SkillGachaDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SkillGachaDirector : AnimationDirector
{
  public Animator gachaAnimator;
  public Animator cameraAnimator;
  public AnimationClip[] camereAnimClips;
  public Transform ballSocket;
  public Transform basket;
  public Renderer line;
  public GameObject[] balls;
  public GameObject[] ballsRen;
  public GameObject[] openEffects;
  public GameObject npcEffect;
  public GameObject jumpBallEffect;
  public Transform jumpBallEffectPosition;
  public GameObject dropBallEffect;
  public Transform dropBallEffectPosition;
  public GameObject[] uiRarityEffectPrefabs;
  public Transform flashEffectPosition;
  public GameObject flashEffectPrefab;
  public GameObject[] flashEffectRarityPrefabs;
  public const string STATE_SINGLE = "SKILL_GACHA_SINGLE";
  public const string STATE_REAM = "SKILL_GACHA_REAM";
  public const string STATE_REAM_DROP_1 = "SKILL_GACHA_REAM_DROP_1";
  public const string STATE_REAM_DROP_2 = "SKILL_GACHA_REAM_DROP_2";
  public const int DISPLAY_BALL_MAX = 20;
  public const float BALL_EXTERNAL_FORCE_RATE = 120f;
  public const int REAM_NUM = 11;
  private SkillGachaDirector.ISectionCommand sectionCommandReceiver;
  private IEnumerator coroutine;
  private Transform npcModel;
  private Animator npcAnimator;
  private Transform mainBall;
  private Collider[] basketColliders;
  private float saveFixedUpdateTime;
  private List<GameObject> managedObjects = new List<GameObject>();
  private List<GameObject> ballObjects = new List<GameObject>();
  private List<Transform> managedEffects = new List<Transform>();
  private List<Transform> flashEffectList = new List<Transform>();
  private int rarityIndex;
  private RARITY_TYPE rarity;
  private SkillGachaDirector.FLASH_TYPE firstFlashType;
  private int dropCount;
  private int flashCount;
  private GachaResult.GachaReward reward;
  private bool isReam;
  private bool m_isAlreadySkipped;
  private bool m_isFinishLoad;
  public const int SINGLE_FLASH_EFFECT_MAX = 3;
  public const int REAM_FLASH_EFFECT_MAX = 4;
  public const int MODEL_TEXTURE_MAX = 3;
  public const int MODEL_TEXTURE_ID_TOP = 2;
  private Texture2D[] basketModelTextureList = new Texture2D[3];
  private Texture backupBasketModelTexture;
  private Color backupBasketSpeColor = Color.white;

  private bool IsSingleGacha => !this.isReam;

  protected override void Awake()
  {
    base.Awake();
    this.commandReceiver = (Component) this;
    if (this.balls.Length != 0)
    {
      for (int index = 0; index < this.balls.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.balls[index], (Object) null))
          this.balls[index].SetActive(false);
      }
    }
    this.SetActiveRenBalls(false);
    if (Object.op_Inequality((Object) this.ballSocket, (Object) null))
    {
      this.basketColliders = ((Component) this.ballSocket).GetComponentsInChildren<Collider>(true);
      this.SetActivateBasketCollider(false);
    }
    if (Object.op_Inequality((Object) this.line, (Object) null))
      this.line.enabled = false;
    Material material = this.GetMaterial(this.basket);
    if (!Object.op_Inequality((Object) material, (Object) null))
      return;
    this.backupBasketModelTexture = material.mainTexture;
    this.backupBasketSpeColor = material.GetColor("_SpeLightColor");
  }

  protected override void OnDestroy() => this.backupBasketModelTexture = (Texture) null;

  private void Start() => this.Init();

  private void Init()
  {
    this.skip = false;
    this.m_isAlreadySkipped = false;
    this.Play("INIT");
  }

  public void StartDirection(
    SkillGachaDirector.ISectionCommand command_receiver)
  {
    if (this.coroutine != null)
    {
      this.StopCoroutine(this.coroutine);
      this.coroutine = (IEnumerator) null;
    }
    if (command_receiver == null)
      return;
    this.sectionCommandReceiver = command_receiver;
    this.StartCoroutine(this.coroutine = this.DoSkillGacha());
  }

  private IEnumerator DoSkillGacha()
  {
    Transform transform = ((Component) this).transform;
    this.Reset();
    if (Object.op_Inequality((Object) this.line, (Object) null))
      this.line.enabled = true;
    this.isReam = false;
    if (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.IsReam())
      this.isReam = true;
    this.SetActivateBasketCollider(true);
    Vector3 position = this.basket.position;
    for (int index = 0; index < 20; ++index)
      this.CreateBall(transform, 0, Vector3.op_Addition(position, Quaternion.op_Multiply(Quaternion.AngleAxis((float) (index * 45), Vector3.right), new Vector3(0.04f * (float) (index - 10), 0.08f, 0.0f))), false);
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    if (this.isReam)
    {
      GachaResult currentGachaResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
      int index1 = 0;
      for (int count = currentGachaResult.reward.Count; index1 < count; ++index1)
      {
        GachaResult.GachaReward gachaReward = currentGachaResult.reward[index1];
        SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) gachaReward.itemId);
        if (skillItemData != null)
        {
          load_queue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(skillItemData.modelID));
          load_queue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemSymbolModel(skillItemData.iconID));
        }
      }
      List<GachaResult.GachaReward> reward = currentGachaResult.reward;
      if (this.ballsRen != null)
      {
        for (int index2 = 0; index2 < this.ballsRen.Length; ++index2)
        {
          if (index2 < reward.Count)
          {
            GachaResult.GachaReward gachaReward = reward[index2];
            SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) gachaReward.itemId);
            if (skillItemData != null)
              ((Renderer) this.ballsRen[index2].GetComponent<MeshRenderer>()).sharedMaterial = ((Renderer) this.balls[skillItemData.rarity.ToRarityExpressionID()].GetComponent<MeshRenderer>()).sharedMaterial;
          }
        }
      }
    }
    foreach (int se_id in (int[]) Enum.GetValues(typeof (SkillGachaDirector.AUDIO)))
      load_queue.CacheSE(se_id);
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    for (int tx = 0; tx < 3; ++tx)
    {
      LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.MAGI_BASKET_MODEL_TEX, ResourceName.GetMagiGachaModelTexutre(2 + tx));
      while (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      this.basketModelTextureList[tx] = (Texture2D) loadObj.loadedObject;
      loadObj = (LoadObject) null;
    }
    if (Object.op_Inequality((Object) this.backupBasketModelTexture, (Object) null))
    {
      Material material = this.GetMaterial(this.basket);
      if (Object.op_Inequality((Object) material, (Object) null))
      {
        material.mainTexture = this.backupBasketModelTexture;
        material.SetColor("_SpeLightColor", this.backupBasketSpeColor);
      }
    }
    yield return (object) new WaitForSeconds(1f);
    this.npcModel = Utility.CreateGameObject("NPC", ((Component) this).transform.parent);
    this.managedObjects.Add(((Component) this.npcModel).gameObject);
    NPCLoader npc_loader = ((Component) this.npcModel).gameObject.AddComponent<NPCLoader>();
    npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(1).npcModelID, 0, false, true, SHADER_TYPE.NORMAL, (System.Action) null);
    while (npc_loader.isLoading)
      yield return (object) null;
    if (load_queue != null && load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    this.npcAnimator = npc_loader.animator;
    yield return (object) null;
    this.m_isFinishLoad = true;
    this.npcAnimator.cullingMode = (AnimatorCullingMode) 0;
    this.npcAnimator.Rebind();
    this.npcAnimator.cullingMode = (AnimatorCullingMode) 0;
    this.gachaAnimator.Rebind();
    this.cameraAnimator.cullingMode = (AnimatorCullingMode) 0;
    this.cameraAnimator.Rebind();
    string state_name = !this.IsSingleGacha ? "SKILL_GACHA_REAM" : "SKILL_GACHA_SINGLE";
    this.dropCount = 0;
    this.Drop();
    this.Play(state_name);
    if (this.isReam)
    {
      while (this.dropCount < 11)
      {
        if (this.skip && this.IsFinishDrop10())
        {
          if (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
            yield return (object) null;
          Time.timeScale = 1f;
          this.skip = false;
          yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
        }
        yield return (object) null;
      }
    }
    while (((Component) this.mainBall).gameObject.activeSelf)
      yield return (object) null;
    if (this.skip)
    {
      yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
      Time.timeScale = 1f;
    }
    yield return (object) null;
    this.sectionCommandReceiver.OnEnd();
    this.coroutine = (IEnumerator) null;
  }

  private void Play3Anim(string state_name)
  {
  }

  private void Drop()
  {
    this.sectionCommandReceiver.OnHideRarity();
    int index = 0;
    for (int count = this.managedEffects.Count; index < count; ++index)
    {
      if (Object.op_Inequality((Object) this.managedEffects[index], (Object) null))
        Object.Destroy((Object) ((Component) this.managedEffects[index]).gameObject);
    }
    this.managedEffects.Clear();
    this.rarity = RARITY_TYPE.D;
    this.reward = (GachaResult.GachaReward) null;
    if (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && this.dropCount < MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count)
    {
      this.reward = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[this.dropCount];
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) this.reward.itemId);
      if (skillItemData != null)
        this.rarity = skillItemData.rarity;
    }
    if (this.reward == null)
      this.reward = new GachaResult.GachaReward();
    this.rarityIndex = this.rarity.ToRarityExpressionID();
    if (Object.op_Inequality((Object) this.mainBall, (Object) null))
      Object.Destroy((Object) ((Component) this.mainBall).gameObject);
    this.mainBall = this.CreateBall(this.ballSocket, this.rarityIndex, Vector3.zero, true);
    string state_name;
    if (this.dropCount == 0)
    {
      state_name = !this.IsSingleGacha ? "SKILL_GACHA_REAM" : "SKILL_GACHA_SINGLE";
      this.Play(state_name);
    }
    else
      state_name = this.dropCount >= 10 ? "SKILL_GACHA_REAM_DROP_2" : "SKILL_GACHA_REAM_DROP_1";
    this.npcAnimator.Play(state_name, 0, 0.0f);
    this.gachaAnimator.Play(state_name, 0, 0.0f);
    this.cameraAnimator.Play(state_name, 0, 0.0f);
  }

  private Transform CreateBall(Transform parent, int rarityType, Vector3 pos, bool is_main)
  {
    GameObject gameObject = ResourceUtility.Instantiate<GameObject>(this.balls[rarityType]);
    Transform transform = gameObject.transform;
    transform.parent = parent;
    transform.localPosition = pos;
    transform.localScale = Vector3.one;
    if (is_main)
    {
      Object.Destroy((Object) gameObject.GetComponent<Rigidbody>());
      Object.Destroy((Object) gameObject.GetComponent<Collider>());
    }
    else
    {
      SkillGachaDirector.MagiBall magiBall = gameObject.AddComponent<SkillGachaDirector.MagiBall>();
      if (Object.op_Inequality((Object) magiBall, (Object) null))
        magiBall.FlashType = (SkillGachaDirector.FLASH_TYPE) rarityType;
      this.ballObjects.Add(gameObject);
    }
    gameObject.SetActive(true);
    return transform;
  }

  protected override void LateUpdate()
  {
    Object.op_Inequality((Object) this.useCamera, (Object) null);
    base.LateUpdate();
  }

  private void Delete()
  {
    int index1 = 0;
    for (int count = this.managedObjects.Count; index1 < count; ++index1)
    {
      Object.DestroyImmediate((Object) this.managedObjects[index1]);
      this.managedObjects[index1] = (GameObject) null;
    }
    this.managedObjects.Clear();
    if (this.flashEffectList != null && this.flashEffectList.Count > 0)
    {
      int count = this.flashEffectList.Count;
      for (int index2 = 0; index2 < count; ++index2)
      {
        Object.DestroyImmediate((Object) ((Component) this.flashEffectList[index2]).gameObject);
        this.flashEffectList[index2] = (Transform) null;
      }
    }
    this.flashEffectList.Clear();
    this.mainBall = (Transform) null;
    this.npcModel = (Transform) null;
    this.npcAnimator = (Animator) null;
    int count1 = this.ballObjects.Count;
    for (int index3 = 0; index3 < count1; ++index3)
    {
      Object.DestroyImmediate((Object) this.ballObjects[index3]);
      this.ballObjects[index3] = (GameObject) null;
    }
    this.ballObjects.Clear();
    if (Object.op_Inequality((Object) this.line, (Object) null))
      this.line.enabled = false;
    this.Play("INIT");
  }

  public override void Reset()
  {
    this.skip = false;
    this.m_isAlreadySkipped = false;
    this.m_isFinishLoad = false;
    this.dropCount = 0;
    this.flashCount = 0;
    this.reward = (GachaResult.GachaReward) null;
    this.SetActiveRenBalls(false);
    this.Delete();
  }

  public override void Skip()
  {
    if (this.skip || this.IsFinishDrop10() || this.m_isAlreadySkipped)
      return;
    this.m_isAlreadySkipped = true;
    AudioObjectPool.StopAllLentObjects();
    base.Skip();
    this.StartCoroutine(this.DoSkip());
  }

  private IEnumerator DoSkip()
  {
    yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out();
    Time.timeScale = 100f;
    while (!this.m_isFinishLoad)
      yield return (object) null;
    if (this.IsSingleGacha)
    {
      int num = (int) this.UpdateGachaModelEffectSingle(3);
    }
  }

  public bool IsFinishDrop10() => this.isReam && this.dropCount >= 10;

  private void OnDirectionCommand(string cmd)
  {
    bool isSkip = !MonoBehaviourSingleton<TransitionManager>.I.isChanging && MonoBehaviourSingleton<TransitionManager>.I.isTransing;
    if (cmd == "NPC_EFFECT")
    {
      if (!Object.op_Inequality((Object) this.npcModel, (Object) null))
        return;
      Transform parent = Utility.Find(this.npcModel, "Head");
      if (!Object.op_Inequality((Object) parent, (Object) null))
        return;
      ResourceUtility.Realizes((Object) this.npcEffect, parent);
    }
    else if (cmd == "DROP_EFFECT")
    {
      if (isSkip || !Object.op_Inequality((Object) this.dropBallEffect, (Object) null) || !Object.op_Inequality((Object) this.dropBallEffectPosition, (Object) null))
        return;
      this.managedEffects.Add(ResourceUtility.Realizes((Object) this.dropBallEffect, this.dropBallEffectPosition));
    }
    else if (cmd == "JUMP_EFFECT")
    {
      if (isSkip)
        return;
      this.PlayAUDIO(SkillGachaDirector.AUDIO.BALL_POP);
      if (!Object.op_Inequality((Object) this.jumpBallEffect, (Object) null) || !Object.op_Inequality((Object) this.jumpBallEffectPosition, (Object) null))
        return;
      this.managedEffects.Add(ResourceUtility.Realizes((Object) this.jumpBallEffect, this.jumpBallEffectPosition));
    }
    else if (cmd == "SHOW_RARITY")
    {
      if (isSkip)
        return;
      if (this.isReam)
        this.sectionCommandReceiver.OnShowRarity(this.rarity);
      this.PlayAUDIO(SkillGachaDirector.AUDIO.BALL_SHINE);
    }
    else if (cmd == "OPEN_EFFECT")
    {
      if (isSkip)
        return;
      this.managedEffects.Add(ResourceUtility.Realizes((Object) this.openEffects[this.rarityIndex], this.ballSocket));
      if (this.rarityIndex == 0 && this.IsSingleGacha || this.rarityIndex == 10)
        this.PlayAUDIO(SkillGachaDirector.AUDIO.BALL_BREAK);
      else
        this.PlayAUDIO(SkillGachaDirector.AUDIO.BALL_BREAK_S);
    }
    else if (cmd == "FLASH_EFFECT")
      this.EventFlashEffect(isSkip);
    else if (cmd == "HIDE_BALL")
    {
      if (Object.op_Inequality((Object) this.mainBall, (Object) null))
        ((Component) this.mainBall).gameObject.SetActive(false);
      if (!this.isReam || this.sectionCommandReceiver == null || this.reward == null)
        return;
      this.sectionCommandReceiver.OnShowSkillModel((uint) this.reward.itemId);
    }
    else if (cmd == "NEXT_DROP")
    {
      if (!this.isReam)
        return;
      int num = 9;
      if (this.skip && isSkip && this.dropCount < num)
        this.dropCount = num;
      ++this.dropCount;
      if (this.dropCount <= 10)
        this.Drop();
      if (this.sectionCommandReceiver == null)
        return;
      this.sectionCommandReceiver.OnHideSkillModel();
    }
    else
    {
      if (cmd == "SHOW_REN_BALLS")
        this.SetActiveRenBalls(true);
      if (!(cmd == "HIDE_REN_BALLS"))
        return;
      this.SetActiveRenBalls(false);
    }
  }

  private void EventFlashEffect(bool isSkip)
  {
    if (isSkip)
    {
      if (!this.isReam)
        return;
      ++this.flashCount;
      int num = (int) this.UpdateGachaModelEffectReam(this.flashCount);
    }
    else
    {
      if (Object.op_Equality((Object) this.flashEffectPrefab, (Object) null) || this.flashEffectRarityPrefabs == null)
        return;
      this.ApplyRandomVectorForBalls();
      ++this.flashCount;
      GameObject flashEffectPrefab = this.flashEffectPrefab;
      GameObject effectRarityPrefab;
      if (this.isReam)
      {
        if (this.flashCount >= 4)
        {
          SkillGachaDirector.FLASH_TYPE flashTypeAtLast = this.GetFlashTypeAtLast();
          effectRarityPrefab = this.flashEffectRarityPrefabs[(int) flashTypeAtLast];
          this.SwitchBasketModelTexture(flashTypeAtLast);
          this.SwitchBasketBallColor(flashTypeAtLast);
          this.PlayAUDIOFlash(flashTypeAtLast);
        }
        else
          effectRarityPrefab = this.flashEffectRarityPrefabs[(int) this.UpdateGachaModelEffectReam(this.flashCount)];
      }
      else
        effectRarityPrefab = this.flashEffectRarityPrefabs[(int) this.UpdateGachaModelEffectSingle(this.flashCount)];
      if (!Object.op_Inequality((Object) effectRarityPrefab, (Object) null) || !Object.op_Inequality((Object) this.flashEffectPosition, (Object) null))
        return;
      Transform transform = ResourceUtility.Realizes((Object) effectRarityPrefab, this.flashEffectPosition);
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      this.flashEffectList.Add(transform);
    }
  }

  private SkillGachaDirector.FLASH_TYPE UpdateGachaModelEffectSingle(int targetFlashCount)
  {
    SkillGachaDirector.FLASH_TYPE flashType = SkillGachaDirector.FLASH_TYPE.GREEN;
    if (targetFlashCount > 1)
    {
      if (this.CalcNumRarityData(RARITY_TYPE.SS) > 0 || this.CalcNumRarityData(RARITY_TYPE.S) > 0)
      {
        flashType = SkillGachaDirector.FLASH_TYPE.GOLD;
        if (targetFlashCount < 3)
          flashType = Random.Range(0, 100) > 50 ? SkillGachaDirector.FLASH_TYPE.GREEN : SkillGachaDirector.FLASH_TYPE.SILVER;
      }
      else if (this.CalcNumRarityData(RARITY_TYPE.A) > 0)
      {
        flashType = SkillGachaDirector.FLASH_TYPE.SILVER;
        if (targetFlashCount < 3)
          flashType = Random.Range(0, 100) > 50 ? SkillGachaDirector.FLASH_TYPE.GREEN : SkillGachaDirector.FLASH_TYPE.SILVER;
      }
    }
    this.PlayAUDIOFlash(flashType);
    this.SwitchBasketBallColor(flashType);
    this.SwitchBasketModelTexture(flashType);
    return flashType;
  }

  private void SwitchBasketBallColor(SkillGachaDirector.FLASH_TYPE targetFlashType)
  {
    foreach (GameObject ballObject in this.ballObjects)
    {
      MeshRenderer component = ballObject.GetComponent<MeshRenderer>();
      if (!Object.op_Equality((Object) component, (Object) null))
      {
        Material material1 = ((Renderer) component).material;
        if (!Object.op_Equality((Object) material1, (Object) null))
        {
          Material material2 = ((Renderer) this.balls[(int) targetFlashType].GetComponent<MeshRenderer>()).material;
          material1.mainTexture = material2.mainTexture;
          material1.SetColor("_SpeLightColor", material2.GetColor("_SpeLightColor"));
          material1.SetFloat("_SpeWidth", material2.GetFloat("_SpeWidth"));
        }
      }
    }
  }

  private void SwitchBasketBallColorReam(
    SkillGachaDirector.FLASH_TYPE targetFlashType,
    int numChange)
  {
    int num = 0;
    foreach (GameObject ballObject in this.ballObjects)
    {
      if (num >= numChange)
        break;
      SkillGachaDirector.MagiBall component1 = ballObject.GetComponent<SkillGachaDirector.MagiBall>();
      if (!Object.op_Equality((Object) component1, (Object) null) && component1.FlashType == SkillGachaDirector.FLASH_TYPE.GREEN)
      {
        MeshRenderer component2 = ballObject.GetComponent<MeshRenderer>();
        if (!Object.op_Equality((Object) component2, (Object) null))
        {
          Material material1 = ((Renderer) component2).material;
          if (!Object.op_Equality((Object) material1, (Object) null))
          {
            Material material2 = ((Renderer) this.balls[(int) targetFlashType].GetComponent<MeshRenderer>()).material;
            material1.mainTexture = material2.mainTexture;
            material1.SetColor("_SpeLightColor", material2.GetColor("_SpeLightColor"));
            material1.SetFloat("_SpeWidth", material2.GetFloat("_SpeWidth"));
            component1.FlashType = targetFlashType;
            ++num;
          }
        }
      }
    }
  }

  private SkillGachaDirector.FLASH_TYPE UpdateGachaModelEffectReam(int targetFlashCount)
  {
    SkillGachaDirector.FLASH_TYPE flashType = SkillGachaDirector.FLASH_TYPE.GREEN;
    switch (targetFlashCount)
    {
      case 2:
        int numRarityData10 = this.GetNumRarityData10(RARITY_TYPE.A);
        if (numRarityData10 > 0)
        {
          flashType = SkillGachaDirector.FLASH_TYPE.SILVER;
          this.SwitchBasketBallColorReam(flashType, numRarityData10 * 2);
        }
        this.firstFlashType = flashType;
        break;
      case 3:
        flashType = this.firstFlashType;
        int num = this.GetNumRarityData10(RARITY_TYPE.SS) + this.GetNumRarityData10(RARITY_TYPE.S);
        if (num > 0)
        {
          flashType = SkillGachaDirector.FLASH_TYPE.GOLD;
          this.SwitchBasketBallColorReam(flashType, num * 2);
          break;
        }
        break;
    }
    this.SwitchBasketModelTexture(flashType);
    this.PlayAUDIOFlash(flashType);
    return flashType;
  }

  private void SwitchBasketModelTexture(SkillGachaDirector.FLASH_TYPE targetRarityType)
  {
    if (this.basketModelTextureList == null)
    {
      Log.Error("Not found downloaded texture!!");
    }
    else
    {
      Texture2D basketModelTexture = this.basketModelTextureList[(int) targetRarityType];
      if (Object.op_Equality((Object) basketModelTexture, (Object) null))
      {
        Log.Error("Not found texture for basket model!!");
      }
      else
      {
        Material material = this.GetMaterial(this.basket);
        if (!Object.op_Inequality((Object) material, (Object) null))
          return;
        material.mainTexture = (Texture) basketModelTexture;
        material.SetColor("_SpeLightColor", Color.white);
      }
    }
  }

  private Material GetMaterial(Transform targetTrans)
  {
    MeshRenderer component = ((Component) targetTrans).GetComponent<MeshRenderer>();
    if (Object.op_Equality((Object) component, (Object) null))
    {
      Log.Error("Not found MeshRender!!");
      return (Material) null;
    }
    Material[] materials = ((Renderer) component).materials;
    if (materials != null)
      return materials[0];
    Log.Error("material list is null!!");
    return (Material) null;
  }

  private void ApplyRandomVectorForBalls()
  {
    foreach (GameObject ballObject in this.ballObjects)
    {
      if (!Object.op_Equality((Object) ballObject, (Object) null))
      {
        Rigidbody component = ballObject.GetComponent<Rigidbody>();
        if (!Object.op_Equality((Object) component, (Object) null))
          component.AddForce(Vector3.op_Multiply(Vector3.up, 120f));
      }
    }
  }

  private SkillGachaDirector.FLASH_TYPE GetFlashTypeAtLast()
  {
    SkillGachaDirector.FLASH_TYPE flashTypeAtLast = SkillGachaDirector.FLASH_TYPE.GREEN;
    if (this.CheckContainRarityLast(RARITY_TYPE.SS) || this.CheckContainRarityLast(RARITY_TYPE.S))
      flashTypeAtLast = SkillGachaDirector.FLASH_TYPE.GOLD;
    else if (this.CheckContainRarityLast(RARITY_TYPE.A))
      flashTypeAtLast = SkillGachaDirector.FLASH_TYPE.SILVER;
    return flashTypeAtLast;
  }

  private bool CheckContainRarityLast(RARITY_TYPE targetRarityType)
  {
    return this.CalcNumRarityData(targetRarityType, true) > 0;
  }

  private int GetNumRarityData10(RARITY_TYPE targetRarityType)
  {
    return this.CalcNumRarityData(targetRarityType, isCheck10: true);
  }

  private int CalcNumRarityData(RARITY_TYPE targetRarityType, bool isCheckOnlyLast = false, bool isCheck10 = false)
  {
    int num = 0;
    if (!MonoBehaviourSingleton<GachaManager>.IsValid())
    {
      Log.Error("Invalid GachaManager!!");
      return num;
    }
    GachaResult currentGachaResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
    if (currentGachaResult == null)
    {
      Log.Error("gachaResult is null!!");
      return num;
    }
    int count = currentGachaResult.reward.Count;
    List<GachaResult.GachaReward> reward = currentGachaResult.reward;
    if (isCheckOnlyLast)
      return Singleton<SkillItemTable>.I.GetSkillItemData((uint) reward[count - 1].itemId).rarity != targetRarityType ? 0 : 1;
    if (isCheck10 && this.isReam)
      --count;
    for (int index = 0; index < count; ++index)
    {
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) reward[index].itemId);
      if (skillItemData != null && skillItemData.rarity == targetRarityType)
        ++num;
    }
    return num;
  }

  private void PlayAUDIOFlash(SkillGachaDirector.FLASH_TYPE flash)
  {
    SkillGachaDirector.AUDIO audio = SkillGachaDirector.AUDIO.FLASH_RARITY_01;
    if (flash > SkillGachaDirector.FLASH_TYPE.GREEN)
      audio = SkillGachaDirector.AUDIO.FLASH_RARITY_02;
    this.PlayAUDIO(audio);
  }

  private void PlayAUDIO(SkillGachaDirector.AUDIO audio)
  {
    if (this.skip)
      return;
    SoundManager.PlayOneShotUISE((int) audio);
  }

  public void PlayUIRarityEffect(
    RARITY_TYPE rarity,
    Transform effect_parent_ui,
    Transform effect_target_ui)
  {
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
    this.PlayRarityAudio(rarity);
  }

  public void PlayRarityAudio(RARITY_TYPE rarity_type)
  {
    SoundManager.PlayOneShotUISE((int) rarity_type < 4 ? 40000126 : 40000127);
  }

  public override void __FUNCTION__PlayCachedAudio(int se_id)
  {
    if (this.skip)
      return;
    base.__FUNCTION__PlayCachedAudio(se_id);
  }

  public void SetActiveRenBalls(bool isActive)
  {
    if (this.ballsRen == null || this.ballsRen.Length == 0)
      return;
    int index1 = 0;
    if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null)
    {
      for (int index2 = Mathf.Min(this.ballsRen.Length, MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count); index1 < index2; ++index1)
      {
        if (Object.op_Inequality((Object) this.ballsRen[index1], (Object) null))
          this.ballsRen[index1].SetActive(isActive);
      }
    }
    for (; index1 < this.ballsRen.Length; ++index1)
    {
      if (Object.op_Inequality((Object) this.ballsRen[index1], (Object) null))
        this.ballsRen[index1].SetActive(false);
    }
  }

  public void SetActivateBasketCollider(bool isActivate)
  {
    if (this.basketColliders == null)
    {
      Log.Error("basketCollider is null!!");
    }
    else
    {
      int index = 0;
      for (int length = this.basketColliders.Length; index < length; ++index)
        this.basketColliders[index].enabled = isActivate;
    }
  }

  public interface ISectionCommand
  {
    void OnShowRarity(RARITY_TYPE rarity);

    void OnHideRarity();

    void OnShowSkillModel(uint skill_item_id);

    void OnHideSkillModel();

    void OnEnd();
  }

  public enum AUDIO
  {
    DROP_HEAVY = 40000101, // 0x02625A65
    BALL_ROLL_REAM = 40000104, // 0x02625A68
    RARITY_01 = 40000126, // 0x02625A7E
    RARITY_02 = 40000127, // 0x02625A7F
    FLASH_RARITY_01 = 40000136, // 0x02625A88
    FLASH_RARITY_02 = 40000137, // 0x02625A89
    OPENING_01 = 40000142, // 0x02625A8E
    OPENING_02 = 40000143, // 0x02625A8F
    ROLLING_01 = 40000144, // 0x02625A90
    ROLLING_02 = 40000145, // 0x02625A91
    BALL_DROP = 40000146, // 0x02625A92
    BALL_ROLLING = 40000147, // 0x02625A93
    BALL_SHINE = 40000148, // 0x02625A94
    BALL_POP = 40000149, // 0x02625A95
    BALL_BREAK = 40000150, // 0x02625A96
    BALL_BREAK_S = 40000151, // 0x02625A97
  }

  public enum FLASH_TYPE
  {
    INVALID = -1, // 0xFFFFFFFF
    GREEN = 0,
    SILVER = 1,
    GOLD = 2,
  }

  public class MagiBall : MonoBehaviour
  {
    public SkillGachaDirector.FLASH_TYPE FlashType { get; set; }
  }
}

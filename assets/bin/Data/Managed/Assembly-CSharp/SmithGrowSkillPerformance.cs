// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillPerformance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class SmithGrowSkillPerformance : GameSection
{
  private SmithManager.ResultData resultData;
  private bool isGreat;
  private bool isExceed;
  private SkillItemInfo[] materials;
  private bool shouldShowGreatEffect;
  private SkillGrowDirector director;
  private ItemLoader magiLoader;
  private ItemLoader magiSymbolLoader;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.resultData = eventData[0] as SmithManager.ResultData;
    this.isGreat = (bool) eventData[1];
    this.materials = eventData[2] as SkillItemInfo[];
    this.isExceed = (bool) eventData[3];
    this.SetToggle((Enum) SmithGrowSkillPerformance.UI.TGL_DIRECTION, true);
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    SkillItemInfo itemData = this.resultData.itemData as SkillItemInfo;
    this.magiLoader = new GameObject("magimodel").AddComponent<ItemLoader>();
    int wait = 1;
    this.magiLoader.LoadSkillItem(itemData.tableID, ((Component) this.magiLoader).transform, ((Component) this.magiLoader).gameObject.layer, (System.Action) (() =>
    {
      ((Component) this.magiLoader.nodeMain).gameObject.SetActive(false);
      --wait;
    }));
    ++wait;
    this.magiSymbolLoader = new GameObject("magisymbol").AddComponent<ItemLoader>();
    this.magiSymbolLoader.LoadSkillItemSymbol(itemData.tableID, ((Component) this.magiSymbolLoader).transform, ((Component) this.magiSymbolLoader).gameObject.layer, (System.Action) (() =>
    {
      ((Component) this.magiSymbolLoader.nodeMain).gameObject.SetActive(false);
      --wait;
    }));
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_direction = load_queue.Load(RESOURCE_CATEGORY.UI, "GrowSkillDirection");
    LoadObject[] materialLoadObjects = new LoadObject[this.materials.Length];
    for (int index = 0; index < this.materials.Length; ++index)
    {
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(this.materials[index].tableID);
      materialLoadObjects[index] = load_queue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(skillItemData.modelID));
    }
    ++wait;
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(3);
    GameObject npcRoot = new GameObject("NPC");
    GameObject go = npcRoot;
    Action<Animator> on_complete = (Action<Animator>) (animator => --wait);
    npcData.LoadModel(go, false, true, on_complete, false);
    this.CacheAudio(load_queue);
    yield return (object) load_queue.Wait();
    while (wait > 0)
      yield return (object) null;
    Transform transform = ResourceUtility.Realizes(lo_direction.loadedObject, MonoBehaviourSingleton<StageManager>.I.stageObject);
    GameObject[] materials = new GameObject[this.materials.Length];
    for (int index = 0; index < this.materials.Length; ++index)
    {
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(this.materials[index].tableID);
      Transform t = ResourceUtility.Realizes(materialLoadObjects[index].loadedObject);
      PlayerLoader.SetEquipColor(t, skillItemData.modelColor.ToColor());
      materials[index] = ((Component) t).gameObject;
    }
    ((Component) this.magiLoader.nodeMain).gameObject.SetActive(true);
    ((Component) this.magiSymbolLoader.nodeMain).gameObject.SetActive(true);
    SkillGrowDirector component = ((Component) transform).GetComponent<SkillGrowDirector>();
    component.Init();
    component.SetNPC(npcRoot);
    component.SetMagiModel(((Component) this.magiLoader).gameObject, ((Component) this.magiSymbolLoader).gameObject, materials);
    component.SetMaterials(this.materials);
    this.director = component;
    base.Initialize();
  }

  protected override void OnOpen()
  {
    this.director.StartDirection(new System.Action(this.OnEndDirection));
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    SkillItemInfo itemData = this.resultData.itemData as SkillItemInfo;
    this.SetActive((Enum) SmithGrowSkillPerformance.UI.OBJ_GREAT, this.shouldShowGreatEffect);
    if (this.shouldShowGreatEffect)
    {
      this.PlayTween((Enum) SmithGrowSkillPerformance.UI.OBJ_GREAT, callback: (EventDelegate.Callback) (() => this.DispatchEvent("SKIP")), is_input_block: false);
      SoundManager.PlayOneShotSE(40000066);
    }
    this.SetLabelText((Enum) SmithGrowSkillPerformance.UI.LBL_GET_EXP, (itemData.exp - this.resultData.beforeExp).ToString());
  }

  public override void Exit()
  {
    if (Object.op_Implicit((Object) this.director))
    {
      this.director.Reset();
      Object.Destroy((Object) ((Component) this.director).gameObject);
    }
    if (Object.op_Implicit((Object) this.magiLoader))
      Object.Destroy((Object) ((Component) this.magiLoader).gameObject);
    base.Exit();
  }

  private void OnQuery_SKIP()
  {
    SkillGrowDirector director = this.director;
    if (director.isPlaying)
    {
      director.Skip();
      GameSection.StopEvent();
    }
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) this.resultData,
        (object) this.isGreat,
        (object) this.isExceed
      });
  }

  private void OnEndDirection()
  {
    if (this.isGreat)
      this.shouldShowGreatEffect = true;
    this.SetToggle((Enum) SmithGrowSkillPerformance.UI.TGL_DIRECTION, false);
    this.RefreshUI();
    if (this.shouldShowGreatEffect)
      return;
    this.DispatchEvent("SKIP");
  }

  private enum UI
  {
    STR_RESULT,
    TGL_DIRECTION,
    LBL_GET_EXP,
    OBJ_GREAT,
  }

  public enum AUDIO
  {
    NORMAL_EFFECT = 40000065, // 0x02625A41
    GREAT_EFFECT = 40000066, // 0x02625A42
  }
}

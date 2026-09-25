// Decompiled with JetBrains decompiler
// Type: SkillGrowDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SkillGrowDirector : AnimationDirector
{
  [SerializeField]
  private int tmpid = 3;
  [SerializeField]
  private GameObject[] magiEffects;
  [SerializeField]
  private GameObject foundationEffect;
  [SerializeField]
  private GameObject growingEffect;
  [SerializeField]
  private GameObject completeEffect;
  [SerializeField]
  private GameObject npcEffect;
  [SerializeField]
  private Transform magiObjectParent;
  [SerializeField]
  private Transform magiSymbolParent;
  [SerializeField]
  private Transform magiEffectParent;
  [SerializeField]
  private Transform foundationEffectParent;
  [SerializeField]
  private Transform growingEffectParent;
  [SerializeField]
  private Transform completeEffectParent;
  [SerializeField]
  private Transform npcParent;
  [SerializeField]
  private Transform npcEffectParent;
  [SerializeField]
  private GameObject magiMaterialEffectPrefab;
  [SerializeField]
  private Camera magiCamera;
  [SerializeField]
  private MeshRenderer magiInnerQuadRenderer;
  [SerializeField]
  private Camera magiInnerCamera;
  [SerializeField]
  private Camera npcCamera;
  [SerializeField]
  private MeshRenderer magiInnerTexRenderer;
  [SerializeField]
  private Animator[] cameraAnimators;
  private SkillItemInfo[] materials;
  private BlurFilter mainCameraBlur;
  private BlurFilter directionCameraBlur;
  private RenderTexture magiInnerRenderTexture;
  private GameObject[] magiMaterialEffects;
  private Animator npcAnimator;
  private List<GameObject> effects = new List<GameObject>();
  private int origDownsample;
  private bool initialized;
  private List<Animator> playingAnimators = new List<Animator>();

  public bool skipped => this.skip;

  private void Start() => this.Init();

  public void Init()
  {
    if (this.initialized)
      return;
    this.Play("MainAnim_Init");
    this.directionCameraBlur = ((Component) this.useCamera).GetComponent<BlurFilter>();
    if (MonoBehaviourSingleton<AppMain>.IsValid())
    {
      this.mainCameraBlur = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<BlurFilter>();
    }
    else
    {
      this.mainCameraBlur = this.directionCameraBlur;
      ((Component) this.useCamera).gameObject.AddComponent<RenderTargetCacher>();
    }
    this.origDownsample = this.mainCameraBlur.downSample;
    this.mainCameraBlur.downSample = this.directionCameraBlur.downSample;
    ((Behaviour) this).enabled = false;
    // ISSUE: method pointer
    rymFX.OnDisableDelegate = (rymFX.CallbackFunction) Delegate.Combine((Delegate) rymFX.OnDisableDelegate, (Delegate) new rymFX.CallbackFunction((object) this, __methodptr(OnFxDisable)));
    RenderTexture temporary = RenderTexture.GetTemporary(256 /*0x0100*/, 256 /*0x0100*/);
    this.magiInnerCamera.targetTexture = temporary;
    ((Renderer) this.magiInnerQuadRenderer).material.mainTexture = (Texture) temporary;
    this.magiInnerRenderTexture = temporary;
    this.initialized = true;
  }

  private void OnFxDisable(rymFX fx)
  {
    if (!this.skip)
      return;
    fx.PlayRate = 1f;
  }

  private void ResetPlayRate()
  {
    foreach (GameObject effect in this.effects)
    {
      if (Object.op_Implicit((Object) effect))
        effect.GetComponent<rymFX>().PlayRate = 1f;
    }
  }

  protected override void OnDestroy()
  {
    // ISSUE: method pointer
    rymFX.OnDisableDelegate = (rymFX.CallbackFunction) Delegate.Remove((Delegate) rymFX.OnDisableDelegate, (Delegate) new rymFX.CallbackFunction((object) this, __methodptr(OnFxDisable)));
    this.mainCameraBlur.downSample = this.origDownsample;
    RenderTexture.ReleaseTemporary(this.magiInnerRenderTexture);
    base.OnDestroy();
  }

  private IEnumerator TEst()
  {
    ((Component) this).gameObject.AddComponent<ResourceManager>();
    SkillItemInfo skillItemInfo = new SkillItemInfo()
    {
      tableData = new SkillItemTable.SkillItemData()
    };
    skillItemInfo.tableData.type = SKILL_SLOT_TYPE.ATTACK;
    skillItemInfo.tableData.modelID = 1;
    this.SetMaterials(new List<SkillItemInfo>()
    {
      skillItemInfo,
      skillItemInfo,
      skillItemInfo,
      skillItemInfo,
      skillItemInfo
    }.ToArray());
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(this.tmpid));
    LoadObject lo_symbol = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel(80000001));
    LoadObject[] materialLoadObjects = new LoadObject[this.materials.Length];
    for (int index = 0; index < this.materials.Length; ++index)
      materialLoadObjects[index] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(Random.Range(1, 5)));
    LoadObject npcTableLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "NPCTable");
    yield return (object) loadingQueue.Wait();
    TextAsset loadedObject = npcTableLoadObject.loadedObject as TextAsset;
    if (!Singleton<NPCTable>.IsValid())
    {
      Singleton<NPCTable>.Create();
      Singleton<NPCTable>.I.CreateTable(loadedObject.text);
    }
    bool wait = true;
    Singleton<NPCTable>.I.GetNPCData(3).LoadModel(((Component) this.npcParent).gameObject, false, true, (Action<Animator>) (animator =>
    {
      this.SetNPC(((Component) this.npcParent).gameObject);
      wait = false;
    }), false);
    GameObject[] materials = new GameObject[this.materials.Length];
    for (int index = 0; index < this.materials.Length; ++index)
    {
      Transform transform = ResourceUtility.Realizes(materialLoadObjects[index].loadedObject);
      materials[index] = ((Component) transform).gameObject;
    }
    this.SetMagiModel(((Component) ResourceUtility.Realizes(lo.loadedObject)).gameObject, ((Component) ResourceUtility.Realizes(lo_symbol.loadedObject)).gameObject, materials);
    while (wait)
      yield return (object) null;
    yield return (object) new WaitForSeconds(0.2f);
    this.StartDirection((System.Action) (() => { }));
  }

  public void SetNPC(GameObject npc)
  {
    Utility.Attach(this.npcParent, npc.transform);
    Utility.SetLayerWithChildren(npc.transform, ((Component) this.npcParent).gameObject.layer);
    this.npcAnimator = npc.GetComponentInChildren<Animator>();
  }

  public void SetMagiModel(GameObject magi, GameObject symbol, GameObject[] materials)
  {
    Utility.Attach(this.magiObjectParent, magi.transform);
    Utility.SetLayerWithChildren(magi.transform, ((Component) this.magiObjectParent).gameObject.layer);
    Utility.Attach(this.magiSymbolParent, symbol.transform);
    Utility.SetLayerWithChildren(symbol.transform, ((Component) this.magiSymbolParent).gameObject.layer);
    this.CreateEffect(this.foundationEffect, this.foundationEffectParent, this.magiCamera);
    RenderTargetCacher component = ((Component) this.mainCameraBlur).GetComponent<RenderTargetCacher>();
    foreach (Renderer componentsInChild in magi.GetComponentsInChildren<Renderer>())
    {
      Material material = componentsInChild.material;
      material.SetTexture("_EnvTex", (Texture) component.GetTexture());
      material.SetVector("_LightDir", new Vector4(-0.24f, -0.24f, -1.64f, 1f));
    }
    component.cacheAfter = true;
    this.magiMaterialEffects = new GameObject[materials.Length];
    for (int index = 0; index < materials.Length; ++index)
    {
      GameObject material1 = materials[index];
      ((Object) material1).name = "MAGI";
      material1.AddComponent<GrowMagiMaterialRotator>().Setup(new Vector3(Random.value * 360f, Random.value * 360f, Random.value * 360f), Random.Range(180f, 540f));
      Transform transform = ResourceUtility.Realizes((Object) this.magiMaterialEffectPrefab, this.magiEffectParent, ((Component) this.magiEffectParent).gameObject.layer);
      Utility.Attach(transform.GetChild(0), material1.transform);
      Utility.SetLayerWithChildren(material1.transform, ((Component) this.magiEffectParent).gameObject.layer);
      this.magiMaterialEffects[index] = ((Component) transform).gameObject;
      foreach (Renderer componentsInChild in material1.GetComponentsInChildren<Renderer>())
      {
        Material material2 = componentsInChild.material;
        material2.SetTexture("_EnvTex", (Texture) component.GetTexture());
        material2.SetVector("_LightDir", new Vector4(13.07f, -12.7f, -0.6f, 1f));
      }
      ((Component) transform).gameObject.SetActive(false);
    }
  }

  public void SetMaterials(SkillItemInfo[] materials) => this.materials = materials;

  public void StartDirection(System.Action onEnd)
  {
    ((Behaviour) this).enabled = true;
    this.SetLinkCamera(Object.op_Inequality((Object) this.mainCameraBlur, (Object) this.directionCameraBlur));
    this.mainCameraBlur.StartFilter();
    this.CreateEffect(this.completeEffect, this.completeEffectParent, this.magiCamera);
    this.CreateEffect(this.npcEffect, this.npcEffectParent, this.npcCamera);
    this.StartCreateMagiEffects();
    this.npcAnimator.Play("MAGI_GROW");
    this.npcAnimator.Update(0.0f);
    SoundManager.PlayOneShotSE(40000065);
    foreach (Animator cameraAnimator in this.cameraAnimators)
    {
      cameraAnimator.Play("CameraAnim_Start", 0, 0.0f);
      cameraAnimator.Update(0.0f);
    }
    this.Play("MainAnim_Start", (System.Action) (() =>
    {
      this.ResetPlayRate();
      if (onEnd == null)
        return;
      onEnd();
    }));
  }

  private void StartCreateMagiEffects()
  {
    float num1 = 250f;
    float num2 = 360f / (float) this.magiMaterialEffects.Length;
    foreach (GameObject magiMaterialEffect in this.magiMaterialEffects)
    {
      magiMaterialEffect.transform.localEulerAngles = new Vector3(0.0f, num1, 0.0f);
      num1 += num2;
      magiMaterialEffect.SetActive(true);
      Animator componentInChildren = magiMaterialEffect.GetComponentInChildren<Animator>();
      componentInChildren.Play("MagiMaterialAnim_Init", 0, 0.0f);
      componentInChildren.Update(0.0f);
      this.playingAnimators.Add(componentInChildren);
    }
  }

  private IEnumerator CreateMagiEffects()
  {
    GameObject[] gameObjectArray = this.magiMaterialEffects;
    for (int index = 0; index < gameObjectArray.Length; ++index)
    {
      GameObject gameObject = gameObjectArray[index];
      gameObject.SetActive(true);
      gameObject.GetComponentInChildren<Animation>().Play("MaterialAnim_1");
      for (int i = 0; i < 6; ++i)
      {
        yield return (object) null;
        if (this.skip)
          break;
      }
    }
    gameObjectArray = (GameObject[]) null;
  }

  private Transform CreateEffect(GameObject effect, Transform parent, Camera cam)
  {
    Transform effect1 = ResourceUtility.Realizes((Object) effect, parent, ((Component) parent).gameObject.layer);
    ((Component) effect1).GetComponent<rymFX>().Cameras = new Camera[1]
    {
      cam
    };
    this.effects.Add(((Component) effect1).gameObject);
    return effect1;
  }

  public override void Skip()
  {
    if (this.skip)
      return;
    foreach (GameObject effect in this.effects)
    {
      if (Object.op_Implicit((Object) effect))
      {
        rymFX component = effect.GetComponent<rymFX>();
        float num = component.GetLoopLastFrame() - component.GetCurFrame();
        component.UpdateFx(num / 30f, (Camera) null, false);
      }
    }
    base.Skip();
  }

  protected override void Update()
  {
    for (int index = 0; index < this.cameraAnimators.Length; ++index)
      this.cameraAnimators[index].speed = this.skip ? 1000f : 1f;
    for (int index = 0; index < this.playingAnimators.Count; ++index)
      this.playingAnimators[index].speed = this.skip ? 1000f : 1f;
    base.Update();
  }

  protected override void LateUpdate()
  {
    if (Object.op_Inequality((Object) this.useCamera, (Object) null))
    {
      float x = ((Component) this.cameraAnimators[0]).transform.localScale.x;
      if ((double) x > 0.0)
      {
        float verticalFov = Utility.HorizontalToVerticalFOV(x);
        this.useCamera.fieldOfView = verticalFov;
        this.npcCamera.fieldOfView = verticalFov;
      }
    }
    this.mainCameraBlur.blurStrength = this.directionCameraBlur.blurStrength;
    base.LateUpdate();
  }

  public override void Reset()
  {
    this.Play("MainAnim_Init");
    foreach (GameObject effect in this.effects)
    {
      if (Object.op_Implicit((Object) effect))
      {
        effect.GetComponent<rymFX>().PlayRate = 1f;
        Object.Destroy((Object) effect);
      }
    }
    this.effects.Clear();
    this.mainCameraBlur.blurStrength = (float) this.origDownsample;
    this.mainCameraBlur.StopFilter();
    ((Component) this.mainCameraBlur).GetComponent<RenderTargetCacher>().cacheAfter = false;
    this.skip = false;
    base.Reset();
    ((Behaviour) this).enabled = false;
  }
}

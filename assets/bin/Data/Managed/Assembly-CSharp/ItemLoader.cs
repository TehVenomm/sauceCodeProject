// Decompiled with JetBrains decompiler
// Type: ItemLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemLoader : ModelLoaderBase
{
  private IEnumerator coroutine;
  private LoadingQueue loadingQueue;
  private System.Action callback;
  private int sexID = -1;
  private int faceID = -1;
  private int faceModelID = -1;

  public override bool IsLoading() => this.isLoading;

  public override Animator GetAnimator() => throw new NotImplementedException();

  public override Transform GetHead() => throw new NotImplementedException();

  public override void SetEnabled(bool is_enable) => throw new NotImplementedException();

  public Transform _transform { get; private set; }

  public Transform nodeMain { get; private set; }

  public Transform nodeSub { get; private set; }

  public uint equipItemID { get; private set; }

  public uint itemID { get; private set; }

  public uint skillItemID { get; private set; }

  public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo displayInfo { get; private set; }

  public bool isLoading => this.coroutine != null;

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this.Clear();
  }

  public void Load(
    SortCompareData data,
    Transform parent,
    int layer,
    int sex_id,
    int face_id,
    System.Action _callback = null)
  {
    switch (data)
    {
      case EquipItemSortData _:
      case SmithCreateSortData _:
        this.LoadEquip(data.GetTableID(), parent, layer, sex_id, face_id, _callback);
        break;
      case ItemSortData _:
        this.LoadItem(data.GetTableID(), parent, layer, _callback);
        break;
      case SkillItemSortData _:
        this.LoadSkillItem(data.GetTableID(), parent, layer, _callback);
        break;
      default:
        this.Clear();
        break;
    }
  }

  public void LoadEquip(
    uint equip_item_id,
    Transform parent,
    int layer,
    int sex_id,
    int face_id,
    System.Action _callback = null)
  {
    if ((int) this.equipItemID == (int) equip_item_id && this.sexID == sex_id && this.faceID == face_id)
    {
      if (_callback == null)
        return;
      _callback();
    }
    else
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id);
      if (equipItemData != null)
      {
        switch (equipItemData.type)
        {
          case EQUIPMENT_TYPE.ARMOR:
          case EQUIPMENT_TYPE.VISUAL_ARMOR:
            this.Init(this.DoLoadFullBody(equipItemData), parent, layer, sex_id, face_id, _callback);
            break;
          case EQUIPMENT_TYPE.HELM:
          case EQUIPMENT_TYPE.VISUAL_HELM:
            this.Init(this.DoLoadHelm(equipItemData), parent, layer, sex_id, face_id, _callback);
            break;
          case EQUIPMENT_TYPE.ARM:
          case EQUIPMENT_TYPE.VISUAL_ARM:
            this.Init(this.DoLoadFullBody(equipItemData), parent, layer, sex_id, face_id, _callback);
            break;
          case EQUIPMENT_TYPE.LEG:
          case EQUIPMENT_TYPE.VISUAL_LEG:
            this.Init(this.DoLoadFullBody(equipItemData), parent, layer, sex_id, face_id, _callback);
            break;
          default:
            this.Init(this.DoLoadWeapon(equipItemData), parent, layer, sex_id, face_id, _callback);
            break;
        }
        this.equipItemID = equip_item_id;
      }
      else
        this.Clear();
    }
  }

  public void LoadItem(uint item_id, Transform parent, int layer, System.Action _callback = null)
  {
    if ((int) this.itemID == (int) item_id)
      return;
    if (1000000U > item_id)
    {
      if (1U != item_id && 2U != item_id)
        item_id = 2U;
      this.Init(this.DoLoadItem(item_id), parent, layer, -1, -1, _callback);
    }
    else
      this.Init(this.DoLoadItem(Singleton<ItemTable>.I.GetItemData(item_id)), parent, layer, -1, -1, _callback);
    this.itemID = item_id;
  }

  public void LoadSkillItem(uint skill_item_id, Transform parent, int layer, System.Action _callback = null)
  {
    if ((int) this.skillItemID == (int) skill_item_id)
      return;
    SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id);
    if (skillItemData != null)
    {
      this.Init(this.DoLoadSkillItem(skillItemData), parent, layer, -1, -1, _callback);
      this.skillItemID = skill_item_id;
    }
    else
      this.Clear();
  }

  public void LoadSkillItemSymbol(
    uint skill_item_id,
    Transform parent,
    int layer,
    System.Action _callback = null)
  {
    if ((int) this.itemID == (int) skill_item_id)
      return;
    SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id);
    if (skillItemData != null)
    {
      if (skillItemData.iconID <= 0)
      {
        this.Clear();
      }
      else
      {
        this.Init(this.DoLoadSkillItemSymbol(skillItemData), parent, layer, -1, -1, _callback);
        this.itemID = skill_item_id;
      }
    }
    else
      this.Clear();
  }

  public void LoadAccessory(uint accessory_id, Transform parent, int layer, System.Action _callback = null)
  {
    if ((int) this.itemID == (int) accessory_id)
      return;
    Singleton<AccessoryTable>.I.GetData(accessory_id);
    this.Init(this.DoLoadAccessory(accessory_id), parent, layer, -1, -1, _callback);
    this.itemID = accessory_id;
  }

  private void Init(
    IEnumerator _coroutine,
    Transform parent,
    int layer,
    int sex_id,
    int face_id,
    System.Action _callback)
  {
    this.Clear();
    this.sexID = sex_id != -1 ? sex_id : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    this.faceID = face_id;
    this.faceModelID = face_id != -1 ? MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetFaceModelID(this.sexID, face_id) : MonoBehaviourSingleton<UserInfoManager>.I.GetFaceModelID();
    this.callback = _callback;
    this.loadingQueue = new LoadingQueue((MonoBehaviour) this);
    ((Component) this._transform).gameObject.layer = layer;
    this.coroutine = _coroutine;
    this.StartCoroutine(_coroutine);
  }

  public void Clear()
  {
    for (int index = this._transform.childCount - 1; index >= 0; --index)
      Object.Destroy((Object) ((Component) this._transform.GetChild(index)).gameObject);
    if (this.coroutine != null)
    {
      this.StopCoroutine(this.coroutine);
      this.coroutine = (IEnumerator) null;
    }
    this.nodeSub = (Transform) null;
    this.nodeMain = (Transform) null;
    this.callback = (System.Action) null;
    this.equipItemID = 0U;
    this.itemID = uint.MaxValue;
    this.skillItemID = 0U;
    this.loadingQueue = (LoadingQueue) null;
    this.sexID = -1;
    this.faceID = -1;
    this.faceModelID = 0;
  }

  private void OnDisable()
  {
    if (AppMain.isApplicationQuit)
      return;
    this.Clear();
  }

  private IEnumerator DoLoadWeapon(EquipItemTable.EquipItemData data)
  {
    int modelId = data.GetModelID(this.sexID);
    string playerWeapon = ResourceName.GetPlayerWeapon(modelId);
    byte highTex = 0;
    if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      highTex = MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable.GetWeaponFlag(modelId);
    LoadObject lo = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_WEAPON, playerWeapon);
    LoadObject lo_high_reso_tex = PlayerLoader.LoadHighResoTexs(this.loadingQueue, playerWeapon, (int) highTex);
    yield return (object) this.loadingQueue.Wait();
    Transform equipItemRoot = lo.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    equipItemRoot.localPosition = Vector3.zero;
    equipItemRoot.localRotation = Quaternion.identity;
    Renderer[] componentsInChildren = ((Component) equipItemRoot).GetComponentsInChildren<Renderer>();
    PlayerLoader.SetEquipColor3(componentsInChildren, NGUIMath.IntToColor(data.modelColor0), NGUIMath.IntToColor(data.modelColor1), NGUIMath.IntToColor(data.modelColor2));
    Material materialR = (Material) null;
    Material materialL = (Material) null;
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
    {
      Renderer renderer = componentsInChildren[index];
      if (((Object) renderer).name.EndsWith("_L"))
      {
        materialL = renderer.material;
        this.nodeSub = ((Component) renderer).transform;
      }
      else
      {
        materialR = renderer.material;
        this.nodeMain = ((Component) renderer).transform;
      }
    }
    yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(this.loadingQueue, equipItemRoot));
    PlayerLoader.ApplyWeaponHighResoTexs(lo_high_reso_tex, (int) highTex, materialR, materialL);
    this.displayInfo = new GlobalSettingsManager.UIModelRenderingParam.DisplayInfo(MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.WeaponDisplayInfos[(int) data.type]);
    if (data.id == 50020201U || data.id == 50020200U)
    {
      this.displayInfo.mainPos = new Vector3(0.0f, 0.0f, -0.21f);
      this.displayInfo.mainRot.x += 180f;
      this.displayInfo.subRot.x += 180f;
    }
    if (data.id == 60020200U || data.id == 60020201U || data.id == 60020202U)
    {
      this.displayInfo.mainPos = new Vector3(0.0f, 0.0f, 0.0f);
      this.displayInfo.mainRot = new Vector3(-64.50903f, 93.68915f, -118.1268f);
    }
    this.OnLoadFinished();
  }

  private IEnumerator DoLoadFullBody(EquipItemTable.EquipItemData data)
  {
    EquipModelTable.Data model_data = data.GetModelData(this.sexID);
    bool is_bdy = data.type == EQUIPMENT_TYPE.ARMOR || data.type == EQUIPMENT_TYPE.VISUAL_ARMOR;
    bool is_arm = data.type == EQUIPMENT_TYPE.ARM || data.type == EQUIPMENT_TYPE.VISUAL_ARM;
    bool is_leg = data.type == EQUIPMENT_TYPE.LEG || data.type == EQUIPMENT_TYPE.VISUAL_LEG;
    int sexId = this.sexID;
    int faceModelId = this.faceModelID;
    int id1 = -1;
    int id2 = is_bdy ? data.GetModelID(sexId) : MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinBodyIDs[sexId];
    int id3 = is_arm ? data.GetModelID(sexId) : -1;
    int id4 = is_leg ? data.GetModelID(sexId) : MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinLegIDs[sexId];
    Color mannequin_color = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinSkinColor;
    Color skin_color = mannequin_color;
    Color hair_color = mannequin_color;
    Color equip_color = NGUIMath.IntToColor(data.modelColor0);
    LoadObject lo_face = model_data.needFace ? (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_FACE, ResourceName.GetPlayerFace(faceModelId)) : (LoadObject) null;
    LoadObject lo_hair = id1 > -1 ? (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_HEAD, ResourceName.GetPlayerHead(id1)) : (LoadObject) null;
    LoadObject lo_body = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_BDY, ResourceName.GetPlayerBody(id2));
    LoadObject lo_arm = !model_data.needArm || id3 <= -1 ? (LoadObject) null : (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ARM, ResourceName.GetPlayerArm(id3));
    LoadObject lo_leg = !model_data.needLeg || id4 <= -1 ? (LoadObject) null : (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_LEG, ResourceName.GetPlayerLeg(id4));
    yield return (object) this.loadingQueue.Wait();
    Transform body = lo_body.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    body.localPosition = Vector3.zero;
    body.localRotation = Quaternion.identity;
    PlayerLoader.SetSkinAndEquipColor(body, skin_color, is_bdy ? equip_color : mannequin_color, 0.0f);
    if (!is_bdy)
      this._SetMannequinMaterial(body);
    else
      yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(this.loadingQueue, body));
    this.nodeMain = body;
    SkinnedMeshRenderer componentInChildren = ((Component) body).GetComponentInChildren<SkinnedMeshRenderer>();
    Transform parent = Utility.Find(body, "Head");
    Transform transform1 = Utility.Find(body, "L_Upperarm");
    Transform transform2 = Utility.Find(body, "R_Upperarm");
    if (Object.op_Inequality((Object) transform1, (Object) null) && Object.op_Inequality((Object) transform2, (Object) null))
    {
      Vector3 localEulerAngles1 = transform1.localEulerAngles;
      localEulerAngles1.y = -40f;
      transform1.localEulerAngles = localEulerAngles1;
      Vector3 localEulerAngles2 = transform2.localEulerAngles;
      localEulerAngles2.y = -40f;
      transform2.localEulerAngles = localEulerAngles2;
    }
    if (lo_face != null)
    {
      Transform t = lo_face.Realizes(parent, ((Component) this._transform).gameObject.layer);
      PlayerLoader.SetSkinColor(t, skin_color);
      this._SetMannequinMaterial(t);
    }
    if (lo_hair != null)
    {
      Transform t = lo_hair.Realizes(parent, ((Component) this._transform).gameObject.layer);
      PlayerLoader.SetEquipColor(t, hair_color);
      this._SetMannequinMaterial(t);
    }
    if (lo_arm != null)
    {
      Transform t = PlayerLoader.AddSkin(lo_arm, componentInChildren, ((Component) this._transform).gameObject.layer);
      PlayerLoader.SetSkinAndEquipColor(t, skin_color, is_arm ? equip_color : mannequin_color, is_arm ? model_data.GetZBias() : 0.0001f);
      if (is_arm)
        PlayerLoader.InvisibleBodyTriangles((int) model_data.bodyDraw, componentInChildren);
      if (!is_arm)
        this._SetMannequinMaterial(t);
    }
    if (lo_leg != null)
    {
      Transform t = PlayerLoader.AddSkin(lo_leg, componentInChildren, ((Component) this._transform).gameObject.layer);
      PlayerLoader.SetSkinAndEquipColor(t, skin_color, is_leg ? equip_color : mannequin_color, is_leg ? model_data.GetZBias() : 0.0001f);
      if (!is_leg)
        this._SetMannequinMaterial(t);
    }
    if (is_bdy)
      this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.armorDisplayInfo;
    else if (is_arm)
      this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.armDisplayInfo;
    else if (is_leg)
      this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.legDisplayInfo;
    this.OnLoadFinished();
  }

  private void _SetMannequinMaterial(Transform t)
  {
    Renderer[] componentsInChildren = ((Component) t).GetComponentsInChildren<Renderer>();
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
      componentsInChildren[index].material = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinMaterial;
  }

  private IEnumerator DoLoadHelm(EquipItemTable.EquipItemData data)
  {
    EquipModelTable.Data modelData = data.GetModelData(this.sexID);
    LoadObject lo_head = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_HEAD, ResourceName.GetPlayerHead(data.GetModelID(this.sexID)));
    LoadObject lo_face = (LoadObject) null;
    if (modelData.needFace)
      lo_face = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_FACE, ResourceName.GetPlayerFace(this.faceModelID));
    yield return (object) this.loadingQueue.Wait();
    Transform head = lo_head.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    head.localPosition = Vector3.zero;
    head.localRotation = Quaternion.identity;
    PlayerLoader.SetEquipColor(head, NGUIMath.IntToColor(data.modelColor0));
    this.nodeMain = head;
    yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(this.loadingQueue, head));
    if (lo_face != null)
      this._SetMannequinMaterial(lo_face.Realizes(head, ((Component) this._transform).gameObject.layer));
    this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.helmDisplayInfo;
    this.OnLoadFinished();
  }

  private IEnumerator DoLoadItem(ItemTable.ItemData data) => this.DoLoadItem((uint) data.iconID);

  private IEnumerator DoLoadItem(uint itemID)
  {
    LoadObject lo = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel((int) itemID));
    yield return (object) this.loadingQueue.Wait();
    Transform transform = lo.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
    this.nodeMain = transform;
    this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
    this.OnLoadFinished();
  }

  private IEnumerator DoLoadSkillItem(SkillItemTable.SkillItemData data)
  {
    LoadObject lo = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(data.modelID));
    yield return (object) this.loadingQueue.Wait();
    Transform t = lo.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    t.localPosition = Vector3.zero;
    t.localRotation = Quaternion.identity;
    PlayerLoader.SetEquipColor(t, data.modelColor.ToColor());
    this.nodeMain = t;
    this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
    this.OnLoadFinished();
  }

  private IEnumerator DoLoadSkillItemSymbol(SkillItemTable.SkillItemData data)
  {
    LoadObject lo = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemSymbolModel(data.iconID));
    yield return (object) this.loadingQueue.Wait();
    Transform transform = lo.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
    this.nodeMain = transform;
    this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
    this.OnLoadFinished();
  }

  private IEnumerator DoLoadAccessory(uint accessoryID)
  {
    LoadObject lo = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, ResourceName.GetPlayerAccessory(accessoryID));
    yield return (object) this.loadingQueue.Wait();
    Transform equipItemRoot = lo.Realizes(this._transform, ((Component) this._transform).gameObject.layer);
    equipItemRoot.localPosition = Vector3.zero;
    equipItemRoot.localRotation = Quaternion.identity;
    this.nodeMain = equipItemRoot;
    yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(this.loadingQueue, equipItemRoot));
    this.displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
    this.OnLoadFinished();
  }

  private void OnLoadFinished()
  {
    this.ApplyDisplayInfo();
    if (this.coroutine != null)
    {
      this.StopCoroutine(this.coroutine);
      this.coroutine = (IEnumerator) null;
    }
    ShaderGlobal.ChangeWantUIShader(((Component) this).GetComponentsInChildren<Renderer>());
    if (this.callback == null)
      return;
    this.callback();
  }

  public void ApplyDisplayInfo()
  {
    if (this.displayInfo == null)
      return;
    if (Object.op_Inequality((Object) this.nodeMain, (Object) null))
    {
      this.nodeMain.localPosition = this.displayInfo.mainPos;
      this.nodeMain.localEulerAngles = this.displayInfo.mainRot;
    }
    if (!Object.op_Inequality((Object) this.nodeSub, (Object) null))
      return;
    this.nodeSub.localPosition = this.displayInfo.subPos;
    this.nodeSub.localEulerAngles = this.displayInfo.subRot;
  }

  public static IEnumerator InitRoopEffect(
    LoadingQueue queue,
    Transform equipItemRoot,
    SHADER_TYPE shaderType = SHADER_TYPE.NORMAL)
  {
    EffectPlayProcessor processor = ((Component) equipItemRoot).gameObject.GetComponentInChildren<EffectPlayProcessor>();
    if (Object.op_Inequality((Object) processor, (Object) null) && processor.effectSettings != null)
    {
      int index = 0;
      for (int length = processor.effectSettings.Length; index < length; ++index)
      {
        if (!string.IsNullOrEmpty(processor.effectSettings[index].effectName))
          queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, processor.effectSettings[index].effectName);
      }
    }
    yield return (object) queue.Wait();
    if (Object.op_Inequality((Object) processor, (Object) null))
    {
      List<Transform> transformList = processor.PlayEffect("InitRoop");
      if (transformList != null)
      {
        for (int index = 0; index < transformList.Count; ++index)
        {
          Utility.SetLayerWithChildren(transformList[index], ((Component) equipItemRoot).gameObject.layer);
          if (shaderType != SHADER_TYPE.NORMAL)
          {
            Renderer[] componentsInChildren = ((Component) transformList[index]).GetComponentsInChildren<Renderer>();
            if (shaderType == SHADER_TYPE.LIGHTWEIGHT)
              ShaderGlobal.ChangeWantLightweightShader(componentsInChildren);
            else if (shaderType == SHADER_TYPE.UI)
              ShaderGlobal.ChangeWantUIShader(componentsInChildren);
          }
        }
      }
    }
  }
}

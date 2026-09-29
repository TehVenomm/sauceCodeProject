// Decompiled with JetBrains decompiler
// Type: UIModelRenderTexture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UIModelRenderTexture : MonoBehaviour
{
  public const float NORMAL_FOV = 45f;
  public const float CHARA_FOV = 10f;
  public const float MAGI_FOV = 35f;
  public const float MAGI_SYMBOL_FOV = 13f;
  public const float ACCESSORY_FOV = 45f;
  public const float NORMAL_ROTATE_SPEED = 12f;
  public const float MAGI_ROTATE_SPEED = 22f;
  public const float ACCESSORY_ROTATE_SPEED = 22f;
  private UITexture uiTexture;
  private Transform model;
  private Vector3 modelPos;
  private Vector3 modelRot = new Vector3(0.0f, 180f, 0.0f);
  private float cameraFOV;
  private float rotateSpeed = 12f;
  private bool lightRotation;
  private PlayerLoader playerLoader;
  private PlayerLoadInfo playerLoadInfo;
  private int playerAnimID = -1;
  private bool isPriorityVisualEquip = true;
  private NPCLoader npcLoader;
  private NPCTable.NPCData npcData;
  private ItemLoader itemLoader;
  private int equipItemID = -1;
  private int skillItemID = -1;
  private int skillSymbolItemID = -1;
  private int itemID = -1;
  private int referenceSexID = -1;
  private int referenceFaceID = -1;
  private int accessoryID = -1;
  private EnemyLoader enemyLoader;
  private int enemyID = -1;
  private string foundationName;
  private OutGameSettingsManager.EnemyDisplayInfo enemyDispplayInfo;
  private AudioObject audioObject;
  private bool isEnemyHowl;
  private UIRenderTexture uiRenderTexture;
  private IEnumerator coroutine;
  private int modelLayer;
  private float uiModelScale = 1f;
  private Action<PlayerLoader> onPlayerLoadFinishedCallBack;
  private Action<NPCLoader> onNPCLoadFinishedCallBack;
  private OutGameSettingsManager.EnemyDisplayInfo.SCENE targetScene;
  private bool oneshot;
  private bool isRandomPlaying;
  private const float crossFadeTime = 0.5f;

  public static UIModelRenderTexture Get(Transform t)
  {
    UIModelRenderTexture modelRenderTexture = ((Component) t).GetComponent<UIModelRenderTexture>();
    if (Object.op_Equality((Object) modelRenderTexture, (Object) null))
      modelRenderTexture = ((Component) t).gameObject.AddComponent<UIModelRenderTexture>();
    return modelRenderTexture;
  }

  public EnemyAnimCtrl enemyAnimCtrl { get; private set; }

  public void SetRotateSpeed(float val) => this.rotateSpeed = val;

  public void InitPlayer(
    UITexture ui_tex,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback)
  {
    if (Object.op_Inequality((Object) this.playerLoader, (Object) null) && this.playerLoader.isLoading || this.playerLoadInfo != null && this.playerLoadInfo.Equals(info) && this.isPriorityVisualEquip == is_priority_visual_equip)
      return;
    this.InitPlayerInFact(ui_tex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  public void InitPlayerOneShot(
    UITexture ui_tex,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback)
  {
    this.oneshot = true;
    this.InitPlayer(ui_tex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  public void ForceInitPlayer(
    UITexture ui_tex,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback)
  {
    if (this.playerLoadInfo != null && this.playerLoadInfo.Equals(info) && this.isPriorityVisualEquip == is_priority_visual_equip)
    {
      if (onload_callback == null)
        return;
      onload_callback((PlayerLoader) null);
    }
    else
    {
      int num1 = this.IsLoadingPlayer() ? 1 : 0;
      bool flag = Object.op_Inequality((Object) this.npcLoader, (Object) null);
      int num2 = flag ? 1 : 0;
      if ((num1 | num2) != 0)
      {
        if (flag)
          this.npcData = (NPCTable.NPCData) null;
        this.DeleteModel();
      }
      this.InitPlayerInFact(ui_tex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
    }
  }

  public void InitPlayerInFact(
    UITexture ui_tex,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback)
  {
    this.isPriorityVisualEquip = is_priority_visual_equip;
    this.Init(ui_tex, 10f, UIModelRenderTexture.LOADER_TYPE.PLAYER);
    if (Object.op_Inequality((Object) this.uiRenderTexture, (Object) null) && (double) this.uiRenderTexture.nearClipPlane == -1.0 && (double) pos.z > 13.0)
      this.uiRenderTexture.nearClipPlane = pos.z - 5f;
    this.playerLoadInfo = info;
    this.modelPos = pos;
    this.modelRot = rot;
    this.playerAnimID = anim_id;
    this.onPlayerLoadFinishedCallBack = onload_callback;
    this.playerLoader.StartLoad(info, this.modelLayer, this.playerAnimID, false, false, false, false, false, false, true, true, SHADER_TYPE.UI, new PlayerLoader.OnCompleteLoad(this.OnPlayerLoadFinished));
    this.LoadStart();
  }

  public bool IsLoadingPlayer()
  {
    return Object.op_Inequality((Object) this.playerLoader, (Object) null) && this.playerLoader.isLoading;
  }

  public void InitNPC(
    UITexture ui_tex,
    int npc_id,
    Vector3 pos,
    Vector3 rot,
    float fov,
    Action<NPCLoader> onload_callback)
  {
    if (Object.op_Inequality((Object) this.npcLoader, (Object) null) && this.npcLoader.isLoading)
      return;
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(npc_id);
    if (npcData == null || npcData == this.npcData)
      return;
    if (Object.op_Inequality((Object) this.playerLoader, (Object) null))
    {
      this.playerLoadInfo = (PlayerLoadInfo) null;
      this.DeleteModel();
    }
    this.Init(ui_tex, (double) fov != -1.0 ? fov : 10f, UIModelRenderTexture.LOADER_TYPE.NPC);
    if (Object.op_Inequality((Object) this.uiRenderTexture, (Object) null) && (double) this.uiRenderTexture.nearClipPlane == -1.0 && ((double) fov >= 40.0 && (double) pos.z > 13.0 || (double) fov < 40.0 && (double) pos.z > 5.0))
      this.uiRenderTexture.nearClipPlane = pos.z - 5f;
    this.npcData = npcData;
    this.modelPos = pos;
    this.modelRot = rot;
    this.cameraFOV = fov;
    this.onNPCLoadFinishedCallBack = onload_callback;
    int num1 = npcData.specialModelID > 0 ? npcData.specialModelID : npcData.npcModelID;
    HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(Singleton<HomeThemeTable>.I.CurrentHomeTheme);
    int num2 = -1;
    if (homeThemeData != null)
      num2 = Singleton<HomeThemeTable>.I.GetNpcModelID(homeThemeData, npcData.id);
    this.npcLoader.Load(num2 > 0 ? num2 : num1, this.modelLayer, false, false, SHADER_TYPE.UI, new System.Action(this.OnNPCLoadFinished));
    this.LoadStart();
  }

  public bool IsLoadingNPC()
  {
    return Object.op_Inequality((Object) this.npcLoader, (Object) null) && this.npcLoader.isLoading;
  }

  public void Init(UITexture ui_tex, SortCompareData data)
  {
    this.Init(ui_tex, 45f);
    switch (data)
    {
      case EquipItemSortData _:
      case SmithCreateSortData _:
        this.InitEquip(ui_tex, data.GetTableID(), this.referenceSexID, this.referenceFaceID, this.uiModelScale);
        break;
      case ItemSortData _:
        this.InitItem(ui_tex, data.GetTableID());
        break;
      case SkillItemSortData _:
        this.InitSkillItem(ui_tex, data.GetTableID());
        break;
    }
  }

  public void InitEquip(
    UITexture ui_tex,
    uint equip_item_id,
    int sex_id,
    int face_id,
    float scale)
  {
    this.referenceSexID = sex_id;
    this.referenceFaceID = face_id;
    this.Init(ui_tex, 45f);
    this.equipItemID = (int) equip_item_id;
    this.uiModelScale = scale;
    this.itemLoader.LoadEquip(equip_item_id, this.model, this.modelLayer, this.referenceSexID, this.referenceFaceID, new System.Action(this.OnLoadFinished));
    this.LoadStart();
  }

  public void InitItem(UITexture ui_tex, uint item_id, bool rotation = true)
  {
    this.Init(ui_tex, 45f);
    this.itemID = (int) item_id;
    this.itemLoader.LoadItem(item_id, this.model, this.modelLayer, new System.Action(this.OnLoadFinished));
    this.LoadStart();
    if (rotation)
      this.rotateSpeed = 12f;
    else
      this.rotateSpeed = 0.0f;
  }

  public void InitSkillItem(
    UITexture ui_tex,
    uint skill_item_id,
    bool rotation = true,
    bool light_rotation = false,
    float fov = 35f)
  {
    this.Init(ui_tex, fov);
    this.skillItemID = (int) skill_item_id;
    this.itemLoader.LoadSkillItem(skill_item_id, this.model, this.modelLayer, new System.Action(this.OnLoadFinished));
    this.LoadStart();
    this.rotateSpeed = !rotation ? 0.0f : 22f;
    this.lightRotation = light_rotation;
  }

  public void InitSkillItemSymbol(UITexture ui_tex, uint skill_item_id, bool rotation = true, float fov = 13f)
  {
    this.Init(ui_tex, fov);
    this.skillSymbolItemID = (int) skill_item_id;
    this.itemLoader.LoadSkillItemSymbol(skill_item_id, this.model, this.modelLayer, new System.Action(this.OnLoadFinished));
    BlurFilter blurFilter = ((Component) this).gameObject.GetComponent<BlurFilter>();
    if (Object.op_Equality((Object) blurFilter, (Object) null))
    {
      blurFilter = ((Component) this).gameObject.AddComponent<BlurFilter>();
      blurFilter.downSample = 1;
      blurFilter.blurStrength = 0.4f;
    }
    this.uiRenderTexture.postEffectFilter = (FilterBase) blurFilter;
    this.LoadStart();
    if (rotation)
      this.rotateSpeed = 22f;
    else
      this.rotateSpeed = 0.0f;
  }

  public void InitEnemy(
    UITexture ui_tex,
    uint enemy_id,
    string foundation_name,
    OutGameSettingsManager.EnemyDisplayInfo.SCENE target_scene,
    Action<bool, EnemyLoader> callback = null,
    UIModelRenderTexture.ENEMY_MOVE_TYPE moveType = UIModelRenderTexture.ENEMY_MOVE_TYPE.DEFULT,
    bool is_Howl = true)
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemy_id);
    if (enemyData == null)
    {
      this.Clear();
      if (callback == null)
        return;
      callback(false, (EnemyLoader) null);
    }
    else
    {
      this.Init(ui_tex, 45f, UIModelRenderTexture.LOADER_TYPE.ENEMY);
      this.enemyID = (int) enemy_id;
      this.foundationName = foundation_name;
      this.targetScene = target_scene;
      this.isEnemyHowl = is_Howl;
      int animId = enemyData.animId;
      float scale = enemyData.modelScale;
      this.enemyDispplayInfo = this.targetScene != OutGameSettingsManager.EnemyDisplayInfo.SCENE.QUEST ? MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData) : MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForQuestSelect(enemyData);
      if (this.enemyDispplayInfo != null)
      {
        if (this.enemyDispplayInfo.animID > 0)
          animId = this.enemyDispplayInfo.animID;
        scale = this.enemyDispplayInfo.scale;
      }
      this.enemyLoader.StartLoad(enemyData.modelId, animId, scale, enemyData.baseEffectName, enemyData.baseEffectNode, false, false, true, SHADER_TYPE.UI, this.modelLayer, foundation_name, callback: (EnemyLoader.OnCompleteLoad) (enemy =>
      {
        if (callback != null)
          callback(true, this.enemyLoader);
        if (Object.op_Inequality((Object) this.enemyLoader, (Object) null) && Object.op_Inequality((Object) this.enemyLoader.animator, (Object) null))
        {
          switch (moveType)
          {
            case UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE:
              this.enemyLoader.animator.applyRootMotion = false;
              break;
            case UIModelRenderTexture.ENEMY_MOVE_TYPE.STOP:
              this.enemyLoader.animator.speed = 0.0f;
              break;
          }
        }
        this.OnEnemyLoadFinished(enemy);
      }));
      this.LoadStart();
    }
  }

  public void InitAccessory(
    UITexture ui_tex,
    uint accessory_id,
    float scale,
    bool rotation = true,
    bool light_rotation = false)
  {
    this.Init(ui_tex, 45f);
    this.accessoryID = (int) accessory_id;
    this.uiModelScale = scale;
    this.itemLoader.LoadAccessory(accessory_id, this.model, this.modelLayer, new System.Action(this.OnLoadFinished));
    this.LoadStart();
    this.rotateSpeed = rotation ? 22f : 0.0f;
    this.lightRotation = light_rotation;
  }

  private void Init(UITexture ui_tex, float fov, UIModelRenderTexture.LOADER_TYPE loader_type = UIModelRenderTexture.LOADER_TYPE.ITEM)
  {
    if (!Object.op_Equality((Object) this.model, (Object) null))
      return;
    this.uiTexture = ui_tex;
    this.uiRenderTexture = UIRenderTexture.Get(ui_tex, fov);
    this.model = Utility.CreateGameObject("UIModel", this.uiRenderTexture.modelTransform, this.uiRenderTexture.renderLayer);
    switch (loader_type)
    {
      case UIModelRenderTexture.LOADER_TYPE.PLAYER:
        this.playerLoader = ((Component) this.model).gameObject.AddComponent<PlayerLoader>();
        break;
      case UIModelRenderTexture.LOADER_TYPE.NPC:
        this.npcLoader = ((Component) this.model).gameObject.AddComponent<NPCLoader>();
        break;
      case UIModelRenderTexture.LOADER_TYPE.ITEM:
        this.itemLoader = ((Component) this.model).gameObject.AddComponent<ItemLoader>();
        break;
      case UIModelRenderTexture.LOADER_TYPE.ENEMY:
        this.enemyLoader = ((Component) this.model).gameObject.AddComponent<EnemyLoader>();
        break;
    }
    this.modelLayer = this.uiRenderTexture.renderLayer;
  }

  private void OnLoadFinished()
  {
    if (Object.op_Equality((Object) this.model, (Object) null))
      return;
    if (!((Component) this).gameObject.activeInHierarchy)
    {
      this.DeleteModel();
    }
    else
    {
      if (this.coroutine != null)
      {
        this.StopCoroutine(this.coroutine);
        this.coroutine = (IEnumerator) null;
      }
      if ((double) this.uiModelScale != 1.0)
        this.model.localScale = new Vector3(this.uiModelScale, this.uiModelScale, this.uiModelScale);
      this.coroutine = this.DoViewing();
      this.StartCoroutine(this.coroutine);
    }
  }

  private void OnPlayerLoadFinished(object o)
  {
    this.OnLoadFinished();
    if (this.onPlayerLoadFinishedCallBack == null)
      return;
    this.onPlayerLoadFinishedCallBack(this.playerLoader);
  }

  private void OnNPCLoadFinished()
  {
    this.OnLoadFinished();
    if (this.onNPCLoadFinishedCallBack == null)
      return;
    this.onNPCLoadFinishedCallBack(this.npcLoader);
  }

  private void OnEnemyLoadFinished(Enemy o)
  {
    this.OnLoadFinished();
    if (Object.op_Equality((Object) this.enemyLoader, (Object) null) || Object.op_Equality((Object) this.enemyLoader.body, (Object) null))
      return;
    ((Component) this.enemyLoader.body).GetComponentsInChildren<Renderer>(Temporary.rendererList);
    int index1 = 0;
    for (int count = Temporary.rendererList.Count; index1 < count; ++index1)
    {
      Renderer renderer = Temporary.rendererList[index1];
      switch (renderer)
      {
        case MeshRenderer _:
        case SkinnedMeshRenderer _:
          Material[] materials = renderer.materials;
          int index2 = 0;
          for (int length = materials.Length; index2 < length; ++index2)
          {
            Material material = materials[index2];
            if (Object.op_Inequality((Object) material, (Object) null))
            {
              string name = ((Object) material.shader).name;
              if (name.Contains("cut"))
              {
                material.shader = ResourceUtility.FindShader("Transparent/Cutout/Diffuse");
              }
              else
              {
                if (name.Contains("_zako_"))
                  material.shader = ResourceUtility.FindShader(name + "__s");
                else if (this.CanChangeHighQualityShader(name))
                {
                  Shader shader = ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_single_tex__no_cull");
                  material.shader = !material.HasProperty("_CullMode") || material.GetInt("_CullMode") != 0 || !Object.op_Inequality((Object) shader, (Object) null) ? ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_single_tex__s") : shader;
                }
                if (this.enemyLoader.bodyID == 2023)
                  material.shader = ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_reflective_simple");
                else if (this.enemyLoader.bodyID == 2043)
                  material.shader = ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_reflective_for_shadow");
              }
            }
          }
          break;
      }
    }
    Temporary.rendererList.Clear();
  }

  private bool CanChangeHighQualityShader(string currentShaderName)
  {
    return !currentShaderName.Contains("enemy_reflective");
  }

  private void DeleteModel()
  {
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      if (Object.op_Inequality((Object) this.playerLoader, (Object) null))
      {
        this.playerLoader.DeleteLoadedObjects();
        Object.Destroy((Object) this.playerLoader);
        this.playerLoader = (PlayerLoader) null;
      }
      if (Object.op_Inequality((Object) this.npcLoader, (Object) null))
      {
        this.npcLoader.Clear();
        Object.Destroy((Object) this.npcLoader);
        this.npcLoader = (NPCLoader) null;
      }
      if (Object.op_Inequality((Object) this.enemyLoader, (Object) null))
      {
        this.enemyLoader.DeleteLoadedObjects();
        Object.Destroy((Object) this.enemyLoader);
        this.enemyLoader = (EnemyLoader) null;
        this.enemyAnimCtrl = (EnemyAnimCtrl) null;
      }
      if (Object.op_Inequality((Object) this.itemLoader, (Object) null))
      {
        this.itemLoader.Clear();
        Object.Destroy((Object) this.itemLoader);
        this.itemLoader = (ItemLoader) null;
      }
      Object.Destroy((Object) ((Component) this.model).gameObject);
      this.model = (Transform) null;
      if (Object.op_Inequality((Object) this.uiRenderTexture, (Object) null) && Object.op_Inequality((Object) this.uiRenderTexture.postEffectFilter, (Object) null))
        this.uiRenderTexture.postEffectFilter = (FilterBase) null;
    }
    if (this.coroutine == null)
      return;
    this.StopCoroutine(this.coroutine);
    this.coroutine = (IEnumerator) null;
  }

  private void ReloadModel()
  {
    if (Object.op_Equality((Object) this.model, (Object) null))
    {
      if (this.playerLoadInfo != null)
      {
        PlayerLoadInfo playerLoadInfo = this.playerLoadInfo;
        this.playerLoadInfo = (PlayerLoadInfo) null;
        this.InitPlayer(this.uiTexture, playerLoadInfo, this.playerAnimID, this.modelPos, this.modelRot, this.isPriorityVisualEquip, this.onPlayerLoadFinishedCallBack);
      }
      else if (this.npcData != null)
      {
        NPCTable.NPCData npcData = this.npcData;
        this.npcData = (NPCTable.NPCData) null;
        this.InitNPC(this.uiTexture, npcData.id, this.modelPos, this.modelRot, this.cameraFOV, this.onNPCLoadFinishedCallBack);
      }
      else if (this.equipItemID != -1)
        this.InitEquip(this.uiTexture, (uint) this.equipItemID, this.referenceSexID, this.referenceFaceID, this.uiModelScale);
      else if (this.itemID != -1)
        this.InitItem(this.uiTexture, (uint) this.itemID);
      else if (this.skillItemID != -1)
        this.InitSkillItem(this.uiTexture, (uint) this.skillItemID);
      else if (this.skillSymbolItemID != -1)
        this.InitSkillItemSymbol(this.uiTexture, (uint) this.skillSymbolItemID);
      else if (this.enemyID != -1)
      {
        this.InitEnemy(this.uiTexture, (uint) this.enemyID, this.foundationName, this.targetScene);
      }
      else
      {
        if (this.accessoryID == -1)
          return;
        this.InitAccessory(this.uiTexture, (uint) this.accessoryID, this.uiModelScale);
      }
    }
    else if (this.equipItemID != -1)
    {
      if (!Object.op_Inequality((Object) this.itemLoader, (Object) null) || this.itemLoader.IsLoading())
        return;
      this.OnLoadFinished();
    }
    else if (this.itemID != -1)
    {
      if (!Object.op_Inequality((Object) this.itemLoader, (Object) null) || this.itemLoader.IsLoading())
        return;
      this.OnLoadFinished();
    }
    else if (this.skillItemID != -1)
    {
      if (!Object.op_Inequality((Object) this.itemLoader, (Object) null) || this.itemLoader.IsLoading())
        return;
      this.OnLoadFinished();
    }
    else if (this.skillSymbolItemID != -1)
    {
      if (!Object.op_Inequality((Object) this.itemLoader, (Object) null) || this.itemLoader.IsLoading())
        return;
      this.OnLoadFinished();
    }
    else
    {
      if (this.enemyID != -1 || this.accessoryID == -1 || !Object.op_Inequality((Object) this.itemLoader, (Object) null) || this.itemLoader.IsLoading())
        return;
      this.OnLoadFinished();
    }
  }

  public void Clear()
  {
    if (Object.op_Inequality((Object) this.uiRenderTexture, (Object) null))
    {
      this.uiRenderTexture.Release();
      Object.Destroy((Object) this.uiRenderTexture);
      this.uiRenderTexture = (UIRenderTexture) null;
    }
    this.DeleteModel();
    this.playerLoadInfo = (PlayerLoadInfo) null;
    this.npcData = (NPCTable.NPCData) null;
    this.equipItemID = -1;
    this.skillItemID = -1;
    this.skillSymbolItemID = -1;
    this.itemID = -1;
    this.enemyID = -1;
    this.accessoryID = -1;
    this.foundationName = (string) null;
    this.uiTexture = (UITexture) null;
    this.referenceSexID = -1;
    this.referenceFaceID = -1;
  }

  private void OnEnable() => this.ReloadModel();

  private void OnDisable()
  {
    if (AppMain.isApplicationQuit)
      return;
    this.StopAudio(15);
  }

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    this.StopAudio(0);
    this.Clear();
  }

  private void LoadStart()
  {
    if (Object.op_Inequality((Object) this.itemLoader, (Object) null) && !this.itemLoader.isLoading || Object.op_Inequality((Object) this.npcLoader, (Object) null) && !this.npcLoader.isLoading || Object.op_Inequality((Object) this.enemyLoader, (Object) null) && !this.enemyLoader.isLoading)
      return;
    if (this.coroutine != null)
    {
      this.StopCoroutine(this.coroutine);
      this.coroutine = (IEnumerator) null;
    }
    this.uiRenderTexture.enableTexture = false;
  }

  private IEnumerator DoViewing()
  {
    if (Object.op_Equality((Object) this.model, (Object) null))
    {
      Log.Error("model is null!!");
    }
    else
    {
      this.model.localEulerAngles = Vector3.zero;
      this.uiRenderTexture.enableTexture = true;
      float rot_wait = 1f;
      if (Object.op_Inequality((Object) this.playerLoader, (Object) null))
      {
        this.model.localPosition = this.modelPos;
        this.model.localEulerAngles = this.modelRot;
      }
      else if (Object.op_Inequality((Object) this.npcLoader, (Object) null))
      {
        this.model.localPosition = this.modelPos;
        this.model.localEulerAngles = this.modelRot;
      }
      else if (Object.op_Inequality((Object) this.enemyLoader, (Object) null))
      {
        if (this.enemyDispplayInfo == null)
        {
          Bounds bounds = new Bounds();
          int index = 0;
          for (int length = this.enemyLoader.renderersBody.Length; index < length; ++index)
            ((Bounds) ref bounds).Encapsulate(this.enemyLoader.renderersBody[index].bounds);
          float num = (float) ((double) ((Bounds) ref bounds).extents.x * 0.5 / (double) Mathf.Tan(0.3926991f) + 1.0);
          this.model.localPosition = new Vector3(0.0f, ((Bounds) ref bounds).extents.y * -0.5f, num);
          this.model.localEulerAngles = new Vector3(0.0f, 180f, 0.0f);
        }
        else
        {
          this.model.localPosition = new Vector3(0.0f, -0.8f, 5f);
          if (this.enemyDispplayInfo.seIdhowl > 0 && this.isEnemyHowl)
            this.audioObject = SoundManager.PlayUISE(this.enemyDispplayInfo.seIdhowl);
          this.enemyLoader.body.localPosition = this.enemyDispplayInfo.pos;
          this.enemyLoader.body.localEulerAngles = new Vector3(0.0f, this.enemyDispplayInfo.angleY, 0.0f);
          this.enemyAnimCtrl = ((Component) this.model).gameObject.AddComponent<EnemyAnimCtrl>();
          this.enemyAnimCtrl.Init(this.enemyLoader, this.uiRenderTexture.renderCamera);
          Animator animator = this.enemyLoader.GetAnimator();
          if (Object.op_Inequality((Object) animator, (Object) null))
          {
            int hash = Animator.StringToHash("Base Layer.GACHA_HOWL");
            if (animator.HasState(0, hash))
            {
              animator.Play(hash, 0, 0.0f);
              animator.Update(0.0f);
            }
          }
        }
      }
      else if (Object.op_Inequality((Object) this.itemLoader, (Object) null) && this.accessoryID != -1)
        this.model.localEulerAngles = new Vector3(0.0f, 180f, 0.0f);
      Vector3 lightDir = new Vector3(1.19f, -1.59f, -1f);
      Quaternion rotation = Quaternion.AngleAxis(1f, new Vector3(-0.07124705f, 0.0f, -0.9974587f));
      MeshRenderer renderer = (MeshRenderer) null;
      if (this.lightRotation)
        renderer = ((Component) this.model).GetComponentInChildren<MeshRenderer>();
      if (this.oneshot)
      {
        yield return (object) new WaitForEndOfFrame();
        if (Object.op_Inequality((Object) this.uiRenderTexture, (Object) null) && Object.op_Inequality((Object) this.uiRenderTexture.renderCamera, (Object) null))
          ((Behaviour) this.uiRenderTexture.renderCamera).enabled = false;
        this.DeleteModel();
      }
      else
      {
        while (true)
        {
          if (Object.op_Inequality((Object) this.itemLoader, (Object) null))
          {
            this.model.localPosition = new Vector3(0.0f, 0.0f, this.itemLoader.displayInfo.zFromCamera);
            this.itemLoader.ApplyDisplayInfo();
            if ((double) rot_wait <= 0.0)
              this.model.Rotate(Vector3.op_Multiply(new Vector3(0.0f, this.rotateSpeed, 0.0f), Time.deltaTime));
            else
              rot_wait -= Time.deltaTime;
            if (this.lightRotation)
            {
              lightDir = Quaternion.op_Multiply(rotation, lightDir);
              ((Renderer) renderer).material.SetVector("_LightDir", Vector4.op_Implicit(lightDir));
            }
          }
          yield return (object) null;
        }
      }
    }
  }

  public void SetApplyEnemyRootMotion(bool enable)
  {
    if (Object.op_Equality((Object) this.enemyLoader, (Object) null) || Object.op_Equality((Object) this.enemyLoader.animator, (Object) null))
      return;
    this.enemyLoader.animator.applyRootMotion = enable;
  }

  public void PlayRandomEnemyAnimation()
  {
    if (Object.op_Equality((Object) this.enemyLoader, (Object) null) || Object.op_Equality((Object) this.enemyLoader.animator, (Object) null))
      return;
    Object.Destroy((Object) this.enemyAnimCtrl);
    this.StartCoroutine(this._PlayRandomEnemyAnimation());
  }

  private IEnumerator _PlayRandomEnemyAnimation()
  {
    if (!this.isRandomPlaying)
    {
      this.isRandomPlaying = true;
      this.enemyLoader.animator.Play(this.enemyLoader.animEventData.animations[Random.Range(0, this.enemyLoader.animEventData.animations.Length)].name);
      yield return (object) new WaitForSeconds(((AnimatorClipInfo) ref this.enemyLoader.animator.GetCurrentAnimatorClipInfo(0)[0]).clip.length - 0.5f);
      this.isRandomPlaying = false;
      this.enemyLoader.animator.CrossFade("IDLE", 0.5f);
    }
  }

  public Transform GetModelTransform() => this.model;

  public void StopAudio(int fade_count)
  {
    if (!Object.op_Inequality((Object) this.audioObject, (Object) null))
      return;
    this.audioObject.Stop(fade_count);
    this.audioObject = (AudioObject) null;
  }

  private enum LOADER_TYPE
  {
    PLAYER,
    NPC,
    ITEM,
    ENEMY,
  }

  public enum ENEMY_MOVE_TYPE
  {
    DEFULT,
    DONT_MOVE,
    STOP,
  }
}

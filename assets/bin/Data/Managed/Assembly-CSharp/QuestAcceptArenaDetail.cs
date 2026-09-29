// Decompiled with JetBrains decompiler
// Type: QuestAcceptArenaDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptArenaDetail : QuestDeliveryDetail
{
  private const int PREDOWNLOAD_MESSAGE_KEY = 3000;
  private bool isShowDropInfo;
  private ArenaTable.ArenaData arenaData;
  private const string WINDOW_SPRITE = "RequestWindowBase_Arena";
  private const string MESSAGE_SPRITE = "Checkhukidashi_Rush";

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "PointShopGetPointTable";
      yield return "DeliveryRewardTable";
      yield return "FieldMapTable";
      yield return "ArenaTable";
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    this.arenaData = this.info.GetArenaData();
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) QuestAcceptArenaDetail.UI.TEX_RUSH_IMAGE)).GetComponent<UITexture>(), RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(this.arenaData.rank));
    if ((this.isComplete || this.isNotice) && !this.isCompletedEventDelivery)
    {
      this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE, false);
      this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE_OFF, false);
    }
    else
    {
      if (this.info.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
        return;
      this.StartCoroutine(this.StartPredownload());
    }
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_DROP_REWARD, true);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_CLEAR_REWARD, true);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_RANK_UP_ROOT, false);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_COMPLETE_RANK_UP, false);
    base.UpdateUI();
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE_OFF, false);
    this.UpdateTime();
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptArenaDetail.UI.SPR_WINDOW, "RequestWindowBase_Arena");
    this.SetDifficultySprite();
    this.UpdateRewardInfo();
    if (this.info.GetConditionType() != DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
      return;
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE, false);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE_OFF, false);
  }

  private void UpdateTime()
  {
    if (this.info.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
    {
      int have;
      int need;
      MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int) this.info.id, out have, out need, out string _, out string _);
      if (this.isComplete)
        have = need;
      this.SetLabelText((Enum) QuestAcceptArenaDetail.UI.LBL_LIMIT_TIME, $"{have}/{need}");
      this.SetLabelText((Enum) QuestAcceptArenaDetail.UI.LBL_LIMIT_TIME_NAME, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 17U));
    }
    else
    {
      int secByMilliSec = QuestUtility.ToSecByMilliSec(this.arenaData.timeLimit);
      this.SetLabelText((Enum) QuestAcceptArenaDetail.UI.LBL_LIMIT_TIME, $"{secByMilliSec / 60}:{secByMilliSec % 60:D2}");
    }
    this.SetLabelText((Enum) QuestAcceptArenaDetail.UI.LBL_RUSH_LEVEL, "");
  }

  protected override void UpdateNPC(string map_name, string enemy_name)
  {
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.CHARA_ALL, false);
  }

  protected override void SetBaseFrame()
  {
    this.baseRoot = this.GetCtrl((Enum) QuestAcceptArenaDetail.UI.OBJ_BASE_FRAME);
  }

  protected override void SetTargetFrame()
  {
    this.targetFrame = this.GetCtrl((Enum) QuestAcceptArenaDetail.UI.OBJ_TARGET_FRAME);
  }

  protected override void SetSubmissionFrame() => this.submissionFrame = (Transform) null;

  protected override void OnEndCompletetween(bool is_unlock_portal, string effectName)
  {
    base.OnEndCompletetween(is_unlock_portal, effectName);
  }

  private IEnumerator PlayRankUpEffect(bool is_unlock_portal, string effectName)
  {
    ((Renderer) ((Component) ((Component) this.GetCtrl((Enum) QuestAcceptArenaDetail.UI.OBJ_PARTICLE)).GetComponent<ParticleSystem>()).GetComponent<ParticleSystemRenderer>()).sharedMaterial.renderQueue = 4000;
    yield return (object) new WaitForSeconds(1f);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_BASE_FRAME, false);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_COMPLETE_RANK_UP, true);
    this.PlayTween((Enum) QuestAcceptArenaDetail.UI.OBJ_RANK_UP, callback: (EventDelegate.Callback) (() =>
    {
      if (!is_unlock_portal)
        return;
      this.PlayUnlockPortalTween(effectName, (System.Action) (() => { }));
    }), is_input_block: false);
  }

  private void UpdateRewardInfo()
  {
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_CLEAR_ICON_ROOT, !this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_DROP_ICON_ROOT, this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_CLEAR_REWARD, !this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_DROP_REWARD, this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.OBJ_COMPLETE_ROOT, !this.isShowDropInfo);
  }

  private void OnQuery_CREATE()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) this.arenaData.questIds[0]);
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(this.arenaData.id);
    GameSection.SetEventData((object) this.info);
  }

  private IEnumerator StartPredownload()
  {
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE, false);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE_OFF, false);
    List<QuestAcceptArenaDetail.ResourceInfo> list = new List<QuestAcceptArenaDetail.ResourceInfo>();
    List<QuestTable.QuestTableData> questDataArray = this.arenaData.GetQuestDataArray();
    if (!questDataArray.IsNullOrEmpty<QuestTable.QuestTableData>())
    {
      for (int index1 = 0; index1 < questDataArray.Count; ++index1)
      {
        uint mapId = questDataArray[index1].mapId;
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
        if (fieldMapData == null)
          yield break;
        string name = fieldMapData.stageName;
        if (string.IsNullOrEmpty(name))
          name = "ST011D_01";
        StageTable.StageData data = Singleton<StageTable>.I.GetData(name);
        if (data == null)
          yield break;
        list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, data.scene));
        list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, data.sky));
        if (!string.IsNullOrEmpty(data.cameraLinkEffect))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect));
        if (!string.IsNullOrEmpty(data.cameraLinkEffectY0))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0));
        if (!string.IsNullOrEmpty(data.rootEffect))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect));
        for (int index2 = 0; index2 < 8; ++index2)
        {
          if (!string.IsNullOrEmpty(data.useEffects[index2]))
            list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[index2]));
        }
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questDataArray[index1].enemyID[0]);
        int modelId = enemyData.modelId;
        string enemyBody = ResourceName.GetEnemyBody(modelId);
        string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
        string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
        if (!string.IsNullOrEmpty(enemyBody))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
        if (!string.IsNullOrEmpty(enemyMaterial))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
        if (!string.IsNullOrEmpty(enemyAnim))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
        if (!string.IsNullOrEmpty(enemyData.baseEffectName))
          list.Add(new QuestAcceptArenaDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
      }
      if (list.Find((Predicate<QuestAcceptArenaDetail.ResourceInfo>) (x => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName))) != null)
      {
        List<string> assetNames = new List<string>();
        foreach (QuestAcceptArenaDetail.ResourceInfo resourceInfo in list)
        {
          if (!string.IsNullOrEmpty(resourceInfo.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(resourceInfo.category, resourceInfo.packageName))
            assetNames.Add(resourceInfo.category.ToAssetBundleName(resourceInfo.packageName));
        }
        this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE_OFF, true);
        yield return (object) ResourceSizeInfo.Init();
        string act = (string) null;
        yield return (object) ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetAssetsSizeMB(assetNames.ToArray()), 3002U, CommonDialog.TYPE.YES_NO, (Action<string>) (str => act = str));
        if (act == "NO")
        {
          while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
            yield return (object) null;
          this.DispatchEvent("[BACK]");
          yield break;
        }
        LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
        foreach (QuestAcceptArenaDetail.ResourceInfo resourceInfo in list)
        {
          if (!string.IsNullOrEmpty(resourceInfo.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(resourceInfo.category, resourceInfo.packageName))
          {
            ResourceManager.downloadOnly = true;
            loadQueue.Load(resourceInfo.category, resourceInfo.packageName, (string[]) null);
            ResourceManager.downloadOnly = false;
            yield return (object) loadQueue.Wait();
          }
        }
        assetNames = (List<string>) null;
        loadQueue = (LoadingQueue) null;
      }
      bool is_visible = true;
      this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE, is_visible);
      this.SetActive((Enum) QuestAcceptArenaDetail.UI.BTN_CREATE_OFF, !is_visible);
    }
  }

  private void SetDifficultySprite()
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryID);
    this.SetActive((Enum) QuestAcceptArenaDetail.UI.SPR_TYPE_DIFFICULTY, deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD);
  }

  protected new enum UI
  {
    OBJ_BASE_ROOT,
    OBJ_BACK,
    OBJ_COMPLETE_ROOT,
    BTN_COMPLETE,
    CHARA_ALL,
    OBJ_UNLOCK_PORTAL_ROOT,
    LBL_UNLOCK_PORTAL,
    LBL_QUEST_TITLE,
    LBL_CHARA_MESSAGE,
    LBL_PERSON_NAME,
    TEX_NPC,
    BTN_JUMP_QUEST,
    BTN_JUMP_INVALID,
    BTN_JUMP_MAP,
    BTN_JUMP_GACHATOP,
    GRD_REWARD,
    LBL_MONEY,
    LBL_EXP,
    SPR_WINDOW,
    SPR_MESSAGE_BG,
    OBJ_NEED_ITEM_ROOT,
    LBL_NEED_ITEM_NAME,
    LBL_NEED,
    LBL_HAVE,
    LBL_PLACE_NAME,
    LBL_ENEMY_NAME,
    OBJ_DIFFICULTY_ROOT,
    OBJ_ENEMY_NAME_ROOT,
    LBL_GET_PLACE,
    OBJ_ENEMY,
    SPR_ELEMENT_ROOT,
    SPR_ENM_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    BTN_SUBMISSION,
    STR_BTN_SUBMISSION,
    STR_BTN_SUBMISSION_BACK,
    OBJ_TOP_CROWN_ROOT,
    OBJ_TOP_CROWN_1,
    OBJ_TOP_CROWN_2,
    OBJ_TOP_CROWN_3,
    STR_MISSION_EMPTY,
    SPR_CROWN_1,
    SPR_CROWN_2,
    SPR_CROWN_3,
    OBJ_SUBMISSION_ROOT,
    OBJ_MISSION_INFO,
    OBJ_MISSION_INFO_1,
    OBJ_MISSION_INFO_2,
    OBJ_MISSION_INFO_3,
    LBL_MISSION_INFO_1,
    LBL_MISSION_INFO_2,
    LBL_MISSION_INFO_3,
    SPR_MISSION_INFO_CROWN_1,
    SPR_MISSION_INFO_CROWN_2,
    SPR_MISSION_INFO_CROWN_3,
    STR_MISSION,
    OBJ_BASE_FRAME,
    OBJ_TARGET_FRAME,
    OBJ_SUBMISSION_FRAME,
    OBJ_NORMAL_ROOT,
    OBJ_EVENT_ROOT,
    LBL_POINT_NORMAL,
    TEX_NORMAL_ICON,
    LBL_POINT_EVENT,
    TEX_EVENT_ICON,
    BTN_CREATE,
    BTN_JOIN,
    LBL_LIMIT_TIME,
    OBJ_CHANGE_INFO,
    BTN_CHANGE_INFO,
    OBJ_DROP_ICON_ROOT,
    OBJ_CLEAR_ICON_ROOT,
    OBJ_DROP_REWARD,
    OBJ_CLEAR_REWARD,
    GRD_DROP_REWARD,
    LBL_RUSH_LEVEL,
    TEX_RUSH_IMAGE,
    BTN_CREATE_OFF,
    OBJ_RANK_UP_ROOT,
    OBJ_RANK_UP,
    TEX_RANK_PRE,
    TEX_RANK_NEW,
    OBJ_PARTICLE,
    BTN_COMPLETE_RANK_UP,
    LBL_LIMIT_TIME_NAME,
    SPR_TYPE_DIFFICULTY,
  }

  protected class ResourceInfo
  {
    public RESOURCE_CATEGORY category;
    public string packageName;

    public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
    {
      this.category = category;
      this.packageName = packageName;
    }
  }
}

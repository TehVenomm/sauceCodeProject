// Decompiled with JetBrains decompiler
// Type: QuestAcceptRushDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptRushDetail : QuestDeliveryDetail
{
  private const int PREDOWNLOAD_MESSAGE_KEY = 3000;
  private bool isShowDropInfo;
  private QuestTable.QuestTableData questTableData;
  private const string WINDOW_SPRITE = "RequestWindowBase_Rush";
  private const string MESSAGE_SPRITE = "Checkhukidashi_Rush";

  public override void Initialize()
  {
    base.Initialize();
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) QuestAcceptRushDetail.UI.TEX_RUSH_IMAGE)).GetComponent<UITexture>(), RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int) this.info.GetQuestData().rushIconId));
    if ((this.isComplete || this.isNotice) && !this.isCompletedEventDelivery)
    {
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN, false);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE, false);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING, false);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN_OFF, false);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE_OFF, false);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING_OFF, false);
    }
    else
      this.StartCoroutine(this.StartPredownload());
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_DROP_REWARD, true);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_CLEAR_REWARD, true);
    base.UpdateUI();
    this.questTableData = this.info.GetQuestData();
    if (this.questTableData == null)
      return;
    int limitTime = (int) this.questTableData.limitTime;
    this.SetLabelText((Enum) QuestAcceptRushDetail.UI.LBL_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
    this.SetLabelText((Enum) QuestAcceptRushDetail.UI.LBL_RUSH_LEVEL, "");
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptRushDetail.UI.SPR_WINDOW, "RequestWindowBase_Rush");
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptRushDetail.UI.SPR_MESSAGE_BG, "Checkhukidashi_Rush");
    this.SetDifficultySprite();
    this.UpdateRewardInfo();
  }

  protected override void SetBaseFrame()
  {
    this.baseRoot = this.GetCtrl((Enum) QuestAcceptRushDetail.UI.OBJ_BASE_FRAME);
  }

  protected override void SetTargetFrame()
  {
    this.targetFrame = this.GetCtrl((Enum) QuestAcceptRushDetail.UI.OBJ_TARGET_FRAME);
  }

  protected override void SetSubmissionFrame()
  {
    this.submissionFrame = this.GetCtrl((Enum) QuestAcceptRushDetail.UI.OBJ_SUBMISSION_FRAME);
  }

  private void CreateRoom()
  {
    GameSection.StayEvent();
    PartyManager.PartySetting setting = new PartyManager.PartySetting(true, 0, 0);
    MonoBehaviourSingleton<PartyManager>.I.SendCreate((int) this.info.needs[0].questId, setting, (Action<bool>) (is_success =>
    {
      if (is_success)
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void UpdateRewardInfo()
  {
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_CLEAR_ICON_ROOT, !this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_DROP_ICON_ROOT, this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_CLEAR_REWARD, !this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_DROP_REWARD, this.isShowDropInfo);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.OBJ_COMPLETE_ROOT, !this.isShowDropInfo);
  }

  private void OnQuery_SWITCH_SUBMISSION()
  {
    if (!Object.op_Implicit((Object) this.targetFrame) || !Object.op_Implicit((Object) this.submissionFrame))
      return;
    bool activeSelf = ((Component) this.targetFrame).gameObject.activeSelf;
    ((Component) this.targetFrame).gameObject.SetActive(!activeSelf);
    ((Component) this.submissionFrame).gameObject.SetActive(activeSelf);
    this.isCompletedEventDelivery = true;
    this.RefreshUI();
  }

  private void OnQuery_CHANGE_INFO()
  {
    this.isShowDropInfo = !this.isShowDropInfo;
    this.UpdateRewardInfo();
  }

  private void OnQuery_CREATE()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.needs[0].questId);
    if (this.questTableData == null)
      return;
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.questTableData.questType
    });
  }

  private void OnQuery_JOIN()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.needs[0].questId);
  }

  protected IEnumerator StartPredownload()
  {
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN, false);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE, false);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING, false);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN_OFF, false);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE_OFF, false);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING_OFF, false);
    List<QuestAcceptRushDetail.ResourceInfo> list = new List<QuestAcceptRushDetail.ResourceInfo>();
    List<QuestTable.QuestTableData> sameRushQuestData = QuestTable.GetSameRushQuestData(this.info.GetQuestData().rushId);
    sameRushQuestData.Remove(this.info.GetQuestData());
    foreach (QuestTable.QuestTableData questTableData in sameRushQuestData)
    {
      uint mapId = questTableData.mapId;
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
      if (fieldMapData == null)
        yield break;
      string name = fieldMapData.stageName;
      if (string.IsNullOrEmpty(name))
        name = "ST011D_01";
      StageTable.StageData data = Singleton<StageTable>.I.GetData(name);
      if (data == null)
        yield break;
      list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, data.scene));
      list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, data.sky));
      if (!string.IsNullOrEmpty(data.cameraLinkEffect))
        list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect));
      if (!string.IsNullOrEmpty(data.cameraLinkEffectY0))
        list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0));
      if (!string.IsNullOrEmpty(data.rootEffect))
        list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect));
      for (int index = 0; index < 8; ++index)
      {
        if (!string.IsNullOrEmpty(data.useEffects[index]))
          list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[index]));
      }
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questTableData.enemyID[0]);
      int modelId = enemyData.modelId;
      string enemyBody = ResourceName.GetEnemyBody(modelId);
      string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
      string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
      list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
      if (!string.IsNullOrEmpty(enemyMaterial))
        list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
      list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
      if (!string.IsNullOrEmpty(enemyData.baseEffectName))
        list.Add(new QuestAcceptRushDetail.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
    }
    if (list.Find((Predicate<QuestAcceptRushDetail.ResourceInfo>) (x => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName))) != null)
    {
      List<string> assetNames = new List<string>();
      foreach (QuestAcceptRushDetail.ResourceInfo resourceInfo in list)
      {
        if (!string.IsNullOrEmpty(resourceInfo.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(resourceInfo.category, resourceInfo.packageName))
          assetNames.Add(resourceInfo.category.ToAssetBundleName(resourceInfo.packageName));
      }
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN_OFF, true);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE_OFF, true);
      this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING_OFF, true);
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
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
      foreach (QuestAcceptRushDetail.ResourceInfo resourceInfo in list)
      {
        if (!string.IsNullOrEmpty(resourceInfo.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(resourceInfo.category, resourceInfo.packageName))
        {
          ResourceManager.downloadOnly = true;
          load_queue.Load(resourceInfo.category, resourceInfo.packageName, (string[]) null);
          ResourceManager.downloadOnly = false;
          yield return (object) load_queue.Wait();
        }
      }
      assetNames = (List<string>) null;
      load_queue = (LoadingQueue) null;
    }
    bool is_visible = true;
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN, is_visible);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE, is_visible);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING, is_visible);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_JOIN_OFF, !is_visible);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_CREATE_OFF, !is_visible);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.BTN_AUTO_MATCHING_OFF, !is_visible);
  }

  private void OnQuery_AUTO_MATCHING()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    int retryCount = 0;
    PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0);
    MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int) this.info.GetQuestData().questID, retryCount, false, (Action<bool, int, bool, float>) ((is_success, maxRetryCount, isJoined, waitTime) =>
    {
      if (!is_success)
        GameSection.ResumeEvent(false);
      else if (maxRetryCount > 0)
      {
        ++retryCount;
        this.StartCoroutine(this.MatchAtRandom(setting, retryCount, waitTime));
      }
      else if (!isJoined)
      {
        this.OnQuery_AUTO_CREATE_ROOM();
      }
      else
      {
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
        GameSection.ResumeEvent(true);
      }
    }));
  }

  private IEnumerator MatchAtRandom(PartyManager.PartySetting setting, int retryCount, float time)
  {
    yield return (object) new WaitForSeconds(time);
    MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int) this.info.needs[0].questId, retryCount, false, (Action<bool, int, bool, float>) ((is_success, maxRetryCount, isJoined, waitTime) =>
    {
      if (!is_success)
        GameSection.ResumeEvent(false);
      else if (maxRetryCount > 0)
      {
        if (retryCount >= maxRetryCount)
        {
          this.OnQuery_AUTO_CREATE_ROOM();
        }
        else
        {
          ++retryCount;
          this.StartCoroutine(this.MatchAtRandom(setting, retryCount, waitTime));
        }
      }
      else if (!isJoined)
      {
        this.OnQuery_AUTO_CREATE_ROOM();
      }
      else
      {
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
        GameSection.ResumeEvent(true);
      }
    }));
  }

  private void OnQuery_AUTO_CREATE_ROOM()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.GetQuestData().questID);
    if (this.questTableData != null)
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.questTableData.questType
      });
    PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0);
    MonoBehaviourSingleton<PartyManager>.I.SendCreate((int) this.info.GetQuestData().questID, setting, (Action<bool>) (is_success =>
    {
      if (is_success)
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void SetDifficultySprite()
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryID);
    this.SetActive((Enum) QuestAcceptRushDetail.UI.SPR_TYPE_DIFFICULTY, deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD);
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
    BTN_MATCHING,
    BTN_JUMP_SMITH,
    BTN_JUMP_STATUS,
    BTN_JUMP_STORAGE,
    BTN_JUMP_POINT_SHOP,
    BTN_JUMP_WORLDMAP,
    BTN_WAVEMATCH_NEW,
    BTN_WAVEMATCH_PASS,
    BTN_WAVEMATCH_AUTO,
    OBJ_CARNIVAL_ROOT,
    LBL_POINT_CARNIVAL,
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
    BTN_AUTO_MATCHING,
    BTN_CREATE_OFF,
    BTN_JOIN_OFF,
    BTN_AUTO_MATCHING_OFF,
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

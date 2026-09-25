// Decompiled with JetBrains decompiler
// Type: QuestDeliveryDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestDeliveryDetail : GameSection
{
  private readonly string[] SPR_WINDOW_TYPE = new string[5]
  {
    "RequestWindowBase",
    "RequestWindowBase_Event",
    "RequestWindowBase_Story",
    "RequestWindowBase_Hard",
    "RequestWindowBase_Event"
  };
  private readonly string[] SPR_MESSAGE_BG_TYPE = new string[5]
  {
    "CheckHukidashi",
    "Checkhukidashi_Event",
    "Checkhukidashi_Story",
    "Checkhukidashi_Hard",
    "Checkhukidashi_Event"
  };
  protected Transform baseRoot;
  protected Transform targetFrame;
  protected Transform submissionFrame;
  protected int deliveryID;
  protected DeliveryTable.DeliveryData info;
  private DeliveryRewardTable.DeliveryRewardData[] rewardData;
  private DeliveryRewardList competeReward;
  public List<PointShopGetPointTable.Data> pointShopGetPointData;
  private bool isInGameScene;
  protected bool isCompletedEventDelivery;
  protected bool isNotice;
  protected bool completeJumpButton;
  private bool isQuestEnemy;
  private uint targetQuestID;
  private uint targetMapID;
  private int[] targetPortalID;
  private bool hasDispedMessage;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "PointShopGetPointTable";
      yield return "DeliveryRewardTable";
      yield return "FieldMapTable";
    }
  }

  private QuestDeliveryDetail.JumpButtonType GetJumpButtonTypeByQuestType(QUEST_TYPE questType)
  {
    switch (questType)
    {
      case QUEST_TYPE.EVENT:
        return QuestDeliveryDetail.JumpButtonType.eventRoom;
      case QUEST_TYPE.ORDER:
        return QuestDeliveryDetail.JumpButtonType.orderRoom;
      case QUEST_TYPE.WAVE:
      case QUEST_TYPE.WAVE_STRATEGY:
        return QuestDeliveryDetail.JumpButtonType.WaveRoom;
      case QUEST_TYPE.SERIES:
        return QuestDeliveryDetail.JumpButtonType.seriesRoom;
      default:
        return QuestDeliveryDetail.JumpButtonType.Invalid;
    }
  }

  protected bool isComplete => this.competeReward != null;

  public override void Initialize()
  {
    this.isInGameScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene";
    object[] eventData = GameSection.GetEventData() as object[];
    this.deliveryID = (int) eventData[0];
    this.competeReward = eventData[1] as DeliveryRewardList;
    if (eventData.Length >= 3)
      this.isCompletedEventDelivery = (bool) eventData[2];
    this.info = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryID);
    this.rewardData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData((uint) this.deliveryID);
    this.pointShopGetPointData = Singleton<PointShopGetPointTable>.I.GetFromDeiliveryId((uint) this.deliveryID);
    this.SetBaseFrame();
    this.SetTargetFrame();
    this.SetSubmissionFrame();
    this.completeJumpButton = false;
    base.Initialize();
  }

  private void OpenTutorial()
  {
    if (!HomeTutorialManager.DoesTutorial() || this.isInGameScene)
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep2_2");
  }

  private void CompleteTutorial()
  {
    if (TutorialStep.HasAllTutorialCompleted() || this.isInGameScene)
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep5_1");
  }

  protected virtual void SetBaseFrame()
  {
    this.baseRoot = this.SetPrefab((Enum) QuestDeliveryDetail.UI.OBJ_BASE_ROOT, "QuestRequestCheckBase");
  }

  protected virtual void SetTargetFrame()
  {
    this.targetFrame = this.SetPrefab((Enum) QuestDeliveryDetail.UI.OBJ_NEED_ITEM_ROOT, "QuestRequestCheckItem");
  }

  protected virtual void SetSubmissionFrame()
  {
  }

  protected virtual Enum GetBtnChangeEquipValue() => (Enum) QuestDeliveryDetail.UI.BTN_CHANGE_EQUIP;

  protected virtual bool IsChangableEquip() => false;

  protected void AdjustBtnPosToChangableEquipUI1Btn()
  {
    Transform ctrl = this.FindCtrl(this.baseRoot, this.GetBtnChangeEquipValue());
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    ctrl.localPosition = this.GetEquipBtnPos();
    this.SetActive(this.baseRoot, this.GetBtnChangeEquipValue(), true);
  }

  protected virtual Vector3 GetEquipBtnPos() => new Vector3(170f, -340f, 0.0f);

  private void AdjustBtnPositionToChangableEquipUI(QuestDeliveryDetail.JumpButtonType type)
  {
    if (this.isInGameScene || !this.IsChangableEquip())
    {
      this.SetActive(this.baseRoot, this.GetBtnChangeEquipValue(), false);
    }
    else
    {
      switch (type)
      {
        case QuestDeliveryDetail.JumpButtonType.Map:
        case QuestDeliveryDetail.JumpButtonType.Quest:
        case QuestDeliveryDetail.JumpButtonType.WorldMap:
          this.AdjustBtnPosToChangableEquipUI1Btn();
          break;
      }
    }
  }

  protected void UpdateSubMissionButton()
  {
    uint questId = this.info.needs[0].questId;
    if (questId <= 0U)
      this.SetActive((Enum) QuestDeliveryDetail.UI.BTN_SUBMISSION, false);
    else
      this.SetActive((Enum) QuestDeliveryDetail.UI.BTN_SUBMISSION, Singleton<QuestTable>.I.GetQuestData(questId).IsMissionExist());
  }

  protected void UpdateSubMission()
  {
    QuestDeliveryDetail.UI[] uiArray1 = new QuestDeliveryDetail.UI[3]
    {
      QuestDeliveryDetail.UI.OBJ_MISSION_INFO_1,
      QuestDeliveryDetail.UI.OBJ_MISSION_INFO_2,
      QuestDeliveryDetail.UI.OBJ_MISSION_INFO_3
    };
    QuestDeliveryDetail.UI[] uiArray2 = new QuestDeliveryDetail.UI[3]
    {
      QuestDeliveryDetail.UI.OBJ_TOP_CROWN_1,
      QuestDeliveryDetail.UI.OBJ_TOP_CROWN_2,
      QuestDeliveryDetail.UI.OBJ_TOP_CROWN_3
    };
    QuestDeliveryDetail.UI[] uiArray3 = new QuestDeliveryDetail.UI[3]
    {
      QuestDeliveryDetail.UI.LBL_MISSION_INFO_1,
      QuestDeliveryDetail.UI.LBL_MISSION_INFO_2,
      QuestDeliveryDetail.UI.LBL_MISSION_INFO_3
    };
    QuestDeliveryDetail.UI[] uiArray4 = new QuestDeliveryDetail.UI[3]
    {
      QuestDeliveryDetail.UI.SPR_MISSION_INFO_CROWN_1,
      QuestDeliveryDetail.UI.SPR_MISSION_INFO_CROWN_2,
      QuestDeliveryDetail.UI.SPR_MISSION_INFO_CROWN_3
    };
    QuestDeliveryDetail.UI[] uiArray5 = new QuestDeliveryDetail.UI[3]
    {
      QuestDeliveryDetail.UI.SPR_CROWN_1,
      QuestDeliveryDetail.UI.SPR_CROWN_2,
      QuestDeliveryDetail.UI.SPR_CROWN_3
    };
    if (this.info.needs.Length == 0)
      return;
    uint questId = this.info.needs[0].questId;
    if (questId <= 0U)
      return;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
    if (!questData.IsMissionExist())
    {
      this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_SUBMISSION_ROOT, false);
    }
    else
    {
      ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(questId);
      if (clearStatusQuestData == null)
      {
        this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_SUBMISSION_ROOT, true);
        int index = 0;
        for (int length = questData.missionID.Length; index < length; ++index)
        {
          uint id = questData.missionID[index];
          this.SetActive(this.submissionFrame, (Enum) uiArray1[index], id > 0U);
          this.SetActive(this.submissionFrame, (Enum) uiArray2[index], id > 0U);
          this.SetActive(this.submissionFrame, (Enum) uiArray3[index], id > 0U);
          if (id > 0U)
          {
            this.SetActive(this.submissionFrame, (Enum) uiArray4[index], false);
            this.SetActive(this.submissionFrame, (Enum) uiArray5[index], false);
            QuestTable.MissionTableData missionData = Singleton<QuestTable>.I.GetMissionData(id);
            this.SetLabelText(this.submissionFrame, (Enum) uiArray3[index], missionData.missionText);
          }
        }
      }
      else
      {
        this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_SUBMISSION_ROOT, true);
        int index = 0;
        for (int count = clearStatusQuestData.missionStatus.Count; index < count; ++index)
        {
          CLEAR_STATUS missionStatu = (CLEAR_STATUS) clearStatusQuestData.missionStatus[index];
          this.SetActive(this.submissionFrame, (Enum) uiArray1[index], questData.missionID[index] > 0U);
          this.SetActive(this.submissionFrame, (Enum) uiArray2[index], questData.missionID[index] > 0U);
          this.SetActive(this.submissionFrame, (Enum) uiArray4[index], missionStatu >= CLEAR_STATUS.CLEAR);
          this.SetActive(this.submissionFrame, (Enum) uiArray5[index], missionStatu >= CLEAR_STATUS.CLEAR);
          QuestTable.MissionTableData missionData = Singleton<QuestTable>.I.GetMissionData(questData.missionID[index]);
          this.SetLabelText(this.submissionFrame, (Enum) uiArray3[index], missionData.missionText);
        }
      }
    }
  }

  protected void UpdateHappenTarget()
  {
    QuestTable.QuestTableData questData = this.info.GetQuestData();
    if (questData == null)
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    if (enemyData == null)
      return;
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.FindCtrl(this.targetFrame, (Enum) QuestDeliveryDetail.UI.OBJ_ENEMY), enemyData.element);
  }

  public override void UpdateUI()
  {
    this.OpenTutorial();
    this.UpdateTitle();
    this.SetSprite(this.baseRoot, (Enum) QuestDeliveryDetail.UI.SPR_WINDOW, this.SPR_WINDOW_TYPE[this.info.DeliveryTypeIndex()]);
    this.SetSprite(this.baseRoot, (Enum) QuestDeliveryDetail.UI.SPR_MESSAGE_BG, this.SPR_MESSAGE_BG_TYPE[this.info.DeliveryTypeIndex()]);
    bool is_visible1 = false;
    if (Object.op_Implicit((Object) this.submissionFrame))
    {
      this.UpdateSubMissionButton();
      this.UpdateSubMission();
      is_visible1 = ((Component) this.submissionFrame).gameObject.activeSelf;
      this.SetActive((Enum) QuestDeliveryDetail.UI.STR_BTN_SUBMISSION, !is_visible1);
      this.SetActive((Enum) QuestDeliveryDetail.UI.STR_BTN_SUBMISSION_BACK, is_visible1);
    }
    Transform targetFrame = this.targetFrame;
    string map_name;
    string enemy_name;
    DIFFICULTY_TYPE? difficulty;
    MonoBehaviourSingleton<DeliveryManager>.I.GetTargetEnemyData(this.deliveryID, out this.targetQuestID, out this.targetMapID, out map_name, out enemy_name, out difficulty, out this.targetPortalID);
    this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_PLACE_NAME, map_name);
    int have;
    int need;
    MonoBehaviourSingleton<DeliveryManager>.I.GetAllProgressDelivery(this.deliveryID, out have, out need);
    this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_HAVE, this.isComplete ? need.ToString() : have.ToString());
    this.SetColor(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_HAVE, this.isComplete ? Color.white : Color.red);
    this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_NEED, need.ToString());
    this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_NEED_ITEM_NAME, MonoBehaviourSingleton<DeliveryManager>.I.GetTargetItemName(this.deliveryID));
    if (this.info.IsDefeatCondition())
    {
      if (this.targetQuestID > 0U)
      {
        this.isQuestEnemy = true;
        Transform ctrl = this.FindCtrl(targetFrame, (Enum) QuestDeliveryDetail.UI.OBJ_DIFFICULTY_ROOT);
        int num1 = (int) difficulty.Value;
        int num2 = 0;
        for (int childCount = ctrl.childCount; num2 < childCount; ++num2)
          this.SetActive(ctrl.GetChild(num2), num2 <= num1);
        this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_GET_PLACE, this.sectionData.GetText("GET_QUEST"));
      }
      else
      {
        this.isQuestEnemy = false;
        this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_GET_PLACE, this.sectionData.GetText("GET_AREA"));
      }
      this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_ENEMY_NAME, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 3U), (object) enemy_name));
    }
    else
    {
      this.isQuestEnemy = false;
      this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_GET_PLACE, StringTable.Get(STRING_CATEGORY.DELIVERY_CONDITION_PLACE, (uint) this.info.GetConditionType()));
      this.SetLabelText(targetFrame, (Enum) QuestDeliveryDetail.UI.LBL_ENEMY_NAME, enemy_name);
    }
    this.SetActive(targetFrame, (Enum) QuestDeliveryDetail.UI.OBJ_DIFFICULTY_ROOT, this.isQuestEnemy);
    this.SetActive(targetFrame, (Enum) QuestDeliveryDetail.UI.OBJ_ENEMY_NAME_ROOT, !this.isQuestEnemy);
    this.UpdateNPC(map_name, enemy_name);
    if ((this.isComplete || this.isNotice) && !this.isCompletedEventDelivery)
    {
      this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_BACK, false);
      this.SetActive((Enum) QuestDeliveryDetail.UI.BTN_CREATE, false);
      this.SetActive((Enum) QuestDeliveryDetail.UI.BTN_JOIN, false);
      this.SetActive((Enum) QuestDeliveryDetail.UI.BTN_MATCHING, false);
      this.SetActive(this.GetBtnChangeEquipValue(), false);
      if (this.isNotice)
        this.UpdateUIJumpButton(QuestDeliveryDetail.JumpButtonType.Complete);
    }
    else
    {
      this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_BACK, true);
      bool flag1 = true;
      bool flag2 = false;
      if (this.info == null || this.info.IsDefeatCondition() || this.targetMapID != 0U)
      {
        if (this.isQuestEnemy)
        {
          if (this.isInGameScene)
            flag1 = false;
        }
        else
        {
          bool flag3 = FieldManager.HasWorldMap(this.targetMapID);
          if (this.isInGameScene)
          {
            if ((int) MonoBehaviourSingleton<FieldManager>.I.currentMapID == (int) this.targetMapID)
            {
              if (flag3)
                flag2 = true;
              else
                flag1 = false;
            }
            else if (flag3)
            {
              if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(this.targetMapID) || WorldMapManager.IsValidPortalIDs(this.targetPortalID))
                flag2 = true;
            }
            else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(this.targetMapID))
              flag1 = false;
          }
          else if (flag3)
          {
            if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(this.targetMapID) || WorldMapManager.IsValidPortalIDs(this.targetPortalID))
              flag2 = true;
          }
          else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(this.targetMapID))
            flag1 = false;
        }
      }
      else
        flag1 = this.info.GetDeliveryJumpType() != 0;
      if (this.info != null && this.info.subType == DELIVERY_SUB_TYPE.READ_STORY)
        flag1 = false;
      QuestDeliveryDetail.JumpButtonType type = QuestDeliveryDetail.JumpButtonType.Invalid;
      if (flag1)
      {
        if (this.info != null && this.info.GetDeliveryJumpType() != DeliveryTable.DELIVERY_JUMPTYPE.UNDEFINED)
        {
          type = this.ConvertDeliveryJumpType();
        }
        else
        {
          if (this.info != null)
          {
            QuestTable.QuestTableData questData = this.info.GetQuestData();
            if (questData != null)
              type = this.GetJumpButtonTypeByQuestType(questData.questType);
          }
          if (type != QuestDeliveryDetail.JumpButtonType.WaveRoom && type != QuestDeliveryDetail.JumpButtonType.seriesRoom && type != QuestDeliveryDetail.JumpButtonType.orderRoom && type != QuestDeliveryDetail.JumpButtonType.eventRoom)
            type = flag2 ? QuestDeliveryDetail.JumpButtonType.Map : QuestDeliveryDetail.JumpButtonType.Quest;
        }
        this.UpdateUIJumpButton(type);
      }
      else
      {
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_QUEST, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_MAP, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_GACHATOP, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_INVALID, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_NEW, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_PASS, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_AUTO, false);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_COMPLETE, false);
        this.SetActive(this.GetBtnChangeEquipValue(), false);
      }
      if (flag2 && (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) this.targetMapID)
        this.SetColor(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_PLACE_NAME, Color.red);
      else
        this.SetColor(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_PLACE_NAME, Color.white);
    }
    int money = 0;
    int exp = 0;
    int carnivalPoint = 0;
    if (this.rewardData != null)
      this.SetGrid(this.baseRoot, (Enum) QuestDeliveryDetail.UI.GRD_REWARD, "", this.rewardData.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        DeliveryRewardTable.DeliveryRewardData.Reward reward = this.rewardData[i].reward;
        bool is_visible2 = false;
        if (reward.type == REWARD_TYPE.MONEY)
          money += reward.num;
        else if (reward.type == REWARD_TYPE.EXP)
          exp += reward.num;
        else if (reward.type == REWARD_TYPE.RANKING_POINT)
        {
          carnivalPoint += reward.num;
        }
        else
        {
          is_visible2 = true;
          ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, string.Empty, questIconSizeType: ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
          this.SetMaterialInfo(rewardItemIcon.transform, reward.type, reward.item_id);
          rewardItemIcon.SetRewardBG(true);
        }
        this.SetActive(t, is_visible2);
      }));
    this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_MONEY, money.ToString());
    this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_EXP, exp.ToString());
    this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_CARNIVAL_ROOT, carnivalPoint > 0);
    if (carnivalPoint > 0)
      this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_POINT_CARNIVAL, carnivalPoint.ToString());
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_COMPLETE_ROOT, this.isComplete && !is_visible1);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_UNLOCK_PORTAL_ROOT, this.isComplete);
    if (this.isComplete)
    {
      string text = string.Empty;
      List<FieldMapTable.PortalTableData> relationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData(this.info.id);
      switch (relationPortalData.Count)
      {
        case 0:
          bool is_unlock_portal = !string.IsNullOrEmpty(text);
          if (!TutorialStep.HasFirstDeliveryCompleted())
            is_unlock_portal = false;
          this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_UNLOCK_PORTAL_ROOT, is_unlock_portal && !this.isCompletedEventDelivery);
          this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_UNLOCK_PORTAL, text);
          if (this.isCompletedEventDelivery)
          {
            this.SkipTween(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_COMPLETE_ROOT);
            break;
          }
          this.StartCoroutine(this.StartTweenCoroutine(is_unlock_portal));
          break;
        case 1:
          FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(relationPortalData[0].srcMapID);
          if (fieldMapData != null)
          {
            text = fieldMapData.mapName;
            goto case 0;
          }
          goto case 0;
        default:
          text = this.sectionData.GetText("MULTI_UNLOCK");
          goto case 0;
      }
    }
    this.StartCoroutine(this.SetPointShopGetPointUI());
  }

  private IEnumerator StartTweenCoroutine(bool is_unlock_portal)
  {
    while (GameSceneManager.isAutoEventSkip)
      yield return (object) null;
    string effectName = "ef_ui_portal_unlock_01";
    if (is_unlock_portal)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effectName);
      yield return (object) loadingQueue.Wait();
      this.ResetTween(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_UNLOCK_PORTAL_ROOT);
    }
    this.PlayCompleteTween((EventDelegate.Callback) (() => this.OnEndCompletetween(is_unlock_portal, effectName)));
    this.CompleteTutorial();
  }

  private void PlayCompleteTween(EventDelegate.Callback callback)
  {
    this.ResetTween(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_COMPLETE_ROOT);
    this.PlayTween(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_COMPLETE_ROOT, callback: (EventDelegate.Callback) (() =>
    {
      this.PlayAudio((Enum) QuestDeliveryDetail.AUDIO.REQUEST_COMPLETE);
      callback();
    }), is_input_block: false);
  }

  protected virtual void OnEndCompletetween(bool is_unlock_portal, string effectName)
  {
    this.completeJumpButton = true;
    if (is_unlock_portal)
    {
      this.PlayUnlockPortalTween(effectName, (System.Action) (() =>
      {
        this.UpdateUIJumpButton(QuestDeliveryDetail.JumpButtonType.Complete);
        this.DispMessageEventOpen();
      }));
    }
    else
    {
      this.UpdateUIJumpButton(QuestDeliveryDetail.JumpButtonType.Complete);
      this.DispMessageEventOpen();
    }
  }

  protected void PlayUnlockPortalTween(string effectName, System.Action callback)
  {
    Transform ctrl = this.GetCtrl((Enum) QuestDeliveryDetail.UI.OBJ_UNLOCK_PORTAL_ROOT);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      Transform uiEffect = EffectManager.GetUIEffect(effectName, ctrl, -0.2f);
      if (Object.op_Inequality((Object) uiEffect, (Object) null))
      {
        rymFX component = ((Component) uiEffect).GetComponent<rymFX>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.ChangeRenderQueue = 3999;
      }
    }
    this.PlayTween(this.baseRoot, (Enum) QuestDeliveryDetail.UI.OBJ_UNLOCK_PORTAL_ROOT, callback: (EventDelegate.Callback) (() =>
    {
      this.PlayAudio((Enum) QuestDeliveryDetail.AUDIO.UNLOCKE_PORTAL, as_jingle: true);
      callback();
    }), is_input_block: false);
  }

  protected void DispMessageEventOpen()
  {
    if (MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds == null || MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds.Count == 0)
      return;
    this.hasDispedMessage = true;
    int releasedEventId = MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds[0];
    MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds.RemoveAt(0);
    Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => e.eventId == releasedEventId)).First<Network.EventData>();
    if (eventData == null)
    {
      Debug.LogError((object) "イベント開放に関して、指定されたIDのイベントが存在しません");
      this.DispMessageEventOpen();
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 4U), (object) eventData.name)), (Action<string>) (ret => this.DispMessageEventOpen()));
  }

  protected virtual void UpdateTitle()
  {
    this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_QUEST_TITLE, this.info.name);
  }

  protected virtual void UpdateNPC(string map_name, string enemy_name)
  {
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData((int) this.info.npcID);
    this.SetNPCIcon(this.baseRoot, (Enum) QuestDeliveryDetail.UI.TEX_NPC, npcData.npcModelID, this.isComplete);
    this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_PERSON_NAME, npcData.displayName);
    this.SetLabelText(this.baseRoot, (Enum) QuestDeliveryDetail.UI.LBL_CHARA_MESSAGE, (this.isComplete ? this.info.npcClearComment : this.info.npcComment).Replace("{MAP_NAME}", map_name).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name).Replace("{ENEMY_NAME}", enemy_name));
  }

  protected void JumpQuest()
  {
    if (!TutorialStep.HasFirstDeliveryCompleted())
    {
      GameSection.StopEvent();
      this.DispatchEvent("TUTORIAL_TO_FIELD");
    }
    else
    {
      this.PlayAudio((Enum) QuestDeliveryDetail.AUDIO.GO_TO_FIELD);
      if (this.isQuestEnemy)
      {
        if (this.isInGameScene)
          return;
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
        {
          new EventData("[BACK]", (object) null),
          new EventData("TAB_QUEST", (object) (uint) this.deliveryID),
          new EventData("SELECT_QUEST", (object) this.targetQuestID)
        });
      }
      else
      {
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.targetMapID);
        if (fieldMapData == null || fieldMapData.jumpPortalID == 0U)
          Log.Error("QuestDeliveryDetail.JumpQuest() jumpPortalID is not found.");
        else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
        {
          MonoBehaviourSingleton<WorldMapManager>.I.SetJumpPortalID(fieldMapData.jumpPortalID);
          GameSection.StopEvent();
          this.DispatchEvent("QUEST_TO_FIELD");
        }
        else
        {
          if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID == (int) this.targetMapID)
            return;
          MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
        }
      }
    }
  }

  protected void OnQuery_TO_SMITH()
  {
    XorUInt targetId;
    QuestDeliveryDetail.SMITH_SECTION smithSection = this.GetSmithSection(this.info, out targetId);
    MonoBehaviourSingleton<StatusManager>.I.InitUniqueEquip();
    if (smithSection == QuestDeliveryDetail.SMITH_SECTION.INVALID)
      this.ToSmith();
    else
      this.ToSmith(smithSection, targetId);
  }

  protected void OnQuery_TO_STATUS() => this.OnQuery_MAIN_MENU_STATUS();

  protected void OnQuery_TO_STORAGE() => this.OpenStorage();

  protected void OnQuery_TO_POINT_SHOP() => this.ToPointShop();

  protected void OnQuery_TO_WORLDMAP() => this.OnQuery_MAIN_MENU_QUEST();

  public void OnQuery_PORTAL_RELEASE()
  {
    object eventData = GameSection.GetEventData();
    if (eventData is List<uint>)
      GameSaveData.instance.newReleasePortals = eventData as List<uint>;
    if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
    {
      MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame = new List<int>((IEnumerable<int>) MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene);
      MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Clear();
      MonoBehaviourSingleton<InGameProgress>.I.DeliveryAddCheck();
    }
    else
      MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
  }

  protected virtual void UpdateUIJumpButton(QuestDeliveryDetail.JumpButtonType type)
  {
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_QUEST, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_MAP, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_GACHATOP, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_INVALID, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_COMPLETE, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_POINT_SHOP, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_WORLDMAP, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_SMITH, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_STATUS, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_STORAGE, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_NEW, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_PASS, false);
    this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_AUTO, false);
    this.SetActive(this.baseRoot, this.GetBtnChangeEquipValue(), false);
    this.AdjustBtnPositionToChangableEquipUI(type);
    switch (type)
    {
      case QuestDeliveryDetail.JumpButtonType.Complete:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_COMPLETE, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.Map:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_MAP, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.Quest:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_QUEST, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.Gacha:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_GACHATOP, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.Smith:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_SMITH, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.Status:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_STATUS, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.Storage:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_STORAGE, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.PointShop:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_POINT_SHOP, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.WorldMap:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_WORLDMAP, true);
        break;
      case QuestDeliveryDetail.JumpButtonType.WaveRoom:
      case QuestDeliveryDetail.JumpButtonType.seriesRoom:
      case QuestDeliveryDetail.JumpButtonType.orderRoom:
      case QuestDeliveryDetail.JumpButtonType.eventRoom:
        if (this.isInGameScene)
          break;
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_NEW, true);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_PASS, true);
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_WAVEMATCH_AUTO, true);
        break;
      default:
        this.SetActive(this.baseRoot, (Enum) QuestDeliveryDetail.UI.BTN_JUMP_INVALID, true);
        break;
    }
  }

  protected void JumpMap()
  {
    if (FieldManager.HasWorldMap(this.targetMapID))
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.targetMapID);
      if (Array.IndexOf<uint>(MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdList(), fieldMapData.regionId) < 0)
      {
        GameSection.ChangeEvent("NOT_OPEN", (object) new object[1]
        {
          (object) Singleton<RegionTable>.I.GetData(fieldMapData.regionId).regionName
        });
        return;
      }
    }
    MonoBehaviourSingleton<WorldMapManager>.I.PushDisplayQuestTarget((int) this.targetMapID, this.targetPortalID);
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
    bool flag1 = true;
    if (Singleton<TutorialMessageTable>.IsValid())
      flag1 = Singleton<TutorialMessageTable>.I.ReadData.HasRead(10003);
    bool flag2 = false;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryID);
    if (deliveryTableData != null && deliveryTableData.type == DELIVERY_TYPE.STORY && 10100011 >= this.deliveryID && !flag1)
    {
      flag2 = true;
      if (Singleton<TutorialMessageTable>.IsValid())
      {
        TutorialReadData readData = Singleton<TutorialMessageTable>.I.ReadData;
        readData.SetReadId(10003, true);
        readData.Save();
      }
    }
    if (flag2)
      this.RequestEvent("DIRECT_REGION_TUTORIAL");
    else
      this.RequestEvent("DIRECT_REGION_QUEST");
  }

  private IEnumerator SetPointShopGetPointUI()
  {
    if (this.pointShopGetPointData != null && this.pointShopGetPointData.Count > 0)
    {
      LoadingQueue queue = new LoadingQueue((MonoBehaviour) this);
      queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int) this.pointShopGetPointData[0].pointShopId));
      if (queue.IsLoading())
        yield return (object) queue.Wait();
      this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_NORMAL_ROOT, true);
      this.SetLabelText((Enum) QuestDeliveryDetail.UI.LBL_POINT_NORMAL, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) this.pointShopGetPointData[0].basePoint));
      ResourceLoad.LoadPointIconImageTexture(((Component) this.GetCtrl((Enum) QuestDeliveryDetail.UI.TEX_NORMAL_ICON)).GetComponent<UITexture>(), this.pointShopGetPointData[0].pointShopId);
      if (this.pointShopGetPointData.Count >= 2)
      {
        queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int) this.pointShopGetPointData[1].pointShopId));
        if (queue.IsLoading())
          yield return (object) queue.Wait();
        this.SetActive((Enum) QuestDeliveryDetail.UI.OBJ_EVENT_ROOT, true);
        this.SetLabelText((Enum) QuestDeliveryDetail.UI.LBL_POINT_EVENT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) this.pointShopGetPointData[1].basePoint));
        ResourceLoad.LoadPointIconImageTexture(((Component) this.GetCtrl((Enum) QuestDeliveryDetail.UI.TEX_EVENT_ICON)).GetComponent<UITexture>(), this.pointShopGetPointData[1].pointShopId);
      }
      queue = (LoadingQueue) null;
    }
  }

  private QuestDeliveryDetail.JumpButtonType ConvertDeliveryJumpType()
  {
    switch (this.info.GetDeliveryJumpType())
    {
      case DeliveryTable.DELIVERY_JUMPTYPE.TO_GACHA:
        return QuestDeliveryDetail.JumpButtonType.Gacha;
      case DeliveryTable.DELIVERY_JUMPTYPE.TO_SMITH:
        return QuestDeliveryDetail.JumpButtonType.Smith;
      case DeliveryTable.DELIVERY_JUMPTYPE.TO_STATUS:
        return QuestDeliveryDetail.JumpButtonType.Status;
      case DeliveryTable.DELIVERY_JUMPTYPE.TO_STORAGE:
        return QuestDeliveryDetail.JumpButtonType.Storage;
      case DeliveryTable.DELIVERY_JUMPTYPE.TO_POINT_SHOP:
        return QuestDeliveryDetail.JumpButtonType.PointShop;
      case DeliveryTable.DELIVERY_JUMPTYPE.TO_WORLD_MAP:
        return QuestDeliveryDetail.JumpButtonType.WorldMap;
      default:
        return QuestDeliveryDetail.JumpButtonType.Invalid;
    }
  }

  protected void WaveMatchNew()
  {
    QuestTable.QuestTableData questData = this.info.GetQuestData();
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID);
    GameSection.SetEventData((object) new object[1]
    {
      (object) questData.questType
    });
  }

  protected void WaveMatchPass()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.needs[0].questId);
  }

  protected void WaveMatchAuto()
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
        this.WaveMatchCreate();
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
          this.WaveMatchCreate();
        }
        else
        {
          ++retryCount;
          this.StartCoroutine(this.MatchAtRandom(setting, retryCount, waitTime));
        }
      }
      else if (!isJoined)
      {
        this.WaveMatchCreate();
      }
      else
      {
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
        GameSection.ResumeEvent(true);
      }
    }));
  }

  protected void WaveMatchCreate()
  {
    QuestTable.QuestTableData questData = this.info.GetQuestData();
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID);
    GameSection.SetEventData((object) new object[1]
    {
      (object) questData.questType
    });
    PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0);
    MonoBehaviourSingleton<PartyManager>.I.SendCreate((int) questData.questID, setting, (Action<bool>) (is_success =>
    {
      if (is_success)
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_SECTION_BACK()
  {
    if (this.info == null || !this.completeJumpButton)
      return;
    if (this.info.GetUIType() == DeliveryTable.UIType.EVENT || this.info.GetUIType() == DeliveryTable.UIType.SUB_EVENT)
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<DeliveryManager>.I.SendEventList((Action<bool>) (is_success => GameSection.ResumeEvent(true)));
    }
    else
    {
      if (this.info.DeliveryTypeIndex() == 1)
        return;
      GameSection.StayEvent();
      MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList((Action<bool>) (is_success => GameSection.ResumeEvent(true)));
    }
  }

  private QuestDeliveryDetail.SMITH_SECTION GetSmithSection(
    DeliveryTable.DeliveryData _info,
    out XorUInt targetId)
  {
    targetId = (XorUInt) 0U;
    if (_info == null)
      return QuestDeliveryDetail.SMITH_SECTION.INVALID;
    QuestDeliveryDetail.SMITH_SECTION smithSection = QuestDeliveryDetail.SMITH_SECTION.INVALID;
    DeliveryTable.DeliveryData.NeedData[] needs = _info.needs;
    int index = 0;
    for (int length = needs.Length; index < length; ++index)
    {
      DeliveryTable.DeliveryData.NeedData needData = needs[index];
      switch (needData.conditionType)
      {
        case DELIVERY_CONDITION_TYPE.WEAPON_PROTECTOR_GROW:
        case DELIVERY_CONDITION_TYPE.WEAPON_GROW:
        case DELIVERY_CONDITION_TYPE.PROTECTOR_GROW:
        case DELIVERY_CONDITION_TYPE.EQUIP_GROW_MAX_EQUIP_ID_OR:
          targetId = needData.needId;
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) _info.id) && MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNumWithShadow((uint) targetId) > 0)
            return QuestDeliveryDetail.SMITH_SECTION.EQUIP_GROW;
          break;
        case DELIVERY_CONDITION_TYPE.MAGI_GROW:
          targetId = needData.needId;
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) _info.id))
            return QuestDeliveryDetail.SMITH_SECTION.MAGI_GROW;
          break;
        case DELIVERY_CONDITION_TYPE.WEAPON_PROTECTOR_CREATE:
        case DELIVERY_CONDITION_TYPE.WEAPON_CREATE:
        case DELIVERY_CONDITION_TYPE.PROTECTOR_CREATE:
          targetId = needData.needId;
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) _info.id))
            return QuestDeliveryDetail.SMITH_SECTION.EQUIP_CREATE;
          break;
        case DELIVERY_CONDITION_TYPE.WEAPON_PROTECTOR_EVOLVE:
        case DELIVERY_CONDITION_TYPE.WEAPON_EVOLVE:
        case DELIVERY_CONDITION_TYPE.PROTECTOR_EVOLVE:
          targetId = needData.needId;
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) _info.id) && MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNumWithShadow((uint) targetId) > 0)
            return QuestDeliveryDetail.SMITH_SECTION.EQUIP_EVOLVE;
          break;
        case DELIVERY_CONDITION_TYPE.CHANGE_ABILITY:
          targetId = needData.needId;
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) _info.id))
            return QuestDeliveryDetail.SMITH_SECTION.CHANGE_ABILITY;
          break;
        case DELIVERY_CONDITION_TYPE.EQUIP_EXCEED:
          targetId = needData.needId;
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) _info.id) && MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNumWithShadow((uint) targetId) > 0)
            return QuestDeliveryDetail.SMITH_SECTION.EQUIP_EXCEED;
          break;
        case DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID:
          if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) needData.needId.value))
          {
            smithSection = this.GetSmithSection(Singleton<DeliveryTable>.I.GetDeliveryTableData(needData.needId.value), out targetId);
            if (smithSection != QuestDeliveryDetail.SMITH_SECTION.INVALID)
              return smithSection;
            break;
          }
          break;
        default:
          smithSection = QuestDeliveryDetail.SMITH_SECTION.INVALID;
          break;
      }
    }
    return smithSection;
  }

  private void ToSmith(QuestDeliveryDetail.SMITH_SECTION section, XorUInt targetId)
  {
    if ((uint) targetId == 0U)
    {
      this.ToSmith();
    }
    else
    {
      switch (section)
      {
        case QuestDeliveryDetail.SMITH_SECTION.EQUIP_GROW:
        case QuestDeliveryDetail.SMITH_SECTION.EQUIP_EXCEED:
        case QuestDeliveryDetail.SMITH_SECTION.EQUIP_EVOLVE:
          EquipItemInfo growEquipItem = this.GetGrowEquipItem(section, targetId);
          if (growEquipItem != null)
          {
            this.GetOrCreateSmithData<SmithManager.SmithGrowData>().selectEquipData = growEquipItem;
            if (growEquipItem.IsLevelMax() && growEquipItem.tableData.IsEvolve())
            {
              this.ChangeSmithScene("SmithEvolve");
              return;
            }
            this.ChangeSmithScene("SmithGrow");
            return;
          }
          break;
      }
      this.ToSmith();
    }
  }

  private EquipItemInfo GetGrowEquipItem(
    QuestDeliveryDetail.SMITH_SECTION section,
    XorUInt targetId)
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ALL_EQUIP;
    EquipItemInfo[] all = Array.FindAll<EquipItemInfo>(MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone(), (Predicate<EquipItemInfo>) (e => (int) e.tableID == (int) (uint) targetId || (int) e.tableData.shadowEvolveEquipItemId == (int) (uint) targetId));
    Array.Sort<EquipItemInfo>(all, (Comparison<EquipItemInfo>) ((a, b) =>
    {
      if (b.uniqueID - a.uniqueID < 0UL)
        return 1;
      return b.uniqueID - a.uniqueID <= 0UL ? 0 : -1;
    }));
    EquipItemInfo growEquipItem = (EquipItemInfo) null;
    switch (section)
    {
      case QuestDeliveryDetail.SMITH_SECTION.EQUIP_GROW:
        for (int index = 0; index < all.Length; ++index)
        {
          EquipItemInfo equipItemInfo = all[index];
          if (growEquipItem == null || growEquipItem.IsLevelMax() && !equipItemInfo.IsLevelMax())
            growEquipItem = equipItemInfo;
        }
        break;
      case QuestDeliveryDetail.SMITH_SECTION.EQUIP_EXCEED:
        for (int index = 0; index < all.Length; ++index)
        {
          EquipItemInfo equipItemInfo = all[index];
          if (growEquipItem == null || growEquipItem.IsExceedMax() && !equipItemInfo.IsExceedMax())
            growEquipItem = equipItemInfo;
        }
        break;
      case QuestDeliveryDetail.SMITH_SECTION.EQUIP_EVOLVE:
        for (int index = 0; index < all.Length; ++index)
        {
          EquipItemInfo equipItemInfo = all[index];
          if (growEquipItem == null || growEquipItem.IsLevelAndEvolveMax() && !equipItemInfo.IsLevelAndEvolveMax())
            growEquipItem = equipItemInfo;
        }
        break;
    }
    if (growEquipItem != null && growEquipItem.IsLevelAndEvolveMax() && growEquipItem.IsExceedMax())
      growEquipItem = (EquipItemInfo) null;
    return growEquipItem;
  }

  private SortSettings CreateSortSettings()
  {
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    return smithData.selectCreateEquipItemType < SortBase.TYPE.ARMOR || smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL ? (smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL ? SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM) : SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM)) : (smithData.selectCreateEquipItemType == SortBase.TYPE.ARMOR_ALL ? SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM) : SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM));
  }

  private T GetOrCreateSmithData<T>() where T : SmithManager.SmithDataBase, new()
  {
    return MonoBehaviourSingleton<SmithManager>.I.GetSmithData<T>() ?? MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<T>();
  }

  private void ChangeSmithScene(string to_section)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Smith", to_section);
  }

  protected enum UI
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
    SPR_ELEMENT,
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
    OBJ_CLEAR_ICON_ROOT,
    OBJ_CLEAR_REWARD,
    BTN_CREATE_OFF,
    SPR_TYPE_DIFFICULTY,
    BTN_CHANGE_EQUIP,
  }

  private enum AUDIO
  {
    REQUEST_COMPLETE = 40000029, // 0x02625A1D
    GO_TO_FIELD = 40000124, // 0x02625A7C
    UNLOCKE_PORTAL = 40000161, // 0x02625AA1
  }

  public enum JumpButtonType
  {
    Invalid,
    Complete,
    Map,
    Quest,
    Gacha,
    Smith,
    Status,
    Storage,
    PointShop,
    WorldMap,
    WaveRoom,
    seriesRoom,
    orderRoom,
    eventRoom,
  }

  private enum SMITH_SECTION
  {
    INVALID,
    EQUIP_GROW,
    EQUIP_EXCEED,
    EQUIP_EVOLVE,
    EQUIP_CREATE,
    MAGI_GROW,
    CHANGE_ABILITY,
  }
}

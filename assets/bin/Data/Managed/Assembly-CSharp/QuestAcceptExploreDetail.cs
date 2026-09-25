// Decompiled with JetBrains decompiler
// Type: QuestAcceptExploreDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestAcceptExploreDetail : QuestDeliveryDetail
{
  private Network.EventData eventData;
  private QuestTable.QuestTableData questTableData;
  private const string WINDOW_SPRITE = "RequestWindowBase_Explorer";
  private const string MESSAGE_SPRITE = "Checkhukidashi_Explorer";

  public override void Initialize()
  {
    base.Initialize();
    foreach (Network.EventData eventData in MonoBehaviourSingleton<QuestManager>.I.eventList)
    {
      if (eventData.eventId == this.info.eventID)
      {
        this.eventData = eventData;
        break;
      }
    }
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestAcceptExploreDetail.UI.OBJ_CLEAR_REWARD, true);
    base.UpdateUI();
    this.questTableData = this.info.GetQuestData();
    if (this.questTableData == null)
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.questTableData.GetMainEnemyID());
    if (enemyData == null)
      return;
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.GetCtrl((Enum) QuestAcceptExploreDetail.UI.OBJ_ENEMY)).SetDepth(7);
    this.SetElementSprite((Enum) QuestAcceptExploreDetail.UI.SPR_ENM_ELEMENT, (int) enemyData.element);
    this.SetElementSprite((Enum) QuestAcceptExploreDetail.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive((Enum) QuestAcceptExploreDetail.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
    int limitTime = (int) this.questTableData.limitTime;
    this.SetLabelText((Enum) QuestAcceptExploreDetail.UI.LBL_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
    if ((this.isComplete || this.isNotice) && !this.isCompletedEventDelivery)
    {
      this.SetActive((Enum) QuestAcceptExploreDetail.UI.BTN_CREATE_OFF, false);
    }
    else
    {
      this.SetActive((Enum) QuestAcceptExploreDetail.UI.BTN_CREATE, this.IsCreatableRoom());
      this.SetActive((Enum) QuestAcceptExploreDetail.UI.BTN_CREATE_OFF, !this.IsCreatableRoom());
    }
    this.SetDifficultySprite();
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptExploreDetail.UI.SPR_WINDOW, "RequestWindowBase_Explorer");
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptExploreDetail.UI.SPR_MESSAGE_BG, "Checkhukidashi_Explorer");
    this.SetActive(this.baseRoot, (Enum) QuestAcceptExploreDetail.UI.OBJ_COMPLETE_ROOT, this.isComplete);
  }

  protected override void SetBaseFrame()
  {
    this.baseRoot = this.GetCtrl((Enum) QuestAcceptExploreDetail.UI.OBJ_BASE_FRAME);
  }

  protected override void SetTargetFrame()
  {
    this.targetFrame = this.GetCtrl((Enum) QuestAcceptExploreDetail.UI.OBJ_TARGET_FRAME);
  }

  protected override void SetSubmissionFrame()
  {
    this.submissionFrame = this.GetCtrl((Enum) QuestAcceptExploreDetail.UI.OBJ_SUBMISSION_FRAME);
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

  private void OnQuery_CREATE()
  {
    if (!this.IsCreatableRoom())
    {
      string event_data = StringTable.Get(STRING_CATEGORY.MATCHING, 0U);
      new object[1][0] = (object) event_data;
      GameSection.ChangeEvent("HOST_LIMIT", (object) event_data);
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.needs[0].questId);
      if (this.questTableData == null)
        return;
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.questTableData.questType
      });
    }
  }

  private void OnQuery_JOIN()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.needs[0].questId);
  }

  private void OnQuery_MATCHING()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    int retryCount = 0;
    PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0, _ex: 1);
    MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int) this.info.needs[0].questId, retryCount, true, (Action<bool, int, bool, float>) ((is_success, maxRetryCount, isJoined, waitTime) =>
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
    MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int) this.info.needs[0].questId, retryCount, true, (Action<bool, int, bool, float>) ((is_success, maxRetryCount, isJoined, waitTime) =>
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
    GameSection.ResumeEvent(false);
    string event_data = StringTable.Get(STRING_CATEGORY.MATCHING, 1U);
    new object[1][0] = (object) event_data;
    this.DispatchEvent("HOST_LIMIT", (object) event_data);
  }

  private bool IsCreatableRoom() => this.eventData.hostCountLimit > 0;

  private void SetDifficultySprite()
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryID);
    this.SetActive((Enum) QuestAcceptExploreDetail.UI.SPR_TYPE_DIFFICULTY, deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD);
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
    OBJ_CLEAR_ICON_ROOT,
    OBJ_CLEAR_REWARD,
    BTN_CREATE_OFF,
    SPR_TYPE_DIFFICULTY,
  }
}

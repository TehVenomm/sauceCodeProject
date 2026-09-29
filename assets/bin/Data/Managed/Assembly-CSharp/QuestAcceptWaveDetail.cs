// Decompiled with JetBrains decompiler
// Type: QuestAcceptWaveDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestAcceptWaveDetail : QuestDeliveryDetail
{
  private const string WINDOW_SPRITE = "RequestWindowBase";
  private const string MESSAGE_SPRITE = "RequestFukidashi";
  private Network.EventData eventData;
  private QuestTable.QuestTableData questTableData;

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
    this.SetActive((Enum) QuestAcceptWaveDetail.UI.OBJ_CLEAR_REWARD, true);
    base.UpdateUI();
    this.questTableData = this.info.GetQuestData();
    if (this.questTableData == null)
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.questTableData.GetMainEnemyID());
    if (enemyData == null)
      return;
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.GetCtrl((Enum) QuestAcceptWaveDetail.UI.OBJ_ENEMY)).SetDepth(7);
    this.SetElementSprite((Enum) QuestAcceptWaveDetail.UI.SPR_ENM_ELEMENT, (int) enemyData.element);
    int limitTime = (int) this.questTableData.limitTime;
    this.SetLabelText((Enum) QuestAcceptWaveDetail.UI.LBL_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
    this.SetDifficultySprite();
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptWaveDetail.UI.SPR_WINDOW, "RequestWindowBase");
    this.SetSprite(this.baseRoot, (Enum) QuestAcceptWaveDetail.UI.SPR_MESSAGE_BG, "RequestFukidashi");
    this.SetActive(this.baseRoot, (Enum) QuestAcceptWaveDetail.UI.OBJ_COMPLETE_ROOT, this.isComplete);
  }

  protected override void SetBaseFrame()
  {
    this.baseRoot = this.GetCtrl((Enum) QuestAcceptWaveDetail.UI.OBJ_BASE_FRAME);
  }

  protected override void SetTargetFrame()
  {
    this.targetFrame = this.GetCtrl((Enum) QuestAcceptWaveDetail.UI.OBJ_TARGET_FRAME);
  }

  private void SetDifficultySprite()
  {
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryID);
    this.SetActive((Enum) QuestAcceptWaveDetail.UI.SPR_TYPE_DIFFICULTY, deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD);
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

  private void OnQuery_MATCHING()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    int retryCount = 0;
    PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0, _ex: 1);
    MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int) this.info.needs[0].questId, retryCount, false, (Action<bool, int, bool, float>) ((is_success, maxRetryCount, isJoined, waitTime) =>
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
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.info.needs[0].questId);
    if (this.questTableData != null)
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.questTableData.questType
      });
    PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0);
    MonoBehaviourSingleton<PartyManager>.I.SendCreate((int) this.info.needs[0].questId, setting, (Action<bool>) (is_success =>
    {
      if (is_success)
        MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
      GameSection.ResumeEvent(is_success);
    }));
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

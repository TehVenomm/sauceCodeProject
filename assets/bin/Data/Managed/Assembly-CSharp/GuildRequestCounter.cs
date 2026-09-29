// Decompiled with JetBrains decompiler
// Type: GuildRequestCounter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class GuildRequestCounter : GameSection
{
  private QuestInfoData selectedQuestInfoData;
  private int selectedQuestNum;
  private List<GuildRequestCounter.GuildRequestPrefab> prefabCache = new List<GuildRequestCounter.GuildRequestPrefab>();
  private const float UPDATE_INTARVAL = 0.2f;
  private float timer;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    this.selectedQuestInfoData = GameSection.GetEventData() as QuestInfoData;
    if (this.selectedQuestInfoData != null)
      this.selectedQuestNum = !this.IsFromShadow() ? this.selectedQuestInfoData.questData.num : MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestList((Action<bool>) (r => wait = false));
    while (wait)
      yield return (object) null;
    wait = true;
    this.SendGetChallengeInfo((System.Action) (() => wait = false), (Action<bool>) null);
    while (wait)
      yield return (object) null;
    base.Initialize();
  }

  private void Update()
  {
    if (this.state != UIBehaviour.STATE.OPEN)
      return;
    this.UpdateTimers();
  }

  private void UpdateTimers()
  {
    if ((double) this.timer < 0.20000000298023224)
      this.timer += Time.deltaTime;
    if ((double) this.timer < 0.20000000298023224)
      return;
    this.timer = 0.0f;
    for (int index = 0; index < this.prefabCache.Count; ++index)
    {
      GuildRequestCounter.GuildRequestPrefab guildRequestPrefab = this.prefabCache[index];
      this.UpdateHoundRemainTime(guildRequestPrefab.item, guildRequestPrefab.prefab);
      this.UpdateQuestTimer(guildRequestPrefab.item, guildRequestPrefab.prefab);
      this.UpdateBonusRemainTime(guildRequestPrefab.item, guildRequestPrefab.prefab);
      if (guildRequestPrefab.IsHoundTimeupNow() || guildRequestPrefab.IsQuestEndNow())
        this.RefreshUI();
      guildRequestPrefab.SetBeforeTime();
    }
  }

  public override void UpdateUI()
  {
    int count = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Count;
    MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Sort((Comparison<GuildRequestItem>) ((a, b) =>
    {
      if (a.crystalNum != b.crystalNum)
        return a.crystalNum - b.crystalNum;
      if (a.questId > 0 && b.questId <= 0)
        return -1;
      if (a.questId <= 0 && b.questId > 0)
        return 1;
      if (a.GetHoundRemainTime().TotalSeconds > 0.0 && b.GetHoundRemainTime().TotalSeconds <= 0.0)
        return -1;
      return a.GetHoundRemainTime().TotalSeconds <= 0.0 && b.GetHoundRemainTime().TotalSeconds > 0.0 ? 1 : a.slotNo - b.slotNo;
    }));
    this.ShowNonRequestList(count > 0);
    this.prefabCache.Clear();
    bool isExistEmployButton = false;
    this.SetGrid((Enum) GuildRequestCounter.UI.GRD_REQUEST_HOUND, "GuildRequestItem", count, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      GuildRequestItem guildRequestItem = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList[i];
      this.prefabCache.Add(new GuildRequestCounter.GuildRequestPrefab(guildRequestItem, t));
      this.InitButtonColor(guildRequestItem, i, t, b);
      this.UpdateHoundRemainTime(guildRequestItem, t);
      if (guildRequestItem.IsSortieing())
      {
        if (!guildRequestItem.IsComplete() && guildRequestItem.IsExpired())
        {
          this.InitTimeupButton(guildRequestItem, i, t, b);
          return;
        }
        if (!guildRequestItem.IsComplete())
        {
          this.InitSortieingButton(guildRequestItem, i, t, b);
          return;
        }
        if (guildRequestItem.IsComplete())
        {
          this.InitCompleteButton(guildRequestItem, i, t, b);
          return;
        }
      }
      if (guildRequestItem.IsExpired())
      {
        if (isExistEmployButton)
        {
          this.InitInactiveButton(guildRequestItem, i, t, b);
        }
        else
        {
          this.InitEmployButton(guildRequestItem, i, t, b);
          isExistEmployButton = true;
        }
      }
      else
        this.InitHoundStartButton(guildRequestItem, i, t, b);
    }));
    this.InitCompleteAllButton(MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList);
    base.UpdateUI();
  }

  private bool IsOpenFromGachaQuest() => this.selectedQuestInfoData != null;

  private bool IsFromShadow()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Any<GameSectionHistory.HistoryData>((Func<GameSectionHistory.HistoryData, bool>) (h => h.sectionName == "QuestAcceptChallengeCounter" || h.sectionName == "GuildRequestChallengeCounter"));
  }

  private void InitButtonColor(GuildRequestItem item, int index, Transform parent, bool recycle)
  {
    if (item.crystalNum == 0)
    {
      this.SetSprite(parent, (Enum) GuildRequestCounter.UI.SPR_BG, "GuildRequestPlateB");
      this.SetSprite(parent, (Enum) GuildRequestCounter.UI.SPR_QUEST_INFO_BASE, "GuildRequestQuestPlateB");
    }
    else
    {
      this.SetSprite(parent, (Enum) GuildRequestCounter.UI.SPR_BG, "GuildRequestPlateP");
      this.SetSprite(parent, (Enum) GuildRequestCounter.UI.SPR_QUEST_INFO_BASE, "GuildRequestQuestPlateP");
    }
  }

  private void InitTimeupButton(GuildRequestItem item, int index, Transform parent, bool recycle)
  {
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_QUEST_ROOT, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.PBR_GAUGE, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_BONUS_REMAIN_TIME, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_CURRENT_POINT, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_COMPLETE, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_CANCEL, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_CONFIRM, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_COMPLETE_ICON, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_TIMEUP_ICON, true);
    this.SetEvent(this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.BTN_CONFIRM), "CONTINUE", (object) item);
    this.InitQuestButton(item, index, parent);
    this.SetTimeupColor(item, parent);
    this.UpdateQuestTimer(item, parent);
  }

  private void InitInactiveButton(
    GuildRequestItem item,
    int index,
    Transform parent,
    bool recycle)
  {
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_QUEST_ROOT, false);
  }

  private void InitEmployButton(GuildRequestItem item, int index, Transform parent, bool recycle)
  {
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_QUEST_ROOT, false);
    this.SetEvent(this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY), "EMPLOY", (object) item);
  }

  private void InitSortieingButton(
    GuildRequestItem item,
    int index,
    Transform parent,
    bool recycle)
  {
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_QUEST_ROOT, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.PBR_GAUGE, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_BONUS_REMAIN_TIME, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_CURRENT_POINT, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_COMPLETE, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_CANCEL, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_CONFIRM, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_COMPLETE_ICON, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_TIMEUP_ICON, false);
    this.SetEvent(this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.BTN_CANCEL), "CANCEL", (object) item);
    this.SetDefaultColor(item, parent);
    this.InitQuestButton(item, index, parent);
    this.UpdateQuestTimer(item, parent);
  }

  private void InitCompleteButton(
    GuildRequestItem item,
    int index,
    Transform parent,
    bool recycle)
  {
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_QUEST_ROOT, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.PBR_GAUGE, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_BONUS_REMAIN_TIME, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_CURRENT_POINT, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_COMPLETE, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_CANCEL, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_CONFIRM, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_COMPLETE_ICON, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_TIMEUP_ICON, false);
    this.SetEvent(this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.BTN_COMPLETE), "COMPLETE", (object) item);
    this.UpdateBonusRemainTime(item, parent);
    this.SetDefaultColor(item, parent);
    this.InitQuestButton(item, index, parent);
    this.UpdateQuestTimer(item, parent);
  }

  private void InitCompleteAllButton(List<GuildRequestItem> guildRequestItemList)
  {
    bool is_visible = guildRequestItemList.Any<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.IsSortieing() && g.IsComplete()));
    this.SetActive((Enum) GuildRequestCounter.UI.BTN_COMPLETE_ALL, is_visible);
    this.SetActive((Enum) GuildRequestCounter.UI.BTN_COMPLETE_ALL_DISABLE, !is_visible);
  }

  private void InitHoundStartButton(
    GuildRequestItem item,
    int index,
    Transform parent,
    bool recycle)
  {
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_EMPLOY, false);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START, true);
    this.SetActive(parent, (Enum) GuildRequestCounter.UI.OBJ_QUEST_ROOT, false);
    Transform ctrl = this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.BTN_HOUND_START);
    UIButton component = ((Component) ctrl).GetComponent<UIButton>();
    if (this.IsOpenFromGachaQuest() && this.selectedQuestNum == 0)
    {
      component.isEnabled = false;
      this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_HOUND_START, new Color(0.5f, 0.5f, 0.5f));
    }
    else
    {
      component.isEnabled = true;
      this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_HOUND_START, new Color(1f, 1f, 1f));
    }
    if (this.IsOpenFromGachaQuest())
      this.SetEvent(ctrl, "SORTIE", (object) item);
    else
      this.SetEvent(ctrl, "SELECT", (object) item);
  }

  private void InitQuestButton(GuildRequestItem item, int index, Transform parent)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) item.questId);
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(questData.rarity), this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
    this.SetLabelText(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_NAME, questData.questText);
  }

  private void SetDefaultColor(GuildRequestItem item, Transform parent)
  {
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_QUEST_INFO_BASE, new Color(1f, 1f, 1f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_NAME, new Color(1f, 1f, 1f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_CURRENT_POINT, new Color(1f, 1f, 1f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_GAUGE, new Color(1f, 1f, 1f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_GAUGE_BG, new Color(1f, 1f, 1f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, new Color(1f, 1f, 1f));
  }

  private void SetTimeupColor(GuildRequestItem item, Transform parent)
  {
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_QUEST_INFO_BASE, new Color(0.5f, 0.5f, 0.5f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_NAME, new Color(0.5f, 0.5f, 0.5f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_CURRENT_POINT, new Color(0.5f, 0.5f, 0.5f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_GAUGE, new Color(0.5f, 0.5f, 0.5f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.SPR_GAUGE_BG, new Color(0.5f, 0.5f, 0.5f));
    this.SetColor(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, new Color(0.5f, 0.5f, 0.5f));
  }

  private void UpdateQuestTimer(GuildRequestItem item, Transform parent)
  {
    this.SetQuestRemainTime(item, parent);
    this.SetQuestPoint(item, parent);
  }

  private void SetQuestRemainTime(GuildRequestItem item, Transform parent)
  {
    double totalSeconds = item.GetQuestRemainTime().TotalSeconds;
    if (totalSeconds < 0.0)
    {
      this.SetActive(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, false);
    }
    else
    {
      string text = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 11U), (object) UIUtility.TimeFormat((int) totalSeconds, true));
      this.SetLabelText(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_REMAIN_TIME, text);
    }
  }

  private void SetQuestPoint(GuildRequestItem item, Transform parent)
  {
    double totalSeconds = item.GetQuestRemainTime().TotalSeconds;
    if (totalSeconds < 0.0)
    {
      this.SetProgressValue(parent, (Enum) GuildRequestCounter.UI.PBR_GAUGE, 1f);
    }
    else
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) item.questId);
      TimeSpan needTime = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTime(questData.rarity);
      float num1 = (float) ((needTime.TotalSeconds - totalSeconds) / needTime.TotalSeconds);
      this.SetProgressValue(parent, (Enum) GuildRequestCounter.UI.PBR_GAUGE, num1);
      int needPoint = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questData.rarity);
      int questRemainPoint = item.GetQuestRemainPoint();
      int num2 = needPoint - questRemainPoint;
      this.SetLabelText(parent, (Enum) GuildRequestCounter.UI.LBL_QUEST_CURRENT_POINT, $"{(object) num2}/{(object) needPoint}pt");
    }
  }

  private void UpdateHoundRemainTime(GuildRequestItem item, Transform parent)
  {
    double totalSeconds = item.GetHoundRemainTime().TotalSeconds;
    Transform ctrl = this.FindCtrl(parent, (Enum) GuildRequestCounter.UI.LBL_HOUND_REMAIN_TIME);
    UILabel component = ((Component) ctrl).GetComponent<UILabel>();
    if (item.crystalNum > 0)
    {
      string format = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 15U);
      string str = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, (uint) (16 /*0x10*/ + item.slotNo - 1));
      if (totalSeconds < 0.0)
      {
        string text = string.Format(format, (object) str, (object) UIUtility.TimeFormat(0, true));
        this.SetLabelText(ctrl, text);
        this.SetColor(ctrl, Color.yellow);
        component.effectStyle = UILabel.Effect.None;
      }
      else
      {
        string text = string.Format(format, (object) str, (object) UIUtility.TimeFormat((int) totalSeconds, true));
        this.SetLabelText(ctrl, text);
        this.SetColor(ctrl, Color.yellow);
        component.effectStyle = UILabel.Effect.None;
      }
    }
    else
    {
      string text = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 12U);
      this.SetLabelText(ctrl, text);
      this.SetColor(ctrl, Color.white);
      component.effectStyle = UILabel.Effect.Outline8;
      component.effectColor = Color.black;
    }
  }

  private void UpdateBonusRemainTime(GuildRequestItem item, Transform parent)
  {
    string text = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 14U), (object) item.GetBonusRemainTimeWithFormat());
    this.SetLabelText(parent, (Enum) GuildRequestCounter.UI.LBL_BONUS_REMAIN_TIME, text);
  }

  private void ShowNonRequestList(bool isShow)
  {
    if (isShow && MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData != null && MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Count == 0)
    {
      this.SetActive((Enum) GuildRequestCounter.UI.LBL_REQUEST_NON_LIST, true);
      this.SetLabelText((Enum) GuildRequestCounter.UI.LBL_REQUEST_NON_LIST, StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 100U));
    }
    else
      this.SetActive((Enum) GuildRequestCounter.UI.LBL_REQUEST_NON_LIST, false);
  }

  protected void SendGetChallengeInfo(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo((Action<bool, Error>) ((is_success, err) =>
    {
      if (onFinish != null)
        onFinish();
      if (cb == null)
        return;
      cb(is_success);
    }));
  }

  private void OnQuery_SELECT()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
  }

  private void OnQuery_EMPLOY()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
    GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
    GameSection.SetEventData((object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 1U), (object) selectedItem.crystalNum));
  }

  private void OnQuery_GuildRequestEmploy_YES()
  {
    if (!GameSection.CheckCrystal(MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem().crystalNum))
      return;
    GameSection.SetEventData((object) StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 2U));
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestExtend((Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
  }

  private void OnQuery_CANCEL()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
    GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
    int needPoint = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(Singleton<QuestTable>.I.GetQuestData((uint) selectedItem.questId).rarity);
    int questRemainPoint = selectedItem.GetQuestRemainPoint();
    int num = needPoint - questRemainPoint;
    GameSection.SetEventData((object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 3U), (object) $"{(object) num}/{(object) needPoint}", (object) selectedItem.GetQuestRemainTimeWithFormat()));
  }

  private void OnQuery_GuildRequestCancel_YES()
  {
    uint selectedQuestId = (uint) MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem().questId;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestRetire((Action<bool>) (isSuccess => this.SendGetChallengeInfo((System.Action) (() =>
    {
      this.UpdateSelectedQuestNum(1, selectedQuestId);
      GameSection.ResumeEvent(isSuccess);
    }), (Action<bool>) null)));
  }

  private void UpdateSelectedQuestNum(int i, uint selectedQuestId)
  {
    if (!this.IsOpenFromGachaQuest())
      return;
    if (this.IsFromShadow())
    {
      this.selectedQuestNum = MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num;
    }
    else
    {
      if ((int) selectedQuestId != (int) this.selectedQuestInfoData.questData.tableData.questID)
        return;
      this.selectedQuestNum += i;
    }
  }

  private void OnQuery_COMPLETE()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestComplete((Action<GuildRequestCompleteModel.Param>) (questCompleteData =>
    {
      if (!MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList)
      {
        GameSection.ResumeEvent(questCompleteData != null);
        GameSection.SetEventData((object) questCompleteData);
      }
      else
        MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList((Action<bool>) (b =>
        {
          GameSection.ResumeEvent(questCompleteData != null);
          GameSection.SetEventData((object) questCompleteData);
        }));
    }));
  }

  private void OnQuery_COMPLETE_ALL()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestCompleteAll((Action<GuildRequestCompleteModel.Param>) (questCompleteData =>
    {
      MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = true;
      if (!MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList)
      {
        GameSection.ResumeEvent(questCompleteData != null);
        GameSection.SetEventData((object) questCompleteData);
      }
      else
        MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList((Action<bool>) (b =>
        {
          GameSection.ResumeEvent(questCompleteData != null);
          GameSection.SetEventData((object) questCompleteData);
        }));
    }));
  }

  private void OnQuery_CONTINUE()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
    GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
    int needPoint = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(Singleton<QuestTable>.I.GetQuestData((uint) selectedItem.questId).rarity);
    int questRemainPoint = selectedItem.GetQuestRemainPoint();
    int num = needPoint - questRemainPoint;
    GameSection.SetEventData((object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 4U), (object) selectedItem.crystalNum, (object) $"{(object) num}/{(object) needPoint}", (object) selectedItem.GetQuestRemainTimeWithFormat()));
  }

  private void OnQuery_DETAIL() => GameSection.SetEventData((object) WebViewManager.GuildRequest);

  private void OnQuery_SORTIE()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
    GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
    QuestInfoData selectedQuestInfoData = this.selectedQuestInfoData;
    string str = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(selectedQuestInfoData.questData.tableData.rarity).ToString();
    string needTimeWithFormat = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(selectedQuestInfoData.questData.tableData.rarity);
    string remainTimeWithFormat = selectedItem.GetHoundRemainTimeWithFormat();
    TimeSpan needTime = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTime(selectedQuestInfoData.questData.tableData.rarity);
    TimeSpan houndRemainTime = selectedItem.GetHoundRemainTime();
    GameSection.SetEventData(0.0 >= houndRemainTime.TotalSeconds || !(houndRemainTime < needTime) ? (object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 0U), (object) str, (object) needTimeWithFormat) : (object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 5U), (object) str, (object) needTimeWithFormat, (object) remainTimeWithFormat));
  }

  protected virtual void OnQuery_GuildRequestCounterSortieMessage_YES()
  {
    QuestInfoData selectedQuestInfoData = this.selectedQuestInfoData;
    bool flag = this.IsFromShadow();
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestStart(selectedQuestInfoData, !flag, (Action<bool>) (isSuccess => this.SendGetChallengeInfo((System.Action) (() =>
    {
      this.UpdateSelectedQuestNum(-1, this.selectedQuestInfoData.questData.tableData.questID);
      GameSection.ResumeEvent(isSuccess);
    }), (Action<bool>) null)));
  }

  protected virtual void OnQuery_CLOSE()
  {
    if (!this.IsOpenFromGachaQuest())
      return;
    GameSection.ChangeEvent("BACK_TO_QUEST_SELECT");
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirty((Enum) GuildRequestCounter.UI.GRD_REQUEST_HOUND);
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE;
  }

  private enum UI
  {
    GRD_REQUEST_HOUND,
    LBL_REQUEST_NON_LIST,
    BTN_COMPLETE_ALL,
    BTN_COMPLETE_ALL_DISABLE,
    SPR_BG,
    LBL_HOUND_REMAIN_TIME,
    BTN_HOUND_START,
    SPR_HOUND_START,
    BTN_EMPLOY,
    OBJ_QUEST_ROOT,
    SPR_QUEST_INFO_BASE,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    OBJ_COMPLETE_ICON,
    OBJ_TIMEUP_ICON,
    PBR_GAUGE,
    SPR_GAUGE,
    SPR_GAUGE_BG,
    LBL_QUEST_REMAIN_TIME,
    LBL_BONUS_REMAIN_TIME,
    LBL_QUEST_CURRENT_POINT,
    BTN_COMPLETE,
    BTN_CANCEL,
    BTN_CONFIRM,
  }

  private class GuildRequestPrefab
  {
    public GuildRequestItem item;
    private TimeSpan beforeHoundRemainTime;
    private TimeSpan beforeQuestRemainTime;
    public Transform prefab;

    public GuildRequestPrefab(GuildRequestItem item, Transform prefab)
    {
      this.item = item;
      this.prefab = prefab;
      this.SetBeforeTime();
    }

    public void SetBeforeTime()
    {
      this.beforeHoundRemainTime = this.item.GetHoundRemainTime();
      this.beforeQuestRemainTime = this.item.GetQuestRemainTime();
    }

    public bool IsHoundTimeupNow()
    {
      return this.beforeHoundRemainTime.TotalSeconds > 0.0 && this.item.GetHoundRemainTime().TotalSeconds <= 0.0;
    }

    public bool IsQuestEndNow()
    {
      return this.beforeQuestRemainTime.TotalSeconds > 0.0 && this.item.GetQuestRemainTime().TotalSeconds <= 0.0;
    }
  }
}

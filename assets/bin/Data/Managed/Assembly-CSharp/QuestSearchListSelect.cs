// Decompiled with JetBrains decompiler
// Type: QuestSearchListSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestSearchListSelect : QuestSearchListSelectBase
{
  private const string LIST_ITEM_PREFAB_NAME = "QuestSearchListSelectItem";

  protected override void SendSearchRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendSearch((Action<bool, Error>) ((is_success, err) =>
    {
      onFinish();
      if (!is_success && this.isInitialized)
      {
        if (err != Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
          return;
        GameSection.ChangeStayEvent("NOT_FOUND_QUEST");
        if (cb == null)
          return;
        cb(true);
      }
      else
      {
        if (cb == null)
          return;
        cb(is_success);
      }
    }), false);
  }

  protected override void ResetSearchRequest()
  {
    MonoBehaviourSingleton<PartyManager>.I.ResetSearchRequest();
  }

  public override void UpdateUI()
  {
    QuestSearchRoomCondition.SearchRequestParam searchRequest = MonoBehaviourSingleton<PartyManager>.I.searchRequest;
    bool flag1 = (searchRequest.questTypeBit & 4) != 0;
    bool flag2 = (searchRequest.questTypeBit & 2) != 0;
    bool flag3 = (searchRequest.questTypeBit & 1) != 0;
    bool flag4 = (searchRequest.questTypeBit & 16 /*0x10*/) != 0;
    bool flag5 = flag3 & flag2 & flag1 & flag4 || !(flag3 | flag2 | flag1 | flag4);
    string text1 = string.Empty;
    string text2 = string.Empty;
    if (searchRequest.order == 0)
    {
      text1 = this.sectionData.GetText("STR_SELECT_CONDITION_RECOMMEND");
      text2 = string.Empty;
    }
    else if (flag5)
    {
      text1 = this.sectionData.GetText("STR_SELECT_CONDITION_ALL");
      text2 = string.Empty;
    }
    else
    {
      if (flag1 && string.IsNullOrEmpty(text1))
        text1 = this.sectionData.GetText("STR_SELECT_CONDITION_GACHA");
      if (flag2)
      {
        if (string.IsNullOrEmpty(text1))
          text1 = this.sectionData.GetText("STR_SELECT_CONDITION_EVENT");
        else if (string.IsNullOrEmpty(text2))
          text2 = this.sectionData.GetText("STR_SELECT_CONDITION_EVENT");
      }
      if (flag3)
      {
        if (string.IsNullOrEmpty(text1))
          text1 = this.sectionData.GetText("STR_SELECT_CONDITION_NORMAL");
        else if (string.IsNullOrEmpty(text2))
          text2 = this.sectionData.GetText("STR_SELECT_CONDITION_NORMAL");
      }
    }
    this.SetLabelText((Enum) QuestSearchListSelect.UI.LBL_CONDITION_A, text1);
    this.SetLabelText((Enum) QuestSearchListSelect.UI.LBL_CONDITION_B, text2);
    this.SetActive((Enum) QuestSearchListSelect.UI.SPR_CONDITION_DIFFICULTY, false);
    this.SetActive((Enum) QuestSearchListSelect.UI.STR_NO_CONDITION, true);
    this.SetActive((Enum) QuestSearchListSelect.UI.SPR_CHALLENGE_NOT_CLEAR, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.NotClaer());
    this.SetFontStyle((Enum) QuestSearchListSelect.UI.LBL_CHALLENGE_NOT_CLEAR, (FontStyle) 1);
    this.SetNpcMessage();
    if (!PartyManager.IsValidNotEmptyList())
    {
      this.SetActive((Enum) QuestSearchListSelect.UI.GRD_QUEST, false);
      this.SetActive((Enum) QuestSearchListSelect.UI.STR_NON_LIST, true);
    }
    else
    {
      PartyModel.Party[] partys = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
      this.SetActive((Enum) QuestSearchListSelect.UI.GRD_QUEST, true);
      this.SetActive((Enum) QuestSearchListSelect.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) QuestSearchListSelect.UI.GRD_QUEST, "QuestSearchListSelectItem", partys.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) partys[i].quest.questId);
        if (questData == null)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetEvent(t, "SELECT_ROOM", i);
          this.SetQuestData(questData, t);
          if (this.IsPlateChangeQuestType(questData.questType))
            this.SetGateData(partys[i], t, questData.questType);
          else
            this.SetPartyData(partys[i], t);
          this.SetStatusIconInfo(partys[i], t);
        }
      }));
      base.UpdateUI();
    }
  }

  protected void SetGateData(PartyModel.Party party, Transform t, QUEST_TYPE type)
  {
    int num = party.slotInfos.Count<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (slotInfo => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != party.ownerUserId));
    for (int index = 0; index < 3; ++index)
      this.SetToggle(t, (Enum) this.ui[index], index < num);
    if (type == QUEST_TYPE.GATE)
      this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_HOST_NAME, this.sectionData.GetText("GATE_QUEST_MESSAGE"));
    else
      this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_HOST_NAME, "");
    this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_HOST_LV, "");
    this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_LV, "");
  }

  protected override void SetQuestData(QuestTable.QuestTableData questData, Transform t)
  {
    QuestTable.QuestTableData table = questData;
    this.ResetTween(t, (Enum) QuestSearchListSelect.UI.TWN_DIFFICULT_STAR);
    this.PlayTween(t, (Enum) QuestSearchListSelect.UI.TWN_DIFFICULT_STAR, is_input_block: false);
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) table.GetEnemyIdByIndex(0));
    if (enemyData != null)
    {
      this.SetActive(t, (Enum) QuestSearchListSelect.UI.OBJ_ENEMY, true);
      ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, table.questType == QUEST_TYPE.ORDER ? new RARITY_TYPE?(table.rarity) : new RARITY_TYPE?(), this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
      this.SetActive(t, (Enum) QuestSearchListSelect.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
      this.SetElementSprite(t, (Enum) QuestSearchListSelect.UI.SPR_ELEMENT, (int) enemyData.element);
      this.SetElementSprite(t, (Enum) QuestSearchListSelect.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
      this.SetActive(t, (Enum) QuestSearchListSelect.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
    }
    else
    {
      this.SetActive(t, (Enum) QuestSearchListSelect.UI.OBJ_ENEMY, false);
      this.SetElementSprite(t, (Enum) QuestSearchListSelect.UI.SPR_WEAK_ELEMENT, 6);
      this.SetActive(t, (Enum) QuestSearchListSelect.UI.STR_NON_WEAK_ELEMENT, true);
    }
    Transform ctrl1 = this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.SPR_ICON_DOUBLE);
    Transform ctrl2 = this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.SPR_ICON_DEFENSE_BATTLE);
    Transform ctrl3 = this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.SPR_ICON_SERIES_OF_BATTLES);
    Transform ctrl4 = this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.LBL_RECRUTING_MEMBERS);
    Transform ctrl5 = this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.SPR_ICON_WAVE_MATCH);
    Transform ctrl6 = this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.SPR_WINDOW_BASE);
    if (Object.op_Inequality((Object) ctrl6, (Object) null))
    {
      UISprite component1 = ((Component) ctrl6).GetComponent<UISprite>();
      UISprite component2 = ((Component) this.FindCtrl(t, (Enum) QuestSearchListSelect.UI.OBJ_SEARCH_INFO_ROOT)).GetComponent<UISprite>();
      if (this.IsPlateChangeQuestType(table.questType))
      {
        component1.spriteName = "QuestListPlateO";
        component2.spriteName = "SearchAdWindowO";
        ((Component) ctrl1).gameObject.SetActive(true);
        ((Component) ctrl2).gameObject.SetActive(table.questType == QUEST_TYPE.DEFENSE);
        ((Component) ctrl5).gameObject.SetActive(table.questType == QUEST_TYPE.WAVE || table.questType == QUEST_TYPE.WAVE_STRATEGY);
        ((Component) ctrl3).gameObject.SetActive(table.questType == QUEST_TYPE.SERIES);
        ((Component) ctrl4).gameObject.SetActive(this.IsReqrutingMembersQuestType(table.questType));
        string format = StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 0U);
        string text = "";
        if (enemyData != null)
          text = string.Format(format, (object) questData.GetMainEnemyLv(), (object) enemyData.name);
        this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_QUEST_NAME, text);
        this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_QUEST_NUM, "");
      }
      else
      {
        component1.spriteName = "QuestListPlateN";
        component2.spriteName = "SearchAdWindow";
        ((Component) ctrl1).gameObject.SetActive(false);
        ((Component) ctrl2).gameObject.SetActive(false);
        ((Component) ctrl5).gameObject.SetActive(false);
        ((Component) ctrl3).gameObject.SetActive(false);
        ((Component) ctrl4).gameObject.SetActive(false);
        this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_QUEST_NAME, table.questText);
        this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_QUEST_NUM, string.Format(this.sectionData.GetText("QUEST_NUMBER"), (object) table.locationNumber, (object) table.questNumber));
      }
    }
    else
    {
      this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_QUEST_NAME, table.questText);
      this.SetLabelText(t, (Enum) QuestSearchListSelect.UI.LBL_QUEST_NUM, string.Format(this.sectionData.GetText("QUEST_NUMBER"), (object) table.locationNumber, (object) table.questNumber));
    }
    this.SetMemberIcon(t, table);
  }

  private bool IsPlateChangeQuestType(QUEST_TYPE questType)
  {
    return questType == QUEST_TYPE.GATE || questType == QUEST_TYPE.DEFENSE || questType == QUEST_TYPE.WAVE || questType == QUEST_TYPE.WAVE_STRATEGY || questType == QUEST_TYPE.SERIES;
  }

  private bool IsReqrutingMembersQuestType(QUEST_TYPE questType)
  {
    return questType == QUEST_TYPE.DEFENSE || questType == QUEST_TYPE.WAVE || questType == QUEST_TYPE.WAVE_STRATEGY || questType == QUEST_TYPE.SERIES;
  }

  private void OnCloseDialog_QuestSearchRoomCondition() => this.CloseSearchRoomCondition();

  protected void ResetSearchRequestTemp()
  {
    if (!MonoBehaviourSingleton<PartyManager>.IsValid())
      return;
    MonoBehaviourSingleton<PartyManager>.I.ResetSearchRequestTemp();
  }

  protected override void OnDestroy()
  {
    this.ResetSearchRequestTemp();
    base.OnDestroy();
  }

  protected new enum UI
  {
    GRD_QUEST,
    LBL_HOST_NAME,
    LBL_HOST_LV,
    TGL_MEMBER_1,
    TGL_MEMBER_2,
    TGL_MEMBER_3,
    LBL_LV,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
    STR_NON_LIST,
    BTN_CONDITION,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    OBJ_ICON,
    OBJ_ICON_NEW,
    OBJ_ICON_CLEARED,
    OBJ_ICON_COMPLETE,
    SPR_ICON_NEW,
    SPR_ICON_CLEARED,
    SPR_ICON_COMPLETE,
    LBL_CONDITION_A,
    LBL_CONDITION_B,
    LBL_CONDITION_DIFFICULTY,
    SPR_CONDITION_DIFFICULTY,
    STR_NO_CONDITION,
    LBL_CONDITION_ENEMY,
    OBJ_NPC_MESSAGE,
    SPR_WINDOW_BASE,
    SPR_ICON_DOUBLE,
    OBJ_SEARCH_INFO_ROOT,
    OBJ_QUEST_INFO_ROOT,
    OBJ_ORDER_QUEST_INFO_ROOT,
    SPR_ORDER_RARITY_FRAME,
    SPR_CHALLENGE_NOT_CLEAR,
    LBL_CHALLENGE_NOT_CLEAR,
    SPR_ICON_DEFENSE_BATTLE,
    LBL_RECRUTING_MEMBERS,
    SPR_ICON_WAVE_MATCH,
    SPR_ICON_SERIES_OF_BATTLES,
  }
}

// Decompiled with JetBrains decompiler
// Type: ClanQuestBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanQuestBoard : GameSection
{
  private ClanQuestBoard.UI[] memberUI = new ClanQuestBoard.UI[3]
  {
    ClanQuestBoard.UI.TGL_MEMBER_1,
    ClanQuestBoard.UI.TGL_MEMBER_2,
    ClanQuestBoard.UI.TGL_MEMBER_3
  };
  private List<PartyModel.Party> parties;

  public override void Initialize()
  {
    this.SetLabelText((Enum) ClanQuestBoard.UI.LBL_TITLE, this.sectionData.GetText("TITLE"));
    this.SetLabelText((Enum) ClanQuestBoard.UI.LBL_TITLE_SHADOW, this.sectionData.GetText("TITLE"));
    this.SetLabelText((Enum) ClanQuestBoard.UI.STR_NON_LIST, this.sectionData.GetText("NON_QUEST"));
    this.SetActive((Enum) ClanQuestBoard.UI.SCR_QUEST, true);
    this.StartCoroutine(this.DoInitialize());
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) ClanQuestBoard.UI.SPR_CONDITION_DIFFICULTY, false);
    this.SetActive((Enum) ClanQuestBoard.UI.STR_NO_CONDITION, true);
    this.SetNpcInfo();
    if (this.parties == null)
    {
      this.SetActive((Enum) ClanQuestBoard.UI.GRD_QUEST, false);
      this.SetActive((Enum) ClanQuestBoard.UI.STR_NON_LIST, true);
    }
    else if (this.parties.Count <= 0)
    {
      this.SetActive((Enum) ClanQuestBoard.UI.GRD_QUEST, false);
      this.SetActive((Enum) ClanQuestBoard.UI.STR_NON_LIST, true);
    }
    else
    {
      this.SetActive((Enum) ClanQuestBoard.UI.GRD_QUEST, true);
      this.SetActive((Enum) ClanQuestBoard.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) ClanQuestBoard.UI.GRD_QUEST, "QuestSearchListSelectItem", this.parties.Count, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        QuestTable.QuestTableData questTableData = this.parties[i].quest.explore == null ? Singleton<QuestTable>.I.GetQuestData((uint) this.parties[i].quest.questId) : Singleton<QuestTable>.I.GetQuestData((uint) this.parties[i].quest.explore.mainQuestId);
        if (questTableData == null)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetEvent(t, "SELECT_ROOM", i);
          this.SetQuestData(questTableData, t);
          this.SetPartyData(this.parties[i], t, questTableData.questType);
          this.SetStatusIconInfo(this.parties[i], t);
          this.SetMemberIcon(t, questTableData);
        }
      }));
    }
  }

  protected void SetStatusIconInfo(PartyModel.Party _partyParam, Transform _targetObject)
  {
    if (Object.op_Equality((Object) _targetObject, (Object) null) || _partyParam == null)
      return;
    QuestUserStatusIconController componentInChildren = ((Component) _targetObject).GetComponentInChildren<QuestUserStatusIconController>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.Initialize(new QuestUserStatusIconController.InitParam()
    {
      StatusBit = (uint) _partyParam.iconBit
    });
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) this.StartCoroutine(this.Reload());
  }

  private IEnumerator Reload(Action<bool> cb = null)
  {
    bool isRecv = false;
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendRoomParty((Action<bool, List<PartyModel.Party>>) ((isSuccess, parties) =>
    {
      if (isSuccess)
      {
        this.parties = parties;
        isRecv = true;
      }
      if (cb == null)
        return;
      cb(isSuccess);
    }));
    isRecv = true;
    while (!isRecv)
      yield return (object) null;
    this.SetDirty((Enum) ClanQuestBoard.UI.GRD_QUEST);
    this.RefreshUI();
    base.Initialize();
  }

  private void SetPartyData(PartyModel.Party party, Transform t, QUEST_TYPE type)
  {
    int member_num = 0;
    party.slotInfos.ForEach((Action<PartyModel.SlotInfo>) (data =>
    {
      if (data == null || data.userInfo == null)
        return;
      if (data.userInfo.userId == party.ownerUserId)
      {
        this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_HOST_NAME, data.userInfo.name);
        this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_HOST_LV, data.userInfo.level.ToString());
      }
      else
        ++member_num;
    }));
    for (int index = 0; index < 3; ++index)
      this.SetToggle(t, (Enum) this.memberUI[index], index < member_num);
    this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_LV, this.sectionData.GetText("LV"));
    if (type != QUEST_TYPE.GATE && type != QUEST_TYPE.DEFENSE)
      return;
    if (type == QUEST_TYPE.GATE)
      this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_HOST_NAME, this.sectionData.GetText("GATE_QUEST_MESSAGE"));
    else
      this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_HOST_NAME, "");
    this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_HOST_LV, "");
    this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_LV, "");
  }

  private void SetQuestData(QuestTable.QuestTableData questData, Transform t)
  {
    ClanQuestBoard.UI[] uiArray = new ClanQuestBoard.UI[10]
    {
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_1,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_2,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_3,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_4,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_5,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_6,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_7,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_8,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_9,
      ClanQuestBoard.UI.OBJ_DIFFICULT_STAR_10
    };
    QuestTable.QuestTableData questTableData = questData;
    int num = (int) (questTableData.difficulty + 1);
    int index = 0;
    for (int length = uiArray.Length; index < length; ++index)
      this.SetActive(t, (Enum) uiArray[index], index < num);
    this.ResetTween(t, (Enum) ClanQuestBoard.UI.TWN_DIFFICULT_STAR);
    this.PlayTween(t, (Enum) ClanQuestBoard.UI.TWN_DIFFICULT_STAR, is_input_block: false);
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questTableData.GetMainEnemyID());
    if (enemyData != null)
    {
      this.SetActive(t, (Enum) ClanQuestBoard.UI.OBJ_ENEMY, true);
      ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, questTableData.questType == QUEST_TYPE.ORDER ? new RARITY_TYPE?(questTableData.rarity) : new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ClanQuestBoard.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
      this.SetActive(t, (Enum) ClanQuestBoard.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
      this.SetElementSprite(t, (Enum) ClanQuestBoard.UI.SPR_ELEMENT, (int) enemyData.element);
      this.SetElementSprite(t, (Enum) ClanQuestBoard.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
      this.SetActive(t, (Enum) ClanQuestBoard.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
    }
    else
    {
      this.SetActive(t, (Enum) ClanQuestBoard.UI.OBJ_ENEMY, false);
      this.SetElementSprite(t, (Enum) ClanQuestBoard.UI.SPR_WEAK_ELEMENT, 6);
      this.SetActive(t, (Enum) ClanQuestBoard.UI.STR_NON_WEAK_ELEMENT, true);
    }
    Transform ctrl1 = this.FindCtrl(t, (Enum) ClanQuestBoard.UI.SPR_ICON_DOUBLE);
    Transform ctrl2 = this.FindCtrl(t, (Enum) ClanQuestBoard.UI.SPR_ICON_DEFENSE_BATTLE);
    Transform ctrl3 = this.FindCtrl(t, (Enum) ClanQuestBoard.UI.LBL_RECRUTING_MEMBERS);
    Transform ctrl4 = this.FindCtrl(t, (Enum) ClanQuestBoard.UI.SPR_WINDOW_BASE);
    if (Object.op_Inequality((Object) ctrl4, (Object) null))
    {
      UISprite component1 = ((Component) ctrl4).GetComponent<UISprite>();
      UISprite component2 = ((Component) this.FindCtrl(t, (Enum) ClanQuestBoard.UI.OBJ_SEARCH_INFO_ROOT)).GetComponent<UISprite>();
      if (questTableData.questType == QUEST_TYPE.GATE || questTableData.questType == QUEST_TYPE.DEFENSE)
      {
        component1.spriteName = "QuestListPlateO";
        component2.spriteName = "SearchAdWindowO";
        ((Component) ctrl1).gameObject.SetActive(true);
        ((Component) ctrl2).gameObject.SetActive(questTableData.questType == QUEST_TYPE.DEFENSE);
        ((Component) ctrl3).gameObject.SetActive(questTableData.questType == QUEST_TYPE.DEFENSE);
        string format = StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 0U);
        string text = "";
        if (enemyData != null)
          text = string.Format(format, (object) questData.GetMainEnemyLv(), (object) enemyData.name);
        this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_QUEST_NAME, text);
        this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_QUEST_NUM, "");
      }
      else
      {
        component1.spriteName = "QuestListPlateN";
        component2.spriteName = "SearchAdWindow";
        ((Component) ctrl1).gameObject.SetActive(false);
        ((Component) ctrl2).gameObject.SetActive(false);
        ((Component) ctrl3).gameObject.SetActive(false);
        this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_QUEST_NAME, questTableData.questText);
        this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_QUEST_NUM, string.Format(this.sectionData.GetText("QUEST_NUMBER"), (object) questTableData.locationNumber, (object) questTableData.questNumber));
      }
    }
    else
    {
      this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_QUEST_NAME, questTableData.questText);
      this.SetLabelText(t, (Enum) ClanQuestBoard.UI.LBL_QUEST_NUM, string.Format(this.sectionData.GetText("QUEST_NUMBER"), (object) questTableData.locationNumber, (object) questTableData.questNumber));
    }
  }

  private void SetNpcInfo()
  {
    string text = this.parties.Count > 0 ? this.sectionData.GetText("EXIST_LIST_MSG") : this.sectionData.GetText("NON_LIST_MSG");
    this.SetRenderNPCModel((Enum) ClanQuestBoard.UI.TEX_NPCMODEL, 7, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.boardCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.boardCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.boardCenterNPCFOV);
    this.SetLabelText((Enum) ClanQuestBoard.UI.LBL_MESSAGE, text);
  }

  private void OnQuery_SELECT_ROOM()
  {
    int eventData = (int) GameSection.GetEventData();
    if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint) MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties[eventData].quest.questId))
    {
      GameSection.StopEvent();
    }
    else
    {
      GameSection.SetEventData((object) new object[1]
      {
        (object) false
      });
      GameSection.StayEvent();
      MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties[eventData].id, true, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
  }

  private void OnQuery_RELOAD()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  private void OnQuery_MEMBER()
  {
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData != null)
      return;
    GameSection.ChangeEvent("ERROR");
  }

  protected void SetMemberIcon(Transform t, QuestTable.QuestTableData table)
  {
    if (table == null)
      return;
    this.SetActive(t, (Enum) ClanQuestBoard.UI.TGL_MEMBER_3, true);
    this.SetActive(t, (Enum) ClanQuestBoard.UI.TGL_MEMBER_2, true);
    this.SetActive(t, (Enum) ClanQuestBoard.UI.TGL_MEMBER_1, true);
    if (table.userNumLimit < 4)
      this.SetActive(t, (Enum) ClanQuestBoard.UI.TGL_MEMBER_3, false);
    if (table.userNumLimit < 3)
      this.SetActive(t, (Enum) ClanQuestBoard.UI.TGL_MEMBER_2, false);
    if (table.userNumLimit >= 2)
      return;
    this.SetActive(t, (Enum) ClanQuestBoard.UI.TGL_MEMBER_1, false);
  }

  private enum UI
  {
    LBL_TITLE,
    LBL_TITLE_SHADOW,
    SCR_QUEST,
    GRD_QUEST,
    STR_NON_LIST,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    OBJ_ENEMY,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    SPR_CONDITION_DIFFICULTY,
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
    LBL_HOST_NAME,
    LBL_HOST_LV,
    TGL_MEMBER_1,
    TGL_MEMBER_2,
    TGL_MEMBER_3,
    STR_NO_CONDITION,
    SPR_WINDOW_BASE,
    SPR_ICON_DOUBLE,
    OBJ_SEARCH_INFO_ROOT,
    LBL_LV,
    TEX_NPCMODEL,
    LBL_MESSAGE,
    SPR_ICON_DEFENSE_BATTLE,
    LBL_RECRUTING_MEMBERS,
  }
}

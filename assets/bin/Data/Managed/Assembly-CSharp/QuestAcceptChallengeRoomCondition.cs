// Decompiled with JetBrains decompiler
// Type: QuestAcceptChallengeRoomCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class QuestAcceptChallengeRoomCondition : QuestSearchRoomCondition
{
  private int maxLevel;
  private int maxLevelIndex;
  private Transform levelPopup;
  private QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam challengeRequest;

  public override void Initialize()
  {
    this.searchRequest = (QuestSearchRoomCondition.SearchRequestParam) new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();
    this.challengeRequest = this.searchRequest as QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam;
    base.Initialize();
    this.maxLevel = MonoBehaviourSingleton<UserInfoManager>.I.GetEnemyLevelFromUserLevel();
    bool flag = false;
    for (int index = 0; index < this.enemyLevelList.Count; ++index)
    {
      if (this.enemyLevelList[index] == this.challengeRequest.enemyLevel)
        this.challengeRequest.enemyLevelIndex = index;
      if (this.enemyLevelList[index] == this.maxLevel)
      {
        this.maxLevelIndex = index;
        flag = true;
      }
    }
    if (!flag)
    {
      this.challengeRequest.enemyLevel = this.enemyLevelList[0];
      this.challengeRequest.enemyLevelIndex = 0;
      this.maxLevel = this.enemyLevelList[0];
      this.maxLevelIndex = 0;
    }
    this.UpdateEnemyLevel();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.UpdateEnemyLevel();
  }

  protected virtual QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam GetInitChallengeSearchParam()
  {
    if (!(GameSection.GetEventData() is QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam challengeSearchParam))
      challengeSearchParam = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();
    return challengeSearchParam;
  }

  protected override void LoadSearchRequestParam()
  {
  }

  protected override void CopySearchRequestParam()
  {
    QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam challengeSearchParam = this.GetInitChallengeSearchParam();
    this.challengeRequest.order = challengeSearchParam.order;
    this.challengeRequest.rarityBit = challengeSearchParam.rarityBit;
    this.challengeRequest.elementBit = challengeSearchParam.elementBit;
    this.challengeRequest.enemyLevelMin = challengeSearchParam.enemyLevelMin;
    this.challengeRequest.enemyLevelMax = challengeSearchParam.enemyLevelMax;
    this.challengeRequest.targetEnemySpeciesName = challengeSearchParam.targetEnemySpeciesName;
    this.challengeRequest.questTypeBit = challengeSearchParam.questTypeBit;
    this.challengeRequest.enemyLevel = challengeSearchParam.enemyLevel;
  }

  protected override void CreateEnemyLevelPopText()
  {
    int num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX / 10 + 1;
    for (int index = 1; index < num; ++index)
      this.enemyLevelList.Add(10 * index);
    for (int index = 0; index < this.enemyLevelList.Count; ++index)
      this.enemyLevelNames.Add(this.enemyLevelList[index].ToString());
  }

  private void UpdateEnemyLevel()
  {
    this.SetLabelText((Enum) QuestAcceptChallengeRoomCondition.UI.LBL_TARGET_LEVEL, this.enemyLevelNames[this.challengeRequest.enemyLevelIndex]);
  }

  private void OnQuery_TARGET_LEVEL() => this.ShowLevelPopup();

  private void ShowLevelPopup()
  {
    if (Object.op_Equality((Object) this.levelPopup, (Object) null))
      this.levelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) QuestAcceptChallengeRoomCondition.UI.POP_TARGET_LEVEL), false);
    if (Object.op_Equality((Object) this.levelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.enemyLevelNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index <= this.maxLevelIndex;
    int enemyLevelIndex = this.challengeRequest.enemyLevelIndex;
    UIScrollablePopupList.CreatePopup(this.levelPopup, this.GetCtrl((Enum) QuestAcceptChallengeRoomCondition.UI.POP_TARGET_LEVEL), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.enemyLevelNames.ToArray(), button_enable, enemyLevelIndex, (Action<int>) (index =>
    {
      this.challengeRequest.enemyLevelIndex = index;
      this.challengeRequest.enemyLevel = this.enemyLevelList[index];
      this.RefreshUI();
    }));
  }

  protected override void OnQuery_SEARCH()
  {
    this.FixBit();
    if (this.challengeRequest.rarityBit == 0)
      GameSection.ChangeEvent("NOT_RARITY");
    else if (this.challengeRequest.elementBit == 0)
    {
      GameSection.ChangeEvent("NOT_ELEMENT");
    }
    else
    {
      this.challengeRequest.order = 1;
      GameSection.SetEventData((object) this.challengeRequest);
      base.OnQuery_SEARCH();
    }
  }

  protected override void SendSearch()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(this.challengeRequest, (Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
        this.OnNotFoundQuest();
      GameSection.ResumeEvent(true);
    }), true);
  }

  protected override void OnQuery_SPECIES_SEARCH_REQUEST()
  {
    int eventData = (int) GameSection.GetEventData();
    this.challengeRequest.enemySpeciesIndex = 0;
    this.challengeRequest.targetEnemySpeciesName = (string) null;
    if (Singleton<GachaSearchEnemyTable>.IsValid())
    {
      GachaSearchEnemyTable.GachaSearchEnemyData[] gachaSearchEnemyData = Singleton<GachaSearchEnemyTable>.I.GetSortedGachaSearchEnemyData();
      for (int index = 0; index < gachaSearchEnemyData.Length; ++index)
      {
        if (eventData == gachaSearchEnemyData[index].id)
        {
          this.challengeRequest.targetEnemySpeciesName = gachaSearchEnemyData[index].name;
          break;
        }
      }
    }
    this.challengeRequest.order = 1;
    GameSection.SetEventData((object) this.challengeRequest);
    this.SendSearch();
  }

  public new enum UI
  {
    BTN_N,
    BTN_HN,
    BTN_R,
    BTN_HR,
    BTN_SR,
    BTN_HSR,
    BTN_SSR,
    TGL_ORDER,
    TGL_EVENT,
    TGL_STORY,
    POP_TARGET_ENEMY_TYPE,
    LBL_TARGET_ENEMY_TYPE,
    BTN_FIRE,
    BTN_WATER,
    BTN_THUNDER,
    BTN_SOIL,
    BTN_LIGHT,
    BTN_DARK,
    BTN_NO_ELEMENT,
    POP_TARGET_MIN_LEVEL,
    POP_TARGET_MAX_LEVEL,
    LBL_TARGET_MIN_LEVEL,
    LBL_TARGET_MAX_LEVEL,
    OBJ_SEARCH,
    OBJ_MY_SEARCH,
    OBJ_FRAME,
    PRIORITY_ROOT,
    TGL_BTN_FRIEND,
    TGL_BTN_CLAN,
    POP_TARGET_LEVEL,
    LBL_TARGET_LEVEL,
  }

  public class ChallengeSearchRequestParam : QuestSearchRoomCondition.SearchRequestParam
  {
    public int enemyLevelIndex;
    public int enemyLevel;

    public override bool IsMatchLevel(QuestItemInfo item)
    {
      return this.enemyLevel == item.infoData.questData.tableData.GetMainEnemyLv();
    }

    public ChallengeSearchRequestParam()
    {
      this.enemyLevelIndex = 0;
      this.enemyLevel = 0;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: QuestSearchRoomCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestSearchRoomCondition : QuestSearchRoomConditionBase
{
  protected static readonly QuestSearchRoomCondition.UI[] rarityButton = new QuestSearchRoomCondition.UI[7]
  {
    QuestSearchRoomCondition.UI.BTN_N,
    QuestSearchRoomCondition.UI.BTN_HN,
    QuestSearchRoomCondition.UI.BTN_R,
    QuestSearchRoomCondition.UI.BTN_HR,
    QuestSearchRoomCondition.UI.BTN_SR,
    QuestSearchRoomCondition.UI.BTN_HSR,
    QuestSearchRoomCondition.UI.BTN_SSR
  };
  protected static readonly QuestSearchRoomCondition.UI[] elementButton = new QuestSearchRoomCondition.UI[7]
  {
    QuestSearchRoomCondition.UI.BTN_FIRE,
    QuestSearchRoomCondition.UI.BTN_WATER,
    QuestSearchRoomCondition.UI.BTN_THUNDER,
    QuestSearchRoomCondition.UI.BTN_SOIL,
    QuestSearchRoomCondition.UI.BTN_LIGHT,
    QuestSearchRoomCondition.UI.BTN_DARK,
    QuestSearchRoomCondition.UI.BTN_NO_ELEMENT
  };
  protected QuestSearchRoomCondition.SearchRequestParam searchRequest = new QuestSearchRoomCondition.SearchRequestParam();
  protected GachaSearchEnemyTable.GachaSearchEnemyData[] sortedAllSpeciesData;
  protected GachaSearchEnemyTable.GachaSearchEnemyData[] sortedTargetSpeciesData;
  protected List<string> enemyLevelNames;
  protected List<int> enemyLevelList;
  protected List<string> enemySpeciesNames;
  private Transform speciesPopup;
  private Transform minLevelPopup;
  private Transform maxLevelPopup;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "GachaSearchEnemyTable";
    }
  }

  public override void Initialize()
  {
    this.LoadSearchRequestParam();
    this.CopySearchRequestParam();
    this.SetupRarityButtons();
    this.SetupElementButtons();
    this.sortedAllSpeciesData = this.sortedTargetSpeciesData = Singleton<GachaSearchEnemyTable>.I.GetSortedGachaSearchEnemyData();
    this.CreateEnemySpeciesPopText();
    for (int index = 0; index < this.enemySpeciesNames.Count; ++index)
    {
      if (this.enemySpeciesNames[index] == this.searchRequest.targetEnemySpeciesName)
      {
        this.searchRequest.enemySpeciesIndex = index;
        break;
      }
    }
    this.enemyLevelNames = new List<string>();
    this.enemyLevelList = new List<int>();
    this.CreateEnemyLevelPopText();
    bool flag = false;
    for (int index = 0; index < this.enemyLevelList.Count; ++index)
    {
      if (this.enemyLevelList[index] == this.searchRequest.enemyLevelMin)
        this.searchRequest.enemyMinLevelIndex = index;
      if (this.enemyLevelList[index] == this.searchRequest.enemyLevelMax)
      {
        flag = true;
        this.searchRequest.enemyMaxLevelIndex = index;
      }
    }
    if (!flag)
    {
      this.searchRequest.enemyLevelMax = this.enemyLevelList[this.enemyLevelList.Count - 1];
      this.searchRequest.enemyMaxLevelIndex = this.enemyLevelList.Count - 1;
    }
    this.SetActive((Enum) QuestSearchRoomCondition.UI.OBJ_SEARCH, true);
    this.SetActive((Enum) QuestSearchRoomCondition.UI.OBJ_MY_SEARCH, false);
    GameSection.SetEventData((object) false);
    base.Initialize();
  }

  protected override void LoadSearchRequestParam()
  {
    if (MonoBehaviourSingleton<PartyManager>.I.searchRequestTemp != null)
      return;
    MonoBehaviourSingleton<PartyManager>.I.SetSearchRequestFromPrefs();
  }

  protected override void CopySearchRequestParam()
  {
    QuestSearchRoomCondition.SearchRequestParam searchRequest = MonoBehaviourSingleton<PartyManager>.I.searchRequest;
    this.searchRequest.order = searchRequest.order;
    this.searchRequest.rarityBit = searchRequest.rarityBit;
    this.searchRequest.elementBit = searchRequest.elementBit;
    this.searchRequest.isCs = searchRequest.isCs;
    this.searchRequest.isFs = searchRequest.isFs;
    this.searchRequest.enemyLevelMin = searchRequest.enemyLevelMin;
    this.searchRequest.enemyLevelMax = searchRequest.enemyLevelMax;
    this.searchRequest.enemyMinLevelIndex = searchRequest.enemyMinLevelIndex;
    this.searchRequest.enemyMaxLevelIndex = searchRequest.enemyMaxLevelIndex;
    this.searchRequest.targetEnemySpeciesName = searchRequest.targetEnemySpeciesName;
    this.searchRequest.questTypeBit = searchRequest.questTypeBit;
  }

  protected void SetupRarityButtons()
  {
    for (int event_data = 0; event_data < QuestSearchRoomCondition.rarityButton.Length; ++event_data)
      this.SetEvent((Enum) QuestSearchRoomCondition.rarityButton[event_data], "RARITY", event_data);
  }

  protected void SetupElementButtons()
  {
    for (int event_data = 0; event_data < QuestSearchRoomCondition.elementButton.Length; ++event_data)
      this.SetEvent((Enum) QuestSearchRoomCondition.elementButton[event_data], "ELEMENT", event_data);
  }

  protected void CreateEnemySpeciesPopText()
  {
    this.FilterSpeciesPopupOnRarity();
    this.FilterSpeciesPopupOnElement(this.sortedTargetSpeciesData);
    this.enemySpeciesNames = Singleton<GachaSearchEnemyTable>.I.GetGachaSearchEnemyNames(this.sortedTargetSpeciesData);
    this.enemySpeciesNames.Insert(0, this.sectionData.GetText("NO_CONDITION"));
  }

  protected virtual void CreateEnemyLevelPopText()
  {
    int searchQuestLevelMax = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_LEVEL_MAX;
    int questExtraLevelMax = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX;
    this.enemyLevelList = new List<int>();
    int num = searchQuestLevelMax / 10 + 1;
    for (int index = 0; index < num; ++index)
    {
      if (index == 0)
        this.enemyLevelList.Add(1);
      else
        this.enemyLevelList.Add(10 * index);
    }
    if (questExtraLevelMax >= 0)
      this.enemyLevelList.Add(questExtraLevelMax);
    for (int index = 0; index < this.enemyLevelList.Count; ++index)
      this.enemyLevelNames.Add(this.enemyLevelList[index].ToString());
  }

  public override void UpdateUI()
  {
    this.UpdateRarityToggles();
    this.UpdateElementToggles();
    this.UpdateEnemyMinLevel();
    this.UpdateEnemyMaxLevel();
    this.UpdateEnemySpecies();
  }

  private void UpdateRarityToggles()
  {
    foreach (RARITY_TYPE rarity in Enum.GetValues(typeof (RARITY_TYPE)))
      this.SetToggle(this.GetRairtyToggleTransform((int) rarity), (this.searchRequest.rarityBit & 1 << (int) (rarity & (RARITY_TYPE) 31 /*0x1F*/)) != 0);
  }

  private Transform GetRairtyToggleTransform(int rarity)
  {
    return this.GetCtrl((Enum) QuestSearchRoomCondition.rarityButton[rarity]).parent;
  }

  private void UpdateElementToggles()
  {
    foreach (ELEMENT_TYPE elementIndex in Enum.GetValues(typeof (ELEMENT_TYPE)))
    {
      Transform elementToggleTransform = this.GetElementToggleTransform((int) elementIndex);
      if (Object.op_Inequality((Object) elementToggleTransform, (Object) null))
        this.SetToggle(elementToggleTransform, (this.searchRequest.elementBit & 1 << (int) (elementIndex & (ELEMENT_TYPE) 31 /*0x1F*/)) != 0);
    }
  }

  private Transform GetElementToggleTransform(int elementIndex)
  {
    return elementIndex >= QuestSearchRoomCondition.elementButton.Length || elementIndex < 0 ? (Transform) null : this.GetCtrl((Enum) QuestSearchRoomCondition.elementButton[elementIndex]).parent;
  }

  private void UpdateEnemyMinLevel()
  {
    this.SetLabelText((Enum) QuestSearchRoomCondition.UI.LBL_TARGET_MIN_LEVEL, this.enemyLevelNames[this.searchRequest.enemyMinLevelIndex]);
  }

  private void UpdateEnemyMaxLevel()
  {
    this.SetLabelText((Enum) QuestSearchRoomCondition.UI.LBL_TARGET_MAX_LEVEL, this.enemyLevelNames[this.searchRequest.enemyMaxLevelIndex]);
  }

  private void UpdateEnemySpecies()
  {
    this.FilterSpeciesPopupOnRarity();
    this.FilterSpeciesPopupOnElement(this.sortedTargetSpeciesData);
    this.UpdateSelectingEnemySpecies();
    this.SetLabelText((Enum) QuestSearchRoomCondition.UI.LBL_TARGET_ENEMY_TYPE, this.enemySpeciesNames[this.searchRequest.enemySpeciesIndex]);
  }

  private void FilterSpeciesPopupOnRarity(
    GachaSearchEnemyTable.GachaSearchEnemyData[] targetData = null)
  {
    if (targetData == null)
      targetData = this.sortedAllSpeciesData;
    this.sortedTargetSpeciesData = Singleton<GachaSearchEnemyTable>.I.GetEnemyDataOnRairtyFlag(targetData, this.searchRequest.rarityBit);
    this.enemySpeciesNames = Singleton<GachaSearchEnemyTable>.I.GetGachaSearchEnemyNames(this.sortedTargetSpeciesData);
    this.enemySpeciesNames.Insert(0, this.sectionData.GetText("NO_CONDITION"));
  }

  private void FilterSpeciesPopupOnElement(
    GachaSearchEnemyTable.GachaSearchEnemyData[] targetData = null)
  {
    if (targetData == null)
      targetData = this.sortedAllSpeciesData;
    this.sortedTargetSpeciesData = Singleton<GachaSearchEnemyTable>.I.GetEnemyDataOnElementFlag(targetData, this.searchRequest.elementBit);
    this.enemySpeciesNames = Singleton<GachaSearchEnemyTable>.I.GetGachaSearchEnemyNames(this.sortedTargetSpeciesData);
    this.enemySpeciesNames.Insert(0, this.sectionData.GetText("NO_CONDITION"));
  }

  private void UpdateSelectingEnemySpecies()
  {
    if (string.IsNullOrEmpty(this.searchRequest.targetEnemySpeciesName))
      return;
    bool flag = false;
    for (int index = 0; index < this.enemySpeciesNames.Count; ++index)
    {
      if (this.enemySpeciesNames[index] == this.searchRequest.targetEnemySpeciesName)
      {
        this.searchRequest.enemySpeciesIndex = index;
        flag = true;
        break;
      }
    }
    if (flag)
      return;
    this.searchRequest.enemySpeciesIndex = 0;
    this.searchRequest.targetEnemySpeciesName = (string) null;
  }

  private void OnQuery_RARITY()
  {
    this.searchRequest.rarityBit ^= 1 << (int) GameSection.GetEventData();
    this.RefreshUI();
  }

  private void OnQuery_ELEMENT()
  {
    this.searchRequest.elementBit ^= 1 << (int) GameSection.GetEventData();
    this.RefreshUI();
  }

  private void OnQuery_FRIEND()
  {
    this.searchRequest.isFs = this.searchRequest.isFs == 1 ? 0 : 1;
    this.RefreshUI();
  }

  private void OnQuery_CLAN()
  {
    this.searchRequest.isCs = this.searchRequest.isCs == 1 ? 0 : 1;
    this.RefreshUI();
  }

  private void OnQuery_TARGET_ENEMY_TYPE() => this.ShowEnemySpeciesPopup();

  private void ShowEnemySpeciesPopup()
  {
    if (Object.op_Equality((Object) this.speciesPopup, (Object) null))
      this.speciesPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) QuestSearchRoomCondition.UI.POP_TARGET_ENEMY_TYPE), false);
    if (Object.op_Equality((Object) this.speciesPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.enemySpeciesNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int select_index = 0;
    for (int index = 0; index < this.enemySpeciesNames.Count; ++index)
    {
      if (this.enemySpeciesNames[index] == this.searchRequest.targetEnemySpeciesName)
      {
        this.searchRequest.enemySpeciesIndex = index;
        select_index = this.searchRequest.enemySpeciesIndex;
        break;
      }
    }
    UIScrollablePopupList.CreatePopup(this.speciesPopup, this.GetCtrl((Enum) QuestSearchRoomCondition.UI.POP_TARGET_ENEMY_TYPE), 8, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.enemySpeciesNames.ToArray(), button_enable, select_index, (Action<int>) (index =>
    {
      this.searchRequest.enemySpeciesIndex = index;
      this.searchRequest.targetEnemySpeciesName = this.enemySpeciesNames[index];
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_MIN_LEVEL() => this.ShowMinLevelPopup();

  private void ShowMinLevelPopup()
  {
    if (Object.op_Equality((Object) this.minLevelPopup, (Object) null))
      this.minLevelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) QuestSearchRoomCondition.UI.POP_TARGET_MIN_LEVEL), false);
    if (Object.op_Equality((Object) this.minLevelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.enemyLevelNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index <= this.searchRequest.enemyMaxLevelIndex;
    int enemyMinLevelIndex = this.searchRequest.enemyMinLevelIndex;
    List<string> stringList = new List<string>((IEnumerable<string>) this.enemyLevelNames);
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX > 0)
      stringList.RemoveAt(stringList.Count - 1);
    UIScrollablePopupList.CreatePopup(this.minLevelPopup, this.GetCtrl((Enum) QuestSearchRoomCondition.UI.POP_TARGET_MIN_LEVEL), 6, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, stringList.ToArray(), button_enable, enemyMinLevelIndex, (Action<int>) (index =>
    {
      this.searchRequest.enemyMinLevelIndex = index;
      this.searchRequest.enemyLevelMin = this.enemyLevelList[index];
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_MAX_LEVEL() => this.ShowMaxLevelPopup();

  private void ShowMaxLevelPopup()
  {
    if (Object.op_Equality((Object) this.maxLevelPopup, (Object) null))
      this.maxLevelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) QuestSearchRoomCondition.UI.POP_TARGET_MAX_LEVEL), false);
    if (Object.op_Equality((Object) this.maxLevelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.enemyLevelNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index >= this.searchRequest.enemyMinLevelIndex;
    int enemyMaxLevelIndex = this.searchRequest.enemyMaxLevelIndex;
    UIScrollablePopupList.CreatePopup(this.maxLevelPopup, this.GetCtrl((Enum) QuestSearchRoomCondition.UI.POP_TARGET_MAX_LEVEL), 6, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.enemyLevelNames.ToArray(), button_enable, enemyMaxLevelIndex, (Action<int>) (index =>
    {
      this.searchRequest.enemyMaxLevelIndex = index;
      this.searchRequest.enemyLevelMax = this.enemyLevelList[index];
      this.RefreshUI();
    }));
  }

  protected override void OnQuery_SEARCH()
  {
    this.FixBit();
    if (this.searchRequest.rarityBit == 0)
      GameSection.ChangeEvent("NOT_RARITY");
    else if (this.searchRequest.elementBit == 0)
      GameSection.ChangeEvent("NOT_ELEMENT");
    else
      base.OnQuery_SEARCH();
  }

  protected override void SetCondition()
  {
    this.searchRequest.order = 1;
    MonoBehaviourSingleton<PartyManager>.I.SetSearchRequest(this.searchRequest);
  }

  protected override void SendSearch()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendSearch((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
        this.OnNotFoundQuest();
      GameSection.ResumeEvent(true);
    }), true);
  }

  protected override void OnQuery_MATCHING()
  {
    this.FixBit();
    if (this.searchRequest.rarityBit == 0)
      GameSection.ChangeEvent("NOT_RARITY");
    else if (this.searchRequest.elementBit == 0)
      GameSection.ChangeEvent("NOT_ELEMENT");
    else
      base.OnQuery_MATCHING();
  }

  protected override void SendRandomMatching()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendSearchRandomMatching((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success)
        this.OnNotFoundMatchingParty();
      GameSection.ResumeEvent(true);
    }));
  }

  protected void FixBit()
  {
    int num1 = 0;
    for (int index = 0; index < QuestSearchRoomCondition.rarityButton.Length; ++index)
    {
      int num2 = index;
      if ((this.searchRequest.rarityBit & 1 << num2) != 0 && ((Component) this.GetCtrl((Enum) QuestSearchRoomCondition.rarityButton[index])).gameObject.activeInHierarchy)
        num1 |= 1 << num2;
    }
    this.searchRequest.rarityBit = num1;
    int num3 = 0;
    for (int index = 0; index < QuestSearchRoomCondition.elementButton.Length; ++index)
    {
      int num4 = index;
      if ((this.searchRequest.elementBit & 1 << num4) != 0 && ((Component) this.GetCtrl((Enum) QuestSearchRoomCondition.elementButton[index])).gameObject.activeInHierarchy)
        num3 |= 1 << num4;
    }
    this.searchRequest.elementBit = num3;
  }

  protected virtual void OnQuery_SPECIES_SEARCH_REQUEST()
  {
    int eventData = (int) GameSection.GetEventData();
    QuestSearchRoomCondition.SearchRequestParam request = new QuestSearchRoomCondition.SearchRequestParam();
    request.enemySpeciesIndex = 0;
    request.targetEnemySpeciesName = (string) null;
    if (Singleton<GachaSearchEnemyTable>.IsValid())
    {
      GachaSearchEnemyTable.GachaSearchEnemyData[] gachaSearchEnemyData = Singleton<GachaSearchEnemyTable>.I.GetSortedGachaSearchEnemyData();
      for (int index = 0; index < gachaSearchEnemyData.Length; ++index)
      {
        if (eventData == gachaSearchEnemyData[index].id)
        {
          request.targetEnemySpeciesName = gachaSearchEnemyData[index].name;
          break;
        }
      }
    }
    request.order = 1;
    MonoBehaviourSingleton<PartyManager>.I.SetSearchRequestTemp(request);
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendSearch((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
        this.OnNotFoundQuest();
      GameSection.ResumeEvent(true);
    }), false);
  }

  public enum UI
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
  }

  [Flags]
  private enum FILTER
  {
    NORMAL = 1,
    ORDER = 4,
    GATE = 16, // 0x00000010
    ALL = GATE | ORDER | NORMAL, // 0x00000015
  }

  public class SearchRequestParam
  {
    public int order;
    public int rarityBit;
    public int elementBit;
    public int enemyMinLevelIndex;
    public int enemyLevelMin;
    public int enemyMaxLevelIndex;
    public int enemyLevelMax;
    public int enemySpeciesIndex;
    public string targetEnemySpeciesName;
    public int isFs;
    public int isCs;
    public int questTypeBit;

    public int GetEnemySpeciesId(string name)
    {
      return Singleton<GachaSearchEnemyTable>.I.GetEnemySpeciesId(name);
    }

    public bool IsMatchRarity(QuestItemInfo item)
    {
      for (int index = 0; index < QuestSearchRoomCondition.rarityButton.Length; ++index)
      {
        int num = index;
        if ((this.rarityBit & 1 << num) > 0 && item.infoData.questData.tableData.rarity == (RARITY_TYPE) num)
          return true;
      }
      return false;
    }

    public bool IsMatchElement(QuestItemInfo item)
    {
      QuestTable.QuestTableData tableData = item.infoData.questData.tableData;
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) tableData.GetMainEnemyID());
      for (int index = 0; index < QuestSearchRoomCondition.elementButton.Length; ++index)
      {
        int num = index;
        if ((this.elementBit & 1 << num) > 0 && enemyData.element == (ELEMENT_TYPE) num)
          return true;
      }
      return false;
    }

    public virtual bool IsMatchLevel(QuestItemInfo item)
    {
      QuestTable.QuestTableData tableData = item.infoData.questData.tableData;
      return this.enemyLevelMin <= tableData.GetMainEnemyLv() && tableData.GetMainEnemyLv() <= this.enemyLevelMax;
    }

    public bool IsMatchEnemySpecies(QuestItemInfo item)
    {
      int enemySpeciesId = this.GetEnemySpeciesId(this.targetEnemySpeciesName);
      return enemySpeciesId <= 0 || Singleton<EnemyTable>.I.GetEnemyData((uint) item.infoData.questData.tableData.GetMainEnemyID()).enemySpecies == enemySpeciesId;
    }

    public SearchRequestParam()
    {
      this.order = 0;
      this.rarityBit = 8388607 /*0x7FFFFF*/;
      this.elementBit = 8388607 /*0x7FFFFF*/;
      this.enemyMinLevelIndex = 0;
      this.enemyMaxLevelIndex = 0;
      this.isCs = 1;
      this.isFs = 1;
      this.enemySpeciesIndex = 0;
      this.questTypeBit = 21;
    }
  }
}

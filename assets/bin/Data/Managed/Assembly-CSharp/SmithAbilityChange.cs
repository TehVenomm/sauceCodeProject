// Decompiled with JetBrains decompiler
// Type: SmithAbilityChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class SmithAbilityChange : GameSection
{
  private EquipItemInfo equipItemInfo;
  private AbilityChangeAbilityList abilityList;

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS;
  }

  public override void Initialize()
  {
    this.equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
    this.abilityList = ((Component) this.SetPrefab((Enum) SmithAbilityChange.UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList")).gameObject.AddComponent<AbilityChangeAbilityList>();
    this.abilityList.uiVisible = true;
    this.SetLabelText((Enum) SmithAbilityChange.UI.STR_DECISION_REFLECT, this.sectionData.GetText("STR_DECISION"));
    this.SetLabelText((Enum) SmithAbilityChange.UI.STR_LIST_REFLECT, this.sectionData.GetText("STR_LIST"));
    base.Initialize();
  }

  public override void InitializeReopen()
  {
    this.equipItemInfo = GameSection.GetEventData() as EquipItemInfo;
    base.InitializeReopen();
  }

  protected override void OnOpen()
  {
    this.abilityList.InitUI();
    this.abilityList.Open();
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    this.SetFontStyle((Enum) SmithAbilityChange.UI.STR_TITLE_MONEY, (FontStyle) 2);
    this.abilityList.SetParameter(this.equipItemInfo);
    int needMoney = this.GetNeedMoney();
    this.SetLabelText((Enum) SmithAbilityChange.UI.LBL_GOLD, needMoney.ToString());
    Color color = Color.white;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < needMoney)
      color = Color.red;
    this.SetColor((Enum) SmithAbilityChange.UI.LBL_GOLD, color);
    this.SetActive((Enum) SmithAbilityChange.UI.BTN_USE_ABILITY_ITEM, this.equipItemInfo.tableData.IsEquipableAbilityItem());
    this.SetActive((Enum) SmithAbilityChange.UI.BTN_DECISION, this.abilityList.EnableAbilityChange);
    this.SetActive((Enum) SmithAbilityChange.UI.OBJ_NEED_MONEY, this.abilityList.EnableAbilityChange);
  }

  private int GetNeedMoney()
  {
    return MonoBehaviourSingleton<SmithManager>.I.GetAbilityChangeNeedMoney(this.equipItemInfo);
  }

  private void OnQuery_START()
  {
    SmithManager.ERR_SMITH_SEND errSmithSend = MonoBehaviourSingleton<SmithManager>.I.CheckAbilityChange(this.equipItemInfo);
    if (errSmithSend != SmithManager.ERR_SMITH_SEND.NONE)
      GameSection.ChangeEvent(errSmithSend.ToString());
    else
      GameSection.SetEventData((object) new string[1]
      {
        this.GetNeedMoney().ToString()
      });
  }

  private void OnQuery_SmithConfirmAbilityChange_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<SmithManager>.I.SendAbilityChangeEquipItem(this.equipItemInfo.uniqueID, (Action<Error, EquipItemInfo>) ((error, model) =>
    {
      if (error == Error.None)
      {
        MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = model;
        MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
        GameSection.ResumeEvent(true);
      }
      else
        GameSection.ResumeEvent(false);
    }));
  }

  private void OnQuery_SECTION_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((Predicate<GameSectionHistory.HistoryData>) (x => x.sectionName == "SmithAbilityChangeSelect")))
      return;
    GameSection.StopEvent();
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }

  protected void OnQuery_LOTTERY_LIST()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData,
      (object) SmithEquipBase.SmithType.ABILITY_CHANGE
    });
  }

  private enum UI
  {
    STR_TITLE_MONEY,
    STR_DECISION_REFLECT,
    STR_LIST_REFLECT,
    OBJ_ABILITY_LIST_ROOT,
    LBL_GOLD,
    BTN_USE_ABILITY_ITEM,
    BTN_DECISION,
    OBJ_NEED_MONEY,
  }

  private enum BACK_TO
  {
    STATUS_TOP,
    STATUS_TOP_EQUIPDETAIL,
  }
}

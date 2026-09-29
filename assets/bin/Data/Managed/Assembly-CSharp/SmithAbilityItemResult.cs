// Decompiled with JetBrains decompiler
// Type: SmithAbilityItemResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class SmithAbilityItemResult : GameSection
{
  private EquipItemInfo equipItemInfo;
  private AbilityChangeAbilityList abilityList;
  private int endDirectionCount;
  private bool m_isFirstOpen;
  private bool isFirstTap = true;

  public override string overrideBackKeyEvent => "GO_BACK";

  public override void Initialize()
  {
    this.equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
    this.abilityList = ((Component) this.SetPrefab((Enum) SmithAbilityItemResult.UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList")).gameObject.AddComponent<AbilityChangeAbilityList>();
    this.abilityList.uiVisible = true;
    this.SetRenderEquipModel((Enum) SmithAbilityItemResult.UI.TEX_MODEL, this.equipItemInfo.tableID, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
    this.SetLabelText((Enum) SmithAbilityItemResult.UI.STR_NEXT_REFLECT, this.sectionData.GetText("STR_NEXT"));
    this.ResetTween((Enum) SmithAbilityItemResult.UI.OBJ_DELAY_2);
    MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
    this.SetFullScreenButton((Enum) SmithAbilityItemResult.UI.BTN_TAP_FULL_SCREEN);
    this.m_isFirstOpen = true;
    base.Initialize();
  }

  protected override void OnOpen()
  {
    this.abilityList.InitUI();
    this.abilityList.Open();
    this.PlayTween((Enum) SmithAbilityItemResult.UI.OBJ_DELAY_1, callback: new EventDelegate.Callback(this.PlayDirection));
    this.SetActive((Enum) SmithAbilityItemResult.UI.OBJ_DELAY_2, false);
    base.OnOpen();
  }

  public override void UpdateUI() => this.abilityList.SetParameter(this.equipItemInfo);

  private void EndAllDirection()
  {
    this.SetActive((Enum) SmithAbilityItemResult.UI.OBJ_DELAY_2, true);
    this.ResetTween((Enum) SmithAbilityItemResult.UI.OBJ_DELAY_2);
    this.PlayTween((Enum) SmithAbilityItemResult.UI.OBJ_DELAY_2);
    this.SetActive((Enum) SmithAbilityItemResult.UI.TEX_MODEL, false);
    this.RefreshUI();
  }

  private void PlayDirection() => this.StartCoroutine(this._PlayDirection());

  private IEnumerator _PlayDirection()
  {
    this.PlayDirection(SmithAbilityItemResult.UI.OBJ_DIRECTION_1, SmithAbilityItemResult.UI.LBL_ADD_ABILITY_1, this.equipItemInfo.GetAbilityItem().GetName());
    yield return (object) new WaitForSeconds(0.46f);
    if (this.m_isFirstOpen)
      SoundManager.PlayOneShotUISE(40000049);
    this.m_isFirstOpen = false;
  }

  private void PlayDirection(
    SmithAbilityItemResult.UI directionObj,
    SmithAbilityItemResult.UI label,
    string text)
  {
    this.SetFontStyle((Enum) label, (FontStyle) 2);
    this.SetLabelText((Enum) label, text);
    this.PlayTween((Enum) directionObj, callback: new EventDelegate.Callback(this.EndAllDirection), is_input_block: false);
  }

  private void OnQuery_NEXT() => GameSection.SetEventData((object) this.equipItemInfo);

  private void OnQuery_GO_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((Predicate<GameSectionHistory.HistoryData>) (x => x.sectionName == "SmithAbilityChangeSelect")))
      return;
    GameSection.StopEvent();
    this.DispatchEvent("MAIN_MENU_STATUS");
  }

  private void OnQuery_TAP_FULL_SCREEN()
  {
    if (!this.isFirstTap)
      return;
    this.isFirstTap = false;
    this.equipItemInfo.GetLotteryAbility();
    UITweenCtrl.SetDurationWithRate(this.GetCtrl((Enum) SmithAbilityItemResult.UI.OBJ_DIRECTION_1), 0.5f);
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
    TEX_MODEL,
    STR_NEXT_REFLECT,
    OBJ_DELAY_1,
    OBJ_DELAY_2,
    OBJ_ABILITY_LIST_ROOT,
    OBJ_DIRECTION_1,
    LBL_ADD_ABILITY_1,
    BTN_TAP_FULL_SCREEN,
  }
}

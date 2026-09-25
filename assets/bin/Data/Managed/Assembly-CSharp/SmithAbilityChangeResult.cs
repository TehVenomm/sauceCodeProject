// Decompiled with JetBrains decompiler
// Type: SmithAbilityChangeResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class SmithAbilityChangeResult : GameSection
{
  private EquipItemInfo equipItemInfo;
  private AbilityChangeAbilityList abilityList;
  private int endDirectionCount;
  private bool m_isFirstOpen;
  private bool isFirstTap = true;
  private bool isTimerStart;
  private float timer;
  private static readonly SmithAbilityChangeResult.UI[] UiDirection = new SmithAbilityChangeResult.UI[3]
  {
    SmithAbilityChangeResult.UI.OBJ_DIRECTION_1,
    SmithAbilityChangeResult.UI.OBJ_DIRECTION_2,
    SmithAbilityChangeResult.UI.OBJ_DIRECTION_3
  };

  public override string overrideBackKeyEvent => "GO_BACK";

  public override void Initialize()
  {
    this.equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
    this.abilityList = ((Component) this.SetPrefab((Enum) SmithAbilityChangeResult.UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList")).gameObject.AddComponent<AbilityChangeAbilityList>();
    this.abilityList.uiVisible = true;
    this.SetRenderEquipModel((Enum) SmithAbilityChangeResult.UI.TEX_MODEL, this.equipItemInfo.tableID, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
    this.SetLabelText((Enum) SmithAbilityChangeResult.UI.STR_NEXT_REFLECT, this.sectionData.GetText("STR_NEXT"));
    this.ResetTween((Enum) SmithAbilityChangeResult.UI.OBJ_DELAY_2);
    MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
    this.SetFullScreenButton((Enum) SmithAbilityChangeResult.UI.BTN_TAP_FULL_SCREEN);
    this.m_isFirstOpen = true;
    base.Initialize();
  }

  protected override void OnOpen()
  {
    this.endDirectionCount = 0;
    this.abilityList.InitUI();
    this.abilityList.Open();
    this.PlayTween((Enum) SmithAbilityChangeResult.UI.OBJ_DELAY_1, callback: new EventDelegate.Callback(this.PlayDirection));
    this.SetActive((Enum) SmithAbilityChangeResult.UI.OBJ_DELAY_2, false);
    base.OnOpen();
  }

  public override void UpdateUI() => this.abilityList.SetParameter(this.equipItemInfo);

  private void EndAllDirection()
  {
    this.SetActive((Enum) SmithAbilityChangeResult.UI.OBJ_DELAY_2, true);
    this.ResetTween((Enum) SmithAbilityChangeResult.UI.OBJ_DELAY_2);
    this.PlayTween((Enum) SmithAbilityChangeResult.UI.OBJ_DELAY_2);
    this.SetActive((Enum) SmithAbilityChangeResult.UI.TEX_MODEL, false);
    this.RefreshUI();
  }

  private void PlayDirection() => this.StartCoroutine(this._PlayDirection());

  private IEnumerator _PlayDirection()
  {
    SmithAbilityChangeResult.UI[] ui_add_ability = new SmithAbilityChangeResult.UI[3]
    {
      SmithAbilityChangeResult.UI.LBL_ADD_ABILITY_1,
      SmithAbilityChangeResult.UI.LBL_ADD_ABILITY_2,
      SmithAbilityChangeResult.UI.LBL_ADD_ABILITY_3
    };
    EquipItemAbility[] abilities = this.equipItemInfo.GetLotteryAbility();
    for (int i = 0; i < abilities.Length; ++i)
    {
      this.PlayDirection(SmithAbilityChangeResult.UiDirection[i], ui_add_ability[i], abilities[i]);
      yield return (object) new WaitForSeconds(0.46f);
      if (this.m_isFirstOpen)
        SoundManager.PlayOneShotUISE(40000049);
    }
    this.isTimerStart = true;
    this.m_isFirstOpen = false;
  }

  private void PlayDirection(
    SmithAbilityChangeResult.UI directionObj,
    SmithAbilityChangeResult.UI label,
    EquipItemAbility ability)
  {
    this.SetFontStyle((Enum) label, (FontStyle) 2);
    this.SetLabelText((Enum) label, ability.GetNameAndAP());
    this.PlayTween((Enum) directionObj, callback: new EventDelegate.Callback(this.EndDirection), is_input_block: false);
  }

  private void EndDirection()
  {
    ++this.endDirectionCount;
    if (this.endDirectionCount < this.equipItemInfo.GetValidLotAbility())
      return;
    this.EndAllDirection();
  }

  private void OnQuery_NEXT()
  {
    SmithManager.ERR_SMITH_SEND errSmithSend = MonoBehaviourSingleton<SmithManager>.I.CheckAbilityChange(this.equipItemInfo);
    if (errSmithSend != SmithManager.ERR_SMITH_SEND.NONE)
      GameSection.ChangeEvent(errSmithSend.ToString());
    else
      GameSection.SetEventData((object) new string[1]
      {
        MonoBehaviourSingleton<SmithManager>.I.GetAbilityChangeNeedMoney(this.equipItemInfo).ToString()
      });
  }

  private void OnQuery_GO_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((Predicate<GameSectionHistory.HistoryData>) (x => x.sectionName == "SmithAbilityChangeSelect")))
      return;
    GameSection.StopEvent();
    this.DispatchEvent("MAIN_MENU_STATUS");
  }

  private void OnQuery_SmithConfirmAbilityChangeRetry_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
    {
      new EventData("RETRY", (object) this.equipItemInfo),
      new EventData("SmithConfirmAbilityChange_YES", (object) null),
      new EventData("START_FROM_RESULT", (object) null)
    });
  }

  private void OnQuery_TAP_FULL_SCREEN()
  {
    EquipItemAbility[] lotteryAbility = this.equipItemInfo.GetLotteryAbility();
    if (this.isFirstTap)
    {
      for (int index = 0; index < lotteryAbility.Length; ++index)
        UITweenCtrl.SetDurationWithRate(this.GetCtrl((Enum) SmithAbilityChangeResult.UiDirection[index]), 0.5f);
    }
    float num = 0.5f;
    if (!this.isFirstTap)
      num = 0.35f;
    this.isFirstTap = false;
    if ((double) this.timer <= (double) num)
      return;
    for (int index = 0; index < lotteryAbility.Length; ++index)
      this.SkipTween((Enum) SmithAbilityChangeResult.UiDirection[index]);
  }

  protected void OnQuery_LOTTERY_LIST()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData,
      (object) SmithEquipBase.SmithType.ABILITY_CHANGE
    });
  }

  private void Update()
  {
    if (!this.isTimerStart)
      return;
    this.timer += Time.deltaTime;
  }

  private enum UI
  {
    TEX_MODEL,
    STR_NEXT_REFLECT,
    OBJ_DELAY_1,
    OBJ_DELAY_2,
    OBJ_ABILITY_LIST_ROOT,
    OBJ_DIRECTION_1,
    OBJ_DIRECTION_2,
    OBJ_DIRECTION_3,
    LBL_ADD_ABILITY_1,
    LBL_ADD_ABILITY_2,
    LBL_ADD_ABILITY_3,
    BTN_TAP_FULL_SCREEN,
  }
}

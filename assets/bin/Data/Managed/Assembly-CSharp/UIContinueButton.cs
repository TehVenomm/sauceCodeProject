// Decompiled with JetBrains decompiler
// Type: UIContinueButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIContinueButton : MonoBehaviourSingleton<UIContinueButton>
{
  [SerializeField]
  protected UIButton retireButton;
  [SerializeField]
  protected UIButton restartButton;
  [SerializeField]
  protected UILabel restartButtonLabel;
  [SerializeField]
  protected UIButton continueButton;
  [SerializeField]
  protected UILabel continueButtonLabel;
  [SerializeField]
  protected UILabel continueButtonCrystalNum;
  [SerializeField]
  protected UISprite continueButtonIcon;
  [SerializeField]
  protected GameObject continueButtonEffect;
  [SerializeField]
  protected UILabel continueCount;
  [SerializeField]
  protected UILabel stoneRescuableCount;
  [SerializeField]
  protected TweenPosition[] startAction;
  [SerializeField]
  protected Color[] colors = new Color[11];
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  protected bool onClicked;
  protected bool isDisableButtons;
  private Vector3 orgRetirePos;
  private TweenPosition retireTweenPos;

  protected override void Awake()
  {
    base.Awake();
    this.retireTweenPos = ((Component) this.retireButton).GetComponent<TweenPosition>();
    this.orgRetirePos = this.retireTweenPos.to;
    ((Component) this).gameObject.SetActive(false);
  }

  public void Initialize()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
    MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
    ((Component) this).gameObject.SetActive(true);
    int crystal_num = 0;
    int crystal_use = 0;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      crystal_num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
      crystal_use = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_CONTINUE_USE_CRYSTAL;
      if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialCurrentQuest())
        crystal_use = 0;
    }
    bool isEnableContinue = crystal_num >= crystal_use;
    bool flag = QuestManager.IsValidInGameExplore();
    if (flag)
    {
      this.SetupRestartButton(MonoBehaviourSingleton<StageObjectManager>.I.self.IsAbleToRescueByRemainRescueTime());
    }
    else
    {
      if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
        isEnableContinue = false;
      this.SetupContinueButton(isEnableContinue, crystal_use, crystal_num);
    }
    ((Component) this.continueButton).gameObject.SetActive(!flag && !self.IsStone());
    ((Component) this.restartButton).gameObject.SetActive(flag && !self.IsStone());
    ((Component) this.retireButton).gameObject.SetActive(!self.IsStone());
    float rescueTime = self.rescueTime;
    if (Object.op_Inequality((Object) this.continueCount, (Object) null))
    {
      this.continueCount.color = Color.white;
      this.continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1000U, (object) rescueTime.ToString("F2"));
    }
    float stoneRescueTime = self.stoneRescueTime;
    if (Object.op_Inequality((Object) this.stoneRescuableCount, (Object) null))
    {
      this.stoneRescuableCount.color = Color.yellow;
      this.continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1010U, (object) stoneRescueTime.ToString("F2"));
    }
    int index = 0;
    for (int length = this.startAction.Length; index < length; ++index)
    {
      this.startAction[index].ResetToBeginning();
      this.startAction[index].PlayForward();
    }
  }

  private void SetupContinueButton(bool isEnableContinue, int crystal_use, int crystal_num)
  {
    if (Object.op_Inequality((Object) this.continueButton, (Object) null))
      this.continueButton.isEnabled = isEnableContinue;
    if (Object.op_Inequality((Object) this.continueButtonLabel, (Object) null))
    {
      if (isEnableContinue)
      {
        this.continueButtonLabel.applyGradient = true;
        this.continueButtonLabel.gradientTop = this.colors[0];
        this.continueButtonLabel.gradientBottom = this.colors[1];
        this.continueButtonLabel.effectColor = this.colors[2];
        this.continueButtonLabel.color = Color.white;
      }
      else
      {
        this.continueButtonLabel.applyGradient = false;
        this.continueButtonLabel.color = this.colors[3];
        this.continueButtonLabel.effectColor = this.colors[4];
      }
      if (crystal_use <= 0)
        this.continueButtonLabel.text = StringTable.Get(STRING_CATEGORY.IN_GAME, 1006U);
      else
        this.continueButtonLabel.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1003U, (object) crystal_use);
    }
    if (Object.op_Inequality((Object) this.continueButtonCrystalNum, (Object) null))
    {
      this.continueButtonCrystalNum.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1004U, (object) crystal_num);
      if (isEnableContinue)
      {
        this.continueButtonCrystalNum.color = this.colors[5];
        this.continueButtonCrystalNum.effectColor = this.colors[6];
      }
      else
      {
        this.continueButtonCrystalNum.color = this.colors[7];
        this.continueButtonCrystalNum.effectColor = this.colors[8];
      }
    }
    if (Object.op_Inequality((Object) this.continueButtonIcon, (Object) null))
    {
      if (isEnableContinue)
        this.continueButtonIcon.color = this.colors[9];
      else
        this.continueButtonIcon.color = this.colors[10];
    }
    if (!Object.op_Inequality((Object) this.continueButtonEffect, (Object) null))
      return;
    this.continueButtonEffect.SetActive(isEnableContinue);
  }

  private void SetupRestartButton(bool isEnableRestart)
  {
    if (Object.op_Inequality((Object) this.restartButton, (Object) null))
      this.restartButton.isEnabled = isEnableRestart;
    if (!Object.op_Inequality((Object) this.continueButtonLabel, (Object) null))
      return;
    if (isEnableRestart)
    {
      this.restartButtonLabel.applyGradient = true;
      this.restartButtonLabel.gradientTop = this.colors[0];
      this.restartButtonLabel.gradientBottom = this.colors[1];
      this.restartButtonLabel.effectColor = this.colors[2];
      this.restartButtonLabel.color = Color.white;
    }
    else
    {
      this.restartButtonLabel.applyGradient = false;
      this.restartButtonLabel.color = this.colors[3];
      this.restartButtonLabel.effectColor = this.colors[4];
    }
  }

  private void OnEnable()
  {
    if (!Object.op_Inequality((Object) this.panelChange, (Object) null))
      return;
    this.panelChange.UnLock();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!Object.op_Inequality((Object) this.panelChange, (Object) null))
      return;
    this.panelChange.Lock();
  }

  public bool CheckVisible()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null) || self.actionID != (Character.ACTION_ID) 24 && self.actionID != (Character.ACTION_ID) 43 || !self.isDead && !self.IsStone() || MonoBehaviourSingleton<CoopManager>.IsValid() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() && (double) self.continueTime <= 0.0 && (double) self.stoneRescueTime <= 0.0)
      return false;
    if (QuestManager.IsValidInGameExplore() && !self.IsStone() && !MonoBehaviourSingleton<StageObjectManager>.I.self.IsAbleToRescueByRemainRescueTime())
    {
      this.DoRetire();
      return false;
    }
    if (!QuestManager.IsValidInGameSeriesArena() || self.IsStone())
      return true;
    this.DoRetire();
    return false;
  }

  private void Update()
  {
    if (!this.CheckVisible())
    {
      MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      if (Object.op_Equality((Object) self, (Object) null))
        return;
      if (this.IsContiueable() && !self.IsStone())
      {
        float rescueTime = self.rescueTime;
        if ((double) rescueTime > 0.0)
        {
          this.continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1000U, (object) rescueTime.ToString("F2"));
          this.continueCount.color = Color.white;
        }
        else
        {
          if (QuestManager.IsValidInGameExplore())
          {
            if (MonoBehaviourSingleton<StageObjectManager>.I.self.IsAbleToRescueByRemainRescueTime())
              this.DoRestart();
            else
              this.DoRetire();
          }
          this.continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1001U, (object) self.continueTime.ToString("F2"));
          this.continueCount.color = Color.red;
        }
      }
      else
        this.continueCount.text = "";
      if (self.IsStone())
      {
        float stoneRescueTime = self.stoneRescueTime;
        if ((double) stoneRescueTime > 0.0)
        {
          this.stoneRescuableCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1010U, (object) stoneRescueTime.ToString("F2"));
          this.stoneRescuableCount.color = Color.yellow;
        }
        else
          this.stoneRescuableCount.text = "";
      }
      else
        this.stoneRescuableCount.text = "";
    }
  }

  protected bool IsContiueable()
  {
    return Object.op_Inequality((Object) this.continueCount, (Object) null) && MonoBehaviourSingleton<CoopManager>.IsValid() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest();
  }

  public void OnClickRetire()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    if (FieldManager.IsValidInGameNoBoss())
    {
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      if (Object.op_Inequality((Object) self, (Object) null) && (double) self.rescueTime > 0.0 && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", ((Component) this).gameObject, "RETIRE", (object) StringTable.Get(STRING_CATEGORY.IN_GAME, 1008U));
      else
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", ((Component) this).gameObject, "RETIRE", (object) StringTable.Get(STRING_CATEGORY.IN_GAME, 1009U));
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", ((Component) this).gameObject, "RETIRE");
  }

  public void DoRetire()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
  }

  public void DoRetry()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.BattleRetry();
  }

  public void OnClickContinue()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickContinue", ((Component) this).gameObject, "CONTINUE");
  }

  public void DoContinue()
  {
    if (!this.CheckVisible())
      return;
    this.continueButton.isEnabled = false;
    this.retireButton.isEnabled = false;
    MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol = true;
    if (QuestManager.IsValidInGame())
      MonoBehaviourSingleton<QuestManager>.I.SendQuestContinue(new Action<bool, Error>(this.CallBackContinue));
    else
      MonoBehaviourSingleton<FieldManager>.I.SendFieldContinue(new Action<bool, Error>(this.CallBackContinue));
  }

  private void CallBackContinue(bool res, Error error)
  {
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol = false;
    if (res)
    {
      this.OnContinueSelf();
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      this.continueButton.isEnabled = true;
      this.retireButton.isEnabled = true;
      string text = StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001U, (object) (int) error);
      if (!string.IsNullOrEmpty(text))
        UIInGamePopupDialog.PushOpen(text, true);
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      if (!Object.op_Inequality((Object) self, (Object) null) || self.actionID != (Character.ACTION_ID) 24 || (double) self.continueTime > 0.0 || !MonoBehaviourSingleton<InGameProgress>.IsValid())
        return;
      MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
    }
  }

  public void OnClickRestart()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRestart", ((Component) this).gameObject, "RESTART");
  }

  public void DoRestart()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<QuestManager>.IsValid() || !MonoBehaviourSingleton<FieldManager>.IsValid() || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    int exploreStartMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreStartMapId();
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) exploreStartMapId);
    if (fieldMapData == null || fieldMapData.jumpPortalID == 0U)
    {
      Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
    }
    else
    {
      MonoBehaviourSingleton<StageObjectManager>.I.self.RestartExplore();
      if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
      {
        MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(MonoBehaviourSingleton<StageObjectManager>.I.boss);
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncExploreBoss(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus());
      }
      MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
      MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
    }
  }

  private void OnContinueSelf()
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    int hpMax = self.hpMax;
    self.ActDeadStandup(hpMax, Player.eContinueType.CONTINUE);
    if (!QuestManager.IsValidInGame())
      return;
    float continueHealRate = MonoBehaviourSingleton<InGameSettingsManager>.I.player.continueHealRate;
    if ((double) continueHealRate <= 0.0)
      return;
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    int index = 0;
    for (int count = playerList.Count; index < count; ++index)
    {
      Player player = playerList[index] as Player;
      if (Object.op_Inequality((Object) player, (Object) self))
      {
        Character.HealData healData = new Character.HealData(Mathf.FloorToInt((float) player.hpMax * continueHealRate), HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, (List<int>) null);
        player.OnHealReceive(healData);
      }
    }
  }

  public void SetDisableButtons(bool disable)
  {
    this.isDisableButtons = disable;
    if (Object.op_Inequality((Object) this.retireButton, (Object) null))
      this.retireButton.isEnabled = !disable;
    if (!Object.op_Inequality((Object) this.continueButton, (Object) null))
      return;
    this.continueButton.isEnabled = !disable;
  }

  public void SetContinueButton(bool enable)
  {
    if (enable)
    {
      ((Component) this.continueButton).gameObject.SetActive(true);
      this.retireTweenPos.to = this.orgRetirePos;
    }
    else
    {
      ((Component) this.continueButton).gameObject.SetActive(false);
      Vector3 to = this.retireTweenPos.to;
      to.x = 0.0f;
      this.retireTweenPos.to = to;
    }
  }

  public bool IsEnableButtonAll => !this.isDisableButtons;

  protected enum COLOR_SET
  {
    CONTINUE_TOP,
    CONTINUE_BOTTOM,
    CONTINUE_ENABLE_EFF,
    CONTINUE_DISABLE,
    CONTINUE_DISABLE_EFF,
    CRYSTAL_ENABLE,
    CRYSTAL_ENABLE_EFF,
    CRYSTAL_DISABLE,
    CRYSTAL_DISABLE_EFF,
    ICON_ENABLE,
    ICON_DISABLE,
    MAX,
  }
}

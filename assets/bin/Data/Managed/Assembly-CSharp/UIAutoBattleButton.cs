// Decompiled with JetBrains decompiler
// Type: UIAutoBattleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class UIAutoBattleButton : UIBehaviour
{
  protected Self self;
  private bool isAbleCountCycle;
  private bool cachedAutoFlg;
  private bool updateTimer;
  private bool btnEnable = true;
  private bool needUpdateUI;
  [SerializeField]
  private GameObject sprAutoOn;
  [SerializeField]
  private GameObject sprAutoOff;
  [SerializeField]
  private GameObject sprAutoPlay;
  [SerializeField]
  private GameObject sprAutoPause;
  [SerializeField]
  private UILabel lblAutoTime;
  [SerializeField]
  private BoxCollider btnCollider;
  private AutoModeStatus automodeStatus = new AutoModeStatus();
  private bool canUseAutoMode;

  public double stampCircle { get; private set; }

  private void SetupAutoButton(double timeLeft)
  {
    this.self = MonoBehaviourSingleton<UIPlayerStatus>.I.targetPlayer as Self;
    if (timeLeft < 0.0)
      timeLeft = 0.0;
    this.Initialize(timeLeft, GameSaveData.instance.isAutoMode);
    if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !QuestManager.IsValidInGame() && this.automodeStatus.IsRemain())
      this.canUseAutoMode = true;
    if (Object.op_Equality((Object) this.self, (Object) null))
      this.canUseAutoMode = false;
    ((Component) this).gameObject.SetActive(this.canUseAutoMode);
    if (this.canUseAutoMode)
    {
      if (GameSaveData.instance.isAutoMode)
      {
        if (!this.cachedAutoFlg)
          this.StartAutoMode();
      }
      else if (this.cachedAutoFlg)
        this.StopAutoMode();
      this.UpdateButton();
    }
    else if (GameSaveData.instance.isAutoMode)
    {
      if (!this.cachedAutoFlg)
        return;
      this.PauseAutoMode();
    }
    else
    {
      if (!this.cachedAutoFlg)
        return;
      this.StopAutoMode();
    }
  }

  private void Initialize(double second, bool timerState)
  {
    this.updateTimer = timerState;
    this.automodeStatus.Init(second);
    this.lblAutoTime.text = this.automodeStatus.GetRemainTime();
    this.resetStampCircle();
  }

  private void Update()
  {
    if (!this.cachedAutoFlg)
      this.updateTimer = false;
    if (!this.updateTimer)
      return;
    this.automodeStatus.SubTime((double) Time.deltaTime);
    this.lblAutoTime.text = this.automodeStatus.GetRemainTime();
    if (!this.automodeStatus.IsRemain())
      this.PauseAutoMode();
    if (!this.isAbleCountCycle)
      return;
    this.stampCircle -= (double) Time.deltaTime;
    if (this.stampCircle >= 0.0)
      return;
    this.isAbleCountCycle = false;
    this.AutoPlayTimestamp((Action<bool>) (b =>
    {
      if (!b)
        return;
      this.resetStampCircle();
      this.isAbleCountCycle = true;
    }));
  }

  private void UpdateButton()
  {
    if (this.cachedAutoFlg)
    {
      this.sprAutoOn.SetActive(false);
      this.sprAutoOff.SetActive(true);
      this.sprAutoPlay.SetActive(false);
      this.sprAutoPause.SetActive(true);
    }
    else
    {
      this.sprAutoOn.SetActive(true);
      this.sprAutoOff.SetActive(false);
      this.sprAutoPlay.SetActive(true);
      this.sprAutoPause.SetActive(false);
    }
    if (!this.automodeStatus.IsRemain())
      this.canUseAutoMode = false;
    ((Component) this).gameObject.SetActive(this.canUseAutoMode);
  }

  private bool IsAuto() => this.self.isAutoMode;

  public void OnBtnClick()
  {
    if (this.IsAuto())
    {
      SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
      this.StopAutoMode();
    }
    else if (this.automodeStatus.IsRemain())
    {
      SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
      this.StartAutoMode();
    }
    else
      SoundManager.PlaySystemSE(SoundID.UISE.INVALID);
  }

  private void ForcePauseAutoMode()
  {
    this.self.SwitchAutoBattle(false);
    this.cachedAutoFlg = false;
    this.UpdateButton();
  }

  private void ForceResumeAutoMode()
  {
    this.self.SwitchAutoBattle(true);
    this.cachedAutoFlg = true;
    this.updateTimer = true;
    this.UpdateButton();
  }

  private void PauseAutoMode()
  {
    this.self.SwitchAutoBattle(false);
    this.cachedAutoFlg = false;
    this.AutoPlayStopConn((Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.UpdateButton();
    }));
  }

  private void StopAutoMode()
  {
    this.self.SwitchAutoBattle(false);
    GameSaveData.instance.isAutoMode = false;
    this.cachedAutoFlg = false;
    this.AutoPlayStopConn((Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.UpdateButton();
    }));
  }

  private void StartAutoMode()
  {
    this.resetStampCircle();
    this.AutoPlayStartConn((Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.self.SwitchAutoBattle(true);
      GameSaveData.instance.isAutoMode = true;
      this.cachedAutoFlg = true;
      this.updateTimer = true;
      this.UpdateButton();
    }));
  }

  public void AutoPlaySwitch(int playState, Action<bool> call_back)
  {
    AutoPlaySwitchModel.RequestSendForm postData = new AutoPlaySwitchModel.RequestSendForm();
    postData.type = playState;
    if (!this.btnEnable)
      return;
    if (Object.op_Inequality((Object) this.btnCollider, (Object) null))
      ((Collider) this.btnCollider).enabled = false;
    this.btnEnable = false;
    Protocol.Send<AutoPlaySwitchModel.RequestSendForm, AutoPlaySwitchModel>(AutoPlaySwitchModel.URL, postData, (Action<AutoPlaySwitchModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.btnEnable = true;
        if (Object.op_Inequality((Object) this.btnCollider, (Object) null))
          ((Collider) this.btnCollider).enabled = true;
        this.Initialize(ret.result.timeLeft, playState == 0);
      }
      call_back(flag);
    }));
  }

  public void AutoPlayStartConn(Action<bool> call_back = null)
  {
    this.AutoPlaySwitch(0, (Action<bool>) (b =>
    {
      if (b)
        this.isAbleCountCycle = true;
      if (call_back == null)
        return;
      call_back(b);
    }));
  }

  public void AutoPlayStopConn(Action<bool> call_back = null)
  {
    int playState = 1;
    this.isAbleCountCycle = false;
    this.AutoPlaySwitch(playState, (Action<bool>) (b =>
    {
      if (call_back == null)
        return;
      call_back(b);
    }));
  }

  public void AutoPlayTimestamp(Action<bool> call_back)
  {
    Protocol.Send<AutoPlaySwitchModel.RequestSendForm, AutoPlayTimestampModel>(AutoPlayTimestampModel.URL, new AutoPlaySwitchModel.RequestSendForm()
    {
      type = 0
    }, (Action<AutoPlayTimestampModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (ret.result.timeLeft == 0.0)
        {
          this.self.SwitchAutoBattle(false);
          GameSaveData.instance.isAutoMode = false;
          this.cachedAutoFlg = false;
          this.Initialize(0.0, false);
        }
        else if (this.needUpdateUI)
        {
          this.needUpdateUI = false;
          this.ForceResumeAutoMode();
          this.Initialize(ret.result.timeLeft, true);
        }
      }
      if (!flag)
      {
        this.needUpdateUI = true;
        this.ForcePauseAutoMode();
      }
      call_back(flag);
    }));
  }

  public void GetAutoPlayTime(Action<bool> call_back)
  {
    if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) || QuestManager.IsValidInGame())
    {
      this.SetupAutoButton(0.0);
      call_back(true);
    }
    else
      Protocol.Send<AutoPlayTimeModel>(AutoPlayTimeModel.URL, (WWWForm) null, (Action<AutoPlayTimeModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.SetupAutoButton(ret.result.timeLeft);
        }
        call_back(flag);
      }));
  }

  public void OnUseItem(double timeleft)
  {
    this.Initialize(timeleft, GameSaveData.instance.isAutoMode);
    if (!this.automodeStatus.IsRemain())
      return;
    if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !QuestManager.IsValidInGame())
      this.canUseAutoMode = true;
    if (this.canUseAutoMode)
    {
      if (GameSaveData.instance.isAutoMode)
      {
        if (this.cachedAutoFlg)
          return;
        this.StartAutoMode();
      }
      else
        this.UpdateButton();
    }
    else
      ((Component) this).gameObject.SetActive(false);
  }

  public void EnableButton()
  {
    if (!this.btnEnable)
      this.btnEnable = true;
    if (((Collider) this.btnCollider).enabled)
      return;
    ((Collider) this.btnCollider).enabled = true;
  }

  public void DisableButton()
  {
    if (this.btnEnable)
      this.btnEnable = false;
    if (!((Collider) this.btnCollider).enabled)
      return;
    ((Collider) this.btnCollider).enabled = false;
  }

  private void resetStampCircle() => this.stampCircle = 10.0;

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
    {
      if (this.cachedAutoFlg)
      {
        if (Object.op_Inequality((Object) this.self, (Object) null))
          this.self.SwitchAutoBattle(false);
        this.cachedAutoFlg = false;
        this.AutoPlayForceStop();
      }
    }
    else if (this.cachedAutoFlg)
    {
      if (Object.op_Inequality((Object) this.self, (Object) null))
        this.self.SwitchAutoBattle(false);
      GameSaveData.instance.isAutoMode = false;
      this.cachedAutoFlg = false;
      this.AutoPlayForceStop();
    }
    base.OnDestroy();
  }

  private void OnApplicationQuit()
  {
    if (!this.cachedAutoFlg)
      return;
    if (Object.op_Inequality((Object) this.self, (Object) null))
      this.self.SwitchAutoBattle(false);
    GameSaveData.instance.isAutoMode = false;
    this.cachedAutoFlg = false;
    this.AutoPlayForceStop();
  }

  private void OnApplicationPause(bool pause)
  {
    if (!pause || !this.IsAuto())
      return;
    this.StopAutoMode();
  }

  public void AutoPlayForceStop()
  {
    Protocol.Send<AutoPlaySwitchModel.RequestSendForm, AutoPlaySwitchModel>(AutoPlaySwitchModel.URL, new AutoPlaySwitchModel.RequestSendForm()
    {
      type = 1
    }, (Action<AutoPlaySwitchModel>) (ret => { }));
  }
}

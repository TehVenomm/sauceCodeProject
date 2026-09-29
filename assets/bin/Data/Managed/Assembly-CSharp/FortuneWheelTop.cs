// Decompiled with JetBrains decompiler
// Type: FortuneWheelTop
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
public class FortuneWheelTop : GameSection
{
  private const int MAX_DAY_ACTIVE = 7;
  private const float INTERVAL_UPDATE_TIME = 10f;
  private const int MAX_USER_REWARD_LOG = 30;
  private JackportNumber jackportNumber;
  private SpinTicketNumber spinTicketNumber;
  private FortuneWheelSpinHandle spinHandle;
  private List<FortuneWheelServerLog> serverLogList = new List<FortuneWheelServerLog>();
  private List<FortuneWheelUserLog> userLogList;
  private GameObject m_SpinItemPrefab;
  private FortuneWheelManager.SPIN_TYPE spinType;
  private bool isX10Spin;
  private int spinX10Count;
  private bool isUserSkip;
  private int spinMultiRewardAddedIndex;
  private int spinX10ServerAddedIndex;
  private bool isSpinning;
  private UISprite activeBar;
  private bool updatingView;
  private bool skipUpdate;
  private bool isAddItem;
  private float delay10Spin = 2f;
  private float delayMuliSpin = 0.7f;
  private int countMultipleSpin;
  private bool spinMultiple;
  private float timeUpdate;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.spinType = FortuneWheelManager.SPIN_TYPE.X1;
    this.jackportNumber = ((Component) this.GetCtrl((Enum) FortuneWheelTop.UI.JACKPOT_NUMBER)).GetComponent<JackportNumber>();
    this.spinTicketNumber = ((Component) this.GetCtrl((Enum) FortuneWheelTop.UI.SPIN_TICKET_NUM)).GetComponent<SpinTicketNumber>();
    this.spinHandle = ((Component) this.GetCtrl((Enum) FortuneWheelTop.UI.SPIN_ICON_POINT_GROUP)).GetComponent<FortuneWheelSpinHandle>();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_quest_spinitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "JackpotSpinItem");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.m_SpinItemPrefab = lo_quest_spinitem.loadedObject as GameObject;
    bool wait = true;
    MonoBehaviourSingleton<FortuneWheelManager>.I.SendInfo((Action<bool>) (is_success =>
    {
      wait = false;
      if (!is_success)
        return;
      this.serverLogList = new List<FortuneWheelServerLog>((IEnumerable<FortuneWheelServerLog>) MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.history.server);
      this.userLogList = new List<FortuneWheelUserLog>((IEnumerable<FortuneWheelUserLog>) MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.history.user);
      this.userLogList.Reverse();
    }));
    while (wait)
      yield return (object) null;
    base.Initialize();
    yield return (object) new WaitForEndOfFrame();
    MonoBehaviourSingleton<FortuneWheelManager>.I.OnJackpotWin += new FortuneWheelManager.OnJackpot(this.OnJackpotWinHandler);
    MonoBehaviourSingleton<FortuneWheelManager>.I.OnRequestUpdateUI += new System.Action(this.OnRequestUpdateUIHandler);
    this.spinHandle.IniSpin(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.itemList, this.m_SpinItemPrefab);
    this.SetWheelState(this.IsOpenWheel());
    this.SetButtonState(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
    this.SetSpinState();
    SoundManager.RequestBGM(13);
  }

  private void SetSpinState(bool force = false)
  {
    if (force)
    {
      this.SetActive((Enum) FortuneWheelTop.UI.SPR_FREE_TXT, force);
      this.SetActive((Enum) FortuneWheelTop.UI.SPIN_TICKET_NUM, !force);
    }
    else
    {
      this.SetActive((Enum) FortuneWheelTop.UI.SPR_FREE_TXT, MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
      this.SetActive((Enum) FortuneWheelTop.UI.SPIN_TICKET_NUM, !MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
    }
  }

  private void DebugLog(List<FortuneWheelUserLog> list)
  {
    foreach (FortuneWheelUserLog fortuneWheelUserLog in list)
      Debug.Log((object) ("item " + fortuneWheelUserLog.rewardString));
  }

  private void OnRequestUpdateUIHandler() => this.StartCoroutine(this.RequestRefreshUI());

  private void OnJackpotWinHandler(FortuneWheelManager.JackpotWinData data)
  {
    this.StartCoroutine(this.ShowJackpot(data));
  }

  private IEnumerator ShowJackpot(FortuneWheelManager.JackpotWinData data)
  {
    yield return (object) new WaitForSeconds(2f);
    while (this.isSpinning)
      yield return (object) null;
    this.DispatchEvent("JACKPOT_WIN", (object) data);
  }

  private IEnumerator RequestRefreshUI()
  {
    yield return (object) new WaitForEndOfFrame();
    this.RefreshUI();
  }

  private IEnumerator CloseWinJackpotDialog()
  {
    yield return (object) new WaitForSeconds(6f);
    GameSection.BackSection();
  }

  private void SetButtonState(bool isFree = false)
  {
    if (!isFree)
    {
      this.DisableAllButton();
      switch (this.spinType)
      {
        case FortuneWheelManager.SPIN_TYPE.X1:
          this.EnableSpinTypeButton(FortuneWheelTop.UI.BTN_SPIN_X1_ENABLE, FortuneWheelTop.UI.BTN_SPIN_X1_DISABLE);
          break;
        case FortuneWheelManager.SPIN_TYPE.X10:
          this.EnableSpinTypeButton(FortuneWheelTop.UI.BTN_SPIN_X10_ENABLE, FortuneWheelTop.UI.BTN_SPIN_X10_DISABLE);
          break;
        case FortuneWheelManager.SPIN_TYPE.X50:
          this.EnableSpinTypeButton(FortuneWheelTop.UI.BTN_SPIN_X50_ENABLE, FortuneWheelTop.UI.BTN_SPIN_X50_DISABLE);
          break;
        case FortuneWheelManager.SPIN_TYPE.X100:
          this.EnableSpinTypeButton(FortuneWheelTop.UI.BTN_SPIN_X100_ENABLE, FortuneWheelTop.UI.BTN_SPIN_X100_DISABLE);
          break;
      }
    }
    else
      this.DisableAllButton();
  }

  private void DisableAllButton()
  {
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X1_ENABLE, false);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X1_DISABLE, true);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X10_ENABLE, false);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X10_DISABLE, true);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X50_ENABLE, false);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X50_DISABLE, true);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X100_ENABLE, false);
    this.SetActive((Enum) FortuneWheelTop.UI.BTN_SPIN_X100_DISABLE, true);
  }

  private void EnableSpinTypeButton(FortuneWheelTop.UI btnEnable, FortuneWheelTop.UI btnDisable)
  {
    this.SetActive((Enum) btnEnable, true);
    this.SetActive((Enum) btnDisable, false);
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) FortuneWheelTop.UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
    this.SetLabelText((Enum) FortuneWheelTop.UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
    this.UpdateLog(true);
  }

  private bool IsOpenWheel()
  {
    return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData != null && MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.loyaltyPoint >= MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.loyaltyPointRequired && MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.isOpen;
  }

  private void SetWheelState(bool isOpen)
  {
    this.SetActive((Enum) FortuneWheelTop.UI.SPIN_UNLOCK_GROUP, isOpen);
    this.SetActive((Enum) FortuneWheelTop.UI.SPIN_LOCK_GROUP, !isOpen);
    if (isOpen)
      return;
    this.SetActiveDay(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.loyaltyPoint);
  }

  private void SetActiveDay(int numDayActive)
  {
    for (int index = 0; index < 7; ++index)
    {
      Transform ctrl = this.GetCtrl((Enum) (FortuneWheelTop.UI) Enum.Parse(typeof (FortuneWheelTop.UI), "OBJ_COUNT_DAY_" + (object) (index + 1)));
      if (index < numDayActive)
      {
        this.SetActive(((Component) ctrl).transform, (Enum) FortuneWheelTop.UI.SPR_COUNT_DAY_ACTIVE, true);
        this.SetActive(((Component) ctrl).transform, (Enum) FortuneWheelTop.UI.SPR_COUNT_DAY_INACTIVE, false);
      }
      else
      {
        this.SetActive(((Component) ctrl).transform, (Enum) FortuneWheelTop.UI.SPR_COUNT_DAY_ACTIVE, false);
        this.SetActive(((Component) ctrl).transform, (Enum) FortuneWheelTop.UI.SPR_COUNT_DAY_INACTIVE, true);
      }
    }
    this.SetActiveBarState(numDayActive);
  }

  private void SetActiveBarState(int numDayActive)
  {
    if (Object.op_Equality((Object) this.activeBar, (Object) null))
      this.activeBar = ((Component) this.GetCtrl((Enum) FortuneWheelTop.UI.SPR_BAR_ACTIVE)).GetComponent<UISprite>();
    this.activeBar.fillAmount = 0.166666672f * (float) (numDayActive - 1);
  }

  private IEnumerator IEUpdateView()
  {
    if (this.spinMultiRewardAddedIndex < MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards.Count)
    {
      if (this.isUserSkip)
      {
        if (this.spinMultiRewardAddedIndex < MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards.Count && this.spinType != FortuneWheelManager.SPIN_TYPE.X1)
        {
          for (int rewardAddedIndex = this.spinMultiRewardAddedIndex; rewardAddedIndex < MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards.Count; ++rewardAddedIndex)
          {
            this.userLogList.Add(this.GenerateRewardItem());
            this.CheckWinJackpot(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards[this.spinMultiRewardAddedIndex]);
            ++this.spinMultiRewardAddedIndex;
          }
          if (MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server != null && MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Count > 0)
          {
            this.serverLogList.InsertRange(0, (IEnumerable<FortuneWheelServerLog>) MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server);
            MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Clear();
          }
          this.UpdateLog();
          this.updatingView = false;
        }
      }
      else
      {
        yield return (object) new WaitForSeconds(0.5f);
        yield return (object) new WaitForEndOfFrame();
        if (!this.isUserSkip)
        {
          this.userLogList.Add(this.GenerateRewardItem());
          this.isAddItem = true;
          this.CheckWinJackpot(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards[this.spinMultiRewardAddedIndex]);
          ++this.spinMultiRewardAddedIndex;
          if (MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server != null && MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Count > 0 && (this.spinType != FortuneWheelManager.SPIN_TYPE.X1 && (FortuneWheelManager.SPIN_TYPE) this.countMultipleSpin == this.spinType || this.spinType == FortuneWheelManager.SPIN_TYPE.X1))
          {
            this.serverLogList.InsertRange(0, (IEnumerable<FortuneWheelServerLog>) MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server);
            MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Clear();
          }
        }
        this.UpdateLog();
        this.updatingView = false;
      }
    }
  }

  private FortuneWheelUserLog GenerateRewardItem()
  {
    IEnumerable<FortuneWheelItem> source = MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.vaultInfo.itemList.Where<FortuneWheelItem>((Func<FortuneWheelItem, bool>) (s => s.id == MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards[this.spinMultiRewardAddedIndex].spinItemId));
    return new FortuneWheelUserLog()
    {
      rewardId = source.First<FortuneWheelItem>().rewardId,
      rewardType = source.First<FortuneWheelItem>().rewardType,
      rewardNum = source.First<FortuneWheelItem>().rewardNum
    };
  }

  private void CheckWinJackpot(FortuneWheelReward data)
  {
    if (data.value < 100)
      return;
    string userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
    string name = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
    string jackpot = data.num.ToString();
    int percentage = data.percentage;
    this.skipUpdate = true;
    this.DispatchEvent("JACKPOT_WIN", (object) new FortuneWheelManager.JackpotWinData(userId, jackpot, name, percentage));
  }

  private void UpdateView() => this.StartCoroutine(this.IEUpdateView());

  private void UpdateLog(bool isSkipAnim = false)
  {
    this.ReloadUserRewardLog(isSkipAnim);
    this.ReloadServerLog();
  }

  private void UpdateStat()
  {
    this.jackportNumber.ShowNumber(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.jackpot.ToString());
    this.spinTicketNumber.ShowNumber(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket.ToString());
  }

  private void ReloadUserRewardLog(bool isSkipAnim)
  {
    if (this.userLogList == null || this.userLogList.Count == 0)
      return;
    if (this.isAddItem)
    {
      this.PlayAudio((Enum) FortuneWheelTop.AUDIO.REWARD_ADDED);
      this.isAddItem = false;
    }
    if (this.userLogList.Count > 30)
      this.userLogList.RemoveRange(0, this.userLogList.Count - 30);
    this.SetGrid((Enum) FortuneWheelTop.UI.GRD_REWARD_LOG, "FortuneWheelRewardLogItem", this.userLogList.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      FortuneWheelUserLog userLog = this.userLogList[i];
      FortuneWheelRewardLogItem component = ((Component) t).GetComponent<FortuneWheelRewardLogItem>();
      if (userLog.rewardType == 550)
        component.InitJackpot();
      else
        component.InitLog((REWARD_TYPE) userLog.rewardType, (uint) userLog.rewardId, userLog.rewardNum);
      if (!isSkipAnim && !this.isUserSkip)
      {
        if (i != this.userLogList.Count - 1)
          return;
        t.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        this.PlayTween(t, callback: (EventDelegate.Callback) (() => { }), is_input_block: false);
      }
      else
        t.localScale = Vector3.one;
    }));
    UIScrollView component1 = ((Component) this.GetCtrl((Enum) FortuneWheelTop.UI.SCR_USER_REWARD)).GetComponent<UIScrollView>();
    if (!component1.CurrentFit())
      return;
    component1.SetDragAmount(1f, 0.0f, false);
  }

  private void ReloadServerLog()
  {
    if (this.serverLogList == null || this.serverLogList.Count == 0)
      return;
    this.SetGrid((Enum) FortuneWheelTop.UI.GRD_SERVER_LOG, "FortuneWheelServerLogItem", this.serverLogList.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      FortuneWheelServerLog serverLog = this.serverLogList[i];
      FortuneWheelServerLogItem component = ((Component) t).GetComponent<FortuneWheelServerLogItem>();
      string serverLogString = this.GetServerLogString(serverLog);
      if (serverLog.rewardType == 550)
        component.InitJackpot(serverLogString);
      else
        component.InitLog(serverLogString, (REWARD_TYPE) serverLog.rewardType, (uint) serverLog.rewardId);
    }));
  }

  private string GetServerLogString(FortuneWheelServerLog data)
  {
    DateTime localTime = DateTime.Parse(data.createdDate).ToLocalTime();
    string str = $"{localTime.Hour}:{localTime.Minute}:{localTime.Second}";
    return string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 2U), (object) str, (object) data.userName, (object) data.rewardString);
  }

  private void OnQuery_SPIN()
  {
    if (!this.isSpinning && this.countMultipleSpin == 0)
    {
      this.isUserSkip = false;
      if (this.IsSpinAvailable(this.spinType))
        this.HandleSpin();
      else
        GameSection.ChangeEvent("CONFIRM_BUY_JACKPOT", (object) this.sectionData.GetText("STR_CONFIRM_BUY"));
    }
    else
    {
      if (this.spinType == FortuneWheelManager.SPIN_TYPE.X1)
        return;
      GameSection.ChangeEvent("SKIP_SPIN_X10", (object) "Skip?");
    }
  }

  private void OnQuery_SPIN_X1() => this.SetSpinType(FortuneWheelManager.SPIN_TYPE.X1);

  private void OnQuery_SPIN_X10() => this.SetSpinType(FortuneWheelManager.SPIN_TYPE.X10);

  private void OnQuery_SPIN_X50() => this.SetSpinType(FortuneWheelManager.SPIN_TYPE.X50);

  private void OnQuery_SPIN_X100() => this.SetSpinType(FortuneWheelManager.SPIN_TYPE.X100);

  private void SetSpinType(FortuneWheelManager.SPIN_TYPE spinType)
  {
    if (MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin)
      return;
    if (!this.isSpinning && !this.spinMultiple)
    {
      Debug.Log((object) ("Change Spintype = " + (object) spinType));
      this.spinType = spinType;
      this.SetButtonState();
    }
    else
    {
      if (this.spinType == FortuneWheelManager.SPIN_TYPE.X1)
        return;
      GameSection.ChangeEvent("SKIP_SPIN_X10", (object) "Skip?");
    }
  }

  private void OnQuery_SkipX10Dialog_YES()
  {
    if (this.spinType == FortuneWheelManager.SPIN_TYPE.X1)
      return;
    this.spinHandle.Skip();
    this.StopCoroutine(this.IESpinMultiIMP(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData));
    this.isUserSkip = true;
    this.isSpinning = false;
    this.UpdateView();
  }

  private void OnQuery_DETAIL_JACKPOT()
  {
    GameSection.SetEventData((object) WebViewManager.FortuneWheel);
  }

  private IEnumerator IEBack()
  {
    yield return (object) new WaitForSeconds(2f);
    GameSection.ChangeEvent("OK");
  }

  private bool IsX10Available()
  {
    return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket >= 10;
  }

  private bool IsSpinAvailable(FortuneWheelManager.SPIN_TYPE type)
  {
    return (FortuneWheelManager.SPIN_TYPE) MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket >= type;
  }

  private bool IsX1Available()
  {
    return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket >= 1;
  }

  private void IncrestCount() => ++this.spinX10Count;

  private void Spin(FortuneWheelManager.SPIN_TYPE spinType, System.Action endSpinAct = null)
  {
    if (this.isSpinning)
      return;
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<FortuneWheelManager>.I.SendSpin(spinType, (Action<bool>) (success =>
    {
      if (!success)
        return;
      this.spinHandle.StartSpin(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData, spinType, (Action<bool>) (b =>
      {
        this.isSpinning = b;
        if (!this.isSpinning)
        {
          if (endSpinAct != null)
            endSpinAct();
          this.UpdateView();
        }
        this.PlayAudio((Enum) FortuneWheelTop.AUDIO.GEAR_STOP);
      }));
      this.UpdateAfterSpin();
      this.PlayAudio((Enum) FortuneWheelTop.AUDIO.GEAR_SPIN);
    }))));
  }

  private void UpdateAfterSpin()
  {
    MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket = MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.vaultInfo.curTicket;
    this.UpdateStat();
    this.SetButtonState(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
    this.SetSpinState();
  }

  private void HandleSpin()
  {
    if (this.spinType == FortuneWheelManager.SPIN_TYPE.X1)
    {
      this.spinMultiRewardAddedIndex = 0;
      this.Spin(FortuneWheelManager.SPIN_TYPE.X1);
    }
    else if (this.IsSpinAvailable(this.spinType))
    {
      this.countMultipleSpin = 0;
      this.spinMultiRewardAddedIndex = 0;
      this.HandleSpinMulti();
    }
    else
    {
      GameSection.ChangeEvent("CAN_NOT_SPIN_X10", (object) this.sectionData.GetText("STR_SPIN_X10_ERROR_MESSAGE"));
      this.isX10Spin = false;
      this.spinType = FortuneWheelManager.SPIN_TYPE.X1;
      this.SetButtonState();
    }
  }

  private void HandleSpinMulti()
  {
    if (this.isSpinning)
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<FortuneWheelManager>.I.SendSpin(this.spinType, (Action<bool>) (success =>
    {
      if (success)
        this.StartCoroutine(this.IESpinMultiIMP(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData));
      this.UpdateAfterSpin();
      GameSection.ResumeEvent(true);
    }));
  }

  private IEnumerator IESpinMultiIMP(FortuneWheelData data)
  {
    this.spinMultiple = true;
    while ((FortuneWheelManager.SPIN_TYPE) this.countMultipleSpin < this.spinType && !this.isUserSkip)
    {
      while (this.isSpinning || this.updatingView)
        yield return (object) null;
      if ((FortuneWheelManager.SPIN_TYPE) this.countMultipleSpin < this.spinType && !this.isUserSkip)
      {
        this.SpinMulti(data, this.countMultipleSpin, new Action<bool>(this.SpinMultiEndAct));
        yield return (object) new WaitForSeconds(this.spinType == FortuneWheelManager.SPIN_TYPE.X10 ? this.delay10Spin : this.delayMuliSpin);
      }
      else
        break;
    }
    this.countMultipleSpin = 0;
    this.spinMultiple = false;
  }

  private void SpinMultiEndAct(bool b)
  {
    this.isSpinning = b;
    if (!this.isSpinning && !this.isUserSkip)
    {
      ++this.countMultipleSpin;
    }
    else
    {
      if (!this.isUserSkip)
        return;
      this.countMultipleSpin = 0;
    }
  }

  private void SpinMulti(FortuneWheelData data, int rewardIndex, Action<bool> endSpinAct)
  {
    this.updatingView = true;
    this.spinHandle.StartSpin(data, this.spinType, (Action<bool>) (b =>
    {
      this.isSpinning = b;
      if (!this.isSpinning)
      {
        if (endSpinAct != null)
          endSpinAct(b);
        this.UpdateView();
      }
      this.PlayAudio((Enum) FortuneWheelTop.AUDIO.GEAR_STOP);
    }), rewardIndex);
    this.PlayAudio((Enum) FortuneWheelTop.AUDIO.GEAR_SPIN);
  }

  private void Update()
  {
    this.timeUpdate += Time.deltaTime;
    if ((double) this.timeUpdate < 10.0)
      return;
    this.timeUpdate = 0.0f;
    if (!this.isSpinning && this.countMultipleSpin == 0 && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nameof (FortuneWheelTop) && !this.skipUpdate)
      Protocol.Force((System.Action) (() => MonoBehaviourSingleton<FortuneWheelManager>.I.UpdateData((Action<bool>) (success =>
      {
        if (!success)
          return;
        this.UpdateJackpot();
      }))));
    this.skipUpdate = false;
  }

  private void UpdateJackpot() => this.StartCoroutine(this.IEUpdateJackpot());

  private IEnumerator IEUpdateJackpot()
  {
    yield return (object) new WaitForEndOfFrame();
    this.jackportNumber.ShowNumber(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.jackpot.ToString());
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.CHANGED_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.SetLabelText((Enum) FortuneWheelTop.UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
    this.SetLabelText((Enum) FortuneWheelTop.UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
    this.UpdateStat();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<FortuneWheelManager>.I, (Object) null))
    {
      MonoBehaviourSingleton<FortuneWheelManager>.I.OnJackpotWin -= new FortuneWheelManager.OnJackpot(this.OnJackpotWinHandler);
      MonoBehaviourSingleton<FortuneWheelManager>.I.OnRequestUpdateUI -= new System.Action(this.OnRequestUpdateUIHandler);
    }
    SoundManager.RequestBGM(2);
  }

  private enum UI
  {
    LBL_GOLD_NUM,
    LBL_CRYSTAL_NUM,
    SCR_USER_REWARD,
    SCR_SERVER_REWARD,
    GRD_SERVER_LOG,
    GRD_REWARD_LOG,
    ITEM_ICON,
    JACKPOT_NUMBER,
    SPIN_TICKET_NUM,
    SPIN_ICON_POINT_GROUP,
    BTN_SPIN_X1_ENABLE,
    BTN_SPIN_X1_DISABLE,
    BTN_SPIN_X10_ENABLE,
    BTN_SPIN_X10_DISABLE,
    BTN_SPIN_X50_ENABLE,
    BTN_SPIN_X50_DISABLE,
    BTN_SPIN_X100_ENABLE,
    BTN_SPIN_X100_DISABLE,
    OBJ_COUNT_GROUP,
    OBJ_COUNTS,
    OBJ_COUNT_DAY_1,
    OBJ_COUNT_DAY_2,
    OBJ_COUNT_DAY_3,
    OBJ_COUNT_DAY_4,
    OBJ_COUNT_DAY_5,
    OBJ_COUNT_DAY_6,
    OBJ_COUNT_DAY_7,
    SPR_COUNT_DAY_ACTIVE,
    SPR_COUNT_DAY_INACTIVE,
    SPIN_UNLOCK_GROUP,
    SPIN_LOCK_GROUP,
    SPR_BAR_ACTIVE,
    SPR_FREE_TXT,
  }

  private enum AUDIO
  {
    REWARD_ADDED = 10000063, // 0x009896BF
    GEAR_SPIN = 10000079, // 0x009896CF
    GEAR_STOP = 40000133, // 0x02625A85
  }
}

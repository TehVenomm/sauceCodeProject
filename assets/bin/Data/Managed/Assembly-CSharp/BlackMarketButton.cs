// Decompiled with JetBrains decompiler
// Type: BlackMarketButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class BlackMarketButton : UIBehaviour
{
  private const float UPDATE_INTARVAL = 0.25f;
  private float timer;
  private bool isDarkMarketOpen;
  private UILabel timeLbl;

  protected override void OnOpen()
  {
    if (Object.op_Equality((Object) this.timeLbl, (Object) null))
      this.timeLbl = ((Component) this.GetCtrl((Enum) BlackMarketButton.UI.TIME_COUNTDOWN_TXT)).GetComponent<UILabel>();
    this.PlayTween((Enum) BlackMarketButton.UI.OBJ_TWEEN, is_input_block: false);
    this.OnInvitationBtnOpen(MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite || MonoBehaviourSingleton<UserInfoManager>.I.ExistsRallyInvite);
    if (!string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
    {
      if ((int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds > 0)
        this.UpdateDrakMarketState(true);
      else
        this.UpdateDrakMarketState(false);
    }
    else
      this.UpdateDrakMarketState(false);
    base.OnOpen();
  }

  public void OnInvitationBtnOpen(bool isOpen)
  {
    this.PlayTween((Enum) BlackMarketButton.UI.OBJ_TWEEN, isOpen, tween_ctrl_id: 1);
  }

  public void InitTime(int time)
  {
    if (Object.op_Equality((Object) this.timeLbl, (Object) null))
      this.timeLbl = ((Component) this.GetCtrl((Enum) BlackMarketButton.UI.TIME_COUNTDOWN_TXT)).GetComponent<UILabel>();
    this.SetActive((Enum) BlackMarketButton.UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
    this.UpdateDrakMarketState(true);
  }

  public void ResetMarketTime()
  {
    if (Object.op_Equality((Object) this.timeLbl, (Object) null))
      this.timeLbl = ((Component) this.GetCtrl((Enum) BlackMarketButton.UI.TIME_COUNTDOWN_TXT)).GetComponent<UILabel>();
    if ((int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds > 0)
    {
      GameSaveData.instance.canShowNoteDarkMarket = true;
      this.SetActive((Enum) BlackMarketButton.UI.SPR_NOTE_UPDATE, true);
      MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37U), "");
      this.UpdateDrakMarketState(true);
    }
    else
      this.UpdateDrakMarketState(false);
  }

  public void UpdateDrakMarketState(bool isOpen)
  {
    this.isDarkMarketOpen = isOpen;
    if (!isOpen)
    {
      this.SetActive((Enum) BlackMarketButton.UI.BTN_CLOSE, true);
      this.SetActive((Enum) BlackMarketButton.UI.BTN_OPEN, false);
      this.timeLbl.text = "Preparing...";
    }
    else
    {
      this.SetActive((Enum) BlackMarketButton.UI.BTN_CLOSE, false);
      this.SetActive((Enum) BlackMarketButton.UI.BTN_OPEN, true);
    }
  }

  private void Update()
  {
    if (!this.isDarkMarketOpen)
      return;
    this.UpdateTimers();
  }

  private void UpdateTimers()
  {
    if ((double) this.timer < 0.25)
      this.timer += Time.deltaTime;
    if ((double) this.timer < 0.25)
      return;
    this.timer = 0.0f;
    if (!string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
    {
      int totalSeconds = (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
      this.timeLbl.text = UIUtility.TimeFormat(totalSeconds, true);
      if (totalSeconds > 0)
        return;
      this.UpdateDrakMarketState(false);
    }
    else
      this.UpdateDrakMarketState(false);
  }

  public void UpdateNoteMarket()
  {
    this.SetActive((Enum) BlackMarketButton.UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
  }

  private enum UI
  {
    OBJ_TWEEN,
    TIME_COUNTDOWN_TXT,
    SPR_NOTE_UPDATE,
    BTN_OPEN,
    BTN_CLOSE,
  }
}

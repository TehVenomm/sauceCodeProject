// Decompiled with JetBrains decompiler
// Type: GuildDonateSendDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GuildDonateSendDialog : GameSection
{
  private DonateInfo _info;
  private int m_maxNum;
  private int m_nowSelect;
  private bool canUpdateUI = true;

  public override void Initialize()
  {
    this._info = GameSection.GetEventData() as DonateInfo;
    if (this._info != null)
    {
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.SPR_SELECT_FRAME, true);
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.SPR_REACH_LIMIT, false);
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.LBL_NUMBER_REQUEST, false);
      this.canUpdateUI = true;
      int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) this._info.itemId), 1);
      int num = this._info.quantity - this._info.itemNum;
      this.m_maxNum = itemNum >= num ? num : itemNum;
      this.m_nowSelect = this.m_maxNum;
    }
    else if (MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap < MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap)
    {
      this.m_maxNum = MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap - MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap;
      this.m_nowSelect = 1;
      this.canUpdateUI = true;
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.SPR_SELECT_FRAME, true);
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.SPR_REACH_LIMIT, false);
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.LBL_NUMBER_REQUEST, true);
      this.SetSupportEncoding((Enum) GuildDonateSendDialog.UI.LBL_NUMBER_REQUEST, true);
      this.SetLabelText((Enum) GuildDonateSendDialog.UI.LBL_NUMBER_REQUEST, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 32U /*0x20*/), (object) MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap, (object) MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
    }
    else
    {
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.SPR_SELECT_FRAME, false);
      this.SetActive(this._transform, (Enum) GuildDonateSendDialog.UI.SPR_REACH_LIMIT, true);
      this.SetSupportEncoding((Enum) GuildDonateSendDialog.UI.LBL_REQUEST_LIMIT, true);
      this.SetLabelText((Enum) GuildDonateSendDialog.UI.LBL_REQUEST_LIMIT, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 33U), (object) MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap, (object) MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
      this.canUpdateUI = false;
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (!this.canUpdateUI)
      return;
    string key = "TEXT_SELECT";
    this.SetLabelText((Enum) GuildDonateSendDialog.UI.LBL_CAPTION, this.sectionData.GetText(key));
    this.SetLabelText((Enum) GuildDonateSendDialog.UI.STR_TITLE_U, this.sectionData.GetText(key));
    this.SetLabelText((Enum) GuildDonateSendDialog.UI.STR_TITLE_D, this.sectionData.GetText(key));
    this.SetLabelText((Enum) GuildDonateSendDialog.UI.STR_SELECT_NUM, this.sectionData.GetText("TEXT_SELECT_NUM"));
    this.SetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM, this.m_nowSelect, 0, this.m_maxNum, new EventDelegate.Callback(this.OnChagenSlider));
  }

  private void OnChagenSlider()
  {
    this.SetLabelText((Enum) GuildDonateSendDialog.UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", (object) this.GetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM)));
  }

  private void OnQuery_SELECT_NUM_MINUS()
  {
    this.SetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM, this.GetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM) - 1);
  }

  private void OnQuery_SELECT_NUM_PLUS()
  {
    this.SetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM, this.GetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM) + 1);
  }

  protected int GetSliderNum()
  {
    return this.GetProgressInt((Enum) GuildDonateSendDialog.UI.SLD_SELECT_NUM);
  }

  private void OnQuery_SELECT()
  {
    if (this._info != null)
    {
      int num = this.GetSliderNum();
      if (num > 0)
      {
        GameSection.StayEvent();
        MonoBehaviourSingleton<GuildManager>.I.SendDonateSend(this._info.id, num, (Action<bool>) (success =>
        {
          if (MonoBehaviourSingleton<GuildManager>.I.donateInviteList != null)
          {
            int count = MonoBehaviourSingleton<GuildManager>.I.donateInviteList.Count;
            for (int index = 0; index < count; ++index)
            {
              if (MonoBehaviourSingleton<GuildManager>.I.donateInviteList[index].id == this._info.id)
              {
                MonoBehaviourSingleton<GuildManager>.I.donateInviteList[index].itemNum += num;
                if (MonoBehaviourSingleton<GuildManager>.I.donateInviteList[index].itemNum >= MonoBehaviourSingleton<GuildManager>.I.donateInviteList[index].quantity)
                {
                  MonoBehaviourSingleton<GuildManager>.I.donateInviteList.RemoveAt(index);
                  break;
                }
                break;
              }
            }
          }
          MonoBehaviourSingleton<GuildManager>.I.SendDonateList((Action<bool>) (donate_success =>
          {
            GameSection.ResumeEvent(donate_success);
            GameSection.BackSection();
          }));
        }));
      }
      else
        GameSection.BackSection();
    }
    else
    {
      GameSection.SetEventData((object) this.GetSliderNum().ToString());
      GameSection.BackSection();
    }
  }

  private void OnQuery_CLOSE()
  {
    GameSection.SetEventData((object) "0");
    GameSection.BackSection();
  }

  protected enum UI
  {
    LBL_SELECT_NUM,
    LBL_SELECT_PRICE,
    BTN_SELECT_NUM_MINUS,
    BTN_SELECT_NUM_PLUS,
    SLD_SELECT_NUM,
    SPR_SELECT_FRAME,
    LBL_NUMBER_REQUEST,
    SPR_REACH_LIMIT,
    LBL_REQUEST_LIMIT,
    STR_TITLE_U,
    STR_TITLE_D,
    STR_SELECT_NUM,
    LBL_CAPTION,
  }
}

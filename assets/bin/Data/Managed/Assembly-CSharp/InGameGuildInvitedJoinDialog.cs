// Decompiled with JetBrains decompiler
// Type: InGameGuildInvitedJoinDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;

#nullable disable
public class InGameGuildInvitedJoinDialog : GameSection
{
  private GuildStatisticInfo _info;
  private int _clanId;
  private GuildInvitedModel.GuildInvitedInfo guildInviteInfo;

  public override void Initialize()
  {
    this.guildInviteInfo = GameSection.GetEventData() as GuildInvitedModel.GuildInvitedInfo;
    this._clanId = this.guildInviteInfo.clanId;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool finish_get_statistic = false;
    MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(this._clanId, (Action<bool, GuildStatisticInfo>) ((success, info) =>
    {
      finish_get_statistic = true;
      this._info = info;
    }));
    while (!finish_get_statistic)
      yield return (object) null;
    if (this._info != null)
      this.SetEvent(this.GetCtrl((Enum) InGameGuildInvitedJoinDialog.UI.BTN_ACCEPT_INVITE), "JOIN", (object) null);
    base.Initialize();
  }

  public override void StartSection()
  {
    if (this._info != null)
      return;
    MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Remove(this.guildInviteInfo);
    GameSection.BackSection();
  }

  public override void UpdateUI()
  {
    if (this._info == null)
      return;
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_GUILD_NAME, this._info.clanName);
    if (this._info.emblem != null && this._info.emblem.Length >= 3)
    {
      this.SetSprite((Enum) InGameGuildInvitedJoinDialog.UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(this._info.emblem[0]));
      this.SetSprite((Enum) InGameGuildInvitedJoinDialog.UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(this._info.emblem[1]));
      this.SetSprite((Enum) InGameGuildInvitedJoinDialog.UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(this._info.emblem[2]));
    }
    else
    {
      this.SetSprite((Enum) InGameGuildInvitedJoinDialog.UI.SPR_EMBLEM_LAYER_1, "");
      this.SetSprite((Enum) InGameGuildInvitedJoinDialog.UI.SPR_EMBLEM_LAYER_2, "");
      this.SetSprite((Enum) InGameGuildInvitedJoinDialog.UI.SPR_EMBLEM_LAYER_3, "");
    }
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_LEVEL, string.Format(this.sectionData.GetText("TEXT_LEVEL"), (object) this._info.level));
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_MEM, $"{this._info.currentMem}/{this._info.memCap}");
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_DESC, this._info.description);
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_TAG, this._info.tag);
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_DAYS, (object) (DateTime.UtcNow - DateTime.Parse(this._info.createAt)).Days);
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_DONATE, (object) this._info.donate);
    this.SetLabelText((Enum) InGameGuildInvitedJoinDialog.UI.LBL_HUNTER_NUM, $"{this._info.currentMem}/{this._info.memCap}");
  }

  private void OnQuery_JOIN()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(this._clanId, -1, (Action<bool, Error>) ((isSuccess, error) => this.DoWaitProtocolBusyFinish((System.Action) (() =>
    {
      if (!GuildManager.IsValidInGuild())
        GameSection.ChangeStayEvent("REQUEST");
      GameSection.ResumeEvent(isSuccess);
      if (!GuildManager.IsValidInGuild())
        return;
      MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Clear();
      MonoBehaviourSingleton<UserInfoManager>.I.ClearPartyInvite();
      MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
      MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
      MonoBehaviourSingleton<UserInfoManager>.I.showJoinClanInGame = true;
      this.BackToHome();
    }))));
  }

  private void OnQuery_REJECT()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendRejectInviteClan(this.guildInviteInfo.requestId, (Action<bool>) (isSuccess => this.DoWaitProtocolBusyFinish((System.Action) (() =>
    {
      GameSection.ResumeEvent(isSuccess);
      MonoBehaviourSingleton<GuildManager>.I.guildInviteList.Remove(this.guildInviteInfo);
    }))));
  }

  private void OnQuery_DETAIL()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this._info,
      (object) this._clanId
    });
  }

  private void OnQuery_MORE() => GameSection.SetEventData((object) this._info.description);

  private void BackToHome()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    if (QuestManager.IsValidInGame())
      MonoBehaviourSingleton<InGameProgress>.I.InviteInQuest();
    else
      MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
  }

  private enum UI
  {
    LBL_GUILD_NAME,
    SPR_EMBLEM_LAYER_1,
    SPR_EMBLEM_LAYER_2,
    SPR_EMBLEM_LAYER_3,
    LBL_TAG,
    LBL_LEVEL,
    LBL_MEM,
    LBL_DESC,
    LBL_DAYS,
    LBL_DONATE,
    LBL_GUILD_ID,
    BTN_JOIN,
    BTN_REQUEST,
    BTN_DETAIL,
    LBL_HUNTER_NUM,
    BTN_REJECT_INVITE,
    BTN_ACCEPT_INVITE,
  }
}

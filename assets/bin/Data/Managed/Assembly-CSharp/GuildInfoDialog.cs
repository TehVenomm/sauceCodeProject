// Decompiled with JetBrains decompiler
// Type: GuildInfoDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;

#nullable disable
public class GuildInfoDialog : GameSection
{
  private GuildStatisticInfo _info;
  private int _clanId;

  public override void Initialize()
  {
    this._clanId = (int) GameSection.GetEventData();
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
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_GUILD_NAME, this._info.clanName);
    if (this._info.emblem != null && this._info.emblem.Length >= 3)
    {
      this.SetSprite((Enum) GuildInfoDialog.UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(this._info.emblem[0]));
      this.SetSprite((Enum) GuildInfoDialog.UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(this._info.emblem[1]));
      this.SetSprite((Enum) GuildInfoDialog.UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(this._info.emblem[2]));
    }
    else
    {
      this.SetSprite((Enum) GuildInfoDialog.UI.SPR_EMBLEM_LAYER_1, "");
      this.SetSprite((Enum) GuildInfoDialog.UI.SPR_EMBLEM_LAYER_2, "");
      this.SetSprite((Enum) GuildInfoDialog.UI.SPR_EMBLEM_LAYER_3, "");
    }
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_TAG, this._info.tag);
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_LEVEL, string.Format(this.sectionData.GetText("TEXT_LEVEL"), (object) this._info.level));
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_MEM, $"{this._info.currentMem}/{this._info.memCap}");
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_DESC, this._info.description);
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_DAYS, (object) (DateTime.UtcNow - DateTime.Parse(this._info.createAt)).Days);
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_DONATE, (object) this._info.donate);
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_GUILD_ID, $"{this._clanId:D5}");
    this.SetLabelText((Enum) GuildInfoDialog.UI.LBL_HUNTER_NUM, $"{this._info.currentMem}/{this._info.memCap}");
    this.SetActive((Enum) GuildInfoDialog.UI.BTN_JOIN, this._info.privacy == 0);
    this.SetActive((Enum) GuildInfoDialog.UI.BTN_REQUEST, this._info.privacy == 1);
    this.SetButtonEnabled((Enum) GuildInfoDialog.UI.BTN_JOIN, this._info.canJoin, true);
    this.SetButtonEnabled((Enum) GuildInfoDialog.UI.BTN_REQUEST, this._info.canJoin, true);
  }

  private void OnQuery_JOIN()
  {
    GameSection.SetEventData((object) $"Welcome to {this._info.clanName}!");
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(this._clanId, -1, (Action<bool, Error>) ((isSuccess, error) => this.DoWaitProtocolBusyFinish((System.Action) (() =>
    {
      if (!GuildManager.IsValidInGuild())
        GameSection.ChangeStayEvent("REQUEST");
      else
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
      GameSection.ResumeEvent(isSuccess);
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

  private void OnQuery_REQUEST()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(this._clanId, -1, (Action<bool, Error>) ((isSuccess, error) => this.DoWaitProtocolBusyFinish((System.Action) (() =>
    {
      if (isSuccess)
        this.SetButtonEnabled((Enum) GuildInfoDialog.UI.BTN_REQUEST, false, true);
      if (GuildManager.IsValidInGuild())
        GameSection.ChangeStayEvent("JOIN");
      GameSection.ResumeEvent(true);
    }))));
  }

  private void OnQuery_MORE() => GameSection.SetEventData((object) this._info.description);

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
    LBL_LOCATION,
    LBL_DAYS,
    LBL_DONATE,
    LBL_GUILD_ID,
    BTN_JOIN,
    BTN_REQUEST,
    BTN_DETAIL,
    LBL_HUNTER_NUM,
  }
}

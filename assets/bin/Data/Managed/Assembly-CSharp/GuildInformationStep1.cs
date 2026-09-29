// Decompiled with JetBrains decompiler
// Type: GuildInformationStep1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class GuildInformationStep1 : GameSection
{
  private GuildManager.CreateGuildRequestParam mCreateRequest;

  public override void Initialize()
  {
    base.Initialize();
    if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
      MonoBehaviourSingleton<GuildManager>.I.CreateAddedGuildRequestParam(MonoBehaviourSingleton<GuildManager>.I.guildStatData);
    else
      MonoBehaviourSingleton<GuildManager>.I.ClearCreateGuildRequestParam();
    this.mCreateRequest = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam();
    this.UpdateUIInfo();
  }

  private IEnumerator GetClanStatistic(int clanID)
  {
    bool finish_get_statistic = false;
    GuildStatisticInfo _info = (GuildStatisticInfo) null;
    MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, (Action<bool, GuildStatisticInfo>) ((success, info) =>
    {
      finish_get_statistic = true;
      if (!success)
        return;
      _info = info;
    }));
    while (!finish_get_statistic)
      yield return (object) null;
    MonoBehaviourSingleton<GuildManager>.I.CreateAddedGuildRequestParam(_info);
    this.mCreateRequest = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam();
    this.UpdateUIInfo();
  }

  private void UpdateUIInfo()
  {
    if (string.IsNullOrEmpty(this.mCreateRequest.GuildName))
      this.mCreateRequest.SetGuildName("");
    this.SetInput((Enum) GuildInformationStep1.UI.IPT_NAME, this.mCreateRequest.GuildName, 30, new EventDelegate.Callback(this.OnChangeGuildName));
    if (string.IsNullOrEmpty(this.mCreateRequest.GuildTag))
      this.mCreateRequest.SetGuildTag("");
    this.SetInput((Enum) GuildInformationStep1.UI.IPT_TAG, this.mCreateRequest.GuildTag, 4, new EventDelegate.Callback(this.OnChangeGuildTag));
    if (string.IsNullOrEmpty(this.mCreateRequest.GuildDescribe))
      this.mCreateRequest.SetGuildDescribe("");
    this.SetInput((Enum) GuildInformationStep1.UI.IPT_DESCRIBE, this.mCreateRequest.GuildDescribe, 256 /*0x0100*/, new EventDelegate.Callback(this.OnChangeGuildDescribe));
    this.UpdateEmblems();
    this.SetTouchAndRelease((Enum) GuildInformationStep1.UI.BTN_INFO, "TAG_INFO_SHOW", "TAG_INFO_HIDE");
    this.SetActive((Enum) GuildInformationStep1.UI.SPR_TAG, false);
    bool is_visible = MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1;
    this.SetActive((Enum) GuildInformationStep1.UI.BTN_NEXT, !is_visible);
    this.SetActive((Enum) GuildInformationStep1.UI.BTN_NEXT_UPDATE, is_visible);
  }

  protected virtual void OnChangeGuildName()
  {
    if (!((Component) this.FindCtrl(this._transform, (Enum) GuildInformationStep1.UI.LBL_INPUT)).gameObject.activeInHierarchy)
      this.SetActive((Enum) GuildInformationStep1.UI.LBL_INPUT, true);
    string inputValue = this.GetInputValue((Enum) GuildInformationStep1.UI.IPT_NAME);
    if (string.IsNullOrEmpty(inputValue))
    {
      this.SetActive((Enum) GuildInformationStep1.UI.LBL_DEFAULT_INPUT_NAME, true);
      if (((Component) this.FindCtrl(this._transform, (Enum) GuildInformationStep1.UI.IPT_NAME)).gameObject.activeInHierarchy)
        this.SetActive((Enum) GuildInformationStep1.UI.LBL_INPUT, false);
    }
    else
      this.SetActive((Enum) GuildInformationStep1.UI.LBL_DEFAULT_INPUT_NAME, false);
    this.mCreateRequest.SetGuildName(inputValue);
  }

  protected virtual void OnChangeGuildTag()
  {
    string upper = this.GetInputValue((Enum) GuildInformationStep1.UI.IPT_TAG).ToUpper();
    this.SetLabelText((Enum) GuildInformationStep1.UI.LBL_INPUT_TAG, upper);
    this.mCreateRequest.SetGuildTag(upper);
  }

  protected virtual void OnChangeGuildDescribe()
  {
    string inputValue = this.GetInputValue((Enum) GuildInformationStep1.UI.IPT_DESCRIBE);
    if (string.IsNullOrEmpty(inputValue))
      this.SetActive((Enum) GuildInformationStep1.UI.LBL_DEFAULT_INPUT_DESCRIBE, true);
    else
      this.SetActive((Enum) GuildInformationStep1.UI.LBL_DEFAULT_INPUT_DESCRIBE, false);
    this.mCreateRequest.SetGuildDescribe(inputValue.Replace("\n", "\\n"));
  }

  private void UpdateEmblems()
  {
    if (this.mCreateRequest.EmblemLayerIDs[0] == -1)
      this.mCreateRequest.SetEmblemID(0, GuildManager.sDefaultEmblemIDLayer1);
    this.SetSprite((Enum) GuildInformationStep1.UI.SPR_GUILD_EMBLEM_1, GuildItemManager.I.GetItemSprite(this.mCreateRequest.EmblemLayerIDs[0]));
    if (this.mCreateRequest.EmblemLayerIDs[1] == -1)
      this.mCreateRequest.SetEmblemID(1, GuildManager.sDefaultEmblemIDLayer2);
    this.SetSprite((Enum) GuildInformationStep1.UI.SPR_GUILD_EMBLEM_2, GuildItemManager.I.GetItemSprite(this.mCreateRequest.EmblemLayerIDs[1]));
    if (this.mCreateRequest.EmblemLayerIDs[2] == -1)
      this.mCreateRequest.SetEmblemID(2, GuildManager.sDefaultEmblemIDLayer3);
    this.SetSprite((Enum) GuildInformationStep1.UI.SPR_GUILD_EMBLEM_3, GuildItemManager.I.GetItemSprite(this.mCreateRequest.EmblemLayerIDs[2]));
  }

  private void OnQuery_TAG_INFO_SHOW()
  {
    this.SetActive((Enum) GuildInformationStep1.UI.SPR_TAG, true);
  }

  private void OnQuery_TAG_INFO_HIDE()
  {
    this.SetActive((Enum) GuildInformationStep1.UI.SPR_TAG, false);
  }

  private void OnQuery_RANDOM_EMBLEM()
  {
    int[] numArray = GuildItemManager.I.RandomEmblem(true);
    this.mCreateRequest.SetEmblemID(0, numArray[0]);
    this.mCreateRequest.SetEmblemID(1, numArray[1]);
    this.mCreateRequest.SetEmblemID(2, numArray[2]);
    this.UpdateEmblems();
  }

  private void OnQuery_CLOSE()
  {
    if ((MonoBehaviourSingleton<GuildManager>.I.guildData == null ? 0 : (MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1 ? 1 : 0)) == 0)
      return;
    GameSection.ChangeEvent("CLOSE_UPDATE");
  }

  private void OnQuery_STEP_1()
  {
    MonoBehaviourSingleton<GuildManager>.I.SetGuildChangeData(this.mCreateRequest);
    if (string.IsNullOrEmpty(this.mCreateRequest.GuildName))
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your Clan name"), (Action<string>) (ret => { }));
      GameSection.StopEvent();
    }
    else if (string.IsNullOrEmpty(this.mCreateRequest.GuildTag))
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your Clan tag. This will be added to your Hunter name as a prefix."), (Action<string>) (ret => { }));
      GameSection.StopEvent();
    }
    else if (string.IsNullOrEmpty(this.mCreateRequest.GuildDescribe))
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, " Please enter your Clan description. You can give an introduction, share your motto, or list rules."), (Action<string>) (ret => { }));
      GameSection.StopEvent();
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<GuildManager>.I.SendCheckClanSetting(MonoBehaviourSingleton<GuildManager>.I.guildData == null ? 0 : MonoBehaviourSingleton<GuildManager>.I.guildData.clanId, this.mCreateRequest.GuildName, this.mCreateRequest.GuildTag, this.mCreateRequest.GuildDescribe, (Action<bool, Error>) ((isSuccess, error) => GameSection.ResumeEvent(isSuccess)));
    }
  }

  private void OnCloseDialog_CustomEmblem() => this.UpdateEmblems();

  public enum UI
  {
    SPR_GUILD_EMBLEM_1,
    SPR_GUILD_EMBLEM_2,
    SPR_GUILD_EMBLEM_3,
    IPT_NAME,
    IPT_TAG,
    LBL_INPUT_TAG,
    IPT_DESCRIBE,
    BTN_INFO,
    SPR_TAG,
    LBL_DEFAULT_INPUT_DESCRIBE,
    LBL_DEFAULT_INPUT_NAME,
    BTN_NEXT,
    BTN_NEXT_UPDATE,
    LBL_INPUT,
  }
}

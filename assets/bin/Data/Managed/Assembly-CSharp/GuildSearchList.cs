// Decompiled with JetBrains decompiler
// Type: GuildSearchList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class GuildSearchList : GameSection
{
  private GuildSearchModel.GuildSearchInfo[] guilds;

  public override void Initialize()
  {
    MonoBehaviourSingleton<GuildManager>.I.ResetGuildSearchRequest();
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) this.StartCoroutine(this.Reload());
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (!GuildManager.IsValidNotEmptyGuildList())
    {
      this.SetActive((Enum) GuildSearchList.UI.GRD_GUILD, false);
      this.SetActive((Enum) GuildSearchList.UI.STR_NON_LIST, true);
    }
    else
    {
      this.guilds = MonoBehaviourSingleton<GuildManager>.I.guilds.ToArray();
      this.SetActive((Enum) GuildSearchList.UI.GRD_GUILD, true);
      this.SetActive((Enum) GuildSearchList.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) GuildSearchList.UI.GRD_GUILD, "GuildSearchListItem", this.guilds.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        GuildSearchModel.GuildSearchInfo guild = this.guilds[i];
        this.SetEvent(t, "INFO", guild.clanId);
        this.SetGuildData(this.guilds[i], t);
      }));
      base.UpdateUI();
    }
  }

  private void SetGuildData(GuildSearchModel.GuildSearchInfo guild, Transform t)
  {
    this.SetLabelText(t, (Enum) GuildSearchList.UI.LBL_GUILD_NAME, string.Format(guild.name + " [{0}]", (object) guild.tag));
    this.SetLabelText(t, (Enum) GuildSearchList.UI.LBL_LABEL, ((GuildManager.GUILD_TYPE) guild.privacy).ToString());
    this.SetLabelText(t, (Enum) GuildSearchList.UI.LBL_HOST_LV, (object) guild.level);
    this.SetLabelText(t, (Enum) GuildSearchList.UI.LBL_HOST_MEMBER_NUM, $"{(object) guild.currentMem}/{(object) guild.memCap}");
    if (guild.emblem != null && guild.emblem.Length >= 3)
    {
      this.SetSprite(t, (Enum) GuildSearchList.UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(guild.emblem[0]));
      this.SetSprite(t, (Enum) GuildSearchList.UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(guild.emblem[1]));
      this.SetSprite(t, (Enum) GuildSearchList.UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(guild.emblem[2]));
    }
    else
    {
      this.SetSprite(t, (Enum) GuildSearchList.UI.SPR_EMBLEM_LAYER_1, "");
      this.SetSprite(t, (Enum) GuildSearchList.UI.SPR_EMBLEM_LAYER_2, "");
      this.SetSprite(t, (Enum) GuildSearchList.UI.SPR_EMBLEM_LAYER_3, "");
    }
    this.SetLabelText(t, (Enum) GuildSearchList.UI.LBL_HOST_NAME, guild.admin);
  }

  private void OnQuery_RELOAD()
  {
    MonoBehaviourSingleton<GuildManager>.I.mSearchKeywork = "";
    GameSection.StayEvent();
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  private void OnCloseDialog_GuildSearchSettings() => this.RefreshUI();

  private void OnCloseDialog_GuildEntryPassRoom() => this.RefreshUI();

  private IEnumerator Reload(Action<bool> cb = null)
  {
    bool is_recv = false;
    this.SendRequest((System.Action) (() => is_recv = true), cb);
    while (!is_recv)
      yield return (object) null;
    this.SetDirty((Enum) GuildSearchList.UI.GRD_GUILD);
    this.RefreshUI();
  }

  private void SendRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<GuildManager>.I.SendSearch((Action<bool, Error>) ((isSuccess, error) =>
    {
      onFinish();
      if (cb == null)
        return;
      cb(isSuccess);
    }), false);
  }

  protected enum UI
  {
    GRD_GUILD,
    STR_NON_LIST,
    LBL_HOST_NAME,
    LBL_HOST_LV,
    LBL_GUILD_NAME,
    LBL_LABEL,
    LBL_STYLE,
    OBJ_SYMBOL,
    SPR_EMBLEM_LAYER_1,
    SPR_EMBLEM_LAYER_2,
    SPR_EMBLEM_LAYER_3,
    LBL_HOST_MEMBER_NUM,
  }
}

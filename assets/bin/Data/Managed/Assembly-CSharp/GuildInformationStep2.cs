// Decompiled with JetBrains decompiler
// Type: GuildInformationStep2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildInformationStep2 : GameSection
{
  private GuildManager.CreateGuildRequestParam mCreateRequest;
  private List<string> lockNames;
  protected int lockIndex;
  private Transform lockPopup;
  private List<string> levelNames;
  protected int levelIndex;
  private Transform levelPopup;
  protected int mapIndex;
  private List<string> spriteMapNames;
  private List<string> additioonMapInfos;

  public override void Initialize()
  {
    base.Initialize();
    if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
      MonoBehaviourSingleton<GuildManager>.I.CreateAddedGuildRequestParam(MonoBehaviourSingleton<GuildManager>.I.guildChangeData);
    this.mCreateRequest = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam();
    this.InitUIData();
  }

  private void InitUIData()
  {
    this.lockNames = new List<string>();
    this.lockNames.Add("PUBLIC");
    this.lockNames.Add("PRIVATE");
    this.lockNames.Add("CLOSED");
    this.lockIndex = (int) this.mCreateRequest.GuildType;
    this.levelNames = new List<string>();
    this.levelNames.Add("15");
    this.levelNames.Add("30");
    this.levelNames.Add("80");
    this.levelNames.Add("150");
    this.levelNames.Add("250");
    this.levelIndex = 0;
    for (int index = 0; index < this.levelNames.Count; ++index)
    {
      if (int.Parse(this.levelNames[index]) == this.mCreateRequest.GuildMinLevel)
      {
        this.levelIndex = index;
        break;
      }
    }
    this.spriteMapNames = new List<string>();
    this.spriteMapNames.Add("temp_map1");
    this.spriteMapNames.Add("temp_map2");
    this.additioonMapInfos = new List<string>();
    this.additioonMapInfos.Add("+5% Fire Damage");
    this.additioonMapInfos.Add("+5% Water Damage");
    this.mapIndex = this.mCreateRequest.GuildMapID;
    this.UpdateMap();
    bool is_visible = MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1;
    this.SetActive((Enum) GuildInformationStep2.UI.BTN_NEXT, !is_visible);
    this.SetActive((Enum) GuildInformationStep2.UI.BTN_NEXT_UPDATE, is_visible);
    this.SetActive((Enum) GuildInformationStep2.UI.SPR_TYPE_INFO, false);
    this.SetTouchAndRelease((Enum) GuildInformationStep2.UI.BTN_INFO, "TYPE_INFO_SHOW", "TYPE_INFO_HIDE");
    this.SetActive((Enum) GuildInformationStep2.UI.SPR_TYPE_INFO, false);
    this.SetSupportEncoding((Enum) GuildInformationStep2.UI.STR_TYPE_INFOR, true);
    this.mCreateRequest.SetGuildType(this.mCreateRequest.GuildType);
    this.mCreateRequest.SetGuildMinLevel(int.Parse(this.levelNames[this.levelIndex]));
  }

  public override void UpdateUI()
  {
    this.UpdateMinLevel();
    this.UpdateLock();
  }

  private void UpdateMinLevel()
  {
    this.SetLabelText((Enum) GuildInformationStep2.UI.LBL_TARGET_MIN_LEVEL, this.levelNames[this.levelIndex]);
  }

  private void UpdateLock()
  {
    this.SetLabelText((Enum) GuildInformationStep2.UI.LBL_TARGET_LOCK, this.lockNames[this.lockIndex]);
  }

  private void UpdateMap()
  {
    this.SetSprite((Enum) GuildInformationStep2.UI.SPR_MAP, this.spriteMapNames[this.mapIndex]);
    this.SetLabelText((Enum) GuildInformationStep2.UI.STR_MAP_ADDITION, this.additioonMapInfos[this.mapIndex]);
  }

  private void OnQuery_MAP_NEXT()
  {
    this.mapIndex = (this.mapIndex + 1) % 2;
    this.UpdateMap();
  }

  private void OnQuery_MAP_BACK()
  {
    this.mapIndex = (this.mapIndex - 1 + 2) % 2;
    this.UpdateMap();
  }

  private void OnQuery_TARGET_LOCK()
  {
    if (Object.op_Equality((Object) this.lockPopup, (Object) null))
      this.lockPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) GuildInformationStep2.UI.POP_TARGET_LOCK), false);
    if (Object.op_Equality((Object) this.lockPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.lockNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int lockIndex = this.lockIndex;
    UIScrollablePopupList.CreatePopup(this.lockPopup, this.GetCtrl((Enum) GuildInformationStep2.UI.POP_TARGET_LOCK), 3, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.lockNames.ToArray(), button_enable, lockIndex, (Action<int>) (index =>
    {
      this.lockIndex = index;
      this.mCreateRequest.SetGuildType((GuildManager.GUILD_TYPE) this.lockIndex);
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_LEVEL()
  {
    if (Object.op_Equality((Object) this.levelPopup, (Object) null))
      this.levelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) GuildInformationStep2.UI.POP_TARGET_MIN_LEVEL), false);
    if (Object.op_Equality((Object) this.levelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.levelNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int levelIndex = this.levelIndex;
    UIScrollablePopupList.CreatePopup(this.levelPopup, this.GetCtrl((Enum) GuildInformationStep2.UI.POP_TARGET_MIN_LEVEL), this.levelNames.Count, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.levelNames.ToArray(), button_enable, levelIndex, (Action<int>) (index =>
    {
      this.levelIndex = index;
      this.mCreateRequest.SetGuildMinLevel(int.Parse(this.levelNames[this.levelIndex]));
      this.RefreshUI();
    }));
  }

  private void OnQuery_SETTING_UPDATE()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendChangeSetting(this.mCreateRequest, (Action<bool, Error>) ((isSuccess, error) =>
    {
      if (isSuccess)
        MonoBehaviourSingleton<GuildManager>.I.GetClanStat((Action<bool>) (success => GameSection.ResumeEvent(true)));
      else
        GameSection.ResumeEvent(true);
    }));
  }

  private void OnQuery_CLOSE()
  {
    MonoBehaviourSingleton<GuildManager>.I.ClearCreateGuildRequestParam();
    if ((MonoBehaviourSingleton<GuildManager>.I.guildData == null ? 0 : (MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1 ? 1 : 0)) == 0)
      return;
    GameSection.ChangeEvent("CLOSE_UPDATE");
  }

  private void OnQuery_TYPE_INFO_SHOW()
  {
    this.SetActive((Enum) GuildInformationStep2.UI.SPR_TYPE_INFO, true);
  }

  private void OnQuery_TYPE_INFO_HIDE()
  {
    this.SetActive((Enum) GuildInformationStep2.UI.SPR_TYPE_INFO, false);
  }

  public enum UI
  {
    POP_TARGET_LOCK,
    LBL_TARGET_LOCK,
    POP_TARGET_MIN_LEVEL,
    LBL_TARGET_MIN_LEVEL,
    SPR_MAP,
    STR_MAP_ADDITION,
    BTN_INFO,
    SPR_TYPE_INFO,
    STR_TYPE_INFOR,
    BTN_NEXT,
    BTN_NEXT_UPDATE,
  }
}

// Decompiled with JetBrains decompiler
// Type: GuildInfoDetailDialog
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
public class GuildInfoDetailDialog : GameSection
{
  private List<FriendCharaInfo> members;
  private GuildStatisticInfo _info;
  private int _clanId;
  private int _clanMasterUserId;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this._info = eventData[0] as GuildStatisticInfo;
    this._clanId = (int) eventData[1];
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool is_finish = false;
    MonoBehaviourSingleton<GuildManager>.I.SendMemberList(this._clanId, (Action<bool, GuildMemberListModel>) ((success, ret) =>
    {
      this.members = new List<FriendCharaInfo>((IEnumerable<FriendCharaInfo>) ret.result.list);
      this.members.Remove(this.members.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)));
      this._clanMasterUserId = ret.result.clanMasterId;
      is_finish = true;
    }));
    while (!is_finish)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) GuildInfoDetailDialog.UI.LBL_HUNTER, string.Format(this.sectionData.GetText("TEXT_HUNTER"), (object) this._info.currentMem, (object) this._info.memCap));
    this.SetGrid((Enum) GuildInfoDetailDialog.UI.GRD_LIST, "GuildInfoDetailListItem", this.members.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      FriendCharaInfo member = this.members[i];
      string str = member.userId == this._clanMasterUserId ? $"({this.sectionData.GetText("TEXT_MASTER")})" : "";
      this.SetLabelText(t, (Enum) GuildInfoDetailDialog.UI.LBL_NAME, member.name + str);
      this.SetEvent(t, "DETAIL", (object) member);
    }));
  }

  private enum UI
  {
    LBL_HUNTER,
    SCR_LIST,
    GRD_LIST,
    LBL_NAME,
  }
}

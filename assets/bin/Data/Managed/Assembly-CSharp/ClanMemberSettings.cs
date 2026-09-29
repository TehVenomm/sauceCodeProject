// Decompiled with JetBrains decompiler
// Type: ClanMemberSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ClanMemberSettings : GameSection
{
  private FriendCharaInfo userInfo;
  private string[] statuses;
  private string[] dispStatuses;
  private int selectedStatus;
  private Transform statusPopup;

  public override void Initialize()
  {
    if (GameSection.GetEventData() is FriendCharaInfo eventData)
      this.userInfo = eventData;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    this.statuses = StringTable.GetAllInCategory(STRING_CATEGORY.CLAN_STATUS);
    this.dispStatuses = new string[3]
    {
      StringTable.Get(STRING_CATEGORY.CLAN_STATUS, 1U),
      StringTable.Get(STRING_CATEGORY.CLAN_STATUS, 2U),
      StringTable.Get(STRING_CATEGORY.CLAN_STATUS, 3U)
    };
    if (this.statuses.Length > this.userInfo.userClanData.stat)
      this.selectedStatus = Array.IndexOf<string>(this.dispStatuses, this.statuses[this.userInfo.userClanData.stat]);
    yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetRenderPlayerModel((Enum) ClanMemberSettings.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) this.userInfo, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText((Enum) ClanMemberSettings.UI.LBL_NAME, this.userInfo.name);
    this.SetLabelText((Enum) ClanMemberSettings.UI.LBL_LEVEL, this.userInfo.level.ToString());
    this.SetStatusSprite(this.userInfo.userClanData);
    this.UpdateStatus();
  }

  private void UpdateStatus()
  {
    this.SetLabelText((Enum) ClanMemberSettings.UI.LBL_TARGET_STATUS, this.dispStatuses[this.selectedStatus]);
  }

  private void SetStatusSprite(UserClanData userClan)
  {
    if (userClan.IsLeader())
    {
      this.SetActive((Enum) ClanMemberSettings.UI.SPR_STATUS, true);
      this.SetSprite((Enum) ClanMemberSettings.UI.SPR_STATUS, "Clan_HeadmasterIcon");
    }
    else if (userClan.IsSubLeader())
    {
      this.SetActive((Enum) ClanMemberSettings.UI.SPR_STATUS, true);
      this.SetSprite((Enum) ClanMemberSettings.UI.SPR_STATUS, "Clan_DeputyHeadmasterIcon");
    }
    else
      this.SetActive((Enum) ClanMemberSettings.UI.SPR_STATUS, false);
  }

  private void OnQuery_TARGET_STATUS()
  {
    if (Object.op_Equality((Object) this.statusPopup, (Object) null))
      this.statusPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) ClanMemberSettings.UI.POP_TARGET_STATUS), false);
    if (Object.op_Equality((Object) this.statusPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.dispStatuses.Length];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int selectedStatus = this.selectedStatus;
    UIScrollablePopupList.CreatePopup(this.statusPopup, this.GetCtrl((Enum) ClanMemberSettings.UI.POP_TARGET_STATUS), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.dispStatuses, button_enable, selectedStatus, (Action<int>) (index =>
    {
      this.selectedStatus = index;
      this.RefreshUI();
    }));
  }

  private void OnQuery_CHANGE()
  {
    GameSection.SetEventData((object) new string[2]
    {
      this.userInfo.name,
      this.dispStatuses[this.selectedStatus]
    });
  }

  private void OnQuery_ClanMemberStatusChangeConfirmDialog_YES()
  {
    GameSection.StayEvent();
    int num = Array.IndexOf<string>(this.statuses, this.dispStatuses[this.selectedStatus]);
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestEditMember(new ClanEditMemberModel.RequestSendForm()
    {
      uId = this.userInfo.userId,
      stat = num
    }, (Action<bool>) (isSuccess =>
    {
      if (!isSuccess)
        return;
      MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClanData =>
      {
        MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
        GameSection.ResumeEvent(isSuccess);
      }));
    }));
  }

  private void OnQuery_KICK()
  {
    GameSection.SetEventData((object) new string[2]
    {
      this.userInfo.name,
      this.userInfo.userClanData.name
    });
  }

  private void OnQuery_ClanKickConfirmDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestKick(new ClanKickModel.RequestSendForm()
    {
      uId = this.userInfo.userId
    }, (Action<bool>) (isSuccess =>
    {
      if (MonoBehaviourSingleton<ClanManager>.IsValid())
        MonoBehaviourSingleton<ClanManager>.I.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(this.userInfo.userId);
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  private enum UI
  {
    TEX_MODEL,
    LBL_NAME,
    LBL_LEVEL,
    SPR_STATUS,
    LBL_TARGET_STATUS,
    POP_TARGET_STATUS,
  }
}

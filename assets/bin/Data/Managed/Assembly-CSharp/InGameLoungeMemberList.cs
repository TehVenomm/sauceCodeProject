// Decompiled with JetBrains decompiler
// Type: InGameLoungeMemberList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameLoungeMemberList : GameSection
{
  private List<CharaInfo> memberInfo;

  public override void Initialize()
  {
    this.memberInfo = new List<CharaInfo>(8);
    this.UpdateMemberList();
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
    {
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
      this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    int count = this.memberInfo.Count;
    if (count <= 0)
    {
      this.SetActive((Enum) InGameLoungeMemberList.UI.GRD_LIST, false);
      this.SetActive((Enum) InGameLoungeMemberList.UI.STR_NON_LIST, true);
    }
    else
    {
      this.SetGrid((Enum) InGameLoungeMemberList.UI.GRD_LIST, "InGameLoungeMemberListItem", count, false, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetupListItem(this.memberInfo[i], i, t)));
      this.SetActive((Enum) InGameLoungeMemberList.UI.STR_NON_LIST, false);
    }
  }

  public override void Exit()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    base.Exit();
  }

  private void SetupListItem(CharaInfo userInfo, int i, Transform t)
  {
    FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
    this.SetLabelText(t, (Enum) InGameLoungeMemberList.UI.LBL_NAME, userInfo.name);
    this.SetLabelText(t, (Enum) InGameLoungeMemberList.UI.LBL_LEVEL, userInfo.level.ToString());
    string clanId = userInfo.userClanData != null ? userInfo.userClanData.cId : "0";
    this.SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower, clanId);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
    this.SetPlayingStatus(t, userInfo.userId);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
    bool is_visible = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId;
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.BTN_KICK, is_visible);
    this.SetEvent(t, (Enum) InGameLoungeMemberList.UI.BTN_KICK, "KICK", i);
    this.SetEvent(t, (Enum) InGameLoungeMemberList.UI.BTN_JOIN, "JOIN", i);
  }

  private void SetFollowStatus(
    Transform t,
    int user_id,
    bool following,
    bool follower,
    string clanId)
  {
    bool is_visible1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_BLACKLIST_ICON, is_visible1);
    if (is_visible1)
      return;
    bool is_visible2 = false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
      is_visible2 = clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_BLACKLIST_ICON, is_visible1);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.OBJ_FOLLOW, following | follower);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_FOLLOW, following);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_FOLLOWER, follower);
    this.SetActive(t, (Enum) InGameLoungeMemberList.UI.SPR_SAME_CLAN_ICON, is_visible2);
    UIGrid component = this.GetComponent<UIGrid>(t, (Enum) InGameLoungeMemberList.UI.GRD_FOLLOW_ARROW);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reposition();
  }

  private void OnScreenRotate(bool is_portrait)
  {
    UIPanel panel = ((Component) this.GetCtrl((Enum) InGameLoungeMemberList.UI.SCR_LIST)).GetComponent<UIPanel>();
    Vector4 baseClipRegion = panel.baseClipRegion;
    if (is_portrait)
    {
      Vector3 localPosition = this.GetCtrl((Enum) InGameLoungeMemberList.UI.PORTRAIT_FRAME).localPosition;
      int height = this.GetHeight((Enum) InGameLoungeMemberList.UI.PORTRAIT_FRAME);
      this.GetCtrl((Enum) InGameLoungeMemberList.UI.FRAME).localPosition = localPosition;
      this.SetHeight((Enum) InGameLoungeMemberList.UI.FRAME, height);
      this.GetCtrl((Enum) InGameLoungeMemberList.UI.SCR_LIST).parent = this.GetCtrl((Enum) InGameLoungeMemberList.UI.PORTRAIT_LIST);
      baseClipRegion.w = (float) this.GetHeight((Enum) InGameLoungeMemberList.UI.PORTRAIT_LIST);
    }
    else
    {
      Vector3 localPosition = this.GetCtrl((Enum) InGameLoungeMemberList.UI.LANDSCAPE_FRAME).localPosition;
      int height = this.GetHeight((Enum) InGameLoungeMemberList.UI.LANDSCAPE_FRAME);
      this.GetCtrl((Enum) InGameLoungeMemberList.UI.FRAME).localPosition = localPosition;
      this.SetHeight((Enum) InGameLoungeMemberList.UI.FRAME, height);
      this.GetCtrl((Enum) InGameLoungeMemberList.UI.SCR_LIST).parent = this.GetCtrl((Enum) InGameLoungeMemberList.UI.LANDSCAPE_LIST);
      baseClipRegion.w = (float) this.GetHeight((Enum) InGameLoungeMemberList.UI.LANDSCAPE_LIST);
    }
    panel.baseClipRegion = baseClipRegion;
    panel.clipOffset = Vector2.zero;
    this.GetCtrl((Enum) InGameLoungeMemberList.UI.SCR_LIST).localPosition = Vector3.zero;
    this.ScrollViewResetPosition((Enum) InGameLoungeMemberList.UI.SCR_LIST);
    this.UpdateAnchors();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() =>
    {
      this.RefreshUI();
      panel.Refresh();
    });
  }

  private void UpdateMemberList()
  {
    this.memberInfo.Clear();
    List<PartyModel.SlotInfo> slotInfos = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
    for (int index = 0; index < slotInfos.Count; ++index)
    {
      if (slotInfos[index].userInfo != null && slotInfos[index].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        this.memberInfo.Add(slotInfos[index].userInfo);
    }
    this.RefreshUI();
  }

  private void SetPlayingStatus(Transform root, int userId)
  {
    this.SetActive(root, (Enum) InGameLoungeMemberList.UI.OBJ_LOUNGE, false);
    this.SetActive(root, (Enum) InGameLoungeMemberList.UI.OBJ_FIELD, false);
    this.SetActive(root, (Enum) InGameLoungeMemberList.UI.OBJ_QUEST, false);
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
      return;
    LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
    if (loungeMemberStatu == null)
      return;
    switch (loungeMemberStatu.GetStatus())
    {
      case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.OBJ_LOUNGE, true);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
        this.SetQuestInfo(root, loungeMemberStatu.questId);
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.LBL_PLAYING_QUEST, false);
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.LBL_PLAYING_READY, true);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.QUEST:
        this.SetQuestInfo(root, loungeMemberStatu.questId);
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.BTN_JOIN, !this.CheckRush(loungeMemberStatu.questId));
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.LBL_PLAYING_QUEST, true);
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.LBL_PLAYING_READY, false);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.FIELD:
        this.SetActive(root, (Enum) InGameLoungeMemberList.UI.OBJ_FIELD, true);
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) loungeMemberStatu.fieldMapId);
        if (fieldMapData == null)
        {
          this.SetLabelText(root, (Enum) InGameLoungeMemberList.UI.LBL_AREA_NAME, "");
          break;
        }
        RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
        if (data == null)
        {
          this.SetLabelText(root, (Enum) InGameLoungeMemberList.UI.LBL_AREA_NAME, fieldMapData.mapName);
          break;
        }
        this.SetLabelText(root, (Enum) InGameLoungeMemberList.UI.LBL_AREA_NAME, $"{data.regionName} - {fieldMapData.mapName}");
        break;
    }
  }

  private void SetQuestInfo(Transform root, int questId)
  {
    this.SetActive(root, (Enum) InGameLoungeMemberList.UI.OBJ_QUEST, true);
    string questText = Singleton<QuestTable>.I.GetQuestData((uint) questId).questText;
    this.SetLabelText(root, (Enum) InGameLoungeMemberList.UI.LBL_QUEST_NAME, questText);
  }

  private bool CheckRush(int questId)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) questId);
    return questData != null && questData.rushId != 0U;
  }

  private void JoinField(int fieldMapId)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    if ((long) fieldMapId == (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
    {
      GameSection.StopEvent();
    }
    else
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) fieldMapId);
      if (fieldMapData == null || fieldMapData.jumpPortalID == 0U)
        Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
      else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, false))
        GameSection.StopEvent();
      else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
      {
        this.DispatchEvent("CANT_JUMP");
      }
      else
      {
        MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
        MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
      }
    }
  }

  private void JoinParty(string partyId)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
    {
      new EventData("MAIN_MENU_LOUNGE", (object) null),
      new EventData("GACHA_QUEST_COUNTER", (object) null),
      new EventData("JOIN_ROOM", (object) partyId)
    });
  }

  private void OnQuery_CHANGE_INFO() => this.UpdateMemberList();

  private void OnQuery_JOIN()
  {
    CharaInfo charaInfo = this.memberInfo[(int) GameSection.GetEventData()];
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
      return;
    LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[charaInfo.userId];
    switch (loungeMemberStatu.GetStatus())
    {
      case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
        this.OnQuery_MAIN_MENU_LOUNGE();
        break;
      case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
      case LoungeMemberStatus.MEMBER_STATUS.QUEST:
        this.JoinParty(loungeMemberStatu.partyId);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.FIELD:
        this.JoinField(loungeMemberStatu.fieldMapId);
        break;
    }
  }

  private void OnQuery_KICK()
  {
    CharaInfo charaInfo = this.memberInfo[(int) GameSection.GetEventData()];
    GameSection.SetEventData((object) new object[1]
    {
      (object) charaInfo.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRoomPartyKick((Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)), charaInfo.userId);
  }

  private enum UI
  {
    FRAME,
    GRD_LIST,
    STR_NON_LIST,
    SCR_LIST,
    LBL_NAME,
    LBL_LEVEL,
    LBL_HP,
    LBL_ATK,
    LBL_DEF,
    LBL_COMMENT,
    GRD_FOLLOW_ARROW,
    OBJ_FOLLOW,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    BTN_FOLLOW,
    BTN_BLACK_LIST,
    SPR_ICON_FIRST_MET,
    PORTRAIT_FRAME,
    PORTRAIT_LIST,
    LANDSCAPE_FRAME,
    LANDSCAPE_LIST,
    SPR_ICON_HOST,
    BTN_KICK,
    OBJ_LOUNGE,
    OBJ_QUEST,
    OBJ_FIELD,
    BTN_JOIN,
    LBL_AREA_NAME,
    LBL_QUEST_NAME,
    LBL_PLAYING_QUEST,
    LBL_PLAYING_READY,
  }
}

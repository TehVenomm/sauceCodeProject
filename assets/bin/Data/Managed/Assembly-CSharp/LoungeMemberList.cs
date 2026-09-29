// Decompiled with JetBrains decompiler
// Type: LoungeMemberList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoungeMemberList : GameSection
{
  private List<PartyModel.SlotInfo> members;

  public override void Initialize()
  {
    this.members = new List<PartyModel.SlotInfo>(8);
    this.SetLabelText((Enum) LoungeMemberList.UI.STR_LIST_NUM, this.sectionData.GetText("MEMBER_NUMBER"));
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool isWait = true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.GetRallyList((Action<bool>) (rallySuccess => isWait = false));
    while (isWait)
      yield return (object) null;
    base.Initialize();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
  }

  public override void UpdateUI()
  {
    this.SetMembers();
    this.UpdateListUI();
    this.SetActive((Enum) LoungeMemberList.UI.LBL_NON_LIST, this.members.Count <= 0);
    this.SetLabelText((Enum) LoungeMemberList.UI.LBL_MEMBER_NUMBER_NOW, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount().ToString());
    this.SetLabelText((Enum) LoungeMemberList.UI.LBL_MEMBER_NUMBER_MAX, (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.num + 1).ToString());
  }

  private void SetMembers()
  {
    List<PartyModel.SlotInfo> slotInfos = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
    this.members.Clear();
    for (int index = 0; index < slotInfos.Count; ++index)
    {
      if (slotInfos[index].userInfo != null && slotInfos[index].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        this.members.Add(slotInfos[index]);
    }
  }

  private void UpdateListUI()
  {
    this.SetDynamicList((Enum) LoungeMemberList.UI.GRD_LIST, "LoungeMemberListItem", this.members.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetupListItem(this.members[i], i, t, is_recycle)));
  }

  private void SetupListItem(PartyModel.SlotInfo data, int i, Transform t, bool is_recycle)
  {
    this.SetEvent(t, "DETAIL", i);
    this.SetMemberInfo(data, i, t);
  }

  private void SetMemberInfo(PartyModel.SlotInfo data, int i, Transform t)
  {
    CharaInfo userInfo = data.userInfo;
    FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
    MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4).SetEquipSet(data.userInfo.equipSet);
    this.SetRenderPlayerModel(t, (Enum) LoungeMemberList.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(userInfo, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) LoungeMemberList.UI.LBL_NAME, userInfo.name);
    this.SetLabelText(t, (Enum) LoungeMemberList.UI.LBL_LEVEL, userInfo.level.ToString());
    string clanId = userInfo.userClanData != null ? userInfo.userClanData.cId : "0";
    this.SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower, clanId);
    this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
    this.SetPlayingStatus(t, userInfo.userId);
    this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
    ((Component) this.FindCtrl(t, (Enum) LoungeMemberList.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(userInfo.selectedDegrees, false, (Action<DegreePlate>) (x => ((Component) this.GetCtrl((Enum) LoungeMemberList.UI.GRD_LIST)).GetComponent<UIGrid>().Reposition()));
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsRallyUser(userInfo.userId))
      return;
    this.SetBadge(t, -1, (SpriteAlignment) 1, 10, 0, true);
  }

  private void SetFollowStatus(
    Transform t,
    int user_id,
    bool following,
    bool follower,
    string clanId)
  {
    bool is_visible1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_BLACKLIST_ICON, is_visible1);
    if (is_visible1)
    {
      this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_FOLLOW, false);
      this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_FOLLOWER, false);
    }
    else
    {
      bool is_visible2 = false;
      if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
        is_visible2 = clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
      this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_BLACKLIST_ICON, is_visible1);
      this.SetActive(t, (Enum) LoungeMemberList.UI.OBJ_FOLLOW, following | follower);
      this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_FOLLOW, following);
      this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_FOLLOWER, follower);
      this.SetActive(t, (Enum) LoungeMemberList.UI.SPR_SAME_CLAN_ICON, is_visible2);
      UIGrid component = this.GetComponent<UIGrid>(t, (Enum) LoungeMemberList.UI.GRD_FOLLOW_ARROW);
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Reposition();
    }
  }

  private void SetPlayingStatus(Transform root, int userId)
  {
    this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_LOUNGE, false);
    this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_FIELD, false);
    this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_QUEST, false);
    this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_ARENA, false);
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
    {
      this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_LOUNGE, true);
    }
    else
    {
      LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
      if (loungeMemberStatu == null)
      {
        this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_LOUNGE, true);
      }
      else
      {
        switch (loungeMemberStatu.GetStatus())
        {
          case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
            this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_LOUNGE, true);
            break;
          case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
            this.SetQuestInfo(root, loungeMemberStatu.questId);
            this.SetActive(root, (Enum) LoungeMemberList.UI.LBL_PLAYING_QUEST, false);
            this.SetActive(root, (Enum) LoungeMemberList.UI.LBL_PLAYING_READY, true);
            break;
          case LoungeMemberStatus.MEMBER_STATUS.QUEST:
            this.SetQuestInfo(root, loungeMemberStatu.questId);
            this.SetActive(root, (Enum) LoungeMemberList.UI.LBL_PLAYING_QUEST, true);
            this.SetActive(root, (Enum) LoungeMemberList.UI.LBL_PLAYING_READY, false);
            break;
          case LoungeMemberStatus.MEMBER_STATUS.FIELD:
            this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_FIELD, true);
            FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) loungeMemberStatu.fieldMapId);
            if (fieldMapData == null)
            {
              this.SetLabelText(root, (Enum) LoungeMemberList.UI.LBL_AREA_NAME, "");
              break;
            }
            RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
            if (data == null)
            {
              this.SetLabelText(root, (Enum) LoungeMemberList.UI.LBL_AREA_NAME, fieldMapData.mapName);
              break;
            }
            this.SetLabelText(root, (Enum) LoungeMemberList.UI.LBL_AREA_NAME, $"{data.regionName} - {fieldMapData.mapName}");
            break;
          case LoungeMemberStatus.MEMBER_STATUS.ARENA:
            this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_ARENA, true);
            this.SetActive(root, (Enum) LoungeMemberList.UI.LBL_PLAYING_ARENA, true);
            break;
        }
      }
    }
  }

  private void SetQuestInfo(Transform root, int questId)
  {
    this.SetActive(root, (Enum) LoungeMemberList.UI.OBJ_QUEST, true);
    string questText = Singleton<QuestTable>.I.GetQuestData((uint) questId).questText;
    this.SetLabelText(root, (Enum) LoungeMemberList.UI.LBL_QUEST_NAME, questText);
  }

  private void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    CharaInfo userInfo = this.members[eventData].userInfo;
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) userInfo);
  }

  private enum UI
  {
    GRD_LIST,
    TEX_MODEL,
    LBL_LEVEL,
    LBL_NAME,
    GRD_FOLLOW_ARROW,
    OBJ_FOLLOW,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    OBJ_FIELD,
    OBJ_QUEST,
    OBJ_LOUNGE,
    OBJ_ARENA,
    LBL_PLAYING_FIELD,
    LBL_AREA_NAME,
    LBL_PLAYING_QUEST,
    LBL_PLAYING_READY,
    LBL_QUEST_NAME,
    LBL_IN_LOUNGE,
    LBL_PLAYING_ARENA,
    LBL_NON_LIST,
    SPR_ICON_HOST,
    SPR_ICON_FIRST_MET,
    LBL_MEMBER_NUMBER_NOW,
    LBL_MEMBER_NUMBER_MAX,
    STR_LIST_NUM,
    OBJ_DEGREE_FRAME_ROOT,
  }
}

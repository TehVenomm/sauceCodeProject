// Decompiled with JetBrains decompiler
// Type: QuestAcceptInvitation
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
public class QuestAcceptInvitation : QuestSearchListSelect
{
  private PartyModel.Party[] parties;
  private LoungeModel.Lounge[] lounges;
  private LoungeModel.SlotInfo[] rallyInvites;
  private GuildInvitedModel.GuildInvitedInfo[] guildInvites;
  private DonateInvitationInfo[] guildDonateInvites;
  private QuestAcceptInvitation.UI[] loungeMembers = new QuestAcceptInvitation.UI[7]
  {
    QuestAcceptInvitation.UI.TGL_L_MEMBER_1,
    QuestAcceptInvitation.UI.TGL_L_MEMBER_2,
    QuestAcceptInvitation.UI.TGL_L_MEMBER_3,
    QuestAcceptInvitation.UI.TGL_L_MEMBER_4,
    QuestAcceptInvitation.UI.TGL_L_MEMBER_5,
    QuestAcceptInvitation.UI.TGL_L_MEMBER_6,
    QuestAcceptInvitation.UI.TGL_L_MEMBER_7
  };

  protected override void SendSearchRequest(System.Action onFinish, Action<bool> cb)
  {
    this.StartCoroutine(this.GetInvitedList(onFinish, cb));
  }

  private IEnumerator GetInvitedList(System.Action onFinish, Action<bool> cb)
  {
    bool partySuccess_ = false;
    bool loungeSuccess_ = false;
    bool rallySuccess_ = false;
    bool guildInviteSuccess_ = false;
    bool guildDonateInviteSuccess_ = false;
    bool waitGetData = true;
    MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty((Action<bool>) (partySuccess =>
    {
      partySuccess_ = partySuccess;
      waitGetData = false;
    }));
    while (waitGetData)
      yield return (object) null;
    waitGetData = true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInvitedLounge((Action<bool>) (loungeSuccess =>
    {
      loungeSuccess_ = loungeSuccess;
      waitGetData = false;
    }));
    while (waitGetData)
      yield return (object) null;
    waitGetData = true;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.GetRallyList((Action<bool>) (rallySuccess =>
    {
      rallySuccess_ = rallySuccess;
      waitGetData = false;
    }));
    while (waitGetData)
      yield return (object) null;
    waitGetData = true;
    MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList((Action<bool>) (guildDonateInviteSuccess =>
    {
      guildDonateInviteSuccess_ = guildDonateInviteSuccess;
      waitGetData = false;
    }));
    while (waitGetData)
      yield return (object) null;
    onFinish();
    if (cb != null)
      cb(partySuccess_ & loungeSuccess_ & rallySuccess_ & guildInviteSuccess_ & guildDonateInviteSuccess_);
  }

  public override void UpdateUI()
  {
    if (!PartyManager.IsValidNotEmptyList() && !GuildManager.IsValidNotEmptyInviteList() && !GuildManager.IsValidNotEmptyDonateInviteList())
    {
      MonoBehaviourSingleton<UserInfoManager>.I.ClearPartyInvite();
      MonoBehaviourSingleton<UIManager>.I.invitationButton.Close();
      MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
    }
    if (!PartyManager.IsValidNotEmptyList() && !LoungeMatchingManager.IsValidNotEmptyList() && !LoungeMatchingManager.IsValidNotEmptyRallyList() && !GuildManager.IsValidNotEmptyInviteList() && !GuildManager.IsValidNotEmptyDonateInviteList())
    {
      this.SetActive((Enum) QuestAcceptInvitation.UI.GRD_QUEST, false);
      this.SetActive((Enum) QuestAcceptInvitation.UI.TBL_QUEST, false);
      this.SetActive((Enum) QuestAcceptInvitation.UI.STR_NON_LIST, true);
    }
    else
    {
      this.parties = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
      this.lounges = MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.ToArray();
      this.rallyInvites = MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite.ToArray();
      this.guildInvites = !MonoBehaviourSingleton<GuildManager>.IsValid() || MonoBehaviourSingleton<GuildManager>.I.guildInviteList == null ? new GuildInvitedModel.GuildInvitedInfo[0] : MonoBehaviourSingleton<GuildManager>.I.guildInviteList.ToArray();
      this.guildDonateInvites = MonoBehaviourSingleton<GuildManager>.I.donateInviteList.ToArray();
      this.SetActive((Enum) QuestAcceptInvitation.UI.TBL_QUEST, true);
      this.SetActive((Enum) QuestAcceptInvitation.UI.GRD_QUEST, true);
      this.SetActive((Enum) QuestAcceptInvitation.UI.STR_NON_LIST, false);
      this.UpdateTable();
    }
  }

  protected void UpdateTable()
  {
    int length1 = this.parties.Length;
    int length2 = this.lounges.Length;
    int length3 = this.rallyInvites.Length;
    int length4 = this.guildInvites.Length;
    int length5 = this.guildDonateInvites.Length;
    int item_num = length1 + length2 + length3 + length4 + length5;
    int partyStartIndex = length2;
    int rallyStartIndex = length1 + length2;
    int guildInviteStartIndex = length1 + length2 + length3;
    int guildDonateInviteStartIndex = length1 + length2 + length3 + length4;
    Transform ctrl = this.GetCtrl((Enum) QuestAcceptInvitation.UI.TBL_QUEST);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      int num = 0;
      for (int childCount = ctrl.childCount; num < childCount; ++num)
      {
        Transform child = ctrl.GetChild(0);
        child.parent = (Transform) null;
        Object.Destroy((Object) ((Component) child).gameObject);
      }
    }
    this.SetTable((Enum) QuestAcceptInvitation.UI.TBL_QUEST, "", item_num, true, (Func<int, Transform, Transform>) ((i, parent) => i < guildDonateInviteStartIndex ? (i < guildInviteStartIndex ? (i < rallyStartIndex ? (i < partyStartIndex ? this.Realizes("LoungeSearchListItem", parent) : this.Realizes("QuestInvitationListItem", parent)) : this.Realizes("LoungeMemberListItem", parent)) : this.Realizes("GuildInvitedListItem", parent)) : this.Realizes(this.InitGuildDonateInviteObject(i - guildDonateInviteStartIndex), parent)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.SetActive(t, true);
      if (i >= guildDonateInviteStartIndex)
        this.InitGuildDonateInvite(i - guildDonateInviteStartIndex, t);
      else if (i >= guildInviteStartIndex)
        this.InitGuild(i - guildInviteStartIndex, t);
      else if (i >= rallyStartIndex)
        this.InitRally(i - rallyStartIndex, t);
      else if (i >= partyStartIndex)
        this.InitParty(i - partyStartIndex, t);
      else
        this.InitLounge(i, t);
    }));
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) QuestAcceptInvitation.UI.SCR_QUEST)).enabled = true;
    this.RepositionTable();
  }

  private void InitLounge(int index, Transform t)
  {
    this.SetEvent(t, "SELECT_LOUNGE", index);
    LoungeModel.Lounge lounge = this.lounges[index];
    CharaInfo charaInfo = (CharaInfo) null;
    for (int index1 = 0; index1 < lounge.slotInfos.Count; ++index1)
    {
      if (lounge.slotInfos[index1].userInfo.userId == lounge.ownerUserId)
      {
        charaInfo = lounge.slotInfos[index1].userInfo;
        break;
      }
    }
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_HOST_NAME, charaInfo.name);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_HOST_LV, charaInfo.level.ToString());
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_LOUNGE_NAME, lounge.name);
    string text = StringTable.Get(STRING_CATEGORY.LOUNGE_LABEL, (uint) lounge.label);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_LABEL, text);
    this.SetStamp(t, lounge.stampId);
    int num1 = lounge.num + 1;
    int num2 = lounge.slotInfos.Count<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (slotInfo => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != lounge.ownerUserId));
    for (int index2 = 0; index2 < 7; ++index2)
    {
      bool is_visible = index2 < num1 - 1;
      this.SetActive(t, (Enum) this.loungeMembers[index2], is_visible);
      this.SetToggle(t, (Enum) this.loungeMembers[index2], index2 < num2);
    }
  }

  private void InitParty(int index, Transform t)
  {
    QuestTable.QuestTableData questTableData = this.parties[index].quest.explore == null ? Singleton<QuestTable>.I.GetQuestData((uint) this.parties[index].quest.questId) : Singleton<QuestTable>.I.GetQuestData((uint) this.parties[index].quest.explore.mainQuestId);
    if (questTableData == null)
    {
      this.SetActive(t, false);
    }
    else
    {
      QuestAcceptInvitation.UI enum_value = QuestAcceptInvitation.UI.OBJ_QUEST_INFO_ROOT;
      if (questTableData.questType == QUEST_TYPE.ORDER)
      {
        enum_value = QuestAcceptInvitation.UI.OBJ_ORDER_QUEST_INFO_ROOT;
        this.SetToggle(t, (Enum) QuestAcceptInvitation.UI.OBJ_ORDER_QUEST_INFO_ROOT, true);
      }
      else
        this.SetToggle(t, (Enum) QuestAcceptInvitation.UI.OBJ_ORDER_QUEST_INFO_ROOT, false);
      this.SetEvent(t, "SELECT_ROOM", index);
      Transform ctrl = this.FindCtrl(t, (Enum) enum_value);
      this.SetEnemyIconGradeFrame(ctrl, (Enum) QuestAcceptInvitation.UI.SPR_ORDER_RARITY_FRAME, questTableData);
      this.SetQuestData(questTableData, ctrl);
      this.SetPartyData(this.parties[index], t);
      this.SetMemberIcon(t, questTableData);
    }
  }

  private void InitRally(int index, Transform t)
  {
    this.SetEvent(t, "JOIN_MAP", index);
    LoungeModel.SlotInfo rallyInvite = this.rallyInvites[index];
    CharaInfo userInfo = rallyInvite.userInfo;
    FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
    MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(index + 4).SetEquipSet(rallyInvite.userInfo.equipSet);
    this.SetRenderPlayerModel(t, (Enum) QuestAcceptInvitation.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(userInfo, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_NAME, userInfo.name);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_LEVEL, userInfo.level.ToString());
    this.SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower);
    this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
    this.SetPlayingStatus(t, userInfo.userId);
    this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
    ((Component) this.FindCtrl(t, (Enum) QuestAcceptInvitation.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(userInfo.selectedDegrees, false, (Action<DegreePlate>) (x => this.RepositionTable()));
  }

  private void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
  {
    bool is_visible = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_BLACKLIST_ICON, is_visible);
    if (is_visible)
    {
      this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_FOLLOW, false);
      this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_FOLLOWER, false);
    }
    else
    {
      this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_FOLLOW, following);
      this.SetActive(t, (Enum) QuestAcceptInvitation.UI.SPR_FOLLOWER, follower);
    }
  }

  private void SetPlayingStatus(Transform root, int userId)
  {
    this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_LOUNGE, false);
    this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_FIELD, false);
    this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_QUEST, false);
    this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_ARENA, false);
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
    {
      this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_LOUNGE, true);
    }
    else
    {
      LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
      if (loungeMemberStatu == null)
      {
        this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_LOUNGE, true);
      }
      else
      {
        switch (loungeMemberStatu.GetStatus())
        {
          case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_LOUNGE, true);
            break;
          case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
            this.SetQuestInfo(root, loungeMemberStatu.questId);
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.LBL_PLAYING_QUEST, false);
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.LBL_PLAYING_READY, true);
            break;
          case LoungeMemberStatus.MEMBER_STATUS.QUEST:
            this.SetQuestInfo(root, loungeMemberStatu.questId);
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.LBL_PLAYING_QUEST, true);
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.LBL_PLAYING_READY, false);
            break;
          case LoungeMemberStatus.MEMBER_STATUS.FIELD:
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_FIELD, true);
            FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) loungeMemberStatu.fieldMapId);
            if (fieldMapData == null)
            {
              this.SetLabelText(root, (Enum) QuestAcceptInvitation.UI.LBL_AREA_NAME, "");
              break;
            }
            RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
            if (data == null)
            {
              this.SetLabelText(root, (Enum) QuestAcceptInvitation.UI.LBL_AREA_NAME, fieldMapData.mapName);
              break;
            }
            this.SetLabelText(root, (Enum) QuestAcceptInvitation.UI.LBL_AREA_NAME, $"{data.regionName} - {fieldMapData.mapName}");
            break;
          case LoungeMemberStatus.MEMBER_STATUS.ARENA:
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_ARENA, true);
            this.SetActive(root, (Enum) QuestAcceptInvitation.UI.LBL_PLAYING_ARENA, true);
            break;
        }
      }
    }
  }

  private void SetQuestInfo(Transform root, int questId)
  {
    this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_QUEST, true);
    string questText = Singleton<QuestTable>.I.GetQuestData((uint) questId).questText;
    this.SetLabelText(root, (Enum) QuestAcceptInvitation.UI.LBL_QUEST_NAME, questText);
  }

  private string InitGuildDonateInviteObject(int index)
  {
    DonateInvitationInfo guildDonateInvite = this.guildDonateInvites[index];
    string str = "GuildDonateInvitationListItem";
    double num = guildDonateInvite.expired / 1000.0 - this.DateTimeToTimestampSeconds();
    if (guildDonateInvite.itemNum >= guildDonateInvite.quantity)
      str = "GuildDonateInvitationListItemFull";
    else if (num < 1.0)
      str = "GuildDonateInvitationListItemExpired";
    return str;
  }

  private void InitGuildDonateInvite(int index, Transform t)
  {
    DonateInvitationInfo info = this.guildDonateInvites[index];
    if (MonoBehaviourSingleton<GuildManager>.I.guildData.emblem != null && MonoBehaviourSingleton<GuildManager>.I.guildData.emblem.Length >= 3)
    {
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[0]));
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[1]));
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[2]));
    }
    else
    {
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_1, "");
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_2, "");
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_3, "");
    }
    if (info.expired / 1000.0 - this.DateTimeToTimestampSeconds() < 1.0 || info.itemNum >= info.quantity)
      return;
    int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) info.itemId), 1);
    int num = info.itemNum >= info.quantity ? 1 : 0;
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_CHAT_MESSAGE, info.msg);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_USER_NAME, info.nickName);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_MATERIAL_NAME, info.itemName);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_QUATITY, (object) itemNum);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_DONATE_NUM, (object) info.itemNum);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_DONATE_MAX, (object) info.quantity);
    this.SetSliderValue(t, (Enum) QuestAcceptInvitation.UI.SLD_PROGRESS, (float) info.itemNum / (float) info.quantity);
    if (num == 0 && itemNum > 0 && info.itemNum < info.quantity)
      this.SetButtonEvent(t, (Enum) QuestAcceptInvitation.UI.BTN_GIFT, new EventDelegate((EventDelegate.Callback) (() => this.DispatchEvent("SEND_GUILD_DONATE", (object) info.ParseDonateInfo()))));
    else
      this.SetButtonEnabled(t, (Enum) QuestAcceptInvitation.UI.BTN_GIFT, false);
    Transform ctrl = this.FindCtrl(t, (Enum) QuestAcceptInvitation.UI.OBJ_MATERIAL_ICON);
    ItemInfo itemInfo = ItemInfo.CreateItemInfo(new Network.Item()
    {
      uniqId = "0",
      itemId = info.itemId,
      num = info.itemNum
    });
    ItemSortData data = new ItemSortData();
    data.SetItem((object) itemInfo);
    this.SetItemIcon(ctrl, data, ctrl);
  }

  private void InitGuild(int index, Transform t)
  {
    GuildInvitedModel.GuildInvitedInfo guildInvite = this.guildInvites[index];
    if (LoungeMatchingManager.IsValidInLounge())
      this.SetEvent(t, "SELECT_GUILD_LOUNGE", (object) guildInvite);
    else
      this.SetEvent(t, "SELECT_GUILD_HOME", (object) guildInvite);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_GUILD_NAME, guildInvite.name);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_HOST_LV, (object) guildInvite.level);
    if (guildInvite.emblem != null && guildInvite.emblem.Length >= 3)
    {
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(guildInvite.emblem[0]));
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(guildInvite.emblem[1]));
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(guildInvite.emblem[2]));
    }
    else
    {
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_1, "");
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_2, "");
      this.SetSprite(t, (Enum) QuestAcceptInvitation.UI.SPR_EMBLEM_LAYER_3, "");
    }
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_HOST_NAME, guildInvite.admin);
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_USER_INVITE, guildInvite.sender + "'s recruiting");
    this.SetLabelText(t, (Enum) QuestAcceptInvitation.UI.LBL_MEM_NUM, $"{(object) guildInvite.currentMem}/{(object) guildInvite.memCap}");
  }

  private void SetItemIcon(Transform holder, ItemSortData data, Transform parent_scroll)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
    int icon_id = -1;
    if (data != null)
    {
      itemIconType = data.GetIconType();
      icon_id = data.GetIconID();
      rarity = new RARITY_TYPE?(data.GetRarity());
      element = data.GetIconElement();
      magi_enable_icon_type = data.GetIconMagiEnableType();
      data.GetNum();
    }
    bool is_new = false;
    switch (itemIconType)
    {
      case ITEM_ICON_TYPE.NONE:
        int enemy_icon_id = 0;
        if (itemIconType == ITEM_ICON_TYPE.ITEM)
          enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
        ItemIcon itemIcon;
        if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
          itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
          {
            icon_type = data.GetIconType(),
            icon_id = data.GetIconID(),
            rarity = new RARITY_TYPE?(data.GetRarity()),
            parent = holder,
            element = data.GetIconElement(),
            magi_enable_equip_type = data.GetIconMagiEnableType(),
            num = data.GetNum(),
            enemy_icon_id = enemy_icon_id,
            questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
          });
        else
          itemIcon = ItemIcon.Create(itemIconType, icon_id, rarity, holder, element, magi_enable_icon_type, event_name: "DROP", is_new: is_new, enemy_icon_id: enemy_icon_id);
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
        break;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (data.GetUniqID() != 0UL)
        {
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(itemIconType, data.GetUniqID());
          goto case ITEM_ICON_TYPE.NONE;
        }
        goto case ITEM_ICON_TYPE.NONE;
      default:
        is_new = true;
        goto case ITEM_ICON_TYPE.NONE;
    }
  }

  private void OnQuery_JOIN_MAP()
  {
    LoungeMemberStatus loungeMemberStatu = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[this.rallyInvites[(int) GameSection.GetEventData()].userInfo.userId];
    switch (loungeMemberStatu.GetStatus())
    {
      case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
      case LoungeMemberStatus.MEMBER_STATUS.QUEST:
        this.JoinParty(loungeMemberStatu.partyId, loungeMemberStatu.questId);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.FIELD:
        this.JoinField(loungeMemberStatu.fieldMapId);
        break;
    }
  }

  private void JoinField(int fieldMapId)
  {
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
        this.DispatchEvent("CANT_JUMP");
      else if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      {
        if (QuestManager.IsValidInGame())
        {
          MonoBehaviourSingleton<WorldMapManager>.I.SetJumpPortalID(fieldMapData.jumpPortalID);
          MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = true;
          MonoBehaviourSingleton<InGameProgress>.I.QuestToField(fieldMapData.jumpPortalID);
        }
        else
        {
          MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
          MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
        }
      }
      else
      {
        GameSection.StayEvent();
        CoopApp.EnterField(fieldMapData.jumpPortalID, 0U, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
        {
          if (!is_connect)
          {
            GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
            GameSection.ResumeEvent(true);
            MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.DispatchEvent("CLOSE"));
          }
          else
          {
            GameSection.ResumeEvent(is_regist);
            if (!is_regist)
              return;
            MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
          }
        }));
      }
    }
  }

  private void JoinParty(string partyId, int questID)
  {
    if (partyId.Equals(MonoBehaviourSingleton<PartyManager>.I.GetPartyId()))
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
    {
      new EventData("MAIN_MENU_LOUNGE", (object) null),
      new EventData("GACHA_QUEST_COUNTER", (object) null),
      new EventData("JOIN_ROOM", (object) partyId)
    });
  }

  private void SetStamp(Transform root, int stampId)
  {
    if (Singleton<StampTable>.I.GetData((uint) stampId) == null)
      return;
    this.StartCoroutine(this.LoadStamp(root, stampId));
  }

  private IEnumerator LoadStamp(Transform root, int stampId)
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_stamp = load_queue.LoadChatStamp(stampId);
    while (load_queue.IsLoading())
      yield return (object) null;
    if (Object.op_Inequality(lo_stamp.loadedObject, (Object) null))
    {
      Texture2D loadedObject = lo_stamp.loadedObject as Texture2D;
      this.SetActive(root, (Enum) QuestAcceptInvitation.UI.OBJ_SYMBOL, true);
      this.SetTexture(root, (Enum) QuestAcceptInvitation.UI.TEX_STAMP, (Texture) loadedObject);
    }
  }

  private void RepositionTable()
  {
    UITable component = this.GetComponent<UITable>((Enum) QuestAcceptInvitation.UI.TBL_QUEST);
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.Reposition();
    List<Transform> childList = component.GetChildList();
    for (int index = 0; index < childList.Count; ++index)
    {
      Vector3 localPosition = childList[index].localPosition;
      localPosition.x = 0.0f;
      childList[index].localPosition = localPosition;
    }
  }

  protected virtual void OnQuery_SELECT_LOUNGE()
  {
    int eventData = (int) GameSection.GetEventData();
    if (LoungeMatchingManager.IsValidInLounge())
    {
      GameSection.StopEvent();
      if (this.lounges[eventData].id == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.id)
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[5]
      {
        new EventData("LOUNGE", (object) null),
        new EventData("LOUNGE_SETTINGS", (object) null),
        new EventData("EXIT", (object) null),
        new EventData("LOUNGE", (object) null),
        new EventData("FRIEND_INVITED_LOUNGE", (object) this.lounges[eventData].id)
      });
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(this.lounges[eventData].id, (Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
    }
  }

  protected virtual void OnQuery_SELECT_GUILD()
  {
  }

  private void OnCloseDialog_GuildDonateSendDialog() => this.RefreshUI();

  private void OnCloseDialog_GuildInvitedJoinDialog() => this.RefreshUI();

  private double DateTimeToTimestampSeconds()
  {
    return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
  }

  protected new enum UI
  {
    GRD_QUEST,
    LBL_HOST_NAME,
    LBL_HOST_LV,
    TGL_MEMBER_1,
    TGL_MEMBER_2,
    TGL_MEMBER_3,
    LBL_LV,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
    STR_NON_LIST,
    BTN_CONDITION,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    OBJ_ICON,
    OBJ_ICON_NEW,
    OBJ_ICON_CLEARED,
    OBJ_ICON_COMPLETE,
    SPR_ICON_NEW,
    SPR_ICON_CLEARED,
    SPR_ICON_COMPLETE,
    LBL_CONDITION_A,
    LBL_CONDITION_B,
    LBL_CONDITION_DIFFICULTY,
    SPR_CONDITION_DIFFICULTY,
    STR_NO_CONDITION,
    LBL_CONDITION_ENEMY,
    OBJ_NPC_MESSAGE,
    SPR_WINDOW_BASE,
    SPR_ICON_DOUBLE,
    OBJ_SEARCH_INFO_ROOT,
    OBJ_QUEST_INFO_ROOT,
    OBJ_ORDER_QUEST_INFO_ROOT,
    SPR_ORDER_RARITY_FRAME,
    SPR_CHALLENGE_NOT_CLEAR,
    LBL_CHALLENGE_NOT_CLEAR,
    SPR_ICON_DEFENSE_BATTLE,
    LBL_RECRUTING_MEMBERS,
    TBL_QUEST,
    SCR_QUEST,
    LBL_LOUNGE_NAME,
    LBL_LABEL,
    OBJ_SYMBOL,
    TEX_STAMP,
    TGL_L_MEMBER_1,
    TGL_L_MEMBER_2,
    TGL_L_MEMBER_3,
    TGL_L_MEMBER_4,
    TGL_L_MEMBER_5,
    TGL_L_MEMBER_6,
    TGL_L_MEMBER_7,
    TEX_MODEL,
    LBL_NAME,
    LBL_LEVEL,
    SPR_ICON_HOST,
    SPR_ICON_FIRST_MET,
    OBJ_DEGREE_FRAME_ROOT,
    GRD_LIST,
    SPR_BLACKLIST_ICON,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    OBJ_LOUNGE,
    OBJ_FIELD,
    OBJ_QUEST,
    OBJ_ARENA,
    LBL_PLAYING_QUEST,
    LBL_PLAYING_READY,
    LBL_PLAYING_ARENA,
    LBL_AREA_NAME,
    LBL_GUILD_NAME,
    SPR_EMBLEM_LAYER_1,
    SPR_EMBLEM_LAYER_2,
    SPR_EMBLEM_LAYER_3,
    LBL_USER_INVITE,
    LBL_MEM_NUM,
    LBL_USER_NAME,
    LBL_CHAT_MESSAGE,
    LBL_MATERIAL_NAME,
    SLD_PROGRESS,
    OBJ_MATERIAL_ICON,
    LBL_QUATITY,
    OBJ_FULL,
    OBJ_NORMAL,
    LBL_DONATE_NUM,
    LBL_DONATE_MAX,
    BTN_GIFT,
  }
}

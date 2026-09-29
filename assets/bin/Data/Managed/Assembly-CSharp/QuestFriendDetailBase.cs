// Decompiled with JetBrains decompiler
// Type: QuestFriendDetailBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestFriendDetailBase : FriendInfo
{
  protected bool isLoading;
  protected bool reloadModel;
  protected InGameRecorder.PlayerRecord record;
  protected bool isSelfData;
  protected int detailUserID;
  protected EquipSetInfo localEquipSet;
  protected int selfCharaEquipSetNo;
  protected bool isQuestResult;
  protected bool isChangeEquip;
  protected List<int> mSelectedDegrees;

  protected bool AlwaysNowStatusModel { get; private set; }

  protected override bool showMagiButton => !this.IsFriendInfo && this.isSelfData;

  protected override List<int> SelectedDegrees => this.mSelectedDegrees;

  private bool isSelfEventEquipSet
  {
    get
    {
      return this.isSelfData && MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet() && !this.isChangeEquip;
    }
  }

  public override void Initialize()
  {
    this.detailUserID = 0;
    this.isSelfData = false;
    this.isQuestResult = false;
    if (this.record == null)
    {
      this.record = GameSection.GetEventData() as InGameRecorder.PlayerRecord;
      if (this.record != null)
      {
        this.detailUserID = this.record.id != 0 ? this.record.charaInfo.userId : MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
        this.isSelfData = this.detailUserID == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
        this.mSelectedDegrees = this.isSelfData ? MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds : this.record.charaInfo.selectedDegrees;
        if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("InGame"))
          this.isQuestResult = true;
        else if (!this.isChangeEquip && !this.isSelfEventEquipSet)
          this.AlwaysNowStatusModel = true;
      }
    }
    this.selfCharaEquipSetNo = this.isSelfData ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo : -1;
    this.transRoot = this.SetPrefab((Enum) QuestFriendDetailBase.UI.OBJ_EQUIP_SET_ROOT, "FriendInfoBase");
    this.StartCoroutine(this.DoInitialize());
  }

  protected new IEnumerator DoInitialize()
  {
    this.LoadModel();
    while (this.isLoading)
      yield return (object) null;
    GameSection.SetEventData((object) null);
    base.Initialize();
  }

  protected override void OnOpen()
  {
  }

  protected override void LoadModel()
  {
    if (this.record == null)
      return;
    PlayerLoadInfo load_player_info = this.record.playerLoadInfo;
    if (this.isSelfData)
    {
      if (this.reloadModel)
      {
        if (this.isQuestResult)
        {
          load_player_info = PlayerLoadInfo.FromCharaInfo(this.record.charaInfo, true, true, true, this.isVisualMode);
          if (load_player_info.weaponModelID == -1)
          {
            EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(this.selfCharaEquipSetNo);
            EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipSet.item[0].tableID);
            if (equipItemData != null)
            {
              load_player_info.weaponModelID = equipItemData.GetModelID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
              load_player_info.weaponColor0 = equipItemData.modelColor0;
              load_player_info.weaponColor1 = equipItemData.modelColor1;
              load_player_info.weaponColor2 = equipItemData.modelColor2;
              load_player_info.weaponEffectID = (int) equipItemData.effectID;
              load_player_info.weaponEffectColor = equipItemData.effectColor;
              load_player_info.weaponEffectParam = equipItemData.effectParam;
              load_player_info.weaponSpAttackType = (uint) equipItemData.spAttackType;
            }
          }
          else
          {
            load_player_info.weaponModelID = this.record.playerLoadInfo.weaponModelID;
            load_player_info.weaponColor0 = this.record.playerLoadInfo.weaponColor0;
            load_player_info.weaponColor1 = this.record.playerLoadInfo.weaponColor1;
            load_player_info.weaponColor2 = this.record.playerLoadInfo.weaponColor2;
            load_player_info.weaponEffectID = this.record.playerLoadInfo.weaponEffectID;
            load_player_info.weaponEffectColor = this.record.playerLoadInfo.weaponEffectColor;
            load_player_info.weaponEffectParam = this.record.playerLoadInfo.weaponEffectParam;
            load_player_info.weaponSpAttackType = this.record.playerLoadInfo.weaponSpAttackType;
          }
          this.record.animID = -1;
        }
        else
          load_player_info = PlayerLoadInfo.FromUserStatus(true, this.isVisualMode, this.selfCharaEquipSetNo);
      }
      else if (this.AlwaysNowStatusModel || this.IsNullWeaponSloat(this.record.playerLoadInfo.weaponModelID))
      {
        this.record.playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, this.isVisualMode);
        this.record.animID = -1;
        load_player_info = this.record.playerLoadInfo;
      }
    }
    else if (this.isVisualMode)
    {
      load_player_info = this.record.playerLoadInfo;
    }
    else
    {
      load_player_info = PlayerLoadInfo.FromCharaInfo(this.record.charaInfo, true, true, true, this.isVisualMode);
      load_player_info.weaponModelID = this.record.playerLoadInfo.weaponModelID;
      load_player_info.weaponColor0 = this.record.playerLoadInfo.weaponColor0;
      load_player_info.weaponColor1 = this.record.playerLoadInfo.weaponColor1;
      load_player_info.weaponColor2 = this.record.playerLoadInfo.weaponColor2;
      load_player_info.weaponEffectID = this.record.playerLoadInfo.weaponEffectID;
      load_player_info.weaponEffectColor = this.record.playerLoadInfo.weaponEffectColor;
      load_player_info.weaponEffectParam = this.record.playerLoadInfo.weaponEffectParam;
      load_player_info.weaponSpAttackType = this.record.playerLoadInfo.weaponSpAttackType;
    }
    this.SetRenderPlayerModel(load_player_info);
  }

  protected virtual bool IsNullWeaponSloat(int id) => id == -1;

  protected void SetRenderPlayerModel(PlayerLoadInfo load_player_info)
  {
    this.SetRenderPlayerModel(this.transRoot, (Enum) QuestFriendDetailBase.UI.TEX_MODEL, load_player_info, this.record.animID, new Vector3(0.0f, -0.75f, 14f), new Vector3(0.0f, 180f, 0.0f), this.isVisualMode, (Action<PlayerLoader>) (player_loader =>
    {
      if (Object.op_Inequality((Object) player_loader, (Object) null))
        this.loader = player_loader;
      if (!Object.op_Inequality((Object) this.loader, (Object) null) || !Object.op_Inequality((Object) this.loader.animator, (Object) null))
        return;
      if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      {
        if (!MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
          return;
        this.loader.animator.Play(this.loader.GetWinLoopMotionState());
      }
      else
        PlayerAnimCtrl.Get(this.loader.animator, PlayerAnimCtrl.battleAnims[this.record.playerLoadInfo.weaponModelID / 1000]);
    }));
  }

  protected override void UpdateUserIDLabel()
  {
    if (!this.isSelfData)
    {
      bool is_visible = !this.record.isNPC;
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_USER_ID_ROOT, is_visible);
      if (!is_visible)
        return;
      this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_USER_ID, this.record.charaInfo.code);
    }
    else
      this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_USER_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
  }

  public override int GetCharaSex() => this.record.charaInfo.sex;

  public virtual void SetupCommentText()
  {
    bool is_visible = !this.record.isNPC;
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_COMMENT, is_visible);
    if (!is_visible)
      return;
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_COMMENT, this.record.charaInfo.comment);
  }

  public virtual void SetupFollowButton()
  {
    bool isNpc = this.record.isNPC;
    QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(this.detailUserID);
    if (this.record.isSelf | isNpc || userInfo == null)
    {
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, false);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, false);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_BLACKLIST_ROOT, false);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_FOLLOW_ARROW_ROOT, true);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_FOLLOWER_ARROW, false);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_FOLLOW_ARROW, false);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_BLACKLIST_ICON, false);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_SAME_CLAN_ICON, true);
    }
    else
    {
      bool following = !userInfo.CanSendFollow;
      bool isFollower = userInfo.IsFollower;
      this.SetEvent(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, "FOLLOW", 0);
      if (MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow && !following)
      {
        this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, true);
        this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, false);
        this.SetEvent(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
      }
      else
      {
        bool flag = !this.record.isNPC;
        this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, flag && !following);
        this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, flag && following);
      }
      bool flag1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(this.detailUserID);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_BLACKLIST_ROOT, true);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_BLACKLIST_IN, !flag1);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_BLACKLIST_OUT, flag1);
      bool same_clan_user = false;
      if (this.record != null && this.record.charaInfo != null && this.record.charaInfo.userClanData != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
        same_clan_user = this.record.charaInfo.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
      this.SetFollowStatus(following, isFollower, flag1, same_clan_user);
    }
  }

  protected virtual void SetupLastLogin()
  {
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_LAST_LOGIN, false);
  }

  public override void UpdateUI()
  {
    this.localEquipSet = !this.isSelfData || this.isSelfEventEquipSet ? MonoBehaviourSingleton<StatusManager>.I.CreateEquipSetData(this.record.charaInfo.equipSet) : MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(this.selfCharaEquipSetNo);
    this.OnUpdateFriendDetailUI();
  }

  protected void OnUpdateFriendDetailUI()
  {
    int num1;
    int num2;
    int hp;
    int level;
    if (!this.record.isSelf || MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
    {
      if (this.record.isNPC)
      {
        num1 = (int) this.record.charaInfo.atk;
        num2 = (int) this.record.charaInfo.def;
        hp = (int) this.record.charaInfo.hp;
      }
      else
      {
        EquipSetCalculator equipSetCalculator;
        if (MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex == -1)
        {
          MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = 0;
          equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
          equipSetCalculator.SetEquipSet(this.record.charaInfo.equipSet);
        }
        else
          equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
        SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) this.record.charaInfo.hp, (int) this.record.charaInfo.atk, (int) this.record.charaInfo.def);
        num1 = finalStatus.GetAttacksSum();
        num2 = finalStatus.GetDefencesSum();
        hp = finalStatus.hp;
      }
      level = (int) this.record.charaInfo.level;
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_LEVEL_ROOT, !this.record.isNPC);
    }
    else
    {
      SimpleStatus finalStatus = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(this.selfCharaEquipSetNo).GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
      num1 = finalStatus.GetAttacksSum();
      num2 = finalStatus.GetDefencesSum();
      hp = finalStatus.hp;
      level = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
    }
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_ATK, num1.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_DEF, num2.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_HP, hp.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_LEVEL, level.ToString());
    this.SetupInfo();
    this.UpdateEquipIcon((List<CharaInfo.EquipItem>) null);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_MAGI, this.showMagiButton);
    this.CreateDegree();
    this.SetMoveMessageButton();
    if (this.record != null && this.record.charaInfo != null && this.record.charaInfo.userClanData != null)
      this.UpdateClanInfo(this.record.charaInfo);
    else
      this.DisableClanInfo();
  }

  protected void SetupInfo()
  {
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_FRIEND_INFO_ROOT, this.IsFriendInfo);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_CHANGE_EQUIP_INFO_ROOT, !this.IsFriendInfo);
    if (!this.IsFriendInfo)
      return;
    this.UpdateUserIDLabel();
    CharaInfo.ClanInfo clanInfo = this.record.charaInfo.clanInfo;
    if (clanInfo == null)
    {
      clanInfo = new CharaInfo.ClanInfo();
      clanInfo.clanId = -1;
      clanInfo.tag = string.Empty;
    }
    bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
    this.SetSupportEncoding(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_NAME, true);
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, this.record.charaInfo.name, this.record.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
    this.SetupCommentText();
    this.SetupLastLogin();
    this.SetupFollowButton();
  }

  protected override void UpdateEquipIcon(List<CharaInfo.EquipItem> equip_set_info)
  {
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_CHANGE_MODE, this.isVisualMode);
    int index1 = 0;
    for (int index2 = 7; index1 < index2; ++index1)
    {
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons[index1]), "EMPTY", 0);
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[index1]), "EMPTY", 0);
      this.SetLabelText(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index1]), string.Empty);
    }
    bool flag1 = this.isVisualMode;
    bool flag2 = this.isVisualMode;
    bool flag3 = this.isVisualMode;
    bool flag4 = this.isVisualMode;
    int event_data = 0;
    for (int length = this.localEquipSet.item.Length; event_data < length; ++event_data)
    {
      int num = -1;
      EquipItemInfo equipItemInfo = this.localEquipSet.item[event_data];
      EquipItemTable.EquipItemData equipItemData = (EquipItemTable.EquipItemData) null;
      if (equipItemInfo != null)
      {
        switch (equipItemInfo.tableData.type)
        {
          case EQUIPMENT_TYPE.ARMOR:
            flag2 = false;
            break;
          case EQUIPMENT_TYPE.HELM:
            flag1 = false;
            break;
          case EQUIPMENT_TYPE.ARM:
            flag3 = false;
            break;
          case EQUIPMENT_TYPE.LEG:
            flag4 = false;
            break;
        }
        equipItemData = !this.isVisualMode ? Singleton<EquipItemTable>.I.GetEquipItemData(equipItemInfo.tableID) : this.GetVisualModeTargetTable(equipItemInfo.tableData.id, equipItemInfo.tableData.type, this.record.charaInfo);
      }
      if (this.isVisualMode)
      {
        if (equipItemData != null)
        {
          num = equipItemData.GetIconID(this.GetCharaSex());
          this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons_level[event_data]), false);
        }
      }
      else if (equipItemInfo != null && equipItemInfo.tableID != 0U)
      {
        num = equipItemData.GetIconID(this.GetCharaSex());
        this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons_level[event_data]), true);
        string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) equipItemInfo.level.ToString());
        this.SetLabelText(this.FindCtrl(this.transRoot, (Enum) this.icons_level[event_data]), text);
      }
      Transform ctrl = this.FindCtrl(this.transRoot, (Enum) this.icons[event_data]);
      ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, this.GetCharaSex(), ctrl, event_name: "EQUIP", event_data: event_data);
      this.SetLongTouch(iconByEquipItemInfo.transform, "DETAIL", (object) event_data);
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[event_data]), "DETAIL", event_data);
      this.SetEvent(iconByEquipItemInfo.transform, "DETAIL", event_data);
      ((Component) iconByEquipItemInfo).gameObject.SetActive(num != -1);
      if (num != -1)
        iconByEquipItemInfo.SetEquipExtInvertedColor(equipItemInfo, this.GetComponent<UILabel>(this.transRoot, (Enum) this.icons_level[event_data]));
    }
    if (flag1 && this.record.charaInfo.hId != 0)
      this.SetVisualModeIcon(4, this.record.charaInfo.hId, EQUIPMENT_TYPE.HELM, this.record.charaInfo);
    if (flag2 && this.record.charaInfo.aId != 0)
      this.SetVisualModeIcon(3, this.record.charaInfo.aId, EQUIPMENT_TYPE.ARMOR, this.record.charaInfo);
    if (flag3 && this.record.charaInfo.rId != 0)
      this.SetVisualModeIcon(5, this.record.charaInfo.rId, EQUIPMENT_TYPE.ARM, this.record.charaInfo);
    if (!flag4 || this.record.charaInfo.lId == 0)
      return;
    this.SetVisualModeIcon(6, this.record.charaInfo.lId, EQUIPMENT_TYPE.LEG, this.record.charaInfo);
  }

  protected override void OnQuery_DETAIL()
  {
    if (this.isVisualMode)
    {
      GameSection.ChangeEvent("VISUAL_DETAIL");
      this.OnQuery_VISUAL_DETAIL();
    }
    else
    {
      int eventData = (int) GameSection.GetEventData();
      if (this.localEquipSet.item[eventData] == null)
        GameSection.StopEvent();
      else if (this.isSelfData && !this.isSelfEventEquipSet)
        GameSection.SetEventData((object) this.CreateSelfEventData(eventData));
      else
        GameSection.SetEventData((object) new object[4]
        {
          (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
          (object) this.GetEquipSetAttachSkillListData(this.record.charaInfo.equipSet)[eventData],
          (object) this.record.charaInfo.sex,
          (object) this.record.charaInfo.faceId
        });
    }
  }

  protected object[] CreateSelfEventData(int index)
  {
    return new object[4]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
      (object) this.GetEquipSetAttachSkillListData(this.selfCharaEquipSetNo)[index],
      (object) this.record.charaInfo.sex,
      (object) this.record.charaInfo.faceId
    };
  }

  protected override void OnQuery_SKILL_LIST()
  {
    if (this.isSelfData)
      GameSection.SetEventData((object) new object[4]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
        (object) this.GetEquipSetAttachSkillListData(this.selfCharaEquipSetNo),
        (object) true,
        (object) this.record.charaInfo.sex
      });
    else
      GameSection.SetEventData((object) new object[4]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
        (object) this.GetEquipSetAttachSkillListData(this.record.charaInfo.equipSet),
        (object) true,
        (object) this.record.charaInfo.sex
      });
  }

  protected override void OnQuery_ABILITY()
  {
    List<CharaInfo.EquipItem> equipSet = !this.isSelfData || this.isSelfEventEquipSet ? this.record.charaInfo.equipSet : (List<CharaInfo.EquipItem>) null;
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.localEquipSet,
      (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(this.localEquipSet),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) this.record.charaInfo.atk, (int) this.record.charaInfo.def, (int) this.record.charaInfo.hp, equipSet)
    });
  }

  protected override void OnQuery_STATUS()
  {
    List<CharaInfo.EquipItem> equipSet = !this.isSelfData || this.isSelfEventEquipSet ? this.record.charaInfo.equipSet : (List<CharaInfo.EquipItem>) null;
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.localEquipSet,
      (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(this.localEquipSet),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) this.record.charaInfo.atk, (int) this.record.charaInfo.def, (int) this.record.charaInfo.hp, equipSet)
    });
  }

  protected override void OnQuery_FOLLOW()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.record.charaInfo.name
    });
    List<int> send_follow_list = new List<int>();
    send_follow_list.Add(this.record.charaInfo.userId);
    if (this.isQuestResult)
    {
      this.SendFollow(send_follow_list, (Action<bool>) (is_success =>
      {
        if (!MonoBehaviourSingleton<CoopApp>.IsValid())
          return;
        CoopApp.UpdateField();
      }));
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<PartyManager>.I.SendFollowAgency(send_follow_list, (Action<bool>) (is_success =>
      {
        if (this.isQuestResult & is_success)
          MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(this.record.charaInfo.userId, true);
        GameSection.ResumeEvent(is_success);
      }));
    }
  }

  protected override void OnQuery_UNFOLLOW()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.record.charaInfo.name
    });
  }

  protected virtual void OnQuery_QuestResultFriendUnFollow_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.record.charaInfo.name
    });
    if (this.isQuestResult)
    {
      this.SendUnFollow(this.record.charaInfo.userId, (Action<bool>) (is_success => { }));
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<PartyManager>.I.SendUnFollowAgency(this.record.charaInfo.userId, (Action<bool>) (is_success =>
      {
        if (this.isQuestResult & is_success)
          MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(this.record.charaInfo.userId, false);
        GameSection.ResumeEvent(is_success);
      }));
    }
  }

  protected override void OnQuery_BLACK_LIST_IN()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.record.charaInfo.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<BlackListManager>.I.SendAdd(this.record.charaInfo.userId, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected override void OnQuery_BLACK_LIST_OUT()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.record.charaInfo.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<BlackListManager>.I.SendDelete(this.record.charaInfo.userId, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected override void OnQuery_CHANGE_MODE()
  {
    this.reloadModel = true;
    base.OnQuery_CHANGE_MODE();
  }

  protected override void OnQuery_SECTION_BACK()
  {
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = -1;
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
  }

  protected new enum UI
  {
    LBL_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_COMMENT,
    LBL_COMMENT,
    OBJ_LAST_LOGIN,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_LEVEL,
    OBJ_LEVEL_ROOT,
    LBL_USER_ID,
    OBJ_USER_ID_ROOT,
    TEX_MODEL,
    BTN_FOLLOW,
    BTN_UNFOLLOW,
    OBJ_BLACKLIST_ROOT,
    BTN_BLACKLIST_IN,
    BTN_BLACKLIST_OUT,
    OBJ_ICON_WEAPON_1,
    OBJ_ICON_WEAPON_2,
    OBJ_ICON_WEAPON_3,
    OBJ_ICON_ARMOR,
    OBJ_ICON_HELM,
    OBJ_ICON_ARM,
    OBJ_ICON_LEG,
    BTN_ICON_WEAPON_1,
    BTN_ICON_WEAPON_2,
    BTN_ICON_WEAPON_3,
    BTN_ICON_ARMOR,
    BTN_ICON_HELM,
    BTN_ICON_ARM,
    BTN_ICON_LEG,
    OBJ_EQUIP_ROOT,
    OBJ_EQUIP_SET_ROOT,
    OBJ_FRIEND_INFO_ROOT,
    OBJ_CHANGE_EQUIP_INFO_ROOT,
    LBL_MAX,
    LBL_NOW,
    OBJ_FOLLOW_ARROW_ROOT,
    SPR_FOLLOW_ARROW,
    SPR_FOLLOWER_ARROW,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_WEAPON_2,
    LBL_LEVEL_WEAPON_3,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    LBL_CHANGE_MODE,
    BTN_MAGI,
    LBL_SET_NAME,
    OBJ_DEGREE_PLATE_ROOT,
    BTN_DELETEFOLLOWER,
    BTN_MOVE_TO_MSG,
    OBJ_CLAN_ROOT,
    BTN_CLAN_SCOUT,
    SPR_CLAN_SCOUT,
    BTN_CLAN_DETAIL,
    TXT_CLAN_TITLE,
    SPR_CLAN_NAME,
    BTN_CLAN_SCOUT_CANCEL,
    BTN_CLAN_SCOUT_OFF,
  }
}

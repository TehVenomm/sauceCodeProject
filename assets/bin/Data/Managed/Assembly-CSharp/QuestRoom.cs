// Decompiled with JetBrains decompiler
// Type: QuestRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestRoom : GameSection
{
  private object[] eventData;
  private QuestTable.QuestTableData questData;
  private bool openAfterUpdate;
  private QuestRoomObserver observer;
  private PARTY_STATUS partyStatus;
  private bool isExplore;
  private bool isRush;
  private bool isWaveMatch;
  private bool isWaveMatchEvent;
  private bool isRandomSearch;
  private bool canShowRepeatQuest;
  private IEnumerator preDownloadCoroutine;
  private bool goToInGame;
  private QuestRoom.UI[] weaponIcon = new QuestRoom.UI[3]
  {
    QuestRoom.UI.SPR_WEAPON_1,
    QuestRoom.UI.SPR_WEAPON_2,
    QuestRoom.UI.SPR_WEAPON_3
  };
  private static readonly string[] ITEM_TYPE_ICON_SPRITE_NAME = new string[5]
  {
    "Sword",
    "Brade",
    "Lance",
    "Edge",
    "Arrow"
  };
  private QuestRoom.RoomUserModelInfo[] roomUserModelInfo;
  private QuestRoom.ChatBalloon[] balloons;
  private int[] chatBalloonDepth;
  private const int ROOM_MEMBER_MAX = 4;
  private int eSetNo;
  private int selfUserId;
  private bool isTutorialRoom;
  private CharaInfo[] npcData = new CharaInfo[3];
  private bool[] loadNPC;
  private PARTY_PLAYER_STATUS[] partyPlayerStatus = new PARTY_PLAYER_STATUS[4];
  private bool section_back_event;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "FieldMapTable";
    }
  }

  public override void Initialize()
  {
    this.selfUserId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    MonoBehaviourSingleton<StatusManager>.I.InitUniqueEquip();
    this.eventData = GameSection.GetEventData() as object[];
    bool from_search_section = this.eventData != null && this.eventData[0] is bool;
    bool is_entry_pass = from_search_section && (bool) this.eventData[0];
    if (MonoBehaviourSingleton<PartyManager>.I.randomMatchingInfo != null)
      this.isRandomSearch = MonoBehaviourSingleton<PartyManager>.I.randomMatchingInfo.usedSearchRandomMatching;
    this.observer = ((Component) this).gameObject.AddComponent<QuestRoomObserver>().Initialize(from_search_section, is_entry_pass, (Action<string>) (dispatch_event_name => this.DispatchEvent(dispatch_event_name)), (Action<string>) (change_event_name => GameSection.ChangeEvent(change_event_name)), (System.Action) (() => GameSection.StayEvent()), (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)), new bool?(true));
    if (PartyManager.IsValidInParty())
    {
      MonoBehaviourSingleton<StatusManager>.I.SetupEventEquipSet(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
      this.questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
      PartyModel.QuestInfo quest = MonoBehaviourSingleton<PartyManager>.I.partyData.quest;
      this.isExplore = quest.explore != null;
      this.isRush = quest.rush != null;
      if (this.questData != null)
      {
        this.isWaveMatch = this.questData.questType == QUEST_TYPE.WAVE || this.questData.questType == QUEST_TYPE.WAVE_STRATEGY;
        this.isWaveMatchEvent = this.questData.questType == QUEST_TYPE.EVENT_WAVE || this.questData.questType == QUEST_TYPE.EVENT_WAVE_STRATEGY;
        this.canShowRepeatQuest = this.questData.questType == QUEST_TYPE.ORDER;
      }
      this.preDownloadCoroutine = this.StartPredownload();
      this.StartCoroutine(this.preDownloadCoroutine);
    }
    else
      MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
    this.SetActive((Enum) QuestRoom.UI.OBJ_QUEST_INFO, !this.isExplore && !this.isRush && !this.isWaveMatch && !this.isWaveMatchEvent);
    this.SetActive((Enum) QuestRoom.UI.OBJ_EXPLORE_INFO, this.isExplore);
    this.SetActive((Enum) QuestRoom.UI.OBJ_RUSH_INFO, this.isRush);
    this.SetActive((Enum) QuestRoom.UI.OBJ_WAVE_MATCH_INFO, this.isWaveMatch || this.isWaveMatchEvent);
    this.roomUserModelInfo = new QuestRoom.RoomUserModelInfo[4];
    for (int index = 0; index < 4; ++index)
      this.roomUserModelInfo[index] = new QuestRoom.RoomUserModelInfo();
    this.balloons = new QuestRoom.ChatBalloon[4];
    for (int index = 0; index < 4; ++index)
      this.balloons[index] = new QuestRoom.ChatBalloon((MonoBehaviour) this);
    this.chatBalloonDepth = new int[4];
    this.eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    this.partyStatus = PARTY_STATUS.NONE;
    this.InitializeChatUI();
    if (MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
    {
      this.isTutorialRoom = true;
      int[] numArray = new int[3]{ 2000, 2001, 2002 };
      this.loadNPC = new bool[4];
      for (int index = 0; index < 3; ++index)
      {
        NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(numArray[index]);
        this.npcData[index] = new CharaInfo();
        CharaInfo info = this.npcData[index];
        npcData.CopyCharaInfo(info);
      }
      this.canShowRepeatQuest = false;
    }
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.enableRepeatQuest)
      this.canShowRepeatQuest = false;
    if (!MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable)
      this.canShowRepeatQuest = false;
    if (this.canShowRepeatQuest && this.selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
      this.StartCoroutine(this.SetDeaultRepeat());
    else
      base.Initialize();
  }

  private IEnumerator SetDeaultRepeat()
  {
    MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest = GameSaveData.instance.defaultRepeatPartyOn;
    bool wait = true;
    MonoBehaviourSingleton<PartyManager>.I.SendRepeat(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest, (Action<bool>) (is_success =>
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
      wait = false;
    }));
    while (wait)
      yield return (object) null;
    base.Initialize();
  }

  public override void Exit()
  {
    if (this.goToInGame)
    {
      base.Exit();
    }
    else
    {
      MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
      this.StartCoroutine(this.DoExit());
    }
  }

  private IEnumerator DoExit()
  {
    if (PartyManager.IsValidInParty())
    {
      bool wait = true;
      MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (b => wait = false));
      while (wait)
        yield return (object) null;
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInLounge();
      if (ClanMatchingManager.IsValidInClan())
        MonoBehaviourSingleton<ClanMatchingManager>.I.SendInClanBase();
    }
    base.Exit();
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<ChatManager>.IsValid())
    {
      if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
      {
        MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveText -= new ChatRoom.OnReceiveText(this.OnReceiveChatText);
        MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveStamp -= new ChatRoom.OnReceiveStamp(this.OnReceiveChatStamp);
      }
      if (this.goToInGame)
        MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
      else
        MonoBehaviourSingleton<ChatManager>.I.DestroyRoomChat();
    }
    if (this.preDownloadCoroutine != null)
      this.StopCoroutine(this.preDownloadCoroutine);
    base.OnDestroy();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestRoom.UI.BTN_BACK_2, false);
    if (!PartyManager.IsValidInParty() || this.questData == null)
      return;
    if (this.partyStatus == PARTY_STATUS.NONE)
      this.partyStatus = MonoBehaviourSingleton<PartyManager>.I.GetStatus();
    if (this.partyStatus == PARTY_STATUS.WAITING && MonoBehaviourSingleton<PartyManager>.I.GetStatus() >= PARTY_STATUS.PLAYING)
      return;
    this.SetLabelText((Enum) QuestRoom.UI.LBL_QUEST_NAME, this.questData.questText);
    QuestTable.QuestTableData questTableData = (QuestTable.QuestTableData) null;
    int mainEnemyId;
    if (this.isExplore)
    {
      int mainQuestId = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.mainQuestId;
      questTableData = Singleton<QuestTable>.I.GetQuestData((uint) mainQuestId);
      mainEnemyId = questTableData.GetMainEnemyID();
    }
    else
      mainEnemyId = this.questData.GetMainEnemyID();
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId);
    int elen_type = 6;
    if (enemyData != null)
      elen_type = (int) enemyData.weakElement;
    this.SetElementSprite((Enum) QuestRoom.UI.SPR_WEAK, elen_type);
    this.SetActive((Enum) QuestRoom.UI.LBL_NON_WEAK, elen_type == 6);
    if (this.isExplore)
    {
      this.SetLabelText((Enum) QuestRoom.UI.LBL_EXPLORE_NAME, questTableData.questText);
      this.SetElementSprite((Enum) QuestRoom.UI.SPR_WEAK_EXPLORE, elen_type);
      this.SetActive((Enum) QuestRoom.UI.LBL_NON_WEAK_EXPLORE, elen_type == 6);
      ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.GetCtrl((Enum) QuestRoom.UI.OBJ_ENEMY)).SetDepth(7);
      this.SetElementSprite((Enum) QuestRoom.UI.SPR_ENM_ELEMENT, (int) enemyData.element);
      int limitTime = (int) questTableData.limitTime;
      this.SetLabelText((Enum) QuestRoom.UI.LBL_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
    }
    if (this.isRush)
    {
      Transform ctrl = this.GetCtrl((Enum) QuestRoom.UI.TEX_RUSH_ICON);
      if (Object.op_Inequality((Object) ctrl, (Object) null))
      {
        UITexture component = ((Component) ctrl).GetComponent<UITexture>();
        if (Object.op_Inequality((Object) component, (Object) null))
          ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int) this.questData.rushIconId));
      }
      int limitTime = (int) this.questData.limitTime;
      this.SetLabelText((Enum) QuestRoom.UI.LBL_RUSH_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
      this.SetLabelText((Enum) QuestRoom.UI.LBL_RUSH_NAME, this.questData.questText);
      this.SetActive((Enum) QuestRoom.UI.LBL_RUSH_FLOORNUMBER, false);
    }
    if (this.isWaveMatch || this.isWaveMatchEvent)
    {
      int limitTime = (int) this.questData.limitTime;
      this.SetLabelText((Enum) QuestRoom.UI.LBL_WM_TITLE, this.questData.questText);
      this.SetLabelText((Enum) QuestRoom.UI.LBL_WM_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
      this.SetActive((Enum) QuestRoom.UI.LBL_WM_TYPE, this.isWaveMatch);
      this.SetActive((Enum) QuestRoom.UI.LBL_WM_EVENT_TYPE, this.isWaveMatchEvent);
    }
    DeliveryTable.DeliveryData deliveryData = (DeliveryTable.DeliveryData) null;
    if (this.isExplore)
      deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questTableData.questID);
    else if (this.isRush || this.isWaveMatch || this.isWaveMatchEvent)
      deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(this.questData.questID);
    this.SetActive((Enum) QuestRoom.UI.SPR_TYPE_DIFFICULTY, deliveryData != null && deliveryData.difficulty >= DIFFICULTY_MODE.HARD);
    this.SetLabelText((Enum) QuestRoom.UI.LBL_ROOM_ID, MonoBehaviourSingleton<PartyManager>.I.GetPartyNumber());
    this.SetGrid((Enum) QuestRoom.UI.GRD_PLAYER_INFO, "", 4, false, (Func<int, Transform, Transform>) ((i, t) =>
    {
      Transform t1 = this.Realizes(i != MonoBehaviourSingleton<PartyManager>.I.GetSlotIndex(this.selfUserId) ? "QuestRoomUserInfo" : "QuestRoomUserInfoSelf", t, false);
      this.SetupChatBalloon(t1, i);
      return t1;
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.UpdateRoomUserInfo(t, i);
      this.SetEvent(t, (Enum) QuestRoom.UI.BTN_NAME_BG, "MEMBER_DETAIL", i);
      this.SetEvent(t, (Enum) QuestRoom.UI.BTN_FRAME, "MEMBER_DETAIL", i);
      this.UpdateChatBalloon(i);
    }));
    bool is_active1 = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(this.selfUserId).status == 21;
    int num = this.selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId() ? 1 : 0;
    bool is_visible1 = MonoBehaviourSingleton<PartyManager>.I.GetStatus() >= PARTY_STATUS.PLAYING;
    if (num != 0)
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_READY, false);
      this.SetActive((Enum) QuestRoom.UI.BTN_READY_BACK, false);
      this.SetActive((Enum) QuestRoom.UI.BTN_OWNER_READY, true);
      this.SetActive((Enum) QuestRoom.UI.BTN_INVITE, true);
      bool is_visible2 = MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.partySetting != null && MonoBehaviourSingleton<PartyManager>.I.partySetting.isLock;
      this.SetActive((Enum) QuestRoom.UI.BTN_CHANGE_PUBLIC, is_visible2);
      this.SetActive((Enum) QuestRoom.UI.BTN_CHANGE_PUBLIC_OFF, !is_visible2);
    }
    else
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_READY, !is_visible1);
      this.SetActive((Enum) QuestRoom.UI.BTN_READY_BACK, !is_visible1);
      this.SetActive((Enum) QuestRoom.UI.BTN_OWNER_READY, is_visible1);
      if (!is_visible1)
        this.SetToggleButton((Enum) QuestRoom.UI.TGL_READY, is_active1, (Action<bool>) (is_active =>
        {
          if (is_active)
            this.DispatchEvent("READY");
          else
            this.DispatchEvent("READY_BACK");
        }));
      this.SetActive((Enum) QuestRoom.UI.BTN_INVITE, false);
      this.SetActive((Enum) QuestRoom.UI.BTN_CHANGE_PUBLIC, false);
      this.SetActive((Enum) QuestRoom.UI.BTN_CHANGE_PUBLIC_OFF, false);
    }
    if (this.questData.questType == QUEST_TYPE.GATE)
    {
      string format = StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 0U);
      string text = "";
      if (enemyData != null)
        text = string.Format(format, (object) this.questData.GetMainEnemyLv(), (object) enemyData.name);
      this.SetLabelText((Enum) QuestRoom.UI.LBL_QUEST_NAME, text);
    }
    this.openAfterUpdate = false;
    if (this.canShowRepeatQuest)
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
    }
    else
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_OFF, false);
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_ON, false);
    }
  }

  private void UpdateRoomUserInfo(Transform trans, int index)
  {
    QuestRoomUserInfo component = ((Component) trans).GetComponent<QuestRoomUserInfo>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    PartyModel.SlotInfo slotInfoByIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(index);
    CharaInfo org = slotInfoByIndex?.userInfo;
    QuestRoom.RoomUserModelInfo.ROOM_USER_STATE state;
    if (slotInfoByIndex == null || org == null)
    {
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_NAME, string.Empty);
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_LV, string.Empty);
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_ATK, string.Empty);
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_DEF, string.Empty);
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_HP, string.Empty);
      this.SetActive(trans, (Enum) QuestRoom.UI.SPR_WEAPON_1, false);
      this.SetActive(trans, (Enum) QuestRoom.UI.SPR_WEAPON_2, false);
      this.SetActive(trans, (Enum) QuestRoom.UI.SPR_WEAPON_3, false);
      state = QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.EMPTY;
      if (this.isTutorialRoom)
      {
        if (this.npcData[index - 1] != null && !this.loadNPC[index])
        {
          this.loadNPC[index] = true;
          component.DeleteModel();
          component.LoadModel(index, this.npcData[index - 1]);
        }
        this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_NAME, "NPC " + (object) index);
        state = QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.NONE;
      }
      else
        component.LoadModel(index, (CharaInfo) null);
    }
    else
    {
      if (this.isTutorialRoom)
        this.loadNPC[index] = false;
      if (org.userId == this.selfUserId && this.eSetNo != MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo)
      {
        this.eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
        StageObjectManager.CreatePlayerInfo createPlayerInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo();
        if (createPlayerInfo != null)
          org = createPlayerInfo.charaInfo;
      }
      if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
        AssignedEquipmentTable.MergeAssignedEquip(ref org, MonoBehaviourSingleton<StatusManager>.I.EventEquipSet);
      int ownerUserId = MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
      bool flag1 = org.userId == ownerUserId;
      bool flag2 = slotInfoByIndex.status == 21;
      int num1 = slotInfoByIndex.status == 30 ? 1 : 0;
      bool flag3 = MonoBehaviourSingleton<PartyManager>.I.IsEquipChangeByIndex(index);
      state = num1 == 0 ? (!flag1 ? (!flag3 ? (!flag2 ? QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.STAY : QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.READY) : QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.EQUIP_CHANGE) : (!flag3 ? QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.NONE : QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.EQUIP_CHANGE)) : QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.BATTLE;
      this.SetActive(trans, (Enum) QuestRoom.UI.SPR_WEAPON_1, false);
      this.SetActive(trans, (Enum) QuestRoom.UI.SPR_WEAPON_2, false);
      this.SetActive(trans, (Enum) QuestRoom.UI.SPR_WEAPON_3, false);
      int weapon_index = 0;
      org.equipSet.ForEach((Action<CharaInfo.EquipItem>) (data =>
      {
        if (data == null)
          return;
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) data.eId);
        if (equipItemData == null || !equipItemData.IsWeapon())
          return;
        this.SetActive(trans, (Enum) this.weaponIcon[weapon_index], true);
        int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemData.type);
        this.SetSprite(trans, (Enum) this.weaponIcon[weapon_index], QuestRoom.ITEM_TYPE_ICON_SPRITE_NAME[equipmentTypeIndex]);
        ++weapon_index;
      }));
      EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(index);
      int num2 = this.roomUserModelInfo[index].userID != org.userId ? 1 : 0;
      bool flag4 = !component.IsAllSameEquip(org);
      int num3 = flag4 ? 1 : 0;
      if ((num2 | num3) != 0 || this.openAfterUpdate)
      {
        if (flag4)
          equipSetCalculator.SetEquipSet(org.equipSet);
        component.DeleteModel();
        component.LoadModel(index, org);
      }
      SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) org.hp, (int) org.atk, (int) org.def);
      Transform root1 = trans;
      // ISSUE: variable of a boxed type
      __Boxed<QuestRoom.UI> label_enum1 = (Enum) QuestRoom.UI.LBL_ATK;
      int num4 = finalStatus.GetAttacksSum();
      string text1 = num4.ToString();
      this.SetLabelText(root1, (Enum) label_enum1, text1);
      Transform root2 = trans;
      // ISSUE: variable of a boxed type
      __Boxed<QuestRoom.UI> label_enum2 = (Enum) QuestRoom.UI.LBL_DEF;
      num4 = finalStatus.GetDefencesSum();
      string text2 = num4.ToString();
      this.SetLabelText(root2, (Enum) label_enum2, text2);
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_HP, finalStatus.hp.ToString());
      CharaInfo.ClanInfo clanInfo = org.clanInfo;
      if (clanInfo == null)
      {
        clanInfo = new CharaInfo.ClanInfo();
        clanInfo.clanId = -1;
        clanInfo.tag = string.Empty;
      }
      bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
      this.SetSupportEncoding(trans, (Enum) QuestRoom.UI.LBL_NAME, true);
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, org.name, org.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
      this.SetLabelText(trans, (Enum) QuestRoom.UI.LBL_LV, org.level.ToString());
    }
    int userId = org != null ? org.userId : 0;
    if (!this.roomUserModelInfo[index].IsSameRoomState(state, userId) || this.openAfterUpdate)
    {
      if (state == QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.EMPTY)
      {
        if (index < this.questData.userNumLimit)
        {
          this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_EMPTY, true);
          this.SetActive(trans, (Enum) QuestRoom.UI.LBL_USER_LIMIT_NUM, false);
        }
        else
        {
          this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_EMPTY, false);
          this.SetActive(trans, (Enum) QuestRoom.UI.LBL_USER_LIMIT_NUM, true);
        }
      }
      else
      {
        this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_EMPTY, false);
        this.SetActive(trans, (Enum) QuestRoom.UI.LBL_USER_LIMIT_NUM, false);
      }
      this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_BATTLE, state == QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.BATTLE);
      this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_READY, state == QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.READY);
      this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_READY_WAIT, state == QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.STAY);
      this.ActiveAndTween(trans, (Enum) QuestRoom.UI.SPR_USER_EQUIP_CHANGE, state == QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.EQUIP_CHANGE);
    }
    this.roomUserModelInfo[index].Reset();
    this.roomUserModelInfo[index].SetInfo(component, state, userId);
  }

  private void SetupChatBalloon(Transform t, int index)
  {
    QuestRoom.ChatBalloon balloon = this.balloons[index];
    UIPanel component1 = ((Component) this.FindCtrl(t, (Enum) QuestRoom.UI.OBJ_CHAT)).GetComponent<UIPanel>();
    UITweener component2 = ((Component) this.FindCtrl(t, (Enum) QuestRoom.UI.OBJ_TEXT_TWEEN)).GetComponent<UITweener>();
    UITweener component3 = ((Component) this.FindCtrl(t, (Enum) QuestRoom.UI.OBJ_STAMP_TWEEN)).GetComponent<UITweener>();
    UILabel component4 = ((Component) this.FindCtrl(t, (Enum) QuestRoom.UI.LBL_CHAT_TEXT)).GetComponent<UILabel>();
    UISprite component5 = ((Component) this.FindCtrl(t, (Enum) QuestRoom.UI.SPR_CHAT_TEXT_BG)).GetComponent<UISprite>();
    UITexture component6 = ((Component) this.FindCtrl(t, (Enum) QuestRoom.UI.TEX_CHAT_STAMP)).GetComponent<UITexture>();
    UIPanel balloonPanel = component1;
    UITweener chat_text_tween = component2;
    UITweener chat_stamp_tween = component3;
    UILabel chat_text_label = component4;
    UISprite chat_text_bg = component5;
    UITexture chat_stamp_texture = component6;
    balloon.Init(balloonPanel, chat_text_tween, chat_stamp_tween, chat_text_label, chat_text_bg, chat_stamp_texture);
  }

  private void UpdateChatBalloon(int index)
  {
    QuestRoom.ChatBalloon balloon = this.balloons[index];
    balloon.SetPosition(index);
    PartyModel.SlotInfo slotInfoByIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(index);
    int user_id = -1;
    if (slotInfoByIndex != null && slotInfoByIndex.userInfo != null)
      user_id = slotInfoByIndex.userInfo.userId;
    if (user_id == balloon.userId)
      return;
    balloon.SetUser(user_id);
  }

  private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
  {
    this.SetActive(root, _enum, is_active);
    if (!is_active)
      return;
    this.ResetTween(root, _enum);
    this.PlayTween(root, _enum, is_input_block: false);
  }

  protected void OnQuery_QuestRoomChangePublicDialog_YES()
  {
    GameSection.StayEvent();
    PartyManager.PartySetting partySetting = MonoBehaviourSingleton<PartyManager>.I.partySetting;
    partySetting.isLock = false;
    partySetting.level = partySetting.reserveLimitLevel;
    MonoBehaviourSingleton<PartyManager>.I.SendEdit(partySetting, (Action<bool>) (isSuccess =>
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_CHANGE_PUBLIC, false);
      this.SetActive((Enum) QuestRoom.UI.BTN_CHANGE_PUBLIC_OFF, true);
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  protected void OnQuery_CHANGE_EQUIP()
  {
    GameSection.StayEvent();
    int index1 = 0;
    for (int index2 = 4; index1 < index2; ++index1)
      this.roomUserModelInfo[index1].Reset();
    MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(true, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      GameSection.SetEventData((object) new object[2]
      {
        (object) this.observer.fromSearchSection,
        (object) this.observer.isEntryPass
      });
    }));
  }

  protected void OnQuery_READY()
  {
    if (this.selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
    {
      uint questId = 0;
      if (this.questData != null)
        questId = this.questData.questID;
      if (MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(questId))
        TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_START, (Action<bool>) (b =>
        {
          if (!b)
            return;
          this.DispatchEvent("QUEST_ROOM_IN_GAME", (object) this.questData);
        }));
      else if (MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderShadowQuest())
        TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_START, (Action<bool>) (b =>
        {
          if (!b)
            return;
          this.DispatchEvent("QUEST_ROOM_IN_GAME", (object) this.questData);
        }));
      else
        this.DispatchEvent("QUEST_ROOM_IN_GAME", (object) this.questData);
    }
    else
      this.DispatchEvent("READY_GO");
  }

  protected void OnQuery_READY_GO()
  {
    switch (MonoBehaviourSingleton<PartyManager>.I.GetStatus())
    {
      case PARTY_STATUS.WAITING:
        GameSection.StayEvent();
        MonoBehaviourSingleton<PartyManager>.I.SendReady(true, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
        break;
      case PARTY_STATUS.PLAYING:
      case PARTY_STATUS.PLAYING_MATCH_CLOSE:
        GameSection.StayEvent();
        MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty((Action<bool>) (is_success =>
        {
          GameSection.ResumeEvent(is_success);
          this.DispatchEvent("QUEST_ROOM_IN_GAME", (object) this.questData);
        }));
        break;
      default:
        GameSection.StopEvent();
        break;
    }
  }

  protected void OnQuery_READY_BACK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendReady(false, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected void OnQuery_SECTION_BACK()
  {
    if (this.selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
      this.DispatchEvent("BACK_HOST");
    else if (this.isExplore && !this.observer.fromSearchSection)
      this.DispatchEvent("BACK_CLIENT_EXPLORE");
    else if (this.isRush && !this.observer.fromSearchSection)
      this.DispatchEvent("BACK_CLIENT_RUSH");
    else
      this.DispatchEvent("SECTION_BACK_DO");
    if (!MonoBehaviourSingleton<ChatManager>.IsValid())
      return;
    MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
  }

  protected void OnQuery_SECTION_BACK_DO()
  {
    this.section_back_event = true;
    if (this.isRandomSearch)
    {
      if (MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "QuestAcceptSearchMatchingFailed")
        GameSection.ChangeEvent("BACK_SEARCH_MATCHING_FAILED");
      else
        GameSection.ChangeEvent("BACK_SEARCH_MATCHING");
    }
    else if (this.observer.fromSearchSection)
      GameSection.ChangeEvent(this.observer.isEntryPass ? "BACK_ROOM_PASS_ENTRY" : "BACK_ROOM_SEARCH");
    else if (this.isExplore)
      GameSection.ChangeEvent("BACK_CLIENT_EXPLORE");
    else if (this.isRush)
      GameSection.ChangeEvent("BACK_CLIENT_RUSH");
    else
      GameSection.SetEventData((object) this.eventData);
    bool flag = false;
    if (!flag && !this.section_back_event)
      flag = true;
    if (!flag && !PartyManager.IsValidInParty())
      flag = true;
    if (flag)
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (b =>
    {
      GameSection.ResumeEvent(b);
      MonoBehaviourSingleton<CoopManager>.I.Clear();
      QuestRoomObserver.OffObserve();
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInLounge();
      if (!ClanMatchingManager.IsValidInClan())
        return;
      MonoBehaviourSingleton<ClanMatchingManager>.I.SendInClanBase();
    }));
  }

  private void OnQuery_QuestAcceptRoomBackHost_YES()
  {
    if (this.isExplore)
      this.RequestEvent("BACK_HOST_EXPLORE");
    else if (this.isRush)
      this.RequestEvent("BACK_HOST_RUSH");
    else
      this.RequestEvent("SECTION_BACK_DO_HOST");
  }

  private void OnQuery_SECTION_BACK_DO_HOST() => QuestRoomObserver.OffObserve();

  private void OnQuery_BACK_HOST_EXPLORE() => QuestRoomObserver.OffObserve();

  protected override void OnQuery_QUEST_ROOM_IN_GAME()
  {
    this.goToInGame = true;
    base.OnQuery_QUEST_ROOM_IN_GAME();
  }

  protected override void OnOpen()
  {
    MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
    this.openAfterUpdate = true;
    if (this.loadNPC == null)
      return;
    int index = 0;
    for (int length = this.loadNPC.Length; index < length; ++index)
      this.loadNPC[index] = false;
  }

  protected override void OnCloseStart() => MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
  {
    if ((notify_flags & GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_START) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.goToInGame = true;
      this.DispatchEvent("QUEST_ROOM_IN_GAME", (object) this.questData);
      MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty((Action<bool>) (b => { }));
    }
    if ((notify_flags & GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE) != (GameSection.NOTIFY_FLAG) 0 && this.canShowRepeatQuest)
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
    }
    base.OnNotify(notify_flags);
  }

  protected void OnQuery_MEMBER_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    PartyModel.SlotInfo slotInfoByIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(eventData);
    if (slotInfoByIndex == null || slotInfoByIndex.userInfo == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
      playerRecord.id = eventData + 1;
      playerRecord.isNPC = false;
      playerRecord.isSelf = slotInfoByIndex.userInfo.userId == this.selfUserId;
      playerRecord.animID = 90;
      playerRecord.charaInfo = slotInfoByIndex.userInfo;
      if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
        AssignedEquipmentTable.MergeAssignedEquip(ref playerRecord.charaInfo, MonoBehaviourSingleton<StatusManager>.I.EventEquipSet);
      playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(playerRecord.charaInfo, true, true, true, true);
      MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData;
      GameSection.SetEventData((object) new object[3]
      {
        (object) playerRecord,
        (object) this.observer.fromSearchSection,
        (object) this.observer.isEntryPass
      });
    }
  }

  private void Update()
  {
    if (!this.isOpen || this.section_back_event || Object.op_Equality((Object) this.observer, (Object) null) || !this.observer.IsValidParty() || !this.observer.IsConnect())
      return;
    bool flag = false;
    for (int idx = 0; idx < 4; ++idx)
    {
      if (!flag)
      {
        PARTY_PLAYER_STATUS partyPlayerStatus = PARTY_PLAYER_STATUS.NONE;
        if (MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(idx) != null)
          partyPlayerStatus = (PARTY_PLAYER_STATUS) MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(idx).status;
        if (this.partyPlayerStatus[idx] != partyPlayerStatus)
          flag = true;
        this.partyPlayerStatus[idx] = partyPlayerStatus;
      }
    }
    if (!flag)
      return;
    this.RefreshUI();
  }

  protected void OnQuery_QuestRoomInvalid_OK() => this.observer.SetupBackSectionEvent();

  private void InitializeChatUI()
  {
    MonoBehaviourSingleton<ChatManager>.I.CreateRoomChatWithParty();
    MonoBehaviourSingleton<ChatManager>.I.roomChat.JoinRoom(0);
    if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
    {
      MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveText += new ChatRoom.OnReceiveText(this.OnReceiveChatText);
      MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveStamp += new ChatRoom.OnReceiveStamp(this.OnReceiveChatStamp);
    }
    UIButton component = ((Component) this.GetCtrl((Enum) QuestRoom.UI.BTN_CHAT)).GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if (!TutorialStep.HasAllTutorialCompleted())
      ((Component) component).gameObject.SetActive(false);
    else if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
    {
      MonoBehaviourSingleton<UIManager>.I.mainChat.SetRoomChatNameType(false);
      MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver((UIBehaviour) this);
      component.onClick.Clear();
      component.onClick.Add(new EventDelegate(new EventDelegate.Callback(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly_NotOneShot)));
    }
    else
      ((Component) component).gameObject.SetActive(false);
  }

  public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != (MainChat.NOTIFY_FLAG) 0)
      this.SetBadge((Enum) QuestRoom.UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNumWithoutRoom(), (SpriteAlignment) 1, offset_y: -10);
    if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != (MainChat.NOTIFY_FLAG) 0)
      ((Component) this.GetCtrl((Enum) QuestRoom.UI.BTN_CHAT)).gameObject.SetActive(true);
    if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != (MainChat.NOTIFY_FLAG) 0)
      ((Component) this.GetCtrl((Enum) QuestRoom.UI.BTN_CHAT)).gameObject.SetActive(false);
    if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) == (MainChat.NOTIFY_FLAG) 0)
      return;
    ((Component) this.GetCtrl((Enum) QuestRoom.UI.BTN_CHAT)).gameObject.SetActive(false);
  }

  private void UpdateChatBalloonDepth()
  {
    for (int index = 0; index < this.balloons.Length; ++index)
      this.balloons[index].UpdateDepth(this.chatBalloonDepth[index]);
  }

  private void ChatBalloonMoveForward(int index)
  {
    for (int index1 = 0; index1 < this.chatBalloonDepth.Length; ++index1)
      --this.chatBalloonDepth[index1];
    this.chatBalloonDepth[index] = 4;
    this.UpdateChatBalloonDepth();
  }

  private void OnReceiveChatText(
    int userId,
    string userName,
    string message,
    string chatItemId,
    bool isOldMessage = false)
  {
    int slotIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotIndex(userId);
    if (slotIndex < 0)
      return;
    this.ChatBalloonMoveForward(slotIndex);
    this.balloons[slotIndex].ShowText(message);
  }

  private void OnReceiveChatStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    int slotIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotIndex(userId);
    if (slotIndex < 0)
      return;
    this.ChatBalloonMoveForward(slotIndex);
    this.balloons[slotIndex].ShowStamp(stampId);
  }

  protected IEnumerator StartPredownload()
  {
    List<QuestRoom.ResourceInfo> list = new List<QuestRoom.ResourceInfo>();
    uint mapId = this.questData.mapId;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
    if (fieldMapData != null)
    {
      string name = fieldMapData.stageName;
      if (string.IsNullOrEmpty(name))
        name = "ST011D_01";
      StageTable.StageData data = Singleton<StageTable>.I.GetData(name);
      if (data != null)
      {
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, data.scene));
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, data.sky));
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect));
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0));
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect));
        for (int index = 0; index < 8; ++index)
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[index]));
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.questData.enemyID[0]);
        int modelId = enemyData.modelId;
        string enemyBody = ResourceName.GetEnemyBody(modelId);
        string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
        string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
        if (!string.IsNullOrEmpty(enemyMaterial))
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
        list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
        LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
        foreach (QuestRoom.ResourceInfo resourceInfo in list)
        {
          if (!string.IsNullOrEmpty(resourceInfo.packageName))
          {
            ResourceManager.downloadOnly = true;
            load_queue.Load(resourceInfo.category, resourceInfo.packageName, (string[]) null);
            ResourceManager.downloadOnly = false;
            yield return (object) load_queue.Wait();
          }
        }
        if (MonoBehaviourSingleton<ResourceManager>.I.cache.PreloadedInGameResouces.Count < 64 /*0x40*/)
        {
          GlobalSettingsManager.LinkResources linkResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
          InGameSettingsManager.UseResources gameCommonResources = linkResources.inGameCommonResources;
          for (int index = 0; index < gameCommonResources.effects.Length; ++index)
            list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, gameCommonResources.effects[index]));
          for (int index = 0; index < gameCommonResources.uiEffects.Length; ++index)
            list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_UI, gameCommonResources.uiEffects[index]));
          InGameSettingsManager.UseResources gameQuestResources = linkResources.inGameQuestResources;
          for (int index = 0; index < gameQuestResources.effects.Length; ++index)
            list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, gameQuestResources.effects[index]));
          for (int index = 0; index < gameQuestResources.uiEffects.Length; ++index)
            list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_UI, gameQuestResources.uiEffects[index]));
          for (int index = 0; index < linkResources.stunnedEffectList.Length; ++index)
            list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.stunnedEffectList[index]));
          for (int index = 0; index < linkResources.charmEffectList.Length; ++index)
            list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.charmEffectList[index]));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.battleStartEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.changeWeaponEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.spActionStartEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.arrowBleedOtherEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.shadowSealingEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_frozen_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyParalyzeHitEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyPoisonHitEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyFreezeHitEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyOtherSimpleHitEffectName));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_04"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_flinch_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_shock_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01"));
          list.Add(new QuestRoom.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01"));
          for (int i = 0; i < list.Count; ++i)
          {
            if (!string.IsNullOrEmpty(list[i].packageName) && MonoBehaviourSingleton<ResourceManager>.I.cache.PreloadedInGameResouces.Count < 64 /*0x40*/)
            {
              switch (list[i].category)
              {
                case RESOURCE_CATEGORY.EFFECT_ACTION:
                case RESOURCE_CATEGORY.EFFECT_UI:
                  load_queue.CacheEffect(list[i].category, list[i].packageName);
                  yield return (object) load_queue.Wait();
                  MonoBehaviourSingleton<ResourceManager>.I.cache.AddPreloadResources(list[i].packageName);
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }
  }

  protected void OnQuery_REPEAT_BATTLE()
  {
    if (this.selfUserId != MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendRepeat(!MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest, (Action<bool>) (is_success =>
    {
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
      this.SetActive((Enum) QuestRoom.UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
      GameSaveData.instance.defaultRepeatPartyOn = MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest;
      GameSection.ResumeEvent(is_success);
    }));
  }

  private enum UI
  {
    LBL_QUEST_NAME,
    LBL_ROOM_ID,
    SPR_WEAK,
    LBL_NON_WEAK,
    BTN_INVITE,
    GRD_PLAYER_INFO,
    LBL_READY,
    BTN_READY,
    BTN_READY_BACK,
    TGL_READY,
    BTN_OWNER_READY,
    BTN_BACK,
    LBL_NAME,
    LBL_LV,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_USER_READY,
    SPR_USER_READY_WAIT,
    SPR_USER_EQUIP_CHANGE,
    SPR_USER_EMPTY,
    SPR_USER_BATTLE,
    BTN_EMO_0,
    BTN_EMO_1,
    BTN_EMO_2,
    SPR_WEAPON_1,
    SPR_WEAPON_2,
    SPR_WEAPON_3,
    BTN_NAME_BG,
    BTN_FRAME,
    LBL_USER_LIMIT_NUM,
    BTN_CHAT,
    OBJ_CHAT,
    OBJ_STAMP_TWEEN,
    OBJ_TEXT_TWEEN,
    TEX_CHAT_STAMP,
    LBL_CHAT_TEXT,
    SPR_CHAT_TEXT_BG,
    BTN_CHANGE_PUBLIC,
    BTN_BACK_2,
    BTN_CHANGE_PUBLIC_OFF,
    OBJ_QUEST_INFO,
    OBJ_EXPLORE_INFO,
    LBL_EXPLORE_NAME,
    LBL_LIMIT_TIME,
    SPR_WEAK_EXPLORE,
    LBL_NON_WEAK_EXPLORE,
    OBJ_ENEMY,
    SPR_ENM_ELEMENT,
    OBJ_RUSH_INFO,
    LBL_RUSH_NAME,
    LBL_RUSH_LIMIT_TIME,
    LBL_RUSH_FLOORNUMBER,
    TEX_RUSH_ICON,
    SPR_TYPE_DIFFICULTY,
    OBJ_WAVE_MATCH_INFO,
    LBL_WM_TITLE,
    LBL_WM_LIMIT_TIME,
    LBL_WM_TYPE,
    LBL_WM_EVENT_TYPE,
    BTN_REPEAT_ON,
    BTN_REPEAT_OFF,
  }

  private class RoomUserModelInfo
  {
    public QuestRoomUserInfo roomInfo { private set; get; }

    public int userID { private set; get; }

    public QuestRoom.RoomUserModelInfo.ROOM_USER_STATE roomState { private set; get; }

    public int emotionStateIndex { get; private set; }

    public RoomUserModelInfo()
    {
      this.emotionStateIndex = 0;
      this.Reset();
    }

    public void SetInfo(
      QuestRoomUserInfo room_info,
      QuestRoom.RoomUserModelInfo.ROOM_USER_STATE state,
      int id)
    {
      this.roomInfo = room_info;
      this.roomState = state;
      this.userID = id;
    }

    public void Reset()
    {
      this.roomInfo = (QuestRoomUserInfo) null;
      this.roomState = QuestRoom.RoomUserModelInfo.ROOM_USER_STATE.EMPTY;
      this.userID = 0;
    }

    public bool SetStateIndex(int index, int max)
    {
      if (index < 0 || index > max)
        return false;
      this.emotionStateIndex = index;
      return true;
    }

    public bool IsSameRoomState(QuestRoom.RoomUserModelInfo.ROOM_USER_STATE state, int user_id)
    {
      return state == this.roomState && this.userID == user_id;
    }

    public enum ROOM_USER_STATE
    {
      NONE,
      EMPTY,
      STAY,
      READY,
      EQUIP_CHANGE,
      BATTLE,
    }
  }

  private class ChatBalloon
  {
    private int position;
    private UIPanel balloonPanel;
    private UITweener chatTextTween;
    private UITweener chatStampTween;
    private UILabel chatTextLabel;
    private UISprite chatTextBG;
    private UITexture chatStampTexture;
    private MonoBehaviour behaviour;
    private bool isText;
    private bool isStamp;
    private EventDelegate onFinishText;
    private EventDelegate onFinishStamp;
    private IEnumerator loading;

    public int userId { get; private set; }

    public ChatBalloon(MonoBehaviour behaviour)
    {
      this.behaviour = behaviour;
      this.userId = -1;
      this.CreateDelegate();
    }

    private void CreateDelegate()
    {
      this.onFinishText = new EventDelegate(new EventDelegate.Callback(this.ResetText));
      this.onFinishStamp = new EventDelegate(new EventDelegate.Callback(this.ResetStamp));
    }

    public void Init(
      UIPanel balloonPanel,
      UITweener chat_text_tween,
      UITweener chat_stamp_tween,
      UILabel chat_text_label,
      UISprite chat_text_bg,
      UITexture chat_stamp_texture)
    {
      this.balloonPanel = balloonPanel;
      if (Object.op_Inequality((Object) this.chatTextTween, (Object) null))
      {
        this.ResetText();
        this.chatTextTween.RemoveOnFinished(this.onFinishText);
      }
      if (Object.op_Inequality((Object) this.chatStampTween, (Object) null))
      {
        this.ResetStamp();
        this.chatStampTween.RemoveOnFinished(this.onFinishStamp);
      }
      this.chatTextTween = chat_text_tween;
      this.chatStampTween = chat_stamp_tween;
      this.chatTextLabel = chat_text_label;
      this.chatTextBG = chat_text_bg;
      this.chatStampTexture = chat_stamp_texture;
      this.chatTextTween.AddOnFinished(this.onFinishText);
      this.chatStampTween.AddOnFinished(this.onFinishStamp);
      this.Reset();
    }

    public void SetUser(int user_id)
    {
      this.userId = user_id;
      this.Reset();
    }

    public void SetPosition(int position)
    {
      if (this.position == position)
        return;
      this.position = position;
      this.Reset();
    }

    public void UpdateDepth(int adddepth)
    {
      this.balloonPanel.depth = this.balloonPanel.parentPanel.depth + adddepth;
    }

    public void Reset()
    {
      this.ResetText();
      this.ResetStamp();
    }

    private void ResetIfNeeded()
    {
      if (this.isText)
        this.ResetText();
      if (!this.isStamp)
        return;
      this.ResetStamp();
    }

    public void ShowStamp(int stampId)
    {
      this.ResetIfNeeded();
      this.isStamp = true;
      this.loading = this.LoadStamp(stampId);
      this.behaviour.StartCoroutine(this.loading);
    }

    private IEnumerator LoadStamp(int stampId)
    {
      LoadingQueue loadingQueue = new LoadingQueue(this.behaviour);
      LoadObject lo_stamp = loadingQueue.LoadChatStamp(stampId);
      yield return (object) loadingQueue.Wait();
      this.chatStampTexture.mainTexture = (Texture) (lo_stamp.loadedObject as Texture2D);
      this.PlayTween(this.chatStampTween);
    }

    public void ShowText(string message)
    {
      this.ResetIfNeeded();
      this.isText = true;
      this.chatTextLabel.text = Utility.GetTrimLineText(3, message);
      this.FitTextBG();
      this.MoveTextBG();
      this.PlayTween(this.chatTextTween);
    }

    private void FitTextBG()
    {
      this.chatTextBG.width = Mathf.CeilToInt(this.chatTextLabel.printedSize.x) + 34;
    }

    private void MoveTextBG()
    {
      float num1 = -150f;
      float num2 = -68f;
      float num3 = 128f;
      float num4 = 390f;
      Vector3 localPosition = ((Component) this.chatTextBG).transform.localPosition;
      if ((double) this.chatTextBG.width < (double) num3)
      {
        localPosition.x = (float) -((double) this.chatTextBG.width * 0.5);
        ((Component) this.chatTextBG).transform.localPosition = localPosition;
      }
      else
      {
        if (this.position == 0)
          localPosition.x = -68f;
        else if (this.position == 1)
        {
          float num5 = Mathf.Max((float) this.chatTextBG.width - num3, 0.0f) / (num4 - num3);
          localPosition.x = (num1 - num2) * num5 + num2;
        }
        else if (this.position == 2)
        {
          float num6 = Mathf.Max((float) this.chatTextBG.width - num3, 0.0f) / (num4 - num3);
          localPosition.x = (num1 - num2) * num6 + num2;
          localPosition.x = (float) -((double) this.chatTextBG.width + (double) localPosition.x);
        }
        else if (this.position == 3)
          localPosition.x = (float) -(this.chatTextBG.width - 68);
        ((Component) this.chatTextBG).transform.localPosition = localPosition;
      }
    }

    private void PlayTween(UITweener tween)
    {
      ((Component) tween).gameObject.SetActive(true);
      tween.ResetToBeginning();
      tween.PlayForward();
    }

    private void ResetStamp()
    {
      this.isStamp = false;
      if (this.loading != null)
      {
        this.behaviour.StopCoroutine(this.loading);
        this.loading = (IEnumerator) null;
      }
      this.Reset(this.chatStampTween);
    }

    private void ResetText()
    {
      this.isText = false;
      this.Reset(this.chatTextTween);
    }

    private void Reset(UITweener balloonRoot)
    {
      balloonRoot.ResetToBeginning();
      ((Component) balloonRoot).gameObject.SetActive(false);
    }
  }

  protected class ResourceInfo
  {
    public RESOURCE_CATEGORY category;
    public string packageName;

    public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
    {
      this.category = category;
      this.packageName = packageName;
    }
  }
}

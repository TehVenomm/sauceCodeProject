// Decompiled with JetBrains decompiler
// Type: QuestAcceptSeriesArenaRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class QuestAcceptSeriesArenaRoom : OffLineQuestRoomBase
{
  private bool isReady;
  private QuestTable.QuestTableData questData;
  private DeliveryTable.DeliveryData deliveryData;
  private int orderNo;

  public override void Initialize()
  {
    base.Initialize();
    uint currentQuestId = MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
    this.deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(currentQuestId);
    this.questData = this.deliveryData.GetQuestData();
  }

  public override void UpdateUI()
  {
    this.UpdateUser();
    this.UpdateTopBar();
    this.UpdateEnemyList();
    this.UpdateStartButton();
    this.UpdateSubMission();
  }

  protected new void UpdateUser()
  {
    this.isReady = true;
    this.SetGrid((Enum) QuestAcceptSeriesArenaRoom.UI.GRD_PLAYER_INFO, "", 3, false, (Func<int, Transform, Transform>) ((i, t) => this.Realizes("QuestRoomUserInfoSelf", t, false)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.UpdateRoomUserInfo(t, i);
      this.SetEvent(t, (Enum) QuestAcceptSeriesArenaRoom.UI.BTN_NAME_BG, "CHANGE_EQUIP", i + 1);
      this.SetEvent(t, (Enum) QuestAcceptSeriesArenaRoom.UI.BTN_FRAME, "CHANGE_EQUIP", i + 1);
    }));
  }

  protected override void UpdateRoomUserInfo(Transform trans, int index)
  {
    this.SetLabelText(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_ATK, string.Empty);
    this.SetLabelText(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_DEF, string.Empty);
    this.SetLabelText(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_HP, string.Empty);
    this.SetLabelText(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_NAME, string.Empty);
    this.SetLabelText(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_LV, string.Empty);
    this.SetActive(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.SPR_USER_EQUIPING, false);
    base.UpdateRoomUserInfo(trans, index);
    if (Object.op_Equality((Object) ((Component) trans).GetComponent<QuestRoomUserInfo>(), (Object) null))
      return;
    this.userInfo = this.GetUserCharaInfo(index);
    if (this.userInfo == null)
    {
      this.ActiveAndTween(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.SPR_USER_EQUIPING, true);
      this.isReady = false;
    }
    else
      this.SetLabelText(trans, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_NAME, this.userInfo.equipSetName);
  }

  private void OnQuery_CHANGE_EQUIP()
  {
    MonoBehaviourSingleton<StatusManager>.I.SetSelectUniqueEquipSetNo(MonoBehaviourSingleton<StatusManager>.I.GetOrderUniqueEquipSetNo((int) GameSection.GetEventData()));
  }

  private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
  {
    this.SetActive(root, _enum, is_active);
    if (!is_active)
      return;
    this.ResetTween(root, _enum);
    this.PlayTween(root, _enum, is_input_block: false);
  }

  protected override CharaInfo GetUserCharaInfo(int setNo)
  {
    this.orderNo = setNo + 1;
    return MonoBehaviourSingleton<StatusManager>.I.GetCreateUniquePlayerInfo(this.orderNo).charaInfo;
  }

  protected override EquipSetCalculator GetUserEquipCalculator()
  {
    return MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.GetOrderUniqueEquipSetNo(this.orderNo));
  }

  private void UpdateTopBar()
  {
    int limitTime = (int) this.questData.limitTime;
    this.SetLabelText((Enum) QuestAcceptSeriesArenaRoom.UI.LBL_LIMIT_TIME, $"{limitTime / 60}:{limitTime % 60:D2}");
    this.SetLabelText((Enum) QuestAcceptSeriesArenaRoom.UI.LBL_SERIES_ARENA_NAME, this.deliveryData.name);
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) QuestAcceptSeriesArenaRoom.UI.TEX_ICON)).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(this.questData.rarity));
  }

  private void UpdateEnemyList()
  {
    this.SetGrid((Enum) QuestAcceptSeriesArenaRoom.UI.GRD_ENEMY_INFO, "QuestSeriesArenaRoomEnemyListItem", this.questData.enemyID.Length, false, (Action<int, Transform, bool>) ((i, t, b) => this.InitEnemyItem(i, t, b)));
  }

  private void OnQuery_ENEMY_DETAIL()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.deliveryData,
      (object) (int) GameSection.GetEventData()
    });
  }

  private void UpdateSubMission()
  {
    QuestAcceptSeriesArenaRoom.UI[] uiArray1 = new QuestAcceptSeriesArenaRoom.UI[3]
    {
      QuestAcceptSeriesArenaRoom.UI.LBL_MISSION_INFO_1,
      QuestAcceptSeriesArenaRoom.UI.LBL_MISSION_INFO_2,
      QuestAcceptSeriesArenaRoom.UI.LBL_MISSION_INFO_3
    };
    QuestAcceptSeriesArenaRoom.UI[] uiArray2 = new QuestAcceptSeriesArenaRoom.UI[3]
    {
      QuestAcceptSeriesArenaRoom.UI.SPR_MISSION_CROWN_ON_1,
      QuestAcceptSeriesArenaRoom.UI.SPR_MISSION_CROWN_ON_2,
      QuestAcceptSeriesArenaRoom.UI.SPR_MISSION_CROWN_ON_3
    };
    QuestAcceptSeriesArenaRoom.UI[] uiArray3 = new QuestAcceptSeriesArenaRoom.UI[3]
    {
      QuestAcceptSeriesArenaRoom.UI.SPR_MISSION_CROWN_OFF_1,
      QuestAcceptSeriesArenaRoom.UI.SPR_MISSION_CROWN_OFF_2,
      QuestAcceptSeriesArenaRoom.UI.SPR_MISSION_CROWN_OFF_3
    };
    QuestInfoData.Mission[] missionData = QuestInfoData.CreateMissionData(this.questData);
    if (missionData == null)
      return;
    for (int index = 0; index < missionData.Length; ++index)
    {
      this.SetLabelText((Enum) uiArray1[index], missionData[index].tableData.missionText);
      bool is_visible = missionData[index].state >= CLEAR_STATUS.CLEAR;
      this.SetActive((Enum) uiArray2[index], is_visible);
      this.SetActive((Enum) uiArray3[index], !is_visible);
    }
  }

  private void InitEnemyItem(int i, Transform t, bool isRecycle)
  {
    int num = this.questData.enemyLv[i];
    uint id = (uint) this.questData.enemyID[i];
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(id);
    if (enemyData == null)
      return;
    this.SetLabelText(t, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1U, (object) num));
    this.SetLabelText(t, (Enum) QuestAcceptSeriesArenaRoom.UI.LBL_ENEMY_NAME, enemyData.name);
    ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.FindCtrl(t, (Enum) QuestAcceptSeriesArenaRoom.UI.OBJ_ENEMY), enemyData.element);
    itemIcon.SetEnableCollider(false);
    itemIcon.rarityFrame.spriteName = "MonsterFrame_CD";
    UIBehaviour.SetRarityColorType(1, (UIWidget) itemIcon.rarityFrame);
    this.SetActive(t, (Enum) QuestAcceptSeriesArenaRoom.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
    this.SetElementSprite(t, (Enum) QuestAcceptSeriesArenaRoom.UI.SPR_ELEMENT, (int) enemyData.element);
    this.SetElementSprite(t, (Enum) QuestAcceptSeriesArenaRoom.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive(t, (Enum) QuestAcceptSeriesArenaRoom.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
    this.SetEvent(t, (Enum) QuestAcceptSeriesArenaRoom.UI.BTN_DETAIL, "ENEMY_DETAIL", i);
    this.SetEvent(t, (Enum) QuestAcceptSeriesArenaRoom.UI.BTN_INFO_WINDOW, "ENEMY_DETAIL", i);
  }

  private void UpdateStartButton()
  {
    this.SetActive((Enum) QuestAcceptSeriesArenaRoom.UI.BTN_START, this.isReady);
    this.SetActive((Enum) QuestAcceptSeriesArenaRoom.UI.BTN_NG, !this.isReady);
  }

  protected void OnQuery_START() => this.StartQuest();

  private void StartQuest()
  {
    GameSection.StayEvent();
    CoopApp.EnterSeriesArenaQuestOffline((Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => GameSection.ResumeEvent(isStart)));
  }

  public void OnQuery_TO_MISSION()
  {
    this.GetComponent<UITweenCtrl>((Enum) QuestAcceptSeriesArenaRoom.UI.OBJ_ENEMYS_ROOT).Play();
  }

  public void OnQuery_TO_ENEMY()
  {
    this.GetComponent<UITweenCtrl>((Enum) QuestAcceptSeriesArenaRoom.UI.OBJ_ENEMYS_ROOT).Play(false);
  }

  private new enum UI
  {
    GRD_PLAYER_INFO,
    LBL_NAME,
    LBL_LV,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_USER_READY,
    SPR_USER_READY_WAIT,
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
    OBJ_CHAT,
    LBL_SERIES_ARENA_NAME,
    LBL_LIMIT_TIME,
    OBJ_ENEMYS_ROOT,
    GRD_ENEMY_INFO,
    LBL_ENEMY_NAME,
    LBL_ENEMY_LEVEL,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    OBJ_ENEMY,
    TEX_ICON,
    BTN_START,
    BTN_NG,
    BTN_DETAIL,
    OBJ_MISSION_ROOT,
    LBL_MISSION_INFO_1,
    SPR_MISSION_CROWN_ON_1,
    SPR_MISSION_CROWN_OFF_1,
    LBL_MISSION_INFO_2,
    SPR_MISSION_CROWN_ON_2,
    SPR_MISSION_CROWN_OFF_2,
    LBL_MISSION_INFO_3,
    SPR_MISSION_CROWN_ON_3,
    SPR_MISSION_CROWN_OFF_3,
    BTN_INFO_WINDOW,
    SPR_USER_EQUIPING,
  }
}

// Decompiled with JetBrains decompiler
// Type: QuestAcceptAssignedEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptAssignedEquipment : SkillInfoBase
{
  private InGameRecorder.PlayerRecord record;
  private EquipSetInfo setInfo;
  private PlayerLoader loader;
  private string nowSectionName = string.Empty;
  private AssignedEquipmentTable.AssignedEquipmentData targetData;
  private EquipItemAndSkillData[] allEquipItemAndSkillData;
  private EquipItemInfo[] allEquipItemInfo;
  private List<CharaInfo.EquipItem> equips;
  private DeliveryTable.DeliveryData deliveryData;
  private List<CharaInfo.EquipItem> equipsForRecord;
  private QuestAcceptAssignedEquipment.UI[] icons = new QuestAcceptAssignedEquipment.UI[7]
  {
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_WEAPON_1,
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_WEAPON_2,
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_WEAPON_3,
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_ARMOR,
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_HELM,
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_ARM,
    QuestAcceptAssignedEquipment.UI.OBJ_ICON_LEG
  };
  private QuestAcceptAssignedEquipment.UI[] iconsBtn = new QuestAcceptAssignedEquipment.UI[7]
  {
    QuestAcceptAssignedEquipment.UI.BTN_ICON_WEAPON_1,
    QuestAcceptAssignedEquipment.UI.BTN_ICON_WEAPON_2,
    QuestAcceptAssignedEquipment.UI.BTN_ICON_WEAPON_3,
    QuestAcceptAssignedEquipment.UI.BTN_ICON_ARMOR,
    QuestAcceptAssignedEquipment.UI.BTN_ICON_HELM,
    QuestAcceptAssignedEquipment.UI.BTN_ICON_ARM,
    QuestAcceptAssignedEquipment.UI.BTN_ICON_LEG
  };
  private QuestAcceptAssignedEquipment.UI[] iconsLevel = new QuestAcceptAssignedEquipment.UI[7]
  {
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_WEAPON_1,
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_WEAPON_2,
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_WEAPON_3,
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_ARMOR,
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_HELM,
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_ARM,
    QuestAcceptAssignedEquipment.UI.LBL_LEVEL_LEG
  };

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "AssignedEquipmentTable";
    }
  }

  public override void Initialize()
  {
    if (!Singleton<AssignedEquipmentTable>.IsValid())
    {
      Log.Error("AssignedEquipmentTable isnt Valid!!!");
    }
    else
    {
      this.deliveryData = GameSection.GetEventData() as DeliveryTable.DeliveryData;
      if (this.deliveryData == null)
      {
        Log.Error("DeliveryDataが存在しません");
      }
      else
      {
        this.targetData = Singleton<AssignedEquipmentTable>.I.GetAssignedEquipmentDataFromDeliveryId(this.deliveryData.id);
        if (this.targetData == null)
          Log.Error("依頼ID:{0}の指定装備データが存在しません", (object) this.deliveryData.id);
        this.record = this.CreatePlayerRecord();
        this.LoadModel();
        GameSection.SetEventData((object) null);
        this.nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
        this.setInfo = this.CreateEquipSetInfo();
        this.allEquipItemAndSkillData = new EquipItemAndSkillData[7];
        base.Initialize();
      }
    }
  }

  public override void UpdateUI()
  {
    this.UpdateStatusUI();
    this.UpdateEquipIcon();
    this.UpdateEnemyInfo();
    this.UpdateEquipSetInfo();
  }

  protected override void OnClose()
  {
  }

  private void UpdateEquipIcon()
  {
    if (this.setInfo == null)
      return;
    int index1 = 0;
    for (int index2 = 7; index1 < index2; ++index1)
    {
      this.SetEvent(this.GetCtrl((Enum) this.icons[index1]), "EMPTY", 0);
      this.SetEvent(this.GetCtrl((Enum) this.iconsBtn[index1]), "EMPTY", 0);
      this.SetLabelText(this.GetCtrl((Enum) this.iconsLevel[index1]), string.Empty);
    }
    int index3 = 0;
    for (int length = this.setInfo.item.Length; index3 < length; ++index3)
    {
      int num = -1;
      EquipItemInfo equipItemInfo = this.setInfo.item[index3];
      if (equipItemInfo == null)
      {
        this.SetActive(this.GetCtrl((Enum) this.iconsBtn[index3]), false);
      }
      else
      {
        EquipItemTable.EquipItemData equipItemData = (EquipItemTable.EquipItemData) null;
        if (equipItemInfo != null)
          equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemInfo.tableID);
        if (equipItemInfo != null && equipItemInfo.tableID != 0U)
        {
          num = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
          this.SetActive(this.GetCtrl((Enum) this.iconsLevel[index3]), true);
          string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) equipItemInfo.level.ToString());
          this.SetLabelText(this.GetCtrl((Enum) this.iconsLevel[index3]), text);
        }
        Transform ctrl = this.GetCtrl((Enum) this.icons[index3]);
        ((Component) ctrl).GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
        int index4 = 0;
        for (int count = Temporary.itemIconList.Count; index4 < count; ++index4)
          ((Component) Temporary.itemIconList[index4]).gameObject.SetActive(true);
        Temporary.itemIconList.Clear();
        ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, ctrl, event_name: "EQUIP", event_data: index3);
        if (Object.op_Inequality((Object) iconByEquipItemInfo, (Object) null))
        {
          this.SetLongTouch(iconByEquipItemInfo.transform, "DETAIL", (object) index3);
          this.SetEvent(this.GetCtrl((Enum) this.iconsBtn[index3]), "DETAIL", index3);
          ((Component) iconByEquipItemInfo).gameObject.SetActive(num != -1);
          if (num != -1)
            iconByEquipItemInfo.SetEquipExtInvertedColor(equipItemInfo, this.GetComponent<UILabel>((Enum) this.iconsLevel[index3]));
        }
        this.UpdateEquipSkillButton(equipItemInfo, index3);
      }
    }
    this.ResetTween((Enum) QuestAcceptAssignedEquipment.UI.OBJ_EQUIP_ROOT);
    this.PlayTween((Enum) QuestAcceptAssignedEquipment.UI.OBJ_EQUIP_ROOT, is_input_block: false);
  }

  private void UpdateEnemyInfo()
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(this.deliveryData.needs[0].questId);
    if (questData == null)
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    if (enemyData == null)
      return;
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1U, (object) enemyData.level));
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_ENEMY_NAME, enemyData.name);
    this.GetCtrl((Enum) QuestAcceptAssignedEquipment.UI.OBJ_ENEMY);
    ItemIcon.Create(ItemIcon.GetItemIconType(questData.questType), enemyData.iconId, new RARITY_TYPE?(questData.rarity), this.GetCtrl((Enum) QuestAcceptAssignedEquipment.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
    this.SetActive((Enum) QuestAcceptAssignedEquipment.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
    this.SetElementSprite((Enum) QuestAcceptAssignedEquipment.UI.SPR_ELEMENT, (int) enemyData.element);
    this.SetElementSprite((Enum) QuestAcceptAssignedEquipment.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive((Enum) QuestAcceptAssignedEquipment.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
  }

  private void UpdateEquipSetInfo()
  {
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_ASSIGNED_SET_NAME, this.targetData.setName);
    EquipItemTable.EquipItemData tableData = this.allEquipItemInfo[0].tableData;
    if (tableData == null)
      return;
    this.SetEquipmentTypeIcon((Enum) QuestAcceptAssignedEquipment.UI.SPR_TYPE_ICON_WEP, (Enum) QuestAcceptAssignedEquipment.UI.SPR_TYPE_ICON_BG, (Enum) QuestAcceptAssignedEquipment.UI.SPR_TYPE_ICON_RARITY, tableData);
    this.SetActive((Enum) QuestAcceptAssignedEquipment.UI.SPR_TYPE_ICON_RARITY, false);
    this.SetSprite((Enum) QuestAcceptAssignedEquipment.UI.SPR_SP_ATTACK_TYPE, tableData.spAttackType.GetBigFrameSpriteName());
  }

  private void LoadModel()
  {
    if (this.record == null)
      return;
    PlayerLoadInfo playerLoadInfo = this.record.playerLoadInfo;
    if (this.record.playerLoadInfo.weaponModelID == -1)
    {
      this.record.playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, false);
      this.record.animID = -1;
      playerLoadInfo = this.record.playerLoadInfo;
    }
    this.SetRenderPlayerModel(playerLoadInfo);
  }

  private PlayerLoadInfo CreatePlayerLoadInfo()
  {
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    uint weapon_id = 0;
    uint armor_id = 0;
    uint helm_id = 0;
    uint arm_id = 0;
    uint leg_id = 0;
    for (int index = 0; index < this.targetData.equipmentData.Length; ++index)
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(this.targetData.equipmentData[index].id);
      switch (equipItemData.type)
      {
        case EQUIPMENT_TYPE.ARMOR:
          armor_id = equipItemData.id;
          break;
        case EQUIPMENT_TYPE.HELM:
          helm_id = equipItemData.id;
          break;
        case EQUIPMENT_TYPE.ARM:
          arm_id = equipItemData.id;
          break;
        case EQUIPMENT_TYPE.LEG:
          leg_id = equipItemData.id;
          break;
        default:
          weapon_id = equipItemData.id;
          break;
      }
    }
    playerLoadInfo.SetupLoadInfo(weapon_id, armor_id, helm_id, arm_id, leg_id);
    return playerLoadInfo;
  }

  private void SetRenderPlayerModel(PlayerLoadInfo load_player_info)
  {
    this.SetRenderPlayerModel(this._transform, (Enum) QuestAcceptAssignedEquipment.UI.TEX_MODEL, load_player_info, this.record.animID, new Vector3(0.0f, -0.75f, 14f), new Vector3(0.0f, 180f, 0.0f), false, (Action<PlayerLoader>) (player_loader =>
    {
      if (!Object.op_Inequality((Object) player_loader, (Object) null))
        return;
      this.loader = player_loader;
    }));
  }

  private void UpdateStatusUI()
  {
    EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
    if (this.equips == null)
      this.equips = new List<CharaInfo.EquipItem>();
    else
      this.equips.Clear();
    for (int index = 0; index < this.allEquipItemInfo.Length; ++index)
    {
      if (this.allEquipItemInfo[index] != null)
      {
        CharaInfo.EquipItem equipItem = this.GetEquipItem(this.allEquipItemInfo[index]);
        if (equipItem != null)
          this.equips.Add(equipItem);
      }
    }
    equipSetCalculator.SetEquipSet(this.equips);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    int attacksSum = finalStatus.GetAttacksSum();
    int defencesSum = finalStatus.GetDefencesSum();
    int hp = finalStatus.hp;
    int level = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_ATK, attacksSum.ToString());
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_DEF, defencesSum.ToString());
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_HP, hp.ToString());
    this.SetLabelText((Enum) QuestAcceptAssignedEquipment.UI.LBL_LEVEL, level.ToString());
  }

  private void UpdateEquipSkillButton(EquipItemInfo item, int i)
  {
    Transform ctrl = this.GetCtrl((Enum) this.iconsBtn[i]);
    bool flag = item != null && item.tableID > 0U;
    if (flag)
    {
      SkillSlotUIData[] assignedSkillSlotData = this.GetAssignedSkillSlotData(item);
      this.SetSkillIconButton(ctrl, (Enum) QuestAcceptAssignedEquipment.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", item.tableData, assignedSkillSlotData, button_event_data: i);
    }
    ((Component) this.FindCtrl(ctrl, (Enum) QuestAcceptAssignedEquipment.UI.OBJ_SKILL_BUTTON_ROOT)).gameObject.SetActive(flag);
  }

  private EquipItemAndSkillData CreateEquipItemAndSkillData(int index)
  {
    if (this.allEquipItemAndSkillData[index] != null)
      return this.allEquipItemAndSkillData[index];
    EquipItemAndSkillData itemAndSkillData = new EquipItemAndSkillData();
    EquipItemInfo equipInfo = this.allEquipItemInfo[index];
    itemAndSkillData.equipItemInfo = equipInfo;
    itemAndSkillData.skillSlotUIData = this.GetAssignedSkillSlotData(equipInfo);
    this.allEquipItemAndSkillData[index] = itemAndSkillData;
    return itemAndSkillData;
  }

  private SkillSlotUIData[] GetAssignedSkillSlotData(EquipItemInfo equipInfo)
  {
    if (equipInfo == null)
      return (SkillSlotUIData[]) null;
    AssignedEquipmentTable.EquipmentData targetEquipData = this.GetTargetEquipData((int) equipInfo.tableData.id);
    if (targetEquipData == null)
      return (SkillSlotUIData[]) null;
    int maxSlot = equipInfo.GetMaxSlot();
    if (maxSlot == 0)
      return (SkillSlotUIData[]) null;
    SkillItemTable.SkillSlotData[] skillSlot = equipInfo.tableData.GetSkillSlot(equipInfo.exceed);
    List<SkillItemInfo> skillItemInfoList = new List<SkillItemInfo>(3);
    for (int index = 0; index < targetEquipData.skillIds.Length && maxSlot > index; ++index)
    {
      if (targetEquipData.skillIds[index] != 0U)
      {
        SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(targetEquipData.skillIds[index]);
        SkillItemInfo skillItemInfo = new SkillItemInfo(index, (int) skillItemData.id, skillItemData.GetMaxLv(0), 0);
        skillItemInfoList.Add(skillItemInfo);
      }
      else
        skillItemInfoList.Add((SkillItemInfo) null);
    }
    SkillItemInfo[] array = skillItemInfoList.ToArray();
    SkillSlotUIData[] assignedSkillSlotData = new SkillSlotUIData[maxSlot];
    for (int index = 0; index < array.Length; ++index)
    {
      if (array[index] == null)
      {
        assignedSkillSlotData[index] = new SkillSlotUIData();
        assignedSkillSlotData[index].slotData = new SkillItemTable.SkillSlotData(0U, equipInfo.tableData.GetSkillSlot(equipInfo.exceed)[index].slotType);
      }
      else if (array[index].tableData.type != skillSlot[index].slotType)
      {
        Log.Error("スロットタイプが合致しません " + (object) array[index].tableData.id);
      }
      else
      {
        assignedSkillSlotData[index] = new SkillSlotUIData();
        assignedSkillSlotData[index].slotData = new SkillItemTable.SkillSlotData(array[index].tableData.id, skillSlot[index].slotType);
        assignedSkillSlotData[index].itemData = array[index];
      }
    }
    return assignedSkillSlotData;
  }

  private AssignedEquipmentTable.EquipmentData GetTargetEquipData(int id)
  {
    for (int index = 0; index < this.targetData.equipmentData.Length; ++index)
    {
      if ((long) id == (long) this.targetData.equipmentData[index].id)
        return this.targetData.equipmentData[index];
    }
    return (AssignedEquipmentTable.EquipmentData) null;
  }

  private CharaInfo.EquipItem GetEquipItem(EquipItemInfo info)
  {
    if (info == null)
      return (CharaInfo.EquipItem) null;
    CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
    equipItem.eId = (int) info.tableID;
    equipItem.lv = info.level;
    equipItem.exceed = info.exceed;
    foreach (SkillSlotUIData skillSlotUiData in this.GetAssignedSkillSlotData(info))
    {
      SkillItemInfo itemData = skillSlotUiData.itemData;
      if (itemData != null)
      {
        equipItem.sIds.Add((int) itemData.tableID);
        equipItem.sLvs.Add(itemData.level);
        equipItem.sExs.Add(itemData.exceedCnt);
      }
    }
    for (int index = 0; index < info.ability.Length; ++index)
    {
      if (info.ability[index] != null && info.ability[index].id != 0U)
      {
        equipItem.aIds.Add((int) info.ability[index].id);
        equipItem.aPts.Add(info.ability[index].ap);
      }
    }
    return equipItem;
  }

  private InGameRecorder.PlayerRecord CreatePlayerRecord()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
    playerRecord.id = 0;
    playerRecord.isNPC = false;
    playerRecord.isSelf = true;
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
    playerRecord.playerLoadInfo = this.CreatePlayerLoadInfo();
    playerRecord.animID = PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
    playerRecord.charaInfo = new CharaInfo();
    playerRecord.charaInfo.userId = userInfo.id;
    playerRecord.charaInfo.name = userInfo.name;
    playerRecord.charaInfo.comment = userInfo.comment;
    playerRecord.charaInfo.code = userInfo.code;
    playerRecord.charaInfo.level = userStatus.level;
    playerRecord.charaInfo.atk = userStatus.atk;
    playerRecord.charaInfo.def = userStatus.def;
    playerRecord.charaInfo.hp = userStatus.hp;
    playerRecord.charaInfo.faceId = userStatus.faceId;
    playerRecord.charaInfo.hairId = userStatus.hairId;
    playerRecord.charaInfo.hairColorId = userStatus.hairColorId;
    playerRecord.charaInfo.skinId = userStatus.skinId;
    playerRecord.charaInfo.voiceId = userStatus.voiceId;
    playerRecord.charaInfo.sex = userStatus.sex;
    playerRecord.charaInfo.equipSet = (List<CharaInfo.EquipItem>) null;
    playerRecord.charaInfo.showHelm = 1;
    return playerRecord;
  }

  private List<EquipItem.Ability> GetAssignedAbilityList(
    AssignedEquipmentTable.EquipmentData assignedData)
  {
    if (assignedData == null)
      return (List<EquipItem.Ability>) null;
    List<EquipItem.Ability> assignedAbilityList = new List<EquipItem.Ability>();
    uint[] abilityIds = assignedData.abilityIds;
    int[] abilityPts = assignedData.abilityPts;
    for (int index = 0; index < abilityIds.Length; ++index)
    {
      if (abilityIds[index] != 0U)
        assignedAbilityList.Add(new EquipItem.Ability()
        {
          id = (int) abilityIds[index],
          pt = abilityPts[index]
        });
    }
    return assignedAbilityList;
  }

  private void CreateEquipItemInfo()
  {
    EquipSet equipSet = new EquipSet();
    equipSet.setNo = 1;
    for (int index = 0; index < this.targetData.equipmentData.Length; ++index)
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(this.targetData.equipmentData[index].id);
      EquipItem equipItem = new EquipItem();
      equipItem.uniqId = "";
      equipItem.equipItemId = (int) equipItemData.id;
      equipItem.level = (XorInt) equipItemData.maxLv;
      equipItem.exceed = equipItemData.exceedID != 0U ? 4 : 0;
      equipItem.ability = this.GetAssignedAbilityList(this.targetData.equipmentData[index]);
      switch (equipItemData.type)
      {
        case EQUIPMENT_TYPE.ARMOR:
          equipSet.armor = equipItem;
          break;
        case EQUIPMENT_TYPE.HELM:
          equipSet.helm = equipItem;
          break;
        case EQUIPMENT_TYPE.ARM:
          equipSet.arm = equipItem;
          break;
        case EQUIPMENT_TYPE.LEG:
          equipSet.leg = equipItem;
          break;
        default:
          equipSet.weapon_0 = equipItem;
          break;
      }
    }
    equipSet.weapon_1 = (EquipItem) null;
    equipSet.weapon_2 = (EquipItem) null;
    equipSet.setName = "";
    equipSet.showHelm = 1;
    this.allEquipItemInfo = new EquipItemInfo[7]
    {
      new EquipItemInfo(equipSet.weapon_0),
      null,
      null,
      new EquipItemInfo(equipSet.armor),
      new EquipItemInfo(equipSet.helm),
      new EquipItemInfo(equipSet.arm),
      new EquipItemInfo(equipSet.leg)
    };
  }

  private EquipSetInfo CreateEquipSetInfo()
  {
    if (this.allEquipItemInfo == null)
      this.CreateEquipItemInfo();
    return new EquipSetInfo(this.allEquipItemInfo, "", 1, new AccessoryPlaceInfo());
  }

  private List<CharaInfo.EquipItem> CreateEquipItemListForRecord(List<CharaInfo.EquipItem> equips)
  {
    if (equips == null)
      return (List<CharaInfo.EquipItem>) null;
    List<CharaInfo.EquipItem> itemListForRecord = equips;
    for (int index = 0; index < itemListForRecord.Count; ++index)
    {
      itemListForRecord[index].aIds.Clear();
      itemListForRecord[index].aPts.Clear();
      foreach (AssignedEquipmentTable.EquipmentData assignedData in this.targetData.equipmentData)
      {
        List<int> intList1 = new List<int>(2);
        List<int> intList2 = new List<int>(2);
        if ((long) assignedData.id == (long) itemListForRecord[index].eId)
        {
          List<EquipItem.Ability> assignedAbilityList = this.GetAssignedAbilityList(assignedData);
          if (assignedAbilityList != null)
          {
            foreach (EquipItem.Ability ability in assignedAbilityList)
            {
              intList1.Add(ability.id);
              intList2.Add(ability.pt);
            }
            itemListForRecord[index].aIds = intList1;
            itemListForRecord[index].aPts = intList2;
          }
        }
      }
    }
    return itemListForRecord;
  }

  private void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private void OnDisable()
  {
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
    this.nowSectionName = string.Empty;
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == this.nowSectionName))
      return;
    ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  private void OnQuery_ABILITY()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.setInfo,
      (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(this.setInfo),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) this.record.charaInfo.atk, (int) this.record.charaInfo.def, (int) this.record.charaInfo.hp, this.equips)
    });
    GameSection.SetEventData((object) new object[3]
    {
      (object) (GameSection.GetEventData() as object[]),
      (object) false,
      (object) false
    });
  }

  private void OnQuery_STATUS()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.setInfo,
      (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(this.setInfo),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) this.record.charaInfo.atk, (int) this.record.charaInfo.def, (int) this.record.charaInfo.hp, this.equips)
    });
    GameSection.SetEventData((object) new object[3]
    {
      (object) (GameSection.GetEventData() as object[]),
      (object) false,
      (object) false
    });
  }

  private void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.setInfo.item[eventData] == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) new object[4]
        {
          (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
          (object) this.CreateEquipItemAndSkillData(eventData),
          (object) this.record.charaInfo.sex,
          (object) this.record.charaInfo.faceId
        },
        (object) false,
        (object) false
      });
  }

  private void OnQuery_SKILL_ICON_BUTTON()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.setInfo.item[eventData] == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[3]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
        (object) this.CreateEquipItemAndSkillData(eventData),
        (object) this.record.charaInfo.sex
      });
  }

  protected void OnQuery_START()
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(this.deliveryData.needs[0].questId);
    if (questData == null)
    {
      GameSceneEvent.Cancel();
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID);
      MonoBehaviourSingleton<UIManager>.I.loading.SetShowTipsList(questData.questID);
      this.record.charaInfo.equipSet = this.CreateEquipItemListForRecord(this.equips);
      GameSection.StayEvent();
      CoopApp.EnterQuestOfflineAssignedEquipment(this.targetData, this.record.charaInfo, (Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => GameSection.ResumeEvent(isStart)));
    }
  }

  private void OnQuery_HOW_TO()
  {
    GameSection.SetEventData((object) $"{WebViewManager.Help}/{this.targetData.helpUrl}");
  }

  protected enum UI
  {
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    TEX_MODEL,
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
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_WEAPON_2,
    LBL_LEVEL_WEAPON_3,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    OBJ_EQUIP_ROOT,
    OBJ_EQUIP_SET_ROOT,
    OBJ_SKILL_BUTTON_ROOT,
    LBL_ENEMY_NAME,
    LBL_ENEMY_LEVEL,
    STR_WEAK,
    OBJ_ENEMY,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    SPR_TYPE_ICON_WEP,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    SPR_SP_ATTACK_TYPE,
    LBL_ASSIGNED_SET_NAME,
  }
}

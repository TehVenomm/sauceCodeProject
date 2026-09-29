// Decompiled with JetBrains decompiler
// Type: PlayerLoadInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
[Serializable]
public class PlayerLoadInfo
{
  public int faceModelID = -1;
  public int hairModelID = -1;
  public int headModelID = -1;
  public int bodyModelID = -1;
  public int armModelID = -1;
  public int legModelID = -1;
  public int weaponModelID = -1;
  public int skinColor = -1;
  public int hairColor = -1;
  public int headColor = -1;
  public int bodyColor = -1;
  public int armColor = -1;
  public int legColor = -1;
  public int weaponColor0 = -1;
  public int weaponColor1 = -1;
  public int weaponColor2 = -1;
  public int weaponEffectID;
  public float weaponEffectParam;
  public int weaponEffectColor = -1;
  public uint weaponEvolveId;
  public uint equipType;
  public uint weaponSpAttackType;
  public int actionVoiceBaseID = -1;
  public List<uint> accUIDs = new List<uint>();
  public bool isNeedToCache;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("---- ktnt ----\n");
    stringBuilder.AppendFormat("- isNeedToCache: {0}\n", (object) this.isNeedToCache);
    stringBuilder.AppendFormat("- faceModelID: {0}\n", (object) this.faceModelID);
    stringBuilder.AppendFormat("- hairModelID: {0}\n", (object) this.hairModelID);
    stringBuilder.AppendFormat("- headModelID: {0}\n", (object) this.headModelID);
    stringBuilder.AppendFormat("- bodyModelID: {0}\n", (object) this.bodyModelID);
    stringBuilder.AppendFormat("- armModelID: {0}\n", (object) this.armModelID);
    stringBuilder.AppendFormat("- legModelID: {0}\n", (object) this.legModelID);
    stringBuilder.AppendFormat("- weaponModelID: {0}\n", (object) this.weaponModelID);
    stringBuilder.AppendFormat("- skinColor: {0}\n", (object) this.skinColor);
    stringBuilder.AppendFormat("- hairColor: {0}\n", (object) this.hairColor);
    stringBuilder.AppendFormat("- headColor: {0}\n", (object) this.headColor);
    stringBuilder.AppendFormat("- bodyColor: {0}\n", (object) this.bodyColor);
    stringBuilder.AppendFormat("- armColor: {0}\n", (object) this.armColor);
    stringBuilder.AppendFormat("- legColor: {0}\n", (object) this.legColor);
    stringBuilder.AppendFormat("- weaponColor0: {0}\n", (object) this.weaponColor0);
    stringBuilder.AppendFormat("- weaponColor1: {0}\n", (object) this.weaponColor1);
    stringBuilder.AppendFormat("- weaponColor2: {0}\n", (object) this.weaponColor2);
    stringBuilder.AppendFormat("- weaponEffectID: {0}\n", (object) this.weaponEffectID);
    stringBuilder.AppendFormat("- weaponEffectParam: {0}\n", (object) this.weaponEffectParam);
    stringBuilder.AppendFormat("- weaponEffectColor: {0}\n", (object) this.weaponEffectColor);
    stringBuilder.AppendFormat("- weaponEvolveId: {0}\n", (object) this.weaponEvolveId);
    stringBuilder.AppendFormat("- equipType: {0}\n", (object) this.equipType);
    stringBuilder.AppendFormat("- weaponSpAttackType: {0}\n", (object) this.weaponSpAttackType);
    stringBuilder.AppendFormat("- actionVoiceBaseID: {0}\n", (object) this.actionVoiceBaseID);
    stringBuilder.AppendFormat("- accUIDs:\n");
    foreach (uint accUiD in this.accUIDs)
      stringBuilder.AppendFormat("-- {0}\n", (object) accUiD);
    return stringBuilder.ToString();
  }

  public bool Equals(PlayerLoadInfo info)
  {
    return info.faceModelID == this.faceModelID && info.hairModelID == this.hairModelID && info.headModelID == this.headModelID && info.bodyModelID == this.bodyModelID && info.armModelID == this.armModelID && info.legModelID == this.legModelID && info.weaponModelID == this.weaponModelID && info.skinColor == this.skinColor && info.hairColor == this.hairColor && info.headColor == this.headColor && info.bodyColor == this.bodyColor && info.armColor == this.armColor && info.legColor == this.legColor && info.weaponColor0 == this.weaponColor0 && info.weaponColor1 == this.weaponColor1 && info.weaponColor2 == this.weaponColor2 && info.weaponEffectID == this.weaponEffectID && (double) info.weaponEffectParam == (double) this.weaponEffectParam && info.weaponEffectColor == this.weaponEffectColor && (int) info.weaponEvolveId == (int) this.weaponEvolveId && (int) info.equipType == (int) this.equipType && (int) info.weaponSpAttackType == (int) this.weaponSpAttackType && info.actionVoiceBaseID == this.actionVoiceBaseID && this.EqualsAccessory(info.accUIDs);
  }

  private bool EqualsAccessory(List<uint> _accUIDs)
  {
    if (this.accUIDs.Count != _accUIDs.Count)
      return false;
    int index = 0;
    for (int count = this.accUIDs.Count; index < count; ++index)
    {
      if (!_accUIDs.Contains(this.accUIDs[index]))
        return false;
    }
    return true;
  }

  public PlayerLoadInfo Clone()
  {
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    playerLoadInfo.faceModelID = this.faceModelID;
    playerLoadInfo.hairModelID = this.hairModelID;
    playerLoadInfo.headModelID = this.headModelID;
    playerLoadInfo.bodyModelID = this.bodyModelID;
    playerLoadInfo.armModelID = this.armModelID;
    playerLoadInfo.legModelID = this.legModelID;
    playerLoadInfo.weaponModelID = this.weaponModelID;
    playerLoadInfo.skinColor = this.skinColor;
    playerLoadInfo.hairColor = this.hairColor;
    playerLoadInfo.headColor = this.headColor;
    playerLoadInfo.bodyColor = this.bodyColor;
    playerLoadInfo.armColor = this.armColor;
    playerLoadInfo.legColor = this.legColor;
    playerLoadInfo.weaponColor0 = this.weaponColor0;
    playerLoadInfo.weaponColor1 = this.weaponColor1;
    playerLoadInfo.weaponColor2 = this.weaponColor2;
    playerLoadInfo.weaponEffectID = this.weaponEffectID;
    playerLoadInfo.weaponEffectParam = this.weaponEffectParam;
    playerLoadInfo.weaponEffectColor = this.weaponEffectColor;
    playerLoadInfo.weaponEvolveId = this.weaponEvolveId;
    playerLoadInfo.equipType = this.equipType;
    playerLoadInfo.weaponSpAttackType = this.weaponSpAttackType;
    playerLoadInfo.actionVoiceBaseID = this.actionVoiceBaseID;
    playerLoadInfo.accUIDs.Clear();
    playerLoadInfo.accUIDs.AddRange((IEnumerable<uint>) this.accUIDs);
    return playerLoadInfo;
  }

  public void SetFace(int sex, int face_type_id, int skin_color_id)
  {
    this.faceModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetFaceModelID(sex, face_type_id);
    this.skinColor = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(skin_color_id));
  }

  public void SetHair(int sex, int hair_style_id, int hair_color_id)
  {
    this.hairModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairModelID(sex, hair_style_id);
    this.hairColor = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(hair_color_id));
  }

  public void SetEquip(
    int sex,
    uint equip_item_id,
    bool overwrite = true,
    bool enable_head = true,
    bool enable_weapon = true)
  {
    if (equip_item_id == 0U)
      return;
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id);
    if (equipItemData == null)
      return;
    this.SetEquip(sex, equipItemData, overwrite, enable_head, enable_weapon);
  }

  public void SetEquip(
    int sex,
    EquipItemTable.EquipItemData item_data,
    bool overwrite = true,
    bool enable_head = true,
    bool enable_weapon = true)
  {
    switch (item_data.type)
    {
      case EQUIPMENT_TYPE.ARMOR:
      case EQUIPMENT_TYPE.VISUAL_ARMOR:
        if (!overwrite && this.bodyModelID != -1)
          break;
        this.SetEquipBody(sex, item_data);
        break;
      case EQUIPMENT_TYPE.HELM:
      case EQUIPMENT_TYPE.VISUAL_HELM:
        if (!enable_head || !overwrite && this.headModelID != -1)
          break;
        this.SetEquipHead(sex, item_data);
        break;
      case EQUIPMENT_TYPE.ARM:
      case EQUIPMENT_TYPE.VISUAL_ARM:
        if (!overwrite && this.armModelID != -1)
          break;
        this.SetEquipArm(sex, item_data);
        break;
      case EQUIPMENT_TYPE.LEG:
      case EQUIPMENT_TYPE.VISUAL_LEG:
        if (!overwrite && this.legModelID != -1)
          break;
        this.SetEquipLeg(sex, item_data);
        break;
      default:
        if (!enable_weapon || !overwrite && this.weaponModelID != -1)
          break;
        this.SetEquipWeapon(sex, item_data);
        break;
    }
  }

  public void RemoveEquip(int sex, int slot_index)
  {
    switch (slot_index)
    {
      case 3:
        this.SetEquipBody(sex, (EquipItemTable.EquipItemData) null);
        break;
      case 4:
        this.SetEquipHead(sex, (EquipItemTable.EquipItemData) null);
        break;
      case 5:
        this.SetEquipArm(sex, (EquipItemTable.EquipItemData) null);
        break;
      case 6:
        this.SetEquipLeg(sex, (EquipItemTable.EquipItemData) null);
        break;
      default:
        this.SetEquipWeapon(sex, (EquipItemTable.EquipItemData) null);
        break;
    }
  }

  public void SetEquipBody(int sex, uint equip_body_item_id)
  {
    this.SetEquipBody(sex, equip_body_item_id != 0U ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_body_item_id) : (EquipItemTable.EquipItemData) null);
  }

  public void SetEquipBody(int sex, EquipItemTable.EquipItemData body_item_data)
  {
    if (body_item_data != null)
    {
      this.bodyModelID = body_item_data.GetModelID(sex);
      this.bodyColor = body_item_data.modelColor0;
    }
    else
    {
      this.bodyModelID = -1;
      this.bodyColor = -1;
    }
  }

  public void SetEquipArm(int sex, uint equip_arm_item_id)
  {
    this.SetEquipArm(sex, equip_arm_item_id != 0U ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_arm_item_id) : (EquipItemTable.EquipItemData) null);
  }

  public void SetEquipArm(int sex, EquipItemTable.EquipItemData arm_item_data)
  {
    if (arm_item_data != null)
    {
      this.armModelID = arm_item_data.GetModelID(sex);
      this.armColor = arm_item_data.modelColor0;
    }
    else
    {
      this.armModelID = -1;
      this.armColor = -1;
    }
  }

  public void SetEquipLeg(int sex, uint equip_leg_item_id)
  {
    this.SetEquipLeg(sex, equip_leg_item_id != 0U ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_leg_item_id) : (EquipItemTable.EquipItemData) null);
  }

  public void SetEquipLeg(int sex, EquipItemTable.EquipItemData leg_item_data)
  {
    if (leg_item_data != null)
    {
      this.legModelID = leg_item_data.GetModelID(sex);
      this.legColor = leg_item_data.modelColor0;
    }
    else
    {
      this.legModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.primeLegIDs[sex];
      this.legColor = -1;
    }
  }

  public void SetEquipHead(int sex, uint equip_head_item_id)
  {
    this.SetEquipHead(sex, equip_head_item_id != 0U ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_head_item_id) : (EquipItemTable.EquipItemData) null);
  }

  public void SetEquipHead(int sex, EquipItemTable.EquipItemData head_item_data)
  {
    if (head_item_data != null)
    {
      this.headModelID = head_item_data.GetModelID(sex);
      this.headColor = head_item_data.modelColor0;
    }
    else
    {
      this.headModelID = -1;
      this.headColor = -1;
    }
  }

  public void SetEquipWeapon(int sex, uint equip_weapon_item_id)
  {
    this.SetEquipWeapon(sex, equip_weapon_item_id != 0U ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_weapon_item_id) : (EquipItemTable.EquipItemData) null);
  }

  public void SetEquipWeapon(int sex, EquipItemTable.EquipItemData weapon_item_data)
  {
    if (weapon_item_data != null)
    {
      this.weaponModelID = weapon_item_data.GetModelID(sex);
      this.weaponColor0 = weapon_item_data.modelColor0;
      this.weaponColor1 = weapon_item_data.modelColor1;
      this.weaponColor2 = weapon_item_data.modelColor2;
      this.weaponEffectID = (int) weapon_item_data.effectID;
      this.weaponEffectParam = weapon_item_data.effectParam;
      this.weaponEffectColor = weapon_item_data.effectColor;
      this.weaponEvolveId = weapon_item_data.evolveId;
      this.equipType = (uint) weapon_item_data.type;
      this.weaponSpAttackType = (uint) weapon_item_data.spAttackType;
    }
    else
    {
      this.weaponModelID = -1;
      this.weaponColor0 = -1;
      this.weaponColor1 = -1;
      this.weaponColor2 = -1;
      this.weaponEffectID = 0;
      this.weaponEffectParam = 0.0f;
      this.weaponEffectColor = -1;
      this.weaponEvolveId = 0U;
      this.equipType = 0U;
      this.weaponSpAttackType = 0U;
    }
  }

  public void SetActionVoiceBaseID(int sex, int voice_type_id)
  {
    if (voice_type_id >= MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount)
      voice_type_id = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount - 1;
    this.actionVoiceBaseID = (voice_type_id * 10 + sex) * 10000;
  }

  public void ApplyUserStatus(bool need_weapon, bool is_priority_visual_equip, int set_no = -1)
  {
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    uint weapon_id = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(0, set_no);
    uint armor_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armorUniqId) : 0U;
    uint helm_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.helmUniqId) : 0U;
    uint arm_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armUniqId) : 0U;
    uint leg_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.legUniqId) : 0U;
    if (armor_id == 0U)
      armor_id = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(3, set_no);
    if (helm_id == 0U)
      helm_id = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(4, set_no);
    if (arm_id == 0U)
      arm_id = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(5, set_no);
    if (leg_id == 0U)
      leg_id = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(6, set_no);
    if (MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(set_no) == 0)
      helm_id = 0U;
    if (weapon_id == 0U)
      weapon_id = 100U;
    if (armor_id == 0U)
      armor_id = 10100U;
    if (!need_weapon)
      weapon_id = 0U;
    this.ApplyAccessory(MonoBehaviourSingleton<StatusManager>.I.GetEquippingAccessoryInfo(set_no));
    this.SetupLoadInfo(weapon_id, armor_id, helm_id, arm_id, leg_id);
  }

  public void ApplyUserUniqueStatus(bool need_weapon, bool is_priority_visual_equip, int set_no = -1)
  {
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    uint weapon_id = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(0, set_no);
    uint armor_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armorUniqId) : 0U;
    uint helm_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.helmUniqId) : 0U;
    uint arm_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armUniqId) : 0U;
    uint leg_id = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.legUniqId) : 0U;
    if (armor_id == 0U)
      armor_id = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(3, set_no);
    if (helm_id == 0U)
      helm_id = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(4, set_no);
    if (arm_id == 0U)
      arm_id = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(5, set_no);
    if (leg_id == 0U)
      leg_id = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(6, set_no);
    if (MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingShowHelm(set_no) == 0)
      helm_id = 0U;
    if (armor_id == 0U)
      armor_id = 10100U;
    if (!need_weapon)
      weapon_id = 0U;
    this.ApplyAccessory(MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingAccessoryInfo(set_no));
    this.SetupLoadInfo(weapon_id, armor_id, helm_id, arm_id, leg_id);
  }

  public void SetupLoadInfo(
    EquipSetInfo equip_set,
    ulong weapon_uniq_id,
    ulong armor_uniq_id,
    ulong helm_uniq_id,
    ulong arm_uniq_id,
    ulong leg_uniq_id,
    bool show_helm)
  {
    EquipItemInfo equipItemInfo1 = weapon_uniq_id == 0UL ? equip_set.item[0] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(weapon_uniq_id);
    EquipItemInfo equipItemInfo2 = armor_uniq_id == 0UL ? equip_set.item[3] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(armor_uniq_id);
    EquipItemInfo equipItemInfo3 = show_helm ? (helm_uniq_id == 0UL ? equip_set.item[4] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(helm_uniq_id)) : (EquipItemInfo) null;
    EquipItemInfo equipItemInfo4 = arm_uniq_id == 0UL ? equip_set.item[5] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(arm_uniq_id);
    EquipItemInfo equipItemInfo5 = leg_uniq_id == 0UL ? equip_set.item[6] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(leg_uniq_id);
    this.ApplyAccessory(equip_set.acc);
    this.SetupLoadInfo(equipItemInfo1 != null ? equipItemInfo1.tableID : 0U, equipItemInfo2 != null ? equipItemInfo2.tableID : 0U, equipItemInfo3 != null ? equipItemInfo3.tableID : 0U, equipItemInfo4 != null ? equipItemInfo4.tableID : 0U, equipItemInfo5 != null ? equipItemInfo5.tableID : 0U);
  }

  public void SetUpCharaMakeLoadInfo(
    EquipSetInfo equip_set,
    ulong armor_uniq_id,
    ulong helm_uniq_id,
    ulong arm_uniq_id,
    ulong leg_uniq_id,
    int sex,
    bool isShowhelm)
  {
    EquipItemInfo equipItemInfo1 = armor_uniq_id == 0UL ? equip_set.item[3] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(armor_uniq_id);
    EquipItemInfo equipItemInfo2 = arm_uniq_id == 0UL ? equip_set.item[5] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(arm_uniq_id);
    EquipItemInfo equipItemInfo3 = helm_uniq_id == 0UL ? equip_set.item[4] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(helm_uniq_id);
    EquipItemInfo equipItemInfo4 = leg_uniq_id == 0UL ? equip_set.item[6] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(leg_uniq_id);
    if (equipItemInfo1 != null)
      this.SetEquipBody(sex, equipItemInfo1.tableID);
    if (equipItemInfo2 != null)
      this.SetEquipArm(sex, equipItemInfo2.tableID);
    if (equipItemInfo4 != null)
      this.SetEquipLeg(sex, equipItemInfo4.tableID);
    else
      this.SetEquipLeg(sex, (EquipItemTable.EquipItemData) null);
    if (isShowhelm && equipItemInfo3 != null)
      this.SetEquipHead(sex, equipItemInfo3.tableID);
    this.ApplyAccessory(equip_set.acc);
  }

  public void SetupLoadInfo(
    uint weapon_id,
    uint armor_id,
    uint helm_id,
    uint arm_id,
    uint leg_id)
  {
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    int sex = userStatus.sex;
    int faceId = userStatus.faceId;
    int skinId = userStatus.skinId;
    int hairId = userStatus.hairId;
    int hairColorId = userStatus.hairColorId;
    uint equip_body_item_id = armor_id;
    uint equip_head_item_id = helm_id;
    uint equip_weapon_item_id = weapon_id;
    uint equip_arm_item_id = arm_id;
    uint equip_leg_item_id = leg_id;
    this.SetFace(sex, faceId, skinId);
    this.SetHair(sex, hairId, hairColorId);
    this.SetEquipBody(sex, equip_body_item_id);
    this.SetEquipHead(sex, equip_head_item_id);
    this.SetEquipWeapon(sex, equip_weapon_item_id);
    this.SetEquipArm(sex, equip_arm_item_id);
    this.SetEquipLeg(sex, equip_leg_item_id);
  }

  public void Apply(
    CharaInfo info,
    bool need_weapon,
    bool need_helm,
    bool need_leg,
    bool is_priority_visual_equip)
  {
    this.SetFace(info.sex, info.faceId, info.skinId);
    this.SetHair(info.sex, info.hairId, info.hairColorId);
    bool show_helm = info.showHelm != 0;
    if (is_priority_visual_equip)
    {
      this.SetEquipBody(info.sex, (uint) info.aId);
      if (show_helm && need_helm)
        this.SetEquipHead(info.sex, (uint) info.hId);
      this.SetEquipArm(info.sex, (uint) info.rId);
      if (info.lId != 0)
        this.SetEquipLeg(info.sex, (uint) info.lId);
    }
    info.equipSet.ForEach((Action<CharaInfo.EquipItem>) (o => this.SetEquip(info.sex, (uint) o.eId, false, show_helm, need_weapon)));
    if (!need_leg)
      this.legModelID = -1;
    else if (this.legModelID == -1)
      this.SetEquipLeg(info.sex, 0U);
    if (this.bodyModelID == -1)
      this.SetEquipBody(info.sex, 10100U);
    this.SetActionVoiceBaseID(info.sex, info.voiceId);
    this.ApplyAccessory(info.accessory);
  }

  public static PlayerLoadInfo FromCharaInfo(
    CharaInfo chara_info,
    bool need_weapon,
    bool need_helm,
    bool need_leg,
    bool is_priority_visual_equip)
  {
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    playerLoadInfo.Apply(chara_info, need_weapon, need_helm, need_leg, is_priority_visual_equip);
    return playerLoadInfo;
  }

  public static PlayerLoadInfo FromUserStatus(
    bool need_weapon,
    bool is_priority_visual_equip,
    int set_no = -1)
  {
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    playerLoadInfo.ApplyUserStatus(need_weapon, is_priority_visual_equip, set_no);
    return playerLoadInfo;
  }

  public static PlayerLoadInfo FromUserUniqueStatus(
    bool need_weapon,
    bool is_priority_visual_equip,
    int set_no = -1)
  {
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    playerLoadInfo.ApplyUserUniqueStatus(need_weapon, is_priority_visual_equip, set_no);
    return playerLoadInfo;
  }

  public static PlayerLoadInfo GenerateForTutorial(
    int sex,
    uint weaponId,
    uint bodyId,
    uint headId,
    uint armId,
    uint legId)
  {
    PlayerLoadInfo forTutorial = new PlayerLoadInfo();
    forTutorial.SetFace(sex, 0, 0);
    forTutorial.SetHair(sex, 0, 0);
    forTutorial.SetEquipWeapon(sex, weaponId);
    forTutorial.SetEquipBody(sex, bodyId);
    forTutorial.SetEquipHead(sex, headId);
    forTutorial.SetEquipArm(sex, armId);
    forTutorial.SetEquipLeg(sex, legId);
    forTutorial.SetActionVoiceBaseID(sex, 0);
    return forTutorial;
  }

  private void ApplyAccessory(AccessoryPlaceInfo info)
  {
    this.accUIDs.Clear();
    int index1 = 0;
    for (int count1 = info.ids.Count; index1 < count1; ++index1)
    {
      uint tableId = MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetTableID(info.ids[index1]);
      if (tableId != 0U)
      {
        List<AccessoryTable.AccessoryInfoData> infoList = Singleton<AccessoryTable>.I.GetInfoList(tableId);
        if (!infoList.IsNullOrEmpty<AccessoryTable.AccessoryInfoData>())
        {
          int num = int.Parse(info.parts[index1]);
          int index2 = 0;
          for (int count2 = infoList.Count; index2 < count2; ++index2)
          {
            AccessoryTable.AccessoryInfoData accessoryInfoData = infoList[index2];
            if (accessoryInfoData.attachPlace == (ACCESSORY_PART) num)
            {
              this.accUIDs.Add(accessoryInfoData.id);
              break;
            }
          }
        }
      }
    }
  }

  private void ApplyAccessory(List<CharaInfo.UserAccessory> list)
  {
    this.accUIDs.Clear();
    if (list.IsNullOrEmpty<CharaInfo.UserAccessory>())
      return;
    int index1 = 0;
    for (int count1 = list.Count; index1 < count1; ++index1)
    {
      CharaInfo.UserAccessory userAccessory = list[index1];
      List<AccessoryTable.AccessoryInfoData> infoList = Singleton<AccessoryTable>.I.GetInfoList((uint) userAccessory.accessoryId);
      if (!infoList.IsNullOrEmpty<AccessoryTable.AccessoryInfoData>())
      {
        int index2 = 0;
        for (int count2 = infoList.Count; index2 < count2; ++index2)
        {
          AccessoryTable.AccessoryInfoData accessoryInfoData = infoList[index2];
          if (accessoryInfoData.attachPlace == (ACCESSORY_PART) userAccessory.place)
          {
            this.accUIDs.Add(accessoryInfoData.id);
            break;
          }
        }
      }
    }
  }
}

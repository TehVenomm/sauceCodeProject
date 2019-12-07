using Network;
using System;
using System.Collections.Generic;
using System.Text;

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
		stringBuilder.AppendFormat("- isNeedToCache: {0}\n", isNeedToCache);
		stringBuilder.AppendFormat("- faceModelID: {0}\n", faceModelID);
		stringBuilder.AppendFormat("- hairModelID: {0}\n", hairModelID);
		stringBuilder.AppendFormat("- headModelID: {0}\n", headModelID);
		stringBuilder.AppendFormat("- bodyModelID: {0}\n", bodyModelID);
		stringBuilder.AppendFormat("- armModelID: {0}\n", armModelID);
		stringBuilder.AppendFormat("- legModelID: {0}\n", legModelID);
		stringBuilder.AppendFormat("- weaponModelID: {0}\n", weaponModelID);
		stringBuilder.AppendFormat("- skinColor: {0}\n", skinColor);
		stringBuilder.AppendFormat("- hairColor: {0}\n", hairColor);
		stringBuilder.AppendFormat("- headColor: {0}\n", headColor);
		stringBuilder.AppendFormat("- bodyColor: {0}\n", bodyColor);
		stringBuilder.AppendFormat("- armColor: {0}\n", armColor);
		stringBuilder.AppendFormat("- legColor: {0}\n", legColor);
		stringBuilder.AppendFormat("- weaponColor0: {0}\n", weaponColor0);
		stringBuilder.AppendFormat("- weaponColor1: {0}\n", weaponColor1);
		stringBuilder.AppendFormat("- weaponColor2: {0}\n", weaponColor2);
		stringBuilder.AppendFormat("- weaponEffectID: {0}\n", weaponEffectID);
		stringBuilder.AppendFormat("- weaponEffectParam: {0}\n", weaponEffectParam);
		stringBuilder.AppendFormat("- weaponEffectColor: {0}\n", weaponEffectColor);
		stringBuilder.AppendFormat("- weaponEvolveId: {0}\n", weaponEvolveId);
		stringBuilder.AppendFormat("- equipType: {0}\n", equipType);
		stringBuilder.AppendFormat("- weaponSpAttackType: {0}\n", weaponSpAttackType);
		stringBuilder.AppendFormat("- actionVoiceBaseID: {0}\n", actionVoiceBaseID);
		stringBuilder.AppendFormat("- accUIDs:\n");
		foreach (uint accUID in accUIDs)
		{
			stringBuilder.AppendFormat("-- {0}\n", accUID);
		}
		return stringBuilder.ToString();
	}

	public bool Equals(PlayerLoadInfo info)
	{
		if (info.faceModelID != faceModelID)
		{
			return false;
		}
		if (info.hairModelID != hairModelID)
		{
			return false;
		}
		if (info.headModelID != headModelID)
		{
			return false;
		}
		if (info.bodyModelID != bodyModelID)
		{
			return false;
		}
		if (info.armModelID != armModelID)
		{
			return false;
		}
		if (info.legModelID != legModelID)
		{
			return false;
		}
		if (info.weaponModelID != weaponModelID)
		{
			return false;
		}
		if (info.skinColor != skinColor)
		{
			return false;
		}
		if (info.hairColor != hairColor)
		{
			return false;
		}
		if (info.headColor != headColor)
		{
			return false;
		}
		if (info.bodyColor != bodyColor)
		{
			return false;
		}
		if (info.armColor != armColor)
		{
			return false;
		}
		if (info.legColor != legColor)
		{
			return false;
		}
		if (info.weaponColor0 != weaponColor0)
		{
			return false;
		}
		if (info.weaponColor1 != weaponColor1)
		{
			return false;
		}
		if (info.weaponColor2 != weaponColor2)
		{
			return false;
		}
		if (info.weaponEffectID != weaponEffectID)
		{
			return false;
		}
		if (info.weaponEffectParam != weaponEffectParam)
		{
			return false;
		}
		if (info.weaponEffectColor != weaponEffectColor)
		{
			return false;
		}
		if (info.weaponEvolveId != weaponEvolveId)
		{
			return false;
		}
		if (info.equipType != equipType)
		{
			return false;
		}
		if (info.weaponSpAttackType != weaponSpAttackType)
		{
			return false;
		}
		if (info.actionVoiceBaseID != actionVoiceBaseID)
		{
			return false;
		}
		if (!EqualsAccessory(info.accUIDs))
		{
			return false;
		}
		return true;
	}

	private bool EqualsAccessory(List<uint> _accUIDs)
	{
		if (accUIDs.Count != _accUIDs.Count)
		{
			return false;
		}
		int i = 0;
		for (int count = accUIDs.Count; i < count; i++)
		{
			if (!_accUIDs.Contains(accUIDs[i]))
			{
				return false;
			}
		}
		return true;
	}

	public PlayerLoadInfo Clone()
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.faceModelID = faceModelID;
		playerLoadInfo.hairModelID = hairModelID;
		playerLoadInfo.headModelID = headModelID;
		playerLoadInfo.bodyModelID = bodyModelID;
		playerLoadInfo.armModelID = armModelID;
		playerLoadInfo.legModelID = legModelID;
		playerLoadInfo.weaponModelID = weaponModelID;
		playerLoadInfo.skinColor = skinColor;
		playerLoadInfo.hairColor = hairColor;
		playerLoadInfo.headColor = headColor;
		playerLoadInfo.bodyColor = bodyColor;
		playerLoadInfo.armColor = armColor;
		playerLoadInfo.legColor = legColor;
		playerLoadInfo.weaponColor0 = weaponColor0;
		playerLoadInfo.weaponColor1 = weaponColor1;
		playerLoadInfo.weaponColor2 = weaponColor2;
		playerLoadInfo.weaponEffectID = weaponEffectID;
		playerLoadInfo.weaponEffectParam = weaponEffectParam;
		playerLoadInfo.weaponEffectColor = weaponEffectColor;
		playerLoadInfo.weaponEvolveId = weaponEvolveId;
		playerLoadInfo.equipType = equipType;
		playerLoadInfo.weaponSpAttackType = weaponSpAttackType;
		playerLoadInfo.actionVoiceBaseID = actionVoiceBaseID;
		playerLoadInfo.accUIDs.Clear();
		playerLoadInfo.accUIDs.AddRange(accUIDs);
		return playerLoadInfo;
	}

	public void SetFace(int sex, int face_type_id, int skin_color_id)
	{
		faceModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetFaceModelID(sex, face_type_id);
		skinColor = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(skin_color_id));
	}

	public void SetHair(int sex, int hair_style_id, int hair_color_id)
	{
		hairModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairModelID(sex, hair_style_id);
		hairColor = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(hair_color_id));
	}

	public void SetEquip(int sex, uint equip_item_id, bool overwrite = true, bool enable_head = true, bool enable_weapon = true)
	{
		if (equip_item_id != 0)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id);
			if (equipItemData != null)
			{
				SetEquip(sex, equipItemData, overwrite, enable_head, enable_weapon);
			}
		}
	}

	public void SetEquip(int sex, EquipItemTable.EquipItemData item_data, bool overwrite = true, bool enable_head = true, bool enable_weapon = true)
	{
		switch (item_data.type)
		{
		case EQUIPMENT_TYPE.ARMOR:
		case EQUIPMENT_TYPE.VISUAL_ARMOR:
			if (overwrite || bodyModelID == -1)
			{
				SetEquipBody(sex, item_data);
			}
			break;
		case EQUIPMENT_TYPE.HELM:
		case EQUIPMENT_TYPE.VISUAL_HELM:
			if (enable_head && (overwrite || headModelID == -1))
			{
				SetEquipHead(sex, item_data);
			}
			break;
		case EQUIPMENT_TYPE.ARM:
		case EQUIPMENT_TYPE.VISUAL_ARM:
			if (overwrite || armModelID == -1)
			{
				SetEquipArm(sex, item_data);
			}
			break;
		case EQUIPMENT_TYPE.LEG:
		case EQUIPMENT_TYPE.VISUAL_LEG:
			if (overwrite || legModelID == -1)
			{
				SetEquipLeg(sex, item_data);
			}
			break;
		default:
			if (enable_weapon && (overwrite || weaponModelID == -1))
			{
				SetEquipWeapon(sex, item_data);
			}
			break;
		}
	}

	public void RemoveEquip(int sex, int slot_index)
	{
		switch (slot_index)
		{
		case 3:
			SetEquipBody(sex, null);
			break;
		case 5:
			SetEquipArm(sex, null);
			break;
		case 6:
			SetEquipLeg(sex, null);
			break;
		case 4:
			SetEquipHead(sex, null);
			break;
		default:
			SetEquipWeapon(sex, null);
			break;
		}
	}

	public void SetEquipBody(int sex, uint equip_body_item_id)
	{
		SetEquipBody(sex, (equip_body_item_id != 0) ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_body_item_id) : null);
	}

	public void SetEquipBody(int sex, EquipItemTable.EquipItemData body_item_data)
	{
		if (body_item_data != null)
		{
			bodyModelID = body_item_data.GetModelID(sex);
			bodyColor = body_item_data.modelColor0;
		}
		else
		{
			bodyModelID = -1;
			bodyColor = -1;
		}
	}

	public void SetEquipArm(int sex, uint equip_arm_item_id)
	{
		SetEquipArm(sex, (equip_arm_item_id != 0) ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_arm_item_id) : null);
	}

	public void SetEquipArm(int sex, EquipItemTable.EquipItemData arm_item_data)
	{
		if (arm_item_data != null)
		{
			armModelID = arm_item_data.GetModelID(sex);
			armColor = arm_item_data.modelColor0;
		}
		else
		{
			armModelID = -1;
			armColor = -1;
		}
	}

	public void SetEquipLeg(int sex, uint equip_leg_item_id)
	{
		SetEquipLeg(sex, (equip_leg_item_id != 0) ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_leg_item_id) : null);
	}

	public void SetEquipLeg(int sex, EquipItemTable.EquipItemData leg_item_data)
	{
		if (leg_item_data != null)
		{
			legModelID = leg_item_data.GetModelID(sex);
			legColor = leg_item_data.modelColor0;
		}
		else
		{
			legModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.primeLegIDs[sex];
			legColor = -1;
		}
	}

	public void SetEquipHead(int sex, uint equip_head_item_id)
	{
		SetEquipHead(sex, (equip_head_item_id != 0) ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_head_item_id) : null);
	}

	public void SetEquipHead(int sex, EquipItemTable.EquipItemData head_item_data)
	{
		if (head_item_data != null)
		{
			headModelID = head_item_data.GetModelID(sex);
			headColor = head_item_data.modelColor0;
		}
		else
		{
			headModelID = -1;
			headColor = -1;
		}
	}

	public void SetEquipWeapon(int sex, uint equip_weapon_item_id)
	{
		SetEquipWeapon(sex, (equip_weapon_item_id != 0) ? Singleton<EquipItemTable>.I.GetEquipItemData(equip_weapon_item_id) : null);
	}

	public void SetEquipWeapon(int sex, EquipItemTable.EquipItemData weapon_item_data)
	{
		if (weapon_item_data != null)
		{
			weaponModelID = weapon_item_data.GetModelID(sex);
			weaponColor0 = weapon_item_data.modelColor0;
			weaponColor1 = weapon_item_data.modelColor1;
			weaponColor2 = weapon_item_data.modelColor2;
			weaponEffectID = weapon_item_data.effectID;
			weaponEffectParam = weapon_item_data.effectParam;
			weaponEffectColor = weapon_item_data.effectColor;
			weaponEvolveId = weapon_item_data.evolveId;
			equipType = (uint)weapon_item_data.type;
			weaponSpAttackType = (uint)weapon_item_data.spAttackType;
		}
		else
		{
			weaponModelID = -1;
			weaponColor0 = -1;
			weaponColor1 = -1;
			weaponColor2 = -1;
			weaponEffectID = 0;
			weaponEffectParam = 0f;
			weaponEffectColor = -1;
			weaponEvolveId = 0u;
			equipType = 0u;
			weaponSpAttackType = 0u;
		}
	}

	public void SetActionVoiceBaseID(int sex, int voice_type_id)
	{
		if (voice_type_id >= MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount)
		{
			voice_type_id = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount - 1;
		}
		actionVoiceBaseID = (voice_type_id * 10 + sex) * 10000;
	}

	public void ApplyUserStatus(bool need_weapon, bool is_priority_visual_equip, int set_no = -1)
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		uint num = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(0, set_no);
		uint num2 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armorUniqId) : 0u;
		uint num3 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.helmUniqId) : 0u;
		uint num4 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armUniqId) : 0u;
		uint num5 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.legUniqId) : 0u;
		if (num2 == 0)
		{
			num2 = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(3, set_no);
		}
		if (num3 == 0)
		{
			num3 = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(4, set_no);
		}
		if (num4 == 0)
		{
			num4 = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(5, set_no);
		}
		if (num5 == 0)
		{
			num5 = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemTableID(6, set_no);
		}
		if (MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(set_no) == 0)
		{
			num3 = 0u;
		}
		if (num == 0)
		{
			num = 100u;
		}
		if (num2 == 0)
		{
			num2 = 10100u;
		}
		if (!need_weapon)
		{
			num = 0u;
		}
		ApplyAccessory(MonoBehaviourSingleton<StatusManager>.I.GetEquippingAccessoryInfo(set_no));
		SetupLoadInfo(num, num2, num3, num4, num5);
	}

	public void ApplyUserUniqueStatus(bool need_weapon, bool is_priority_visual_equip, int set_no = -1)
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		uint weapon_id = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(0, set_no);
		uint num = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armorUniqId) : 0u;
		uint num2 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.helmUniqId) : 0u;
		uint num3 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armUniqId) : 0u;
		uint num4 = is_priority_visual_equip ? MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.legUniqId) : 0u;
		if (num == 0)
		{
			num = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(3, set_no);
		}
		if (num2 == 0)
		{
			num2 = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(4, set_no);
		}
		if (num3 == 0)
		{
			num3 = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(5, set_no);
		}
		if (num4 == 0)
		{
			num4 = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingItemTableID(6, set_no);
		}
		if (MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingShowHelm(set_no) == 0)
		{
			num2 = 0u;
		}
		if (num == 0)
		{
			num = 10100u;
		}
		if (!need_weapon)
		{
			weapon_id = 0u;
		}
		ApplyAccessory(MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquippingAccessoryInfo(set_no));
		SetupLoadInfo(weapon_id, num, num2, num3, num4);
	}

	public void SetupLoadInfo(EquipSetInfo equip_set, ulong weapon_uniq_id, ulong armor_uniq_id, ulong helm_uniq_id, ulong arm_uniq_id, ulong leg_uniq_id, bool show_helm)
	{
		EquipItemInfo equipItemInfo = (weapon_uniq_id == 0L) ? equip_set.item[0] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(weapon_uniq_id);
		EquipItemInfo equipItemInfo2 = (armor_uniq_id == 0L) ? equip_set.item[3] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(armor_uniq_id);
		EquipItemInfo equipItemInfo3 = (!show_helm) ? null : ((helm_uniq_id == 0L) ? equip_set.item[4] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(helm_uniq_id));
		EquipItemInfo equipItemInfo4 = (arm_uniq_id == 0L) ? equip_set.item[5] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(arm_uniq_id);
		EquipItemInfo equipItemInfo5 = (leg_uniq_id == 0L) ? equip_set.item[6] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(leg_uniq_id);
		ApplyAccessory(equip_set.acc);
		SetupLoadInfo(equipItemInfo?.tableID ?? 0, equipItemInfo2?.tableID ?? 0, equipItemInfo3?.tableID ?? 0, equipItemInfo4?.tableID ?? 0, equipItemInfo5?.tableID ?? 0);
	}

	public void SetUpCharaMakeLoadInfo(EquipSetInfo equip_set, ulong armor_uniq_id, ulong helm_uniq_id, ulong arm_uniq_id, ulong leg_uniq_id, int sex, bool isShowhelm)
	{
		EquipItemInfo equipItemInfo = (armor_uniq_id == 0L) ? equip_set.item[3] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(armor_uniq_id);
		EquipItemInfo equipItemInfo2 = (arm_uniq_id == 0L) ? equip_set.item[5] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(arm_uniq_id);
		EquipItemInfo equipItemInfo3 = (helm_uniq_id == 0L) ? equip_set.item[4] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(helm_uniq_id);
		EquipItemInfo equipItemInfo4 = (leg_uniq_id == 0L) ? equip_set.item[6] : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(leg_uniq_id);
		if (equipItemInfo != null)
		{
			SetEquipBody(sex, equipItemInfo.tableID);
		}
		if (equipItemInfo2 != null)
		{
			SetEquipArm(sex, equipItemInfo2.tableID);
		}
		if (equipItemInfo4 != null)
		{
			SetEquipLeg(sex, equipItemInfo4.tableID);
		}
		else
		{
			SetEquipLeg(sex, null);
		}
		if (isShowhelm && equipItemInfo3 != null)
		{
			SetEquipHead(sex, equipItemInfo3.tableID);
		}
		ApplyAccessory(equip_set.acc);
	}

	public void SetupLoadInfo(uint weapon_id, uint armor_id, uint helm_id, uint arm_id, uint leg_id)
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		int sex = userStatus.sex;
		int faceId = userStatus.faceId;
		int skinId = userStatus.skinId;
		int hairId = userStatus.hairId;
		int hairColorId = userStatus.hairColorId;
		SetFace(sex, faceId, skinId);
		SetHair(sex, hairId, hairColorId);
		SetEquipBody(sex, armor_id);
		SetEquipHead(sex, helm_id);
		SetEquipWeapon(sex, weapon_id);
		SetEquipArm(sex, arm_id);
		SetEquipLeg(sex, leg_id);
	}

	public void Apply(CharaInfo info, bool need_weapon, bool need_helm, bool need_leg, bool is_priority_visual_equip)
	{
		SetFace(info.sex, info.faceId, info.skinId);
		SetHair(info.sex, info.hairId, info.hairColorId);
		bool show_helm = info.showHelm != 0;
		if (is_priority_visual_equip)
		{
			SetEquipBody(info.sex, (uint)info.aId);
			if (show_helm && need_helm)
			{
				SetEquipHead(info.sex, (uint)info.hId);
			}
			SetEquipArm(info.sex, (uint)info.rId);
			if (info.lId != 0)
			{
				SetEquipLeg(info.sex, (uint)info.lId);
			}
		}
		info.equipSet.ForEach(delegate(CharaInfo.EquipItem o)
		{
			SetEquip(info.sex, (uint)o.eId, overwrite: false, show_helm, need_weapon);
		});
		if (!need_leg)
		{
			legModelID = -1;
		}
		else if (legModelID == -1)
		{
			SetEquipLeg(info.sex, 0u);
		}
		if (bodyModelID == -1)
		{
			SetEquipBody(info.sex, 10100u);
		}
		SetActionVoiceBaseID(info.sex, info.voiceId);
		ApplyAccessory(info.accessory);
	}

	public static PlayerLoadInfo FromCharaInfo(CharaInfo chara_info, bool need_weapon, bool need_helm, bool need_leg, bool is_priority_visual_equip)
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.Apply(chara_info, need_weapon, need_helm, need_leg, is_priority_visual_equip);
		return playerLoadInfo;
	}

	public static PlayerLoadInfo FromUserStatus(bool need_weapon, bool is_priority_visual_equip, int set_no = -1)
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.ApplyUserStatus(need_weapon, is_priority_visual_equip, set_no);
		return playerLoadInfo;
	}

	public static PlayerLoadInfo FromUserUniqueStatus(bool need_weapon, bool is_priority_visual_equip, int set_no = -1)
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.ApplyUserUniqueStatus(need_weapon, is_priority_visual_equip, set_no);
		return playerLoadInfo;
	}

	public static PlayerLoadInfo GenerateForTutorial(int sex, uint weaponId, uint bodyId, uint headId, uint armId, uint legId)
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.SetFace(sex, 0, 0);
		playerLoadInfo.SetHair(sex, 0, 0);
		playerLoadInfo.SetEquipWeapon(sex, weaponId);
		playerLoadInfo.SetEquipBody(sex, bodyId);
		playerLoadInfo.SetEquipHead(sex, headId);
		playerLoadInfo.SetEquipArm(sex, armId);
		playerLoadInfo.SetEquipLeg(sex, legId);
		playerLoadInfo.SetActionVoiceBaseID(sex, 0);
		return playerLoadInfo;
	}

	private void ApplyAccessory(AccessoryPlaceInfo info)
	{
		accUIDs.Clear();
		int i = 0;
		for (int count = info.ids.Count; i < count; i++)
		{
			uint tableID = MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetTableID(info.ids[i]);
			if (tableID == 0)
			{
				continue;
			}
			List<AccessoryTable.AccessoryInfoData> infoList = Singleton<AccessoryTable>.I.GetInfoList(tableID);
			if (infoList.IsNullOrEmpty())
			{
				continue;
			}
			int num = int.Parse(info.parts[i]);
			int j = 0;
			for (int count2 = infoList.Count; j < count2; j++)
			{
				AccessoryTable.AccessoryInfoData accessoryInfoData = infoList[j];
				if (accessoryInfoData.attachPlace == (ACCESSORY_PART)num)
				{
					accUIDs.Add(accessoryInfoData.id);
					break;
				}
			}
		}
	}

	private void ApplyAccessory(List<CharaInfo.UserAccessory> list)
	{
		accUIDs.Clear();
		if (list.IsNullOrEmpty())
		{
			return;
		}
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			CharaInfo.UserAccessory userAccessory = list[i];
			List<AccessoryTable.AccessoryInfoData> infoList = Singleton<AccessoryTable>.I.GetInfoList((uint)userAccessory.accessoryId);
			if (infoList.IsNullOrEmpty())
			{
				continue;
			}
			int j = 0;
			for (int count2 = infoList.Count; j < count2; j++)
			{
				AccessoryTable.AccessoryInfoData accessoryInfoData = infoList[j];
				if (accessoryInfoData.attachPlace == (ACCESSORY_PART)userAccessory.place)
				{
					accUIDs.Add(accessoryInfoData.id);
					break;
				}
			}
		}
	}
}

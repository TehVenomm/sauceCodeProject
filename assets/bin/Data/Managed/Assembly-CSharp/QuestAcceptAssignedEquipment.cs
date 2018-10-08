using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptAssignedEquipment : SkillInfoBase
{
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
		LBL_ASSIGNED_SET_NAME
	}

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

	private UI[] icons = new UI[7]
	{
		UI.OBJ_ICON_WEAPON_1,
		UI.OBJ_ICON_WEAPON_2,
		UI.OBJ_ICON_WEAPON_3,
		UI.OBJ_ICON_ARMOR,
		UI.OBJ_ICON_HELM,
		UI.OBJ_ICON_ARM,
		UI.OBJ_ICON_LEG
	};

	private UI[] iconsBtn = new UI[7]
	{
		UI.BTN_ICON_WEAPON_1,
		UI.BTN_ICON_WEAPON_2,
		UI.BTN_ICON_WEAPON_3,
		UI.BTN_ICON_ARMOR,
		UI.BTN_ICON_HELM,
		UI.BTN_ICON_ARM,
		UI.BTN_ICON_LEG
	};

	private UI[] iconsLevel = new UI[7]
	{
		UI.LBL_LEVEL_WEAPON_1,
		UI.LBL_LEVEL_WEAPON_2,
		UI.LBL_LEVEL_WEAPON_3,
		UI.LBL_LEVEL_ARMOR,
		UI.LBL_LEVEL_HELM,
		UI.LBL_LEVEL_ARM,
		UI.LBL_LEVEL_LEG
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
			deliveryData = (GameSection.GetEventData() as DeliveryTable.DeliveryData);
			if (deliveryData == null)
			{
				Log.Error("DeliveryDataが存在しません");
			}
			else
			{
				targetData = Singleton<AssignedEquipmentTable>.I.GetAssignedEquipmentDataFromDeliveryId(deliveryData.id);
				if (targetData == null)
				{
					Log.Error("依頼ID:{0}の指定装備デ\u30fcタが存在しません", deliveryData.id);
				}
				record = CreatePlayerRecord();
				LoadModel();
				GameSection.SetEventData(null);
				nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
				setInfo = CreateEquipSetInfo();
				allEquipItemAndSkillData = new EquipItemAndSkillData[7];
				base.Initialize();
			}
		}
	}

	public override void UpdateUI()
	{
		UpdateStatusUI();
		UpdateEquipIcon();
		UpdateEnemyInfo();
		UpdateEquipSetInfo();
	}

	protected override void OnClose()
	{
	}

	private void UpdateEquipIcon()
	{
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		if (setInfo != null)
		{
			int i = 0;
			for (int num = 7; i < num; i++)
			{
				SetEvent(GetCtrl(icons[i]), "EMPTY", 0);
				SetEvent(GetCtrl(iconsBtn[i]), "EMPTY", 0);
				SetLabelText(GetCtrl(iconsLevel[i]), string.Empty);
			}
			int j = 0;
			for (int num2 = setInfo.item.Length; j < num2; j++)
			{
				ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
				RARITY_TYPE? nullable = null;
				ELEMENT_TYPE eLEMENT_TYPE = ELEMENT_TYPE.MAX;
				int num3 = -1;
				EquipItemInfo equipItemInfo = setInfo.item[j];
				if (equipItemInfo == null)
				{
					SetActive(GetCtrl(iconsBtn[j]), false);
				}
				else
				{
					EquipItemTable.EquipItemData equipItemData = null;
					if (equipItemInfo != null)
					{
						equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemInfo.tableID);
					}
					if (equipItemInfo != null && equipItemInfo.tableID != 0)
					{
						num3 = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
						SetActive(GetCtrl(iconsLevel[j]), true);
						string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), equipItemInfo.level.ToString());
						SetLabelText(GetCtrl(iconsLevel[j]), text);
					}
					Transform ctrl = GetCtrl(icons[j]);
					ctrl.GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
					int k = 0;
					for (int count = Temporary.itemIconList.Count; k < count; k++)
					{
						Temporary.itemIconList[k].get_gameObject().SetActive(true);
					}
					Temporary.itemIconList.Clear();
					ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, ctrl, null, -1, "EQUIP", j, false, -1, false, null, false, false);
					if (itemIcon != null)
					{
						SetLongTouch(itemIcon.transform, "DETAIL", j);
						SetEvent(GetCtrl(iconsBtn[j]), "DETAIL", j);
						itemIcon.get_gameObject().SetActive(num3 != -1);
						if (num3 != -1)
						{
							itemIcon.SetEquipExtInvertedColor(equipItemInfo, base.GetComponent<UILabel>((Enum)iconsLevel[j]));
						}
					}
					UpdateEquipSkillButton(equipItemInfo, j);
				}
			}
			ResetTween((Enum)UI.OBJ_EQUIP_ROOT, 0);
			PlayTween((Enum)UI.OBJ_EQUIP_ROOT, true, (EventDelegate.Callback)null, false, 0);
		}
	}

	private void UpdateEnemyInfo()
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(deliveryData.needs[0].questId);
		if (questData != null)
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
			if (enemyData != null)
			{
				SetLabelText((Enum)UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, enemyData.level));
				SetLabelText((Enum)UI.LBL_ENEMY_NAME, enemyData.name);
				Transform ctrl = GetCtrl(UI.OBJ_ENEMY);
				ItemIcon itemIcon = ItemIcon.Create(ItemIcon.GetItemIconType(questData.questType), enemyData.iconId, questData.rarity, GetCtrl(UI.OBJ_ENEMY), enemyData.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
				itemIcon.SetEnableCollider(false);
				SetActive((Enum)UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
				SetElementSprite((Enum)UI.SPR_ELEMENT, (int)enemyData.element);
				SetElementSprite((Enum)UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
				SetActive((Enum)UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
			}
		}
	}

	private void UpdateEquipSetInfo()
	{
		SetLabelText((Enum)UI.LBL_ASSIGNED_SET_NAME, targetData.setName);
		EquipItemTable.EquipItemData tableData = allEquipItemInfo[0].tableData;
		if (tableData != null)
		{
			SetEquipmentTypeIcon((Enum)UI.SPR_TYPE_ICON_WEP, (Enum)UI.SPR_TYPE_ICON_BG, (Enum)UI.SPR_TYPE_ICON_RARITY, tableData);
			SetActive((Enum)UI.SPR_TYPE_ICON_RARITY, false);
			SetSprite((Enum)UI.SPR_SP_ATTACK_TYPE, tableData.spAttackType.GetBigFrameSpriteName());
		}
	}

	private void LoadModel()
	{
		if (record != null)
		{
			PlayerLoadInfo playerLoadInfo = record.playerLoadInfo;
			if (record.playerLoadInfo.weaponModelID == -1)
			{
				record.playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, false, -1);
				record.animID = -1;
				playerLoadInfo = record.playerLoadInfo;
			}
			SetRenderPlayerModel(playerLoadInfo);
		}
	}

	private PlayerLoadInfo CreatePlayerLoadInfo()
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		uint weapon_id = 0u;
		uint armor_id = 0u;
		uint helm_id = 0u;
		uint arm_id = 0u;
		uint leg_id = 0u;
		for (int i = 0; i < targetData.equipmentData.Length; i++)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(targetData.equipmentData[i].id);
			switch (equipItemData.type)
			{
			case EQUIPMENT_TYPE.ARM:
				arm_id = equipItemData.id;
				break;
			case EQUIPMENT_TYPE.HELM:
				helm_id = equipItemData.id;
				break;
			case EQUIPMENT_TYPE.LEG:
				leg_id = equipItemData.id;
				break;
			case EQUIPMENT_TYPE.ARMOR:
				armor_id = equipItemData.id;
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		SetRenderPlayerModel(base._transform, UI.TEX_MODEL, load_player_info, record.animID, new Vector3(0f, -0.75f, 14f), new Vector3(0f, 180f, 0f), false, delegate(PlayerLoader player_loader)
		{
			if (player_loader != null)
			{
				loader = player_loader;
			}
		});
	}

	private void UpdateStatusUI()
	{
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
		if (equips == null)
		{
			equips = new List<CharaInfo.EquipItem>();
		}
		else
		{
			equips.Clear();
		}
		for (int i = 0; i < allEquipItemInfo.Length; i++)
		{
			if (allEquipItemInfo[i] != null)
			{
				CharaInfo.EquipItem equipItem = GetEquipItem(allEquipItemInfo[i]);
				if (equipItem != null)
				{
					equips.Add(equipItem);
				}
			}
		}
		otherEquipSetCalculator.SetEquipSet(equips, false);
		SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
		int attacksSum = finalStatus.GetAttacksSum();
		int defencesSum = finalStatus.GetDefencesSum();
		int hp = finalStatus.hp;
		int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
		SetLabelText((Enum)UI.LBL_ATK, attacksSum.ToString());
		SetLabelText((Enum)UI.LBL_DEF, defencesSum.ToString());
		SetLabelText((Enum)UI.LBL_HP, hp.ToString());
		SetLabelText((Enum)UI.LBL_LEVEL, num.ToString());
	}

	private void UpdateEquipSkillButton(EquipItemInfo item, int i)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(iconsBtn[i]);
		bool flag = item != null && item.tableID != 0;
		if (flag)
		{
			SkillSlotUIData[] assignedSkillSlotData = GetAssignedSkillSlotData(item);
			SetSkillIconButton(ctrl, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", item.tableData, assignedSkillSlotData, "SKILL_ICON_BUTTON", i);
		}
		FindCtrl(ctrl, UI.OBJ_SKILL_BUTTON_ROOT).get_gameObject().SetActive(flag);
	}

	private EquipItemAndSkillData CreateEquipItemAndSkillData(int index)
	{
		if (allEquipItemAndSkillData[index] != null)
		{
			return allEquipItemAndSkillData[index];
		}
		EquipItemAndSkillData equipItemAndSkillData = new EquipItemAndSkillData();
		equipItemAndSkillData.skillSlotUIData = GetAssignedSkillSlotData(equipItemAndSkillData.equipItemInfo = allEquipItemInfo[index]);
		allEquipItemAndSkillData[index] = equipItemAndSkillData;
		return equipItemAndSkillData;
	}

	private SkillSlotUIData[] GetAssignedSkillSlotData(EquipItemInfo equipInfo)
	{
		if (equipInfo == null)
		{
			return null;
		}
		AssignedEquipmentTable.EquipmentData targetEquipData = GetTargetEquipData((int)equipInfo.tableData.id);
		if (targetEquipData == null)
		{
			return null;
		}
		int maxSlot = equipInfo.GetMaxSlot();
		if (maxSlot == 0)
		{
			return null;
		}
		SkillItemTable.SkillSlotData[] skillSlot = equipInfo.tableData.GetSkillSlot(equipInfo.exceed);
		List<SkillItemInfo> list = new List<SkillItemInfo>(3);
		for (int i = 0; i < targetEquipData.skillIds.Length && maxSlot > i; i++)
		{
			if (targetEquipData.skillIds[i] != 0)
			{
				SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(targetEquipData.skillIds[i]);
				SkillItemInfo item = new SkillItemInfo(i, (int)skillItemData.id, skillItemData.GetMaxLv(0), 0);
				list.Add(item);
			}
			else
			{
				list.Add(null);
			}
		}
		SkillItemInfo[] array = list.ToArray();
		SkillSlotUIData[] array2 = new SkillSlotUIData[maxSlot];
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] == null)
			{
				array2[j] = new SkillSlotUIData();
				array2[j].slotData = new SkillItemTable.SkillSlotData(0u, equipInfo.tableData.GetSkillSlot(equipInfo.exceed)[j].slotType);
			}
			else if (array[j].tableData.type != skillSlot[j].slotType)
			{
				Log.Error("スロットタイプが合致しません " + array[j].tableData.id);
			}
			else
			{
				array2[j] = new SkillSlotUIData();
				array2[j].slotData = new SkillItemTable.SkillSlotData(array[j].tableData.id, skillSlot[j].slotType);
				array2[j].itemData = array[j];
			}
		}
		return array2;
	}

	private AssignedEquipmentTable.EquipmentData GetTargetEquipData(int id)
	{
		for (int i = 0; i < targetData.equipmentData.Length; i++)
		{
			if (id == targetData.equipmentData[i].id)
			{
				return targetData.equipmentData[i];
			}
		}
		return null;
	}

	private CharaInfo.EquipItem GetEquipItem(EquipItemInfo info)
	{
		if (info == null)
		{
			return null;
		}
		CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
		equipItem.eId = (int)info.tableID;
		equipItem.lv = info.level;
		equipItem.exceed = info.exceed;
		SkillSlotUIData[] assignedSkillSlotData = GetAssignedSkillSlotData(info);
		for (int i = 0; i < assignedSkillSlotData.Length; i++)
		{
			SkillItemInfo itemData = assignedSkillSlotData[i].itemData;
			if (itemData != null)
			{
				equipItem.sIds.Add((int)itemData.tableID);
				equipItem.sLvs.Add(itemData.level);
				equipItem.sExs.Add(itemData.exceedCnt);
			}
		}
		for (int j = 0; j < info.ability.Length; j++)
		{
			if (info.ability[j] != null && info.ability[j].id != 0)
			{
				equipItem.aIds.Add((int)info.ability[j].id);
				equipItem.aPts.Add(info.ability[j].ap);
			}
		}
		return equipItem;
	}

	private InGameRecorder.PlayerRecord CreatePlayerRecord()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
		playerRecord.id = 0;
		playerRecord.isNPC = false;
		playerRecord.isSelf = true;
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
		playerRecord.playerLoadInfo = CreatePlayerLoadInfo();
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
		playerRecord.charaInfo.equipSet = null;
		playerRecord.charaInfo.showHelm = 1;
		return playerRecord;
	}

	private List<EquipItem.Ability> GetAssignedAbilityList(AssignedEquipmentTable.EquipmentData assignedData)
	{
		if (assignedData == null)
		{
			return null;
		}
		List<EquipItem.Ability> list = new List<EquipItem.Ability>();
		uint[] abilityIds = assignedData.abilityIds;
		int[] abilityPts = assignedData.abilityPts;
		for (int i = 0; i < abilityIds.Length; i++)
		{
			if (abilityIds[i] != 0)
			{
				EquipItem.Ability ability = new EquipItem.Ability();
				ability.id = (int)abilityIds[i];
				ability.pt = abilityPts[i];
				list.Add(ability);
			}
		}
		return list;
	}

	private void CreateEquipItemInfo()
	{
		EquipSet equipSet = new EquipSet();
		equipSet.setNo = 1;
		for (int i = 0; i < targetData.equipmentData.Length; i++)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(targetData.equipmentData[i].id);
			EquipItem equipItem = new EquipItem();
			equipItem.uniqId = string.Empty;
			equipItem.equipItemId = (int)equipItemData.id;
			equipItem.level = equipItemData.maxLv;
			equipItem.exceed = ((equipItemData.exceedID != 0) ? 4 : 0);
			equipItem.ability = GetAssignedAbilityList(targetData.equipmentData[i]);
			switch (equipItemData.type)
			{
			case EQUIPMENT_TYPE.ARM:
				equipSet.arm = equipItem;
				break;
			case EQUIPMENT_TYPE.HELM:
				equipSet.helm = equipItem;
				break;
			case EQUIPMENT_TYPE.LEG:
				equipSet.leg = equipItem;
				break;
			case EQUIPMENT_TYPE.ARMOR:
				equipSet.armor = equipItem;
				break;
			default:
				equipSet.weapon_0 = equipItem;
				break;
			}
		}
		equipSet.weapon_1 = null;
		equipSet.weapon_2 = null;
		equipSet.setName = string.Empty;
		equipSet.showHelm = 1;
		allEquipItemInfo = new EquipItemInfo[7]
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
		if (allEquipItemInfo == null)
		{
			CreateEquipItemInfo();
		}
		return new EquipSetInfo(allEquipItemInfo, string.Empty, 1, new AccessoryPlaceInfo());
	}

	private List<CharaInfo.EquipItem> CreateEquipItemListForRecord(List<CharaInfo.EquipItem> equips)
	{
		if (equips == null)
		{
			return null;
		}
		for (int i = 0; i < equips.Count; i++)
		{
			equips[i].aIds.Clear();
			equips[i].aPts.Clear();
			List<EquipItem.Ability> list = null;
			AssignedEquipmentTable.EquipmentData[] equipmentData = targetData.equipmentData;
			foreach (AssignedEquipmentTable.EquipmentData equipmentData2 in equipmentData)
			{
				List<int> list2 = new List<int>(2);
				List<int> list3 = new List<int>(2);
				if (equipmentData2.id == equips[i].eId)
				{
					List<EquipItem.Ability> assignedAbilityList = GetAssignedAbilityList(equipmentData2);
					if (assignedAbilityList != null)
					{
						foreach (EquipItem.Ability item in assignedAbilityList)
						{
							list2.Add(item.id);
							list3.Add(item.pt);
						}
						equips[i].aIds = list2;
						equips[i].aPts = list3;
					}
				}
			}
		}
		return equips;
	}

	private void OnEnable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	private void OnDisable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		nowSectionName = string.Empty;
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nowSectionName)
		{
			loader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}

	private void OnQuery_ABILITY()
	{
		GameSection.SetEventData(new object[3]
		{
			setInfo,
			MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(setInfo, null),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(record.charaInfo.atk, record.charaInfo.def, record.charaInfo.hp, equips)
		});
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			array,
			false,
			false
		});
	}

	private void OnQuery_STATUS()
	{
		GameSection.SetEventData(new object[3]
		{
			setInfo,
			MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(setInfo, null),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(record.charaInfo.atk, record.charaInfo.def, record.charaInfo.hp, equips)
		});
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			array,
			false,
			false
		});
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		if (setInfo.item[num] == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			object[] array = new object[4]
			{
				ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
				CreateEquipItemAndSkillData(num),
				record.charaInfo.sex,
				record.charaInfo.faceId
			};
			GameSection.SetEventData(new object[3]
			{
				array,
				false,
				false
			});
		}
	}

	private void OnQuery_SKILL_ICON_BUTTON()
	{
		int num = (int)GameSection.GetEventData();
		if (setInfo.item[num] == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
				CreateEquipItemAndSkillData(num),
				record.charaInfo.sex
			});
		}
	}

	protected unsafe void OnQuery_START()
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(deliveryData.needs[0].questId);
		if (questData == null)
		{
			GameSceneEvent.Cancel();
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, true);
			record.charaInfo.equipSet = CreateEquipItemListForRecord(equips);
			GameSection.StayEvent();
			AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData = targetData;
			CharaInfo charaInfo = record.charaInfo;
			if (_003C_003Ef__am_0024cacheD == null)
			{
				_003C_003Ef__am_0024cacheD = new Action<bool, bool, bool, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			CoopApp.EnterQuestOfflineAssignedEquipment(assignedEquipmentData, charaInfo, _003C_003Ef__am_0024cacheD);
		}
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.Help + "/" + targetData.helpUrl);
	}
}

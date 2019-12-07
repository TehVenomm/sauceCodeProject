using Network;
using System;
using UnityEngine;

public class QuestAcceptSeriesArenaRoom : OffLineQuestRoomBase
{
	private enum UI
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
		SPR_USER_EQUIPING
	}

	private bool isReady;

	private QuestTable.QuestTableData questData;

	private DeliveryTable.DeliveryData deliveryData;

	private int orderNo;

	public override void Initialize()
	{
		base.Initialize();
		uint currentQuestID = MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
		deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(currentQuestID);
		questData = deliveryData.GetQuestData();
	}

	public override void UpdateUI()
	{
		UpdateUser();
		UpdateTopBar();
		UpdateEnemyList();
		UpdateStartButton();
		UpdateSubMission();
	}

	protected new void UpdateUser()
	{
		isReady = true;
		SetGrid(UI.GRD_PLAYER_INFO, "", 3, reset: false, delegate(int i, Transform t)
		{
			string prefab_name = "QuestRoomUserInfoSelf";
			return Realizes(prefab_name, t, check_panel: false);
		}, delegate(int i, Transform t, bool is_recycle)
		{
			UpdateRoomUserInfo(t, i);
			SetEvent(t, UI.BTN_NAME_BG, "CHANGE_EQUIP", i + 1);
			SetEvent(t, UI.BTN_FRAME, "CHANGE_EQUIP", i + 1);
		});
	}

	protected override void UpdateRoomUserInfo(Transform trans, int index)
	{
		SetLabelText(trans, UI.LBL_ATK, string.Empty);
		SetLabelText(trans, UI.LBL_DEF, string.Empty);
		SetLabelText(trans, UI.LBL_HP, string.Empty);
		SetLabelText(trans, UI.LBL_NAME, string.Empty);
		SetLabelText(trans, UI.LBL_LV, string.Empty);
		SetActive(trans, UI.SPR_USER_EQUIPING, is_visible: false);
		base.UpdateRoomUserInfo(trans, index);
		if (!(trans.GetComponent<QuestRoomUserInfo>() == null))
		{
			userInfo = GetUserCharaInfo(index);
			if (userInfo == null)
			{
				ActiveAndTween(trans, UI.SPR_USER_EQUIPING, is_active: true);
				isReady = false;
			}
			else
			{
				SetLabelText(trans, UI.LBL_NAME, userInfo.equipSetName);
			}
		}
	}

	private void OnQuery_CHANGE_EQUIP()
	{
		int order = (int)GameSection.GetEventData();
		MonoBehaviourSingleton<StatusManager>.I.SetSelectUniqueEquipSetNo(MonoBehaviourSingleton<StatusManager>.I.GetOrderUniqueEquipSetNo(order));
	}

	private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
	{
		SetActive(root, _enum, is_active);
		if (is_active)
		{
			ResetTween(root, _enum);
			PlayTween(root, _enum, forward: true, null, is_input_block: false);
		}
	}

	protected override CharaInfo GetUserCharaInfo(int setNo)
	{
		orderNo = setNo + 1;
		return MonoBehaviourSingleton<StatusManager>.I.GetCreateUniquePlayerInfo(orderNo).charaInfo;
	}

	protected override EquipSetCalculator GetUserEquipCalculator()
	{
		int orderUniqueEquipSetNo = MonoBehaviourSingleton<StatusManager>.I.GetOrderUniqueEquipSetNo(orderNo);
		return MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSetCalculator(orderUniqueEquipSetNo);
	}

	private void UpdateTopBar()
	{
		int num = (int)questData.limitTime;
		SetLabelText(UI.LBL_LIMIT_TIME, $"{num / 60}:{num % 60:D2}");
		SetLabelText(UI.LBL_SERIES_ARENA_NAME, deliveryData.name);
		ResourceLoad.LoadWithSetUITexture(GetCtrl(UI.TEX_ICON).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(questData.rarity));
	}

	private void UpdateEnemyList()
	{
		SetGrid(UI.GRD_ENEMY_INFO, "QuestSeriesArenaRoomEnemyListItem", questData.enemyID.Length, reset: false, delegate(int i, Transform t, bool b)
		{
			InitEnemyItem(i, t, b);
		});
	}

	private void OnQuery_ENEMY_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[2]
		{
			deliveryData,
			num
		});
	}

	private void UpdateSubMission()
	{
		UI[] array = new UI[3]
		{
			UI.LBL_MISSION_INFO_1,
			UI.LBL_MISSION_INFO_2,
			UI.LBL_MISSION_INFO_3
		};
		UI[] array2 = new UI[3]
		{
			UI.SPR_MISSION_CROWN_ON_1,
			UI.SPR_MISSION_CROWN_ON_2,
			UI.SPR_MISSION_CROWN_ON_3
		};
		UI[] array3 = new UI[3]
		{
			UI.SPR_MISSION_CROWN_OFF_1,
			UI.SPR_MISSION_CROWN_OFF_2,
			UI.SPR_MISSION_CROWN_OFF_3
		};
		QuestInfoData.Mission[] array4 = null;
		array4 = QuestInfoData.CreateMissionData(questData);
		if (array4 != null)
		{
			for (int i = 0; i < array4.Length; i++)
			{
				SetLabelText(array[i], array4[i].tableData.missionText);
				bool flag = array4[i].state >= CLEAR_STATUS.CLEAR;
				SetActive(array2[i], flag);
				SetActive(array3[i], !flag);
			}
		}
	}

	private void InitEnemyItem(int i, Transform t, bool isRecycle)
	{
		int num = questData.enemyLv[i];
		uint id = (uint)questData.enemyID[i];
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(id);
		if (enemyData != null)
		{
			SetLabelText(t, UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, num));
			SetLabelText(t, UI.LBL_ENEMY_NAME, enemyData.name);
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
			itemIcon.SetEnableCollider(is_enable: false);
			itemIcon.rarityFrame.spriteName = "MonsterFrame_CD";
			UIBehaviour.SetRarityColorType(1, itemIcon.rarityFrame);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
			SetEvent(t, UI.BTN_DETAIL, "ENEMY_DETAIL", i);
			SetEvent(t, UI.BTN_INFO_WINDOW, "ENEMY_DETAIL", i);
		}
	}

	private void UpdateStartButton()
	{
		SetActive(UI.BTN_START, isReady);
		SetActive(UI.BTN_NG, !isReady);
	}

	protected void OnQuery_START()
	{
		StartQuest();
	}

	private void StartQuest()
	{
		GameSection.StayEvent();
		CoopApp.EnterSeriesArenaQuestOffline(delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
		{
			GameSection.ResumeEvent(isStart);
		});
	}

	public void OnQuery_TO_MISSION()
	{
		GetComponent<UITweenCtrl>(UI.OBJ_ENEMYS_ROOT).Play();
	}

	public void OnQuery_TO_ENEMY()
	{
		GetComponent<UITweenCtrl>(UI.OBJ_ENEMYS_ROOT).Play(forward: false);
	}
}

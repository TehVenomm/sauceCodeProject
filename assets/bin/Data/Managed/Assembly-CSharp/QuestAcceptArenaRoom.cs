using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptArenaRoom : OffLineQuestRoomBase
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
		LBL_ARENA_NAME,
		LBL_LIMIT_TIME,
		TBL_LIST,
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
		LBL_LIMIT,
		LBL_CONDITION,
		SPR_TYPE_DIFFICULTY
	}

	private ArenaTable.ArenaData arenaData;

	private DeliveryTable.DeliveryData deliveryData;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "ArenaTable";
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		arenaData = Singleton<ArenaTable>.I.GetArenaData(MonoBehaviourSingleton<QuestManager>.I.currentArenaId);
		deliveryData = (GameSection.GetEventData() as DeliveryTable.DeliveryData);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdateTopBar();
		UpdateEnemyList();
		UpdateLimitText();
		UpdateConditionText();
		UpdateStartButton();
		SetDifficultySprite();
	}

	private void UpdateTopBar()
	{
		int num = QuestUtility.ToSecByMilliSec(arenaData.timeLimit);
		SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{num / 60}:{num % 60:D2}");
		string empty = string.Empty;
		if (deliveryData != null)
		{
			empty = QuestUtility.GetArenaTitle(arenaData.group, deliveryData.name);
		}
		else
		{
			string str = StringTable.Format(STRING_CATEGORY.ARENA, 0u, arenaData.group);
			string str2 = StringTable.Format(STRING_CATEGORY.ARENA, 1u, arenaData.rank);
			empty = str + "\u3000" + str2;
		}
		SetLabelText((Enum)UI.LBL_ARENA_NAME, empty);
		UITexture component = GetCtrl(UI.TEX_ICON).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(arenaData.rank));
	}

	private unsafe void UpdateEnemyList()
	{
		if (arenaData != null)
		{
			List<QuestTable.QuestTableData> questDataArray = arenaData.GetQuestDataArray();
			_003CUpdateEnemyList_003Ec__AnonStorey41F _003CUpdateEnemyList_003Ec__AnonStorey41F;
			SetTable(UI.TBL_LIST, "QuestArenaRoomEnemyListItem", questDataArray.Count, false, new Action<int, Transform, bool>((object)_003CUpdateEnemyList_003Ec__AnonStorey41F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void InitEnemyItem(int i, Transform t, bool isRecycle, QuestTable.QuestTableData questData)
	{
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		if (enemyData != null)
		{
			SetLabelText(t, UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, arenaData.level));
			SetLabelText(t, UI.LBL_ENEMY_NAME, enemyData.name);
			ItemIcon itemIcon = ItemIcon.Create(ItemIcon.GetItemIconType(questData.questType), enemyData.iconId, questData.rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
			itemIcon.SetEnableCollider(false);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
	}

	private void UpdateLimitText()
	{
		SetLabelText((Enum)UI.LBL_LIMIT, QuestUtility.GetLimitText(arenaData));
	}

	private void UpdateConditionText()
	{
		SetLabelText((Enum)UI.LBL_CONDITION, QuestUtility.GetConditionText(arenaData));
	}

	private void UpdateStartButton()
	{
		bool flag = QuestUtility.JudgeLimit(arenaData, userInfo.equipSet);
		SetActive((Enum)UI.BTN_START, flag);
		SetActive((Enum)UI.BTN_NG, !flag);
	}

	protected void OnQuery_START()
	{
		StartQuest();
	}

	private unsafe void StartQuest()
	{
		GameSection.StayEvent();
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Action<bool, bool, bool, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		CoopApp.EnterArenaQuestOffline(_003C_003Ef__am_0024cache2);
	}

	private void SetDifficultySprite()
	{
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryData != null && deliveryData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}

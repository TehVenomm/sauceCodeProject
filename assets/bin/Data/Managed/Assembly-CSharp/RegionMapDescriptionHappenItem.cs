using Network;
using UnityEngine;

public class RegionMapDescriptionHappenItem : UIBehaviour
{
	private enum UI
	{
		OBJ_ENEMY,
		SPR_ENM_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		OBJ_SUBMISSION_ROOT,
		OBJ_MISSION_INFO_1,
		OBJ_MISSION_INFO_2,
		OBJ_MISSION_INFO_3,
		SPR_MISSION_INFO_CROWN,
		LBL_MISSION_INFO,
		LBL_MONSTER_NAME,
		LBL_MONSTER_LEVEL,
		STR_UNKNOWN_WEAK_ELEMENT
	}

	public void SetUp(QuestTable.QuestTableData quest)
	{
		Transform transform = GetTransform();
		if (quest != null)
		{
			SetUpEnemy(transform, quest);
			SetUpSubMissions(transform, quest);
		}
	}

	private void SetUpEnemy(Transform t, QuestTable.QuestTableData quest)
	{
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)quest.GetMainEnemyID());
		if (enemyData != null)
		{
			ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(quest.questID);
			bool flag = clearStatusQuestData != null;
			int icon_id = 10999;
			string text = "？？？？？";
			string text2 = "？？";
			if (flag)
			{
				icon_id = enemyData.iconId;
				text = enemyData.name;
				text2 = quest.GetMainEnemyLv().ToString();
			}
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, icon_id, null, FindCtrl(t, UI.OBJ_ENEMY));
			itemIcon.SetDepth(7);
			SetElementSprite(t, UI.SPR_ENM_ELEMENT, (int)enemyData.element);
			SetActive(t, UI.SPR_ENM_ELEMENT, flag);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.SPR_WEAK_ELEMENT, flag);
			bool flag2 = enemyData.weakElement == ELEMENT_TYPE.MAX;
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, flag2 && flag);
			SetActive(t, UI.STR_UNKNOWN_WEAK_ELEMENT, !flag);
			SetLabelText(t, UI.LBL_MONSTER_NAME, text);
			SetLabelText(t, UI.LBL_MONSTER_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, text2));
		}
	}

	private void SetUpSubMissions(Transform t, QuestTable.QuestTableData quest)
	{
		UI[] array = new UI[3]
		{
			UI.OBJ_MISSION_INFO_1,
			UI.OBJ_MISSION_INFO_2,
			UI.OBJ_MISSION_INFO_3
		};
		if (!quest.IsMissionExist())
		{
			SetActive(t, UI.OBJ_SUBMISSION_ROOT, is_visible: false);
			return;
		}
		SetActive(t, UI.OBJ_SUBMISSION_ROOT, is_visible: true);
		ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(quest.questID);
		if (clearStatusQuestData == null)
		{
			int i = 0;
			for (int num = quest.missionID.Length; i < num; i++)
			{
				uint num2 = quest.missionID[i];
				SetActive(t, array[i], num2 != 0);
				SetSubMissionNotCleared(FindCtrl(t, array[i]), num2);
			}
			return;
		}
		int j = 0;
		for (int count = clearStatusQuestData.missionStatus.Count; j < count; j++)
		{
			uint num3 = quest.missionID[j];
			SetActive(t, array[j], num3 != 0);
			CLEAR_STATUS clearStatus = (CLEAR_STATUS)clearStatusQuestData.missionStatus[j];
			SetSubMissionCleared(FindCtrl(t, array[j]), num3, clearStatus);
		}
	}

	private void SetSubMission(Transform parent, uint missionID, bool isCleared)
	{
		SetActive(parent, UI.SPR_MISSION_INFO_CROWN, isCleared);
		SetActive(parent, UI.LBL_MISSION_INFO, is_visible: true);
		QuestTable.MissionTableData missionData = Singleton<QuestTable>.I.GetMissionData(missionID);
		SetLabelText(parent, UI.LBL_MISSION_INFO, missionData.missionText);
	}

	private void SetSubMissionNotCleared(Transform parent, uint missionID)
	{
		SetSubMission(parent, missionID, isCleared: false);
	}

	private void SetSubMissionCleared(Transform parent, uint missionID, CLEAR_STATUS clearStatus)
	{
		SetSubMission(parent, missionID, clearStatus >= CLEAR_STATUS.CLEAR);
	}

	private Transform GetTransform()
	{
		Transform transform = base._transform;
		if (transform == null)
		{
			transform = this.get_transform();
		}
		return transform;
	}
}

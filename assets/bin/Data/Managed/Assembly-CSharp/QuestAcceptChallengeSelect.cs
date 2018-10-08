using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptChallengeSelect : QuestAcceptSelect
{
	private class QuestDataSet
	{
		public QuestInfoData questInfoData;

		public QuestTable.QuestTableData tableData;

		public QuestDataSet(QuestData questData, QuestTable.QuestTableData tableData)
		{
			questInfoData = MonoBehaviourSingleton<QuestManager>.I.CreateQuestChallengeInfoData(questData, tableData);
			this.tableData = tableData;
		}
	}

	private List<QuestDataSet> enableQuestList;

	private int selectedQuestIndex;

	public void OnCloseDialog_QuestAcceptChallengeRoomSettings()
	{
		_OnCloseRoomSettings();
	}

	public override void Initialize()
	{
		root = SetPrefab(base.collectUI, "QuestAcceptChallengeSelect", true);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return (object)_Initialize();
		bool sended = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeEnmey(questInfo.questData.tableData.enemyID[0], delegate(bool isSuccess, QuestChallengeEnemyModel.Param result)
		{
			((_003CDoInitialize_003Ec__Iterator124)/*Error near IL_006a: stateMachine*/)._003C_003Ef__this.OnSendFinished(isSuccess, result);
			((_003CDoInitialize_003Ec__Iterator124)/*Error near IL_006a: stateMachine*/)._003Csended_003E__0 = true;
		});
		if (!sended)
		{
			yield return (object)null;
		}
		InitializeBase();
	}

	private void OnSendFinished(bool isSuccess, QuestChallengeEnemyModel.Param result)
	{
		SetupQuestList(result.shadow);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdateEnemyLevelLabel();
		UpdateEnemyLevelButton();
		UpdateButtons();
	}

	private void UpdateEnemyLevelLabel()
	{
		string text = StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, GetSelectedQuestDataSet().tableData.enemyLv[0]);
		SetLabelText(UI.LBL_ENEMY_LEVEL, text);
	}

	private void UpdateEnemyLevelButton()
	{
		if (selectedQuestIndex >= enableQuestList.Count - 1)
		{
			SetColor(UI.OBJ_LEVEL_R, Color.clear);
			SetActive(UI.OBJ_LEVEL_INACTIVE_R, true);
		}
		else
		{
			SetColor(UI.OBJ_LEVEL_R, Color.white);
			SetActive(UI.OBJ_LEVEL_INACTIVE_R, false);
		}
		if (selectedQuestIndex <= 0)
		{
			SetColor(UI.OBJ_LEVEL_L, Color.clear);
			SetActive(UI.OBJ_LEVEL_INACTIVE_L, true);
		}
		else
		{
			SetColor(UI.OBJ_LEVEL_L, Color.white);
			SetActive(UI.OBJ_LEVEL_INACTIVE_L, false);
		}
	}

	private void UpdateButtons()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			GetCtrl(UI.BTN_GUILD_REQUEST).localPosition = new Vector3(136f, 10f, 0f);
			GetCtrl(UI.BTN_GUILD_REQUEST).localScale = new Vector3(0.462f, 0.462f, 0f);
			GetCtrl(UI.BTN_PARTY).localPosition = new Vector3(-34f, 10f, 0f);
			GetCtrl(UI.BTN_PARTY).localScale = new Vector3(0.462f, 0.462f, 0f);
		}
	}

	private void OnQuery_LEVEL_R()
	{
		selectedQuestIndex++;
		OnLevelLRButton();
	}

	private void OnQuery_LEVEL_L()
	{
		selectedQuestIndex--;
		OnLevelLRButton();
	}

	private void OnLevelLRButton()
	{
		questInfo = GetSelectedQuestDataSet().questInfoData;
		RefreshUI();
	}

	private QuestDataSet GetSelectedQuestDataSet()
	{
		return enableQuestList[selectedQuestIndex];
	}

	private void SetupQuestList(List<QuestData> allQuest)
	{
		int enemyLevelFromUserLevel = MonoBehaviourSingleton<UserInfoManager>.I.GetEnemyLevelFromUserLevel();
		enableQuestList = new List<QuestDataSet>();
		int i = 0;
		for (int count = allQuest.Count; i < count; i++)
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)allQuest[i].questId);
			if (questData.enemyLv[0] == Singleton<QuestTable>.I.GetQuestData(questInfo.questData.tableData.questID).enemyLv[0])
			{
				selectedQuestIndex = i;
			}
			if (questData.enemyLv[0] <= enemyLevelFromUserLevel)
			{
				enableQuestList.Add(new QuestDataSet(allQuest[i], questData));
			}
		}
	}

	protected override void OnQuery_CREATE_ROOM()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questInfo.questData.tableData.questID, true);
		base.OnQuery_CREATE_ROOM();
	}

	protected override void OnQuery_GUILD_REQUEST()
	{
		GameSection.SetEventData(questInfo);
	}
}

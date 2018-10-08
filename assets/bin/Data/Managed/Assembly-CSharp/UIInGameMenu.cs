using UnityEngine;

public class UIInGameMenu : MonoBehaviourSingleton<UIInGameMenu>
{
	[SerializeField]
	protected GameObject partyMenuUI;

	[SerializeField]
	protected GameObject normalMenuUI;

	[SerializeField]
	protected GameObject happenMenuUI;

	[SerializeField]
	protected GameObject retryableMenuUI;

	[SerializeField]
	protected UILabel partyNumber;

	[SerializeField]
	protected GameObject m_missionRoot;

	[SerializeField]
	protected UILabel[] m_missionTexts;

	[SerializeField]
	protected GameObject[] m_missionCrownOn;

	[SerializeField]
	protected GameObject[] m_missionCrownOff;

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().SetActive(false);
	}

	public void Initialize()
	{
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
		{
			partyMenuUI.SetActive(true);
			normalMenuUI.SetActive(false);
			happenMenuUI.SetActive(false);
			retryableMenuUI.SetActive(false);
			string text = MonoBehaviourSingleton<PartyManager>.I.GetPartyNumber();
			if (string.IsNullOrEmpty(text))
			{
				partyNumber.text = "-";
			}
			else
			{
				partyNumber.text = text;
			}
		}
		else if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
		{
			partyMenuUI.SetActive(false);
			normalMenuUI.SetActive(false);
			happenMenuUI.SetActive(true);
			retryableMenuUI.SetActive(false);
			QuestInfoData.Mission[] array = null;
			array = QuestInfoData.CreateMissionData(questData);
			if (array != null)
			{
				int i = 0;
				for (int num = array.Length; i < num; i++)
				{
					QuestInfoData.Mission mission = array[i];
					m_missionCrownOn[i].SetActive(CLEAR_STATUS.CLEAR == mission.state);
					m_missionCrownOff[i].SetActive(CLEAR_STATUS.CLEAR != mission.state);
					m_missionTexts[i].text = mission.tableData.missionText;
				}
			}
		}
		else if (questData != null && questData.questType == QUEST_TYPE.ARENA)
		{
			partyMenuUI.SetActive(false);
			normalMenuUI.SetActive(false);
			happenMenuUI.SetActive(false);
			retryableMenuUI.SetActive(true);
		}
		else
		{
			partyMenuUI.SetActive(false);
			normalMenuUI.SetActive(true);
			happenMenuUI.SetActive(false);
			retryableMenuUI.SetActive(false);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		this.get_gameObject().SetActive(true);
	}

	public void OnClickClose()
	{
		MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
		Close();
	}

	public void Close()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(false);
	}

	public void OnClickOption()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameMenu.OnClickOption", this.get_gameObject(), "OPTION", null, null, true);
		}
	}

	public void OnClickRetire()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			if (FieldManager.IsValidInGameNoBoss())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self != null && self.rescueTime > 0f)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", this.get_gameObject(), "RETIRE", StringTable.Get(STRING_CATEGORY.IN_GAME, 1008u), null, true);
				}
				else
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", this.get_gameObject(), "RETIRE", StringTable.Get(STRING_CATEGORY.IN_GAME, 1009u), null, true);
				}
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameMenu.OnClickRetire", this.get_gameObject(), "RETIRE", null, null, true);
			}
		}
	}

	public void DoRetire()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
		}
	}

	public void OnClickRetry()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameMenu.OnClickRetry", this.get_gameObject(), "RETRY", null, null, true);
		}
	}
}

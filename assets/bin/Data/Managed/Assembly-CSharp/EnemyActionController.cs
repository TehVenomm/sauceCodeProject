using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController
{
	public enum REGION_FLAG
	{
		DEAD,
		ALIVE,
		REVIVABLE
	}

	public enum EXACTION_CONDITION
	{
		NONE,
		SHIELD_ON,
		MAX
	}

	[Serializable]
	public class ActionInfo
	{
		public EnemyActionTable.EnemyActionData data;

		public List<int> useAliveRegionIDs = new List<int>();

		public List<int> useDeadRegionIDs = new List<int>();

		public List<int> useReviveRegionIDs = new List<int>();

		public bool isForceDisable;
	}

	public GrabController grabController;

	private Brain brain;

	private Enemy enemy;

	public List<ActionInfo> actions = new List<ActionInfo>();

	public ActionInfo EMPTY_ACTION = new ActionInfo
	{
		data = new EnemyActionTable.EnemyActionData()
	};

	private List<EnemyAngryTable.Data> m_angryDataList = new List<EnemyAngryTable.Data>();

	private List<uint> m_execAngryIdList = new List<uint>();

	public int m_angryStartIndex = -1;

	private List<EnemyActionTable.EnemyActionData> m_basisActionDataList = new List<EnemyActionTable.EnemyActionData>();

	private List<EnemyActionTable.EnemyActionData> m_exActionDataList = new List<EnemyActionTable.EnemyActionData>();

	private EnemyTable.EnemyData m_enemyData;

	private int m_nowActionID;

	public int modeId = 1;

	public int nowIndex
	{
		get;
		protected set;
	}

	public int oldIndex
	{
		get;
		protected set;
	}

	public ActionInfo nowAction
	{
		get
		{
			if (nowIndex >= 0 && nowIndex < actions.Count)
			{
				return actions[nowIndex];
			}
			return EMPTY_ACTION;
		}
	}

	public int canUseCount
	{
		get;
		private set;
	}

	public int totalWeight
	{
		get;
		private set;
	}

	public uint actionID
	{
		get;
		private set;
	}

	public EnemyActionController(Brain brain)
	{
		this.brain = brain;
		enemy = (brain.owner as Enemy);
		oldIndex = -1;
		grabController = new GrabController();
	}

	public int GetCounterAttackId()
	{
		for (int i = 0; i < actions.Count; i++)
		{
			if (actions[i].data.isCounterAttack && (actions[i].data.modeId <= 0 || actions[i].data.modeId == modeId))
			{
				for (int j = 0; j < actions[i].data.combiActionTypeInfos.Length; j++)
				{
					if (actions[i].data.combiActionTypeInfos[j].type == EnemyActionTable.ACTION_TYPE.ATTACK)
					{
						return actions[i].data.combiActionTypeInfos[j].id;
					}
				}
			}
		}
		return 2147483647;
	}

	public int GetNowModeCounterModeId()
	{
		for (int i = 0; i < actions.Count; i++)
		{
			if (actions[i].data.isCounterAttack && (actions[i].data.modeId <= 0 || actions[i].data.modeId == modeId))
			{
				return actions[i].data.modeId;
			}
		}
		return -1;
	}

	public void LoadTable()
	{
		actionID = 0u;
		modeId = 1;
		if (Singleton<EnemyActionTable>.IsValid())
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)enemy.enemyID);
			if (enemyData != null)
			{
				actionID = (uint)enemyData.actionId;
				List<EnemyActionTable.EnemyActionData> enemyActionList = Singleton<EnemyActionTable>.I.GetEnemyActionList(actionID);
				if (enemyActionList != null)
				{
					m_basisActionDataList = enemyActionList;
					if (enemy.ExActionID > 0)
					{
						m_exActionDataList = Singleton<EnemyActionTable>.I.GetEnemyActionList((uint)enemy.ExActionID);
					}
					m_enemyData = enemyData;
					PrepareActionInfoList(enemyActionList, enemyData.actionId);
				}
			}
		}
	}

	private void PrepareActionInfoList(List<EnemyActionTable.EnemyActionData> actionDataList, int nextActionID)
	{
		if (m_nowActionID != nextActionID)
		{
			actions = new List<ActionInfo>();
			m_angryDataList = new List<EnemyAngryTable.Data>();
			m_nowActionID = nextActionID;
			actionDataList.ForEach(delegate(EnemyActionTable.EnemyActionData data)
			{
				ActionInfo actionInfo = new ActionInfo
				{
					data = data
				};
				int num = data.regionFlags.Length;
				for (int i = 0; i < num; i++)
				{
					EnemyActionTable.EnemyActionData.RegionFlag regionFlag = data.regionFlags[i];
					if (regionFlag.name.Length > 0)
					{
						int regionID = enemy.GetRegionID(regionFlag.name);
						if (regionID > 0)
						{
							switch (regionFlag.flag)
							{
							case 0:
								actionInfo.useDeadRegionIDs.Add(regionID);
								break;
							case 1:
								actionInfo.useAliveRegionIDs.Add(regionID);
								break;
							case 2:
								actionInfo.useReviveRegionIDs.Add(regionID);
								break;
							}
						}
					}
				}
				EnemyActionTable.ActionTypeInfo[] combiActionTypeInfos = actionInfo.data.combiActionTypeInfos;
				if (combiActionTypeInfos != null && combiActionTypeInfos.Length > 0)
				{
					int num2 = combiActionTypeInfos.Length;
					for (int j = 0; j < num2; j++)
					{
						EnemyActionTable.ACTION_TYPE type = combiActionTypeInfos[j].type;
						if (type == EnemyActionTable.ACTION_TYPE.ANGRY)
						{
							uint angryId = actionInfo.data.angryId;
							if (Singleton<EnemyAngryTable>.IsValid() && angryId != 0)
							{
								EnemyAngryTable.Data data2 = Singleton<EnemyAngryTable>.I.GetData(angryId);
								if (data2 != null)
								{
									data2.actionID = actionInfo.data.actionID;
									m_angryDataList.Add(data2);
								}
							}
							actionInfo.data.isUse = false;
						}
					}
				}
				if (data.startWaitInterval > 0f)
				{
					data.startWaitTime = Time.get_time();
				}
				if ((int)m_enemyData.level >= data.useLvLimit)
				{
					actions.Add(actionInfo);
				}
			});
		}
	}

	private bool canActionWithAliveRegion(ActionInfo action)
	{
		EnemyRegionWork[] works = enemy.regionWorks;
		int num = action.useAliveRegionIDs.Find((int id) => id < works.Length && (int)works[id].hp <= 0);
		if (num > 0)
		{
			return false;
		}
		int num2 = action.useDeadRegionIDs.Find((int id) => (id < works.Length && (int)works[id].hp > 0) || enemy.IsEnableReviveRegion(id));
		if (num2 > 0)
		{
			return false;
		}
		int num3 = action.useReviveRegionIDs.Find((int id) => !enemy.IsEnableReviveRegion(id));
		if (num3 > 0)
		{
			return false;
		}
		return true;
	}

	private bool CheckActionByAngryCondition(ActionInfo action)
	{
		uint validAngryId = action.data.validAngryId;
		if (validAngryId == 0)
		{
			return true;
		}
		return enemy.NowAngryID == validAngryId;
	}

	protected virtual int GetWeight(ActionInfo action, DISTANCE d, PLACE p)
	{
		int num = 0;
		int num2 = action.data.distanceWeights[(int)d];
		int num3 = action.data.placeWeights[(int)p];
		num = num2 * num3;
		if (brain.opponentMem.counter.nearNum >= 2)
		{
			num += action.data.nearMultiPlayerWeight;
		}
		return num;
	}

	private void UpdateActionInfoList()
	{
		EnemyTable.EnemyData enemyData = m_enemyData;
		EXACTION_CONDITION exActionCondition = (EXACTION_CONDITION)enemy.ExActionCondition;
		switch (exActionCondition)
		{
		case EXACTION_CONDITION.SHIELD_ON:
			if (enemy.ExActionID > 0 && m_exActionDataList != null && m_exActionDataList.Count > 0)
			{
				bool flag = false;
				EXACTION_CONDITION eXACTION_CONDITION = exActionCondition;
				if (eXACTION_CONDITION == EXACTION_CONDITION.SHIELD_ON)
				{
					flag = enemy.IsValidShield();
				}
				if (flag)
				{
					PrepareActionInfoList(m_exActionDataList, enemy.ExActionID);
				}
				else
				{
					PrepareActionInfoList(m_basisActionDataList, enemyData.actionId);
				}
			}
			break;
		}
	}

	public void SelectAction()
	{
		UpdateActionInfoList();
		canUseCount = 0;
		totalWeight = 0;
		int[] array = new int[actions.Count];
		int continuout_index = -1;
		int i = 0;
		for (int count = actions.Count; i < count; i++)
		{
			ActionInfo actionInfo = actions[i];
			bool flag = actionInfo.data.isUse;
			if (actionInfo.isForceDisable)
			{
				flag = false;
			}
			if (actionInfo.data.modeId > 0 && actionInfo.data.modeId != modeId)
			{
				flag = false;
			}
			if (flag)
			{
				canUseCount++;
				if (canActionWithAliveRegion(actionInfo) && CheckActionByAngryCondition(actionInfo) && (!(actionInfo.data.startWaitInterval > 0f) || !(Time.get_time() - actionInfo.data.startWaitTime < actionInfo.data.startWaitInterval)) && (!(actionInfo.data.lotteryWaitInterval > 0f) || !(Time.get_time() - actionInfo.data.lotteryWaitTime < actionInfo.data.lotteryWaitInterval)))
				{
					int num = 0;
					OpponentMemory.OpponentRecord opponent = brain.targetCtrl.GetOpponent();
					if (opponent != null)
					{
						num = GetWeight(actionInfo, opponent.record.distanceType, opponent.record.place);
					}
					array[i] = num;
					if (oldIndex == i && enemy.isBoss)
					{
						array[i] /= 2;
						if (num > 0)
						{
							continuout_index = i;
						}
					}
					totalWeight += array[i];
				}
			}
		}
		if (canUseCount > 0 && !SetupAngryStartAction())
		{
			if (totalWeight <= 0)
			{
				SetupActionIndexWhenNoWeight(continuout_index);
			}
			else
			{
				int num2 = 0;
				int num3 = Random.Range(0, totalWeight) + 1;
				int j = 0;
				for (int count2 = actions.Count; j < count2; j++)
				{
					if (array[j] > 0)
					{
						num2 += array[j];
						if (num2 >= num3)
						{
							oldIndex = nowIndex;
							nowIndex = j;
							break;
						}
					}
				}
				if (grabController.IsReadyForRelease())
				{
					nowIndex = actions.FindIndex((ActionInfo action) => action.data.actionID == grabController.releaseActionId);
				}
				else if (enemy.counterFlag)
				{
					enemy.counterFlag = false;
					int count3 = actions.Count;
					int num4 = 0;
					while (true)
					{
						if (num4 >= count3)
						{
							return;
						}
						if (actions[num4].data.isCounterAttack && (actions[num4].data.modeId <= 0 || actions[num4].data.modeId == modeId))
						{
							break;
						}
						num4++;
					}
					nowIndex = num4;
				}
			}
		}
	}

	private bool SetupAngryStartAction()
	{
		if (m_angryDataList == null || m_angryDataList.Count <= 0)
		{
			return false;
		}
		int count = m_angryDataList.Count;
		if (count <= 0)
		{
			return false;
		}
		m_execAngryIdList.Clear();
		int num = -1;
		for (int i = 0; i < count; i++)
		{
			EnemyAngryTable.Data angryData = m_angryDataList[i];
			if (angryData == null)
			{
				Log.Error("angryData is null!! idx:{0}", i);
			}
			else if (!enemy.CheckAngryID(angryData.id))
			{
				bool flag = false;
				switch (angryData.condition)
				{
				case ANGRY_CONDITION.LESS_HP:
				{
					float num2 = (float)enemy.hp / (float)enemy.hpMax;
					float num3 = (float)angryData.value1 / 100f;
					if (num2 <= num3)
					{
						flag = true;
					}
					break;
				}
				case ANGRY_CONDITION.NUM_DOWN:
					if (enemy.downCount >= angryData.value1)
					{
						flag = true;
					}
					break;
				case ANGRY_CONDITION.BREAK_PARTS:
				{
					EnemyRegionWork enemyRegionWork = enemy.SearchRegionWork(angryData.value1);
					if (enemyRegionWork != null && (int)enemyRegionWork.hp <= 0)
					{
						flag = true;
					}
					break;
				}
				}
				if (flag)
				{
					num = actions.FindIndex((ActionInfo action) => action.data.actionID == angryData.actionID);
					m_execAngryIdList.Add(angryData.id);
				}
			}
		}
		if (num < 0)
		{
			return false;
		}
		foreach (uint execAngryId in m_execAngryIdList)
		{
			enemy.RegisterAngryID(execAngryId);
		}
		oldIndex = nowIndex;
		nowIndex = num;
		totalWeight = 1;
		return true;
	}

	public void OnReviveRegion(int regionId)
	{
		if (!(enemy == null))
		{
			EnemyRegionWork enemyRegionWork = enemy.SearchRegionWork(regionId);
			if (enemyRegionWork != null && m_angryDataList != null)
			{
				int count = m_angryDataList.Count;
				int num = 0;
				EnemyAngryTable.Data data;
				while (true)
				{
					if (num >= count)
					{
						return;
					}
					data = m_angryDataList[num];
					if (data.condition == ANGRY_CONDITION.BREAK_PARTS && regionId == data.value1)
					{
						break;
					}
					num++;
				}
				enemy.UnRegisterAngryID(data.id);
			}
		}
	}

	private void SetupActionIndexWhenNoWeight(int continuout_index)
	{
		if (continuout_index >= 0)
		{
			oldIndex = nowIndex;
			nowIndex = continuout_index;
			totalWeight = 1;
		}
	}
}

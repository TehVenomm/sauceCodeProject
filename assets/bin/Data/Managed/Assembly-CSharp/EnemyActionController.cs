// Decompiled with JetBrains decompiler
// Type: EnemyActionController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyActionController
{
  public GrabController grabController;
  private EnemyWaveStrategyController waveStrategyCtrl;
  private Brain brain;
  private Enemy enemy;
  public List<EnemyActionController.ActionInfo> actions = new List<EnemyActionController.ActionInfo>();
  public EnemyActionController.ActionInfo EMPTY_ACTION = new EnemyActionController.ActionInfo()
  {
    data = new EnemyActionTable.EnemyActionData()
  };
  public EnemyActionController.ActionInfo alteredAction;
  private List<EnemyAngryTable.Data> m_angryDataList = new List<EnemyAngryTable.Data>();
  private List<uint> m_execAngryIdList = new List<uint>();
  public int m_angryStartIndex = -1;
  private List<EnemyActionTable.EnemyActionData> m_basisActionDataList = new List<EnemyActionTable.EnemyActionData>();
  private List<EnemyActionTable.EnemyActionData> m_exActionDataList = new List<EnemyActionTable.EnemyActionData>();
  private EnemyTable.EnemyData m_enemyData;
  private int m_nowActionID;
  public int modeId = 1;

  public EnemyActionController(Brain brain)
  {
    this.brain = brain;
    this.enemy = brain.owner as Enemy;
    this.oldIndex = -1;
    this.grabController = new GrabController();
    this.waveStrategyCtrl = new EnemyWaveStrategyController(this.enemy);
  }

  public int nowIndex { get; protected set; }

  public int oldIndex { get; protected set; }

  public EnemyActionController.ActionInfo nowAction
  {
    get
    {
      if (this.nowIndex >= 0 && this.nowIndex < this.actions.Count)
        return this.actions[this.nowIndex];
      return this.alteredAction != null ? this.alteredAction : this.EMPTY_ACTION;
    }
  }

  public int canUseCount { get; private set; }

  public int totalWeight { get; private set; }

  public uint actionID { get; private set; }

  public int GetCounterAttackId()
  {
    for (int index1 = 0; index1 < this.actions.Count; ++index1)
    {
      if (this.actions[index1].data.isCounterAttack && (this.actions[index1].data.modeId <= 0 || this.actions[index1].data.modeId == this.modeId))
      {
        for (int index2 = 0; index2 < this.actions[index1].data.combiActionTypeInfos.Length; ++index2)
        {
          if (this.actions[index1].data.combiActionTypeInfos[index2].type == EnemyActionTable.ACTION_TYPE.ATTACK)
            return this.actions[index1].data.combiActionTypeInfos[index2].id;
        }
      }
    }
    return int.MaxValue;
  }

  public int GetNowModeCounterModeId()
  {
    for (int index = 0; index < this.actions.Count; ++index)
    {
      if (this.actions[index].data.isCounterAttack && (this.actions[index].data.modeId <= 0 || this.actions[index].data.modeId == this.modeId))
        return this.actions[index].data.modeId;
    }
    return -1;
  }

  public void LoadTable()
  {
    this.actionID = 0U;
    this.modeId = 1;
    if (!Singleton<EnemyActionTable>.IsValid())
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.enemy.enemyID);
    if (enemyData == null)
      return;
    this.actionID = (uint) enemyData.actionId;
    List<EnemyActionTable.EnemyActionData> enemyActionList = Singleton<EnemyActionTable>.I.GetEnemyActionList(this.actionID);
    if (enemyActionList == null)
      return;
    this.m_basisActionDataList = enemyActionList;
    if (this.enemy.ExActionID > 0)
      this.m_exActionDataList = Singleton<EnemyActionTable>.I.GetEnemyActionList((uint) this.enemy.ExActionID);
    this.m_enemyData = enemyData;
    this.PrepareActionInfoList(enemyActionList, enemyData.actionId);
  }

  private void PrepareActionInfoList(
    List<EnemyActionTable.EnemyActionData> actionDataList,
    int nextActionID)
  {
    if (this.m_nowActionID == nextActionID)
      return;
    this.actions = new List<EnemyActionController.ActionInfo>();
    this.m_angryDataList = new List<EnemyAngryTable.Data>();
    this.m_nowActionID = nextActionID;
    actionDataList.ForEach((Action<EnemyActionTable.EnemyActionData>) (data =>
    {
      EnemyActionController.ActionInfo actionInfo = new EnemyActionController.ActionInfo()
      {
        data = data
      };
      int length1 = data.regionFlags.Length;
      for (int index = 0; index < length1; ++index)
      {
        EnemyActionTable.EnemyActionData.RegionFlag regionFlag = data.regionFlags[index];
        if (regionFlag.name.Length > 0)
        {
          int regionId = this.enemy.GetRegionID(regionFlag.name);
          if (regionId > 0)
          {
            switch (regionFlag.flag)
            {
              case 0:
                actionInfo.useDeadRegionIDs.Add(regionId);
                continue;
              case 1:
                actionInfo.useAliveRegionIDs.Add(regionId);
                continue;
              case 2:
                actionInfo.useReviveRegionIDs.Add(regionId);
                continue;
              default:
                continue;
            }
          }
        }
      }
      EnemyActionTable.ActionTypeInfo[] combiActionTypeInfos = actionInfo.data.combiActionTypeInfos;
      if (combiActionTypeInfos != null && combiActionTypeInfos.Length != 0)
      {
        int length2 = combiActionTypeInfos.Length;
        for (int index = 0; index < length2; ++index)
        {
          if (combiActionTypeInfos[index].type == EnemyActionTable.ACTION_TYPE.ANGRY)
          {
            uint angryId = actionInfo.data.angryId;
            if (Singleton<EnemyAngryTable>.IsValid() && angryId > 0U)
            {
              EnemyAngryTable.Data data1 = Singleton<EnemyAngryTable>.I.GetData(angryId);
              if (data1 != null)
              {
                data1.actionID = actionInfo.data.actionID;
                this.m_angryDataList.Add(data1);
              }
            }
            actionInfo.data.isUse = false;
          }
        }
      }
      if ((double) data.startWaitInterval > 0.0)
        data.startWaitTime = Time.time;
      if ((int) this.m_enemyData.level < data.useLvLimit)
        return;
      this.actions.Add(actionInfo);
    }));
  }

  private bool canActionWithAliveRegion(EnemyActionController.ActionInfo action)
  {
    EnemyRegionWork[] works = this.enemy.regionWorks;
    return action.useAliveRegionIDs.Find((Predicate<int>) (id => id < works.Length && (int) works[id].hp <= 0)) <= 0 && action.useDeadRegionIDs.Find((Predicate<int>) (id => id < works.Length && (int) works[id].hp > 0 || this.enemy.IsEnableReviveRegion(id))) <= 0 && action.useReviveRegionIDs.Find((Predicate<int>) (id => !this.enemy.IsEnableReviveRegion(id))) <= 0;
  }

  private bool CheckActionByAngryCondition(EnemyActionController.ActionInfo action)
  {
    uint validAngryId = action.data.validAngryId;
    return validAngryId <= 0U || (int) this.enemy.NowAngryID == (int) validAngryId;
  }

  protected virtual int GetWeight(EnemyActionController.ActionInfo action, DISTANCE d, PLACE p)
  {
    int weight = action.data.distanceWeights[(int) d] * action.data.placeWeights[(int) p];
    if (this.brain.opponentMem.counter.nearNum >= 2)
      weight += action.data.nearMultiPlayerWeight;
    return weight;
  }

  private void UpdateActionInfoList()
  {
    EnemyTable.EnemyData enemyData = this.m_enemyData;
    EnemyActionController.EXACTION_CONDITION exActionCondition = (EnemyActionController.EXACTION_CONDITION) this.enemy.ExActionCondition;
    if (exActionCondition <= EnemyActionController.EXACTION_CONDITION.NONE || exActionCondition >= EnemyActionController.EXACTION_CONDITION.MAX || this.enemy.ExActionID <= 0 || this.m_exActionDataList == null || this.m_exActionDataList.Count <= 0)
      return;
    bool flag = false;
    if (exActionCondition == EnemyActionController.EXACTION_CONDITION.SHIELD_ON)
      flag = this.enemy.IsValidShield();
    if (flag)
      this.PrepareActionInfoList(this.m_exActionDataList, this.enemy.ExActionID);
    else
      this.PrepareActionInfoList(this.m_basisActionDataList, enemyData.actionId);
  }

  public void SelectAction()
  {
    this.UpdateActionInfoList();
    this.canUseCount = 0;
    this.totalWeight = 0;
    this.alteredAction = (EnemyActionController.ActionInfo) null;
    int[] numArray = new int[this.actions.Count];
    int continuout_index = -1;
    int index1 = 0;
    for (int count = this.actions.Count; index1 < count; ++index1)
    {
      EnemyActionController.ActionInfo action = this.actions[index1];
      bool flag = action.data.isUse;
      if (action.isForceDisable)
        flag = false;
      if (action.data.modeId > 0 && action.data.modeId != this.modeId)
        flag = false;
      if (flag)
      {
        ++this.canUseCount;
        if (this.canActionWithAliveRegion(action) && this.CheckActionByAngryCondition(action) && ((double) action.data.startWaitInterval <= 0.0 || (double) Time.time - (double) action.data.startWaitTime >= (double) action.data.startWaitInterval) && ((double) action.data.lotteryWaitInterval <= 0.0 || (double) Time.time - (double) action.data.lotteryWaitTime >= (double) action.data.lotteryWaitInterval))
        {
          int num = 0;
          OpponentMemory.OpponentRecord opponent = this.brain.targetCtrl.GetOpponent();
          if (opponent != null)
            num = this.GetWeight(action, opponent.record.distanceType, opponent.record.place);
          numArray[index1] = num;
          if (this.oldIndex == index1 && this.enemy.isBoss)
          {
            numArray[index1] /= 2;
            if (num > 0)
              continuout_index = index1;
          }
          this.totalWeight += numArray[index1];
        }
      }
    }
    if (this.canUseCount <= 0 || this.SetupAngryStartAction())
      return;
    if (this.totalWeight <= 0)
    {
      this.SetupActionIndexWhenNoWeight(continuout_index);
    }
    else
    {
      int num1 = 0;
      int num2 = Random.Range(0, this.totalWeight) + 1;
      int index2 = 0;
      for (int count = this.actions.Count; index2 < count; ++index2)
      {
        if (numArray[index2] > 0)
        {
          num1 += numArray[index2];
          if (num1 >= num2)
          {
            this.oldIndex = this.nowIndex;
            this.nowIndex = index2;
            break;
          }
        }
      }
      if (this.grabController.IsReadyForRelease())
        this.nowIndex = this.actions.FindIndex((Predicate<EnemyActionController.ActionInfo>) (action => (long) action.data.actionID == (long) this.grabController.releaseActionId));
      else if (this.waveStrategyCtrl.IsActive() && !this.waveStrategyCtrl.IsArrivedTarget())
      {
        this.nowIndex = -1;
        this.alteredAction = this.waveStrategyCtrl.GetAlteredAction();
      }
      else
      {
        if (!this.enemy.counterFlag)
          return;
        this.enemy.counterFlag = false;
        int count = this.actions.Count;
        for (int index3 = 0; index3 < count; ++index3)
        {
          if (this.actions[index3].data.isCounterAttack && (this.actions[index3].data.modeId <= 0 || this.actions[index3].data.modeId == this.modeId))
          {
            this.nowIndex = index3;
            break;
          }
        }
      }
    }
  }

  private bool SetupAngryStartAction()
  {
    if (this.m_angryDataList == null || this.m_angryDataList.Count <= 0)
      return false;
    int count = this.m_angryDataList.Count;
    if (count <= 0)
      return false;
    this.m_execAngryIdList.Clear();
    int num = -1;
    for (int index = 0; index < count; ++index)
    {
      EnemyAngryTable.Data angryData = this.m_angryDataList[index];
      if (angryData == null)
        Log.Error("angryData is null!! idx:{0}", (object) index);
      else if (!this.enemy.CheckAngryID(angryData.id))
      {
        bool flag = false;
        switch (angryData.condition)
        {
          case ANGRY_CONDITION.LESS_HP:
            if ((double) this.enemy.hp / (double) this.enemy.hpMax <= (double) ((float) angryData.value1 / 100f))
            {
              flag = true;
              break;
            }
            break;
          case ANGRY_CONDITION.NUM_DOWN:
            if (this.enemy.downCount >= angryData.value1)
            {
              flag = true;
              break;
            }
            break;
          case ANGRY_CONDITION.BREAK_PARTS:
            EnemyRegionWork enemyRegionWork = this.enemy.SearchRegionWork(angryData.value1);
            if (enemyRegionWork != null && (int) enemyRegionWork.hp <= 0)
            {
              flag = true;
              break;
            }
            break;
        }
        if (flag)
        {
          num = this.actions.FindIndex((Predicate<EnemyActionController.ActionInfo>) (action => (int) action.data.actionID == (int) angryData.actionID));
          this.m_execAngryIdList.Add(angryData.id);
        }
      }
    }
    if (num < 0)
      return false;
    foreach (uint execAngryId in this.m_execAngryIdList)
      this.enemy.RegisterAngryID(execAngryId);
    this.oldIndex = this.nowIndex;
    this.nowIndex = num;
    this.totalWeight = 1;
    return true;
  }

  public void OnReviveRegion(int regionId)
  {
    if (Object.op_Equality((Object) this.enemy, (Object) null) || this.enemy.SearchRegionWork(regionId) == null || this.m_angryDataList == null)
      return;
    int count = this.m_angryDataList.Count;
    for (int index = 0; index < count; ++index)
    {
      EnemyAngryTable.Data angryData = this.m_angryDataList[index];
      if (angryData.condition == ANGRY_CONDITION.BREAK_PARTS && regionId == angryData.value1)
      {
        this.enemy.UnRegisterAngryID(angryData.id);
        break;
      }
    }
  }

  private void SetupActionIndexWhenNoWeight(int continuout_index)
  {
    if (continuout_index < 0)
      return;
    this.oldIndex = this.nowIndex;
    this.nowIndex = continuout_index;
    this.totalWeight = 1;
  }

  public bool GetMoveMaxLength(ref float length)
  {
    if (!this.waveStrategyCtrl.IsActive())
      return false;
    length = 0.0f;
    return true;
  }

  public enum REGION_FLAG
  {
    DEAD,
    ALIVE,
    REVIVABLE,
  }

  public enum EXACTION_CONDITION
  {
    NONE,
    SHIELD_ON,
    MAX,
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
}

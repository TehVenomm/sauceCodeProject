// Decompiled with JetBrains decompiler
// Type: EnemyActionTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class EnemyActionTable : Singleton<EnemyActionTable>, IDataTable
{
  private const int COMBINATION_ACTION_MAX = 5;
  private const int BREAK_REGION_MAX = 3;
  private static string[] actionNames = new string[12]
  {
    "none",
    "step",
    "step_back",
    "rotate",
    "move",
    "move_homing",
    "angry_",
    "attack_",
    "escape",
    "move_side",
    "move_point",
    "move_lookat"
  };
  private UIntKeyTable<List<EnemyActionTable.EnemyActionData>> dataTable;

  public static EnemyActionTable.ActionTypeInfo GetActionTypeInfo(string name)
  {
    EnemyActionTable.ActionTypeInfo actionTypeInfo = new EnemyActionTable.ActionTypeInfo();
    if (name == null || name.Length <= 0)
      return actionTypeInfo;
    int index = 0;
    for (int length = EnemyActionTable.actionNames.Length; index < length; ++index)
    {
      if (EnemyActionTable.actionNames[index] == name)
      {
        actionTypeInfo.type = (EnemyActionTable.ACTION_TYPE) index;
        break;
      }
      if (EnemyActionTable.actionNames[index].EndsWith("_") && name.StartsWith(EnemyActionTable.actionNames[index]))
      {
        actionTypeInfo.type = (EnemyActionTable.ACTION_TYPE) index;
        actionTypeInfo.id = int.Parse(name.Substring(EnemyActionTable.actionNames[index].Length));
        break;
      }
    }
    if (actionTypeInfo.type == EnemyActionTable.ACTION_TYPE.NONE)
      Log.Error(LOG.ERROR, "Anim名（{0}）は動作しません。Tableで設定しているAnim名を確認・変更してください。", (object) name);
    return actionTypeInfo;
  }

  public static EnemyActionTable.ActionTypeInfo[] GetActionTypeInfos(params string[] names)
  {
    EnemyActionTable.ActionTypeInfo[] actionTypeInfos = new EnemyActionTable.ActionTypeInfo[names.Length];
    int index = 0;
    for (int length = actionTypeInfos.Length; index < length; ++index)
      actionTypeInfos[index] = EnemyActionTable.GetActionTypeInfo(names[index]);
    return actionTypeInfos;
  }

  public void CreateTable(string csv)
  {
    this.dataTable = TableUtility.CreateUIntKeyListTable<EnemyActionTable.EnemyActionData>(csv, new TableUtility.CallBackUIntKeyReadCSV<EnemyActionTable.EnemyActionData>(EnemyActionTable.EnemyActionData.CB), "enemyID,actionID,actionName,anim0,anim1,anim2,anim3,anim4,weight0,weight1,weight2,weight3,weight4,weight5,weight6,weight7,weight8,act0,act1,act2,act3,act4,act5,act6,act7,act8,act9,angryId,validAngryId,lotteryWaitInterval,counter,useLvLimit,modeId,startWaitInterval");
    this.dataTable.TrimExcess();
  }

  public void AddTable(string csv)
  {
    TableUtility.AddUIntKeyListTable<EnemyActionTable.EnemyActionData>(this.dataTable, csv, new TableUtility.CallBackUIntKeyReadCSV<EnemyActionTable.EnemyActionData>(EnemyActionTable.EnemyActionData.CB), "enemyID,actionID,actionName,anim0,anim1,anim2,anim3,anim4,weight0,weight1,weight2,weight3,weight4,weight5,weight6,weight7,weight8,act0,act1,act2,act3,act4,act5,act6,act7,act8,act9,angryId,validAngryId,lotteryWaitInterval,counter,useLvLimit,modeId,startWaitInterval");
  }

  public List<EnemyActionTable.EnemyActionData> GetEnemyActionList(uint id)
  {
    return this.dataTable.Get(id);
  }

  [Serializable]
  public class EnemyActionData
  {
    public uint actionID;
    public string name;
    public string[] combiActionNames = new string[5];
    public EnemyActionTable.ActionTypeInfo[] combiActionTypeInfos = new EnemyActionTable.ActionTypeInfo[5];
    public int[] distanceWeights = new int[4];
    public int[] placeWeights = new int[4];
    public int nearMultiPlayerWeight;
    public uint angryId;
    public uint validAngryId;
    public int modeId;
    public bool isUse;
    public bool isRotate;
    public int atkRange;
    public float afterWaitTime;
    public float lotteryWaitInterval;
    public float lotteryWaitTime = float.MinValue;
    public float startWaitInterval;
    public float startWaitTime = float.MinValue;
    public bool isCounterAttack;
    public int useLvLimit;
    public EnemyActionTable.EnemyActionData.RegionFlag[] regionFlags = new EnemyActionTable.EnemyActionData.RegionFlag[3];
    public const string NT = "enemyID,actionID,actionName,anim0,anim1,anim2,anim3,anim4,weight0,weight1,weight2,weight3,weight4,weight5,weight6,weight7,weight8,act0,act1,act2,act3,act4,act5,act6,act7,act8,act9,angryId,validAngryId,lotteryWaitInterval,counter,useLvLimit,modeId,startWaitInterval";

    public bool isMove => (double) this.atkRange > 0.0;

    public override string ToString()
    {
      string str1 = $"{string.Empty + (object) this.actionID},{this.name}";
      for (int index = 0; index < 5; ++index)
        str1 = $"{str1},{this.combiActionNames[index]}";
      for (int index = 0; index < 4; ++index)
        str1 = $"{str1},{(object) this.distanceWeights[index]}";
      for (int index = 0; index < 4; ++index)
        str1 = $"{str1},{(object) this.placeWeights[index]}";
      string str2 = $"{$"{$"{$"{$"{str1},{(object) this.nearMultiPlayerWeight}"},{this.isUse.ToString()}"},{this.isRotate.ToString()}"},{(object) this.atkRange}"},{(object) this.afterWaitTime}";
      for (int index = 0; index < 3; ++index)
        str2 = $"{$"{str2},{this.regionFlags[index].name}"},{(object) this.regionFlags[index].flag}";
      return str2;
    }

    public static bool CB(CSVReader csv, EnemyActionTable.EnemyActionData data, ref uint key1)
    {
      csv.Pop(ref data.actionID);
      csv.Pop(ref data.name);
      for (int index = 0; index < 5; ++index)
      {
        csv.Pop(ref data.combiActionNames[index]);
        data.combiActionTypeInfos[index] = EnemyActionTable.GetActionTypeInfo(data.combiActionNames[index]);
      }
      for (int index = 0; index < 4; ++index)
        csv.Pop(ref data.distanceWeights[index]);
      for (int index = 0; index < 4; ++index)
        csv.Pop(ref data.placeWeights[index]);
      csv.Pop(ref data.nearMultiPlayerWeight);
      csv.Pop(ref data.isUse);
      csv.Pop(ref data.isRotate);
      csv.Pop(ref data.atkRange);
      csv.Pop(ref data.afterWaitTime);
      for (int index = 0; index < 3; ++index)
      {
        data.regionFlags[index] = new EnemyActionTable.EnemyActionData.RegionFlag();
        csv.Pop(ref data.regionFlags[index].name);
        csv.Pop(ref data.regionFlags[index].flag);
      }
      csv.Pop(ref data.angryId);
      csv.Pop(ref data.validAngryId);
      csv.Pop(ref data.lotteryWaitInterval);
      csv.Pop(ref data.isCounterAttack);
      csv.Pop(ref data.useLvLimit);
      csv.Pop(ref data.modeId);
      csv.Pop(ref data.startWaitInterval);
      return true;
    }

    public class RegionFlag
    {
      public string name = "";
      public int flag;
    }
  }

  public enum ACTION_TYPE
  {
    NONE,
    STEP,
    STEP_BACK,
    ROTATE,
    MOVE,
    MOVE_HOMING,
    ANGRY,
    ATTACK,
    ESCAPE,
    MOVE_SIDE,
    MOVE_POINT,
    MOVE_LOOKAT,
  }

  [Serializable]
  public class ActionTypeInfo
  {
    public EnemyActionTable.ACTION_TYPE type;
    public int id;
  }
}

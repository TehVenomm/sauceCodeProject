using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class EnemyActionTable : Singleton<EnemyActionTable>, IDataTable
{
	[Serializable]
	public class EnemyActionData
	{
		public class RegionFlag
		{
			public string name = string.Empty;

			public int flag;
		}

		public uint actionID;

		public string name;

		public string[] combiActionNames = new string[5];

		public ActionTypeInfo[] combiActionTypeInfos = new ActionTypeInfo[5];

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

		public RegionFlag[] regionFlags = new RegionFlag[3];

		public const string NT = "enemyID,actionID,actionName,anim0,anim1,anim2,anim3,anim4,weight0,weight1,weight2,weight3,weight4,weight5,weight6,weight7,weight8,act0,act1,act2,act3,act4,act5,act6,act7,act8,act9,angryId,validAngryId,lotteryWaitInterval,counter,useLvLimit,modeId,startWaitInterval";

		public bool isMove => (float)atkRange > 0f;

		public override string ToString()
		{
			string empty = string.Empty;
			empty += actionID;
			empty = empty + "," + name;
			for (int i = 0; i < 5; i++)
			{
				empty = empty + "," + combiActionNames[i];
			}
			for (int j = 0; j < 4; j++)
			{
				empty = empty + "," + distanceWeights[j];
			}
			for (int k = 0; k < 4; k++)
			{
				empty = empty + "," + placeWeights[k];
			}
			empty = empty + "," + nearMultiPlayerWeight;
			empty = empty + "," + isUse;
			empty = empty + "," + isRotate;
			empty = empty + "," + atkRange;
			empty = empty + "," + afterWaitTime;
			for (int l = 0; l < 3; l++)
			{
				empty = empty + "," + regionFlags[l].name;
				empty = empty + "," + regionFlags[l].flag;
			}
			return empty;
		}

		public static bool CB(CSVReader csv, EnemyActionData data, ref uint key1)
		{
			csv.Pop(ref data.actionID);
			csv.Pop(ref data.name);
			for (int i = 0; i < 5; i++)
			{
				csv.Pop(ref data.combiActionNames[i]);
				data.combiActionTypeInfos[i] = GetActionTypeInfo(data.combiActionNames[i]);
			}
			for (int j = 0; j < 4; j++)
			{
				csv.Pop(ref data.distanceWeights[j]);
			}
			for (int k = 0; k < 4; k++)
			{
				csv.Pop(ref data.placeWeights[k]);
			}
			csv.Pop(ref data.nearMultiPlayerWeight);
			csv.Pop(ref data.isUse);
			csv.Pop(ref data.isRotate);
			csv.Pop(ref data.atkRange);
			csv.Pop(ref data.afterWaitTime);
			for (int l = 0; l < 3; l++)
			{
				data.regionFlags[l] = new RegionFlag();
				csv.Pop(ref data.regionFlags[l].name);
				csv.Pop(ref data.regionFlags[l].flag);
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
		MOVE_LOOKAT
	}

	[Serializable]
	public class ActionTypeInfo
	{
		public ACTION_TYPE type;

		public int id;
	}

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

	private UIntKeyTable<List<EnemyActionData>> dataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<EnemyActionData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<EnemyActionData> _003C_003Ef__mg_0024cache1;

	public static ActionTypeInfo GetActionTypeInfo(string name)
	{
		ActionTypeInfo actionTypeInfo = new ActionTypeInfo();
		if (name == null || name.Length <= 0)
		{
			return actionTypeInfo;
		}
		int i = 0;
		for (int num = actionNames.Length; i < num; i++)
		{
			if (actionNames[i] == name)
			{
				actionTypeInfo.type = (ACTION_TYPE)i;
				break;
			}
			if (actionNames[i].EndsWith("_") && name.StartsWith(actionNames[i]))
			{
				actionTypeInfo.type = (ACTION_TYPE)i;
				actionTypeInfo.id = int.Parse(name.Substring(actionNames[i].Length));
				break;
			}
		}
		if (actionTypeInfo.type == ACTION_TYPE.NONE)
		{
			Log.Error(LOG.ERROR, "Anim名（{0}）は動作しません。Tableで設定しているAnim名を確認・変更してください。", name);
		}
		return actionTypeInfo;
	}

	public static ActionTypeInfo[] GetActionTypeInfos(params string[] names)
	{
		ActionTypeInfo[] array = new ActionTypeInfo[names.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i] = GetActionTypeInfo(names[i]);
		}
		return array;
	}

	public void CreateTable(string csv)
	{
		dataTable = TableUtility.CreateUIntKeyListTable<EnemyActionData>(csv, EnemyActionData.CB, "enemyID,actionID,actionName,anim0,anim1,anim2,anim3,anim4,weight0,weight1,weight2,weight3,weight4,weight5,weight6,weight7,weight8,act0,act1,act2,act3,act4,act5,act6,act7,act8,act9,angryId,validAngryId,lotteryWaitInterval,counter,useLvLimit,modeId,startWaitInterval");
		dataTable.TrimExcess();
	}

	public void AddTable(string csv)
	{
		TableUtility.AddUIntKeyListTable(dataTable, csv, EnemyActionData.CB, "enemyID,actionID,actionName,anim0,anim1,anim2,anim3,anim4,weight0,weight1,weight2,weight3,weight4,weight5,weight6,weight7,weight8,act0,act1,act2,act3,act4,act5,act6,act7,act8,act9,angryId,validAngryId,lotteryWaitInterval,counter,useLvLimit,modeId,startWaitInterval");
	}

	public List<EnemyActionData> GetEnemyActionList(uint id)
	{
		return dataTable.Get(id);
	}
}

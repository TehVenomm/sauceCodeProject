using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FieldMapTable : Singleton<FieldMapTable>
{
	public class FieldMapTableData
	{
		public const uint NOT_CONECTING_REGION_ID = uint.MaxValue;

		public const uint DEFAULT_ICON_ID = 0u;

		public uint mapID;

		public uint regionId;

		public string mapName;

		public string stageName;

		public string happenStageName;

		public int grade;

		public DIFFICULTY_MODE fieldMode = DIFFICULTY_MODE.NORMAL;

		public int eventId;

		public uint jumpPortalID;

		public int bgmID;

		public int happenBgmID;

		public uint linkQuestID;

		public uint childRegionId = uint.MaxValue;

		public uint iconId;

		public uint questIconId;

		public uint fieldBuffId;

		public Vector3 camOffsetPortraitPos = Vector3.get_zero();

		public Vector3 camOffsetPortraitRot = Vector3.get_zero();

		public Vector3 camOffsetLandscapePos = Vector3.get_zero();

		public Vector3 camOffsetLandscapeRot = Vector3.get_zero();

		public const string NT = "mapId,regionId,mapName,stageName,happenStageName,fieldGrade,fieldMode,eventId,jumpPortalId,bgmId,happenBgmId,linkQuestID,childRegionId,iconId,questIconId,fieldBuffId,camOffsetPortraitPos,camOffsetPortraitRot,camOffsetLandscapePos,camOffsetLandscapeRot";

		public bool IsEventData => eventId != 0;

		public bool hasChildRegion => childRegionId != uint.MaxValue;

		public static bool cb(CSVReader csv_reader, FieldMapTableData data, ref uint key)
		{
			data.mapID = key;
			csv_reader.Pop(ref data.regionId);
			csv_reader.Pop(ref data.mapName);
			csv_reader.Pop(ref data.stageName);
			csv_reader.Pop(ref data.happenStageName);
			csv_reader.Pop(ref data.grade);
			csv_reader.Pop(ref data.fieldMode);
			csv_reader.Pop(ref data.eventId);
			csv_reader.Pop(ref data.jumpPortalID);
			csv_reader.Pop(ref data.bgmID);
			csv_reader.Pop(ref data.happenBgmID);
			csv_reader.Pop(ref data.linkQuestID);
			csv_reader.Pop(ref data.childRegionId);
			csv_reader.Pop(ref data.iconId);
			csv_reader.Pop(ref data.questIconId);
			csv_reader.Pop(ref data.fieldBuffId);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			TryParseNonSplitStrToVector3(value, out data.camOffsetPortraitPos);
			csv_reader.Pop(ref value);
			TryParseNonSplitStrToVector3(value, out data.camOffsetPortraitRot);
			csv_reader.Pop(ref value);
			TryParseNonSplitStrToVector3(value, out data.camOffsetLandscapePos);
			csv_reader.Pop(ref value);
			TryParseNonSplitStrToVector3(value, out data.camOffsetLandscapeRot);
			return true;
		}

		private static bool TryParseNonSplitStrToVector3(string str, out Vector3 vec)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			vec = Vector3.get_zero();
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			string[] array = str.Split(':');
			if (array.Length < 3)
			{
				return false;
			}
			vec._002Ector(array[0].ToFloatOrDefault(), array[1].ToFloatOrDefault(), array[2].ToFloatOrDefault());
			return true;
		}

		public bool IsExistQuestIconId()
		{
			return questIconId >= 1;
		}
	}

	public class PortalTableData
	{
		public uint portalID;

		public uint srcMapID;

		public uint linkPortalId;

		public float srcX;

		public float srcZ;

		public uint dstMapID;

		public float dstX;

		public float dstZ;

		public float dstDir;

		public float mapX = float.MaxValue;

		public float mapY = float.MaxValue;

		public uint dstQuestID;

		public uint showDeliveryId;

		public uint hideQuestId;

		public uint appearQuestId;

		public uint appearDeliveryId;

		public uint travelMapId;

		public uint openPriority;

		public uint portalPoint;

		public string notAppearText;

		public string placeText;

		public DateTime startAt;

		public uint banEnemy;

		public uint appearRegionId;

		public const string NT = "portalId,linkPortalId,srcMapId,srcX,srcZ,dstMapId,dstX,dstZ,dstDir,mapX,mapY,dstQuestId,showDeliveryId,hideQuestId,appearQuestId,appearDeliveryId,travelMapId,openPriority,portalPoint,notAppearText,placeText,startAt,banEnemy,appearRegionId";

		public static bool cb(CSVReader csv_reader, PortalTableData data, ref uint key)
		{
			data.portalID = key;
			csv_reader.Pop(ref data.linkPortalId);
			csv_reader.Pop(ref data.srcMapID);
			csv_reader.Pop(ref data.srcX);
			csv_reader.Pop(ref data.srcZ);
			csv_reader.Pop(ref data.dstMapID);
			csv_reader.Pop(ref data.dstX);
			csv_reader.Pop(ref data.dstZ);
			csv_reader.Pop(ref data.dstDir);
			csv_reader.Pop(ref data.mapX);
			csv_reader.Pop(ref data.mapY);
			csv_reader.Pop(ref data.dstQuestID);
			csv_reader.Pop(ref data.showDeliveryId);
			csv_reader.Pop(ref data.hideQuestId);
			csv_reader.Pop(ref data.appearQuestId);
			csv_reader.Pop(ref data.appearDeliveryId);
			csv_reader.Pop(ref data.travelMapId);
			csv_reader.Pop(ref data.openPriority);
			csv_reader.Pop(ref data.portalPoint);
			csv_reader.Pop(ref data.notAppearText);
			csv_reader.Pop(ref data.placeText);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				DateTime.TryParse(value, out data.startAt);
			}
			csv_reader.Pop(ref data.banEnemy);
			csv_reader.Pop(ref data.appearRegionId);
			return true;
		}

		public bool isUnlockedTime()
		{
			return TimeManager.GetNow() >= startAt;
		}

		public bool IsWarpPortal()
		{
			return banEnemy == 1;
		}
	}

	public class EnemyPopTableData
	{
		public uint mapID;

		public float popX;

		public bool enableRotY;

		public float rotY;

		public bool enablePopY;

		public float popY;

		public float popZ;

		public float popRadius;

		public uint enemyID;

		public uint enemyLv;

		public int waveNo;

		public int popNumMin;

		public int popNumMax;

		public int popNumInit;

		public int popNumTotal;

		public float popTimeMin;

		public float popTimeMax;

		public bool bigMonsterFlag;

		public bool bossFlag;

		public bool autoActivate;

		public BrainParam.ScountingParam scoutingParam = new BrainParam.ScountingParam();

		public ENEMY_POP_TYPE enemyPopType;

		public int escapeTime;

		public float walkSpeedMin = 1f;

		public float walkSpeedMax = 1f;

		public bool enableWalkSpeed;

		private Vector3 cachedPopBasePosition = Vector3.get_zero();

		private bool isPopBasePositionCached;

		public const string NT = "mapId,popX,rotY,popY,popZ,popRadius,enemyId,enemyLv,waveNo,popNumMin,popNumMax,popNumInit,popNumTotal,popTimeMin,popTimeMax,bigMonsterFlag,bossFlag,autoActivate,scountigRange,scoutingSight,scoutingAudibility,enemyPopType,escapeTime,walkSpeedMin,walkSpeedMax";

		public static bool cb(CSVReader csv_reader, EnemyPopTableData data, ref uint key)
		{
			data.mapID = key;
			csv_reader.Pop(ref data.popX);
			if (csv_reader.Pop(ref data.rotY) == CSVReader.PopResult.SUCCESS)
			{
				data.enableRotY = true;
			}
			if (csv_reader.Pop(ref data.popY) == CSVReader.PopResult.SUCCESS)
			{
				data.enablePopY = true;
			}
			csv_reader.Pop(ref data.popZ);
			csv_reader.Pop(ref data.popRadius);
			csv_reader.Pop(ref data.enemyID);
			csv_reader.Pop(ref data.enemyLv);
			csv_reader.Pop(ref data.waveNo);
			csv_reader.Pop(ref data.popNumMin);
			csv_reader.Pop(ref data.popNumMax);
			csv_reader.Pop(ref data.popNumInit);
			csv_reader.Pop(ref data.popNumTotal);
			csv_reader.Pop(ref data.popTimeMin);
			csv_reader.Pop(ref data.popTimeMax);
			csv_reader.Pop(ref data.bigMonsterFlag);
			csv_reader.Pop(ref data.bossFlag);
			csv_reader.Pop(ref data.autoActivate);
			float value = 0f;
			csv_reader.Pop(ref value);
			data.scoutingParam.scountigRangeSqr = value * value;
			csv_reader.Pop(ref value);
			data.scoutingParam.scoutingSightCos = Mathf.Cos((float)Math.PI / 180f * value);
			csv_reader.Pop(ref value);
			data.scoutingParam.scoutingAudibilitySqr = value * value;
			csv_reader.PopEnum(ref data.enemyPopType, ENEMY_POP_TYPE.NONE);
			csv_reader.Pop(ref data.escapeTime);
			CSVReader.PopResult a = csv_reader.Pop(ref data.walkSpeedMin);
			CSVReader.PopResult a2 = csv_reader.Pop(ref data.walkSpeedMax);
			if (a == CSVReader.PopResult.SUCCESS && a2 == CSVReader.PopResult.SUCCESS)
			{
				data.enableWalkSpeed = true;
			}
			return true;
		}

		public float GeneratePopTime()
		{
			return Random.Range(popTimeMin, popTimeMax);
		}

		public float GenerateWalkSpeed()
		{
			if (!enableWalkSpeed)
			{
				return 1f;
			}
			if (walkSpeedMin < walkSpeedMax)
			{
				return Random.Range(walkSpeedMin, walkSpeedMax);
			}
			if (walkSpeedMin > walkSpeedMax)
			{
				return Random.Range(walkSpeedMax, walkSpeedMin);
			}
			return walkSpeedMin;
		}

		public Vector3 GeneratePopPosVec3()
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (!isPopBasePositionCached)
			{
				cachedPopBasePosition = new Vector3(popX, popY, popZ);
			}
			return cachedPopBasePosition;
		}
	}

	public class GatherPointTableData
	{
		public enum GatherType
		{
			Basic,
			Growth
		}

		public uint pointID;

		public uint pointMapID;

		public float pointX;

		public float pointZ;

		public float pointDir;

		public uint viewID;

		public uint maxNumRate;

		public uint addNumRate;

		public uint growthInterval;

		public FieldGimmickPointTableData.GIMMICK_TYPE gimmickType;

		public float value1;

		public const string NT = "pointId,pointMapId,pointX,pointZ,pointDir,viewId,maxNumRate,addNumRate,growthInterval,gimmickType,value1";

		public static bool cb(CSVReader csv_reader, GatherPointTableData data, ref uint key)
		{
			data.pointID = key;
			csv_reader.Pop(ref data.pointMapID);
			csv_reader.Pop(ref data.pointX);
			csv_reader.Pop(ref data.pointZ);
			csv_reader.Pop(ref data.pointDir);
			csv_reader.Pop(ref data.viewID);
			csv_reader.Pop(ref data.maxNumRate);
			csv_reader.Pop(ref data.addNumRate);
			csv_reader.Pop(ref data.growthInterval);
			csv_reader.Pop(ref data.gimmickType);
			csv_reader.Pop(ref data.value1);
			return true;
		}

		public static GatherType GetGatherType(GatherPointTableData data)
		{
			if (data != null && data.growthInterval != 0)
			{
				return GatherType.Growth;
			}
			return GatherType.Basic;
		}

		public FieldGimmickPointTableData CloneAsGimmickData()
		{
			FieldGimmickPointTableData fieldGimmickPointTableData = new FieldGimmickPointTableData();
			fieldGimmickPointTableData.pointID = pointID;
			fieldGimmickPointTableData.pointMapID = pointMapID;
			fieldGimmickPointTableData.pointX = pointX;
			fieldGimmickPointTableData.pointZ = pointZ;
			fieldGimmickPointTableData.pointDir = pointDir;
			fieldGimmickPointTableData.gimmickType = gimmickType;
			fieldGimmickPointTableData.value1 = value1;
			return fieldGimmickPointTableData;
		}
	}

	public class GatherPointViewTableData
	{
		public uint viewID;

		public uint modelID;

		public string modelHideNodeName;

		public string gatherEffectName;

		public float colRadius;

		public float targetRadius;

		public string targetEffectName;

		public float targetEffectShift;

		public float targetEffectHeight;

		public string actStateName;

		public string toolModelName;

		public string toolNodeName;

		public uint iconID;

		public string itemDetailText;

		public const string NT = "id,modelId,modelHideNodeName,gatherEffectName,colRadius,targetRadius,targetEffectName,targetEffectShift,targetEffectHeight,actStateName,toolModelName,toolNodeName,iconId,itemDetailText";

		public static bool cb(CSVReader csv_reader, GatherPointViewTableData data, ref uint key)
		{
			data.viewID = key;
			csv_reader.Pop(ref data.modelID);
			csv_reader.Pop(ref data.modelHideNodeName);
			csv_reader.Pop(ref data.gatherEffectName);
			csv_reader.Pop(ref data.colRadius);
			csv_reader.Pop(ref data.targetRadius);
			csv_reader.Pop(ref data.targetEffectName);
			csv_reader.Pop(ref data.targetEffectShift);
			csv_reader.Pop(ref data.targetEffectHeight);
			csv_reader.Pop(ref data.actStateName);
			csv_reader.Pop(ref data.toolModelName);
			csv_reader.Pop(ref data.toolNodeName);
			csv_reader.Pop(ref data.iconID);
			csv_reader.Pop(ref data.itemDetailText);
			return true;
		}
	}

	public class FieldGimmickPointTableData
	{
		public enum GIMMICK_TYPE
		{
			NONE,
			HEALING,
			CANNON,
			BOMBROCK,
			GEYSER,
			SONAR,
			CANNON_HEAVY,
			CANNON_RAPID,
			CANNON_SPECIAL,
			WAVE_TARGET,
			WAVE_TARGET2,
			CANNON_FIELD,
			READ_STORY,
			FISHING,
			BINGO,
			CHAT,
			WAVE_TARGET3,
			GENERATOR,
			CANDYWOOD,
			COOP_FISHING,
			SUPPLY,
			CARRIABLE_TURRET,
			CARRIABLE_EVOLVE_ITEM,
			CARRIABLE_DECOY,
			CARRIABLE_BUFF_POINT,
			PORTAL_GIMMICK,
			CARRIABLE_BOMB,
			QUEST
		}

		public uint pointID;

		public GIMMICK_TYPE gimmickType;

		public uint pointMapID;

		public float pointX;

		public float pointZ;

		public float pointDir;

		public float value1;

		public string value2 = string.Empty;

		public const string NT = "pointId,gimmickType,pointMapId,pointX,pointZ,pointDir,value1,value2";

		public static bool cb(CSVReader csv_reader, FieldGimmickPointTableData data, ref uint key)
		{
			data.pointID = key;
			csv_reader.Pop(ref data.gimmickType);
			csv_reader.Pop(ref data.pointMapID);
			csv_reader.Pop(ref data.pointX);
			csv_reader.Pop(ref data.pointZ);
			csv_reader.Pop(ref data.pointDir);
			csv_reader.Pop(ref data.value1);
			csv_reader.Pop(ref data.value2);
			return true;
		}
	}

	public class FieldGimmickActionTableData
	{
		public const float DEFAULT_RADIUS = 1f;

		public const float DEFAULT_START = -1f;

		public const float DEFAULT_DURATION = 5f;

		public const float DEFAULT_INTERVAL = 5f;

		public const float DEFAULT_FORCE = 500f;

		public const float DEFAULT_ANGLE = 20f;

		public const float DEFAULT_LOOP_TIME = 4f;

		public uint actionId;

		public float radius = 1f;

		public float start = -1f;

		public float duration = 5f;

		public float interval = 5f;

		public Character.REACTION_TYPE reactionType;

		public float force = 500f;

		public float angle = 20f;

		public float loopTime = 4f;

		public const string NT = "actionId,radius,start,duration,interval,reactionType,force,angle,loopTime";

		public static bool cb(CSVReader csv_reader, FieldGimmickActionTableData data, ref uint key)
		{
			data.actionId = key;
			csv_reader.Pop(ref data.radius);
			csv_reader.Pop(ref data.start);
			csv_reader.Pop(ref data.duration);
			csv_reader.Pop(ref data.interval);
			csv_reader.Pop(ref data.reactionType);
			csv_reader.Pop(ref data.force);
			csv_reader.Pop(ref data.angle);
			csv_reader.Pop(ref data.loopTime);
			return true;
		}
	}

	private UIntKeyTable<FieldMapTableData> fieldMapTable;

	private UIntKeyTable<PortalTableData> portalTable;

	private UIntKeyTable<List<PortalTableData>> portalSrcMapIDTable;

	private UIntKeyTable<List<EnemyPopTableData>> enemyPopTable;

	private UIntKeyTable<GatherPointTableData> gatherPointTable;

	private UIntKeyTable<List<GatherPointTableData>> gatherPointMapIDTable;

	private UIntKeyTable<GatherPointViewTableData> gatherPointViewTable;

	private UIntKeyTable<FieldGimmickPointTableData> fieldGimmickPointTable;

	private UIntKeyTable<List<FieldGimmickPointTableData>> fieldGimmickPointMapIDTable;

	private UIntKeyTable<FieldGimmickActionTableData> fieldGimmickActionTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<FieldMapTableData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<FieldMapTableData> _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<PortalTableData> _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<PortalTableData> _003C_003Ef__mg_0024cache3;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<EnemyPopTableData> _003C_003Ef__mg_0024cache4;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<EnemyPopTableData> _003C_003Ef__mg_0024cache5;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<GatherPointTableData> _003C_003Ef__mg_0024cache6;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<GatherPointViewTableData> _003C_003Ef__mg_0024cache7;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<FieldGimmickPointTableData> _003C_003Ef__mg_0024cache8;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<FieldGimmickActionTableData> _003C_003Ef__mg_0024cache9;

	public void CreateFieldMapTable(string csv_text)
	{
		fieldMapTable = TableUtility.CreateUIntKeyTable<FieldMapTableData>(csv_text, FieldMapTableData.cb, "mapId,regionId,mapName,stageName,happenStageName,fieldGrade,fieldMode,eventId,jumpPortalId,bgmId,happenBgmId,linkQuestID,childRegionId,iconId,questIconId,fieldBuffId,camOffsetPortraitPos,camOffsetPortraitRot,camOffsetLandscapePos,camOffsetLandscapeRot");
	}

	public void AddFieldMapTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(fieldMapTable, csv_text, FieldMapTableData.cb, "mapId,regionId,mapName,stageName,happenStageName,fieldGrade,fieldMode,eventId,jumpPortalId,bgmId,happenBgmId,linkQuestID,childRegionId,iconId,questIconId,fieldBuffId,camOffsetPortraitPos,camOffsetPortraitRot,camOffsetLandscapePos,camOffsetLandscapeRot");
	}

	public void CreatePortalTable(string csv_text)
	{
		portalTable = TableUtility.CreateUIntKeyTable<PortalTableData>(csv_text, PortalTableData.cb, "portalId,linkPortalId,srcMapId,srcX,srcZ,dstMapId,dstX,dstZ,dstDir,mapX,mapY,dstQuestId,showDeliveryId,hideQuestId,appearQuestId,appearDeliveryId,travelMapId,openPriority,portalPoint,notAppearText,placeText,startAt,banEnemy,appearRegionId");
		portalSrcMapIDTable = new UIntKeyTable<List<PortalTableData>>();
		portalTable.ForEach(delegate(PortalTableData portalData)
		{
			uint srcMapID = portalData.srcMapID;
			List<PortalTableData> list = portalSrcMapIDTable.Get(srcMapID);
			if (list == null)
			{
				list = new List<PortalTableData>();
				portalSrcMapIDTable.Add(srcMapID, list);
			}
			list.Add(portalData);
		});
	}

	public void AddPortalTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(portalTable, csv_text, PortalTableData.cb, "portalId,linkPortalId,srcMapId,srcX,srcZ,dstMapId,dstX,dstZ,dstDir,mapX,mapY,dstQuestId,showDeliveryId,hideQuestId,appearQuestId,appearDeliveryId,travelMapId,openPriority,portalPoint,notAppearText,placeText,startAt,banEnemy,appearRegionId");
		portalSrcMapIDTable = new UIntKeyTable<List<PortalTableData>>();
		portalTable.ForEach(delegate(PortalTableData portalData)
		{
			uint srcMapID = portalData.srcMapID;
			List<PortalTableData> list = portalSrcMapIDTable.Get(srcMapID);
			if (list == null)
			{
				list = new List<PortalTableData>();
				portalSrcMapIDTable.Add(srcMapID, list);
			}
			list.Add(portalData);
		});
	}

	public void CreateEnemyPopTable(string csv_text)
	{
		enemyPopTable = TableUtility.CreateUIntKeyListTable<EnemyPopTableData>(csv_text, EnemyPopTableData.cb, "mapId,popX,rotY,popY,popZ,popRadius,enemyId,enemyLv,waveNo,popNumMin,popNumMax,popNumInit,popNumTotal,popTimeMin,popTimeMax,bigMonsterFlag,bossFlag,autoActivate,scountigRange,scoutingSight,scoutingAudibility,enemyPopType,escapeTime,walkSpeedMin,walkSpeedMax");
	}

	public void AddEnemyPopTable(string csv_text)
	{
		TableUtility.AddUIntKeyListTable(enemyPopTable, csv_text, EnemyPopTableData.cb, "mapId,popX,rotY,popY,popZ,popRadius,enemyId,enemyLv,waveNo,popNumMin,popNumMax,popNumInit,popNumTotal,popTimeMin,popTimeMax,bigMonsterFlag,bossFlag,autoActivate,scountigRange,scoutingSight,scoutingAudibility,enemyPopType,escapeTime,walkSpeedMin,walkSpeedMax");
	}

	public void CreateGatherPointTable(string csv_text)
	{
		gatherPointTable = TableUtility.CreateUIntKeyTable<GatherPointTableData>(csv_text, GatherPointTableData.cb, "pointId,pointMapId,pointX,pointZ,pointDir,viewId,maxNumRate,addNumRate,growthInterval,gimmickType,value1");
		gatherPointMapIDTable = new UIntKeyTable<List<GatherPointTableData>>();
		gatherPointTable.ForEach(delegate(GatherPointTableData pointData)
		{
			uint pointMapID = pointData.pointMapID;
			List<GatherPointTableData> list = gatherPointMapIDTable.Get(pointMapID);
			if (list == null)
			{
				list = new List<GatherPointTableData>();
				gatherPointMapIDTable.Add(pointMapID, list);
			}
			list.Add(pointData);
		});
	}

	public void CreateGatherPointViewTable(string csv_text)
	{
		gatherPointViewTable = TableUtility.CreateUIntKeyTable<GatherPointViewTableData>(csv_text, GatherPointViewTableData.cb, "id,modelId,modelHideNodeName,gatherEffectName,colRadius,targetRadius,targetEffectName,targetEffectShift,targetEffectHeight,actStateName,toolModelName,toolNodeName,iconId,itemDetailText");
	}

	public void CreateGimmickPointTable(string csv_text)
	{
		fieldGimmickPointTable = TableUtility.CreateUIntKeyTable<FieldGimmickPointTableData>(csv_text, FieldGimmickPointTableData.cb, "pointId,gimmickType,pointMapId,pointX,pointZ,pointDir,value1,value2");
		fieldGimmickPointMapIDTable = new UIntKeyTable<List<FieldGimmickPointTableData>>();
		fieldGimmickPointTable.ForEach(delegate(FieldGimmickPointTableData pointData)
		{
			uint pointMapID = pointData.pointMapID;
			List<FieldGimmickPointTableData> list = fieldGimmickPointMapIDTable.Get(pointMapID);
			if (list == null)
			{
				list = new List<FieldGimmickPointTableData>();
				fieldGimmickPointMapIDTable.Add(pointMapID, list);
			}
			list.Add(pointData);
		});
	}

	public void CreateGimmickActionTable(string csv_text)
	{
		fieldGimmickActionTable = TableUtility.CreateUIntKeyTable<FieldGimmickActionTableData>(csv_text, FieldGimmickActionTableData.cb, "actionId,radius,start,duration,interval,reactionType,force,angle,loopTime");
	}

	public FieldMapTableData GetFieldMapData(uint id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return fieldMapTable.Get(id);
	}

	public FieldMapTableData[] GetFieldMapDataInRegion(uint regionId)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		List<FieldMapTableData> data = new List<FieldMapTableData>(20);
		fieldMapTable.ForEach(delegate(FieldMapTableData d)
		{
			if (d.regionId == regionId)
			{
				data.Add(d);
			}
		});
		return data.ToArray();
	}

	public PortalTableData GetPortalData(uint id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return portalTable.Get(id);
	}

	public List<PortalTableData> GetPortalListByMapID(uint map_id, bool do_sort = false)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		List<PortalTableData> list = portalSrcMapIDTable.Get(map_id);
		if (list != null && do_sort)
		{
			list.Sort((PortalTableData l, PortalTableData r) => (int)(l.openPriority - r.openPriority));
		}
		return list;
	}

	public List<PortalTableData> GetDeliveryRelationPortalData(uint delivery_id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		List<PortalTableData> ret = new List<PortalTableData>();
		portalTable.ForEach(delegate(PortalTableData data)
		{
			if (data.appearDeliveryId == delivery_id)
			{
				ret.Add(data);
			}
		});
		List<PortalTableData> remove_list = new List<PortalTableData>();
		ret.ForEach(delegate(PortalTableData data)
		{
			if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)data.srcMapID))
			{
				if (data.linkPortalId != 0)
				{
					PortalTableData portalTableData = portalTable.Get(data.linkPortalId);
					if (portalTableData != null)
					{
						remove_list.Add(portalTableData);
					}
				}
			}
			else
			{
				remove_list.Add(data);
			}
		});
		remove_list.ForEach(delegate(PortalTableData remove_portal)
		{
			ret.RemoveAll((PortalTableData data) => data.portalID == remove_portal.portalID);
		});
		return ret;
	}

	public List<EnemyPopTableData> GetEnemyPopList(uint map_id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return enemyPopTable.Get(map_id);
	}

	public EnemyPopTableData GetEnemyPopData(uint map_id, int index)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		List<EnemyPopTableData> enemyPopList = GetEnemyPopList(map_id);
		if (enemyPopList == null || enemyPopList.Count <= 0)
		{
			return null;
		}
		if (index < 0 || enemyPopList.Count <= index)
		{
			return null;
		}
		return enemyPopList[index];
	}

	public List<EnemyPopTableData> GetRareOrBossEnemyList(int map_id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		List<EnemyPopTableData> list = enemyPopTable.Get((uint)map_id);
		List<EnemyPopTableData> list2 = new List<EnemyPopTableData>();
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			EnemyPopTableData enemyPopTableData = list[i];
			if (enemyPopTableData.enemyPopType != 0)
			{
				list2.Add(list[i]);
			}
		}
		return list2;
	}

	public uint GetTargetEnemyPopMapID(uint enemy_id)
	{
		uint ret = 0u;
		enemyPopTable.ForEach(delegate(List<EnemyPopTableData> data)
		{
			data.ForEach(delegate(EnemyPopTableData pop_data)
			{
				if ((pop_data.mapID <= ret || ret == 0) && pop_data.enemyID == enemy_id && pop_data.enemyID != 0)
				{
					ret = pop_data.mapID;
				}
			});
		});
		return ret;
	}

	public List<uint> GetTargetEnemyPopMapIDs(uint enemy_id)
	{
		List<uint> ret = new List<uint>();
		enemyPopTable.ForEach(delegate(List<EnemyPopTableData> data)
		{
			data.ForEach(delegate(EnemyPopTableData pop_data)
			{
				if (pop_data.enemyID == enemy_id && pop_data.enemyID != 0)
				{
					ret.Add(pop_data.mapID);
				}
			});
		});
		return ret;
	}

	public GatherPointTableData GetGatherPointData(uint id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return gatherPointTable.Get(id);
	}

	public List<GatherPointTableData> GetGatherPointListByMapID(uint map_id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return gatherPointMapIDTable.Get(map_id);
	}

	public GatherPointViewTableData GetGatherPointViewData(uint id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return gatherPointViewTable.Get(id);
	}

	public FieldGimmickPointTableData GetFieldGimmickPointData(uint id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		if (fieldGimmickPointTable == null || fieldGimmickPointTable.GetCount() == 0)
		{
			return null;
		}
		return fieldGimmickPointTable.Get(id);
	}

	public List<FieldGimmickPointTableData> GetFieldGimmickPointListByMapID(uint mapID)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return fieldGimmickPointMapIDTable.Get(mapID);
	}

	public FieldGimmickActionTableData GetFieldGimmickActionData(uint id)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		return fieldGimmickActionTable.Get(id);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_EnemyInitialize : Coop_Model_ObjectSyncPositionBase
{
	public int sid;

	public int hp;

	public int hpMax;

	public float hpDamageRate;

	public float downTotal;

	public int downCount;

	public BadStatus badStatusMax = new BadStatus();

	public List<Enemy.RegionWorkSyncData> regions;

	public int target_id;

	public BuffParam.BuffSyncParam buff_sync_param;

	public uint nowAngryId;

	public List<uint> execAngryIds = new List<uint>();

	public ContinusAttackParam.SyncParam cntAtkSyncParam = new ContinusAttackParam.SyncParam();

	public int barrierHp;

	public bool isHiding;

	public int shieldHp;

	public int grabHp;

	public EnemyAegisController.SetupParam aegisSetupParam;

	public Vector3[] tailPosList;

	public int bulletIndex;

	public float walkSpeedRateFromTable = 1f;

	public ELEMENT_TYPE changeElementIcon = ELEMENT_TYPE.MAX;

	public ELEMENT_TYPE changeWeakElementIcon = ELEMENT_TYPE.MAX;

	public int changeToleranceRegionId = -1;

	public int changeToleranceScroll = -1;

	public List<BlendColorCtrl.ShaderSyncParam> shaderSyncParam;

	public Coop_Model_EnemyInitialize()
	{
		base.packetType = PACKET_TYPE.ENEMY_INITIALIZE;
	}

	public override bool IsPromiseOverAgainCheck()
	{
		return true;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}

	public void SetRegionWorks(EnemyRegionWork[] region_works)
	{
		if (region_works != null)
		{
			regions = new List<Enemy.RegionWorkSyncData>();
			int i = 0;
			for (int num = region_works.Length; i < num; i++)
			{
				Enemy.RegionWorkSyncData regionWorkSyncData = new Enemy.RegionWorkSyncData();
				EnemyRegionWork enemyRegionWork = region_works[i];
				regionWorkSyncData.hp = enemyRegionWork.hp;
				regionWorkSyncData.isBroke = enemyRegionWork.isBroke;
				regionWorkSyncData.bleedList = enemyRegionWork.bleedList;
				regionWorkSyncData.isShieldDamage = enemyRegionWork.isShieldDamage;
				regionWorkSyncData.isShieldCriticalDamage = enemyRegionWork.isShieldCriticalDamage;
				regionWorkSyncData.shadowSealingData = enemyRegionWork.shadowSealingData;
				regions.Add(regionWorkSyncData);
			}
		}
	}
}

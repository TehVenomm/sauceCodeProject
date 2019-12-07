using System;
using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_StageInfo : Coop_Model_Base
{
	[Serializable]
	public class GimmickInfo
	{
		public int id;

		public bool enable;
	}

	public class WaveTargetInfo
	{
		public int id;

		public int hp;
	}

	public class FieldCarriableGimmickInfo
	{
		public int pointId;

		public bool enable;

		public int currentLv;

		public Vector3 position;
	}

	public class FieldSupplyGimmickInfo
	{
		public int pointId;

		public bool canUse;

		public int suppliedCount;
	}

	public float elapsedTime;

	public List<GimmickInfo> gimmicks = new List<GimmickInfo>();

	public List<FieldCarriableGimmickInfo> carriableGimmickInfos = new List<FieldCarriableGimmickInfo>();

	public List<FieldSupplyGimmickInfo> supplyGimmickInfos = new List<FieldSupplyGimmickInfo>();

	public Vector3 enemyPos = Vector3.zero;

	public float rushLimitTime = -1f;

	public bool isInFieldEnemyBossBattle;

	public bool isInFieldFishingEnemyBattle;

	public List<WaveTargetInfo> waveTargets = new List<WaveTargetInfo>();

	public Coop_Model_WaveMatchInfo firstWaveMatchInfo;

	public float firstWaveMatchPopSec;

	public Coop_Model_StageInfo()
	{
		base.packetType = PACKET_TYPE.STAGE_INFO;
	}
}

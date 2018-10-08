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

	public float elapsedTime;

	public List<GimmickInfo> gimmicks = new List<GimmickInfo>();

	public Vector3 enemyPos = Vector3.get_zero();

	public float rushLimitTime = -1f;

	public bool isInFieldEnemyBossBattle;

	public Coop_Model_StageInfo()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.STAGE_INFO;
	}
}

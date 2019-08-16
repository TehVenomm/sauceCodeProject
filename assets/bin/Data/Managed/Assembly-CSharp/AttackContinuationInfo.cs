using System;
using UnityEngine;

[Serializable]
public class AttackContinuationInfo : AttackInfo
{
	public enum CONTINUATION_TYPE
	{
		NONE,
		INHALE,
		BARRIER,
		SPEAR_BURST
	}

	[Serializable]
	public class Inhale
	{
		[Tooltip("吸い込み速度(距離/s)")]
		public float speed;
	}

	[Serializable]
	public class Barrier
	{
	}

	[Serializable]
	public class ByContinuationType
	{
		public Barrier barrier = new Barrier();
	}

	[Tooltip("効果タイプ")]
	public CONTINUATION_TYPE type;

	[Tooltip("座標固定化設定を無効化するか")]
	public bool disableUpdateFixTrans;

	public Inhale inhale = new Inhale();

	public ByContinuationType infoByContinuationType = new ByContinuationType();

	public override AttackInfo GetRateAttackInfo(AttackInfo rate_info, float rate)
	{
		AttackContinuationInfo attackContinuationInfo = base.GetRateAttackInfo(rate_info, rate) as AttackContinuationInfo;
		if (attackContinuationInfo == this)
		{
			return attackContinuationInfo;
		}
		AttackContinuationInfo attackContinuationInfo2 = rate_info as AttackContinuationInfo;
		if (attackContinuationInfo2 == null)
		{
			return attackContinuationInfo;
		}
		attackContinuationInfo.type = type;
		attackContinuationInfo.disableUpdateFixTrans = disableUpdateFixTrans;
		if (attackContinuationInfo.type != attackContinuationInfo2.type)
		{
			return attackContinuationInfo;
		}
		attackContinuationInfo.inhale.speed = AttackInfo.GetRateValue(inhale.speed, attackContinuationInfo2.inhale.speed, rate);
		return attackContinuationInfo;
	}

	protected override AttackInfo CreateInfo()
	{
		return new AttackContinuationInfo();
	}

	public override void Copy(ref AttackInfo rInfo)
	{
		base.Copy(ref rInfo);
		AttackContinuationInfo attackContinuationInfo = rInfo as AttackContinuationInfo;
		if (attackContinuationInfo != null)
		{
			attackContinuationInfo.type = type;
			attackContinuationInfo.disableUpdateFixTrans = disableUpdateFixTrans;
			attackContinuationInfo.inhale.speed = inhale.speed;
		}
	}
}

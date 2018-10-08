using UnityEngine;

public class SplitPlayerAttackInfo
{
	[Tooltip("攻撃情報")]
	public AttackHitInfo attackHitInfo;

	[Tooltip("攻撃継続効果情報")]
	public AttackContinuationInfo attackContinuationInfo;

	public SplitPlayerAttackInfo()
		: this()
	{
	}
}
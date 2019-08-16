using UnityEngine;

public class AttackInfos : MonoBehaviour
{
	[Tooltip("攻撃情報")]
	public AttackHitInfo[] attackHitInfos;

	[Tooltip("攻撃継続効果情報")]
	public AttackContinuationInfo[] attackContinuationInfos;

	public AttackInfos()
		: this()
	{
	}
}

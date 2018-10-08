using UnityEngine;

public class EnemyColliderSettings : MonoBehaviour
{
	[Tooltip("対象collider")]
	public Collider targetCollider;

	[Tooltip("攻撃ヒット無視フラグ")]
	public bool ignoreHitAttack = true;
}

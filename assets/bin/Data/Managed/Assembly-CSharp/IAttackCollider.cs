using System.Collections.Generic;
using UnityEngine;

public interface IAttackCollider
{
	float GetTime();

	bool IsEnable();

	void SortHitStackList(List<AttackHitColliderProcessor.HitResult> stack_list);

	Vector3 GetCrossCheckPoint(Collider from_collider);

	bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object);

	void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param);

	AttackInfo GetAttackInfo();

	StageObject GetFromObject();
}

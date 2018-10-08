using System.Collections.Generic;
using UnityEngine;

public class AttackColliderManager : MonoBehaviourSingleton<AttackColliderManager>
{
	private List<AttackColliderProcessor> processorList = new List<AttackColliderProcessor>();

	private List<AttackColliderProcessor> tempList = new List<AttackColliderProcessor>();

	public AttackColliderProcessor CreateProcessor(AttackInfo _attack_info, StageObject _object, Collider _collider, IAttackCollider _collider_interface, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, DamageDistanceTable.DamageDistanceData damageDistanceData = null)
	{
		AttackColliderProcessor attackColliderProcessor = null;
		if (_attack_info is AttackHitInfo)
		{
			attackColliderProcessor = new AttackHitColliderProcessor();
		}
		else if (_attack_info is AttackContinuationInfo)
		{
			attackColliderProcessor = new AttackContinuationColliderProcessor();
		}
		attackColliderProcessor.SetFromInfo(_attack_info, _object, _collider, _collider_interface);
		attackColliderProcessor.SetAttackMode(attackMode);
		attackColliderProcessor.SetDamageDistanceData(damageDistanceData);
		AddProcessor(attackColliderProcessor);
		return attackColliderProcessor;
	}

	public void AddProcessor(AttackColliderProcessor processor)
	{
		if (processorList.IndexOf(processor) < 0)
		{
			processorList.Add(processor);
		}
	}

	public void RemoveProcessor(AttackColliderProcessor processor)
	{
		processorList.Remove(processor);
	}

	private void FixedUpdate()
	{
		if (processorList.Count > 0)
		{
			tempList.Clear();
			if (tempList.Capacity < processorList.Count)
			{
				tempList.Capacity = processorList.Count;
			}
			int i = 0;
			for (int count = processorList.Count; i < count; i++)
			{
				tempList.Add(processorList[i]);
			}
			int j = 0;
			for (int count2 = tempList.Count; j < count2; j++)
			{
				tempList[j].OnFixedUpdate();
			}
		}
	}
}

using UnityEngine;

public class BulletControllerSearch : BulletControllerBase
{
	private bool isOwnerSelf;

	private Player ownerPlayer;

	private Character targetEnemy;

	private int targetId = -1;

	private float searchStartTime;

	private float angularVelocity;

	private bool isStart;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillParam, pos, rot);
		searchStartTime = bullet.dataSearch.searchStartTime;
		angularVelocity = bullet.dataSearch.angularVelocity;
		base._rigidbody.velocity = Vector3.zero;
		isStart = false;
	}

	public override void RegisterTargetObject(StageObject obj)
	{
		base.RegisterTargetObject(obj);
		if (obj != null)
		{
			targetEnemy = (obj as Character);
			targetId = targetEnemy.id;
		}
	}

	public override void PostInitialize()
	{
		ownerPlayer = (fromObject as Player);
		isOwnerSelf = (fromObject is Self);
	}

	public override void Update()
	{
		base.Update();
		if (searchStartTime > base.timeCount)
		{
			return;
		}
		if (!isStart)
		{
			isStart = true;
			base._rigidbody.velocity = base._transform.forward * base.speed * Time.deltaTime;
		}
		Vector3 vector = GetDestination() - base._transform.position;
		vector.y = 0f;
		float num = Mathf.Abs(Vector3.Angle(base._transform.forward, vector));
		if (!(num <= 0f))
		{
			float num2 = angularVelocity * Time.deltaTime / num;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			base._transform.rotation = Quaternion.Lerp(base._transform.rotation, Quaternion.LookRotation(vector), num2);
			base._rigidbody.velocity = base._transform.forward * base.speed * Time.deltaTime;
		}
	}

	private Vector3 GetDestination()
	{
		if (isOwnerSelf)
		{
			CheckTarget();
			if (targetEnemy == null)
			{
				return fromObject._position;
			}
			return targetEnemy._position;
		}
		if (targetId == -1)
		{
			return fromObject._position;
		}
		return targetEnemy._position;
	}

	private void CheckTarget()
	{
		if (targetEnemy != null && !targetEnemy.isDead)
		{
			return;
		}
		targetEnemy = null;
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		float num = float.MaxValue;
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; i < count; i++)
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[i] as Enemy;
			if (!(enemy == null) && !enemy.isDead && !enemy.enableAssimilation)
			{
				float sqrMagnitude = (enemy._position - base._transform.position).sqrMagnitude;
				if (num > sqrMagnitude && sqrMagnitude <= bulletObject.bulletData.dataSearch.searchRangeSqr)
				{
					targetEnemy = enemy;
					num = sqrMagnitude;
				}
			}
		}
		if (ownerPlayer == null || ownerPlayer.playerSender == null)
		{
			return;
		}
		if (targetEnemy != null)
		{
			if (targetId == targetEnemy.id)
			{
				return;
			}
			targetId = targetEnemy.id;
		}
		else
		{
			if (targetId == -1)
			{
				return;
			}
			targetId = -1;
		}
		ownerPlayer.playerSender.OnBulletObservableSearchTarget(bulletObject.GetObservedID(), targetId);
	}

	public void SetTargetId(int id)
	{
		targetId = id;
		if (targetId == -1)
		{
			targetEnemy = null;
			return;
		}
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; i < count; i++)
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[i] as Enemy;
			if (!(enemy == null) && enemy.id == id)
			{
				targetEnemy = enemy;
				return;
			}
		}
		targetEnemy = null;
		targetId = -1;
	}
}

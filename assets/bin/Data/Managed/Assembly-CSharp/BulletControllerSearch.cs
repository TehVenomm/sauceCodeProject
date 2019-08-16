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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, skillParam, pos, rot);
		searchStartTime = bullet.dataSearch.searchStartTime;
		angularVelocity = bullet.dataSearch.angularVelocity;
		base._rigidbody.set_velocity(Vector3.get_zero());
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
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		if (searchStartTime > base.timeCount)
		{
			return;
		}
		if (!isStart)
		{
			isStart = true;
			base._rigidbody.set_velocity(base._transform.get_forward() * base.speed * Time.get_deltaTime());
		}
		Vector3 val = GetDestination() - base._transform.get_position();
		val.y = 0f;
		float num = Mathf.Abs(Vector3.Angle(base._transform.get_forward(), val));
		if (!(num <= 0f))
		{
			float num2 = angularVelocity * Time.get_deltaTime() / num;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			base._transform.set_rotation(Quaternion.Lerp(base._transform.get_rotation(), Quaternion.LookRotation(val), num2));
			base._rigidbody.set_velocity(base._transform.get_forward() * base.speed * Time.get_deltaTime());
		}
	}

	private Vector3 GetDestination()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
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
				Vector3 val = enemy._position - base._transform.get_position();
				float sqrMagnitude = val.get_sqrMagnitude();
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

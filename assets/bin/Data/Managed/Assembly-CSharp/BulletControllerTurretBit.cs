using UnityEngine;

public class BulletControllerTurretBit : BulletControllerFollow
{
	private bool isEndDelay;

	private float firstShotDelayTimer;

	private float shotInterval_Sec;

	private float shotIntervalTimer;

	private float lookAtInterpolate = 0.2f;

	private float searchInterval_Sec;

	private float searchIntervalTimer;

	private float searchRangeSqr = 10f;

	private AttackInfo normalAtkInfo;

	private AttackInfo finalAttackInfo;

	private BulletData.BulletTurretBit dataTurretBit;

	private Player fromPlayer;

	private Enemy targetEnemy;

	private TargetPoint targetPoint;

	private AtkAttribute exNormalAtk;

	private AtkAttribute exFinalAtk;

	private Player.ATTACK_MODE exAttackMode;

	private SkillInfo.SkillParam exSkillParam;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, skillParam, pos, rot);
		followOffset = bullet.dataTurretBit.followOffset;
		attenuation = bullet.dataTurretBit.attenuation;
		dataTurretBit = bullet.dataTurretBit;
		firstShotDelayTimer = dataTurretBit.firstShotDelay_Sec;
		shotInterval_Sec = dataTurretBit.shotInterval_Sec;
		shotIntervalTimer = 0f;
		lookAtInterpolate = dataTurretBit.lookAtInterpolate;
		searchInterval_Sec = dataTurretBit.searchInterval_Sec;
		searchIntervalTimer = 0f;
		searchRangeSqr = dataTurretBit.searchRangeSqr;
		isEndDelay = false;
		targetEnemy = null;
		targetPoint = null;
	}

	public override void PostInitialize()
	{
		normalAtkInfo = fromObject.FindAttackInfo(dataTurretBit.normalAtkInfoName);
		finalAttackInfo = fromObject.FindAttackInfo(dataTurretBit.finalAtkInfoName);
		if (normalAtkInfo != null)
		{
			exNormalAtk = new AtkAttribute();
			fromPlayer.GetAtk(normalAtkInfo as AttackHitInfo, ref exNormalAtk);
		}
		if (finalAttackInfo != null)
		{
			exFinalAtk = new AtkAttribute();
			fromPlayer.GetAtk(finalAttackInfo as AttackHitInfo, ref exFinalAtk);
		}
		exAttackMode = fromPlayer.attackMode;
		exSkillParam = fromPlayer.skillInfo.actSkillParam;
		if (!(fromPlayer == null) && dataTurretBit.uniqueId > 0)
		{
			fromPlayer.RegistBulletTurret((uint)dataTurretBit.uniqueId, this);
		}
	}

	public override void RegisterFromObject(StageObject obj)
	{
		base.RegisterFromObject(obj);
		fromPlayer = (obj as Player);
	}

	public override void DestroyBulletObject()
	{
		if (isEndDelay && !(targetEnemy == null))
		{
			CreateBullet(finalAttackInfo, isFinal: true);
		}
	}

	public override void Update()
	{
		base.Update();
		if (UpdateTarget())
		{
			SendTarget();
		}
		UpdateShot();
	}

	private bool UpdateTarget()
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		if (fromPlayer == null || fromPlayer.playerSender == null)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		searchIntervalTimer -= Time.get_deltaTime();
		if (searchIntervalTimer > 0f)
		{
			return false;
		}
		searchIntervalTimer += searchInterval_Sec;
		targetEnemy = null;
		this.targetPoint = null;
		float num = float.MaxValue;
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; i < count; i++)
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[i] as Enemy;
			if (!(enemy == null) && !enemy.isDead)
			{
				Vector3 val = enemy._position - base._transform.get_position();
				float sqrMagnitude = val.get_sqrMagnitude();
				if (num > sqrMagnitude && sqrMagnitude <= searchRangeSqr)
				{
					targetEnemy = enemy;
					num = sqrMagnitude;
				}
			}
		}
		if (targetEnemy == null)
		{
			if (!(MonoBehaviourSingleton<StageObjectManager>.I.boss != null))
			{
				shotIntervalTimer = 0f;
				return true;
			}
			targetEnemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		}
		if (!targetEnemy.isBoss)
		{
			return true;
		}
		num = float.MaxValue;
		int j = 0;
		for (int num2 = targetEnemy.targetPoints.Length; j < num2; j++)
		{
			TargetPoint targetPoint = targetEnemy.targetPoints[j];
			if (!targetPoint.get_enabled() || !targetPoint.get_gameObject().get_activeInHierarchy())
			{
				continue;
			}
			if (targetPoint.regionID >= 0 && targetPoint.regionID < targetEnemy.regionWorks.Length)
			{
				EnemyRegionWork enemyRegionWork = targetEnemy.regionWorks[targetPoint.regionID];
				if (!enemyRegionWork.enabled)
				{
					continue;
				}
			}
			Vector3 val2 = targetPoint._transform.get_position() - base._transform.get_position();
			float sqrMagnitude2 = val2.get_sqrMagnitude();
			if (num > sqrMagnitude2)
			{
				this.targetPoint = targetPoint;
				num = sqrMagnitude2;
			}
		}
		return true;
	}

	private void SendTarget()
	{
		int targetId = -1;
		if (targetEnemy != null)
		{
			targetId = targetEnemy.id;
		}
		int regionId = -1;
		if (targetPoint != null)
		{
			regionId = targetPoint.regionID;
		}
		fromPlayer.playerSender.OnBulletObservableTurretBitTarget(bulletObject.GetObservedID(), targetId, regionId);
	}

	public void UpdateShot()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (!isEndDelay)
		{
			firstShotDelayTimer -= Time.get_deltaTime();
			if (firstShotDelayTimer > 0f)
			{
				return;
			}
			isEndDelay = true;
		}
		if (!(targetEnemy == null))
		{
			Quaternion val = Quaternion.LookRotation(GetTargetPos() - base._transform.get_position());
			base._transform.set_rotation(Quaternion.Lerp(base._transform.get_rotation(), val, lookAtInterpolate));
			shotIntervalTimer -= Time.get_deltaTime();
			if (!(shotIntervalTimer > 0f))
			{
				shotIntervalTimer += shotInterval_Sec;
				CreateBullet(normalAtkInfo, isFinal: false);
			}
		}
	}

	private Vector3 GetTargetPos()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (targetEnemy == null)
		{
			return Vector3.get_zero();
		}
		if (targetPoint != null)
		{
			return targetPoint._transform.get_position();
		}
		return targetEnemy._position;
	}

	private void CreateBullet(AttackInfo atkInfo, bool isFinal)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (atkInfo != null)
		{
			BulletData bulletData = atkInfo.bulletData;
			if (!(bulletData == null) && !(fromObject == null))
			{
				AnimEventShot.CreateByExternalBulletData(bulletData, fromObject, atkInfo, base._transform.get_position(), base._transform.get_rotation(), (!isFinal) ? exNormalAtk : exFinalAtk, exAttackMode, exSkillParam);
			}
		}
	}

	public void SetTargetId(int targetID, int regionID)
	{
		targetEnemy = null;
		targetPoint = null;
		if (targetID <= 0)
		{
			return;
		}
		StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Find((StageObject obj) => obj.id == targetID);
		if (!(stageObject == null))
		{
			targetEnemy = (stageObject as Enemy);
			if (regionID >= 0 && !(targetEnemy == null) && targetEnemy.isBoss && regionID < targetEnemy.targetPoints.Length)
			{
				targetPoint = targetEnemy.targetPoints[regionID];
			}
		}
	}
}

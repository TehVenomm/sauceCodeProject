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
		if (fromPlayer == null || fromPlayer.playerSender == null)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		searchIntervalTimer -= Time.deltaTime;
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
				float sqrMagnitude = (enemy._position - base._transform.position).sqrMagnitude;
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
			if (targetPoint.enabled && targetPoint.gameObject.activeInHierarchy && (targetPoint.regionID < 0 || targetPoint.regionID >= targetEnemy.regionWorks.Length || targetEnemy.regionWorks[targetPoint.regionID].enabled))
			{
				float sqrMagnitude2 = (targetPoint._transform.position - base._transform.position).sqrMagnitude;
				if (num > sqrMagnitude2)
				{
					this.targetPoint = targetPoint;
					num = sqrMagnitude2;
				}
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
		if (!isEndDelay)
		{
			firstShotDelayTimer -= Time.deltaTime;
			if (firstShotDelayTimer > 0f)
			{
				return;
			}
			isEndDelay = true;
		}
		if (!(targetEnemy == null))
		{
			Quaternion b = Quaternion.LookRotation(GetTargetPos() - base._transform.position);
			base._transform.rotation = Quaternion.Lerp(base._transform.rotation, b, lookAtInterpolate);
			shotIntervalTimer -= Time.deltaTime;
			if (!(shotIntervalTimer > 0f))
			{
				shotIntervalTimer += shotInterval_Sec;
				CreateBullet(normalAtkInfo, isFinal: false);
			}
		}
	}

	private Vector3 GetTargetPos()
	{
		if (targetEnemy == null)
		{
			return Vector3.zero;
		}
		if (targetPoint != null)
		{
			return targetPoint._transform.position;
		}
		return targetEnemy._position;
	}

	private void CreateBullet(AttackInfo atkInfo, bool isFinal)
	{
		if (atkInfo != null)
		{
			BulletData bulletData = atkInfo.bulletData;
			if (!(bulletData == null) && !(fromObject == null))
			{
				AnimEventShot.CreateByExternalBulletData(bulletData, fromObject, atkInfo, base._transform.position, base._transform.rotation, isFinal ? exFinalAtk : exNormalAtk, exAttackMode, exSkillParam);
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

using UnityEngine;

public class DecoyTurretBitBulletObject : DecoyBulletObject
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

	private BulletData.BulletDecoyTurretBit dataTurretBit;

	private Enemy targetEnemy;

	private TargetPoint targetPoint;

	private AtkAttribute exNormalAtk;

	private AtkAttribute exFinalAtk;

	private Player.ATTACK_MODE exAttackMode;

	private SkillInfo.SkillParam exSkillParam;

	protected Transform rotateTransForm;

	protected Transform cachedChargeEffectTransform;

	protected override BulletData.BulletDecoy GetDecoyData(BulletData bullet)
	{
		return bullet.dataDecoyTurretBit;
	}

	protected override bool IsHitExplede()
	{
		return false;
	}

	protected override void Update()
	{
		base.Update();
		UpdateTarget();
		UpdateShot();
		if (ownerPlayer == null)
		{
			OnDisappear(isExplode: true);
		}
	}

	public override void Initialize(int playerId, int decoyId, BulletData bullet, Vector3 position, SkillInfo.SkillParam skill, bool isHit)
	{
		base.Initialize(playerId, decoyId, bullet, position, skill, isHit);
		dataTurretBit = bullet.dataDecoyTurretBit;
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
		normalAtkInfo = ownerPlayer.FindAttackInfo(dataTurretBit.normalAtkInfoName);
		finalAttackInfo = ownerPlayer.FindAttackInfo(dataTurretBit.finalAtkInfoName);
		if (normalAtkInfo != null)
		{
			exNormalAtk = new AtkAttribute();
			ownerPlayer.GetAtk(normalAtkInfo as AttackHitInfo, ref exNormalAtk, skill);
		}
		if (finalAttackInfo != null)
		{
			exFinalAtk = new AtkAttribute();
			ownerPlayer.GetAtk(finalAttackInfo as AttackHitInfo, ref exFinalAtk, skill);
		}
		exAttackMode = ownerPlayer.attackMode;
		exSkillParam = ownerPlayer.skillInfo.actSkillParam;
		Transform transform = Utility.FindChild(cachedEffectTransform, dataTurretBit.chargeEffectParentNodename);
		if (transform == null)
		{
			transform = cachedEffectTransform;
		}
		if (!string.IsNullOrEmpty(dataTurretBit.chargeEffectName))
		{
			cachedChargeEffectTransform = EffectManager.GetEffect(dataTurretBit.chargeEffectName, MonoBehaviourSingleton<EffectManager>.I._transform);
			cachedChargeEffectTransform.SetParent(transform);
			cachedChargeEffectTransform.localPosition = dataTurretBit.chargeEffectDispOffset;
			cachedChargeEffectTransform.localRotation = Quaternion.Euler(dataTurretBit.chargeEffectDispRotation);
		}
		rotateTransForm = Utility.FindChild(cachedEffectTransform, dataTurretBit.rotateNodeName);
		if (rotateTransForm == null)
		{
			rotateTransForm = cachedEffectTransform;
		}
	}

	private bool UpdateTarget()
	{
		if (ownerPlayer == null || ownerPlayer.playerSender == null)
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

	public void UpdateShot()
	{
		if (rotateTransForm == null)
		{
			return;
		}
		Quaternion b = Quaternion.LookRotation(GetTargetPos() - rotateTransForm.position);
		rotateTransForm.rotation = Quaternion.Lerp(rotateTransForm.rotation, b, lookAtInterpolate);
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

	public override void OnDisappear(bool isExplode)
	{
		CreateBullet(finalAttackInfo, isFinal: true);
		if (cachedChargeEffectTransform != null)
		{
			EffectManager.ReleaseEffect(cachedChargeEffectTransform.gameObject);
		}
		base.OnDisappear(isExplode);
	}

	private void CreateBullet(AttackInfo atkInfo, bool isFinal)
	{
		if (atkInfo != null)
		{
			BulletData bulletData = atkInfo.bulletData;
			if (!(bulletData == null) && !(ownerPlayer == null) && !(cachedEffectTransform == null))
			{
				Quaternion rot = Quaternion.LookRotation(GetTargetPos() - cachedEffectTransform.position);
				AnimEventShot.CreateByExternalBulletData(bulletData, ownerPlayer, atkInfo, cachedEffectTransform.position, rot, isFinal ? exFinalAtk : exNormalAtk, exAttackMode, exSkillParam);
			}
		}
	}
}

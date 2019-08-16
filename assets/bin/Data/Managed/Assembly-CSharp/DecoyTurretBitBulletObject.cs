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
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
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
		Transform val = Utility.FindChild(cachedEffectTransform, dataTurretBit.chargeEffectParentNodename);
		if (val == null)
		{
			val = cachedEffectTransform;
		}
		if (!string.IsNullOrEmpty(dataTurretBit.chargeEffectName))
		{
			cachedChargeEffectTransform = EffectManager.GetEffect(dataTurretBit.chargeEffectName, MonoBehaviourSingleton<EffectManager>.I._transform);
			cachedChargeEffectTransform.SetParent(val);
			cachedChargeEffectTransform.set_localPosition(dataTurretBit.chargeEffectDispOffset);
			cachedChargeEffectTransform.set_localRotation(Quaternion.Euler(dataTurretBit.chargeEffectDispRotation));
		}
		rotateTransForm = Utility.FindChild(cachedEffectTransform, dataTurretBit.rotateNodeName);
		if (rotateTransForm == null)
		{
			rotateTransForm = cachedEffectTransform;
		}
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
		if (ownerPlayer == null || ownerPlayer.playerSender == null)
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

	public void UpdateShot()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (rotateTransForm == null)
		{
			return;
		}
		Vector3 targetPos = GetTargetPos();
		Quaternion val = Quaternion.LookRotation(targetPos - rotateTransForm.get_position());
		rotateTransForm.set_rotation(Quaternion.Lerp(rotateTransForm.get_rotation(), val, lookAtInterpolate));
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

	public override void OnDisappear(bool isExplode)
	{
		CreateBullet(finalAttackInfo, isFinal: true);
		if (cachedChargeEffectTransform != null)
		{
			EffectManager.ReleaseEffect(cachedChargeEffectTransform.get_gameObject());
		}
		base.OnDisappear(isExplode);
	}

	private void CreateBullet(AttackInfo atkInfo, bool isFinal)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (atkInfo != null)
		{
			BulletData bulletData = atkInfo.bulletData;
			if (!(bulletData == null) && !(ownerPlayer == null) && !(cachedEffectTransform == null))
			{
				Quaternion rot = Quaternion.LookRotation(GetTargetPos() - cachedEffectTransform.get_position());
				AnimEventShot.CreateByExternalBulletData(bulletData, ownerPlayer, atkInfo, cachedEffectTransform.get_position(), rot, (!isFinal) ? exNormalAtk : exFinalAtk, exAttackMode, exSkillParam);
			}
		}
	}
}

using UnityEngine;

public class DecoyBulletObject : StageObject
{
	public class HateInfo
	{
		public StageObject target;

		public Hate.TYPE type;

		public int value;
	}

	private readonly string OBJ_NAME = "DecoyBullet:";

	private static int sLocalId;

	protected Player ownerPlayer;

	private BulletData bulletData;

	private SkillInfo.SkillParam skillParam;

	private AttackInfo atkInfo;

	private AtkAttribute exAtk = new AtkAttribute();

	private Transform cachedTransform;

	protected Transform cachedEffectTransform;

	private SphereCollider cachedCollider;

	private int ignoreLayerMask;

	private float lifeTime;

	private float dontHitSec;

	private new bool isInitialized;

	private float useRate = 1f;

	private bool isHitExplode;

	private float hitNum;

	private float hitInterval;

	private bool isEnableHateInterval;

	private float hateInterval;

	private bool isUseLifeTime;

	private FieldCarriableDecoyGimmickObject carriableGimmick;

	public override AttackInfo[] GetAttackInfos()
	{
		if (ownerPlayer == null)
		{
			return null;
		}
		return ownerPlayer.GetAttackInfos();
	}

	public virtual void Initialize(int playerId, int decoyId, BulletData bullet, Vector3 position, SkillInfo.SkillParam skill, bool isHit)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize DecoyBulletObject.");
			return;
		}
		base.objectType = OBJECT_TYPE.DECOY;
		StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerId);
		if ((object)stageObject != null)
		{
			ownerPlayer = (stageObject as Player);
		}
		bulletData = bullet;
		skillParam = skill;
		hateInterval = 0f;
		cachedTransform = base.transform;
		cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
		cachedTransform.position = position;
		cachedTransform.localScale = Vector3.one;
		if (bulletData != null)
		{
			lifeTime = bulletData.data.appearTime;
			dontHitSec = GetDecoyData(bulletData).dontHitSec;
			hitNum = GetDecoyData(bulletData).explodeHitNum;
			isHitExplode = (hitNum > 0f);
			isEnableHateInterval = (GetDecoyData(bulletData).hateInterval > 0f);
			isUseLifeTime = (bulletData.data.appearTime > 0f);
			if (MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				cachedEffectTransform = EffectManager.GetEffect(bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
				cachedEffectTransform.position = cachedTransform.position + bulletData.data.dispOffset;
				cachedEffectTransform.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
			}
		}
		id = decoyId;
		if (decoyId > 0)
		{
			base.gameObject.name = OBJ_NAME + decoyId;
			base.gameObject.layer = 12;
			ignoreLayerMask |= -1073741824;
		}
		if ((object)ownerPlayer != null && isHit && skillParam != null)
		{
			cachedCollider = base.gameObject.AddComponent<SphereCollider>();
			cachedCollider.radius = bulletData.data.radius;
			cachedCollider.isTrigger = true;
			cachedCollider.enabled = true;
			if (!string.IsNullOrEmpty(skillParam.tableData.attackInfoNames[0]))
			{
				atkInfo = FindAttackInfo(skillParam.tableData.attackInfoNames[0]);
			}
			ownerPlayer.GetAtk(atkInfo as AttackHitInfo, ref exAtk, skill);
		}
		if (QuestManager.IsValidInGameWaveStrategy() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.SetAllEnemiesTargetDecoy();
		}
		isInitialized = true;
	}

	protected virtual BulletData.BulletDecoy GetDecoyData(BulletData bullet)
	{
		return bullet.dataDecoy;
	}

	private void Destroy()
	{
		OnDisappear(isExplode: false);
	}

	protected override void Update()
	{
		if (!isInitialized)
		{
			return;
		}
		dontHitSec -= Time.deltaTime;
		if (hitInterval > 0f)
		{
			hitInterval -= Time.deltaTime;
		}
		if (isEnableHateInterval)
		{
			hateInterval -= Time.deltaTime;
			if (hateInterval <= 0f)
			{
				HateCtrl();
			}
		}
		lifeTime -= Time.deltaTime;
		if (isUseLifeTime && lifeTime <= 0f)
		{
			OnDisappear(isExplode: true);
		}
	}

	public virtual void OnDisappear(bool isExplode)
	{
		if ((object)cachedEffectTransform != null && (object)cachedEffectTransform.gameObject != null)
		{
			EffectManager.ReleaseEffect(cachedEffectTransform.gameObject);
		}
		cachedEffectTransform = null;
		if (isExplode && atkInfo != null)
		{
			StageObject stage_object = ownerPlayer;
			if ((object)ownerPlayer == null)
			{
				stage_object = this;
			}
			AnimEventShot.Create(stage_object, atkInfo, cachedTransform.position, Quaternion.identity, null, isScaling: true, null, null, exAtk, Player.ATTACK_MODE.NONE, null, skillParam);
		}
		ownerPlayer = null;
		atkInfo = null;
		bulletData = null;
		if ((object)base.gameObject != null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	protected virtual bool IsHitExplede()
	{
		return isHitExplode;
	}

	private void OnTriggerStay(Collider collider)
	{
		if (!IsHitExplede() || dontHitSec > 0f)
		{
			return;
		}
		int layer = collider.gameObject.layer;
		if (((1 << layer) & ignoreLayerMask) > 0 || hitInterval > 0f)
		{
			return;
		}
		hitInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.decoyHitInterval;
		if (!((hitNum -= 1f) > 0f))
		{
			if ((object)ownerPlayer != null)
			{
				ownerPlayer.ExecExplodeDecoyBullet(id);
			}
			OnDisappear(isExplode: true);
		}
	}

	public void HateCtrl()
	{
		if ((object)bulletData == null || GetDecoyData(bulletData) == null)
		{
			return;
		}
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if ((object)boss == null || (!boss.IsOriginal() && !boss.IsCoopNone()))
		{
			return;
		}
		EnemyBrain component = MonoBehaviourSingleton<StageObjectManager>.I.boss.GetComponent<EnemyBrain>();
		if ((object)component == null)
		{
			return;
		}
		float num = 1f;
		if (skillParam != null)
		{
			num = (float)skillParam.supportValue[0] * 0.01f;
		}
		if (GetDecoyData(bulletData).addDecoyHate.Length != 0)
		{
			int i = 0;
			for (int num2 = GetDecoyData(bulletData).addDecoyHate.Length; i < num2; i++)
			{
				BulletData.BulletDecoy.HateInfo hateInfo = GetDecoyData(bulletData).addDecoyHate[i];
				HateInfo hateInfo2 = new HateInfo();
				hateInfo2.target = this;
				hateInfo2.value = (int)((float)hateInfo.value * num * useRate);
				hateInfo2.type = hateInfo.type;
				component.HandleEvent(BRAIN_EVENT.DECOY, hateInfo2);
			}
		}
		if ((object)ownerPlayer != null && GetDecoyData(bulletData).addOwnerHate.Length != 0)
		{
			int j = 0;
			for (int num3 = GetDecoyData(bulletData).addOwnerHate.Length; j < num3; j++)
			{
				BulletData.BulletDecoy.HateInfo hateInfo3 = GetDecoyData(bulletData).addOwnerHate[j];
				HateInfo hateInfo4 = new HateInfo();
				hateInfo4.target = ownerPlayer;
				hateInfo4.value = (int)((float)hateInfo3.value * num * useRate);
				hateInfo4.type = hateInfo3.type;
				component.HandleEvent(BRAIN_EVENT.DECOY, hateInfo4);
			}
		}
		useRate *= GetDecoyData(bulletData).hateDecreaseRate;
		hateInterval += GetDecoyData(bulletData).hateInterval;
	}

	public bool IsActive()
	{
		if (!base.gameObject.activeSelf)
		{
			return false;
		}
		if (carriableGimmick != null)
		{
			return carriableGimmick.isActive;
		}
		return true;
	}

	public void SetCarriable(FieldCarriableDecoyGimmickObject carriable)
	{
		carriableGimmick = carriable;
	}
}

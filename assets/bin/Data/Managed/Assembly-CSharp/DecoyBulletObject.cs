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

	private Player ownerPlayer;

	private BulletData bulletData;

	private SkillInfo.SkillParam skillParam;

	private AttackInfo atkInfo;

	private AtkAttribute exAtk = new AtkAttribute();

	private Transform cachedTransform;

	private Transform cachedEffectTransform;

	private SphereCollider cachedCollider;

	private int ignoreLayerMask;

	private float lifeTime;

	private float dontHitSec;

	private new bool isInitialized;

	private float useRate = 1f;

	private bool isHitExplode = true;

	private float hitNum;

	private float hitInterval;

	private bool isEnableHateInterval;

	private float hateInterval;

	public override AttackInfo[] GetAttackInfos()
	{
		if ((Object)ownerPlayer == (Object)null)
		{
			return null;
		}
		return ownerPlayer.GetAttackInfos();
	}

	public void Initialize(int playerId, int decoyId, BulletData bullet, Vector3 position, SkillInfo.SkillParam skill, bool isHit)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize DecoyBulletObject.");
		}
		else
		{
			base.objectType = OBJECT_TYPE.DECOY;
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerId);
			if (!object.ReferenceEquals(stageObject, null))
			{
				ownerPlayer = (stageObject as Player);
			}
			bulletData = bullet;
			skillParam = skill;
			lifeTime = bulletData.data.appearTime;
			dontHitSec = bulletData.dataDecoy.dontHitSec;
			hitNum = (float)bulletData.dataDecoy.explodeHitNum;
			isHitExplode = (hitNum > 0f);
			isEnableHateInterval = (bulletData.dataDecoy.hateInterval > 0f);
			hateInterval = 0f;
			cachedTransform = base.transform;
			cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
			cachedTransform.position = position;
			cachedTransform.localScale = Vector3.one;
			if (MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				cachedEffectTransform = EffectManager.GetEffect(bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
				cachedEffectTransform.position = cachedTransform.position + bulletData.data.dispOffset;
				cachedEffectTransform.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
			}
			id = decoyId;
			base.gameObject.name = OBJ_NAME + decoyId;
			base.gameObject.layer = 12;
			ignoreLayerMask |= -1073741824;
			if (!object.ReferenceEquals(ownerPlayer, null) && isHit && skillParam != null)
			{
				cachedCollider = base.gameObject.AddComponent<SphereCollider>();
				cachedCollider.radius = bulletData.data.radius;
				cachedCollider.isTrigger = true;
				cachedCollider.enabled = true;
				if (!string.IsNullOrEmpty(skillParam.tableData.attackInfoNames[0]))
				{
					atkInfo = FindAttackInfo(skillParam.tableData.attackInfoNames[0], true, false);
				}
				ownerPlayer.GetAtk(atkInfo as AttackHitInfo, ref exAtk);
			}
			isInitialized = true;
		}
	}

	private void Destroy()
	{
		OnDisappear(false);
	}

	private new void Update()
	{
		if (isInitialized)
		{
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
			if (lifeTime <= 0f)
			{
				OnDisappear(true);
			}
		}
	}

	public void OnDisappear(bool isExplode)
	{
		if (!object.ReferenceEquals(cachedEffectTransform, null) && !object.ReferenceEquals(cachedEffectTransform.gameObject, null))
		{
			EffectManager.ReleaseEffect(cachedEffectTransform.gameObject, true, false);
		}
		cachedEffectTransform = null;
		if (isExplode && !object.ReferenceEquals(atkInfo, null))
		{
			AtkAttribute atkAttribute = exAtk;
			SkillInfo.SkillParam exSkillParam = skillParam;
			AnimEventShot.Create(ownerPlayer, atkInfo, cachedTransform.position, Quaternion.identity, null, true, null, null, atkAttribute, Player.ATTACK_MODE.NONE, null, exSkillParam);
		}
		ownerPlayer = null;
		atkInfo = null;
		bulletData = null;
		if (!object.ReferenceEquals(base.gameObject, null))
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		if (isHitExplode && !(dontHitSec > 0f))
		{
			int layer = collider.gameObject.layer;
			if (((1 << layer) & ignoreLayerMask) <= 0 && !(hitInterval > 0f))
			{
				hitInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.decoyHitInterval;
				if (!((hitNum -= 1f) > 0f))
				{
					if (!object.ReferenceEquals(ownerPlayer, null))
					{
						ownerPlayer.ExecExplodeDecoyBullet(id);
					}
					OnDisappear(true);
				}
			}
		}
	}

	public void HateCtrl()
	{
		if (!object.ReferenceEquals(bulletData, null) && !object.ReferenceEquals(bulletData.dataDecoy, null))
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (!object.ReferenceEquals(boss, null) && (boss.IsOriginal() || boss.IsCoopNone()))
			{
				EnemyBrain component = MonoBehaviourSingleton<StageObjectManager>.I.boss.GetComponent<EnemyBrain>();
				if (!object.ReferenceEquals(component, null))
				{
					float num = 1f;
					if (skillParam != null)
					{
						num = (float)skillParam.supportValue[0] * 0.01f;
					}
					if (bulletData.dataDecoy.addDecoyHate.Length > 0)
					{
						int i = 0;
						for (int num2 = bulletData.dataDecoy.addDecoyHate.Length; i < num2; i++)
						{
							BulletData.BulletDecoy.HateInfo hateInfo = bulletData.dataDecoy.addDecoyHate[i];
							HateInfo hateInfo2 = new HateInfo();
							hateInfo2.target = this;
							hateInfo2.value = (int)((float)hateInfo.value * num * useRate);
							hateInfo2.type = hateInfo.type;
							component.HandleEvent(BRAIN_EVENT.DECOY, hateInfo2);
						}
					}
					if (!object.ReferenceEquals(ownerPlayer, null) && bulletData.dataDecoy.addOwnerHate.Length > 0)
					{
						int j = 0;
						for (int num3 = bulletData.dataDecoy.addOwnerHate.Length; j < num3; j++)
						{
							BulletData.BulletDecoy.HateInfo hateInfo3 = bulletData.dataDecoy.addOwnerHate[j];
							HateInfo hateInfo4 = new HateInfo();
							hateInfo4.target = ownerPlayer;
							hateInfo4.value = (int)((float)hateInfo3.value * num * useRate);
							hateInfo4.type = hateInfo3.type;
							component.HandleEvent(BRAIN_EVENT.DECOY, hateInfo4);
						}
					}
					useRate *= bulletData.dataDecoy.hateDecreaseRate;
					hateInterval += bulletData.dataDecoy.hateInterval;
				}
			}
		}
	}
}

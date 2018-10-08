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

	public override AttackInfo[] GetAttackInfos()
	{
		if (ownerPlayer == null)
		{
			return null;
		}
		return ownerPlayer.GetAttackInfos();
	}

	public void Initialize(int playerId, int decoyId, BulletData bullet, Vector3 position, SkillInfo.SkillParam skill, bool isHit)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
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
			cachedTransform = this.get_transform();
			cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
			cachedTransform.set_position(position);
			cachedTransform.set_localScale(Vector3.get_one());
			if (MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				cachedEffectTransform = EffectManager.GetEffect(bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
				cachedEffectTransform.set_position(cachedTransform.get_position() + bulletData.data.dispOffset);
				cachedEffectTransform.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
			}
			id = decoyId;
			this.get_gameObject().set_name(OBJ_NAME + decoyId);
			this.get_gameObject().set_layer(12);
			ignoreLayerMask |= -1073741824;
			if (!object.ReferenceEquals(ownerPlayer, null) && isHit)
			{
				cachedCollider = this.get_gameObject().AddComponent<SphereCollider>();
				cachedCollider.set_radius(bulletData.data.radius);
				cachedCollider.set_isTrigger(true);
				cachedCollider.set_enabled(true);
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
			dontHitSec -= Time.get_deltaTime();
			lifeTime -= Time.get_deltaTime();
			if (lifeTime <= 0f)
			{
				OnDisappear(true);
			}
		}
	}

	public void OnDisappear(bool isExplode)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (!object.ReferenceEquals(cachedEffectTransform, null) && !object.ReferenceEquals((object)cachedEffectTransform.get_gameObject(), null))
		{
			EffectManager.ReleaseEffect(cachedEffectTransform.get_gameObject(), true, false);
		}
		cachedEffectTransform = null;
		if (isExplode && !object.ReferenceEquals(atkInfo, null))
		{
			AtkAttribute atkAttribute = exAtk;
			SkillInfo.SkillParam exSkillParam = skillParam;
			AnimEventShot.Create(ownerPlayer, atkInfo, cachedTransform.get_position(), Quaternion.get_identity(), null, true, null, null, atkAttribute, Player.ATTACK_MODE.NONE, null, exSkillParam);
		}
		ownerPlayer = null;
		atkInfo = null;
		bulletData = null;
		if (!object.ReferenceEquals((object)this.get_gameObject(), null))
		{
			Object.Destroy(this.get_gameObject());
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (!(dontHitSec > 0f))
		{
			int layer = collider.get_gameObject().get_layer();
			if (((1 << layer) & ignoreLayerMask) <= 0)
			{
				if (!object.ReferenceEquals(ownerPlayer, null))
				{
					ownerPlayer.ExecExplodeDecoyBullet(id);
				}
				OnDisappear(true);
			}
		}
	}

	public void HateCtrl()
	{
		if (!object.ReferenceEquals(bulletData, null) && !object.ReferenceEquals(bulletData.dataDecoy, null) && !object.ReferenceEquals(MonoBehaviourSingleton<StageObjectManager>.I.boss, null))
		{
			EnemyBrain component = MonoBehaviourSingleton<StageObjectManager>.I.boss.GetComponent<EnemyBrain>();
			if (!object.ReferenceEquals(component, null))
			{
				float num = (float)skillParam.supportValue[0] * 0.01f;
				if (bulletData.dataDecoy.addDecoyHate.Length > 0)
				{
					int i = 0;
					for (int num2 = bulletData.dataDecoy.addDecoyHate.Length; i < num2; i++)
					{
						BulletData.BulletDecoy.HateInfo hateInfo = bulletData.dataDecoy.addDecoyHate[i];
						HateInfo hateInfo2 = new HateInfo();
						hateInfo2.target = this;
						hateInfo2.value = (int)((float)hateInfo.value * num);
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
						hateInfo4.value = (int)((float)hateInfo3.value * num);
						hateInfo4.type = hateInfo3.type;
						component.HandleEvent(BRAIN_EVENT.DECOY, hateInfo4);
					}
				}
			}
		}
	}
}

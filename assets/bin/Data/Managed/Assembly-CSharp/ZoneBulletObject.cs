using System.Collections.Generic;
using UnityEngine;

public class ZoneBulletObject : MonoBehaviour
{
	private readonly string OBJ_NAME = "ZoneBullet:";

	private readonly string HEALATK_NAME = "HealAttackObject";

	private Player ownerPlayer;

	private Player usePlayer;

	private BulletData bulletData;

	private SkillInfo.SkillParam skillParam;

	private Transform cachedPlayerTransform;

	private Transform cachedTransform;

	private Transform cachedEffectTransform;

	private SphereCollider cachedCollider;

	private int ignoreLayerMask;

	private int healValue;

	private float lifeTime;

	private float intervalTime;

	private bool isInitialized;

	private Vector3 tmpVector = default(Vector3);

	private Character.HealData healData;

	private Dictionary<int, float> validSecCollection = new Dictionary<int, float>();

	public ZoneBulletObject()
		: this()
	{
	}//IL_0019: Unknown result type (might be due to invalid IL or missing references)
	//IL_001f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0020: Unknown result type (might be due to invalid IL or missing references)


	public void Initialize(Player player, BulletData bullet, Vector3 position, SkillInfo.SkillParam skill, bool isHealDamgeEnemy, bool isOwner)
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize PresentBulletObject.");
			return;
		}
		ownerPlayer = ((!isOwner) ? null : player);
		usePlayer = player;
		bulletData = bullet;
		skillParam = skill;
		healValue = ((bulletData.dataZone.type == BulletData.BulletZone.TYPE.HEAL) ? skill.supportValue[0] : 0);
		lifeTime = skill.supportTime[0];
		intervalTime = bullet.dataZone.intervalTime;
		this.get_gameObject().set_name(OBJ_NAME + skill.tableData.id);
		cachedPlayerTransform = player.get_transform();
		cachedTransform = this.get_transform();
		cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
		cachedTransform.set_position(position);
		cachedTransform.set_localScale(Vector3.get_one());
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			cachedEffectTransform = EffectManager.GetEffect(bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
			if (!object.ReferenceEquals(cachedEffectTransform, null))
			{
				cachedEffectTransform.set_position(cachedTransform.get_position() + bulletData.data.dispOffset);
				cachedEffectTransform.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
			}
		}
		this.get_gameObject().set_layer(31);
		ignoreLayerMask |= 41984;
		ignoreLayerMask |= 20480;
		ignoreLayerMask |= 2490880;
		cachedCollider = this.get_gameObject().AddComponent<SphereCollider>();
		cachedCollider.set_radius(bulletData.data.radius);
		cachedCollider.set_isTrigger(true);
		cachedCollider.set_enabled(true);
		validSecCollection.Clear();
		if (isHealDamgeEnemy && healValue > 0)
		{
			GameObject val = new GameObject(HEALATK_NAME);
			HealAttackZoneObject healAttackZoneObject = val.AddComponent<HealAttackZoneObject>();
			healAttackZoneObject.Setup(ownerPlayer, cachedTransform, bullet, skill);
		}
		healData = new Character.HealData(healValue, bulletData.dataZone.healType, HEAL_EFFECT_TYPE.BASIS, new List<int>
		{
			10
		});
		isInitialized = true;
	}

	private void Destroy()
	{
		OnDisappear();
	}

	private void Update()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if (!isInitialized)
		{
			return;
		}
		if (bulletData.dataZone.isCarry)
		{
			if (usePlayer != null && !usePlayer.isDead)
			{
				tmpVector = cachedPlayerTransform.get_position();
				tmpVector.y = bulletData.data.dispOffset.y;
				cachedTransform.set_position(tmpVector);
				cachedEffectTransform.set_position(tmpVector);
			}
			else
			{
				lifeTime = 0f;
			}
		}
		lifeTime -= Time.get_deltaTime();
		if (lifeTime <= 0f)
		{
			OnDisappear();
		}
	}

	private void OnDisappear()
	{
		ownerPlayer = null;
		usePlayer = null;
		bulletData = null;
		skillParam = null;
		cachedPlayerTransform = null;
		cachedTransform = null;
		if (!object.ReferenceEquals(cachedCollider, null))
		{
			cachedCollider.set_enabled(false);
		}
		cachedCollider = null;
		if (cachedEffectTransform != null && cachedEffectTransform.get_gameObject() != null)
		{
			EffectManager.ReleaseEffect(cachedEffectTransform.get_gameObject());
		}
		cachedEffectTransform = null;
		if (!object.ReferenceEquals(this.get_gameObject(), null))
		{
			Object.Destroy(this.get_gameObject());
		}
		isInitialized = false;
	}

	private void OnTriggerEnter(Collider collider)
	{
		Player player = _GetValidPlayer(collider);
		if (!object.ReferenceEquals(player, null))
		{
			if (!validSecCollection.ContainsKey(player.id))
			{
				validSecCollection.Add(player.id, 0f);
			}
			else
			{
				validSecCollection[player.id] = 0f;
			}
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		Player player = _GetValidPlayer(collider);
		if (object.ReferenceEquals(player, null))
		{
			return;
		}
		if (!validSecCollection.ContainsKey(player.id))
		{
			validSecCollection.Add(player.id, 0f);
		}
		int id;
		Dictionary<int, float> dictionary;
		(dictionary = validSecCollection)[id = player.id] = dictionary[id] + Time.get_deltaTime();
		if (validSecCollection[player.id] >= intervalTime)
		{
			int id2;
			(dictionary = validSecCollection)[id2 = player.id] = dictionary[id2] - intervalTime;
			if (healValue > 0)
			{
				player.OnHealReceive(healData);
			}
			if (bulletData.dataZone.buffType != BuffParam.BUFFTYPE.NONE)
			{
				player.SetSelfBuff(skillParam.tableData.id, bulletData.dataZone.buffType, skillParam.supportValue[1], skillParam.supportTime[1], skillParam.skillIndex);
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		Player player = _GetValidPlayer(collider);
		if (!object.ReferenceEquals(player, null) && validSecCollection.ContainsKey(player.id))
		{
			validSecCollection.Remove(player.id);
		}
	}

	private Player _GetValidPlayer(Collider collider)
	{
		int layer = collider.get_gameObject().get_layer();
		if (((1 << layer) & ignoreLayerMask) > 0)
		{
			return null;
		}
		Player component = collider.get_gameObject().GetComponent<Player>();
		if (object.ReferenceEquals(component, null))
		{
			return null;
		}
		if (component.IsCoopNone() || component.IsOriginal())
		{
			return component;
		}
		if (!object.ReferenceEquals(ownerPlayer, null) && component.isNpc)
		{
			return component;
		}
		return null;
	}
}

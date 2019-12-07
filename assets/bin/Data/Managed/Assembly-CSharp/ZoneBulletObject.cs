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

	private Vector3 tmpVector;

	private Character.HealData healData;

	private Dictionary<int, float> validSecCollection = new Dictionary<int, float>();

	public void Initialize(Player player, BulletData bullet, Vector3 position, SkillInfo.SkillParam skill, bool isHealDamgeEnemy, bool isOwner)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize PresentBulletObject.");
			return;
		}
		ownerPlayer = (isOwner ? player : null);
		usePlayer = player;
		bulletData = bullet;
		skillParam = skill;
		healValue = ((bulletData.dataZone.type == BulletData.BulletZone.TYPE.HEAL) ? skill.supportValue[0] : 0);
		lifeTime = skill.supportTime[0];
		intervalTime = bullet.dataZone.intervalTime;
		base.gameObject.name = OBJ_NAME + skill.tableData.id;
		cachedPlayerTransform = player.transform;
		cachedTransform = base.transform;
		cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
		cachedTransform.position = position;
		cachedTransform.localScale = Vector3.one;
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			cachedEffectTransform = EffectManager.GetEffect(bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
			if ((object)cachedEffectTransform != null)
			{
				cachedEffectTransform.position = cachedTransform.position + bulletData.data.dispOffset;
				cachedEffectTransform.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
			}
		}
		base.gameObject.layer = 31;
		ignoreLayerMask |= 41984;
		ignoreLayerMask |= 20480;
		ignoreLayerMask |= 2490880;
		cachedCollider = base.gameObject.AddComponent<SphereCollider>();
		cachedCollider.radius = bulletData.data.radius;
		cachedCollider.isTrigger = true;
		cachedCollider.enabled = true;
		validSecCollection.Clear();
		if (isHealDamgeEnemy && healValue > 0)
		{
			new GameObject(HEALATK_NAME).AddComponent<HealAttackZoneObject>().Setup(ownerPlayer, cachedTransform, bullet, skill);
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
		if (!isInitialized)
		{
			return;
		}
		if (bulletData.dataZone.isCarry)
		{
			if (usePlayer != null && !usePlayer.isDead)
			{
				tmpVector = cachedPlayerTransform.position;
				tmpVector.y = bulletData.data.dispOffset.y;
				cachedTransform.position = tmpVector;
				cachedEffectTransform.position = tmpVector;
			}
			else
			{
				lifeTime = 0f;
			}
		}
		lifeTime -= Time.deltaTime;
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
		if ((object)cachedCollider != null)
		{
			cachedCollider.enabled = false;
		}
		cachedCollider = null;
		if (cachedEffectTransform != null && cachedEffectTransform.gameObject != null)
		{
			EffectManager.ReleaseEffect(cachedEffectTransform.gameObject);
		}
		cachedEffectTransform = null;
		if ((object)base.gameObject != null)
		{
			Object.Destroy(base.gameObject);
		}
		isInitialized = false;
	}

	private void OnTriggerEnter(Collider collider)
	{
		Player player = _GetValidPlayer(collider);
		if ((object)player != null)
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
		if ((object)player == null)
		{
			return;
		}
		if (!validSecCollection.ContainsKey(player.id))
		{
			validSecCollection.Add(player.id, 0f);
		}
		validSecCollection[player.id] += Time.deltaTime;
		if (validSecCollection[player.id] >= intervalTime)
		{
			validSecCollection[player.id] -= intervalTime;
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
		if ((object)player != null && validSecCollection.ContainsKey(player.id))
		{
			validSecCollection.Remove(player.id);
		}
	}

	private Player _GetValidPlayer(Collider collider)
	{
		int layer = collider.gameObject.layer;
		if (((1 << layer) & ignoreLayerMask) > 0)
		{
			return null;
		}
		Player component = collider.gameObject.GetComponent<Player>();
		if ((object)component == null)
		{
			return null;
		}
		if (component.IsCoopNone() || component.IsOriginal())
		{
			return component;
		}
		if ((object)ownerPlayer != null && component.isNpc)
		{
			return component;
		}
		return null;
	}
}

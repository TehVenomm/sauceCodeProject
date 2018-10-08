using System.Collections.Generic;
using UnityEngine;

public class BarrierBulletObject : StageObject
{
	private int hpMax;

	private int hp;

	private AtkAttribute def = new AtkAttribute();

	private AtkAttribute tol = new AtkAttribute();

	private BulletObject bulletObj;

	private EffectColorCtrl effectColorCtrl;

	private string effectNameInBarrier = string.Empty;

	private Player owner;

	private List<Player> playerList = new List<Player>();

	private bool isDead;

	public int GetOwnerId()
	{
		return owner.id;
	}

	public int GetHp()
	{
		return hp;
	}

	public int GetMaxHp()
	{
		return hpMax;
	}

	public float GetDefAndTolValue()
	{
		return def.normal;
	}

	public float GetTimeCount()
	{
		return bulletObj.timeCount;
	}

	public bool IsDead()
	{
		return isDead;
	}

	public string GetEffectNameInBarrier()
	{
		return effectNameInBarrier;
	}

	public void Initialize(BulletObject bulletObj)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().set_layer(31);
		this.bulletObj = bulletObj;
		owner = (bulletObj.stageObject as Player);
		if (owner != null)
		{
			int num = owner.id % 110000;
			int weaponIndex = owner.weaponIndex;
			int skillIndex = bulletObj.masterSkill.skillIndex;
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				num = 0;
			}
			if (QuestManager.IsValidTrial())
			{
				num = 0;
			}
			id = 1000000 + int.Parse(num.ToString() + weaponIndex.ToString() + skillIndex.ToString() + owner.bulletIndex.ToString("D3"));
		}
		base._rigidbody = this.GetComponent<Rigidbody>();
		base._rigidbody.set_useGravity(false);
		base._rigidbody.set_isKinematic(true);
		if (bulletObj != null && bulletObj.bulletEffect != null)
		{
			effectColorCtrl = bulletObj.bulletEffect.GetComponent<EffectColorCtrl>();
		}
		int base_value = bulletObj.bulletData.dataBarrier.baseHp;
		int num2 = bulletObj.bulletData.dataBarrier.baseDef;
		if (bulletObj.masterSkill != null)
		{
			GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(bulletObj.masterSkill.tableData.growID, bulletObj.masterSkill.baseInfo.level);
			if (growSkillItemData != null)
			{
				base_value = growSkillItemData.GetGrowResultSupportValue(base_value, 0);
				num2 = growSkillItemData.GetGrowResultSupportValue(num2, 1);
			}
		}
		hp = (hpMax = base_value);
		def.normal = (float)num2;
		tol.AddElementOnly((float)num2);
		if (owner.IsOriginal())
		{
			SetCoopMode(COOP_MODE_TYPE.ORIGINAL, 0);
			if (base.packetSender != null)
			{
				base.packetSender.OnSetCoopMode(COOP_MODE_TYPE.PUPPET);
			}
		}
		effectNameInBarrier = bulletObj.bulletData.dataBarrier.effectNameInBarrier;
		BarrierBulletObject activeBulletBarrierObject = owner.activeBulletBarrierObject;
		if (activeBulletBarrierObject != null)
		{
			owner.activeBulletBarrierObject.bulletObj.ForceBreak();
		}
		owner.activeBulletBarrierObject = this;
		base.isInitialized = true;
	}

	protected override void Update()
	{
		base.Update();
		if (effectColorCtrl != null)
		{
			effectColorCtrl.UpdateColor((float)hp / (float)hpMax);
		}
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (!(from_object is Enemy))
		{
			return false;
		}
		return true;
	}

	protected override void OnAttackedHitDirection(AttackedHitStatusDirection status)
	{
		base.OnAttackedHitDirection(status);
		if (status.hitParam.processor != null)
		{
			BulletObject bulletObject = status.hitParam.processor.colliderInterface as BulletObject;
			if (bulletObject != null)
			{
				status.atk = bulletObject.masterAtk;
				status.skillParam = bulletObject.masterSkill;
			}
			else
			{
				AtkAttribute atk = new AtkAttribute();
				status.fromObject.GetAtk(status.attackInfo, ref atk);
				status.atk = atk;
			}
		}
		if (status.fromType == OBJECT_TYPE.ENEMY)
		{
			status.validDamage = true;
			if (effectColorCtrl != null)
			{
				effectColorCtrl.PlayHitEffect();
			}
		}
	}

	protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
	{
		if (status.validDamage)
		{
			AtkAttribute damage_details = new AtkAttribute();
			status.damage = CalcDamage(status, ref damage_details);
			status.damageDetails = damage_details;
		}
	}

	public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		status.afterHP = hp;
		if (status.validDamage)
		{
			status.afterHP -= status.damage;
		}
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		hp = status.afterHP;
		if (bulletObj.IsEnable())
		{
			if (hp <= 0)
			{
				isDead = true;
				if (!playerList.IsNullOrEmpty())
				{
					int i = 0;
					for (int count = playerList.Count; i < count; i++)
					{
						if (!playerList[i].isDead)
						{
							playerList[i].MakeInvincibleByBrokenBarrier();
						}
					}
				}
				bulletObj.OnDestroy();
			}
			else if (!string.IsNullOrEmpty(status.attackInfo.remainEffectName))
			{
				Transform effect = EffectManager.GetEffect(status.attackInfo.remainEffectName, base._transform);
				effect.set_position(status.hitPos);
			}
		}
	}

	private int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Add(status.atk);
		atkAttribute.Mul(status.attackInfo.atkRate);
		int enemyLevel = 1;
		Enemy enemy = status.fromObject as Enemy;
		if (enemy != null)
		{
			enemyLevel = enemy.enemyLevel;
		}
		float levelRate = InGameUtility.CalcLevelRate(enemyLevel);
		damage_details.normal = (float)(int)(atkAttribute.normal * InGameUtility.CalcDamageRateToPlayer(levelRate, def.normal, 0f, -1, 1f));
		damage_details.fire = (float)(int)(atkAttribute.fire * InGameUtility.CalcDamageRateToPlayer(levelRate, def.fire, tol.fire, -1, 1f));
		damage_details.water = (float)(int)(atkAttribute.water * InGameUtility.CalcDamageRateToPlayer(levelRate, def.water, tol.water, -1, 1f));
		damage_details.thunder = (float)(int)(atkAttribute.thunder * InGameUtility.CalcDamageRateToPlayer(levelRate, def.thunder, tol.thunder, -1, 1f));
		damage_details.soil = (float)(int)(atkAttribute.soil * InGameUtility.CalcDamageRateToPlayer(levelRate, def.soil, tol.soil, -1, 1f));
		damage_details.light = (float)(int)(atkAttribute.light * InGameUtility.CalcDamageRateToPlayer(levelRate, def.light, tol.light, -1, 1f));
		damage_details.dark = (float)(int)(atkAttribute.dark * InGameUtility.CalcDamageRateToPlayer(levelRate, def.dark, tol.dark, -1, 1f));
		damage_details.CheckMinus();
		int num = Mathf.FloorToInt(damage_details.CalcTotal());
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	public void AddPlayer(Player player)
	{
		if (!playerList.Contains(player))
		{
			playerList.Add(player);
		}
	}

	public void RemovePlayer(Player player)
	{
		if (playerList.Contains(player))
		{
			playerList.Remove(player);
		}
	}

	public void ForceExitThroughProcessor(Collider exitCollider)
	{
		if (!(exitCollider == null) && !(bulletObj == null))
		{
			bulletObj.OnTriggerExit(exitCollider);
		}
	}

	public override void OnRecvSetCoopMode(Coop_Model_ObjectCoopInfo model, CoopPacket packet)
	{
		StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(model.id);
		if (!(stageObject == null))
		{
			stageObject.SetCoopMode(model.CoopModeType, packet.fromClientId);
		}
	}

	public override bool DestroyObject()
	{
		if (playerList.IsNullOrEmpty())
		{
			return base.DestroyObject();
		}
		int i = 0;
		for (int count = playerList.Count; i < count; i++)
		{
			playerList[i].ForceClearByBarrier(this);
		}
		return base.DestroyObject();
	}
}

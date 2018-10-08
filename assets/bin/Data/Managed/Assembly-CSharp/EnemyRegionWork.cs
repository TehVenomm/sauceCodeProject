using System.Collections.Generic;
using UnityEngine;

public class EnemyRegionWork
{
	public Enemy.RegionInfo regionInfo;

	public List<Enemy.BleedData> bleedList = new List<Enemy.BleedData>();

	public List<Enemy.BleedWork> bleedWorkList = new List<Enemy.BleedWork>();

	public List<int> weakAttackIDs = new List<int>();

	public Enemy.WEAK_STATE weakState;

	public string deleteAtkName = string.Empty;

	public XorInt hp = 10;

	public int weakSubParam = -1;

	public int parentRegionID = -1;

	public float breakTime = -1f;

	public float displayTimer;

	public bool isBroke;

	public bool enabled = true;

	public int NumOfAttackedByMagi;

	public int validElementType = -1;

	public int regionId;

	public bool isShieldDamage;

	public bool isShieldCriticalDamage;

	public Enemy.ShadowSealingData shadowSealingData = new Enemy.ShadowSealingData();

	public Transform shadowSealingEffect;

	public bool IsValidDisplayTimer => displayTimer > 0f;

	public void Initialize(Enemy.RegionInfo info, int parentRegionId, int _regionId)
	{
		regionInfo = info;
		hp = info.maxHP;
		isShieldDamage = info.isEnableShieldDamage;
		regionId = _regionId;
		if (parentRegionId > 0)
		{
			parentRegionID = parentRegionId;
			enabled = false;
		}
	}

	public void Update()
	{
		if (!(displayTimer <= 0f))
		{
			displayTimer -= Time.deltaTime;
			if (displayTimer <= 0f)
			{
				ResetWeakState();
			}
		}
	}

	public void ResetWeakState()
	{
		weakState = Enemy.WEAK_STATE.NONE;
		weakSubParam = -1;
		displayTimer = 0f;
		deleteAtkName = string.Empty;
		validElementType = -1;
	}

	public void SetupWeakPoint(int weakType, Player.ATTACK_MODE attackMode, float displayTime = 0f, string deleteAttackName = "", int validElement = -1)
	{
		displayTimer = displayTime;
		deleteAtkName = deleteAttackName;
		validElementType = validElement;
		switch (weakType)
		{
		case 0:
			weakState = Enemy.WEAK_STATE.WEAK;
			break;
		case 1:
			weakState = Enemy.WEAK_STATE.DOWN;
			break;
		case 2:
			weakState = Enemy.WEAK_STATE.WEAK_SP_ATTACK;
			break;
		case 3:
			weakState = Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX;
			break;
		case 5:
			weakState = Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK;
			break;
		case 6:
			weakState = Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK;
			break;
		case 7:
			weakState = Enemy.WEAK_STATE.WEAK_SKILL_ATTACK;
			break;
		case 8:
			weakState = Enemy.WEAK_STATE.WEAK_HEAL_ATTACK;
			break;
		case 9:
			weakState = Enemy.WEAK_STATE.WEAK_GRAB;
			break;
		case 10:
			weakState = Enemy.WEAK_STATE.WEAK_CANNON;
			break;
		case 11:
			weakState = Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK;
			break;
		}
		if (attackMode != 0)
		{
			weakSubParam = (int)attackMode;
		}
		if (Enemy.IsWeakStateSpAttack(weakState) && weakSubParam < 0)
		{
			ResetWeakState();
		}
		weakAttackIDs.Clear();
	}

	public void OnReviveRegion(int reviveRegionId)
	{
		if (parentRegionID >= 0 && parentRegionID == reviveRegionId)
		{
			enabled = false;
		}
	}

	public void OnBreakRegion(int breakRegionId)
	{
		if (parentRegionID >= 0 && parentRegionID == breakRegionId)
		{
			enabled = true;
		}
	}

	public float GetBarrierToleranceRate()
	{
		return regionInfo.barrierToleranceRate;
	}

	public void CopyFrom(EnemyRegionWork regionWork)
	{
		hp = regionWork.hp;
		breakTime = regionWork.breakTime;
		isBroke = regionWork.isBroke;
		isShieldCriticalDamage = regionWork.isShieldCriticalDamage;
		isShieldDamage = regionWork.isShieldDamage;
	}
}

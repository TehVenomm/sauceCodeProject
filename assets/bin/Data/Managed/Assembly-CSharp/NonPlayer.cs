using UnityEngine;

public class NonPlayer : Player
{
	public enum eNpcAllayState
	{
		CANNOT,
		SAME,
		ANOTHER,
		CAN
	}

	private float npcAtk;

	public int npcId
	{
		get;
		set;
	}

	public NPCTable.NPCData npcTableData
	{
		get;
		set;
	}

	public int lv
	{
		get;
		set;
	}

	public int lv_index
	{
		get;
		set;
	}

	protected override void Awake()
	{
		base.Awake();
	}

	public override void OnSetPlayerStatus(int _level, int _atk, int _def, int _hp, bool send_packet = true, StageObjectManager.PlayerTransferInfo transfer_info = null, bool usingRealAtk = false)
	{
		base.OnSetPlayerStatus(_level, _atk, _def, _hp, send_packet, transfer_info, false);
		if (!usingRealAtk)
		{
			npcAtk = (float)_atk;
			base.playerAtk = 0f;
		}
		else
		{
			float num2 = npcAtk = (base.playerAtk = (float)_atk);
		}
	}

	public override void InitParameter()
	{
		ResetStatusParam();
		base.attack.normal = npcAtk;
		base.defense.normal = base.playerDef;
		base.defense.fire = base.playerDef;
		base.defense.water = base.playerDef;
		base.defense.thunder = base.playerDef;
		base.defense.soil = base.playerDef;
		base.defense.light = base.playerDef;
		base.defense.dark = base.playerDef;
		base.defenseThreshold = 0;
		base.defenseCoefficient = new AtkAttribute();
		base.hpMax = base.playerHp;
		if (base.hp > base.hpMax)
		{
			int num2 = base.hp = (base.healHp = base.hpMax);
		}
		NpcLevelTable.NpcLevelData npcLevel = Singleton<NpcLevelTable>.I.GetNpcLevel((uint)lv, lv_index);
		if (npcLevel != null)
		{
			base.attack.fire = (float)npcLevel.atk_attribute[0];
			base.attack.water = (float)npcLevel.atk_attribute[1];
			base.attack.thunder = (float)npcLevel.atk_attribute[2];
			base.attack.soil = (float)npcLevel.atk_attribute[3];
			base.attack.light = (float)npcLevel.atk_attribute[4];
			base.attack.dark = (float)npcLevel.atk_attribute[5];
			base.tolerance.normal = 0f;
			base.tolerance.fire = (float)npcLevel.tolerance[0];
			base.tolerance.water = (float)npcLevel.tolerance[1];
			base.tolerance.thunder = (float)npcLevel.tolerance[2];
			base.tolerance.soil = (float)npcLevel.tolerance[3];
			base.tolerance.light = (float)npcLevel.tolerance[4];
			base.tolerance.dark = (float)npcLevel.tolerance[5];
		}
	}

	public eNpcAllayState CanGoPray(StageObject client)
	{
		if (base.isDead)
		{
			return eNpcAllayState.CANNOT;
		}
		if (!base.isNpc)
		{
			return eNpcAllayState.CANNOT;
		}
		if (object.ReferenceEquals(base.controller, null) || object.ReferenceEquals(base.controller.brain, null) || object.ReferenceEquals(base.controller.brain.targetCtrl, null))
		{
			return eNpcAllayState.CANNOT;
		}
		StageObject allyTarget = base.controller.brain.targetCtrl.GetAllyTarget();
		if (!object.ReferenceEquals(allyTarget, null))
		{
			if (allyTarget == client)
			{
				return eNpcAllayState.SAME;
			}
			return eNpcAllayState.ANOTHER;
		}
		return eNpcAllayState.CAN;
	}

	public bool NPCSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
	{
		bool flag = ActSkillAction(skill_index, false);
		SkillInfo.SkillParam actSkillParam = base.skillInfo.actSkillParam;
		if (actSkillParam == null)
		{
			return false;
		}
		if (flag)
		{
			SKILL_SLOT_TYPE type = actSkillParam.tableData.type;
			if (type == SKILL_SLOT_TYPE.ATTACK)
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true, 0f);
			}
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				Debug.Log((object)"NPCSkillAction OnUses");
				MonoBehaviourSingleton<InGameProgress>.I.OnSkillUse(base.skillInfo.actSkillParam);
			}
		}
		return flag;
	}

	public void ActPose(bool force_sync = false, bool recieve = false)
	{
		EndAction();
		base.actionID = (ACTION_ID)40;
		PlayMotion(40, -1f);
	}
}

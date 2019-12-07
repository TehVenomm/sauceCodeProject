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
		base.OnSetPlayerStatus(_level, _atk, _def, _hp, send_packet, transfer_info);
		if (!usingRealAtk)
		{
			npcAtk = _atk;
			base.playerAtk = 0f;
		}
		else
		{
			float num2 = npcAtk = (base.playerAtk = _atk);
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
			base.attack.fire = npcLevel.atk_attribute[0];
			base.attack.water = npcLevel.atk_attribute[1];
			base.attack.thunder = npcLevel.atk_attribute[2];
			base.attack.soil = npcLevel.atk_attribute[3];
			base.attack.light = npcLevel.atk_attribute[4];
			base.attack.dark = npcLevel.atk_attribute[5];
			base.tolerance.normal = 0f;
			base.tolerance.fire = npcLevel.tolerance[0];
			base.tolerance.water = npcLevel.tolerance[1];
			base.tolerance.thunder = npcLevel.tolerance[2];
			base.tolerance.soil = npcLevel.tolerance[3];
			base.tolerance.light = npcLevel.tolerance[4];
			base.tolerance.dark = npcLevel.tolerance[5];
		}
	}

	public eNpcAllayState CanGoPray(StageObject client)
	{
		if (base.isDead)
		{
			return eNpcAllayState.CANNOT;
		}
		if (IsStone())
		{
			return eNpcAllayState.CANNOT;
		}
		if (!base.isNpc)
		{
			return eNpcAllayState.CANNOT;
		}
		if ((object)base.controller == null || (object)base.controller.brain == null || base.controller.brain.targetCtrl == null)
		{
			return eNpcAllayState.CANNOT;
		}
		StageObject allyTarget = base.controller.brain.targetCtrl.GetAllyTarget();
		if ((object)allyTarget != null)
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
		bool flag = ActSkillAction(skill_index);
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
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(isLock: true);
			}
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				Debug.Log("NPCSkillAction OnUses");
				MonoBehaviourSingleton<InGameProgress>.I.OnSkillUse(base.skillInfo.actSkillParam);
			}
		}
		return flag;
	}

	public void ActPose(bool force_sync = false, bool recieve = false)
	{
		EndAction();
		base.actionID = (ACTION_ID)48;
		PlayMotion(48);
	}

	protected override string GetMotionStateName(int motion_id, string _layerName = "")
	{
		if (motion_id - 115 >= 0 && motion_id - 115 < Player.subMotionStateName.Length)
		{
			Character.stateNameBuilder.Length = 0;
			Character.stateNameBuilder.Append((_layerName == "") ? "Base Layer." : _layerName);
			Character.stateNameBuilder.Append(Player.subMotionStateName[motion_id - 115]);
			return Character.stateNameBuilder.ToString();
		}
		if (motion_id < 115)
		{
			return _GetMotionStateName(motion_id, _layerName);
		}
		return null;
	}

	private static string _GetMotionStateName(int motion_id, string _layerName)
	{
		string value = string.IsNullOrEmpty(_layerName) ? "Base Layer." : _layerName;
		if (motion_id < 115)
		{
			Character.stateNameBuilder.Length = 0;
			Character.stateNameBuilder.Append(value);
			switch (motion_id)
			{
			case 48:
				Character.stateNameBuilder.Append(Character.poseStateName);
				break;
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 43:
			case 44:
			case 45:
			case 46:
			case 47:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 58:
			case 59:
			case 60:
			case 61:
			case 62:
			case 63:
			case 64:
			case 65:
			case 66:
			case 67:
			case 68:
			case 69:
			case 70:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 91:
			case 92:
			case 93:
			case 94:
			case 95:
			case 96:
			case 97:
			case 98:
			case 99:
			case 100:
			case 101:
			case 102:
			case 103:
			case 104:
			case 105:
			case 106:
			case 107:
			case 108:
			case 109:
			case 110:
			case 111:
			case 112:
			case 113:
			case 114:
				Character.stateNameBuilder.AppendFormat(Character.motionStateName[15], motion_id - 15);
				break;
			default:
				Character.stateNameBuilder.Append(Character.motionStateName[motion_id]);
				break;
			}
			return Character.stateNameBuilder.ToString();
		}
		return null;
	}
}

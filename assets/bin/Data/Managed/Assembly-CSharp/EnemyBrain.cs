using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : Brain
{
	protected Enemy enemy;

	public EnemyActionController actionCtrl;

	protected override void Awake()
	{
		base.Awake();
		enemy = (base.owner as Enemy);
	}

	protected override void OnInitialize()
	{
		base.OnInitialize();
		if (enemy.brainParam != null)
		{
			param = enemy.brainParam;
			FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, enemy.enemyPopIndex);
			if (enemyPopData != null && enemyPopData.autoActivate)
			{
				param.scoutParam = enemyPopData.scoutingParam;
			}
		}
		base.opponentMemSpanTimer.PauseOn();
		base.targetUpdateSpanTimer.PauseOn();
		if (enemy.isBoss)
		{
			base.opponentMem.SetHateParam(enemy.enemyTableData.personality);
		}
		else
		{
			base.opponentMem.SetHateParam(HateParam.GetDefault());
		}
		base.fsm = new StateMachine(this);
		if (enemy.enemyTableData.active)
		{
			base.fsm.ChangeState(STATE_TYPE.ACTIVE);
		}
		else
		{
			base.fsm.ChangeState(STATE_TYPE.NONACTIVE);
		}
		if (QuestManager.IsValidInGameWaveMatch(false))
		{
			SetNearWaveMatchTarget();
		}
		actionCtrl = new EnemyActionController(this);
		actionCtrl.LoadTable();
	}

	protected override void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			base.OnDestroy();
		}
	}

	public override float GetScale()
	{
		return (enemy.enemyTableData == null) ? 1f : enemy.enemyTableData.modelScale;
	}

	public override Transform GetFront()
	{
		return enemy.head;
	}

	public override Transform GetBack()
	{
		return enemy.hip;
	}

	public override List<StageObject> GetTargetObjectList()
	{
		List<StageObject> list = new List<StageObject>();
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			list.AddRange(MonoBehaviourSingleton<StageObjectManager>.I.playerList);
			list.AddRange(MonoBehaviourSingleton<StageObjectManager>.I.decoyList);
		}
		if (QuestManager.IsValidInGameWaveMatch(false))
		{
			list.AddRange(MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList);
		}
		return list;
	}

	public override List<StageObject> GetAllyObjectList()
	{
		return (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
	}

	private void SetNearWaveMatchTarget()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (base.fsm != null && base.targetCtrl != null)
		{
			StageObject stageObject = null;
			float num = 3.40282347E+38f;
			for (int i = 0; i < MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; i++)
			{
				FieldWaveTargetObject fieldWaveTargetObject = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[i] as FieldWaveTargetObject;
				if (!(fieldWaveTargetObject == null) && !fieldWaveTargetObject.isDead)
				{
					Vector3 val = fieldWaveTargetObject._position - enemy._position;
					float sqrMagnitude = val.get_sqrMagnitude();
					if (sqrMagnitude < num)
					{
						num = sqrMagnitude;
						stageObject = fieldWaveTargetObject;
					}
				}
			}
			if (!(stageObject == null))
			{
				base.fsm.ChangeState(STATE_TYPE.SELECT);
				base.targetCtrl.SetCurrentTarget(stageObject);
				HandleEvent(BRAIN_EVENT.WAVE_TARGET, stageObject);
			}
		}
	}

	public override void HandleEvent(BRAIN_EVENT ev, object param = null)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		switch (ev)
		{
		case BRAIN_EVENT.END_ACTION:
			if (base.opponentMem != null)
			{
				base.opponentMem.Update();
			}
			break;
		case BRAIN_EVENT.OWN_ATTACK_HIT:
			if (base.opponentMem != null)
			{
				OpponentMemory.OpponentRecord opponentRecord = base.opponentMem.Find(param as StageObject);
				if (opponentRecord != null)
				{
					opponentRecord.record.isDamaged = true;
				}
			}
			break;
		case BRAIN_EVENT.ATTACKED_HIT:
		{
			AttackedHitStatusOwner attackedHitStatusOwner2 = (AttackedHitStatusOwner)param;
			if (base.opponentMem != null)
			{
				OpponentMemory opponentMem2 = base.opponentMem;
				Vector3 val2 = attackedHitStatusOwner2.fromPos - attackedHitStatusOwner2.hitPos;
				DISTANCE distance2 = opponentMem2.GetDistance(val2.get_sqrMagnitude());
				int num = (int)((float)attackedHitStatusOwner2.damage * base.opponentMem.hateParam.distanceAttackRatio[(int)distance2]);
				if (isNPC(attackedHitStatusOwner2.fromObject))
				{
					num = (int)((float)num * 0.5f);
				}
				base.opponentMem.AddHate(attackedHitStatusOwner2.fromObject, num, Hate.TYPE.Damage);
			}
			break;
		}
		case BRAIN_EVENT.ATTACKED_WEAK_POINT:
		{
			AttackedHitStatusOwner attackedHitStatusOwner = (AttackedHitStatusOwner)param;
			if (base.opponentMem != null)
			{
				int attackedWeakPointHate = base.opponentMem.hateParam.attackedWeakPointHate;
				OpponentMemory opponentMem = base.opponentMem;
				Vector3 val = attackedHitStatusOwner.fromPos - attackedHitStatusOwner.hitPos;
				DISTANCE distance = opponentMem.GetDistance(val.get_sqrMagnitude());
				attackedWeakPointHate = (int)((float)attackedWeakPointHate * base.opponentMem.hateParam.distanceAttackRatio[(int)distance]);
				if (isNPC(attackedHitStatusOwner.fromObject))
				{
					attackedWeakPointHate = (int)((float)attackedWeakPointHate * 0.5f);
				}
				base.opponentMem.AddHate(attackedHitStatusOwner.fromObject, attackedWeakPointHate, Hate.TYPE.SpecialDamage);
			}
			break;
		}
		case BRAIN_EVENT.PLAYER_HEAL:
			if (base.opponentMem != null)
			{
				Player.HateInfo hateInfo2 = param as Player.HateInfo;
				base.opponentMem.AddHate(hateInfo2.target, hateInfo2.val, Hate.TYPE.Heal);
			}
			break;
		case BRAIN_EVENT.PLAYER_SKILL:
			if (base.opponentMem != null)
			{
				base.opponentMem.AddHate(param as StageObject, base.opponentMem.hateParam.skillHate, Hate.TYPE.Skill);
			}
			break;
		case BRAIN_EVENT.REVIVE_REGION:
			if (actionCtrl != null)
			{
				actionCtrl.OnReviveRegion((int)param);
			}
			break;
		case BRAIN_EVENT.DECOY:
			if (base.opponentMem != null)
			{
				DecoyBulletObject.HateInfo hateInfo = param as DecoyBulletObject.HateInfo;
				base.opponentMem.AddHate(hateInfo.target, hateInfo.value, hateInfo.type);
			}
			break;
		case BRAIN_EVENT.WAVE_TARGET:
			if (base.opponentMem != null)
			{
				base.opponentMem.AddHate(param as StageObject, 1000, Hate.TYPE.Damage);
			}
			break;
		}
		base.HandleEvent(ev, param);
	}

	private bool isNPC(StageObject obj)
	{
		Player player = obj as Player;
		return player != null && player.isNpc;
	}
}

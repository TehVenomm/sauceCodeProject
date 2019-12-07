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

	protected override void Update()
	{
		base.Update();
		if (QuestManager.IsValidInGameWaveStrategy() && (enemy.actionTarget == null || enemy.actionTarget is Player))
		{
			SetNearWaveMatchTarget();
		}
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
		if (QuestManager.IsValidInGameWaveMatch())
		{
			SetNearWaveMatchTarget();
		}
		if (QuestManager.IsValidInGameWaveStrategy())
		{
			base.opponentMem.SetHateParam(null);
			SetNearDecoyTarget();
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
		if (enemy.enemyTableData == null)
		{
			return 1f;
		}
		return enemy.enemyTableData.modelScale;
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
		if (QuestManager.IsValidInGameWaveMatch())
		{
			list.AddRange(MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList);
		}
		return list;
	}

	public override List<StageObject> GetAllyObjectList()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return new List<StageObject>();
		}
		return MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
	}

	private void SetNearWaveMatchTarget()
	{
		if (base.fsm == null || base.targetCtrl == null)
		{
			return;
		}
		StageObject stageObject = null;
		float num = float.MaxValue;
		for (int i = 0; i < MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; i++)
		{
			FieldWaveTargetObject fieldWaveTargetObject = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[i] as FieldWaveTargetObject;
			if (!(fieldWaveTargetObject == null) && !fieldWaveTargetObject.isDead)
			{
				float sqrMagnitude = (fieldWaveTargetObject._position - enemy._position).sqrMagnitude;
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

	public void SetNearDecoyTarget()
	{
		if (base.targetCtrl != null)
		{
			StageObject nearestDecoyObject = AIUtility.GetNearestDecoyObject(base.owner._position);
			if (!(nearestDecoyObject == null))
			{
				base.fsm.ChangeState(STATE_TYPE.SELECT);
				base.targetCtrl.SetCurrentTarget(nearestDecoyObject);
			}
		}
	}

	public void MissDecoyTarget(StageObject decoyObj)
	{
		if (base.targetCtrl != null && !(decoyObj == null))
		{
			DecoyBulletObject decoyBulletObject = base.targetCtrl.GetCurrentTarget() as DecoyBulletObject;
			if (!(decoyBulletObject == null) && !decoyBulletObject.IsActive())
			{
				base.fsm.ChangeState(STATE_TYPE.SELECT);
				base.targetCtrl.MissCurrentTarget();
			}
		}
	}

	public override void HandleEvent(BRAIN_EVENT ev, object param = null)
	{
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
			if (base.opponentMem != null && base.opponentMem.haveHateControl)
			{
				DISTANCE distance2 = base.opponentMem.GetDistance((attackedHitStatusOwner2.fromPos - attackedHitStatusOwner2.hitPos).sqrMagnitude);
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
			if (base.opponentMem != null && base.opponentMem.haveHateControl)
			{
				int attackedWeakPointHate = base.opponentMem.hateParam.attackedWeakPointHate;
				DISTANCE distance = base.opponentMem.GetDistance((attackedHitStatusOwner.fromPos - attackedHitStatusOwner.hitPos).sqrMagnitude);
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
		if (player != null)
		{
			return player.isNpc;
		}
		return false;
	}
}

using System.Collections;
using UnityEngine;

public class EnemyController : ControllerBase
{
	protected IEnumerator mainCoroutine;

	protected bool isStart;

	protected float startWaitTime;

	private float turnUpTimer;

	private Enemy enemy
	{
		get;
		set;
	}

	private EnemyBrain enemyBrain
	{
		get;
		set;
	}

	public InGameSettingsManager.Enemy enemyParameter
	{
		get;
		private set;
	}

	public InGameSettingsManager.EnemyController parameter
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		enemy = (character as Enemy);
		enemyBrain = AttachBrain<EnemyBrain>();
		enemyParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy;
	}

	protected override void Start()
	{
		base.Start();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.enemyController;
		isStart = true;
		if (IsEnableControll())
		{
			OnChangeEnableControll(enable: true);
		}
	}

	public override void SetEnableControll(bool enable, DISABLE_FLAG flag = DISABLE_FLAG.DEFAULT)
	{
		base.SetEnableControll(enable, flag);
	}

	public override void OnChangeEnableControll(bool enable)
	{
		base.OnChangeEnableControll(enable);
		if (enable)
		{
			if (isStart && base.enabled && enemy != null && mainCoroutine == null)
			{
				startWaitTime = parameter.startWaitTime;
				mainCoroutine = AIMain();
				StartCoroutine(mainCoroutine);
			}
		}
		else if (mainCoroutine != null)
		{
			StopAllCoroutines();
			mainCoroutine = null;
		}
	}

	public override void OnActReaction()
	{
		base.OnActReaction();
		if (IsEnableControll() && mainCoroutine != null)
		{
			StopAllCoroutines();
			startWaitTime = parameter.afterReactionWaitTime;
			mainCoroutine = AIMain();
			StartCoroutine(mainCoroutine);
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByEnemy();
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetPositionByEnemy();
		}
	}

	public void OnSetDecoy()
	{
		if (enemy.actionID == Character.ACTION_ID.MOVE || enemy.actionID == Character.ACTION_ID.ROTATE)
		{
			enemy.ActIdle();
		}
		enemyBrain.SetNearDecoyTarget();
	}

	public void OnCheckMissDecoy(StageObject decoyObj)
	{
		enemyBrain.MissDecoyTarget(decoyObj);
	}

	public void Reset()
	{
		base.brain.ResetInitialized();
		StopAllCoroutines();
		mainCoroutine = null;
	}

	public override void OnCharacterInitialized()
	{
		base.OnCharacterInitialized();
	}

	protected override void Update()
	{
		base.Update();
		if (!enemy.isHiding || (!enemy.IsOriginal() && !enemy.IsCoopNone()))
		{
			return;
		}
		if ((double)turnUpTimer >= 0.5)
		{
			if (base.brain.opponentMem != null)
			{
				base.brain.opponentMem.Update();
			}
			StageObject targetObjectOfNearest = base.brain.targetCtrl.GetTargetObjectOfNearest();
			if (targetObjectOfNearest != null)
			{
				Vector2 a = targetObjectOfNearest._position.ToVector2XZ();
				Vector2 b = enemy._position.ToVector2XZ();
				if (Vector2.Distance(a, b) <= enemy.turnUpDistance)
				{
					enemy.TurnUp();
				}
			}
			turnUpTimer = 0f;
		}
		turnUpTimer += Time.deltaTime;
	}

	private IEnumerator AIMain()
	{
		while (base.brain == null || !base.brain.isInitialized)
		{
			yield return 0;
		}
		while (!character.isControllable)
		{
			yield return 0;
		}
		if (startWaitTime > 0f)
		{
			yield return new WaitForSeconds(startWaitTime);
		}
		base.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION);
		while (base.enabled)
		{
			while (character.IsMirror())
			{
				yield return new WaitForSeconds(1f);
			}
			while (enemy.isHiding || enemy.IsHideMotionPlaying())
			{
				yield return null;
			}
			yield return StartCoroutine(OnAction());
			yield return 0;
		}
		mainCoroutine = null;
	}

	public IEnumerator OnAction()
	{
		bool is_action = false;
		if (base.brain.moveCtrl.IsRotate())
		{
			is_action = true;
			if (base.brain.isNonActive)
			{
				yield return StartCoroutine(OnRotate());
			}
			else
			{
				yield return StartCoroutine(OnRotateOfAction());
			}
			while (character.actionID == Character.ACTION_ID.ROTATE)
			{
				yield return 0;
			}
		}
		if (base.brain.moveCtrl.IsSeek())
		{
			is_action = true;
			if (base.brain.isNonActive)
			{
				yield return StartCoroutine(OnMove());
			}
			else
			{
				yield return StartCoroutine(OnMoveOfAction());
			}
			while (character.actionID == Character.ACTION_ID.MOVE)
			{
				yield return 0;
			}
		}
		if (base.brain.weaponCtrl.IsAttack())
		{
			is_action = true;
			yield return StartCoroutine(OnAttackOfAction());
		}
		if (is_action)
		{
			base.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION);
		}
	}

	public IEnumerator OnRotateOfAction()
	{
		if (enemyBrain.actionCtrl.nowAction.data.isRotate)
		{
			yield return StartCoroutine(OnRotateToTarget());
		}
	}

	public IEnumerator OnMoveOfAction()
	{
		EnemyActionController.ActionInfo nowAction = enemyBrain.actionCtrl.nowAction;
		if (nowAction.data.isMove && !(base.brain.targetCtrl.GetDistance() < (float)nowAction.data.atkRange))
		{
			if (base.brain.param.moveParam.enableActionMoveHoming)
			{
				yield return StartCoroutine(OnMoveHoming());
			}
			else
			{
				yield return StartCoroutine(OnMoveToTarget());
			}
		}
	}

	private IEnumerator OnAttackOfAction()
	{
		EnemyActionTable.EnemyActionData nowActData = enemyBrain.actionCtrl.nowAction.data;
		if (nowActData.lotteryWaitInterval > 0f)
		{
			nowActData.lotteryWaitTime = Time.time;
		}
		int j = 0;
		int i = nowActData.combiActionTypeInfos.Length;
		while (j < i)
		{
			EnemyActionTable.ActionTypeInfo actionTypeInfo = nowActData.combiActionTypeInfos[j];
			switch (actionTypeInfo.type)
			{
			case EnemyActionTable.ACTION_TYPE.STEP:
				yield return StartCoroutine(OnStep());
				break;
			case EnemyActionTable.ACTION_TYPE.STEP_BACK:
				yield return StartCoroutine(OnStep(is_back: true));
				break;
			case EnemyActionTable.ACTION_TYPE.ROTATE:
				yield return StartCoroutine(OnRotateToTarget());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE:
				yield return StartCoroutine(OnMoveToTarget());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_HOMING:
				yield return StartCoroutine(OnMoveHoming());
				break;
			case EnemyActionTable.ACTION_TYPE.ATTACK:
				yield return StartCoroutine(OnAtk(actionTypeInfo));
				break;
			case EnemyActionTable.ACTION_TYPE.ANGRY:
				yield return StartCoroutine(OnAngry(actionTypeInfo, nowActData.angryId));
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_SIDE:
				yield return StartCoroutine(OnMoveSideways());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_POINT:
				yield return StartCoroutine(OnMovePoint());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_LOOKAT:
				yield return StartCoroutine(OnMoveLookAt());
				break;
			}
			int num = j + 1;
			j = num;
		}
		while (!character.IsChangeableAction(Character.ACTION_ID.NONE))
		{
			yield return 0;
		}
		float num2 = nowActData.afterWaitTime;
		if (enemyParameter != null)
		{
			num2 += enemyParameter.baseAfterWaitTime;
			num2 += GetAfterWaitTimeByLv(enemy.enemyLevel);
		}
		if (enemy.packetSender != null)
		{
			num2 = enemy.packetSender.GetWaitTime(num2);
		}
		if (num2 > 0f)
		{
			yield return new WaitForSeconds(num2);
		}
	}

	private float GetAfterWaitTimeByLv(int lv)
	{
		if (enemyParameter == null)
		{
			return 0f;
		}
		if (enemyParameter.afterWaitTimeThresholdsByLv.IsNullOrEmpty())
		{
			return 0f;
		}
		if (enemyParameter.afterWaitTimesByLv.IsNullOrEmpty())
		{
			return 0f;
		}
		if (enemyParameter.afterWaitTimeThresholdsByLv.Length >= enemyParameter.afterWaitTimesByLv.Length)
		{
			return 0f;
		}
		int num = enemyParameter.afterWaitTimesByLv.Length - 1;
		int i = 0;
		for (int num2 = enemyParameter.afterWaitTimeThresholdsByLv.Length; i < num2; i++)
		{
			if ((float)lv <= enemyParameter.afterWaitTimeThresholdsByLv[i])
			{
				num = i;
				break;
			}
		}
		return enemyParameter.afterWaitTimesByLv[num];
	}

	public IEnumerator OnStep(bool is_back = false)
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MAX))
		{
			yield return 0;
		}
		Enemy.SUB_MOTION_ID motion_id = Enemy.SUB_MOTION_ID.STEP;
		if (is_back)
		{
			motion_id = Enemy.SUB_MOTION_ID.STEP_BACK;
		}
		enemy.ActStep((int)motion_id);
	}

	public IEnumerator OnAtk(EnemyActionTable.ActionTypeInfo actionTypeInfo)
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.ATTACK))
		{
			yield return 0;
		}
		character.ActAttack(actionTypeInfo.id);
	}

	public IEnumerator OnAngry(EnemyActionTable.ActionTypeInfo actionTypeInfo, uint angryId)
	{
		while (!character.IsChangeableAction((Character.ACTION_ID)15))
		{
			yield return 0;
		}
		enemy.ActAngry(actionTypeInfo.id, angryId);
	}

	public IEnumerator OnRotateToTarget()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			yield return 0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		if (!base.brain.targetCtrl.IsPlaceTarget(PLACE.FRONT))
		{
			if (base.brain.param.moveParam.motionRotate)
			{
				character.ActRotateMotionToTarget();
			}
			else
			{
				character.ActRotateToTarget();
			}
		}
	}

	public IEnumerator OnMoveToTarget()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		float distance = base.brain.targetCtrl.GetDistance();
		if (!(distance < character.moveStopRange))
		{
			float num = distance + base.brain.param.moveParam.moveOverDistance;
			if (num >= base.brain.param.moveParam.moveMaxLength)
			{
				num = base.brain.param.moveParam.moveMaxLength;
			}
			float length = 0f;
			if (enemyBrain.actionCtrl.GetMoveMaxLength(ref length))
			{
				num = length;
			}
			character.ActMoveToTarget(num);
		}
	}

	public IEnumerator OnRotate()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			yield return 0;
		}
		Vector3 vector = base.brain.moveCtrl.targetPos - character._position;
		vector.y = 0f;
		if (!(vector == Vector3.zero))
		{
			float y = Quaternion.LookRotation(vector).eulerAngles.y;
			character.ActRotateToDirection(y);
		}
	}

	public IEnumerator OnMove()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		Vector3 b = base.brain.moveCtrl.targetPos - character._position;
		b.y = 0f;
		float moveMaxLength = base.brain.param.moveParam.moveMaxLength;
		if (moveMaxLength > 0f)
		{
			float magnitude = b.magnitude;
			if (magnitude > moveMaxLength)
			{
				b *= moveMaxLength / magnitude;
			}
		}
		character.ActMoveToPosition(character._position + b);
	}

	public IEnumerator OnMoveHoming()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		if (!(base.brain.targetCtrl.GetDistance() < character.moveStopRange))
		{
			character.ActMoveHoming(base.brain.param.moveParam.moveHomingMaxLength);
		}
	}

	public IEnumerator OnMoveSideways()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		character.ActMoveSideways();
	}

	public IEnumerator OnMovePoint()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		enemy.ActMovePoint(enemy.movePointPos);
	}

	public IEnumerator OnMoveLookAt()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		enemy.ActMoveLookAt(enemy.movePointPos);
	}

	public void OnHitAttack(StageObject target)
	{
		base.brain.HandleEvent(BRAIN_EVENT.OWN_ATTACK_HIT, target);
	}

	public void OnReviveRegion(int regionId)
	{
		base.brain.HandleEvent(BRAIN_EVENT.REVIVE_REGION, regionId);
	}
}

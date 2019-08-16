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
			if (isStart && this.get_enabled() && enemy != null && mainCoroutine == null)
			{
				startWaitTime = parameter.startWaitTime;
				mainCoroutine = AIMain();
				this.StartCoroutine(mainCoroutine);
			}
		}
		else if (mainCoroutine != null)
		{
			this.StopAllCoroutines();
			mainCoroutine = null;
		}
	}

	public override void OnActReaction()
	{
		base.OnActReaction();
		if (IsEnableControll() && mainCoroutine != null)
		{
			this.StopAllCoroutines();
			startWaitTime = parameter.afterReactionWaitTime;
			mainCoroutine = AIMain();
			this.StartCoroutine(mainCoroutine);
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
		this.StopAllCoroutines();
		mainCoroutine = null;
	}

	public override void OnCharacterInitialized()
	{
		base.OnCharacterInitialized();
	}

	protected override void Update()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
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
				Vector2 val = targetObjectOfNearest._position.ToVector2XZ();
				Vector2 val2 = enemy._position.ToVector2XZ();
				float num = Vector2.Distance(val, val2);
				if (num <= enemy.turnUpDistance)
				{
					enemy.TurnUp();
				}
			}
			turnUpTimer = 0f;
		}
		turnUpTimer += Time.get_deltaTime();
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
			yield return (object)new WaitForSeconds(startWaitTime);
		}
		base.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION);
		while (this.get_enabled())
		{
			while (character.IsMirror())
			{
				yield return (object)new WaitForSeconds(1f);
			}
			while (enemy.isHiding || enemy.IsHideMotionPlaying())
			{
				yield return null;
			}
			yield return this.StartCoroutine(OnAction());
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
				yield return this.StartCoroutine(OnRotate());
			}
			else
			{
				yield return this.StartCoroutine(OnRotateOfAction());
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
				yield return this.StartCoroutine(OnMove());
			}
			else
			{
				yield return this.StartCoroutine(OnMoveOfAction());
			}
			while (character.actionID == Character.ACTION_ID.MOVE)
			{
				yield return 0;
			}
		}
		if (base.brain.weaponCtrl.IsAttack())
		{
			is_action = true;
			yield return this.StartCoroutine(OnAttackOfAction());
		}
		if (is_action)
		{
			base.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION);
		}
	}

	public IEnumerator OnRotateOfAction()
	{
		EnemyActionController.ActionInfo actInfo = enemyBrain.actionCtrl.nowAction;
		if (actInfo.data.isRotate)
		{
			yield return this.StartCoroutine(OnRotateToTarget());
		}
	}

	public IEnumerator OnMoveOfAction()
	{
		EnemyActionController.ActionInfo actInfo = enemyBrain.actionCtrl.nowAction;
		if (actInfo.data.isMove && !(base.brain.targetCtrl.GetDistance() < (float)actInfo.data.atkRange))
		{
			if (base.brain.param.moveParam.enableActionMoveHoming)
			{
				yield return this.StartCoroutine(OnMoveHoming());
			}
			else
			{
				yield return this.StartCoroutine(OnMoveToTarget());
			}
		}
	}

	private IEnumerator OnAttackOfAction()
	{
		EnemyActionTable.EnemyActionData nowActData = enemyBrain.actionCtrl.nowAction.data;
		if (nowActData.lotteryWaitInterval > 0f)
		{
			nowActData.lotteryWaitTime = Time.get_time();
		}
		int j = 0;
		for (int i = nowActData.combiActionTypeInfos.Length; j < i; j++)
		{
			EnemyActionTable.ActionTypeInfo actionTypeInfo = nowActData.combiActionTypeInfos[j];
			switch (actionTypeInfo.type)
			{
			case EnemyActionTable.ACTION_TYPE.STEP:
				yield return this.StartCoroutine(OnStep());
				break;
			case EnemyActionTable.ACTION_TYPE.STEP_BACK:
				yield return this.StartCoroutine(OnStep(is_back: true));
				break;
			case EnemyActionTable.ACTION_TYPE.ROTATE:
				yield return this.StartCoroutine(OnRotateToTarget());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE:
				yield return this.StartCoroutine(OnMoveToTarget());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_HOMING:
				yield return this.StartCoroutine(OnMoveHoming());
				break;
			case EnemyActionTable.ACTION_TYPE.ATTACK:
				yield return this.StartCoroutine(OnAtk(actionTypeInfo));
				break;
			case EnemyActionTable.ACTION_TYPE.ANGRY:
				yield return this.StartCoroutine(OnAngry(actionTypeInfo, nowActData.angryId));
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_SIDE:
				yield return this.StartCoroutine(OnMoveSideways());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_POINT:
				yield return this.StartCoroutine(OnMovePoint());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_LOOKAT:
				yield return this.StartCoroutine(OnMoveLookAt());
				break;
			}
		}
		while (!character.IsChangeableAction(Character.ACTION_ID.NONE))
		{
			yield return 0;
		}
		float time2 = nowActData.afterWaitTime;
		if (enemyParameter != null)
		{
			time2 += enemyParameter.baseAfterWaitTime;
			time2 += GetAfterWaitTimeByLv(enemy.enemyLevel);
		}
		if (enemy.packetSender != null)
		{
			time2 = enemy.packetSender.GetWaitTime(time2);
		}
		if (time2 > 0f)
		{
			yield return (object)new WaitForSeconds(time2);
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
		character.ActAttack(actionTypeInfo.id, send_packet: true, sync_immediately: false, string.Empty, string.Empty);
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
			float len = distance + base.brain.param.moveParam.moveOverDistance;
			if (len >= base.brain.param.moveParam.moveMaxLength)
			{
				len = base.brain.param.moveParam.moveMaxLength;
			}
			float alteredLength = 0f;
			if (enemyBrain.actionCtrl.GetMoveMaxLength(ref alteredLength))
			{
				len = alteredLength;
			}
			character.ActMoveToTarget(len);
		}
	}

	public IEnumerator OnRotate()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			yield return 0;
		}
		Vector3 vec_target = base.brain.moveCtrl.targetPos - character._position;
		vec_target.y = 0f;
		if (!(vec_target == Vector3.get_zero()))
		{
			Quaternion val = Quaternion.LookRotation(vec_target);
			Vector3 eulerAngles = val.get_eulerAngles();
			float direction = eulerAngles.y;
			character.ActRotateToDirection(direction);
		}
	}

	public IEnumerator OnMove()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return 0;
		}
		Vector3 vec_target = base.brain.moveCtrl.targetPos - character._position;
		vec_target.y = 0f;
		float max_length = base.brain.param.moveParam.moveMaxLength;
		if (max_length > 0f)
		{
			float magnitude = vec_target.get_magnitude();
			if (magnitude > max_length)
			{
				vec_target *= max_length / magnitude;
			}
		}
		character.ActMoveToPosition(character._position + vec_target);
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
		float distance = base.brain.targetCtrl.GetDistance();
		if (!(distance < character.moveStopRange))
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

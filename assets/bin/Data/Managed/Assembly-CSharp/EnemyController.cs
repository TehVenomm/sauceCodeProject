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
			OnChangeEnableControll(true);
		}
	}

	public override void SetEnableControll(bool enable, DISABLE_FLAG flag = DISABLE_FLAG.DEFAULT)
	{
		base.SetEnableControll(enable, flag);
	}

	public override void OnChangeEnableControll(bool enable)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
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
		}
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
		if (enemy.isHiding && (enemy.IsOriginal() || enemy.IsCoopNone()))
		{
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
	}

	private IEnumerator AIMain()
	{
		while (base.brain == null || !base.brain.isInitialized)
		{
			yield return (object)0;
		}
		while (!character.isControllable)
		{
			yield return (object)0;
		}
		if (startWaitTime > 0f)
		{
			yield return (object)new WaitForSeconds(startWaitTime);
		}
		base.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION, null);
		while (this.get_enabled())
		{
			while (character.IsMirror())
			{
				yield return (object)new WaitForSeconds(1f);
			}
			while (enemy.isHiding || enemy.IsHideMotionPlaying())
			{
				yield return (object)null;
			}
			yield return (object)this.StartCoroutine(OnAction());
			yield return (object)0;
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
				yield return (object)this.StartCoroutine(OnRotate());
			}
			else
			{
				yield return (object)this.StartCoroutine(OnRotateOfAction());
			}
			while (character.actionID == Character.ACTION_ID.ROTATE)
			{
				yield return (object)0;
			}
		}
		if (base.brain.moveCtrl.IsSeek())
		{
			is_action = true;
			if (base.brain.isNonActive)
			{
				yield return (object)this.StartCoroutine(OnMove());
			}
			else
			{
				yield return (object)this.StartCoroutine(OnMoveOfAction());
			}
			while (character.actionID == Character.ACTION_ID.MOVE)
			{
				yield return (object)0;
			}
		}
		if (base.brain.weaponCtrl.IsAttack())
		{
			is_action = true;
			yield return (object)this.StartCoroutine(OnAttackOfAction());
		}
		if (is_action)
		{
			base.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION, null);
		}
	}

	public IEnumerator OnRotateOfAction()
	{
		EnemyActionController.ActionInfo actInfo = enemyBrain.actionCtrl.nowAction;
		if (actInfo.data.isRotate)
		{
			yield return (object)this.StartCoroutine(OnRotateToTarget());
		}
	}

	public IEnumerator OnMoveOfAction()
	{
		EnemyActionController.ActionInfo actInfo = enemyBrain.actionCtrl.nowAction;
		if (actInfo.data.isMove && !(base.brain.targetCtrl.GetDistance() < (float)actInfo.data.atkRange))
		{
			if (base.brain.param.moveParam.enableActionMoveHoming)
			{
				yield return (object)this.StartCoroutine(OnMoveHoming());
			}
			else
			{
				yield return (object)this.StartCoroutine(OnMoveToTarget());
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
				yield return (object)this.StartCoroutine(OnStep(false));
				break;
			case EnemyActionTable.ACTION_TYPE.STEP_BACK:
				yield return (object)this.StartCoroutine(OnStep(true));
				break;
			case EnemyActionTable.ACTION_TYPE.ROTATE:
				yield return (object)this.StartCoroutine(OnRotateToTarget());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE:
				yield return (object)this.StartCoroutine(OnMoveToTarget());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_HOMING:
				yield return (object)this.StartCoroutine(OnMoveHoming());
				break;
			case EnemyActionTable.ACTION_TYPE.ATTACK:
				yield return (object)this.StartCoroutine(OnAtk(actionTypeInfo));
				break;
			case EnemyActionTable.ACTION_TYPE.ANGRY:
				yield return (object)this.StartCoroutine(OnAngry(actionTypeInfo, nowActData.angryId));
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_SIDE:
				yield return (object)this.StartCoroutine(OnMoveSideways());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_POINT:
				yield return (object)this.StartCoroutine(OnMovePoint());
				break;
			case EnemyActionTable.ACTION_TYPE.MOVE_LOOKAT:
				yield return (object)this.StartCoroutine(OnMoveLookAt());
				break;
			}
		}
		while (!character.IsChangeableAction(Character.ACTION_ID.NONE))
		{
			yield return (object)0;
		}
		float time = nowActData.afterWaitTime;
		if (enemyParameter != null)
		{
			time += enemyParameter.baseAfterWaitTime;
		}
		if (enemy.packetSender != null)
		{
			time = enemy.packetSender.GetWaitTime(time);
		}
		if (time > 0f)
		{
			yield return (object)new WaitForSeconds(time);
		}
	}

	public IEnumerator OnStep(bool is_back = false)
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MAX))
		{
			yield return (object)0;
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
			yield return (object)0;
		}
		character.ActAttack(actionTypeInfo.id, true, false);
	}

	public IEnumerator OnAngry(EnemyActionTable.ActionTypeInfo actionTypeInfo, uint angryId)
	{
		while (!character.IsChangeableAction((Character.ACTION_ID)14))
		{
			yield return (object)0;
		}
		enemy.ActAngry(actionTypeInfo.id, angryId);
	}

	public IEnumerator OnRotateToTarget()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			yield return (object)0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		if (!base.brain.targetCtrl.IsPlaceTarget(PLACE.FRONT))
		{
			if (base.brain.param.moveParam.motionRotate)
			{
				character.ActRotateMotionToTarget(false);
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
			yield return (object)0;
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
			character.ActMoveToTarget(len, false);
		}
	}

	public IEnumerator OnRotate()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			yield return (object)0;
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
			yield return (object)0;
		}
		Vector3 vec_target = base.brain.moveCtrl.targetPos - character._position;
		vec_target.y = 0f;
		float max_length = base.brain.param.moveParam.moveMaxLength;
		if (max_length > 0f)
		{
			float length = vec_target.get_magnitude();
			if (length > max_length)
			{
				vec_target *= max_length / length;
			}
		}
		character.ActMoveToPosition(character._position + vec_target, false);
	}

	public IEnumerator OnMoveHoming()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return (object)0;
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
			yield return (object)0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		character.ActMoveSideways(0, false);
	}

	public IEnumerator OnMovePoint()
	{
		while (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			yield return (object)0;
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
			yield return (object)0;
		}
		if (!character.isControllable)
		{
			base.brain.opponentMem.Update();
		}
		enemy.ActMoveLookAt(enemy.movePointPos, false);
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

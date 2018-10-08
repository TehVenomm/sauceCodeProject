using UnityEngine;

public class SnatchController
{
	public enum STATE
	{
		NONE,
		SHOT,
		SHOT_RELEASE,
		SNATCH,
		MOVE,
		MOVE_LOOP
	}

	private int[] attackIds = new int[5]
	{
		0,
		14,
		15,
		16,
		17
	};

	private static readonly int HASH_ANIMATOR_MOVE_LOOP = Animator.StringToHash("attack_94_atk_pose");

	private InGameSettingsManager.Player.OneHandSwordActionInfo ohsInfo;

	private bool isCtrlActive;

	private Player owner;

	private Enemy target;

	private Transform snatchBulletTrans;

	private Transform snatchTrans;

	private Transform handTrans;

	private SnatchLineRenderer renderer;

	private bool isReached;

	private bool isHit;

	private bool isShotReleased;

	private bool isLoopStart;

	private float animStateTimer;

	private float animStateTimeLimit;

	private float animStateTimerForMoveLoop;

	private float animStateTimeLimitForMoveLoop;

	public STATE state
	{
		get;
		private set;
	}

	public void Init(Player player, SnatchLineRenderer lineRenderer)
	{
		ohsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
		owner = player;
		renderer = lineRenderer;
		animStateTimeLimit = ohsInfo.Soul_AnimStateTimeLimit;
		animStateTimeLimitForMoveLoop = ohsInfo.Soul_AnimStateTimeLimitForMoveLoop;
		SetState(STATE.NONE);
	}

	public void OnLoadComplete()
	{
		isCtrlActive = true;
	}

	public void Update()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		if (isCtrlActive)
		{
			animStateTimer += Time.get_deltaTime();
			if (snatchTrans != null)
			{
				snatchTrans.set_rotation(owner._rotation);
			}
			if (handTrans != null)
			{
				renderer.SetPositonStart(handTrans.get_position());
			}
			if (snatchTrans != null)
			{
				renderer.SetPositionEnd(snatchTrans.get_position());
			}
			else if (snatchBulletTrans != null)
			{
				renderer.SetPositionEnd(snatchBulletTrans.get_position());
			}
			switch (state)
			{
			case STATE.NONE:
				if (owner != null && owner.animator != null)
				{
					AnimatorStateInfo currentAnimatorStateInfo = owner.animator.GetCurrentAnimatorStateInfo(0);
					int shortNameHash = currentAnimatorStateInfo.get_shortNameHash();
					if (shortNameHash == HASH_ANIMATOR_MOVE_LOOP)
					{
						animStateTimerForMoveLoop += Time.get_deltaTime();
						if (animStateTimerForMoveLoop > animStateTimeLimitForMoveLoop)
						{
							owner.SetNextTrigger(0);
							animStateTimerForMoveLoop = 0f;
						}
					}
				}
				break;
			case STATE.SHOT:
			case STATE.SHOT_RELEASE:
				if (IsReached())
				{
					owner.OnSnatchMoveEnd(1);
				}
				else if (IsAnimStateTimeLimit())
				{
					owner.OnSnatchMoveEnd(1);
				}
				break;
			case STATE.SNATCH:
				if (target != null && target.isDead)
				{
					owner.SetNextTrigger(1);
					Cancel();
				}
				break;
			case STATE.MOVE:
				owner.CheckSnatchMove();
				break;
			case STATE.MOVE_LOOP:
				if (owner.IsArrivalPosition(GetSnatchPos(), 0f))
				{
					owner.OnSnatchMoveEnd(0);
					animStateTimerForMoveLoop = 0f;
				}
				else if (owner.IsCoopNone() || owner.IsOriginal())
				{
					animStateTimerForMoveLoop += Time.get_deltaTime();
					if (animStateTimerForMoveLoop > animStateTimeLimitForMoveLoop)
					{
						owner.OnSnatchMoveEnd(0);
						animStateTimerForMoveLoop = 0f;
					}
				}
				break;
			}
		}
	}

	public void Cancel()
	{
		OnArrive();
	}

	public void OnShot()
	{
		SetState(STATE.SHOT);
	}

	public void OnHit(int enemyId, Vector3 hitPoint)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		target = (MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId) as Enemy);
		if (FieldManager.IsValidInGameNoBoss() && !owner.IsCoopNone() && !owner.IsOriginal())
		{
			target = null;
		}
		if (!isHit)
		{
			isHit = true;
			if (MonoBehaviourSingleton<SoundManager>.IsValid())
			{
				SoundManager.PlayOneShotSE(ohsInfo.Soul_SnatchHitSeId, hitPoint);
			}
			string effect_name = (!owner.isBoostMode) ? ohsInfo.Soul_SnatchHitEffect : ohsInfo.Soul_SnatchHitEffectOnBoostMode;
			EffectManager.OneShot(effect_name, hitPoint, Quaternion.get_identity(), false);
			snatchTrans = EffectManager.GetEffect(ohsInfo.Soul_SnatchHitRemainEffect, null);
			renderer.SetPositionEnd(snatchTrans.get_position());
			Vector3 val = Vector3.get_zero();
			Vector3 val2 = owner._position - hitPoint;
			float magnitude = val2.get_magnitude();
			if (magnitude > ohsInfo.Soul_MoveStopRange)
			{
				Vector3 val3 = owner._position - hitPoint;
				val = val3.get_normalized() * ohsInfo.Soul_MoveStopRange;
			}
			snatchTrans.set_position(hitPoint + val);
			snatchTrans.set_rotation(owner._rotation);
			if (target != null)
			{
				snatchTrans.set_parent(target._transform);
				target.stackBuffCtrl.IncrementStackCount(StackBuffController.STACK_TYPE.SNATCH);
			}
			switch (state)
			{
			case STATE.SHOT_RELEASE:
				if (target != null)
				{
					target.stackBuffCtrl.DecrementStackCount(StackBuffController.STACK_TYPE.SNATCH);
				}
				isShotReleased = true;
				SetState(STATE.MOVE);
				break;
			default:
				owner.SetNextTrigger(0);
				SetState(STATE.SNATCH);
				break;
			case STATE.MOVE:
				break;
			}
			if (owner.playerSender != null)
			{
				owner.playerSender.OnSnatch(enemyId, hitPoint);
			}
		}
	}

	public void OnRelease()
	{
		switch (state)
		{
		case STATE.SHOT_RELEASE:
			break;
		case STATE.SHOT:
			SetState(STATE.SHOT_RELEASE);
			break;
		case STATE.SNATCH:
			SetState(STATE.MOVE);
			break;
		}
	}

	public void OnArrive()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		if (snatchTrans != null)
		{
			EffectManager.ReleaseEffect(snatchTrans.get_gameObject(), true, false);
			snatchTrans = null;
		}
		if (target != null)
		{
			target.stackBuffCtrl.DecrementStackCount(StackBuffController.STACK_TYPE.SNATCH);
		}
		if (renderer != null)
		{
			renderer.SetInvisible();
		}
		target = null;
		isReached = false;
		isShotReleased = false;
		isHit = false;
		isLoopStart = false;
		animStateTimer = 0f;
		SetState(STATE.NONE);
	}

	public void OnReach()
	{
		isReached = true;
	}

	private void SetState(STATE state)
	{
		this.state = state;
	}

	public void StartMoveLoop()
	{
		SetState(STATE.MOVE_LOOP);
	}

	public void SetSnatchBulletTrans(Transform trans)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		snatchBulletTrans = trans;
		handTrans = owner.FindNode("L_Hand");
		renderer.SetPositonStart(handTrans.get_position());
		renderer.SetPositionEnd(trans.get_position());
		renderer.SetVisible();
	}

	public void ActivateLoopStart()
	{
		isLoopStart = true;
	}

	public Vector3 GetSnatchPos()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (snatchTrans == null)
		{
			return Vector3.get_zero();
		}
		Vector3 position = snatchTrans.get_position();
		position.y = 0f;
		return position;
	}

	public int GetAttackId(SelfController.FLICK_DIRECTION direction)
	{
		return attackIds[(int)direction];
	}

	public bool IsSnatching()
	{
		return state == STATE.SNATCH;
	}

	public bool IsMove()
	{
		return state == STATE.MOVE;
	}

	public bool IsMoveLoop()
	{
		return state == STATE.MOVE_LOOP;
	}

	public bool IsMoveLoopStart()
	{
		return isLoopStart;
	}

	public bool IsReached()
	{
		return isReached;
	}

	public bool IsShotReleased()
	{
		return isShotReleased;
	}

	public bool IsAnimStateTimeLimit()
	{
		return animStateTimer > animStateTimeLimit;
	}
}

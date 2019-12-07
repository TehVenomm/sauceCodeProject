using System;
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
		if (!isCtrlActive)
		{
			return;
		}
		animStateTimer += Time.deltaTime;
		if (snatchTrans != null)
		{
			snatchTrans.rotation = owner._rotation;
		}
		if (handTrans != null)
		{
			renderer.SetPositonStart(handTrans.position);
		}
		if (snatchTrans != null)
		{
			renderer.SetPositionEnd(snatchTrans.position);
		}
		else if (snatchBulletTrans != null)
		{
			renderer.SetPositionEnd(snatchBulletTrans.position);
		}
		switch (state)
		{
		case STATE.NONE:
			if (owner != null && owner.animator != null && owner.animator.GetCurrentAnimatorStateInfo(0).shortNameHash == HASH_ANIMATOR_MOVE_LOOP)
			{
				animStateTimerForMoveLoop += Time.deltaTime;
				if (animStateTimerForMoveLoop > animStateTimeLimitForMoveLoop)
				{
					owner.SetNextTrigger();
					animStateTimerForMoveLoop = 0f;
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
			if (owner.IsArrivalPosition(GetSnatchPos()))
			{
				owner.OnSnatchMoveEnd();
				animStateTimerForMoveLoop = 0f;
			}
			else if (owner.IsCoopNone() || owner.IsOriginal())
			{
				animStateTimerForMoveLoop += Time.deltaTime;
				if (animStateTimerForMoveLoop > animStateTimeLimitForMoveLoop)
				{
					owner.OnSnatchMoveEnd();
					animStateTimerForMoveLoop = 0f;
				}
			}
			break;
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
		target = (MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId) as Enemy);
		if (FieldManager.IsValidInGameNoBoss() && !owner.IsCoopNone() && !owner.IsOriginal())
		{
			target = null;
		}
		if (isHit)
		{
			return;
		}
		isHit = true;
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.PlayOneShotSE(ohsInfo.Soul_SnatchHitSeId, hitPoint);
		}
		EffectManager.OneShot(owner.isBoostMode ? ohsInfo.Soul_SnatchHitEffectOnBoostMode : ohsInfo.Soul_SnatchHitEffect, hitPoint, Quaternion.identity);
		snatchTrans = EffectManager.GetEffect(ohsInfo.Soul_SnatchHitRemainEffect);
		renderer.SetPositionEnd(snatchTrans.position);
		Vector3 b = Vector3.zero;
		if ((owner._position - hitPoint).magnitude > ohsInfo.Soul_MoveStopRange)
		{
			b = (owner._position - hitPoint).normalized * ohsInfo.Soul_MoveStopRange;
		}
		snatchTrans.position = hitPoint + b;
		snatchTrans.rotation = owner._rotation;
		if (target != null)
		{
			snatchTrans.parent = target._transform;
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
			owner.SetNextTrigger();
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

	public void OnRelease()
	{
		switch (state)
		{
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
		if (snatchTrans != null)
		{
			EffectManager.ReleaseEffect(snatchTrans.gameObject);
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
		snatchBulletTrans = trans;
		handTrans = owner.FindNode("L_Hand");
		renderer.SetPositonStart(handTrans.position);
		renderer.SetPositionEnd(trans.position);
		renderer.SetVisible();
	}

	public void ActivateLoopStart()
	{
		isLoopStart = true;
	}

	public Vector3 GetSnatchPos()
	{
		if (snatchTrans == null)
		{
			return Vector3.zero;
		}
		Vector3 position = snatchTrans.position;
		position.y = 0f;
		return position;
	}

	public bool GetSnatchPos(out Vector3 pos)
	{
		pos = Vector3.zero;
		bool result = false;
		if (isCtrlActive && snatchTrans != null)
		{
			pos = snatchTrans.position;
			pos.y = 0f;
			result = true;
		}
		return result;
	}

	public int GetAttackId(SelfController.FLICK_DIRECTION direction)
	{
		return attackIds[(int)direction];
	}

	public bool IsFlickedAttack(int attackID)
	{
		return Array.IndexOf(attackIds, attackID) > 0;
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

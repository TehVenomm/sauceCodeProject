using System.Collections;
using UnityEngine;

public class NpcController : ControllerBase
{
	protected IEnumerator mainCoroutine;

	protected bool isStart;

	protected float startWaitTime;

	private Player player
	{
		get;
		set;
	}

	public NpcBrain npcBrain
	{
		get;
		private set;
	}

	public InGameSettingsManager.NpcController parameter
	{
		get;
		private set;
	}

	public InGameSettingsManager.SelfController selfParameter
	{
		get;
		private set;
	}

	private bool isAttack => (Object)player != (Object)null && player.actionID == Character.ACTION_ID.ATTACK;

	private bool isGuard => (Object)player != (Object)null && player.actionID == (Character.ACTION_ID)18;

	private bool isMove => (Object)player != (Object)null && player.actionID == Character.ACTION_ID.MOVE;

	private bool isChangeableAttack => (Object)player != (Object)null && player.IsChangeableAction(Character.ACTION_ID.ATTACK);

	private bool isChangeableSpecialAction => (Object)player != (Object)null && player.IsChangeableAction((Character.ACTION_ID)32);

	private StageObject target => (!((Object)base.brain != (Object)null)) ? null : base.brain.targetCtrl.GetCurrentTarget();

	protected override void Awake()
	{
		base.Awake();
		player = (character as Player);
		npcBrain = AttachBrain<NpcBrain>();
	}

	protected override void Start()
	{
		base.Start();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.npcController;
		selfParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController;
		isStart = true;
		if (IsEnableControll())
		{
			OnChangeEnableControll(true);
		}
	}

	protected override void Update()
	{
		base.Update();
		if (IsEnableControll())
		{
			OnDead();
		}
	}

	public override void OnChangeEnableControll(bool enable)
	{
		if (enable && !CoopStageObjectUtility.CanControll(player))
		{
			Log.Error(LOG.INGAME, "NpcController:OnChangeEnableControll. field block enable. obj={0}", player);
			enable = false;
		}
		base.OnChangeEnableControll(enable);
		if (enable)
		{
			if (isStart && base.enabled && (Object)player != (Object)null && mainCoroutine == null)
			{
				startWaitTime = parameter.startWaitTime;
				mainCoroutine = AIMain();
				StartCoroutine(mainCoroutine);
			}
		}
		else
		{
			if (mainCoroutine != null)
			{
				StopAllCoroutines();
				mainCoroutine = null;
			}
			if (isGuard)
			{
				player.ActIdle(false, -1f);
			}
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
	}

	private IEnumerator AIMain()
	{
		while ((Object)base.brain == (Object)null || !base.brain.isInitialized)
		{
			yield return (object)0;
		}
		while (!player.isControllable)
		{
			yield return (object)0;
		}
		if (startWaitTime > 0f)
		{
			yield return (object)new WaitForSeconds(startWaitTime);
		}
		while (base.enabled)
		{
			while (player.IsMirror())
			{
				yield return (object)new WaitForSeconds(1f);
			}
			OnMove();
			OnWeapon();
			float time = 0f;
			if ((Object)player.packetSender != (Object)null)
			{
				time = player.packetSender.GetWaitTime(0f);
			}
			if (time > 0f)
			{
				yield return (object)new WaitForSeconds(time);
			}
			else
			{
				yield return (object)0;
			}
		}
		mainCoroutine = null;
	}

	private void OnDead()
	{
		if (!player.isControllable && player.actionID == (Character.ACTION_ID)23 && !player.IsAutoReviving() && player.rescueTime <= 0f && player.deadStartTime >= 0f && !player.isProgressStop() && !(player is Self))
		{
			player.DestroyObject();
		}
	}

	private void OnMove()
	{
		bool flag = false;
		if (base.brain.moveCtrl.IsAvoid())
		{
			if (!player.IsChangeableAction(Character.ACTION_ID.MAX))
			{
				return;
			}
			OnAvoid(base.brain.moveCtrl.avoidPlace);
			flag = true;
		}
		else if (base.brain.moveCtrl.IsSeek())
		{
			if (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
			{
				return;
			}
			OnMoveStick(base.brain.moveCtrl.stickVec, base.brain.moveCtrl.targetPos);
			flag = true;
		}
		else if (base.brain.moveCtrl.IsStop())
		{
			flag = false;
		}
		if (!flag && isMove)
		{
			character.ActIdle(false, -1f);
		}
	}

	private void OnMoveStick(Vector2 stick_vec, Vector3 target_pos)
	{
		Vector3 position = base.transform.position;
		Vector3 vector = target_pos - position;
		vector.y = 0f;
		vector.Normalize();
		Vector3 a = Quaternion.Euler(0f, 90f, 0f) * vector;
		Vector3 a2 = vector;
		Vector3 vector2 = a * stick_vec.x * selfParameter.moveSideSpeed + a2 * stick_vec.y * selfParameter.moveForwardSpeed;
		character.ActMoveVelocity(vector2, selfParameter.moveForwardSpeed, Character.MOTION_ID.WALK);
		character.SetLerpRotation(vector2);
	}

	private void OnAvoid(PLACE avoid_place)
	{
		if (!((Object)target == (Object)null))
		{
			Vector3 forward = target._transform.position - character._transform.position;
			forward.y = 0f;
			Quaternion rotation = Quaternion.LookRotation(forward);
			Vector3 b = Vector3.zero;
			switch (avoid_place)
			{
			case PLACE.FRONT:
				b = rotation * Vector3.forward;
				break;
			case PLACE.BACK:
				b = rotation * Vector3.back;
				break;
			case PLACE.LEFT:
				b = rotation * Vector3.left;
				break;
			case PLACE.RIGHT:
				b = rotation * Vector3.right;
				break;
			}
			character.LookAt(character._transform.position + b, false);
			player.ActAvoid();
		}
	}

	private void OnWeapon()
	{
		if (base.brain.weaponCtrl.IsAttack())
		{
			if (player.attackMode == Player.ATTACK_MODE.ARROW)
			{
				OnArrowAttack();
			}
			else
			{
				OnAttack();
			}
		}
		else if (base.brain.weaponCtrl.IsSpecial())
		{
			if (player.attackMode == Player.ATTACK_MODE.ARROW)
			{
				OnArrowAttack();
			}
			else
			{
				OnSpecialAttack();
			}
		}
		else if (base.brain.weaponCtrl.IsGuard())
		{
			OnGuard();
		}
		else
		{
			if (player.actionID == (Character.ACTION_ID)18)
			{
				player.ActIdle(true, -1f);
			}
			if (player.enableInputCharge)
			{
				player.SetEnableTap(false);
			}
		}
		if (base.brain.weaponCtrl.changeIndex >= 0)
		{
			OnChangeWeapon();
		}
	}

	private void OnAttack()
	{
		if (player.enableTap)
		{
			player.SetEnableTap(false);
		}
		if (isAttack && player.enableInputCombo)
		{
			if (base.brain.weaponCtrl.beforeAttackId == 0 || base.brain.weaponCtrl.beforeAttackId != player.attackID)
			{
				base.brain.weaponCtrl.ComboOn();
				base.brain.weaponCtrl.SetBeforeAttackId(player.attackID);
				player.InputAttackCombo();
			}
		}
		else if (isChangeableAttack)
		{
			character.ActAttack(0, true, false, string.Empty);
			base.brain.weaponCtrl.ComboOff();
			base.brain.weaponCtrl.SetBeforeAttackId(0);
		}
	}

	private void OnSpecialAttack()
	{
		if (!player.isActSpecialAction && isChangeableSpecialAction)
		{
			player.SetEnableTap(true);
			player.ActSpecialAction(true, true);
			base.brain.weaponCtrl.SetChargeRate(1f);
		}
		else if (player.enableInputCharge && player.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
			player.SetEnableTap(false);
		}
	}

	private void OnArrowAttack()
	{
		if (player.isControllable)
		{
			player.SetEnableTap(true);
			player.ActAttack(0, true, false, string.Empty);
			if (base.brain.weaponCtrl.IsSpecial())
			{
				base.brain.weaponCtrl.SetChargeRate(1f);
			}
			else
			{
				base.brain.weaponCtrl.SetChargeRate(Random.value);
			}
		}
		else if (player.enableInputCharge && player.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
			player.SetEnableTap(false);
		}
	}

	private void OnGuard()
	{
		if (player.isGuardAttackMode && !player.isActSpecialAction && isChangeableSpecialAction)
		{
			player.ActSpecialAction(true, true);
		}
	}

	private void OnChangeWeapon()
	{
		int changeIndex = base.brain.weaponCtrl.changeIndex;
		if (changeIndex >= 0 && player.equipWeaponList.Count > changeIndex && changeIndex != player.weaponIndex && player.equipWeaponList[changeIndex] != null)
		{
			player.ActChangeWeapon(player.equipWeaponList[changeIndex], changeIndex);
			base.brain.weaponCtrl.ResetChangeIndex();
		}
	}

	private void LateUpdate()
	{
		UpdateRegionTarget();
	}

	private void UpdateRegionTarget()
	{
		if (!((Object)player == (Object)null))
		{
			player.targetingPointList.Clear();
			if (!((Object)player.actionTarget == (Object)null))
			{
				Enemy enemy = player.actionTarget as Enemy;
				if (!((Object)enemy == (Object)null) && !enemy.isDead && enemy.enableTargetPoint)
				{
					TargetPoint[] targetPoints = enemy.targetPoints;
					if (targetPoints != null && targetPoints.Length != 0)
					{
						TargetPoint targetPoint = null;
						float num = 3.40282347E+38f;
						Vector3 position = player._transform.position;
						Vector2 b = position.ToVector2XZ();
						player.forwardXZ.Normalize();
						int i = 0;
						for (int num2 = targetPoints.Length; i < num2; i++)
						{
							TargetPoint targetPoint2 = targetPoints[i];
							if (targetPoint2.gameObject.activeInHierarchy)
							{
								Vector3 targetPoint3 = targetPoint2.GetTargetPoint();
								Vector2 a = targetPoint3.ToVector2XZ();
								float sqrMagnitude = (a - b).sqrMagnitude;
								if ((Object)targetPoint == (Object)null || sqrMagnitude < num)
								{
									targetPoint = targetPoint2;
									num = sqrMagnitude;
								}
							}
						}
						if ((Object)targetPoint != (Object)null)
						{
							player.targetingPointList.Add(targetPoint);
						}
					}
				}
			}
		}
	}
}

using System.Collections;
using UnityEngine;

public class NpcController : ControllerBase
{
	protected IEnumerator mainCoroutine;

	protected bool isStart;

	protected float startWaitTime;

	private bool isPose;

	private Player player
	{
		get;
		set;
	}

	public NonPlayer nonPlayer
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

	private bool isAttack
	{
		get
		{
			if (!(player != null))
			{
				return false;
			}
			return player.actionID == Character.ACTION_ID.ATTACK;
		}
	}

	private bool isGuard
	{
		get
		{
			if (!(player != null))
			{
				return false;
			}
			return player.actionID == (Character.ACTION_ID)19;
		}
	}

	private bool isMove
	{
		get
		{
			if (!(player != null))
			{
				return false;
			}
			return player.actionID == Character.ACTION_ID.MOVE;
		}
	}

	private bool isChangeableAttack
	{
		get
		{
			if (!(player != null))
			{
				return false;
			}
			return player.IsChangeableAction(Character.ACTION_ID.ATTACK);
		}
	}

	private bool isChangeableSpecialAction
	{
		get
		{
			if (!(player != null))
			{
				return false;
			}
			return player.IsChangeableAction((Character.ACTION_ID)33);
		}
	}

	private StageObject target
	{
		get
		{
			if (!(base.brain != null))
			{
				return null;
			}
			return base.brain.targetCtrl.GetCurrentTarget();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		player = (character as Player);
		npcBrain = AttachBrain<NpcBrain>();
		nonPlayer = base.gameObject.GetComponent<NonPlayer>();
	}

	protected override void Start()
	{
		base.Start();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.npcController;
		selfParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController;
		isStart = true;
		if (IsEnableControll())
		{
			OnChangeEnableControll(enable: true);
		}
		if (player != null && player.IsCarrying())
		{
			player.carryingGimmickObject.EndCarry();
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
			if (isStart && base.enabled && player != null && mainCoroutine == null)
			{
				startWaitTime = parameter.startWaitTime;
				mainCoroutine = AIMain();
				StartCoroutine(mainCoroutine);
			}
			return;
		}
		if (mainCoroutine != null)
		{
			StopAllCoroutines();
			mainCoroutine = null;
		}
		if (isGuard)
		{
			player.ActIdle();
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
		while (isPose)
		{
			yield return null;
		}
		while (base.brain == null || !base.brain.isInitialized)
		{
			yield return 0;
		}
		while (!player.isControllable)
		{
			yield return 0;
		}
		if (startWaitTime > 0f)
		{
			yield return new WaitForSeconds(startWaitTime);
		}
		while (base.enabled)
		{
			while (player.IsMirror())
			{
				yield return new WaitForSeconds(1f);
			}
			OnMove();
			OnWeapon();
			float num = 0f;
			if (player.packetSender != null)
			{
				num = player.packetSender.GetWaitTime(0f);
			}
			if (num > 0f)
			{
				yield return new WaitForSeconds(num);
			}
			else
			{
				yield return 0;
			}
		}
		mainCoroutine = null;
	}

	private void OnDead()
	{
		if (!player.isControllable && player.actionID == (Character.ACTION_ID)24 && !player.IsAutoReviving() && player.rescueTime <= 0f && player.deadStartTime >= 0f && !player.isProgressStop() && !(player is Self))
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
			character.ActIdle();
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
		character.ActMoveVelocity(vector2, selfParameter.moveForwardSpeed);
		character.SetLerpRotation(vector2);
	}

	private void OnAvoid(PLACE avoid_place)
	{
		if (!(target == null))
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
			character.LookAt(character._transform.position + b);
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
			if (player.actionID == (Character.ACTION_ID)19)
			{
				player.ActIdle(is_sync: true);
			}
			if (player.enableInputCharge)
			{
				player.SetEnableTap(enable: false);
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
			player.SetEnableTap(enable: false);
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
			character.ActAttack(0);
			base.brain.weaponCtrl.ComboOff();
			base.brain.weaponCtrl.SetBeforeAttackId(0);
		}
	}

	private void OnSpecialAttack()
	{
		if (!player.isActSpecialAction && isChangeableSpecialAction)
		{
			player.SetEnableTap(enable: true);
			player.ActSpecialAction();
			base.brain.weaponCtrl.SetChargeRate(1f);
		}
		else if (player.enableInputCharge && player.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
			player.SetEnableTap(enable: false);
		}
	}

	private void OnArrowAttack()
	{
		if (player.isControllable)
		{
			player.SetEnableTap(enable: true);
			player.ActAttack(0);
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
			player.SetEnableTap(enable: false);
		}
	}

	private void OnGuard()
	{
		if (player.CheckAttackMode(Player.ATTACK_MODE.ONE_HAND_SWORD) && !player.isActSpecialAction && isChangeableSpecialAction)
		{
			player.ActSpecialAction();
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
		if (player == null)
		{
			return;
		}
		player.targetingPointList.Clear();
		if (player.actionTarget == null)
		{
			return;
		}
		Enemy enemy = player.actionTarget as Enemy;
		if (enemy == null || enemy.isDead || !enemy.enableTargetPoint)
		{
			return;
		}
		TargetPoint[] targetPoints = enemy.targetPoints;
		if (targetPoints == null || targetPoints.Length == 0)
		{
			return;
		}
		TargetPoint targetPoint = null;
		float num = float.MaxValue;
		Vector2 b = player._transform.position.ToVector2XZ();
		player.forwardXZ.Normalize();
		int i = 0;
		for (int num2 = targetPoints.Length; i < num2; i++)
		{
			TargetPoint targetPoint2 = targetPoints[i];
			if (targetPoint2.gameObject.activeInHierarchy)
			{
				float sqrMagnitude = (targetPoint2.GetTargetPoint().ToVector2XZ() - b).sqrMagnitude;
				if (targetPoint == null || sqrMagnitude < num)
				{
					targetPoint = targetPoint2;
					num = sqrMagnitude;
				}
			}
		}
		if (targetPoint != null)
		{
			player.targetingPointList.Add(targetPoint);
		}
	}

	public void UseSkill()
	{
		if (nonPlayer != null)
		{
			nonPlayer = base.gameObject.GetComponent<NonPlayer>();
		}
		Debug.Log("UseSkill");
		nonPlayer.NPCSkillAction(0);
	}

	public void SetPose(bool isActivePose)
	{
		isPose = isActivePose;
	}
}

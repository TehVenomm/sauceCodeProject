using System;
using System.Collections;
using UnityEngine;

public class HomeSelfCharacter : HomePlayerCharacterBase
{
	public static bool CTRL = true;

	public bool InitedAnimation;

	private InputManager.TouchInfo dragTouchInfo;

	private Action<HomeStageAreaEvent> noticeCallback;

	private HomeStageAreaEvent lastEvent;

	private Vector3 sentPosition;

	public HomeCharacterBase targetChara
	{
		get;
		private set;
	}

	public int lastTargetNPCID
	{
		get;
		private set;
	}

	public HomeStageAreaEvent targetEvent
	{
		get;
		private set;
	}

	public override int GetUserId()
	{
		return UserInfoManager.IsValidUser() ? MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id : 0;
	}

	public bool IsEnableControl()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return false;
		}
		if (GameSceneEvent.IsStay())
		{
			return false;
		}
		if (isPlayingSitAnimation || isPlayingStandAnimation)
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "HomeTop" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "LoungeTop" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ClanTop")
		{
			return false;
		}
		return true;
	}

	public void SetNoticeCallback(Action<HomeStageAreaEvent> callback)
	{
		noticeCallback = callback;
	}

	public void Sit()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
		isSitting = true;
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(base._transform.get_position());
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(base._transform.get_position());
		}
		Vector3 position = chairPoint.get_transform().get_position();
		SendMoveToSitPosition(position);
		SendSit();
		this.StartCoroutine(DoSit());
	}

	protected new IEnumerator StandUp()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
		SendStandUp();
		chairPoint.ResetSittingCharacter();
		PLCA standUpMotion;
		switch (chairPoint.chairType)
		{
		default:
			standUpMotion = ((sexType != 0) ? PLCA.STAND_UP_F : PLCA.STAND_UP);
			break;
		case ChairPoint.CHAIR_TYPE.BENTCH:
			standUpMotion = ((sexType != 0) ? PLCA.STAND_BENCH_UP_F : PLCA.STAND_BENCH_UP);
			break;
		case ChairPoint.CHAIR_TYPE.SOFA:
			standUpMotion = ((sexType != 0) ? PLCA.STAND_SOFA_UP_F : PLCA.STAND_SOFA_UP);
			break;
		}
		animCtrl.Play(standUpMotion);
		IHomeManager iHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		iHomeManager.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.NORMAL);
		isSitting = false;
		isStanding = true;
		isPlayingStandAnimation = true;
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animCtrl.animator.GetCurrentAnimatorStateInfo(0);
			if (1f < currentAnimatorStateInfo.get_normalizedTime())
			{
				yield return null;
				continue;
			}
			break;
		}
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = animCtrl.animator.GetCurrentAnimatorStateInfo(0);
			if (!(1f > currentAnimatorStateInfo2.get_normalizedTime()))
			{
				break;
			}
			yield return null;
		}
		isPlayingStandAnimation = false;
		isStanding = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		lastTargetNPCID = -1;
		sexType = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		PlayerLoader playerLoader = this.get_gameObject().AddComponent<PlayerLoader>();
		PlayerLoadInfo playerLoadInfo = PlayerLoadInfo.FromUserStatus(need_weapon: false, is_priority_visual_equip: true);
		playerLoadInfo.isNeedToCache = true;
		playerLoader.StartLoad(playerLoadInfo, 8, 99, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: false, need_dev_frame_instantiate: false, SHADER_TYPE.NORMAL, null);
		return playerLoader;
	}

	protected override void InitCollider()
	{
		base.InitCollider();
		Rigidbody component = this.get_gameObject().GetComponent<Rigidbody>();
		component.set_isKinematic(false);
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType != 0) ? PLCA.RUN_F : PLCA.RUN);
		animCtrl.transitionDuration = 0.15f;
		animCtrl.animator.set_speed(1f);
		InitedAnimation = true;
	}

	private void OnEnable()
	{
		if (CTRL)
		{
			InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
			InputManager.OnTap = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTap, new InputManager.OnTouchDelegate(OnTap));
			dragTouchInfo = null;
		}
	}

	private void OnDisable()
	{
		if (CTRL)
		{
			InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
			InputManager.OnTap = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTap, new InputManager.OnTouchDelegate(OnTap));
		}
	}

	private void OnDrag(InputManager.TouchInfo info)
	{
		if (!MonoBehaviourSingleton<UIManager>.I.IsDisable() && IsEnableControl() && (dragTouchInfo == null || !dragTouchInfo.enable))
		{
			dragTouchInfo = info;
		}
	}

	private void OnTap(InputManager.TouchInfo info)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (!IsEnableControl())
		{
			return;
		}
		HomeCamera homeCamera = null;
		IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		if (currentIHomeManager != null)
		{
			homeCamera = currentIHomeManager.HomeCamera;
		}
		if (homeCamera.viewMode != 0)
		{
			return;
		}
		HomeCharacterBase homeCharacterBase = null;
		HomeStageTouchEvent homeStageTouchEvent = null;
		if (targetEvent != null)
		{
			targetEvent.DispatchEvent();
		}
		else if (targetChara != null)
		{
			homeCharacterBase = targetChara;
		}
		else
		{
			Ray val = default(Ray);
			if (currentIHomeManager != null)
			{
				val = homeCamera.targetCamera.ScreenPointToRay(Vector2.op_Implicit(info.position));
			}
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Raycast(val, ref val2, 100f, 259))
			{
				homeCharacterBase = val2.get_transform().GetComponent<HomeCharacterBase>();
				homeStageTouchEvent = val2.get_transform().GetComponent<HomeStageTouchEvent>();
			}
		}
		if (homeCharacterBase != null)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				if (homeCharacterBase is HomeNPCCharacter)
				{
					lastTargetNPCID = ((HomeNPCCharacter)homeCharacterBase).npcInfo.npcID;
				}
				else
				{
					lastTargetNPCID = 0;
				}
				if (homeCharacterBase.DispatchEvent())
				{
					homeCharacterBase.StopMoving();
				}
			}
		}
		else if (homeStageTouchEvent != null)
		{
			homeStageTouchEvent.DispatchEvent();
		}
	}

	private void Update()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		if (animCtrl == null || !animCtrl.animator.get_enabled())
		{
			return;
		}
		Vector3 val = Vector3.get_zero();
		if (dragTouchInfo != null && dragTouchInfo.enable && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() == 1 && IsEnableControl())
		{
			if (isSitting)
			{
				this.StartCoroutine("StandUp");
				val = Vector3.get_zero();
			}
			else
			{
				val = (dragTouchInfo.position - dragTouchInfo.beginPosition).ToVector3XZ();
				val = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_rotation() * val;
				val.y = 0f;
				val.Normalize();
			}
		}
		else
		{
			val = Vector3.get_zero();
		}
		if (val.get_sqrMagnitude() > 0.01f)
		{
			base._transform.set_rotation(Quaternion.Slerp(base._transform.get_rotation(), Quaternion.LookRotation(val), 0.5f));
			animCtrl.PlayMove();
			base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
			if (MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.IsValid())
			{
				SendMove(isMoving: true);
			}
		}
		else if (!isSitting && !isStanding)
		{
			base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
			if (sentPosition != base._transform.get_position() && (MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.IsValid()))
			{
				SendMove(isMoving: false);
			}
			animCtrl.PlayDefault();
		}
		if (!IsEnableControl())
		{
			return;
		}
		RaycastHit val2 = default(RaycastHit);
		if (Physics.Raycast(base._transform.get_localPosition() + new Vector3(0f, 50f, 0f), Vector3.get_down(), ref val2, 50f, 4))
		{
			HomeStageAreaEvent component = val2.get_collider().GetComponent<HomeStageAreaEvent>();
			if (!(component != null))
			{
				return;
			}
			if (noticeCallback != null)
			{
				noticeCallback(component);
			}
			if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				return;
			}
			float num = component.defaultRadius * component.defaultRadius;
			Vector2 val3 = component._transform.TransformPoint(component._collider.get_center()).ToVector2XZ() - base._transform.get_localPosition().ToVector2XZ();
			if (val3.get_sqrMagnitude() <= num)
			{
				if (lastEvent != component)
				{
					component.DispatchEvent();
					lastEvent = component;
				}
			}
			else
			{
				lastEvent = null;
			}
		}
		else
		{
			if (noticeCallback != null)
			{
				noticeCallback(null);
			}
			lastEvent = null;
		}
	}

	private void SendMove(bool isMoving)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		float num = (!isMoving) ? 0.3f : 5f;
		float num2 = Vector3.Distance(sentPosition, base._transform.get_position());
		if (num2 > num)
		{
			Lounge_Model_RoomMove lounge_Model_RoomMove = new Lounge_Model_RoomMove();
			lounge_Model_RoomMove.id = 1005;
			lounge_Model_RoomMove.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			lounge_Model_RoomMove.pos = base._transform.get_position();
			if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove);
			}
			if (ClanMatchingManager.IsValidInClan() && MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove);
			}
			sentPosition = lounge_Model_RoomMove.pos;
		}
	}

	private void SendMoveToSitPosition(Vector3 pos)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Lounge_Model_RoomMove lounge_Model_RoomMove = new Lounge_Model_RoomMove();
		lounge_Model_RoomMove.id = 1005;
		lounge_Model_RoomMove.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomMove.pos = pos;
		if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove);
		}
		if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove);
		}
		sentPosition = lounge_Model_RoomMove.pos;
	}

	private void SendSit()
	{
		Lounge_Model_RoomAction lounge_Model_RoomAction = new Lounge_Model_RoomAction();
		lounge_Model_RoomAction.id = 1005;
		lounge_Model_RoomAction.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomAction.aid = 1;
		if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction);
		}
		if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction);
		}
	}

	private void SendStandUp()
	{
		Lounge_Model_RoomAction lounge_Model_RoomAction = new Lounge_Model_RoomAction();
		lounge_Model_RoomAction.id = 1005;
		lounge_Model_RoomAction.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomAction.aid = 2;
		if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction);
		}
		if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction);
		}
	}

	public LOUNGE_ACTION_TYPE GetActionType()
	{
		return base.CurrentActionType;
	}
}

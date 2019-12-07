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
		if (!UserInfoManager.IsValidUser())
		{
			return 0;
		}
		return MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
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
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
		isSitting = true;
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(base._transform.position);
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(base._transform.position);
		}
		Vector3 position = chairPoint.transform.position;
		SendMoveToSitPosition(position);
		SendSit();
		StartCoroutine(DoSit());
	}

	protected override IEnumerator StandUp()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
		SendStandUp();
		chairPoint.ResetSittingCharacter();
		PLCA anim;
		switch (chairPoint.chairType)
		{
		default:
			anim = ((sexType == 0) ? PLCA.STAND_UP : PLCA.STAND_UP_F);
			break;
		case ChairPoint.CHAIR_TYPE.BENTCH:
			anim = ((sexType == 0) ? PLCA.STAND_BENCH_UP : PLCA.STAND_BENCH_UP_F);
			break;
		case ChairPoint.CHAIR_TYPE.SOFA:
			anim = ((sexType == 0) ? PLCA.STAND_SOFA_UP : PLCA.STAND_SOFA_UP_F);
			break;
		}
		animCtrl.Play(anim);
		GameSceneGlobalSettings.GetCurrentIHomeManager().HomeCamera.ChangeView(HomeCamera.VIEW_MODE.NORMAL);
		isSitting = false;
		isStanding = true;
		isPlayingStandAnimation = true;
		while (1f < animCtrl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return null;
		}
		while (1f > animCtrl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return null;
		}
		isPlayingStandAnimation = false;
		isStanding = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		lastTargetNPCID = -1;
		sexType = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		PlayerLoader playerLoader = base.gameObject.AddComponent<PlayerLoader>();
		PlayerLoadInfo playerLoadInfo = PlayerLoadInfo.FromUserStatus(need_weapon: false, is_priority_visual_equip: true);
		playerLoadInfo.isNeedToCache = true;
		playerLoader.StartLoad(playerLoadInfo, 8, 99, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: false, need_dev_frame_instantiate: false, SHADER_TYPE.NORMAL, null);
		return playerLoader;
	}

	protected override void InitCollider()
	{
		base.InitCollider();
		base.gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType == 0) ? PLCA.RUN : PLCA.RUN_F);
		animCtrl.transitionDuration = 0.15f;
		animCtrl.animator.speed = 1f;
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
			Ray ray = default(Ray);
			if (currentIHomeManager != null)
			{
				ray = homeCamera.targetCamera.ScreenPointToRay(info.position);
			}
			if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, 259))
			{
				homeCharacterBase = hitInfo.transform.GetComponent<HomeCharacterBase>();
				homeStageTouchEvent = hitInfo.transform.GetComponent<HomeStageTouchEvent>();
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
		if (animCtrl == null || !animCtrl.animator.enabled)
		{
			return;
		}
		Vector3 vector = Vector3.zero;
		if (dragTouchInfo != null && dragTouchInfo.enable && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() == 1 && IsEnableControl())
		{
			if (isSitting)
			{
				StartCoroutine("StandUp");
				vector = Vector3.zero;
			}
			else
			{
				vector = (dragTouchInfo.position - dragTouchInfo.beginPosition).ToVector3XZ();
				vector = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation * vector;
				vector.y = 0f;
				vector.Normalize();
			}
		}
		else
		{
			vector = Vector3.zero;
		}
		if (vector.sqrMagnitude > 0.01f)
		{
			base._transform.rotation = Quaternion.Slerp(base._transform.rotation, Quaternion.LookRotation(vector), 0.5f);
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
			if (sentPosition != base._transform.position && (MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.IsValid()))
			{
				SendMove(isMoving: false);
			}
			animCtrl.PlayDefault();
		}
		if (!IsEnableControl())
		{
			return;
		}
		if (Physics.Raycast(base._transform.localPosition + new Vector3(0f, 50f, 0f), Vector3.down, out RaycastHit hitInfo, 50f, 4))
		{
			HomeStageAreaEvent component = hitInfo.collider.GetComponent<HomeStageAreaEvent>();
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
			if ((component._transform.TransformPoint(component._collider.center).ToVector2XZ() - base._transform.localPosition.ToVector2XZ()).sqrMagnitude <= num)
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
		float num = isMoving ? 5f : 0.3f;
		if (Vector3.Distance(sentPosition, base._transform.position) > num)
		{
			Lounge_Model_RoomMove lounge_Model_RoomMove = new Lounge_Model_RoomMove();
			lounge_Model_RoomMove.id = 1005;
			lounge_Model_RoomMove.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			lounge_Model_RoomMove.pos = base._transform.position;
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

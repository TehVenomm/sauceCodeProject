using System;
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
		if (isPlayingSitAnimation)
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "HomeTop" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "LoungeTop" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "GuildTop")
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
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
		isSitting = true;
		chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this);
		Vector3 position = chairPoint.get_transform().get_position();
		SendMoveToSitPosition(position);
		SendSit();
		this.StartCoroutine(DoSit());
	}

	private void StandUp()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
		SendStandUp();
		chairPoint.ResetSittingCharacter();
		PLCA anim = (sexType != 0) ? PLCA.STAND_UP_F : PLCA.STAND_UP;
		animCtrl.Play(anim, false);
		MonoBehaviourSingleton<LoungeManager>.I.HomeCamera.ChangeView(HomeCamera.VIEW_MODE.NORMAL);
		isSitting = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		lastTargetNPCID = -1;
		sexType = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		PlayerLoader playerLoader = this.get_gameObject().AddComponent<PlayerLoader>();
		PlayerLoadInfo playerLoadInfo = PlayerLoadInfo.FromUserStatus(false, true, -1);
		playerLoadInfo.isNeedToCache = true;
		playerLoader.StartLoad(playerLoadInfo, 8, 99, false, false, true, true, false, false, false, false, SHADER_TYPE.NORMAL, null, true, -1);
		return playerLoader;
	}

	protected override void InitCollider()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		if (IsEnableControl())
		{
			HomeCamera homeCamera = null;
			if (MonoBehaviourSingleton<HomeManager>.IsValid())
			{
				homeCamera = MonoBehaviourSingleton<HomeManager>.I.HomeCamera;
			}
			else if (MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				homeCamera = MonoBehaviourSingleton<LoungeManager>.I.HomeCamera;
			}
			else if (MonoBehaviourSingleton<GuildStageManager>.IsValid())
			{
				homeCamera = MonoBehaviourSingleton<GuildStageManager>.I.HomeCamera;
			}
			if (homeCamera.viewMode == HomeCamera.VIEW_MODE.NORMAL)
			{
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
					if (MonoBehaviourSingleton<HomeManager>.IsValid() || MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<GuildStageManager>.IsValid())
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
		}
	}

	private void Update()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		if (!(animCtrl == null) && animCtrl.animator.get_enabled())
		{
			Vector3 val = Vector3.get_zero();
			if (dragTouchInfo != null && dragTouchInfo.enable && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() == 1 && IsEnableControl())
			{
				val = (dragTouchInfo.position - dragTouchInfo.beginPosition).ToVector3XZ();
				val = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_rotation() * val;
				val.y = 0f;
				val.Normalize();
			}
			else
			{
				val = Vector3.get_zero();
			}
			if (val.get_sqrMagnitude() > 0.01f)
			{
				base._transform.set_rotation(Quaternion.Slerp(base._transform.get_rotation(), Quaternion.LookRotation(val), 0.5f));
				animCtrl.PlayMove(false);
				base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
				if (MonoBehaviourSingleton<LoungeManager>.IsValid())
				{
					SendMove(true);
				}
				if (isSitting)
				{
					StandUp();
				}
			}
			else if (!isSitting)
			{
				base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
				if (sentPosition != base._transform.get_position() && MonoBehaviourSingleton<LoungeManager>.IsValid())
				{
					SendMove(false);
				}
				animCtrl.PlayDefault(false);
			}
			if (IsEnableControl())
			{
				RaycastHit val2 = default(RaycastHit);
				if (Physics.Raycast(base._transform.get_localPosition() + new Vector3(0f, 50f, 0f), Vector3.get_down(), ref val2, 50f, 4))
				{
					HomeStageAreaEvent component = val2.get_collider().GetComponent<HomeStageAreaEvent>();
					if (component != null)
					{
						if (noticeCallback != null)
						{
							noticeCallback(component);
						}
						if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
						{
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
		}
	}

	private void SendMove(bool isMoving)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		float num = (!isMoving) ? 0.3f : 5f;
		float num2 = Vector3.Distance(sentPosition, base._transform.get_position());
		if (num2 > num)
		{
			Lounge_Model_RoomMove lounge_Model_RoomMove = new Lounge_Model_RoomMove();
			lounge_Model_RoomMove.id = 1005;
			lounge_Model_RoomMove.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			lounge_Model_RoomMove.pos = base._transform.get_position();
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove, false, null, null);
			sentPosition = lounge_Model_RoomMove.pos;
		}
	}

	private void SendMoveToSitPosition(Vector3 pos)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Lounge_Model_RoomMove lounge_Model_RoomMove = new Lounge_Model_RoomMove();
		lounge_Model_RoomMove.id = 1005;
		lounge_Model_RoomMove.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomMove.pos = pos;
		MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove, false, null, null);
		sentPosition = lounge_Model_RoomMove.pos;
	}

	private void SendSit()
	{
		Lounge_Model_RoomAction lounge_Model_RoomAction = new Lounge_Model_RoomAction();
		lounge_Model_RoomAction.id = 1005;
		lounge_Model_RoomAction.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomAction.aid = 1;
		MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction, false, null, null);
	}

	private void SendStandUp()
	{
		Lounge_Model_RoomAction lounge_Model_RoomAction = new Lounge_Model_RoomAction();
		lounge_Model_RoomAction.id = 1005;
		lounge_Model_RoomAction.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		lounge_Model_RoomAction.aid = 2;
		MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomAction, false, null, null);
	}

	public LOUNGE_ACTION_TYPE GetActionType()
	{
		return base.CurrentActionType;
	}
}

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
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
		isSitting = true;
		chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this);
		Vector3 position = chairPoint.transform.position;
		SendMoveToSitPosition(position);
		SendSit();
		StartCoroutine(DoSit());
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
		lastTargetNPCID = -1;
		sexType = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		PlayerLoader playerLoader = base.gameObject.AddComponent<PlayerLoader>();
		playerLoader.StartLoad(PlayerLoadInfo.FromUserStatus(false, true, -1), 8, 99, false, false, true, true, false, false, false, false, SHADER_TYPE.NORMAL, null, true, -1);
		return playerLoader;
	}

	protected override void InitCollider()
	{
		base.InitCollider();
		Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
		component.isKinematic = false;
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType != 0) ? PLCA.RUN_F : PLCA.RUN);
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
				if ((UnityEngine.Object)targetEvent != (UnityEngine.Object)null)
				{
					targetEvent.DispatchEvent();
				}
				else if ((UnityEngine.Object)targetChara != (UnityEngine.Object)null)
				{
					homeCharacterBase = targetChara;
				}
				else
				{
					Ray ray = default(Ray);
					if (MonoBehaviourSingleton<HomeManager>.IsValid() || MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<GuildStageManager>.IsValid())
					{
						ray = homeCamera.targetCamera.ScreenPointToRay(info.position);
					}
					if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, 259))
					{
						homeCharacterBase = hitInfo.transform.GetComponent<HomeCharacterBase>();
						homeStageTouchEvent = hitInfo.transform.GetComponent<HomeStageTouchEvent>();
					}
				}
				if ((UnityEngine.Object)homeCharacterBase != (UnityEngine.Object)null)
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
				else if ((UnityEngine.Object)homeStageTouchEvent != (UnityEngine.Object)null)
				{
					homeStageTouchEvent.DispatchEvent();
				}
			}
		}
	}

	private void Update()
	{
		if (!((UnityEngine.Object)animCtrl == (UnityEngine.Object)null) && animCtrl.animator.enabled)
		{
			Vector3 vector = Vector3.zero;
			if (dragTouchInfo != null && dragTouchInfo.enable && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() == 1 && IsEnableControl())
			{
				vector = (dragTouchInfo.position - dragTouchInfo.beginPosition).ToVector3XZ();
				vector = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation * vector;
				vector.y = 0f;
				vector.Normalize();
			}
			else
			{
				vector = Vector3.zero;
			}
			if (vector.sqrMagnitude > 0.01f)
			{
				base._transform.rotation = Quaternion.Slerp(base._transform.rotation, Quaternion.LookRotation(vector), 0.5f);
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
				if (sentPosition != base._transform.position && MonoBehaviourSingleton<LoungeManager>.IsValid())
				{
					SendMove(false);
				}
				animCtrl.PlayDefault(false);
			}
			if (IsEnableControl())
			{
				if (Physics.Raycast(base._transform.localPosition + new Vector3(0f, 50f, 0f), Vector3.down, out RaycastHit hitInfo, 50f, 4))
				{
					HomeStageAreaEvent component = hitInfo.collider.GetComponent<HomeStageAreaEvent>();
					if ((UnityEngine.Object)component != (UnityEngine.Object)null)
					{
						if (noticeCallback != null)
						{
							noticeCallback(component);
						}
						if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
						{
							float num = component.defaultRadius * component.defaultRadius;
							if ((component._transform.TransformPoint(component._collider.center).ToVector2XZ() - base._transform.localPosition.ToVector2XZ()).sqrMagnitude <= num)
							{
								if ((UnityEngine.Object)lastEvent != (UnityEngine.Object)component)
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
		float num = (!isMoving) ? 0.3f : 5f;
		float num2 = Vector3.Distance(sentPosition, base._transform.position);
		if (num2 > num)
		{
			Lounge_Model_RoomMove lounge_Model_RoomMove = new Lounge_Model_RoomMove();
			lounge_Model_RoomMove.id = 1005;
			lounge_Model_RoomMove.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			lounge_Model_RoomMove.pos = base._transform.position;
			MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast(lounge_Model_RoomMove, false, null, null);
			sentPosition = lounge_Model_RoomMove.pos;
		}
	}

	private void SendMoveToSitPosition(Vector3 pos)
	{
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

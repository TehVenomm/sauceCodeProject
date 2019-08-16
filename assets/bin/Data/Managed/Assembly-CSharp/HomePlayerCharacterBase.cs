using Network;
using System.Collections;
using UnityEngine;

public abstract class HomePlayerCharacterBase : HomeCharacterBase
{
	protected FriendCharaInfo charaInfo;

	protected bool isSitting;

	protected bool isSit;

	protected bool isStanding;

	protected ChairPoint chairPoint;

	protected bool isPlayingSitAnimation;

	protected bool isPlayingStandAnimation;

	protected ChatAppeal chatAppeal;

	protected StampAppeal stampAppeal;

	private bool isRegistChat;

	private bool isRegistClanChat;

	private Transform head;

	public LOUNGE_ACTION_TYPE CurrentActionType
	{
		get;
		protected set;
	}

	protected Transform Head
	{
		get
		{
			if (head == null && animator != null)
			{
				head = animator.get_transform().Find("PLC_Origin/Move/Root/Hip/Spine00/Spine01/Neck/Head");
			}
			return head;
		}
	}

	public virtual int GetUserId()
	{
		return (GetFriendCharaInfo() != null) ? GetFriendCharaInfo().userId : 0;
	}

	public override FriendCharaInfo GetFriendCharaInfo()
	{
		return charaInfo;
	}

	public void SetFriendCharcterInfo(FriendCharaInfo charaInfo)
	{
		this.charaInfo = charaInfo;
	}

	protected override void OnAnimPlay(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		if (anim == PLCA.WALK)
		{
			animator.set_applyRootMotion(true);
		}
		else
		{
			animator.set_applyRootMotion((anim == anim_ctrl.moveAnim) ? true : false);
		}
	}

	protected override void OnDestroy()
	{
		if (chatAppeal != null && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			Object.DestroyImmediate(chatAppeal.get_gameObject());
			chatAppeal = null;
			Object.DestroyImmediate(stampAppeal.get_gameObject());
			stampAppeal = null;
			MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat -= RegistOnRecvChat;
			MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat -= UnRegistOnRecvChat;
			MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat -= RegistOnRecvChat;
			MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat -= UnRegistOnRecvChat;
		}
		if (isRegistChat && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			UnRegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
		}
		if (isRegistClanChat && MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
		{
			isRegistClanChat = false;
			MonoBehaviourSingleton<ClanMatchingManager>.I.OnReceiveCharacterMessage -= OnReceiveCharacterMessage;
		}
		base.OnDestroy();
	}

	protected virtual IEnumerator DoSit()
	{
		isPlayingSitAnimation = true;
		chairPoint.SetSittingCharacter(this);
		Vector3 sitPos = chairPoint.get_transform().get_position();
		if (animCtrl == null)
		{
			InitAnim();
		}
		while (true)
		{
			animCtrl.Play(PLCA.WALK);
			Vector3 pos = base._transform.get_position();
			Vector3 diff = sitPos - pos;
			Vector2 val = diff.ToVector2XZ();
			Vector2 dir = val.get_normalized();
			Quaternion val2 = Quaternion.LookRotation(dir.ToVector3XZ());
			Vector3 eulerAngles = val2.get_eulerAngles();
			float rot2 = eulerAngles.y;
			float vel = 0f;
			Vector3 eulerAngles2 = base._transform.get_eulerAngles();
			rot2 = Mathf.SmoothDampAngle(eulerAngles2.y, rot2, ref vel, 0.1f);
			base._transform.set_eulerAngles(new Vector3(0f, rot2, 0f));
			if (diff.get_magnitude() < 0.15f)
			{
				break;
			}
			yield return null;
		}
		Vector3 position = chairPoint.dir.get_transform().get_position();
		Vector3 val3 = position - sitPos;
		Quaternion rotation = Quaternion.LookRotation(val3);
		base._transform.set_rotation(rotation);
		isSit = true;
		PLCA sitMotion2;
		switch (chairPoint.chairType)
		{
		default:
			sitMotion2 = ((sexType != 0) ? PLCA.SIT_F : PLCA.SIT);
			break;
		case ChairPoint.CHAIR_TYPE.BENTCH:
			sitMotion2 = ((sexType != 0) ? PLCA.SIT_BENCH_F : PLCA.SIT_BENCH);
			break;
		case ChairPoint.CHAIR_TYPE.SOFA:
			sitMotion2 = ((sexType != 0) ? PLCA.SIT_SOFA_F : PLCA.SIT_SOFA);
			break;
		}
		animCtrl.Play(sitMotion2);
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
		isPlayingSitAnimation = false;
		switch (chairPoint.chairType)
		{
		default:
			sitMotion2 = ((sexType != 0) ? PLCA.SIT_IDLE_F : PLCA.SIT_IDLE);
			break;
		case ChairPoint.CHAIR_TYPE.BENTCH:
			sitMotion2 = ((sexType != 0) ? PLCA.SIT_BENCH_IDLE_F : PLCA.SIT_BENCH_IDLE);
			break;
		case ChairPoint.CHAIR_TYPE.SOFA:
			sitMotion2 = ((sexType != 0) ? PLCA.SIT_SOFA_IDLE_F : PLCA.SIT_SOFA_IDLE);
			break;
		}
		animCtrl.Play(sitMotion2);
	}

	protected virtual IEnumerator StandUp()
	{
		isSit = false;
		CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
		if (chairPoint != null)
		{
			chairPoint.ResetSittingCharacter();
		}
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

	public void SetChatEvent()
	{
		RegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
		MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += RegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += UnRegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat += RegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat += UnRegistOnRecvChat;
		Transform val = MonoBehaviourSingleton<UIManager>.I.common.CreateStampAppeal();
		stampAppeal = val.get_gameObject().AddComponent<StampAppeal>();
		stampAppeal.collectUI = val;
		stampAppeal.CreateCtrlsArray(typeof(StampAppeal.UI));
		val = MonoBehaviourSingleton<UIManager>.I.common.CreateChatAppeal();
		chatAppeal = val.get_gameObject().AddComponent<ChatAppeal>();
		chatAppeal.collectUI = val;
		chatAppeal.CreateCtrlsArray(typeof(ChatAppeal.UI));
	}

	public void SetClanChatEvent()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
		{
			isRegistClanChat = true;
			MonoBehaviourSingleton<ClanMatchingManager>.I.OnReceiveCharacterMessage += OnReceiveCharacterMessage;
		}
	}

	public void OnReceiveCharacterMessage(ClanChatMessageModel model)
	{
		if (model.type == 1 && model.userId == GetUserId())
		{
			int num = ClanMatchingManager.convertStringToStampId(model.body);
			if (num > 0)
			{
				stampAppeal.View(num, Head ?? base._transform, head == null);
			}
			else
			{
				chatAppeal.View(model.body, Head ?? base._transform, head == null);
			}
		}
	}

	public void RegistOnRecvChat(ChatRoom room)
	{
		if (!isRegistChat && room != null)
		{
			isRegistChat = true;
			room.onReceiveStamp += ShowStamp;
			room.onReceiveText += ShowChatAppeal;
		}
	}

	public void UnRegistOnRecvChat(ChatRoom room)
	{
		if (isRegistChat && room != null)
		{
			isRegistChat = false;
			room.onReceiveStamp -= ShowStamp;
			room.onReceiveText -= ShowChatAppeal;
		}
	}

	public void ShowStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false)
	{
		if (userId == GetUserId())
		{
			stampAppeal.View(stampId, Head ?? base._transform, head == null);
		}
	}

	public void ShowChatAppeal(int userId, string userName, string text, string chatItemId, bool isOldMessage = false)
	{
		if (userId == GetUserId())
		{
			chatAppeal.View(text, Head ?? base._transform, head == null);
		}
	}
}

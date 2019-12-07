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
				head = animator.transform.Find("PLC_Origin/Move/Root/Hip/Spine00/Spine01/Neck/Head");
			}
			return head;
		}
	}

	public virtual int GetUserId()
	{
		if (GetFriendCharaInfo() == null)
		{
			return 0;
		}
		return GetFriendCharaInfo().userId;
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
			animator.applyRootMotion = true;
		}
		else
		{
			animator.applyRootMotion = ((anim == anim_ctrl.moveAnim) ? true : false);
		}
	}

	protected override void OnDestroy()
	{
		if (chatAppeal != null && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			Object.DestroyImmediate(chatAppeal.gameObject);
			chatAppeal = null;
			Object.DestroyImmediate(stampAppeal.gameObject);
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
		Vector3 sitPos = chairPoint.transform.position;
		if (animCtrl == null)
		{
			InitAnim();
		}
		while (true)
		{
			animCtrl.Play(PLCA.WALK);
			Vector3 position = base._transform.position;
			Vector3 vector = sitPos - position;
			float y = Quaternion.LookRotation(vector.ToVector2XZ().normalized.ToVector3XZ()).eulerAngles.y;
			float currentVelocity = 0f;
			y = Mathf.SmoothDampAngle(base._transform.eulerAngles.y, y, ref currentVelocity, 0.1f);
			base._transform.eulerAngles = new Vector3(0f, y, 0f);
			if (vector.magnitude < 0.15f)
			{
				break;
			}
			yield return null;
		}
		Quaternion rotation = Quaternion.LookRotation(chairPoint.dir.transform.position - sitPos);
		base._transform.rotation = rotation;
		isSit = true;
		PLCA anim;
		switch (chairPoint.chairType)
		{
		default:
			anim = ((sexType == 0) ? PLCA.SIT : PLCA.SIT_F);
			break;
		case ChairPoint.CHAIR_TYPE.BENTCH:
			anim = ((sexType == 0) ? PLCA.SIT_BENCH : PLCA.SIT_BENCH_F);
			break;
		case ChairPoint.CHAIR_TYPE.SOFA:
			anim = ((sexType == 0) ? PLCA.SIT_SOFA : PLCA.SIT_SOFA_F);
			break;
		}
		animCtrl.Play(anim);
		while (1f < animCtrl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return null;
		}
		while (1f > animCtrl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return null;
		}
		isPlayingSitAnimation = false;
		switch (chairPoint.chairType)
		{
		default:
			anim = ((sexType == 0) ? PLCA.SIT_IDLE : PLCA.SIT_IDLE_F);
			break;
		case ChairPoint.CHAIR_TYPE.BENTCH:
			anim = ((sexType == 0) ? PLCA.SIT_BENCH_IDLE : PLCA.SIT_BENCH_IDLE_F);
			break;
		case ChairPoint.CHAIR_TYPE.SOFA:
			anim = ((sexType == 0) ? PLCA.SIT_SOFA_IDLE : PLCA.SIT_SOFA_IDLE_F);
			break;
		}
		animCtrl.Play(anim);
	}

	protected virtual IEnumerator StandUp()
	{
		isSit = false;
		CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
		if (chairPoint != null)
		{
			chairPoint.ResetSittingCharacter();
		}
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

	public void SetChatEvent()
	{
		RegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
		MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += RegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += UnRegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat += RegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat += UnRegistOnRecvChat;
		Transform transform = MonoBehaviourSingleton<UIManager>.I.common.CreateStampAppeal();
		stampAppeal = transform.gameObject.AddComponent<StampAppeal>();
		stampAppeal.collectUI = transform;
		stampAppeal.CreateCtrlsArray(typeof(StampAppeal.UI));
		transform = MonoBehaviourSingleton<UIManager>.I.common.CreateChatAppeal();
		chatAppeal = transform.gameObject.AddComponent<ChatAppeal>();
		chatAppeal.collectUI = transform;
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

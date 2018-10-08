using Network;
using System.Collections;
using UnityEngine;

public abstract class HomePlayerCharacterBase : HomeCharacterBase
{
	protected FriendCharaInfo charaInfo;

	protected bool isSitting;

	protected ChairPoint chairPoint;

	protected bool isPlayingSitAnimation;

	protected ChatAppeal chatAppeal;

	protected StampAppeal stampAppeal;

	private bool isRegistChat;

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
			if ((Object)head == (Object)null && (Object)animator != (Object)null)
			{
				head = animator.transform.Find("PLC_Origin/Move/Root/Hip/Spine00/Spine01/Neck/Head");
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
			animator.applyRootMotion = true;
		}
		else
		{
			animator.applyRootMotion = ((anim == anim_ctrl.moveAnim) ? true : false);
		}
	}

	protected override void OnDestroy()
	{
		if ((Object)chatAppeal != (Object)null && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			Object.DestroyImmediate(chatAppeal.gameObject);
			chatAppeal = null;
			Object.DestroyImmediate(stampAppeal.gameObject);
			stampAppeal = null;
			MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat -= RegistOnRecvChat;
			MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat -= UnRegistOnRecvChat;
		}
		if (isRegistChat && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			UnRegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
		}
		base.OnDestroy();
	}

	protected virtual IEnumerator DoSit()
	{
		isPlayingSitAnimation = true;
		chairPoint.SetSittingCharacter(this);
		Vector3 sitPos = chairPoint.transform.position;
		while (true)
		{
			animCtrl.Play(PLCA.WALK, false);
			Vector3 pos = base._transform.position;
			Vector3 diff = sitPos - pos;
			Vector2 dir = diff.ToVector2XZ().normalized;
			Vector3 eulerAngles = Quaternion.LookRotation(dir.ToVector3XZ()).eulerAngles;
			float rot2 = eulerAngles.y;
			float vel = 0f;
			Vector3 eulerAngles2 = base._transform.eulerAngles;
			rot2 = Mathf.SmoothDampAngle(eulerAngles2.y, rot2, ref vel, 0.1f);
			base._transform.eulerAngles = new Vector3(0f, rot2, 0f);
			if (diff.magnitude < 0.15f)
			{
				break;
			}
			yield return (object)null;
		}
		Vector3 dirPoint = chairPoint.dir.transform.position;
		Vector3 sitDir = dirPoint - sitPos;
		Quaternion sitRot = Quaternion.LookRotation(sitDir);
		base._transform.rotation = sitRot;
		PLCA sitMotion = (sexType != 0) ? PLCA.SIT_F : PLCA.SIT;
		animCtrl.Play(sitMotion, false);
		while (1f > animCtrl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return (object)null;
		}
		isPlayingSitAnimation = false;
	}

	public void SetChatEvent()
	{
		RegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
		MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += RegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += UnRegistOnRecvChat;
		Transform transform = MonoBehaviourSingleton<UIManager>.I.common.CreateStampAppeal();
		stampAppeal = transform.gameObject.AddComponent<StampAppeal>();
		stampAppeal.collectUI = transform;
		stampAppeal.CreateCtrlsArray(typeof(StampAppeal.UI));
		transform = MonoBehaviourSingleton<UIManager>.I.common.CreateChatAppeal();
		chatAppeal = transform.gameObject.AddComponent<ChatAppeal>();
		chatAppeal.collectUI = transform;
		chatAppeal.CreateCtrlsArray(typeof(ChatAppeal.UI));
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

	public void ShowStamp(int userId, string userName, int stampId)
	{
		if (userId == GetUserId())
		{
			stampAppeal.View(stampId, Head ?? base._transform, (Object)head == (Object)null);
		}
	}

	public void ShowChatAppeal(int userId, string userName, string text)
	{
		if (userId == GetUserId())
		{
			chatAppeal.View(text, Head ?? base._transform, (Object)head == (Object)null);
		}
	}
}

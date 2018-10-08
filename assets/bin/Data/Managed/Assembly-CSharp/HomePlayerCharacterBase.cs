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
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
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
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (chatAppeal != null && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			Object.DestroyImmediate(chatAppeal.get_gameObject());
			chatAppeal = null;
			Object.DestroyImmediate(stampAppeal.get_gameObject());
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
		Vector3 sitPos = chairPoint.get_transform().get_position();
		while (true)
		{
			animCtrl.Play(PLCA.WALK, false);
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
			yield return (object)null;
		}
		Vector3 dirPoint = chairPoint.dir.get_transform().get_position();
		Vector3 sitDir = dirPoint - sitPos;
		Quaternion sitRot = Quaternion.LookRotation(sitDir);
		base._transform.set_rotation(sitRot);
		PLCA sitMotion = (sexType != 0) ? PLCA.SIT_F : PLCA.SIT;
		animCtrl.Play(sitMotion, false);
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animCtrl.animator.GetCurrentAnimatorStateInfo(0);
			if (!(1f > currentAnimatorStateInfo.get_normalizedTime()))
			{
				break;
			}
			yield return (object)null;
		}
		isPlayingSitAnimation = false;
	}

	public void SetChatEvent()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		RegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
		MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += RegistOnRecvChat;
		MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += UnRegistOnRecvChat;
		Transform val = MonoBehaviourSingleton<UIManager>.I.common.CreateStampAppeal();
		stampAppeal = val.get_gameObject().AddComponent<StampAppeal>();
		stampAppeal.collectUI = val;
		stampAppeal.CreateCtrlsArray(typeof(StampAppeal.UI));
		val = MonoBehaviourSingleton<UIManager>.I.common.CreateChatAppeal();
		chatAppeal = val.get_gameObject().AddComponent<ChatAppeal>();
		chatAppeal.collectUI = val;
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
			stampAppeal.View(stampId, Head ?? base._transform, head == null);
		}
	}

	public void ShowChatAppeal(int userId, string userName, string text)
	{
		if (userId == GetUserId())
		{
			chatAppeal.View(text, Head ?? base._transform, head == null);
		}
	}
}

using Network;
using System;
using System.Collections.Generic;

public class HomeFriendDetail : QuestRoomUserInfoDetail
{
	private FriendCharaInfo charaInfo;

	protected override bool IsRoomObserve()
	{
		return false;
	}

	public override void Initialize()
	{
		charaInfo = (GameSection.GetEventData() as FriendCharaInfo);
		InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
		playerRecord.id = charaInfo.userId;
		playerRecord.isNPC = false;
		playerRecord.isSelf = false;
		playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(charaInfo, need_weapon: true, need_helm: true, need_leg: true, is_priority_visual_equip: true);
		playerRecord.animID = PLAYER_ANIM_TYPE.GetStatus(charaInfo.sex);
		playerRecord.charaInfo = charaInfo;
		GameSection.SetEventData(new object[3]
		{
			playerRecord,
			false,
			false
		});
		base.Initialize();
	}

	public override int GetCharaSex()
	{
		return charaInfo.sex;
	}

	public override void SetupCommentText()
	{
		SetActive(transRoot, UI.SPR_COMMENT, is_visible: true);
		SetLabelText(transRoot, UI.LBL_COMMENT, charaInfo.comment);
	}

	public override void SetupFollowButton()
	{
		FriendCharaInfo friendCharaInfo = MonoBehaviourSingleton<FriendManager>.I.homeCharas.chara.Find((FriendCharaInfo chara) => chara.userId == charaInfo.userId);
		bool flag = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
		bool flag2 = !friendCharaInfo.following;
		bool following = friendCharaInfo.following;
		bool follower = friendCharaInfo.follower;
		SetEvent(transRoot, UI.BTN_FOLLOW, "FOLLOW", 0);
		if (flag && flag2)
		{
			SetActive(transRoot, UI.BTN_FOLLOW, is_visible: true);
			SetActive(transRoot, UI.BTN_UNFOLLOW, is_visible: false);
			SetEvent(transRoot, UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
		}
		else
		{
			SetActive(transRoot, UI.BTN_FOLLOW, flag2);
			SetActive(transRoot, UI.BTN_UNFOLLOW, !flag2);
		}
		SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, is_visible: true);
		bool flag3 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(charaInfo.userId);
		SetActive(transRoot, UI.BTN_BLACKLIST_IN, !flag3);
		SetActive(transRoot, UI.BTN_BLACKLIST_OUT, flag3);
		SetActive(transRoot, UI.SPR_FOLLOW_ARROW, !flag3 && !flag2);
		SetActive(transRoot, UI.SPR_FOLLOWER_ARROW, !flag3 && follower);
		SetActive(transRoot, UI.SPR_BLACKLIST_ICON, flag3);
		bool same_clan_user = false;
		if (record != null && record.charaInfo != null && record.charaInfo.userClanData != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			same_clan_user = (record.charaInfo.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
		}
		SetFollowStatus(following, follower, flag3, same_clan_user);
	}

	protected override void SetupLastLogin()
	{
		SetActive(transRoot, UI.OBJ_LAST_LOGIN, is_visible: true);
		SetLabelText(transRoot, UI.LBL_LAST_LOGIN_TIME, charaInfo.lastLogin);
	}

	protected override void UpdateUserIDLabel()
	{
		SetLabelText(transRoot, UI.LBL_USER_ID, charaInfo.code);
	}

	protected override void OnQuery_FOLLOW()
	{
		GameSection.SetEventData(new object[1]
		{
			charaInfo.name
		});
		List<int> list = new List<int>();
		list.Add(charaInfo.userId);
		SendFollow(list, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(charaInfo.userId, follow: true);
			}
		});
	}

	protected override void OnQuery_UNFOLLOW()
	{
		GameSection.SetEventData(new object[1]
		{
			charaInfo.name
		});
	}

	protected virtual void OnQuery_HomeFriendUnFollowMessage_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			charaInfo.name
		});
		SendUnFollow(charaInfo.userId, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(charaInfo.userId, follow: false);
			}
		});
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
	}

	private new void OnEnable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	private new void OnDisable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		nowSectionName = string.Empty;
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && CanRotateSection())
		{
			loader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}

	private bool CanRotateSection()
	{
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		if (currentSectionName != null && currentSectionName == "HomeFriendDetail")
		{
			return true;
		}
		return false;
	}
}

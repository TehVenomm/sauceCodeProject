using Network;
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
		playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(charaInfo, true, true, true, true);
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
		SetActive(transRoot, UI.SPR_COMMENT, true);
		SetLabelText(transRoot, UI.LBL_COMMENT, charaInfo.comment);
	}

	public override void SetupFollowButton()
	{
		FriendCharaInfo friendCharaInfo = MonoBehaviourSingleton<FriendManager>.I.homeCharas.chara.Find((FriendCharaInfo chara) => chara.userId == charaInfo.userId);
		bool flag = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
		bool flag2 = !friendCharaInfo.following;
		bool follower = friendCharaInfo.follower;
		SetEvent(transRoot, UI.BTN_FOLLOW, "FOLLOW", 0);
		if (flag && flag2)
		{
			SetActive(transRoot, UI.BTN_FOLLOW, true);
			SetActive(transRoot, UI.BTN_UNFOLLOW, false);
			SetEvent(transRoot, UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
		}
		else
		{
			SetActive(transRoot, UI.BTN_FOLLOW, flag2);
			SetActive(transRoot, UI.BTN_UNFOLLOW, !flag2);
		}
		SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, true);
		bool flag3 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(charaInfo.userId);
		SetActive(transRoot, UI.BTN_BLACKLIST_IN, !flag3);
		SetActive(transRoot, UI.BTN_BLACKLIST_OUT, flag3);
		SetActive(transRoot, UI.SPR_FOLLOW_ARROW, !flag3 && !flag2);
		SetActive(transRoot, UI.SPR_FOLLOWER_ARROW, !flag3 && follower);
		SetActive(transRoot, UI.SPR_BLACKLIST_ICON, flag3);
	}

	protected override void SetupLastLogin()
	{
		SetActive(transRoot, UI.OBJ_LAST_LOGIN, true);
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
				MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(charaInfo.userId, true);
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
				MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(charaInfo.userId, false);
			}
		});
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
	}
}

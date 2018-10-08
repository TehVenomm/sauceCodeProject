using Network;
using UnityEngine;

public class FriendArenaRankingInfo : FriendInfo
{
	protected new enum UI
	{
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		SPR_COMMENT,
		LBL_COMMENT,
		OBJ_LAST_LOGIN,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_LEVEL,
		OBJ_LEVEL_ROOT,
		LBL_USER_ID,
		OBJ_USER_ID_ROOT,
		TEX_MODEL,
		BTN_FOLLOW,
		BTN_UNFOLLOW,
		OBJ_BLACKLIST_ROOT,
		BTN_BLACKLIST_IN,
		BTN_BLACKLIST_OUT,
		OBJ_ICON_WEAPON_1,
		OBJ_ICON_WEAPON_2,
		OBJ_ICON_WEAPON_3,
		OBJ_ICON_ARMOR,
		OBJ_ICON_HELM,
		OBJ_ICON_ARM,
		OBJ_ICON_LEG,
		BTN_ICON_WEAPON_1,
		BTN_ICON_WEAPON_2,
		BTN_ICON_WEAPON_3,
		BTN_ICON_ARMOR,
		BTN_ICON_HELM,
		BTN_ICON_ARM,
		BTN_ICON_LEG,
		OBJ_EQUIP_ROOT,
		OBJ_EQUIP_SET_ROOT,
		OBJ_FRIEND_INFO_ROOT,
		OBJ_CHANGE_EQUIP_INFO_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_FOLLOW_ARROW_ROOT,
		SPR_FOLLOW_ARROW,
		SPR_FOLLOWER_ARROW,
		SPR_BLACKLIST_ICON,
		LBL_LEVEL_WEAPON_1,
		LBL_LEVEL_WEAPON_2,
		LBL_LEVEL_WEAPON_3,
		LBL_LEVEL_ARMOR,
		LBL_LEVEL_HELM,
		LBL_LEVEL_ARM,
		LBL_LEVEL_LEG,
		LBL_CHANGE_MODE,
		BTN_MAGI,
		LBL_SET_NAME,
		OBJ_DEGREE_PLATE_ROOT,
		BTN_DELETEFOLLOWER,
		BTN_KICK,
		BTN_JOIN
	}

	private Network.EventData eventData;

	public override void Initialize()
	{
		object[] array = (object[])GameSection.GetEventData();
		friendCharaInfo = (array[0] as FriendCharaInfo);
		data = (array[0] as CharaInfo);
		eventData = (array[1] as Network.EventData);
		if (friendCharaInfo != null)
		{
			dataFollower = friendCharaInfo.follower;
			dataFollowing = friendCharaInfo.following;
		}
		nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		isFollowerList = Object.FindObjectOfType(typeof(FriendFollowerList));
		InitializeBase();
	}

	private void OnQuery_SCORE()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			data.userId
		});
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (data.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			SetActive(UI.BTN_FOLLOW, false);
			SetActive(UI.BTN_UNFOLLOW, false);
			SetActive(UI.OBJ_BLACKLIST_ROOT, false);
			SetActive(UI.OBJ_BLACKLIST_ROOT, false);
			SetActive(UI.BTN_BLACKLIST_IN, false);
			SetActive(UI.BTN_BLACKLIST_OUT, false);
		}
	}
}

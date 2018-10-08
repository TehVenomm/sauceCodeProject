using Network;
using System.Collections.Generic;

public class QuestResultUserCollection
{
	public class ResultUserInfo
	{
		public int userId;

		public List<int> selectDegrees;

		private bool? is_follow_enable;

		public bool IsFollower
		{
			get;
			private set;
		}

		public bool CanSendFollow
		{
			get
			{
				bool? nullable = is_follow_enable;
				return nullable.HasValue && is_follow_enable == true;
			}
		}

		public ResultUserInfo(CharaInfo chara_info)
		{
			userId = chara_info.userId;
			selectDegrees = chara_info.selectedDegrees;
		}

		public void SetFollowEnable(bool is_enable)
		{
			is_follow_enable = is_enable;
		}

		public void SetFollower(bool is_follower)
		{
			IsFollower = is_follower;
		}

		public void SetSelectDegrees(List<int> degrees)
		{
			selectDegrees = degrees;
		}
	}

	private List<ResultUserInfo> list;

	public void Init(PartyModel.Party party)
	{
		list = new List<ResultUserInfo>();
		party.slotInfos.ForEach(delegate(PartyModel.SlotInfo slot)
		{
			if (slot != null && slot.userInfo != null)
			{
				list.Add(new ResultUserInfo(slot.userInfo));
			}
		});
	}

	public void Init(FieldModel.Field field)
	{
		list = new List<ResultUserInfo>();
		field.slotInfos.ForEach(delegate(FieldModel.SlotInfo slot)
		{
			if (slot != null && slot.userInfo != null)
			{
				list.Add(new ResultUserInfo(slot.userInfo));
			}
		});
	}

	public void AddSelf()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			StageObjectManager.CreatePlayerInfo createPlayerInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo();
			AddPlayer(createPlayerInfo.charaInfo);
		}
	}

	public void AddPlayer(CharaInfo chara_info)
	{
		if (chara_info != null)
		{
			if (list == null)
			{
				list = new List<ResultUserInfo>();
			}
			ResultUserInfo userInfo = GetUserInfo(chara_info.userId);
			if (userInfo != null)
			{
				userInfo.userId = chara_info.userId;
			}
			else
			{
				list.Add(new ResultUserInfo(chara_info));
			}
		}
	}

	public void Clear()
	{
		list = null;
	}

	public ResultUserInfo GetUserInfo(int user_id)
	{
		return (list == null) ? null : list.Find((ResultUserInfo u) => u != null && u.userId == user_id);
	}

	public void SetResultFollowInfo(FriendFollowModel.Param follow)
	{
		if (list != null && list.Count > 0 && follow.success != null && follow.success.Count > 0)
		{
			follow.success.ForEach(delegate(int id)
			{
				GetUserInfo(id)?.SetFollowEnable(false);
			});
		}
	}

	public void SetResultUnfollowInfo(FriendUnfollowModel.Param unfollow, int user_id)
	{
		if (list != null && list.Count > 0 && unfollow.success != 0)
		{
			GetUserInfo(user_id)?.SetFollowEnable(true);
		}
	}

	public void SetResultBlacklistInfo(int user_id)
	{
		if (list != null && list.Count > 0)
		{
			GetUserInfo(user_id)?.SetFollowEnable(true);
		}
	}

	public void SetPartyFollowInfo(List<FollowPartyMember> follows)
	{
		if (list != null && list.Count > 0 && follows != null && follows.Count > 0)
		{
			follows.ForEach(delegate(FollowPartyMember f)
			{
				ResultUserInfo userInfo = GetUserInfo(f.userId);
				if (userInfo != null)
				{
					userInfo.SetFollowEnable(!f.following);
					userInfo.SetFollower(f.follower);
					userInfo.SetSelectDegrees(f.selectedDegrees);
				}
			});
		}
	}

	public List<int> GetUserIdList(int my_userid = 0)
	{
		List<int> member_list = new List<int>();
		if (my_userid > 0)
		{
			member_list.Add(my_userid);
		}
		if (list == null)
		{
			return member_list;
		}
		list.ForEach(delegate(ResultUserInfo info)
		{
			if (info != null && (my_userid <= 0 || my_userid != info.userId))
			{
				member_list.Add(info.userId);
			}
		});
		return member_list;
	}
}

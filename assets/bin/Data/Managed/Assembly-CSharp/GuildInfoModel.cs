using Network;
using System;
using System.Collections.Generic;

public class GuildInfoModel : BaseModel
{
	public class GuildInfo
	{
		public ChatChannelInfo chat;

		public List<MemberChatStatus> online = new List<MemberChatStatus>();

		public bool receivable;

		public string askUpdate;

		public bool invitation;

		public int donateCap;

		public int donateMaxCap;

		public GuildModel.Guild guildInfo;

		public bool IsOnline(int user_id)
		{
			foreach (MemberChatStatus item in online)
			{
				if (item.id == user_id)
				{
					return true;
				}
			}
			return false;
		}

		public void OnOnline(int user_id)
		{
			foreach (MemberChatStatus item in online)
			{
				if (item.id == user_id)
				{
					return;
				}
			}
			online.Add(new MemberChatStatus
			{
				id = user_id
			});
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_GUILD_LIST);
		}

		public void OnOffline(int user_id)
		{
			foreach (MemberChatStatus item in online)
			{
				if (item.id == user_id)
				{
					online.Remove(item);
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_GUILD_LIST);
					break;
				}
			}
		}
	}

	[Serializable]
	public class MemberChatStatus
	{
		public int id;
	}

	public static string URL = "clan/ClanHome.go";

	public GuildInfo result = new GuildInfo();
}

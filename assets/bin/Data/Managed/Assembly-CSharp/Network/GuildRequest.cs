using System.Collections.Generic;

namespace Network
{
	public class GuildRequest
	{
		public List<GuildRequestItem> guildRequestItemList;

		public GuildRequest()
		{
			guildRequestItemList = new List<GuildRequestItem>();
		}

		public GuildRequest(List<GuildRequestItem> guildRequestItemList)
		{
			this.guildRequestItemList = guildRequestItemList;
		}
	}
}

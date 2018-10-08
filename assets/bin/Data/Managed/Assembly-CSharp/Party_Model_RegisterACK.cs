using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Party_Model_RegisterACK : Coop_Model_ACK
{
	public class UserInfo
	{
		public int userId;

		public string fieldId;

		public int fieldMapId;

		public string partyId;

		public int questId;

		public DateTime lastExecTime;

		public UserInfo(string data)
		{
			string[] array = data.Split(',');
			if (array.Length >= 6)
			{
				int.TryParse(array[0], out userId);
				fieldId = array[1];
				int.TryParse(array[2], out fieldMapId);
				partyId = array[3];
				int.TryParse(array[4], out questId);
				int.TryParse(array[5], out int result);
				lastExecTime = UTC_TIME.AddSeconds((double)result);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.Append(",cid=");
			stringBuilder.Append(userId);
			stringBuilder.Append(",fid=");
			stringBuilder.Append(fieldId);
			stringBuilder.Append(",fmid=");
			stringBuilder.Append(fieldMapId);
			stringBuilder.Append(",pid=");
			stringBuilder.Append(partyId);
			stringBuilder.Append(",qid=");
			stringBuilder.Append(questId);
			return stringBuilder.ToString();
		}
	}

	public static readonly DateTime UTC_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public List<string> userinfo;

	public Party_Model_RegisterACK()
	{
		base.packetType = PACKET_TYPE.PARTY_REGISTER_ACK;
	}

	public unsafe List<UserInfo> GetConvertUserInfo()
	{
		if (userinfo == null)
		{
			return null;
		}
		List<string> source = userinfo;
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Func<string, UserInfo>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return source.Select<string, UserInfo>(_003C_003Ef__am_0024cache2).ToList();
	}

	public override string ToString()
	{
		string str = base.ToString();
		str = str + ",ack=" + ack;
		str = str + ",positive=" + positive;
		str += ",userInfo=[";
		if (userinfo != null && userinfo.Any())
		{
			userinfo.ForEach(delegate(string x)
			{
				str = str + ",u=" + x;
			});
		}
		str += "]";
		return str;
	}
}

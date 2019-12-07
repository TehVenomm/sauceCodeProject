using System;

namespace Network
{
	[Serializable]
	public class UserInfo
	{
		public int id;

		public string name = "";

		public string comment = "";

		public string lastLogin = "";

		public int isParentPassSet;

		public int isStopperSet;

		public bool isAdvancedUser;

		public bool isAdvancedUserGoogle;

		public bool isAdvancedUserFacebook;

		public bool isSetGooglePassword;

		public string advancedUserMail;

		public string code = "";

		public bool inputInviteFlag;

		public string birthday = "";

		public bool communityFlag;

		public bool codeDispFlag = true;

		public int pushEnable;

		public EndDate editNameAt = new EndDate();

		public ServerConstDefine constDefine = new ServerConstDefine();

		public bool isCharged;

		public bool IsParentPassSet
		{
			get
			{
				return isParentPassSet != 0;
			}
			set
			{
				isParentPassSet = (value ? 1 : 0);
			}
		}

		public bool IsStopperSet
		{
			get
			{
				return isStopperSet != 0;
			}
			set
			{
				isStopperSet = (value ? 1 : 0);
			}
		}

		public bool IsAdvanced
		{
			get
			{
				if (!isAdvancedUser)
				{
					return isAdvancedUserGoogle;
				}
				return true;
			}
		}

		public bool IsModiedName => name != "/colopl_rob";
	}
}

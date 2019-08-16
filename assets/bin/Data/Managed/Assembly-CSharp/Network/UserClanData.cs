namespace Network
{
	public class UserClanData
	{
		public string cId;

		public string name;

		public int stat;

		public int isInBase;

		public int level;

		public int exp;

		public int expNext;

		public int expPrev;

		public bool isMaxLevel;

		public ClanSymbolData sym;

		public bool IsNotRegistered()
		{
			return stat == 0;
		}

		public bool IsRegistered()
		{
			return stat != 0;
		}

		public bool IsNormalMember()
		{
			return stat == 1;
		}

		public bool IsSubLeader()
		{
			return stat == 2;
		}

		public bool IsLeader()
		{
			return stat == 3;
		}

		public bool IsInBase()
		{
			return isInBase == 1;
		}

		public void Clear()
		{
			cId = "0";
			name = string.Empty;
			stat = 0;
			isInBase = 0;
			level = 1;
		}
	}
}

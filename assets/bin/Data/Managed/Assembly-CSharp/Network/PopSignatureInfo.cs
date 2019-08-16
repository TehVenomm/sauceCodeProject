using System;

namespace Network
{
	[Serializable]
	public class PopSignatureInfo
	{
		public int popKeyId;

		public string signature;

		public int enemyId;

		public int enemyLv;

		public int enemyPopType;
	}
}

using System;

namespace Network
{
	[Serializable]
	public class EquipItemCollection
	{
		public string category;

		public string bit;

		public long Bit => long.Parse(bit);

		public bool CheckBit(int flag)
		{
			if (flag < 0 || flag >= 64)
			{
				return false;
			}
			return (Bit & (1L << flag)) != 0;
		}
	}
}

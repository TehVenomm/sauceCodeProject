using System;
using System.Collections.Generic;

public class FieldDropModel : BaseModel
{
	public class RequestSendForm
	{
		[Serializable]
		public class EnemySignatureInfo
		{
			public int defeatKeyId;

			public string signature;

			public int sid;

			public int exp;

			public int money;

			public int ppt;
		}

		[Serializable]
		public class DropSignatureInfo
		{
			[Serializable]
			public class DropData
			{
				public int dropId;

				public int type;

				public int itemId;

				public int num;

				public int param_0;
			}

			[Serializable]
			public class DeliverData
			{
				public int bit;

				public int boostBit;

				public int boostNum;

				public bool isTreasure;
			}

			public int rewardKeyId;

			public string signature;

			public List<DropData> drops = new List<DropData>();

			public DeliverData deliver = new DeliverData();
		}

		public string fieldId;

		public int mapId;

		public List<int> eids = new List<int>();

		public List<EnemySignatureInfo> esigs = new List<EnemySignatureInfo>();

		public List<int> deids = new List<int>();

		public List<DropSignatureInfo> dsigs = new List<DropSignatureInfo>();
	}

	public static string URL = "ajax/field/drop";
}

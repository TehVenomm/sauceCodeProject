using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class DeliveryRewardTable : Singleton<DeliveryRewardTable>, IDataTable
{
	public class DeliveryRewardData
	{
		public class Reward
		{
			public REWARD_TYPE type;

			public uint item_id;

			public int num;

			public int param;
		}

		public uint id;

		public uint rewardIndex;

		public Reward reward;

		public const string NT = "id,type,itemId,num,param_0";

		public static bool cb(CSVReader csv_reader, DeliveryRewardData data, ref uint key1, ref uint key2)
		{
			data.id = key1;
			data.rewardIndex = key2;
			Reward reward = new Reward();
			csv_reader.Pop(ref reward.type);
			csv_reader.Pop(ref reward.item_id);
			csv_reader.Pop(ref reward.num);
			csv_reader.Pop(ref reward.param);
			data.reward = reward;
			return true;
		}

		public static string CBSecondKey(CSVReader csv, int table_data_num)
		{
			return table_data_num.ToString();
		}
	}

	private DoubleUIntKeyTable<DeliveryRewardData> tableData;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntKeyReadCSV<DeliveryRewardData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntSecondKey _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntKeyReadCSV<DeliveryRewardData> _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntSecondKey _003C_003Ef__mg_0024cache3;

	public void CreateTable(string csv_text)
	{
		tableData = TableUtility.CreateDoubleUIntKeyTable<DeliveryRewardData>(csv_text, DeliveryRewardData.cb, "id,type,itemId,num,param_0", DeliveryRewardData.CBSecondKey);
		tableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(tableData, csv_text, DeliveryRewardData.cb, "id,type,itemId,num,param_0", DeliveryRewardData.CBSecondKey);
	}

	public DeliveryRewardData[] GetDeliveryRewardTableData(uint id)
	{
		if (tableData == null)
		{
			return null;
		}
		UIntKeyTable<DeliveryRewardData> uIntKeyTable = tableData.Get(id);
		if (uIntKeyTable == null)
		{
			return null;
		}
		List<DeliveryRewardData> list = new List<DeliveryRewardData>();
		uIntKeyTable.ForEach(delegate(DeliveryRewardData data)
		{
			list.Add(data);
		});
		if (list.Count == 0)
		{
			return null;
		}
		list.Sort((DeliveryRewardData l, DeliveryRewardData r) => (int)(l.rewardIndex - r.rewardIndex));
		return list.ToArray();
	}
}

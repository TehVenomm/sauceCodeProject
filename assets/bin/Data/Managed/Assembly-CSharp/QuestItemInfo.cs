using Network;
using System.Collections.Generic;

public class QuestItemInfo : ItemInfoBase<QuestItem>
{
	public QuestInfoData infoData;

	public List<QuestItem.SellItem> sellItems;

	public List<float> remainTimes;

	public QuestItemInfo()
	{
	}

	public QuestItemInfo(QuestItem recv_data)
	{
		SetValue(recv_data);
	}

	public override void SetValue(QuestItem recv_data)
	{
		base.uniqueID = ulong.Parse(recv_data.uniqId);
		base.tableID = (uint)recv_data.questId;
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(base.tableID);
		int[] mission_clear_status = null;
		ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == base.tableID);
		if (clearStatusQuest != null)
		{
			mission_clear_status = clearStatusQuest.missionStatus.ToArray();
		}
		infoData = new QuestInfoData(questData, recv_data.reward, recv_data.num, 0, mission_clear_status);
		sellItems = recv_data.sellItems;
		remainTimes = recv_data.remainTimes;
	}

	public static InventoryList<QuestItemInfo, QuestItem> CreateList(List<QuestItem> recv_list)
	{
		InventoryList<QuestItemInfo, QuestItem> list = new InventoryList<QuestItemInfo, QuestItem>();
		recv_list.ForEach(delegate(QuestItem o)
		{
			if (o.num > 0)
			{
				list.Add(o);
			}
		});
		return list;
	}
}

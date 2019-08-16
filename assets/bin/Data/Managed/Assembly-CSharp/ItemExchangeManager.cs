using Network;
using System;
using System.Collections.Generic;

public class ItemExchangeManager : MonoBehaviourSingleton<ItemExchangeManager>
{
	public EXCHANGE_TYPE exchangeType
	{
		get;
		private set;
	}

	public void SetExchangeType(EXCHANGE_TYPE value)
	{
		exchangeType = value;
	}

	public bool IsExchangeScene()
	{
		return MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("ItemStorageTop") || MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("ItemStorageSell");
	}

	public void SendInventorySellItem(List<string> uids, List<int> nums, Action<bool> call_back)
	{
		InventorySellItemModel.RequestSendForm requestSendForm = new InventorySellItemModel.RequestSendForm();
		requestSendForm.uids = uids;
		requestSendForm.nums = nums;
		Protocol.Send(InventorySellItemModel.URL, requestSendForm, delegate(InventorySellItemModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInventorySellEquipItem(List<string> uids, Action<bool> call_back)
	{
		InventorySellEquipModel.RequestSendForm requestSendForm = new InventorySellEquipModel.RequestSendForm();
		requestSendForm.uids = uids;
		Protocol.Send(InventorySellEquipModel.URL, requestSendForm, delegate(InventorySellEquipModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInventorySellSkillItem(List<string> uids, Action<bool> call_back)
	{
		InventorySellSkillModel.RequestSendForm requestSendForm = new InventorySellSkillModel.RequestSendForm();
		requestSendForm.uids = uids;
		bool is_attach = false;
		requestSendForm.uids.ForEach(delegate(string str_uniq_id)
		{
			ulong uniq_id = ulong.Parse(str_uniq_id);
			SkillItemInfo skillItem = MonoBehaviourSingleton<InventoryManager>.I.GetSkillItem(uniq_id);
			if (skillItem != null && (skillItem.isAttached || skillItem.isUniqueAttached))
			{
				is_attach = true;
			}
		});
		Protocol.Send(InventorySellSkillModel.URL, requestSendForm, delegate(InventorySellSkillModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (is_attach)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
				}
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendSellQuest(List<string> uids, List<int> nums, Action<bool, SellQuestItemReward, List<uint>> call_back)
	{
		InventorySellQuestModel.RequestSendForm requestSendForm = new InventorySellQuestModel.RequestSendForm();
		requestSendForm.uids = uids;
		requestSendForm.nums = nums;
		Protocol.Send(InventorySellQuestModel.URL, requestSendForm, delegate(InventorySellQuestModel ret)
		{
			bool arg = false;
			List<uint> is_nothing_remains_quest = new List<uint>();
			if (ret.Error == Error.None)
			{
				arg = true;
				List<QuestData> orderQuestList = MonoBehaviourSingleton<QuestManager>.I.orderQuestList;
				if (orderQuestList != null && orderQuestList.Count > 0)
				{
					List<QuestData> new_list = new List<QuestData>();
					orderQuestList.ForEach(delegate(QuestData o)
					{
						LinkedListNode<QuestItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetFirstNode();
						while (true)
						{
							if (linkedListNode == null)
							{
								return;
							}
							if (linkedListNode.Value.infoData.questData.tableData.questID == o.questId && linkedListNode.Value.infoData.questData.num > 0)
							{
								break;
							}
							linkedListNode = linkedListNode.Next;
						}
						new_list.Add(o);
						o.order.num = linkedListNode.Value.infoData.questData.num;
					});
					orderQuestList = new_list;
				}
				MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo data)
				{
					if (data.infoData.questData.num == 0)
					{
						is_nothing_remains_quest.Add(data.infoData.questData.tableData.questID);
					}
				});
			}
			call_back(arg, ret.result.reward, is_nothing_remains_quest);
		}, string.Empty);
	}

	public void SendInventorySellAbilityItem(List<string> uids, Action<bool> call_back)
	{
		InventoryAbilityItemSellModel.RequestSendForm requestSendForm = new InventoryAbilityItemSellModel.RequestSendForm();
		requestSendForm.uids = uids;
		bool is_attach = false;
		requestSendForm.uids.ForEach(delegate(string str_uniq_id)
		{
			ulong uniq_id = ulong.Parse(str_uniq_id);
			AbilityItemInfo abilityItem = MonoBehaviourSingleton<InventoryManager>.I.GetAbilityItem(uniq_id);
			if (abilityItem != null && abilityItem.equipUniqueId != 0)
			{
				is_attach = true;
			}
		});
		Protocol.Send(InventoryAbilityItemSellModel.URL, requestSendForm, delegate(InventoryAbilityItemSellModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (is_attach)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_CHANGE);
				}
			}
			call_back(obj);
		}, string.Empty);
	}
}

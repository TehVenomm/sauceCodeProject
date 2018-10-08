using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCMessageTable : Singleton<NPCMessageTable>, IDataTable
{
	public class Message
	{
		public NPC_MESSAGE_TYPE type;

		public int param;

		public int priority;

		public int npc;

		public string animationStateName;

		public Vector3 pos;

		public Vector3 rot;

		public string message;

		public int voice_id;

		public bool has_voice => voice_id > 0;

		public string GetReplaceText()
		{
			string text = message;
			text = text.Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
			switch (type)
			{
			case NPC_MESSAGE_TYPE.EVENT_QUEST:
			case NPC_MESSAGE_TYPE.ORDER_QUEST:
			case NPC_MESSAGE_TYPE.DELIVERY_QUEST:
			case NPC_MESSAGE_TYPE.NEW_NORMAL_QUEST:
			case NPC_MESSAGE_TYPE.NEW_ORDER_QUEST:
			case NPC_MESSAGE_TYPE.NEW_EVENT_QUEST:
			case NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST:
			{
				string newValue = string.Empty;
				if (param != 0)
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)param);
					if (questData != null)
					{
						newValue = questData.questText;
					}
				}
				text = text.Replace("{QUEST_NAME}", newValue);
				break;
			}
			case NPC_MESSAGE_TYPE.ITEM_HAVE:
			case NPC_MESSAGE_TYPE.ITEM_NOT_HAVE:
				if (param != 0)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)param);
					text = text.Replace("{ITEM_NAME}", (itemData == null) ? string.Empty : itemData.name);
				}
				break;
			}
			return text;
		}

		public bool IsEnable()
		{
			switch (type)
			{
			case NPC_MESSAGE_TYPE.NONE:
				return false;
			case NPC_MESSAGE_TYPE.ORDER_QUEST:
			{
				if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() == 0)
				{
					return false;
				}
				bool any_have = false;
				MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo quest_item)
				{
					if (!any_have && quest_item.infoData.questData.num > 0)
					{
						any_have = true;
					}
				});
				if (!any_have)
				{
					return false;
				}
				break;
			}
			case NPC_MESSAGE_TYPE.NEW_ORDER_QUEST:
			{
				if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() == 0)
				{
					return false;
				}
				bool find_not_clear_quest = false;
				if (param == 0)
				{
					MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo quest_item)
					{
						if (!find_not_clear_quest && quest_item.infoData.questData.num != 0)
						{
							int num2 = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.FindIndex((ClearStatusQuest clear_data) => clear_data.questId == quest_item.tableID && clear_data.questStatus >= 3);
							if (num2 == -1)
							{
								find_not_clear_quest = true;
							}
						}
					});
					if (!find_not_clear_quest)
					{
						return false;
					}
				}
				else
				{
					if (MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)param) == null)
					{
						return false;
					}
					int num = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.FindIndex((ClearStatusQuest clear_data) => clear_data.questId == param && clear_data.questStatus >= 3);
					if (num != -1)
					{
						return false;
					}
				}
				break;
			}
			case NPC_MESSAGE_TYPE.DELIVERY_QUEST:
			case NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST:
			{
				if (type == NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST && !GameSaveData.instance.IsRecommendedDeliveryCheck())
				{
					return false;
				}
				bool find_clear_status = false;
				bool find_not_clear = false;
				MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery.ForEach(delegate(ClearStatusDelivery data)
				{
					if (!find_clear_status)
					{
						if (param != 0)
						{
							if (data.deliveryId == param)
							{
								find_clear_status = true;
								if (data.deliveryStatus == 0)
								{
									find_not_clear = true;
								}
							}
						}
						else if (data.deliveryStatus == 0)
						{
							find_clear_status = true;
							find_not_clear = true;
						}
					}
				});
				Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
				Delivery delivery = null;
				if (deliveryList.Length > 0 && param != 0)
				{
					delivery = Array.Find(deliveryList, (Delivery data) => data.dId == param);
				}
				if (type == NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST)
				{
					if (param != 0 && find_clear_status)
					{
						return false;
					}
					if (param == 0 && deliveryList.Length <= MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery.Count)
					{
						return false;
					}
				}
				else
				{
					if (find_clear_status)
					{
						return find_not_clear;
					}
					if (param == 0)
					{
						return deliveryList.Length > 0;
					}
					if (delivery == null)
					{
						return false;
					}
				}
				break;
			}
			case NPC_MESSAGE_TYPE.ATTACK_OVER:
			case NPC_MESSAGE_TYPE.ATTACK_UNDER:
				MonoBehaviourSingleton<StatusManager>.I.CalcSelfStatusParam(MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo), out int _atk, out int _, out int _);
				if ((type == NPC_MESSAGE_TYPE.ATTACK_OVER && param > _atk) || (type == NPC_MESSAGE_TYPE.ATTACK_UNDER && param < _atk))
				{
					return false;
				}
				break;
			case NPC_MESSAGE_TYPE.CRYSTAL_OVER:
			case NPC_MESSAGE_TYPE.CRYSTAL_UNDER:
			{
				int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
				if ((type == NPC_MESSAGE_TYPE.CRYSTAL_OVER && param > crystal) || (type == NPC_MESSAGE_TYPE.CRYSTAL_UNDER && param < crystal))
				{
					return false;
				}
				break;
			}
			case NPC_MESSAGE_TYPE.MONEY_OVER:
			case NPC_MESSAGE_TYPE.MONEY_UNDER:
			{
				int money = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money;
				if ((type == NPC_MESSAGE_TYPE.MONEY_OVER && param >= money) || (type == NPC_MESSAGE_TYPE.MONEY_UNDER && param < money))
				{
					return false;
				}
				break;
			}
			case NPC_MESSAGE_TYPE.ITEM_HAVE:
			case NPC_MESSAGE_TYPE.ITEM_NOT_HAVE:
			{
				int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint)param);
				if ((type == NPC_MESSAGE_TYPE.ITEM_HAVE && haveingItemNum < 1) || (type == NPC_MESSAGE_TYPE.ITEM_NOT_HAVE && haveingItemNum > 0))
				{
					return false;
				}
				break;
			}
			case NPC_MESSAGE_TYPE.LEVEL_UP:
			{
				bool flag = GameSaveData.instance.lvupMessageFlag == 1;
				if (GameSaveData.instance.lvupMessageFlag != 0)
				{
					GameSaveData.instance.lvupMessageFlag = 0;
					GameSaveData.Save();
				}
				if (!flag)
				{
					return false;
				}
				break;
			}
			case NPC_MESSAGE_TYPE.TUTORIAL:
				if (TutorialStep.HasAllTutorialCompleted())
				{
					return false;
				}
				break;
			}
			return true;
		}
	}

	public class Section
	{
		public string name;

		public List<Message> messages = new List<Message>();

		public Message GetNPCMessage()
		{
			Message ret = null;
			int select_priority = 0;
			List<Message> priority = new List<Message>();
			List<Message> non_priority = new List<Message>();
			int total = 0;
			messages.ForEach(delegate(Message data)
			{
				if (ret != null && data.priority >= 100)
				{
					if (GetSelectPriority(data.type) > select_priority)
					{
						ret = data;
						select_priority = GetSelectPriority(data.type);
					}
				}
				else if (data.IsEnable())
				{
					if (data.priority > 0)
					{
						total += data.priority;
						if (data.priority >= 100)
						{
							ret = data;
							select_priority = GetSelectPriority(data.type);
						}
						else
						{
							priority.Add(data);
						}
					}
					else
					{
						non_priority.Add(data);
					}
				}
			});
			if (ret != null)
			{
				return ret;
			}
			int rnd = Random.Range(0, Mathf.Max(100, total));
			int index = -1;
			int cnt = 0;
			total = 0;
			priority.ForEach(delegate(Message data)
			{
				if (index <= -1)
				{
					total += data.priority;
					if (total > rnd)
					{
						index = cnt;
					}
					cnt++;
				}
			});
			if (index >= 0)
			{
				return priority[index];
			}
			return non_priority[Random.Range(0, non_priority.Count)];
		}

		private int GetSelectPriority(NPC_MESSAGE_TYPE type)
		{
			switch (type)
			{
			default:
				return 0;
			case NPC_MESSAGE_TYPE.DELIVERY_QUEST:
				return 1;
			case NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST:
				return 2;
			}
		}
	}

	private List<Section> sections;

	public void CreateTable(string csv_text)
	{
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		sections = new List<Section>();
		CSVReader cSVReader = new CSVReader(csv_text, "section,type,prm,priority,npcid,anim,posX,posY,posZ,rotX,rotY,rotZ,jp,voiceId", false);
		Section section = null;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			NPC_MESSAGE_TYPE value2 = NPC_MESSAGE_TYPE.NONE;
			int value3 = 0;
			int value4 = 0;
			int value5 = 0;
			string value6 = string.Empty;
			string value7 = string.Empty;
			float value8 = 0f;
			float value9 = 0f;
			float value10 = 0f;
			float value11 = 0f;
			float value12 = 0f;
			float value13 = 0f;
			int value14 = 0;
			cSVReader.Pop(ref value);
			cSVReader.Pop(ref value2);
			cSVReader.Pop(ref value3);
			cSVReader.Pop(ref value4);
			cSVReader.Pop(ref value5);
			cSVReader.Pop(ref value6);
			cSVReader.Pop(ref value8);
			cSVReader.Pop(ref value9);
			cSVReader.Pop(ref value10);
			cSVReader.Pop(ref value11);
			cSVReader.Pop(ref value12);
			cSVReader.Pop(ref value13);
			cSVReader.Pop(ref value7);
			cSVReader.Pop(ref value14);
			if (value.Length > 0)
			{
				if (section != null)
				{
					sections.Add(section);
				}
				section = new Section();
				section.name = value;
			}
			if (value2 != 0)
			{
				Message message = new Message();
				message.type = value2;
				message.param = value3;
				message.priority = value4;
				message.npc = value5;
				message.animationStateName = value6;
				message.pos = new Vector3(value8, value9, value10);
				message.rot = new Vector3(value11, value12, value13);
				message.message = value7;
				message.voice_id = value14;
				section.messages.Add(message);
			}
		}
		if (section != null)
		{
			sections.Add(section);
		}
		sections.TrimExcess();
	}

	public Section GetSection(string section_name)
	{
		return sections.Find((Section o) => o.name == section_name);
	}

	public string GetNPCMessageBySectionData(GameSceneTables.SectionData sectionData)
	{
		Section section = GetSection(sectionData.sectionName + "_TEXT");
		if (section != null)
		{
			return section.GetNPCMessage()?.message;
		}
		return sectionData.GetText("NPC_MESSAGE_" + Random.Range(0, 3));
	}
}

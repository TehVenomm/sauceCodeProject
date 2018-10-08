using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailSingleSellConfirm : GameSection
{
	private enum UI
	{
		LBL_ITEM_NAME,
		LBL_TOTAL,
		OBJ_ICON_ROOT
	}

	private SortCompareData item;

	private int num;

	private int price;

	private ItemDetailEquip.CURRENT_SECTION? callSection;

	private int? setNo;

	private bool is_exchange;

	public override string overrideBackKeyEvent => "NO";

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		item = (array[0] as SortCompareData);
		num = (int)array[1];
		price = (int)array[2];
		callSection = ((!(array[3] is ItemDetailEquip.CURRENT_SECTION)) ? null : (array[3] as ItemDetailEquip.CURRENT_SECTION?));
		setNo = ((!(array[4] is int)) ? null : ((int?)array[4]));
		is_exchange = false;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_ITEM_NAME, item.GetName());
		SetLabelText((Enum)UI.LBL_TOTAL, $"{price:N0}");
		int enemy_icon_id = 0;
		int enemy_icon_id2 = 0;
		if (item is ItemSortData)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item.GetTableID());
			if (itemData != null)
			{
				enemy_icon_id = itemData.enemyIconID;
				enemy_icon_id2 = itemData.enemyIconID2;
			}
		}
		GET_TYPE getType = item.GetGetType();
		ItemIcon itemIcon = ItemIcon.Create(item.GetIconType(), item.GetIconID(), item.GetRarity(), GetCtrl(UI.OBJ_ICON_ROOT), item.GetIconElement(), item.GetIconMagiEnableType(), num, null, -1, false, -1, false, null, false, enemy_icon_id, enemy_icon_id2, false, getType);
		itemIcon.SetRewardBG(true);
		base.UpdateUI();
	}

	private void sellConfirm(Action<bool> callback)
	{
		int? nullable = setNo;
		if (nullable.HasValue)
		{
			ItemDetailEquip.CURRENT_SECTION? nullable2 = callSection;
			if (nullable2.HasValue)
			{
				ItemDetailEquip.CURRENT_SECTION? nullable3 = callSection;
				switch (nullable3.Value)
				{
				case ItemDetailEquip.CURRENT_SECTION.STATUS_TOP:
				case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP:
				case ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR:
				{
					int? nullable4 = setNo;
					int value = nullable4.Value;
					MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(value, delegate(bool is_success)
					{
						if (callback != null)
						{
							callback(is_success);
						}
					});
					break;
				}
				default:
					if (callback != null)
					{
						callback(true);
					}
					break;
				}
				return;
			}
		}
		object[] obj = new object[4]
		{
			"data = null : setNo =null? ",
			null,
			null,
			null
		};
		int? nullable5 = setNo;
		obj[1] = !nullable5.HasValue;
		obj[2] = " : callsection=null? ";
		ItemDetailEquip.CURRENT_SECTION? nullable6 = callSection;
		obj[3] = !nullable6.HasValue;
		Debug.LogWarning((object)string.Concat(obj));
		callback(false);
	}

	public void OnQuery_YES()
	{
		if (num >= item.GetNum())
		{
			GameSection.ChangeEvent("CLOSE_DETAIL", null);
		}
		if (item is ItemSortData)
		{
			GameSection.StayEvent();
			SendItem(delegate(bool b)
			{
				GameSection.ResumeEvent(b, null);
			});
		}
		else if (item is EquipItemSortData)
		{
			GameSection.StayEvent();
			sellConfirm(delegate(bool b)
			{
				if (!b)
				{
					Debug.LogWarning((object)"sellConfirm = false");
					GameSection.ResumeEvent(false, null);
				}
				else
				{
					GameSection.ChangeStayEvent("NON_STACK_SELL", null);
					SendEquip(new List<string>
					{
						item.GetUniqID().ToString()
					}, delegate(bool is_success)
					{
						GameSection.ResumeEvent(is_success, null);
					});
				}
			});
		}
		else if (item is SkillItemSortData)
		{
			GameSection.ChangeEvent("NON_STACK_SELL", null);
			List<string> list = new List<string>();
			list.Add(item.GetUniqID().ToString());
			GameSection.StayEvent();
			SendSkill(list, delegate(bool b)
			{
				GameSection.ResumeEvent(b, null);
			});
		}
		else if (item is AbilityItemSortData)
		{
			GameSection.StayEvent();
			List<string> list2 = new List<string>();
			list2.Add(item.GetUniqID().ToString());
			SendAbilityItem(list2, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}

	private void SendItem(Action<bool> callback)
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		list.Add(item.GetUniqID().ToString());
		list2.Add(num);
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellItem(list, list2, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	private void SendEquip(List<string> uniqs, Action<bool> callback)
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellEquipItem(uniqs, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	private void SendSkill(List<string> uniqs, Action<bool> callback)
	{
		if (is_exchange)
		{
			GameSection.StopEvent();
			GameSection.ResumeEvent(false, null);
		}
		else
		{
			MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellSkillItem(uniqs, delegate(bool is_success)
			{
				if (callback != null)
				{
					callback(is_success);
				}
			});
		}
	}

	private void SendAbilityItem(List<string> uniqs, Action<bool> callback)
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellAbilityItem(uniqs, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}
}

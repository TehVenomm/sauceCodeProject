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
		int num = 0;
		int num2 = 0;
		if (item is ItemSortData)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item.GetTableID());
			if (itemData != null)
			{
				num = itemData.enemyIconID;
				num2 = itemData.enemyIconID2;
			}
		}
		ITEM_ICON_TYPE iconType = item.GetIconType();
		int iconID = item.GetIconID();
		RARITY_TYPE? rarity = item.GetRarity();
		Transform ctrl = GetCtrl(UI.OBJ_ICON_ROOT);
		ELEMENT_TYPE iconElement = item.GetIconElement();
		EQUIPMENT_TYPE? iconMagiEnableType = item.GetIconMagiEnableType();
		int num3 = this.num;
		string event_name = null;
		int event_data = -1;
		bool is_new = false;
		int toggle_group = -1;
		bool is_select = false;
		string icon_under_text = null;
		bool is_equipping = false;
		int enemy_icon_id = num;
		int enemy_icon_id2 = num2;
		GET_TYPE getType = item.GetGetType();
		ItemIcon itemIcon = ItemIcon.Create(iconType, iconID, rarity, ctrl, iconElement, iconMagiEnableType, num3, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType);
		itemIcon.SetRewardBG(is_visible: true);
		base.UpdateUI();
	}

	private void sellConfirm(Action<bool> callback)
	{
		int? num = setNo;
		if (num.HasValue)
		{
			ItemDetailEquip.CURRENT_SECTION? cURRENT_SECTION = callSection;
			if (cURRENT_SECTION.HasValue)
			{
				ItemDetailEquip.CURRENT_SECTION? cURRENT_SECTION2 = callSection;
				switch (cURRENT_SECTION2.Value)
				{
				case ItemDetailEquip.CURRENT_SECTION.STATUS_TOP:
				case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP:
				case ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR:
				{
					int? num2 = setNo;
					int value = num2.Value;
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
						callback(obj: true);
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
		int? num3 = setNo;
		obj[1] = !num3.HasValue;
		obj[2] = " : callsection=null? ";
		ItemDetailEquip.CURRENT_SECTION? cURRENT_SECTION3 = callSection;
		obj[3] = !cURRENT_SECTION3.HasValue;
		Debug.LogWarning((object)string.Concat(obj));
		callback(obj: false);
	}

	public void OnQuery_YES()
	{
		if (GameDefine.IsRequiredAlertByRarity(item.GetRarity()))
		{
			GameSection.SetEventData(new object[1]
			{
				item.GetRarity().ToString()
			});
			GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM");
		}
		else
		{
			PrepareForSellItem();
		}
	}

	protected void PrepareForSellItem()
	{
		if (num >= item.GetNum())
		{
			GameSection.ChangeEvent("CLOSE_DETAIL");
		}
		if (item is ItemSortData)
		{
			GameSection.StayEvent();
			SendItem(delegate(bool b)
			{
				GameSection.ResumeEvent(b);
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
					GameSection.ResumeEvent(is_resume: false);
				}
				else
				{
					GameSection.ChangeStayEvent("NON_STACK_SELL");
					SendEquip(new List<string>
					{
						item.GetUniqID().ToString()
					}, delegate(bool is_success)
					{
						GameSection.ResumeEvent(is_success);
					});
				}
			});
		}
		else if (item is SkillItemSortData)
		{
			GameSection.ChangeEvent("NON_STACK_SELL");
			List<string> list = new List<string>();
			list.Add(item.GetUniqID().ToString());
			GameSection.StayEvent();
			SendSkill(list, delegate(bool b)
			{
				GameSection.ResumeEvent(b);
			});
		}
		else if (item is AbilityItemSortData)
		{
			GameSection.StayEvent();
			List<string> list2 = new List<string>();
			list2.Add(item.GetUniqID().ToString());
			SendAbilityItem(list2, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success);
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
			GameSection.ResumeEvent(is_resume: false);
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

	public void OnQuery_ItemDetailConfirmSellHighRareItem_YES()
	{
		PrepareForSellItem();
	}

	public void OnQuery_ItemDetailConfirmSellHighRareItem_NO()
	{
	}
}

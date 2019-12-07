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
		callSection = ((array[3] is ItemDetailEquip.CURRENT_SECTION) ? (array[3] as ItemDetailEquip.CURRENT_SECTION?) : null);
		setNo = ((array[4] is int) ? ((int?)array[4]) : null);
		is_exchange = false;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_ITEM_NAME, item.GetName());
		SetLabelText(UI.LBL_TOTAL, $"{price:N0}");
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
		ItemIcon.Create(item.GetIconType(), item.GetIconID(), item.GetRarity(), GetCtrl(UI.OBJ_ICON_ROOT), item.GetIconElement(), item.GetIconMagiEnableType(), num, null, -1, is_new: false, -1, is_select: false, null, is_equipping: false, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, item.GetGetType()).SetRewardBG(is_visible: true);
		base.UpdateUI();
	}

	private void sellConfirm(Action<bool> callback)
	{
		if (!setNo.HasValue || !callSection.HasValue)
		{
			Debug.LogWarning("data = null : setNo =null? " + (!setNo.HasValue).ToString() + " : callsection=null? " + (!callSection.HasValue).ToString());
			callback(obj: false);
			return;
		}
		ItemDetailEquip.CURRENT_SECTION value = callSection.Value;
		if (value == ItemDetailEquip.CURRENT_SECTION.STATUS_TOP || value == ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP || value == ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR)
		{
			int value2 = setNo.Value;
			MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(value2, delegate(bool is_success)
			{
				if (callback != null)
				{
					callback(is_success);
				}
			});
		}
		else if (callback != null)
		{
			callback(obj: true);
		}
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
					Debug.LogWarning("sellConfirm = false");
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

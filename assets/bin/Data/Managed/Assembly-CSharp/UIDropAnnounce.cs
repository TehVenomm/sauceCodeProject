using System.Collections.Generic;
using UnityEngine;

public class UIDropAnnounce : MonoBehaviourSingleton<UIDropAnnounce>
{
	public enum COLOR
	{
		NORMAL,
		RARE,
		DELIVERY,
		MAGI_AT,
		MAGI_SU,
		MAGI_HE,
		MAGI_PA,
		LOUNGE,
		SP_N,
		SP_HN,
		SP_R,
		HALLOWEEN,
		ESP_N,
		ESP_HN,
		ESP_R,
		SEASONAL,
		MAX
	}

	public class DropAnnounceInfo
	{
		public string text;

		public COLOR color;

		public static DropAnnounceInfo CreateAccessoryItemInfo(uint id, int num, out bool is_rare)
		{
			is_rare = false;
			if (Singleton<AccessoryTable>.IsValid())
			{
				return null;
			}
			AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(id);
			if (data == null)
			{
				return null;
			}
			DropAnnounceInfo dropAnnounceInfo = new DropAnnounceInfo();
			dropAnnounceInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2004u, data.name, num);
			if (GameDefine.IsRare(data.rarity))
			{
				dropAnnounceInfo.color = COLOR.RARE;
				is_rare = true;
			}
			else
			{
				dropAnnounceInfo.color = COLOR.NORMAL;
			}
			return dropAnnounceInfo;
		}

		public static DropAnnounceInfo CreateSkillItemInfo(uint id, int num, out bool is_rare)
		{
			is_rare = false;
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(id);
			if (skillItemData == null)
			{
				return null;
			}
			DropAnnounceInfo dropAnnounceInfo = new DropAnnounceInfo();
			dropAnnounceInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2002u, skillItemData.name, num);
			switch (skillItemData.type)
			{
			case SKILL_SLOT_TYPE.ATTACK:
				dropAnnounceInfo.color = COLOR.MAGI_AT;
				break;
			case SKILL_SLOT_TYPE.HEAL:
				dropAnnounceInfo.color = COLOR.MAGI_HE;
				break;
			case SKILL_SLOT_TYPE.SUPPORT:
				dropAnnounceInfo.color = COLOR.MAGI_SU;
				break;
			default:
				dropAnnounceInfo.color = COLOR.MAGI_PA;
				break;
			}
			is_rare = true;
			return dropAnnounceInfo;
		}

		public static DropAnnounceInfo CreateEquipItemInfo(uint id, int num, out bool is_rare)
		{
			is_rare = false;
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(id);
			if (equipItemData == null)
			{
				return null;
			}
			DropAnnounceInfo dropAnnounceInfo = new DropAnnounceInfo();
			dropAnnounceInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2003u, equipItemData.name, num);
			if (!GameDefine.IsRare(equipItemData.rarity))
			{
				dropAnnounceInfo.color = COLOR.NORMAL;
			}
			else
			{
				dropAnnounceInfo.color = COLOR.RARE;
				is_rare = true;
			}
			return dropAnnounceInfo;
		}

		public static DropAnnounceInfo CreateItemInfo(uint id, int num, out bool is_rare)
		{
			is_rare = false;
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(id);
			if (itemData == null)
			{
				return null;
			}
			DropAnnounceInfo dropAnnounceInfo = new DropAnnounceInfo();
			int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(id);
			haveingItemNum = Mathf.Min(haveingItemNum, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.ITEM_NUM_MAX);
			dropAnnounceInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2000u, itemData.name, num, haveingItemNum);
			if (!GameDefine.IsRare(itemData.rarity))
			{
				dropAnnounceInfo.color = COLOR.NORMAL;
			}
			else
			{
				dropAnnounceInfo.color = COLOR.RARE;
				is_rare = true;
			}
			return dropAnnounceInfo;
		}
	}

	[SerializeField]
	protected GameObject announceItem;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected float announceItemSize;

	[SerializeField]
	protected int announceMax = 1;

	[SerializeField]
	protected Color[] announceColor = new Color[8]
	{
		new Color(1f, 1f, 1f),
		new Color(1f, 0.5f, 0f),
		new Color(1f, 1f, 0f),
		new Color(1f, 0f, 0f),
		new Color(0f, 1f, 0f),
		new Color(0f, 0f, 1f),
		new Color(0.8f, 1f, 0f),
		new Color(0.5f, 0.9f, 0.5f)
	};

	private List<UIDropAnnounceItem> announceItems = new List<UIDropAnnounceItem>();

	private List<UIDropAnnounceItem> announceDispItems = new List<UIDropAnnounceItem>();

	private List<DropAnnounceInfo> announceQueue = new List<DropAnnounceInfo>();

	protected override void OnDisable()
	{
		base.OnDisable();
		announceQueue.Clear();
		int i = 0;
		for (int count = announceDispItems.Count; i < count; i++)
		{
			if ((Object)panelChange != (Object)null)
			{
				panelChange.Lock();
			}
			announceDispItems[i].gameObject.SetActive(false);
		}
		announceDispItems.Clear();
	}

	public void Announce(DropAnnounceInfo info)
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (announceDispItems.Count == announceMax)
			{
				announceQueue.Add(info);
			}
			else
			{
				UIDropAnnounceItem uIDropAnnounceItem = null;
				int i = 0;
				for (int count = announceItems.Count; i < count; i++)
				{
					if (!announceItems[i].gameObject.activeSelf)
					{
						uIDropAnnounceItem = announceItems[i];
						break;
					}
				}
				if ((Object)uIDropAnnounceItem == (Object)null)
				{
					GameObject gameObject = ResourceUtility.Instantiate(announceItem);
					gameObject.transform.parent = base.gameObject.transform;
					gameObject.transform.localScale = Vector3.one;
					uIDropAnnounceItem = gameObject.GetComponent<UIDropAnnounceItem>();
					announceItems.Add(uIDropAnnounceItem);
				}
				if ((Object)panelChange != (Object)null)
				{
					panelChange.UnLock();
				}
				uIDropAnnounceItem.StartAnnounce(info.text, announceColor[(int)info.color], announceDispItems.Count > 0, OnEnd);
				Vector3 zero = Vector3.zero;
				zero.y = (0f - announceItemSize) * (float)announceDispItems.Count;
				uIDropAnnounceItem.transform.localPosition = zero;
				announceDispItems.Add(uIDropAnnounceItem);
			}
		}
	}

	protected void OnEnd(UIDropAnnounceItem item)
	{
		announceDispItems.Remove(item);
		int i = 0;
		for (int count = announceDispItems.Count; i < count; i++)
		{
			announceDispItems[i].MovePos(i != 0, new Vector3(0f, (0f - announceItemSize) * (float)i, 0f), 0.1f);
		}
		if ((Object)panelChange != (Object)null)
		{
			panelChange.Lock();
		}
		if (announceQueue.Count > 0)
		{
			Announce(announceQueue[0]);
			announceQueue.RemoveAt(0);
		}
	}
}

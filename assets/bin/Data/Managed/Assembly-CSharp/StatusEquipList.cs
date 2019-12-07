using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEquipList : GameSection
{
	protected enum UI
	{
		BTN_SORT,
		BTN_EQUIP_LIST_L,
		BTN_EQUIP_LIST_R,
		LBL_CURRENT_NUM,
		LBL_MAX_NUM,
		LBL_PAGE_NOW,
		LBL_PAGE_MAX,
		GRD_INVENTORY,
		SPR_SELECT_WEAPON,
		SPR_SELECT_DEF,
		OBJ_CAPTION_2,
		LBL_CAPTION,
		BTN_ENEMY_COLLECTION_LIST
	}

	private int obtainedNum;

	private int currentPageIndex;

	private static readonly int ONE_PAGE_EQUIP_NUM = 25;

	public override void Initialize()
	{
		currentPageIndex = 0;
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		Singleton<EquipItemTable>.I.CreateTableForEquipList();
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		obtainedNum = MonoBehaviourSingleton<AchievementManager>.I.GetEquipItemCollectionNum();
		SetLabelText(UI.LBL_CURRENT_NUM, obtainedNum.ToString());
		int equipListCount = Singleton<EquipItemTable>.I.GetEquipListCount();
		SetLabelText(UI.LBL_MAX_NUM, equipListCount.ToString());
		int page_num = equipListCount;
		SetPageNumText(UI.LBL_PAGE_MAX, page_num);
		SetActive(UI.BTN_ENEMY_COLLECTION_LIST, is_visible: true);
		InitializeCaption();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateUI(0);
		base.UpdateUI();
	}

	public int GetMaxPageNum()
	{
		return Singleton<EquipItemTable>.I.GetEquipListCount() / ONE_PAGE_EQUIP_NUM + ((Singleton<EquipItemTable>.I.GetEquipListCount() % ONE_PAGE_EQUIP_NUM != 0) ? 1 : 0);
	}

	public bool UpdateUI(int pageIndex)
	{
		int maxPageNum = GetMaxPageNum();
		if (maxPageNum <= pageIndex)
		{
			return false;
		}
		currentPageIndex = pageIndex;
		SkipTween(UI.SPR_SELECT_WEAPON);
		SkipTween(UI.SPR_SELECT_DEF);
		SetPageNumText(UI.LBL_PAGE_MAX, maxPageNum);
		UpdateInventory();
		UpdateAnchors();
		return true;
	}

	protected void OnQuery_EQUIP_LIST_L()
	{
		currentPageIndex--;
		if (currentPageIndex < 0)
		{
			int num = currentPageIndex = GetMaxPageNum() - 1;
		}
		UpdateInventory();
	}

	protected void OnQuery_EQUIP_LIST_R()
	{
		currentPageIndex++;
		int num = GetMaxPageNum() - 1;
		if (currentPageIndex > num)
		{
			currentPageIndex = 0;
		}
		UpdateInventory();
	}

	private void UpdateInventory()
	{
		EquipItemTable.EquipItemData[] items = null;
		int start = currentPageIndex * ONE_PAGE_EQUIP_NUM;
		int last = start + ONE_PAGE_EQUIP_NUM;
		items = GetEquips(start, last);
		if (items != null)
		{
			SetPageNumText(UI.LBL_PAGE_NOW, currentPageIndex + 1);
			SetDynamicList(UI.GRD_INVENTORY, "", items.Length, reset: false, null, null, delegate(int i, Transform t, bool isRecycle)
			{
				SetActive(t, is_visible: true);
				EquipItemTable.EquipItemData equipItemData = items[i];
				EquipItemSortData equipItemSortData = new EquipItemSortData();
				EquipItemInfo equipItemInfo = new EquipItemInfo();
				equipItemInfo.tableData = equipItemData;
				equipItemInfo.SetDefaultData();
				equipItemSortData.SetItem(equipItemInfo);
				ITEM_ICON_TYPE iconType = ItemIcon.GetItemIconType(equipItemData.type);
				bool num = !MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(equipItemData);
				if (num)
				{
					iconType = ITEM_ICON_TYPE.UNKNOWN;
				}
				bool isNew = false;
				GET_TYPE getType = GET_TYPE.PAY;
				if (equipItemData != null)
				{
					getType = equipItemData.getType;
				}
				ItemIcon itemIcon = ItemIconDetailSmall.CreateSmallListItemIcon(iconType, equipItemSortData, t, isNew, start + i + 1, getType);
				if (!num)
				{
					itemIcon.button.enabled = true;
					SetEvent(itemIcon._transform, "DETAIL", new object[2]
					{
						ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST,
						equipItemData
					});
				}
				else
				{
					itemIcon.button.enabled = false;
					SetEvent(itemIcon._transform, string.Empty, 0);
				}
			});
		}
	}

	private EquipItemTable.EquipItemData[] GetEquips(int start, int last)
	{
		if (last == 0)
		{
			return new EquipItemTable.EquipItemData[0];
		}
		int equipListCount = Singleton<EquipItemTable>.I.GetEquipListCount();
		if (equipListCount <= last)
		{
			last = equipListCount;
		}
		int num = last - start;
		List<EquipItemTable.EquipItemData> list = new List<EquipItemTable.EquipItemData>();
		for (int i = 0; i < num; i++)
		{
			list.Add(Singleton<EquipItemTable>.I.GetEquipListData(start + i));
		}
		return list.ToArray();
	}

	private void InitializeCaption()
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_2);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
		UITweenCtrl component = ctrl.gameObject.GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play();
		}
	}
}

using System;
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		currentPageIndex = 0;
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		Singleton<EquipItemTable>.I.CreateTableForEquipList();
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		obtainedNum = MonoBehaviourSingleton<AchievementManager>.I.GetEquipItemCollectionNum();
		SetLabelText((Enum)UI.LBL_CURRENT_NUM, obtainedNum.ToString());
		int totalEquipNum = Singleton<EquipItemTable>.I.GetEquipListCount();
		SetLabelText((Enum)UI.LBL_MAX_NUM, totalEquipNum.ToString());
		SetPageNumText(page_num: totalEquipNum, enum_lbl: UI.LBL_PAGE_MAX);
		SetActive((Enum)UI.BTN_ENEMY_COLLECTION_LIST, true);
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
		SkipTween((Enum)UI.SPR_SELECT_WEAPON, true, 0);
		SkipTween((Enum)UI.SPR_SELECT_DEF, true, 0);
		SetPageNumText((Enum)UI.LBL_PAGE_MAX, maxPageNum);
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

	private unsafe void UpdateInventory()
	{
		EquipItemTable.EquipItemData[] items = null;
		int start = currentPageIndex * ONE_PAGE_EQUIP_NUM;
		int last = start + ONE_PAGE_EQUIP_NUM;
		items = GetEquips(start, last);
		if (items != null)
		{
			SetPageNumText((Enum)UI.LBL_PAGE_NOW, currentPageIndex + 1);
			_003CUpdateInventory_003Ec__AnonStorey481 _003CUpdateInventory_003Ec__AnonStorey;
			SetDynamicList((Enum)UI.GRD_INVENTORY, string.Empty, items.Length, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateInventory_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_2);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play(true, null);
		}
	}
}

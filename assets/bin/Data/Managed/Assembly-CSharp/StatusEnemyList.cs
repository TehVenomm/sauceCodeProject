using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Obsolete]
public class StatusEnemyList : GameSection
{
	protected enum UI
	{
		BTN_SORT,
		BTN_ENEMY_COLLECTION,
		BTN_EQUIP_LIST_L,
		BTN_EQUIP_LIST_R,
		LBL_CURRENT_NUM,
		LBL_MAX_NUM,
		LBL_TARGET_FIELD,
		LBL_PAGE_NOW,
		LBL_PAGE_MAX,
		POP_TARGET_FIELD,
		GRD_NORMAL_INVENTORY,
		GRD_BIG_INVENTORY,
		GRD_RARE_INVENTORY,
		GRD_FIELD_INVENTORY,
		OBJ_COMMON_FRAME,
		OBJ_RARE_FRAME,
		OBJ_BIGRARE_FRAME,
		OBJ_FIELD_FRAME,
		OBJ_UNKNOWN,
		STR_MONSTER,
		STR_BIGMONSTER,
		STR_RAREMONSTER,
		STR_FIELDMONSTER,
		TEX_ICON,
		OBJ_CAPTION_2,
		LBL_CAPTION
	}

	private UI[] rarityTable = new UI[4]
	{
		UI.OBJ_COMMON_FRAME,
		UI.OBJ_RARE_FRAME,
		UI.OBJ_BIGRARE_FRAME,
		UI.OBJ_FIELD_FRAME
	};

	private List<EnemyCollectionTable.EnemyCollectionData> currentRegionCollectionItems;

	private List<EnemyCollectionTable.EnemyCollectionData> regionCollectionSortItems;

	private List<RegionTable.Data> unlockRegion;

	private List<AchievementCounter> achievementCounter;

	private int currentPageIndex;

	private static readonly int ONE_PAGE_EQUIP_NUM = 5;

	private Transform popup;

	private int popupIndex;

	private List<string> fields;

	public override void Initialize()
	{
		currentPageIndex = 0;
		popupIndex = 0;
		achievementCounter = MonoBehaviourSingleton<AchievementManager>.I.monsterCollectionList;
		SetText((Enum)UI.STR_MONSTER, "NORMAL");
		SetText((Enum)UI.STR_BIGMONSTER, "HAPPEN");
		SetText((Enum)UI.STR_RAREMONSTER, "HAPPEN_RARE");
		SetText((Enum)UI.STR_FIELDMONSTER, "FIELD_CHANGE");
		uint[] openRegionIdList = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdList();
		unlockRegion = (from x in openRegionIdList
		select Singleton<RegionTable>.I.GetData(x)).ToList();
		fields = (from x in unlockRegion
		select x.regionName).ToList();
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
		return Singleton<EquipItemTable>.I.GetEquipListCount() / ONE_PAGE_EQUIP_NUM + 1;
	}

	public bool UpdateUI(int pageIndex)
	{
		int maxPageNum = GetMaxPageNum();
		if (maxPageNum <= pageIndex)
		{
			return false;
		}
		currentPageIndex = pageIndex;
		SetPageNumText((Enum)UI.LBL_PAGE_MAX, maxPageNum);
		SetLabelText((Enum)UI.LBL_TARGET_FIELD, fields[popupIndex]);
		UpdateRegion();
		UpdateInventory();
		UpdateAnchors();
		return true;
	}

	protected void OnQuery_LIST_L()
	{
		currentPageIndex--;
		UpdateInventory();
	}

	protected void OnQuery_LIST_R()
	{
		currentPageIndex++;
		UpdateInventory();
	}

	private void OnQuery_FIELD_LIST()
	{
		ShowLevelPopup();
	}

	private void UpdateRegion()
	{
		uint regionId = unlockRegion.First((RegionTable.Data x) => x.regionName == fields[popupIndex]).regionId;
		currentRegionCollectionItems = (from data in Singleton<EnemyCollectionTable>.I.GetEnemyCollectionDataByRegion(regionId)
		orderby data.id
		select data).ToList();
		int num = 0;
		using (List<EnemyCollectionTable.EnemyCollectionData>.Enumerator enumerator = currentRegionCollectionItems.GetEnumerator())
		{
			EnemyCollectionTable.EnemyCollectionData item;
			while (enumerator.MoveNext())
			{
				item = enumerator.Current;
				if (achievementCounter.Find((AchievementCounter x) => x.subType == item.id) != null)
				{
					num++;
				}
			}
		}
		SetLabelText((Enum)UI.LBL_CURRENT_NUM, $"{num}/{currentRegionCollectionItems.Count}");
	}

	private void UpdateInventory()
	{
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable = from x in currentRegionCollectionItems
		where x.collectionType == COLLECTION_TYPE.NORMAL
		select x;
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable2 = from x in currentRegionCollectionItems
		where x.collectionType == COLLECTION_TYPE.HAPPEN
		select x;
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable3 = from x in currentRegionCollectionItems
		where x.collectionType == COLLECTION_TYPE.HAPPEN_RARE
		select x;
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable4 = from x in currentRegionCollectionItems
		where x.collectionType == COLLECTION_TYPE.FIELD_CHANGE
		select x;
		regionCollectionSortItems = enumerable.Concat(enumerable2).Concat(enumerable3).Concat(enumerable4)
			.ToList();
		int num = Mathf.Max(enumerable.Count(), enumerable2.Count());
		num = Mathf.Max(num, enumerable3.Count());
		num = Mathf.Max(num, enumerable4.Count());
		int num2 = Mathf.CeilToInt((float)num / (float)ONE_PAGE_EQUIP_NUM);
		SetPageNumText((Enum)UI.LBL_PAGE_MAX, num2);
		if (currentPageIndex < 0)
		{
			currentPageIndex = num2 - 1;
		}
		if (currentPageIndex >= num2)
		{
			currentPageIndex = 0;
		}
		int start = currentPageIndex * ONE_PAGE_EQUIP_NUM;
		if (currentRegionCollectionItems != null && currentRegionCollectionItems.Count != 0)
		{
			SetPageNumText((Enum)UI.LBL_PAGE_NOW, currentPageIndex + 1);
			CreateIcon(enumerable, UI.GRD_NORMAL_INVENTORY, start);
			CreateIcon(enumerable2, UI.GRD_BIG_INVENTORY, start);
			CreateIcon(enumerable3, UI.GRD_RARE_INVENTORY, start);
			CreateIcon(enumerable4, UI.GRD_FIELD_INVENTORY, start);
		}
	}

	private void CreateIcon(IEnumerable<EnemyCollectionTable.EnemyCollectionData> items, UI targetType, int start)
	{
		if (items.Count() > start)
		{
			List<EnemyCollectionTable.EnemyCollectionData> indexItems = items.Skip(start).ToList();
			int num = Mathf.Min(indexItems.Count, ONE_PAGE_EQUIP_NUM);
			if (num > 0)
			{
				SetActive((Enum)targetType, true);
				SetDynamicList((Enum)targetType, "EnemyCollectionIcon", num, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool isRecycle)
				{
					SetActive(t, true);
					bool flag = achievementCounter.Find((AchievementCounter x) => x.subType == indexItems[i].id) == null;
					SetActive(t, UI.OBJ_UNKNOWN, flag);
					SetActive(t, UI.TEX_ICON, !flag);
					SetFrame(t, (int)(targetType - 10));
					if (!flag)
					{
						EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(indexItems[i].id).FirstOrDefault();
						SetEnemyIcon(t, UI.TEX_ICON, enemyData.iconId);
					}
					object[] event_data = new object[2]
					{
						indexItems[i].id,
						regionCollectionSortItems
					};
					SetEvent(t, "DETAIL", event_data);
				});
			}
			else
			{
				SetActive((Enum)targetType, false);
			}
		}
		else
		{
			SetActive((Enum)targetType, false);
		}
	}

	private void SetFrame(Transform iconRoot, int rarity)
	{
		rarity = Mathf.Clamp(rarity, 0, rarityTable.Length - 1);
		for (int i = 0; i < rarityTable.Length; i++)
		{
			SetActive(iconRoot, rarityTable[i], rarity == i);
		}
	}

	private void ShowLevelPopup()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (popup == null)
		{
			popup = GetCtrl(UI.POP_TARGET_FIELD).GetComponentInChildren<UIScrollablePopupList>(true).get_transform();
		}
		if (!(popup == null))
		{
			popup.get_gameObject().SetActive(true);
			bool[] array = new bool[fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			UIScrollablePopupList.CreatePopup(popup, GetCtrl(UI.POP_TARGET_FIELD), 4, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, fields.ToArray(), array, popupIndex, delegate(int index)
			{
				popupIndex = index;
				UpdateRegion();
				RefreshUI();
			});
		}
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

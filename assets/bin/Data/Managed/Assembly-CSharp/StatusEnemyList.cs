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

	public unsafe override void Initialize()
	{
		currentPageIndex = 0;
		popupIndex = 0;
		achievementCounter = MonoBehaviourSingleton<AchievementManager>.I.monsterCollectionList;
		SetText((Enum)UI.STR_MONSTER, "NORMAL");
		SetText((Enum)UI.STR_BIGMONSTER, "HAPPEN");
		SetText((Enum)UI.STR_RAREMONSTER, "HAPPEN_RARE");
		SetText((Enum)UI.STR_FIELDMONSTER, "FIELD_CHANGE");
		uint[] openRegionIdList = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdList();
		uint[] source = openRegionIdList;
		if (_003C_003Ef__am_0024cacheA == null)
		{
			_003C_003Ef__am_0024cacheA = new Func<uint, RegionTable.Data>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		unlockRegion = source.Select<uint, RegionTable.Data>(_003C_003Ef__am_0024cacheA).ToList();
		List<RegionTable.Data> source2 = unlockRegion;
		if (_003C_003Ef__am_0024cacheB == null)
		{
			_003C_003Ef__am_0024cacheB = new Func<RegionTable.Data, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		fields = source2.Select<RegionTable.Data, string>(_003C_003Ef__am_0024cacheB).ToList();
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

	private unsafe void UpdateRegion()
	{
		uint regionId = unlockRegion.First(new Func<RegionTable.Data, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).regionId;
		List<EnemyCollectionTable.EnemyCollectionData> enemyCollectionDataByRegion = Singleton<EnemyCollectionTable>.I.GetEnemyCollectionDataByRegion(regionId);
		if (_003C_003Ef__am_0024cacheC == null)
		{
			_003C_003Ef__am_0024cacheC = new Func<EnemyCollectionTable.EnemyCollectionData, uint>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		currentRegionCollectionItems = enemyCollectionDataByRegion.OrderBy<EnemyCollectionTable.EnemyCollectionData, uint>(_003C_003Ef__am_0024cacheC).ToList();
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

	private unsafe void UpdateInventory()
	{
		List<EnemyCollectionTable.EnemyCollectionData> source = currentRegionCollectionItems;
		if (_003C_003Ef__am_0024cacheD == null)
		{
			_003C_003Ef__am_0024cacheD = new Func<EnemyCollectionTable.EnemyCollectionData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable = source.Where(_003C_003Ef__am_0024cacheD);
		List<EnemyCollectionTable.EnemyCollectionData> source2 = currentRegionCollectionItems;
		if (_003C_003Ef__am_0024cacheE == null)
		{
			_003C_003Ef__am_0024cacheE = new Func<EnemyCollectionTable.EnemyCollectionData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable2 = source2.Where(_003C_003Ef__am_0024cacheE);
		List<EnemyCollectionTable.EnemyCollectionData> source3 = currentRegionCollectionItems;
		if (_003C_003Ef__am_0024cacheF == null)
		{
			_003C_003Ef__am_0024cacheF = new Func<EnemyCollectionTable.EnemyCollectionData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable3 = source3.Where(_003C_003Ef__am_0024cacheF);
		List<EnemyCollectionTable.EnemyCollectionData> source4 = currentRegionCollectionItems;
		if (_003C_003Ef__am_0024cache10 == null)
		{
			_003C_003Ef__am_0024cache10 = new Func<EnemyCollectionTable.EnemyCollectionData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<EnemyCollectionTable.EnemyCollectionData> enumerable4 = source4.Where(_003C_003Ef__am_0024cache10);
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

	private unsafe void CreateIcon(IEnumerable<EnemyCollectionTable.EnemyCollectionData> items, UI targetType, int start)
	{
		if (items.Count() > start)
		{
			List<EnemyCollectionTable.EnemyCollectionData> indexItems = items.Skip(start).ToList();
			int num = Mathf.Min(indexItems.Count, ONE_PAGE_EQUIP_NUM);
			if (num > 0)
			{
				SetActive((Enum)targetType, true);
				_003CCreateIcon_003Ec__AnonStorey49D _003CCreateIcon_003Ec__AnonStorey49D;
				SetDynamicList((Enum)targetType, "EnemyCollectionIcon", num, false, null, null, new Action<int, Transform, bool>((object)_003CCreateIcon_003Ec__AnonStorey49D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

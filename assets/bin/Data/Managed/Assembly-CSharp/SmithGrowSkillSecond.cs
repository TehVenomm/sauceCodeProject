using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithGrowSkillSecond : ItemDetailSkill
{
	protected new enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		OBJ_LV_EX,
		LBL_LV_EX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_SELL,
		LBL_DESCRIPTION,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_SUB_STATUS,
		SPR_SKILL_TYPE_ICON,
		SPR_SKILL_TYPE_ICON_BG,
		SPR_SKILL_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_DESCRIPTION,
		STR_TITLE_STATUS,
		STR_TITLE_SELL,
		PRG_EXP_BAR,
		OBJ_NEXT_EXP_ROOT,
		LBL_EQUIP_ITEM_NAME,
		GRD_MATERIAL,
		BTN_DECISION_ON,
		BTN_DECISION_OFF,
		BTN_BACK,
		OBJ_GOLD,
		LBL_GOLD,
		LBL_SELECT_NUM,
		STR_SELL,
		STR_TITLE_MATERIAL,
		STR_TITLE_MONEY,
		LBL_BASE_DESCRIPTION,
		LBL_NEXT_DESCRIPTION,
		SPR_STATUS_UP,
		SPR_BG_NORMAL,
		SPR_BG_EXCEED,
		OBJ_CAPTION,
		LBL_CAPTION
	}

	public int MATERIAL_SELECT_MAX = 10;

	private SkillItemInfo skillItem;

	private SkillItemInfo[] material;

	private int needGold;

	private Color goldColor = Color.get_white();

	private bool isNoticeSendGrow;

	private bool isExceed;

	private bool isSortTypeReset;

	public override void Initialize()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		skillItem = (array[0] as SkillItemInfo);
		material = (array[1] as SkillItemInfo[]);
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
			skillItem
		});
		UILabel component = base.GetComponent<UILabel>((Enum)UI.LBL_GOLD);
		if (component != null)
		{
			goldColor = component.color;
		}
		UITweenCtrl component2 = GetCtrl(UI.OBJ_CAPTION).get_gameObject().GetComponent<UITweenCtrl>();
		if (component2 != null)
		{
			component2.Reset();
			int i = 0;
			for (int num = component2.tweens.Length; i < num; i++)
			{
				component2.tweens[i].ResetToBeginning();
			}
			component2.Play(true, null);
		}
		isExceed = skillItem.IsLevelMax();
		isSortTypeReset = isExceed;
		base.Initialize();
	}

	protected override void OnOpen()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array != null && array.Length > 1)
		{
			SkillItemInfo[] array2 = array[1] as SkillItemInfo[];
			if (array2 != null)
			{
				material = array2;
			}
		}
		isNoticeSendGrow = false;
		base.OnOpen();
	}

	public unsafe override void UpdateUI()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		isExceed = skillItem.IsLevelMax();
		MATERIAL_SELECT_MAX = ((!isExceed) ? 10 : 10);
		SetFontStyle((Enum)UI.STR_TITLE_MATERIAL, 2);
		SetFontStyle((Enum)UI.STR_TITLE_MONEY, 2);
		if (detailBase != null)
		{
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, false);
		}
		UpdateMaterial();
		Transform ctrl = GetCtrl(UI.GRD_MATERIAL);
		while (ctrl.get_childCount() != 0)
		{
			Transform val = ctrl.GetChild(0);
			val.set_parent(null);
			val.get_gameObject().SetActive(false);
			Object.Destroy(val.get_gameObject());
		}
		int material_num = (material != null) ? material.Length : 0;
		_003CUpdateUI_003Ec__AnonStorey48E _003CUpdateUI_003Ec__AnonStorey48E;
		SetGrid(UI.GRD_MATERIAL, null, MATERIAL_SELECT_MAX, false, new Func<int, Transform, Transform>((object)_003CUpdateUI_003Ec__AnonStorey48E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey48E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		int exceedCnt = skillItem.exceedCnt;
		SetActive((Enum)UI.OBJ_LV_EX, exceedCnt > 0);
		if (exceedCnt > 0)
		{
			SetLabelText((Enum)UI.LBL_LV_EX, exceedCnt.ToString());
		}
		if (material != null && material.Length > 0)
		{
			SetActive((Enum)UI.BTN_DECISION_ON, true);
			SetActive((Enum)UI.BTN_DECISION_OFF, false);
		}
		else
		{
			SetActive((Enum)UI.BTN_DECISION_ON, false);
			SetActive((Enum)UI.BTN_DECISION_OFF, true);
		}
		SetLabelText(text: (!isExceed) ? base.sectionData.GetText("CAPTION_GROW") : base.sectionData.GetText("CAPTION_EXCEED"), label_enum: UI.LBL_CAPTION);
		SetActive((Enum)UI.SPR_BG_NORMAL, !isExceed);
		SetActive((Enum)UI.SPR_BG_EXCEED, isExceed);
	}

	public static SkillItemInfo ParamCopy(SkillItemInfo _ref, bool isLevelUp = false, bool isExceedUp = false)
	{
		int lv = isLevelUp ? (_ref.level + 1) : _ref.level;
		int exceed = isExceedUp ? (_ref.exceedCnt + 1) : _ref.exceedCnt;
		SkillItemInfo skillItemInfo = new SkillItemInfo(0, (int)_ref.tableID, lv, exceed);
		skillItemInfo.uniqueID = _ref.uniqueID;
		skillItemInfo.exp = _ref.exp;
		skillItemInfo.expPrev = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(skillItemInfo.tableData.baseNeedExp, skillItemInfo.growData.needExp, false);
		skillItemInfo.expNext = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(skillItemInfo.tableData.baseNeedExp, skillItemInfo.nextGrowData.needExp, false);
		skillItemInfo.exceedExp = _ref.exceedExp;
		skillItemInfo.growCost = _ref.growCost;
		return skillItemInfo;
	}

	private void UpdateMaterial()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		int num = (material != null) ? material.Length : 0;
		SetLabelText((Enum)UI.LBL_SELECT_NUM, (MATERIAL_SELECT_MAX - num).ToString());
		needGold = ((!isExceed) ? ((int)(skillItem.growCost * (float)num)) : 0);
		SetLabelText((Enum)UI.LBL_GOLD, needGold.ToString("N0"));
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < needGold)
		{
			SetColor((Enum)UI.LBL_GOLD, Color.get_red());
		}
		else
		{
			SetColor((Enum)UI.LBL_GOLD, goldColor);
		}
		SetActive((Enum)UI.OBJ_GOLD, !isExceed);
		SkillItemInfo skillItemInfo = ParamCopy(skillItem, false, false);
		SkillItemInfo skillItemInfo2 = ParamCopy(skillItem, false, false);
		if (material != null)
		{
			int i = 0;
			for (int num2 = material.Length; i < num2; i++)
			{
				if (isExceed)
				{
					if (!skillItemInfo2.IsMaxExceed())
					{
						skillItemInfo2.exceedExp += material[i].giveExceedExp;
						while (skillItemInfo2.exceedExpNext <= skillItemInfo2.exceedExp)
						{
							skillItemInfo2 = ParamCopy(skillItemInfo2, false, true);
							if (skillItemInfo2.IsMaxExceed())
							{
								skillItemInfo2.exceedExp = skillItemInfo2.expPrev;
								break;
							}
						}
					}
				}
				else if (!skillItemInfo2.IsLevelMax() && material[i].level <= material[i].GetMaxLevel())
				{
					skillItemInfo2.exp += material[i].giveExp;
					while (skillItemInfo2.expNext <= skillItemInfo2.exp)
					{
						skillItemInfo2 = ParamCopy(skillItemInfo2, true, false);
						if (skillItemInfo2.IsLevelMax())
						{
							skillItemInfo2.exp = skillItemInfo2.expPrev;
							break;
						}
					}
				}
			}
		}
		bool flag = false;
		flag = ((!isExceed) ? (skillItemInfo.level != skillItemInfo2.level) : (skillItemInfo.exceedCnt != skillItemInfo2.exceedCnt));
		SetActive(detailBase, UI.LBL_DESCRIPTION, !flag);
		SetActive((Enum)UI.LBL_BASE_DESCRIPTION, flag);
		SetActive((Enum)UI.LBL_NEXT_DESCRIPTION, flag);
		SetActive((Enum)UI.SPR_STATUS_UP, flag);
		itemData = skillItemInfo2;
		base.UpdateUI();
		SetLabelText(detailBase, UI.LBL_LV_NOW, skillItemInfo2.level.ToString());
		SetLabelText(detailBase, UI.LBL_LV_MAX, skillItemInfo2.GetMaxLevel().ToString());
		SetActive(detailBase, UI.OBJ_LV_EX, skillItemInfo2.IsExceeded());
		SetLabelText(detailBase, UI.LBL_LV_EX, skillItemInfo2.exceedCnt.ToString());
		SetLabelText(detailBase, UI.LBL_ATK, skillItemInfo2.atk.ToString());
		SetLabelText(detailBase, UI.LBL_DEF, skillItemInfo2.def.ToString());
		SetLabelText(detailBase, UI.LBL_HP, skillItemInfo2.hp.ToString());
		SetLabelText(detailBase, UI.LBL_SELL, needGold.ToString());
		SetLabelText(detailBase, UI.STR_SELL, base.sectionData.GetText("STR_SELL"));
		SetSupportEncoding(UI.LBL_DESCRIPTION, true);
		SetSupportEncoding(UI.LBL_BASE_DESCRIPTION, true);
		SetSupportEncoding(UI.LBL_NEXT_DESCRIPTION, true);
		string explanationText = skillItemInfo.GetExplanationText(true);
		SetLabelText(detailBase, UI.LBL_DESCRIPTION, explanationText);
		SetLabelText((Enum)UI.LBL_BASE_DESCRIPTION, explanationText);
		SetLabelText((Enum)UI.LBL_NEXT_DESCRIPTION, skillItemInfo2.GetExplanationStatusUpText(base.sectionData.GetText("STR_STATUS_UP_FORMAT"), isExceed, skillItemInfo.exceedCnt > 0));
		SkillGrowProgress component = FindCtrl(detailBase, UI.PRG_EXP_BAR).GetComponent<SkillGrowProgress>();
		if (isExceed)
		{
			component.SetExceedMode();
			float fill_amount = (float)(skillItem.exceedExp - skillItem.exceedExpPrev) / (float)(skillItem.exceedExpNext - skillItem.exceedExpPrev);
			component.SetBaseGauge(skillItemInfo2.exceedCnt == skillItemInfo.exceedCnt, fill_amount);
			SetProgressInt(detailBase, UI.PRG_EXP_BAR, skillItemInfo2.exceedExp, skillItemInfo2.exceedExpPrev, skillItemInfo2.exceedExpNext, null);
		}
		else
		{
			float fill_amount2 = (float)(skillItem.exp - skillItem.expPrev) / (float)(skillItem.expNext - skillItem.expPrev);
			component.SetGrowMode();
			component.SetBaseGauge(skillItemInfo2.level == skillItemInfo.level, fill_amount2);
			SetProgressInt(detailBase, UI.PRG_EXP_BAR, skillItemInfo2.exp, skillItemInfo2.expPrev, skillItemInfo2.expNext, null);
		}
		UpdateAnchors();
	}

	private bool IsEnableSelect(SortCompareData item)
	{
		if (item == null)
		{
			return false;
		}
		return !item.IsFavorite() && item.GetUniqID() != skillItem.uniqueID;
	}

	private void OnQuery_DECISION()
	{
		if (needGold > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money)
		{
			GameSection.ChangeEvent("NOT_ENOUGH_MONEY", null);
		}
		else if (material == null || material.Length <= 0)
		{
			GameSection.ChangeEvent("NOT_MATERIAL", null);
		}
		else if (skillItem.IsLevelMax() && !skillItem.IsExistNextExceed())
		{
			GameSection.ChangeEvent("NOT_INCLUDE_EXCEED", null);
		}
		else
		{
			isNoticeSendGrow = true;
			GameSection.SetEventData(new object[2]
			{
				skillItem,
				material
			});
		}
	}

	private void OnCloseDialog_SmithGrowSkillConfirm()
	{
		isNoticeSendGrow = false;
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
			material[num]
		});
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & (NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (NOTIFY_FLAG)0L)
		{
			skillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skillItem.uniqueID);
			if (material != null)
			{
				List<SkillItemInfo> list = new List<SkillItemInfo>();
				int i = 0;
				for (int num = material.Length; i < num; i++)
				{
					SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(material[i].uniqueID);
					if (skillItemInfo != null && !skillItemInfo.isFavorite)
					{
						list.Add(material[i]);
					}
				}
				material = list.ToArray();
			}
			SetDirty(UI.GRD_MATERIAL);
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		if (isNoticeSendGrow)
		{
			return (NOTIFY_FLAG)0L;
		}
		return NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
	}

	private void OnQuery_SELECT()
	{
		GameSection.SetEventData(new object[4]
		{
			skillItem,
			material,
			isExceed,
			isSortTypeReset
		});
		isSortTypeReset = false;
	}

	private void OnQuery_SECTION_BACK()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowSkillSelect"))
		{
			GameSection.StopEvent();
			OnQuery_MAIN_MENU_STATUS();
		}
	}
}

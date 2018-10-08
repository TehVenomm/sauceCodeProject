using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipResultBase : SmithEquipBase
{
	private enum UI
	{
		BTN_NEXT,
		BTN_NEXT_GRAY,
		BTN_TO_SELECT,
		BTN_TO_SELECT_CENTER,
		LBL_NEXT_BTN,
		LBL_NEXT_GRAY_BTN,
		LBL_TO_SELECT,
		LBL_TO_SELECT_CENTER,
		LBL_NEXT_BTN_R,
		LBL_NEXT_GRAY_BTN_R,
		LBL_TO_SELECT_R,
		LBL_TO_SELECT_CENTER_R,
		OBJ_ADD_ABILITY,
		LBL_ADD_ABILITY,
		SPR_TITLE_ABILITY,
		SPR_TITLE_EXCEED,
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		STR_LV,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_ELEM,
		LBL_ELEM_DEF,
		SPR_ELEM,
		SPR_ELEM_DEF,
		LBL_AFTER_ATK,
		LBL_AFTER_DEF,
		LBL_AFTER_HP,
		LBL_AFTER_ELEM,
		LBL_AFTER_ELEM_DEF,
		LBL_DIFF_ATK,
		LBL_DIFF_DEF,
		LBL_DIFF_HP,
		LBL_DIFF_ELEM,
		LBL_DIFF_ELEM_DEF,
		SPR_DIFF_ELEM,
		SPR_DIFF_ELEM_DEF,
		LBL_SELL,
		OBJ_SKILL_BUTTON_ROOT,
		BTN_SELL,
		BTN_GROW,
		BTN_GRAY,
		LBL_GRAY_BTN,
		OBJ_FAVORITE_ROOT,
		SPR_FAVORITE,
		SPR_UNFAVORITE,
		SPR_IS_EVOLVE,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		STR_ONLY_VISUAL,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_SELL,
		STR_TITLE_ATK,
		STR_TITLE_ELEM_ATK,
		STR_TITLE_DEF,
		STR_TITLE_ELEM_DEF,
		STR_TITLE_HP,
		TBL_ABILITY,
		STR_NON_ABILITY,
		OBJ_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		OBJ_DELAY,
		OBJ_NEED_UPDATE_ABILITY,
		LBL_NEED_UPDATE_ABILITY,
		SPR_SP_ATTACK_TYPE
	}

	public enum AUDIO
	{
		RESULT = 40000049,
		RESULT_EXCEEED = 40000157
	}

	private static readonly float STATUS_WINDOW_DELAY = 0.5f;

	private AbilityDetailPopUp abilityDetailPopUp;

	private List<Transform> touchAndReleaseButtons = new List<Transform>();

	private int tabIndex;

	protected SmithManager.ResultData resultData;

	private Transform detailBase;

	private int noticeNum;

	private EquipItemAbility[] addAbility;

	private string[] exceedDescriptions;

	public override string overrideBackKeyEvent => "TO_SELECT";

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
	}

	public override void Initialize()
	{
		tabIndex = 0;
		resultData = (SmithManager.ResultData)GameSection.GetEventData();
		type = EquipDialogType.RESULT;
		base.Initialize();
	}

	protected override void OnOpen()
	{
		InitUITweener<UITweener>(UI.OBJ_DELAY, false, null);
		StartCoroutine(DelayedOpenStatus());
		MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
		base.OnOpen();
	}

	private IEnumerator DelayedOpenStatus()
	{
		float t = STATUS_WINDOW_DELAY;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return (object)null;
		}
		GetCtrl(UI.OBJ_DELAY).GetComponent<UITweener>().PlayForward();
		bool is_exceed = false;
		if (resultData != null)
		{
			EquipItemInfo item = resultData.itemData as EquipItemInfo;
			if (resultData.isExceed && item != null && item.exceed > 0)
			{
				is_exceed = true;
			}
		}
		SoundManager.PlayOneShotUISE((!is_exceed) ? 40000049 : 40000157);
	}

	public override void UpdateUI()
	{
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailEquipBase", true);
		SetFontStyle(detailBase, UI.STR_TITLE_ITEM_INFO, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_SKILL_SLOT, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_STATUS, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ABILITY, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_SELL, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ATK, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_ATK, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_DEF, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_DEF, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_HP, FontStyle.Italic);
		base.UpdateUI();
	}

	protected override void ResultEquipInfo()
	{
		if (resultData.itemData != null)
		{
			EquipItemInfo item = resultData.itemData as EquipItemInfo;
			EquipItemTable.EquipItemData tableData = item.tableData;
			bool flag = tableData.IsVisual();
			SetActive(detailBase, UI.BTN_SELL, false);
			SetActive(detailBase, UI.BTN_GROW, false);
			SetActive(detailBase, UI.BTN_GRAY, false);
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, false);
			SetActive(detailBase, UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
			SetActive(detailBase, UI.STR_LV, !flag);
			SetActive(detailBase, UI.STR_ONLY_VISUAL, flag);
			SetupBottomButton();
			SetLabelText(detailBase, UI.LBL_NAME, tableData.name);
			SetLabelText(detailBase, UI.LBL_LV_MAX, tableData.maxLv.ToString());
			SetSprite(detailBase, UI.SPR_SP_ATTACK_TYPE, (!tableData.IsWeapon()) ? string.Empty : tableData.spAttackType.GetBigFrameSpriteName());
			if (smithType == SmithType.GROW)
			{
				string text = base.sectionData.GetText("STATUS_DIFF_FORMAT");
				SetLabelCompareParam(detailBase, UI.LBL_LV_NOW, item.level, resultData.beforeLevel, -1);
				SetLabelDiffParam(detailBase, UI.LBL_AFTER_ATK, item.atk, UI.LBL_DIFF_ATK, resultData.beforeAtk, UI.LBL_ATK, text);
				SetLabelDiffParam(detailBase, UI.LBL_AFTER_DEF, item.def, UI.LBL_DIFF_DEF, resultData.beforeDef, UI.LBL_DEF, text);
				SetLabelDiffParam(detailBase, UI.LBL_AFTER_HP, item.hp, UI.LBL_DIFF_HP, resultData.beforeHp, UI.LBL_HP, text);
				SetLabelDiffParam(detailBase, UI.LBL_AFTER_ELEM, item.elemAtk, UI.LBL_DIFF_ELEM, resultData.beforeElemAtk, UI.LBL_ELEM, text);
				SetDiffElementSprite(detailBase, item.GetElemAtkType(), resultData.beforeElemAtk, item.elemAtk, UI.SPR_ELEM, UI.SPR_DIFF_ELEM, true);
				int num = item.elemDef;
				int num2 = resultData.beforeElemDef;
				if (item.tableData.isFormer)
				{
					num = Mathf.FloorToInt((float)num * 0.1f);
					num2 = Mathf.FloorToInt((float)num2 * 0.1f);
				}
				SetLabelDiffParam(detailBase, UI.LBL_AFTER_ELEM_DEF, num, UI.LBL_DIFF_ELEM_DEF, num2, UI.LBL_ELEM_DEF, text);
				SetDiffElementSprite(detailBase, item.GetElemDefType(), resultData.beforeElemDef, item.elemDef, UI.SPR_ELEM_DEF, UI.SPR_DIFF_ELEM_DEF, false);
			}
			else
			{
				SetLabelText(detailBase, UI.LBL_LV_NOW, item.level.ToString());
				SetLabelText(detailBase, UI.LBL_ATK, item.atk.ToString());
				SetLabelText(detailBase, UI.LBL_DEF, item.def.ToString());
				SetLabelText(detailBase, UI.LBL_HP, item.hp.ToString());
				SetLabelText(detailBase, UI.LBL_ELEM, item.elemAtk.ToString());
				SetElementSprite(detailBase, UI.SPR_ELEM, item.GetElemAtkType());
				int num3 = item.elemDef;
				if (item.tableData.isFormer)
				{
					num3 = Mathf.FloorToInt((float)num3 * 0.1f);
				}
				SetLabelText(detailBase, UI.LBL_ELEM_DEF, num3.ToString());
				SetDefElementSprite(detailBase, UI.SPR_ELEM_DEF, item.GetElemDefType());
			}
			SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", item.tableData, GetSkillSlotData(item), "SKILL_ICON_BUTTON", 0);
			SetLabelText(detailBase, UI.LBL_SELL, tableData.sale.ToString());
			SetEquipmentTypeIcon(detailBase, UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, item.tableData);
			AbilityItemInfo abilityItem = item.GetAbilityItem();
			bool flag2 = abilityItem != null;
			if ((item.ability != null && item.ability.Length > 0) || flag2)
			{
				bool empty_ability = true;
				string allAbilityName = string.Empty;
				string allAp = string.Empty;
				string allAbilityDesc = string.Empty;
				SetTable(detailBase, UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", item.ability.Length + (flag2 ? 1 : 0), false, delegate(int i, Transform t, bool is_recycle)
				{
					if (i < item.ability.Length)
					{
						EquipItemAbility equipItemAbility = item.ability[i];
						if (equipItemAbility.id == 0)
						{
							SetActive(t, false);
						}
						else
						{
							empty_ability = false;
							SetActive(t, true);
							if (equipItemAbility.IsNeedUpdate())
							{
								SetActive(t, UI.OBJ_ABILITY, false);
								SetActive(t, UI.OBJ_FIXEDABILITY, false);
								SetActive(t, UI.OBJ_NEED_UPDATE_ABILITY, true);
								SetButtonEnabled(t, false);
							}
							else if (item.IsFixedAbility(i))
							{
								SetActive(t, UI.OBJ_ABILITY, false);
								SetActive(t, UI.OBJ_FIXEDABILITY, true);
								SetLabelText(t, UI.LBL_FIXEDABILITY, equipItemAbility.GetName());
								SetLabelText(t, UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
							}
							else
							{
								SetLabelText(t, UI.LBL_ABILITY, equipItemAbility.GetName());
								SetLabelText(t, UI.LBL_ABILITY_NUM, equipItemAbility.GetAP());
							}
							SetAbilityItemEvent(t, i, touchAndReleaseButtons);
							allAbilityName += equipItemAbility.GetName();
							allAp += equipItemAbility.GetAP();
							allAbilityDesc += equipItemAbility.GetDescription();
						}
					}
					else
					{
						SetActive(t, UI.OBJ_ABILITY, false);
						SetActive(t, UI.OBJ_ABILITY_ITEM, true);
						SetLabelText(t, UI.LBL_ABILITY_ITEM, abilityItem.GetName());
						SetTouchAndRelease(t.GetComponentInChildren<UIButton>().transform, "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", t);
						allAbilityName += abilityItem.GetName();
						allAbilityDesc += abilityItem.GetDescription();
					}
				});
				PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
				if (empty_ability)
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, true);
				}
				else
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, false);
				}
			}
			else
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, true);
			}
		}
	}

	private void SetDiffElementSprite(Transform t, int elem_type, int before, int after, UI no_diff, UI diff, bool is_weapon)
	{
		bool flag = before != after;
		SetActive(no_diff, !flag);
		SetActive(diff, flag);
		if (is_weapon)
		{
			SetElementSprite(t, (!flag) ? no_diff : diff, elem_type);
		}
		else
		{
			SetDefElementSprite(t, (!flag) ? no_diff : diff, elem_type);
		}
	}

	private void SetupBottomButton()
	{
		switch (smithType)
		{
		case SmithType.EVOLVE:
		{
			SetActive(UI.BTN_NEXT, false);
			SetActive(UI.BTN_NEXT_GRAY, false);
			SetActive(UI.BTN_TO_SELECT, false);
			SetActive(UI.BTN_TO_SELECT_CENTER, true);
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			if (equipItemInfo != null && (!equipItemInfo.IsLevelMax() || !equipItemInfo.IsExceedMax() || equipItemInfo.tableData.IsShadow()))
			{
				SetActive(UI.BTN_NEXT, true);
				SetEvent(UI.BTN_NEXT, "NEXT_GROW_AUTO", 0);
				SetActive(UI.BTN_TO_SELECT, true);
				SetActive(UI.BTN_TO_SELECT_CENTER, false);
				SetLabelText(UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
			}
			break;
		}
		case SmithType.GENERATE:
			SetActive(UI.BTN_NEXT_GRAY, false);
			SetActive(UI.BTN_TO_SELECT, true);
			SetActive(UI.BTN_TO_SELECT_CENTER, false);
			SetLabelText(UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
			break;
		case SmithType.GROW:
		{
			SetActive(UI.BTN_NEXT_GRAY, false);
			SetActive(UI.BTN_TO_SELECT, true);
			SetActive(UI.BTN_TO_SELECT_CENTER, false);
			bool flag = false;
			EquipItemInfo equipItemInfo2 = resultData.itemData as EquipItemInfo;
			if (equipItemInfo2 != null && equipItemInfo2.IsLevelMax())
			{
				if (equipItemInfo2.tableData.IsEvolve())
				{
					SetActive(UI.BTN_NEXT, true);
					SetActive(UI.BTN_NEXT_GRAY, false);
					SetEvent(UI.BTN_NEXT, "NEXT_EVOLVE_AUTO", 0);
					flag = true;
				}
				else if (!equipItemInfo2.IsExceedMax() || equipItemInfo2.tableData.IsShadow())
				{
					SetActive(UI.BTN_NEXT, true);
					SetActive(UI.BTN_NEXT_GRAY, false);
				}
				else
				{
					SetActive(UI.BTN_NEXT, false);
					SetActive(UI.BTN_NEXT_GRAY, true);
				}
			}
			if (flag)
			{
				SetLabelText(UI.LBL_NEXT_BTN, base.sectionData.GetText("NEXT_EVOLVE"));
				SetLabelText(UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("NEXT_EVOLVE"));
			}
			else
			{
				SetLabelText(UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
				SetLabelText(UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("CONTINUE"));
			}
			break;
		}
		case SmithType.SKILL_GROW:
		{
			SetActive(UI.BTN_NEXT_GRAY, false);
			SetActive(UI.BTN_TO_SELECT, true);
			SetActive(UI.BTN_TO_SELECT_CENTER, false);
			SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
			if (skillItemInfo != null && skillItemInfo.IsLevelMax())
			{
				SetActive(UI.BTN_NEXT, false);
				SetActive(UI.BTN_NEXT_GRAY, true);
			}
			SetLabelText(UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
			SetLabelText(UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("CONTINUE"));
			break;
		}
		}
		SetLabelText(UI.LBL_NEXT_BTN_R, GetComponent<UILabel>(UI.LBL_NEXT_BTN).text);
		SetLabelText(UI.LBL_NEXT_GRAY_BTN_R, GetComponent<UILabel>(UI.LBL_NEXT_GRAY_BTN).text);
		SetLabelText(UI.LBL_TO_SELECT_CENTER_R, GetComponent<UILabel>(UI.LBL_TO_SELECT_CENTER).text);
	}

	protected override void EquipImg()
	{
		if (smithType != SmithType.SKILL_GROW)
		{
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			SetRenderEquipModel(UI.TEX_MODEL, equipItemInfo.tableID, -1, -1, 1f);
		}
		else
		{
			SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
			SetRenderSkillItemModel(UI.TEX_MODEL, skillItemInfo.tableID, true, false);
		}
	}

	private void OnQuery_SKILL_ICON_BUTTON()
	{
		if (tabIndex != 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.SMITH_GROW,
				resultData.itemData as EquipItemInfo
			});
		}
	}

	private void OnQuery_ABILITY()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemAbility equipItemAbility = null;
		EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
		if (equipItemInfo != null)
		{
			equipItemAbility = new EquipItemAbility(equipItemInfo.ability[num].id, -1);
		}
		else
		{
			SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
			if (skillItemInfo == null)
			{
				Debug.LogError("err : result data is unknown : atk " + resultData.beforeAtk + " : def " + resultData.beforeDef);
			}
		}
		if (equipItemAbility == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(equipItemAbility);
		}
	}

	private void OnQuery_NEXT_EVOLVE_AUTO()
	{
		EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
		EventData[] autoEvents = new EventData[2]
		{
			new EventData("NEXT_EVOLVE", 1),
			new EventData("TRY_ON", equipItemInfo.uniqueID)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_NEXT_GROW_AUTO()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
		{
			GameSection.ChangeEvent("CONTINUE_GROW", null);
		}
		else
		{
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			EventData[] autoEvents = new EventData[3]
			{
				new EventData("TO_SELECT", 1),
				new EventData("TRY_ON", equipItemInfo.uniqueID),
				new EventData("CLEARLEVEL")
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	protected void StartAddAbilityDirection(EquipItemAbility[] abulity)
	{
		if (abulity != null && abulity.Length != 0)
		{
			addAbility = abulity;
			noticeNum = abulity.Length;
			SetActive(UI.SPR_TITLE_ABILITY, true);
			SetActive(UI.SPR_TITLE_EXCEED, false);
			OnFinishedAddAbilityDirection();
		}
	}

	public void OnFinishedAddAbilityDirection()
	{
		if (noticeNum > 0)
		{
			SetFontStyle(UI.LBL_ADD_ABILITY, FontStyle.Italic);
			SetLabelText(UI.LBL_ADD_ABILITY, addAbility[addAbility.Length - noticeNum].GetNameAndAP());
			noticeNum--;
			SetActive(UI.OBJ_ADD_ABILITY, true);
			ResetTween(UI.OBJ_ADD_ABILITY, 0);
			PlayTween(UI.OBJ_ADD_ABILITY, true, OnFinishedAddAbilityDirection, false, 0);
		}
		else
		{
			SetActive(UI.OBJ_ADD_ABILITY, false);
		}
	}

	protected void StartExceedDirection(string[] descriptions)
	{
		if (descriptions != null && descriptions.Length != 0)
		{
			exceedDescriptions = descriptions;
			noticeNum = descriptions.Length;
			SetActive(UI.SPR_TITLE_ABILITY, false);
			SetActive(UI.SPR_TITLE_EXCEED, true);
			OnFinishedExceedDirection();
		}
	}

	public void OnFinishedExceedDirection()
	{
		if (noticeNum > 0)
		{
			SetFontStyle(UI.LBL_ADD_ABILITY, FontStyle.Italic);
			SetLabelText(UI.LBL_ADD_ABILITY, exceedDescriptions[exceedDescriptions.Length - noticeNum]);
			noticeNum--;
			ResetTween(UI.SPR_TITLE_EXCEED, 0);
			PlayTween(UI.SPR_TITLE_EXCEED, true, null, false, 0);
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			int i = 1;
			for (int num = 4; i <= num; i++)
			{
				ResetTween(UI.SPR_TITLE_EXCEED, i);
				if (i < equipItemInfo.exceed)
				{
					SkipTween(UI.SPR_TITLE_EXCEED, true, i);
				}
				else if (i == equipItemInfo.exceed)
				{
					PlayTween(UI.SPR_TITLE_EXCEED, true, null, false, i);
				}
			}
			SetActive(UI.OBJ_ADD_ABILITY, true);
			ResetTween(UI.OBJ_ADD_ABILITY, 1);
			PlayTween(UI.OBJ_ADD_ABILITY, true, null, false, 1);
		}
		else
		{
			SetActive(UI.OBJ_ADD_ABILITY, false);
		}
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		if (!((Object)abilityDetailPopUp == (Object)null))
		{
			abilityDetailPopUp.Hide();
			GameSection.StopEvent();
		}
	}

	protected void OnQuery_ABILITY_DATA_POPUP()
	{
		object[] array = GameSection.GetEventData() as object[];
		int num = (int)array[0];
		EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
		EquipItemAbility abilityDetailText = equipItemInfo.ability[num];
		Transform targetTrans = array[1] as Transform;
		if ((Object)abilityDetailPopUp == (Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(targetTrans);
		abilityDetailPopUp.SetAbilityDetailText(abilityDetailText);
		GameSection.StopEvent();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & NOTIFY_FLAG.PRETREAT_SCENE) != (NOTIFY_FLAG)0L)
		{
			NoEventReleaseTouchAndReleases(touchAndReleaseButtons);
			OnQuery_RELEASE_ABILITY();
		}
	}

	private void PreCacheAbilityDetail(string name, string ap, string desc)
	{
		if ((Object)abilityDetailPopUp == (Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
	}
}

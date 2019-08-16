using System;
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
		SPR_SP_ATTACK_TYPE,
		BTN_NEXT_RIGHT
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
		InitUITweener<UITweener>((Enum)UI.OBJ_DELAY, is_enable: false, (EventDelegate.Callback)null);
		this.StartCoroutine(DelayedOpenStatus());
		MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
		base.OnOpen();
	}

	private IEnumerator DelayedOpenStatus()
	{
		float t = STATUS_WINDOW_DELAY;
		while (t > 0f)
		{
			t -= Time.get_deltaTime();
			yield return null;
		}
		GetCtrl(UI.OBJ_DELAY).GetComponent<UITweener>().PlayForward();
		bool is_exceed = false;
		if (resultData != null)
		{
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			if (resultData.isExceed && equipItemInfo != null && equipItemInfo.exceed > 0)
			{
				is_exceed = true;
			}
		}
		SoundManager.PlayOneShotUISE((!is_exceed) ? 40000049 : 40000157);
	}

	public override void UpdateUI()
	{
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailEquipBase");
		SetFontStyle(detailBase, UI.STR_TITLE_ITEM_INFO, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_SKILL_SLOT, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_STATUS, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ABILITY, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_SELL, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ATK, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_ATK, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_DEF, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_DEF, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_HP, 2);
		base.UpdateUI();
	}

	protected override void ResultEquipInfo()
	{
		if (resultData.itemData == null)
		{
			return;
		}
		EquipItemInfo item = resultData.itemData as EquipItemInfo;
		EquipItemTable.EquipItemData tableData = item.tableData;
		bool flag = tableData.IsVisual();
		SetActive(detailBase, UI.BTN_SELL, is_visible: false);
		SetActive(detailBase, UI.BTN_GROW, is_visible: false);
		SetActive(detailBase, UI.BTN_GRAY, is_visible: false);
		SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, is_visible: false);
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
			SetLabelCompareParam(detailBase, UI.LBL_LV_NOW, item.level, resultData.beforeLevel);
			SetLabelDiffParam(detailBase, UI.LBL_AFTER_ATK, item.atk, UI.LBL_DIFF_ATK, resultData.beforeAtk, UI.LBL_ATK, text);
			SetLabelDiffParam(detailBase, UI.LBL_AFTER_DEF, item.def, UI.LBL_DIFF_DEF, resultData.beforeDef, UI.LBL_DEF, text);
			SetLabelDiffParam(detailBase, UI.LBL_AFTER_HP, item.hp, UI.LBL_DIFF_HP, resultData.beforeHp, UI.LBL_HP, text);
			SetLabelDiffParam(detailBase, UI.LBL_AFTER_ELEM, item.elemAtk, UI.LBL_DIFF_ELEM, resultData.beforeElemAtk, UI.LBL_ELEM, text);
			SetDiffElementSprite(detailBase, item.GetElemAtkType(), resultData.beforeElemAtk, item.elemAtk, UI.SPR_ELEM, UI.SPR_DIFF_ELEM, is_weapon: true);
			int num = item.elemDef;
			int num2 = resultData.beforeElemDef;
			if (item.tableData.isFormer)
			{
				num = Mathf.FloorToInt((float)num * 0.1f);
				num2 = Mathf.FloorToInt((float)num2 * 0.1f);
			}
			SetLabelDiffParam(detailBase, UI.LBL_AFTER_ELEM_DEF, num, UI.LBL_DIFF_ELEM_DEF, num2, UI.LBL_ELEM_DEF, text);
			SetDiffElementSprite(detailBase, item.GetElemDefType(), resultData.beforeElemDef, item.elemDef, UI.SPR_ELEM_DEF, UI.SPR_DIFF_ELEM_DEF, is_weapon: false);
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
		SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", item.tableData, GetSkillSlotData(item));
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
			SetTable(detailBase, UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", item.ability.Length + (flag2 ? 1 : 0), reset: false, delegate(int i, Transform t, bool is_recycle)
			{
				if (i < item.ability.Length)
				{
					EquipItemAbility equipItemAbility = item.ability[i];
					if (equipItemAbility.id == 0)
					{
						SetActive(t, is_visible: false);
					}
					else
					{
						empty_ability = false;
						SetActive(t, is_visible: true);
						if (equipItemAbility.IsNeedUpdate())
						{
							SetActive(t, UI.OBJ_ABILITY, is_visible: false);
							SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: false);
							SetActive(t, UI.OBJ_NEED_UPDATE_ABILITY, is_visible: true);
							SetButtonEnabled(t, is_enabled: false);
						}
						else if (item.IsFixedAbility(i))
						{
							SetActive(t, UI.OBJ_ABILITY, is_visible: false);
							SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: true);
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
					SetActive(t, UI.OBJ_ABILITY, is_visible: false);
					SetActive(t, UI.OBJ_ABILITY_ITEM, is_visible: true);
					SetLabelText(t, UI.LBL_ABILITY_ITEM, abilityItem.GetName());
					SetTouchAndRelease(t.GetComponentInChildren<UIButton>().get_transform(), "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", t);
					allAbilityName += abilityItem.GetName();
					allAbilityDesc += abilityItem.GetDescription();
				}
			});
			PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
			if (empty_ability)
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: true);
			}
			else
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: false);
			}
		}
		else
		{
			SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: true);
		}
	}

	private void SetDiffElementSprite(Transform t, int elem_type, int before, int after, UI no_diff, UI diff, bool is_weapon)
	{
		bool flag = before != after;
		SetActive((Enum)no_diff, !flag);
		SetActive((Enum)diff, flag);
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
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.BTN_NEXT);
		switch (smithType)
		{
		case SmithType.EVOLVE:
		{
			SetActive((Enum)UI.BTN_NEXT, is_visible: false);
			SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: false);
			SetActive((Enum)UI.BTN_NEXT_RIGHT, is_visible: false);
			SetActive((Enum)UI.BTN_TO_SELECT, is_visible: false);
			SetActive((Enum)UI.BTN_TO_SELECT_CENTER, is_visible: true);
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			if (equipItemInfo != null && (!equipItemInfo.IsLevelMax() || !equipItemInfo.IsExceedMax() || equipItemInfo.tableData.IsShadow()))
			{
				SetActive((Enum)UI.BTN_NEXT, is_visible: true);
				Transform obj2 = ctrl;
				Vector3 localPosition3 = ctrl.get_localPosition();
				float y2 = localPosition3.y;
				Vector3 localPosition4 = ctrl.get_localPosition();
				obj2.set_localPosition(new Vector3(0f, y2, localPosition4.z));
				SetEvent((Enum)UI.BTN_NEXT, "NEXT_GROW_AUTO", 0);
				SetActive((Enum)UI.BTN_TO_SELECT, is_visible: true);
				SetActive((Enum)UI.BTN_TO_SELECT_CENTER, is_visible: false);
				SetLabelText((Enum)UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
			}
			break;
		}
		case SmithType.GENERATE:
		{
			Transform obj3 = ctrl;
			Vector3 localPosition5 = ctrl.get_localPosition();
			float y3 = localPosition5.y;
			Vector3 localPosition6 = ctrl.get_localPosition();
			obj3.set_localPosition(new Vector3(-34f, y3, localPosition6.z));
			SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: false);
			SetActive((Enum)UI.BTN_NEXT_RIGHT, is_visible: true);
			SetEventName((Enum)UI.BTN_NEXT_RIGHT, "TO_GROW");
			SetActive((Enum)UI.BTN_TO_SELECT, is_visible: true);
			SetActive((Enum)UI.BTN_TO_SELECT_CENTER, is_visible: false);
			SetLabelText((Enum)UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
			break;
		}
		case SmithType.GROW:
		{
			Transform obj4 = ctrl;
			Vector3 localPosition7 = ctrl.get_localPosition();
			float y4 = localPosition7.y;
			Vector3 localPosition8 = ctrl.get_localPosition();
			obj4.set_localPosition(new Vector3(0f, y4, localPosition8.z));
			SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: false);
			SetActive((Enum)UI.BTN_NEXT_RIGHT, is_visible: false);
			SetActive((Enum)UI.BTN_TO_SELECT, is_visible: true);
			SetActive((Enum)UI.BTN_TO_SELECT_CENTER, is_visible: false);
			bool flag = false;
			EquipItemInfo equipItemInfo2 = resultData.itemData as EquipItemInfo;
			if (equipItemInfo2 != null && equipItemInfo2.IsLevelMax())
			{
				if (equipItemInfo2.tableData.IsEvolve())
				{
					SetActive((Enum)UI.BTN_NEXT, is_visible: true);
					SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: false);
					SetEvent((Enum)UI.BTN_NEXT, "NEXT_EVOLVE_AUTO", 0);
					flag = true;
				}
				else if (!equipItemInfo2.IsExceedMax() || equipItemInfo2.tableData.IsShadow())
				{
					SetActive((Enum)UI.BTN_NEXT, is_visible: true);
					SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: false);
				}
				else
				{
					SetActive((Enum)UI.BTN_NEXT, is_visible: false);
					SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: true);
				}
			}
			if (flag)
			{
				SetLabelText((Enum)UI.LBL_NEXT_BTN, base.sectionData.GetText("NEXT_EVOLVE"));
				SetLabelText((Enum)UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("NEXT_EVOLVE"));
			}
			else
			{
				SetLabelText((Enum)UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
				SetLabelText((Enum)UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("CONTINUE"));
			}
			break;
		}
		case SmithType.SKILL_GROW:
		{
			Transform obj = ctrl;
			Vector3 localPosition = ctrl.get_localPosition();
			float y = localPosition.y;
			Vector3 localPosition2 = ctrl.get_localPosition();
			obj.set_localPosition(new Vector3(0f, y, localPosition2.z));
			SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: false);
			SetActive((Enum)UI.BTN_NEXT_RIGHT, is_visible: false);
			SetActive((Enum)UI.BTN_TO_SELECT, is_visible: true);
			SetActive((Enum)UI.BTN_TO_SELECT_CENTER, is_visible: false);
			SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
			if (skillItemInfo != null && skillItemInfo.IsLevelMax())
			{
				SetActive((Enum)UI.BTN_NEXT, is_visible: false);
				SetActive((Enum)UI.BTN_NEXT_GRAY, is_visible: true);
			}
			SetLabelText((Enum)UI.LBL_NEXT_BTN, base.sectionData.GetText("CONTINUE"));
			SetLabelText((Enum)UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("CONTINUE"));
			break;
		}
		}
		SetLabelText((Enum)UI.LBL_NEXT_BTN_R, base.GetComponent<UILabel>((Enum)UI.LBL_NEXT_BTN).text);
		SetLabelText((Enum)UI.LBL_NEXT_GRAY_BTN_R, base.GetComponent<UILabel>((Enum)UI.LBL_NEXT_GRAY_BTN).text);
		SetLabelText((Enum)UI.LBL_TO_SELECT_CENTER_R, base.GetComponent<UILabel>((Enum)UI.LBL_TO_SELECT_CENTER).text);
	}

	protected override void EquipImg()
	{
		if (smithType != SmithType.SKILL_GROW)
		{
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			SetRenderEquipModel((Enum)UI.TEX_MODEL, equipItemInfo.tableID, -1, -1, 1f);
		}
		else
		{
			SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
			SetRenderSkillItemModel((Enum)UI.TEX_MODEL, skillItemInfo.tableID, rotation: true, light_rotation: false);
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
				Debug.LogError((object)("err : result data is unknown : atk " + resultData.beforeAtk + " : def " + resultData.beforeDef));
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
			GameSection.ChangeEvent("CONTINUE_GROW");
			return;
		}
		EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
		EventData[] autoEvents = new EventData[3]
		{
			new EventData("TO_SELECT", 1),
			new EventData("TRY_ON", equipItemInfo.uniqueID),
			new EventData("CLEARLEVEL")
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	protected void StartAddAbilityDirection(EquipItemAbility[] abulity)
	{
		if (abulity != null && abulity.Length != 0)
		{
			addAbility = abulity;
			noticeNum = abulity.Length;
			SetActive((Enum)UI.SPR_TITLE_ABILITY, is_visible: true);
			SetActive((Enum)UI.SPR_TITLE_EXCEED, is_visible: false);
			OnFinishedAddAbilityDirection();
		}
	}

	public void OnFinishedAddAbilityDirection()
	{
		if (noticeNum > 0)
		{
			SetFontStyle((Enum)UI.LBL_ADD_ABILITY, 2);
			SetLabelText((Enum)UI.LBL_ADD_ABILITY, addAbility[addAbility.Length - noticeNum].GetNameAndAP());
			noticeNum--;
			SetActive((Enum)UI.OBJ_ADD_ABILITY, is_visible: true);
			ResetTween((Enum)UI.OBJ_ADD_ABILITY, 0);
			PlayTween((Enum)UI.OBJ_ADD_ABILITY, forward: true, (EventDelegate.Callback)OnFinishedAddAbilityDirection, is_input_block: false, 0);
		}
		else
		{
			SetActive((Enum)UI.OBJ_ADD_ABILITY, is_visible: false);
		}
	}

	protected void StartExceedDirection(string[] descriptions)
	{
		if (descriptions != null && descriptions.Length != 0)
		{
			exceedDescriptions = descriptions;
			noticeNum = descriptions.Length;
			SetActive((Enum)UI.SPR_TITLE_ABILITY, is_visible: false);
			SetActive((Enum)UI.SPR_TITLE_EXCEED, is_visible: true);
			OnFinishedExceedDirection();
		}
	}

	public void OnFinishedExceedDirection()
	{
		if (noticeNum > 0)
		{
			SetFontStyle((Enum)UI.LBL_ADD_ABILITY, 2);
			SetLabelText((Enum)UI.LBL_ADD_ABILITY, exceedDescriptions[exceedDescriptions.Length - noticeNum]);
			noticeNum--;
			ResetTween((Enum)UI.SPR_TITLE_EXCEED, 0);
			PlayTween((Enum)UI.SPR_TITLE_EXCEED, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			int i = 1;
			for (int num = 4; i <= num; i++)
			{
				ResetTween((Enum)UI.SPR_TITLE_EXCEED, i);
				if (i < equipItemInfo.exceed)
				{
					SkipTween((Enum)UI.SPR_TITLE_EXCEED, forward: true, i);
				}
				else if (i == equipItemInfo.exceed)
				{
					PlayTween((Enum)UI.SPR_TITLE_EXCEED, forward: true, (EventDelegate.Callback)null, is_input_block: false, i);
				}
			}
			SetActive((Enum)UI.OBJ_ADD_ABILITY, is_visible: true);
			ResetTween((Enum)UI.OBJ_ADD_ABILITY, 1);
			PlayTween((Enum)UI.OBJ_ADD_ABILITY, forward: true, (EventDelegate.Callback)null, is_input_block: false, 1);
		}
		else
		{
			SetActive((Enum)UI.OBJ_ADD_ABILITY, is_visible: false);
		}
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		if (!(abilityDetailPopUp == null))
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
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
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
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
	}

	private void OnQuery_TO_GROW()
	{
		EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
		if (equipItemInfo.IsLevelMax())
		{
			if (equipItemInfo.tableData.IsEvolve())
			{
				SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
				smithGrowData.selectEquipData = equipItemInfo;
				GameSection.ChangeEvent("EVOLVE");
				return;
			}
			if (equipItemInfo.IsExceedMax() && !equipItemInfo.tableData.IsShadow())
			{
				GameSection.ChangeEvent("ALREADY_LV_MAX");
				return;
			}
		}
		SmithManager.SmithGrowData smithGrowData2 = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
		smithGrowData2.selectEquipData = equipItemInfo;
	}
}

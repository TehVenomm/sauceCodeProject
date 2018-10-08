using System;
using System.Collections;
using UnityEngine;

public class SmithGrowSkillResult : ItemDetailSkill
{
	private new enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
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
		PRG_EXP_BAR_BG,
		BTN_NEXT,
		BTN_NEXT_GRAY,
		LBL_NEXT_GRAY_BTN,
		OBJ_ADD_EXCEED,
		LBL_ADD_EXCEED,
		LBL_EXCEED_PREV,
		LBL_EXCEED_NEXT,
		SPR_BG_NORMAL,
		SPR_BG_EXCEED
	}

	public enum AUDIO
	{
		RESULT_EXCEEED = 40000157
	}

	private enum EFFECT_TYPE
	{
		NONE,
		NORMAL,
		GREAT
	}

	private const float DELAY_TIME = 0.3f;

	protected SmithManager.ResultData resultData;

	private bool isGreat;

	private bool isExceed;

	private bool isPlayExceedAnimation;

	public override string overrideBackKeyEvent => "TO_SELECT";

	public override void Initialize()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		resultData = (SmithManager.ResultData)array[0];
		isGreat = (bool)array[1];
		isExceed = (bool)array[2];
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		if (isExceed && resultData.beforeExceedCnt < skillItemInfo.exceedCnt)
		{
			isPlayExceedAnimation = true;
		}
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
			resultData.itemData
		});
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		string greatEffectName = "ef_ui_magi_result_02";
		string normalEffectName = "ef_ui_magi_result_01";
		string effectName = (!isGreat) ? normalEffectName : greatEffectName;
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effectName);
		yield return (object)load_queue.Wait();
		Transform effect = EffectManager.GetUIEffect(effectName, GetCtrl(UI.TEX_MODEL), -1f, -2, GetCtrl(UI.TEX_MODEL).GetComponent<UIWidget>());
		if (effect != null)
		{
			effect.set_localScale(new Vector3(100f, 100f, 1f));
		}
		MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		bool flag = true;
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		if (skillItemInfo != null && skillItemInfo.IsLevelMax())
		{
			flag = skillItemInfo.IsEnableExceed();
		}
		SetActive((Enum)UI.SPR_BG_NORMAL, !isExceed);
		SetActive((Enum)UI.SPR_BG_EXCEED, isExceed);
		SetActive((Enum)UI.BTN_NEXT, flag);
		SetActive((Enum)UI.BTN_NEXT_GRAY, !flag);
		SetLabelText((Enum)UI.LBL_NEXT_GRAY_BTN, base.sectionData.GetText("STR_NEXT"));
		if (isExceed)
		{
			if (resultData != null)
			{
				SetLabelText((Enum)UI.LBL_EXCEED_PREV, StringTable.Format(STRING_CATEGORY.SMITH, 9u, resultData.beforeExceedCnt));
			}
			if (skillItemInfo != null)
			{
				int exceedCnt = skillItemInfo.exceedCnt;
				SetLabelText((Enum)UI.LBL_EXCEED_NEXT, StringTable.Format(STRING_CATEGORY.SMITH, 9u, exceedCnt));
				ExceedSkillItemTable.ExceedSkillItemData exceedSkillItemData = Singleton<ExceedSkillItemTable>.I.GetExceedSkillItemData(exceedCnt);
				if (exceedSkillItemData != null)
				{
					SetLabelText((Enum)UI.LBL_ADD_EXCEED, StringTable.Format(STRING_CATEGORY.SMITH, 8u, exceedSkillItemData.GetDecreaseUseGaugePercent()));
				}
			}
		}
	}

	protected override void OnOpen()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (isPlayExceedAnimation)
		{
			this.StartCoroutine(DoPlayExceedAnimation());
		}
		base.OnOpen();
	}

	private IEnumerator DoPlayExceedAnimation()
	{
		yield return (object)new WaitForSeconds(0.3f);
		Transform effTrans = GetCtrl(UI.OBJ_ADD_EXCEED);
		UITweenCtrl.Play(effTrans, true, null, false, 0);
		UITweenCtrl.Play(effTrans, true, null, false, 1);
		SoundManager.PlayOneShotUISE(40000157);
	}
}

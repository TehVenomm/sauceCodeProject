using System.Collections;
using UnityEngine;

public class SmithAbilityItemResult : GameSection
{
	private enum UI
	{
		TEX_MODEL,
		STR_NEXT_REFLECT,
		OBJ_DELAY_1,
		OBJ_DELAY_2,
		OBJ_ABILITY_LIST_ROOT,
		OBJ_DIRECTION_1,
		LBL_ADD_ABILITY_1,
		BTN_TAP_FULL_SCREEN
	}

	private EquipItemInfo equipItemInfo;

	private AbilityChangeAbilityList abilityList;

	private int endDirectionCount;

	private bool m_isFirstOpen;

	private bool isFirstTap = true;

	public override string overrideBackKeyEvent => "GO_BACK";

	public override void Initialize()
	{
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		Transform transform = SetPrefab(UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList");
		abilityList = transform.gameObject.AddComponent<AbilityChangeAbilityList>();
		abilityList.uiVisible = true;
		SetRenderEquipModel(UI.TEX_MODEL, equipItemInfo.tableID, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
		SetLabelText(UI.STR_NEXT_REFLECT, base.sectionData.GetText("STR_NEXT"));
		ResetTween(UI.OBJ_DELAY_2);
		MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
		SetFullScreenButton(UI.BTN_TAP_FULL_SCREEN);
		m_isFirstOpen = true;
		base.Initialize();
	}

	protected override void OnOpen()
	{
		abilityList.InitUI();
		abilityList.Open();
		PlayTween(UI.OBJ_DELAY_1, forward: true, PlayDirection);
		SetActive(UI.OBJ_DELAY_2, is_visible: false);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		abilityList.SetParameter(equipItemInfo);
	}

	private void EndAllDirection()
	{
		SetActive(UI.OBJ_DELAY_2, is_visible: true);
		ResetTween(UI.OBJ_DELAY_2);
		PlayTween(UI.OBJ_DELAY_2);
		SetActive(UI.TEX_MODEL, is_visible: false);
		RefreshUI();
	}

	private void PlayDirection()
	{
		StartCoroutine(_PlayDirection());
	}

	private IEnumerator _PlayDirection()
	{
		PlayDirection(UI.OBJ_DIRECTION_1, UI.LBL_ADD_ABILITY_1, equipItemInfo.GetAbilityItem().GetName());
		yield return new WaitForSeconds(0.46f);
		if (m_isFirstOpen)
		{
			SoundManager.PlayOneShotUISE(40000049);
		}
		m_isFirstOpen = false;
	}

	private void PlayDirection(UI directionObj, UI label, string text)
	{
		SetFontStyle(label, FontStyle.Italic);
		SetLabelText(label, text);
		PlayTween(directionObj, forward: true, EndAllDirection, is_input_block: false);
	}

	private void OnQuery_NEXT()
	{
		GameSection.SetEventData(equipItemInfo);
	}

	private void OnQuery_GO_BACK()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((GameSectionHistory.HistoryData x) => x.sectionName == "SmithAbilityChangeSelect"))
		{
			GameSection.StopEvent();
			DispatchEvent("MAIN_MENU_STATUS");
		}
	}

	private void OnQuery_TAP_FULL_SCREEN()
	{
		if (isFirstTap)
		{
			isFirstTap = false;
			equipItemInfo.GetLotteryAbility();
			UITweenCtrl.SetDurationWithRate(GetCtrl(UI.OBJ_DIRECTION_1), 0.5f);
		}
	}

	protected void OnQuery_LOTTERY_LIST()
	{
		EquipItemInfo selectEquipData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		GameSection.SetEventData(new object[2]
		{
			selectEquipData,
			SmithEquipBase.SmithType.ABILITY_CHANGE
		});
	}
}

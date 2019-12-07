public class GuildRequestQuestOrderSelect : QuestOrderSelect
{
	private new enum UI
	{
		TEX_NPCMODEL,
		OBJ_NPC_MESSAGE,
		LBL_NPC_MESSAGE,
		BTN_SEARCH,
		BTN_SORT,
		LBL_SORT,
		TGL_ICON_ASC,
		SPR_FRAME,
		SPR_BG_BTN_CLOSE,
		BTN_INPUT_CLOSE,
		BTN_INPUT_CLOSE_BG,
		BG,
		TIELEBAR,
		FRAMEDOWN,
		FRAMEUP,
		LIST_BASE,
		DELIVERY_SCROLLBAR_OVER,
		DELIVERY_SCROLLBAR_BASE,
		OBJ_ORDER_ROOT,
		BTN_ORDER,
		SPR_ORDER_TEXT,
		SPR_ORDER_ICON,
		STR_ORDER_NON_LIST,
		GRD_ORDER_QUEST,
		SCR_ORDER_QUEST2,
		OBJ_ICON_ROOT,
		OBJ_BUTTON_ROOT,
		SCR_ORDER_QUEST,
		SPR_ORDER_RARITY_FRAME,
		LBL_ORDER_NUM,
		OBJ_FRAME,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		LBL_REMAIN,
		OBJ_MISSION_INFO_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		TXT_NEED_POINT,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		OBJ_BANNER_ROOT,
		SPR_BANNER,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_CHALLENGE_ON,
		OBJ_CHALLENGE_OFF,
		LBL_CHALLENGE_ON_MESSAGE,
		LBL_CHALLENGE_OFF_MESSAGE,
		BTN_SHADOW_COUNT
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	protected override void OnOpen()
	{
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.challengeInfo != null && MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount != null)
		{
			SetActive(GetCtrl(UI.OBJ_FRAME), UI.BTN_SHADOW_COUNT, is_visible: true);
		}
		else
		{
			SetActive(GetCtrl(UI.OBJ_FRAME), UI.BTN_SHADOW_COUNT, is_visible: false);
		}
	}

	public override void OnQuery_SELECT_ORDER()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= questGridDatas.Length)
		{
			GameSection.StopEvent();
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(questGridDatas[num].questSortData.GetTableID()))
		{
			GameSection.StopEvent();
			return;
		}
		GameSection.SetEventData(questGridDatas[num].questSortData.itemData.infoData);
		isScrollViewReady = false;
	}

	public override void OnQuery_SELECT_CHALLENGE()
	{
		if (MonoBehaviourSingleton<PartyManager>.IsValid())
		{
			if (!MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy())
			{
				GameSection.ChangeEvent("NO_SATISFY");
			}
			else if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num == 0)
			{
				GameSection.ChangeEvent("NUM_ZERO");
			}
		}
	}

	private void OnCloseDialog_GuildRequestSearchRoomCondition()
	{
		QuestSearchRoomCondition.SearchRequestParam searchRequestParam = GameSection.GetEventData() as QuestSearchRoomCondition.SearchRequestParam;
		if (searchRequestParam != null && searchRequestParam.order == 1)
		{
			param = searchRequestParam;
			nowPage = 1;
			RefreshUI();
		}
	}

	private void OnCloseDialog_GuildRequestSort()
	{
		_OnCloseDialogSort();
	}
}

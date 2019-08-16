using System;

public class InGameQuestAcceptCounter : QuestAcceptCounter
{
	protected new enum UI
	{
		TEX_NPCMODEL,
		OBJ_NPC_MESSAGE,
		LBL_NPC_MESSAGE,
		BTN_SEARCH,
		BTN_EVENT,
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
		OBJ_DELIVERY_ROOT,
		BTN_DELIVERY,
		SPR_DELIVERY_TEXT,
		SPR_DELIVERY_ICON,
		OBJ_QUEST_ROOT,
		BTN_QUEST,
		SPR_QUEST_TEXT,
		SPR_QUEST_ICON,
		STR_ORDER_NON_LIST,
		GRD_ORDER_QUEST,
		SCR_ORDER_QUEST2,
		STR_DELIVERY_NON_LIST,
		SCR_DELIVERY_QUEST,
		GRD_DELIVERY_QUEST,
		GRD_QUEST,
		OBJ_ICON_ROOT,
		OBJ_BUTTON_ROOT,
		SCR_ORDER_QUEST,
		SPR_ORDER_RARITY_FRAME,
		LBL_ORDER_NUM,
		TEX_NPC,
		LBL_DELIVERY_COMMENT,
		LBL_NEED_ITEM_NAME,
		LBL_HAVE,
		LBL_NEED,
		LBL_LIMIT,
		OBJ_REQUEST_OK,
		OBJ_REQUEST_COMPLETED,
		SPR_TYPE_STORY,
		SPR_TYPE_EVENT,
		SPR_TYPE_EVENT_TEXT,
		SPR_TYPE_DAILY_TEXT,
		SPR_TYPE_WEEKLY_TEXT,
		SPR_TYPE_HARD,
		SPR_TYPE_NORMAL,
		SPR_DIFFICULTY_EASY,
		SPR_DIFFICULTY_NORMAL,
		SPR_DIFFICULTY_HARD,
		SPR_DROP_DIFFICULTY_RARE,
		SPR_DROP_DIFFICULTY_SUPER_RARE,
		SCR_NORMAL_QUEST,
		OBJ_ROT_SPRITE,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
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
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		OBJ_FRAME,
		SPR_BG_FRAME
	}

	private bool isInActiveRotate;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
		}
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	public override void UpdateUI()
	{
		if (isInActiveRotate && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			Reposition(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		isInActiveRotate = false;
		base.UpdateUI();
	}

	private void Reposition(bool isPortrait)
	{
		UIScreenRotationHandler[] components = GetCtrl(UI.OBJ_FRAME).GetComponents<UIScreenRotationHandler>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].InvokeRotate();
		}
		GetCtrl(UI.BTN_EVENT).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.SPR_BG_FRAME).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		UpdateAnchors();
		if (GetCtrl(UI.SCR_DELIVERY_QUEST).get_gameObject().get_activeInHierarchy())
		{
			ScrollViewResetPosition((Enum)UI.SCR_DELIVERY_QUEST);
		}
	}

	private void OnScreenRotate(bool isPortrait)
	{
		if (base.transferUI != null)
		{
			isInActiveRotate = !base.transferUI.get_gameObject().get_activeInHierarchy();
		}
		else
		{
			isInActiveRotate = !base.collectUI.get_gameObject().get_activeInHierarchy();
		}
		if (!isInActiveRotate)
		{
			Reposition(isPortrait);
		}
	}
}

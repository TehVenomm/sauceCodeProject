public class InGameDeliveryDetailBase : QuestDeliveryDetail
{
	private new enum UI
	{
		OBJ_BASE_ROOT,
		OBJ_BACK,
		OBJ_COMPLETE_ROOT,
		BTN_COMPLETE,
		CHARA_ALL,
		OBJ_UNLOCK_PORTAL_ROOT,
		LBL_UNLOCK_PORTAL,
		LBL_QUEST_TITLE,
		LBL_CHARA_MESSAGE,
		LBL_PERSON_NAME,
		TEX_NPC,
		BTN_JUMP_QUEST,
		BTN_JUMP_INVALID,
		BTN_JUMP_MAP,
		BTN_JUMP_GACHATOP,
		GRD_REWARD,
		LBL_MONEY,
		LBL_EXP,
		SPR_WINDOW,
		SPR_MESSAGE_BG,
		OBJ_NEED_ITEM_ROOT,
		LBL_NEED_ITEM_NAME,
		LBL_NEED,
		LBL_HAVE,
		LBL_PLACE_NAME,
		LBL_ENEMY_NAME,
		OBJ_DIFFICULTY_ROOT,
		OBJ_ENEMY_NAME_ROOT,
		LBL_GET_PLACE,
		BTN_SUBMISSION,
		STR_BTN_SUBMISSION,
		STR_BTN_SUBMISSION_BACK,
		OBJ_TOP_CROWN_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		STR_MISSION_EMPTY,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_SUBMISSION_ROOT,
		OBJ_MISSION_INFO,
		OBJ_MISSION_INFO_1,
		OBJ_MISSION_INFO_2,
		OBJ_MISSION_INFO_3,
		LBL_MISSION_INFO_1,
		LBL_MISSION_INFO_2,
		LBL_MISSION_INFO_3,
		SPR_MISSION_INFO_CROWN_1,
		SPR_MISSION_INFO_CROWN_2,
		SPR_MISSION_INFO_CROWN_3,
		STR_MISSION,
		OBJ_BASE_FRAME,
		OBJ_TARGET_FRAME,
		OBJ_SUBMISSION_FRAME,
		OBJ_NORMAL_ROOT,
		OBJ_EVENT_ROOT,
		LBL_POINT_NORMAL,
		TEX_NORMAL_ICON,
		LBL_POINT_EVENT,
		TEX_EVENT_ICON,
		BTN_CREATE,
		BTN_JOIN,
		BTN_MATCHING,
		PORTRAIT_WINDOW,
		PORTRAIT_BACK,
		LANDSCAPE_WINDOW,
		LANDSCAPE_BACK
	}

	private bool isUpdatedUI;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetPrefab(UI.OBJ_BASE_ROOT, "InGameDeliveryDetailSettings");
		isUpdatedUI = true;
		OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		if (isUpdatedUI)
		{
			if (is_portrait)
			{
				SetActive(UI.CHARA_ALL, true);
				SetActive(UI.PORTRAIT_BACK, true);
				SetActive(UI.LANDSCAPE_BACK, false);
				GetCtrl(UI.SPR_WINDOW).localPosition = GetCtrl(UI.PORTRAIT_WINDOW).localPosition;
				SetHeight(UI.SPR_WINDOW, GetHeight(UI.PORTRAIT_WINDOW));
			}
			else
			{
				SetActive(UI.CHARA_ALL, false);
				SetActive(UI.PORTRAIT_BACK, false);
				SetActive(UI.LANDSCAPE_BACK, true);
				GetCtrl(UI.SPR_WINDOW).localPosition = GetCtrl(UI.LANDSCAPE_WINDOW).localPosition;
				SetHeight(UI.SPR_WINDOW, GetHeight(UI.LANDSCAPE_WINDOW));
			}
			UpdateAnchors();
		}
	}
}

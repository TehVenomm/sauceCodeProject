using Network;
using System.Collections.Generic;

public class QuestAcceptSeriesArenaDetail : QuestDeliveryDetail
{
	protected new enum UI
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
		OBJ_ENEMY,
		SPR_ELEMENT_ROOT,
		SPR_ENM_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
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
		LBL_LIMIT_TIME,
		OBJ_CHANGE_INFO,
		BTN_CHANGE_INFO,
		OBJ_DROP_ICON_ROOT,
		OBJ_CLEAR_ICON_ROOT,
		OBJ_DROP_REWARD,
		OBJ_CLEAR_REWARD,
		GRD_DROP_REWARD,
		LBL_RUSH_LEVEL,
		TEX_RUSH_IMAGE,
		BTN_CREATE_OFF,
		OBJ_RANK_UP_ROOT,
		OBJ_RANK_UP,
		TEX_RANK_PRE,
		TEX_RANK_NEW,
		OBJ_PARTICLE,
		LBL_LIMIT_TIME_NAME,
		SPR_TYPE_DIFFICULTY
	}

	private const int PREDOWNLOAD_MESSAGE_KEY = 3000;

	private bool isShowDropInfo;

	private const string WINDOW_SPRITE = "RequestWindowBase_Arena";

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "PointShopGetPointTable";
			yield return "DeliveryRewardTable";
			yield return "FieldMapTable";
			yield return "ArenaTable";
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		ResourceLoad.LoadWithSetUITexture(GetCtrl(UI.TEX_RUSH_IMAGE).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(info.GetQuestData().rarity));
	}

	public override void UpdateUI()
	{
		SetActive(UI.OBJ_DROP_REWARD, is_visible: true);
		SetActive(UI.OBJ_CLEAR_REWARD, is_visible: true);
		base.UpdateUI();
		UpdateTime();
		SetSprite(baseRoot, UI.SPR_WINDOW, "RequestWindowBase_Arena");
		SetDifficultySprite();
		UpdateRewardInfo();
	}

	private void UpdateTime()
	{
		ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(info.GetQuestData().questID);
		if (clearStatusQuestData != null && clearStatusQuestData.clearTime > 0)
		{
			string text = QuestUtility.CreateTimeStringByMilliSecSeriesArena(clearStatusQuestData.clearTime);
			SetLabelText(UI.LBL_LIMIT_TIME, text);
		}
		else
		{
			SetLabelText(UI.LBL_RUSH_LEVEL, "");
		}
	}

	protected override void UpdateNPC(string map_name, string enemy_name)
	{
		SetActive(UI.CHARA_ALL, is_visible: false);
	}

	protected override void SetBaseFrame()
	{
		baseRoot = GetCtrl(UI.OBJ_BASE_FRAME);
	}

	protected override void SetTargetFrame()
	{
		targetFrame = GetCtrl(UI.OBJ_TARGET_FRAME);
	}

	protected override void SetSubmissionFrame()
	{
		submissionFrame = null;
	}

	protected override void OnEndCompletetween(bool is_unlock_portal, string effectName)
	{
		base.OnEndCompletetween(is_unlock_portal, effectName);
	}

	private void UpdateRewardInfo()
	{
		SetActive(UI.OBJ_CLEAR_ICON_ROOT, !isShowDropInfo);
		SetActive(UI.OBJ_DROP_ICON_ROOT, isShowDropInfo);
		SetActive(UI.OBJ_CLEAR_REWARD, !isShowDropInfo);
		SetActive(UI.OBJ_DROP_REWARD, isShowDropInfo);
		SetActive(UI.OBJ_COMPLETE_ROOT, !isShowDropInfo);
	}

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive(UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}

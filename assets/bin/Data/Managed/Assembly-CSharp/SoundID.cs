using UnityEngine;

public class SoundID : MonoBehaviour
{
	public enum AudioSettinID
	{
		Default,
		IngameBoss,
		IngameField,
		Lounge
	}

	public enum UISE
	{
		INVALID = -1,
		CLICK = 40000001,
		OK = 40000002,
		CANCEL = 40000003,
		POPUP = 40000004,
		MENU_OPEN = 40000005,
		DIALOG_GOOD = 40000006,
		DIALOG_COMMON = 40000007,
		SELECT1 = 40000009,
		DIALOG_IMPTNT = 40000010,
		GET_PRIZE = 40000018,
		CHAT_BALOON = 40000022,
		POP_QUEST = 40000123
	}

	public enum BGM
	{
		INVALID = -1,
		NONE = 0,
		TITLE = 1,
		HOME = 2,
		QUEST_LIST = 3,
		RESULT_WIN = 4,
		THEME_SERIOUS = 5,
		MYHOUSE = 6,
		GACHA_TOP = 7,
		GACHA_QUEST = 8,
		GACHA_MAGI = 9,
		RESULT_LOSE = 10,
		TITLE_LOGO = 11,
		BOSS_WARNING = 12,
		OPENING = 13,
		DEBRIEFING = 14,
		GATHER = 108,
		FIELD_NORMAL = 112,
		FIELD_MYSTERIOUS = 113,
		FIELD_PASSIONATE = 114,
		LOUNGE = 153,
		JACKPOT_WIN = 191
	}

	public enum ConfigID
	{
		RESULT_COUNTER = 90000001
	}

	public SoundID()
		: this()
	{
	}
}

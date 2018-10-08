public static class GameSectionType
{
	public static bool IsDialog(this GAME_SECTION_TYPE type)
	{
		return type == GAME_SECTION_TYPE.DIALOG || type == GAME_SECTION_TYPE.PAGE_DIALOG || type == GAME_SECTION_TYPE.SINGLE_DIALOG || type == GAME_SECTION_TYPE.COMMON_DIALOG;
	}

	public static bool IsSingle(this GAME_SECTION_TYPE type)
	{
		return type == GAME_SECTION_TYPE.SINGLE_DIALOG || type == GAME_SECTION_TYPE.COMMON_DIALOG;
	}
}

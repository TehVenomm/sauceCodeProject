public class ClanSymbolTabController : TabDepthBtn
{
	protected override string GetBtnSelectedSprite()
	{
		return "PartyBtn_on";
	}

	protected override int GetDepthOffSet()
	{
		return 5;
	}

	protected override string GetBtnUnSelectedSprite()
	{
		return "PartyBtn_off";
	}
}

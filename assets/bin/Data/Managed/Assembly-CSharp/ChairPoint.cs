public class ChairPoint
{
	public ChairPoint dir;

	public HomePlayerCharacterBase sittingChara
	{
		get;
		private set;
	}

	public ChairPoint()
		: this()
	{
	}

	public void SetSittingCharacter(HomePlayerCharacterBase chara)
	{
		sittingChara = chara;
	}

	public void ResetSittingCharacter()
	{
		sittingChara = null;
	}
}

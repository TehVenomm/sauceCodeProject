public class GuildSmithMaterialDetail : ItemDetailTop
{
	private int needNum;

	protected override int? GetNeedNum()
	{
		return needNum;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		needNum = (int)array[1];
		GameSection.SetEventData(array[0]);
		base.Initialize();
	}
}

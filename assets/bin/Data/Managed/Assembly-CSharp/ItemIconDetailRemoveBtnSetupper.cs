public class ItemIconDetailRemoveBtnSetupper : ItemIconDetailSetuperBase
{
	public override void Set(object[] data = null)
	{
		string name = data[0] as string;
		SetName(name);
		SetVisibleBG(true);
		base.Set(null);
	}
}

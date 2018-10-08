public class ItemIconDetailAccessorySetupper : ItemIconDetailSetuperBase
{
	public UILabel lblDescription;

	public override void Set(object[] data = null)
	{
		base.Set(null);
		AccessoryTable.AccessoryData accessoryData = data[0] as AccessoryTable.AccessoryData;
		SetName(accessoryData.name);
		SetVisibleBG(true);
		infoRootAry[0].SetActive(true);
		lblDescription.text = accessoryData.descript;
	}
}

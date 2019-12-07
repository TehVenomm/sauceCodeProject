public class ItemIconDetailAccessorySetupper : ItemIconDetailSetuperBase
{
	public UILabel lblDescription;

	public override void Set(object[] data = null)
	{
		base.Set();
		AccessoryTable.AccessoryData accessoryData = data[0] as AccessoryTable.AccessoryData;
		SetName(accessoryData.name);
		SetVisibleBG(is_visible: true);
		infoRootAry[0].SetActive(value: true);
		lblDescription.text = accessoryData.descript;
	}
}

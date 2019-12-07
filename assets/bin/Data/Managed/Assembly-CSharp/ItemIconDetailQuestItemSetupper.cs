public class ItemIconDetailQuestItemSetupper : ItemIconDetailSetuperBase
{
	public UILabel lblNum;

	public UILabel lblDifficulty;

	public UILabel lblEnemyName;

	public override void Set(object[] data = null)
	{
		base.Set();
		QuestSortData questSortData = data[0] as QuestSortData;
		bool num = (bool)data[1];
		QuestTable.QuestTableData tableData = questSortData.itemData.infoData.questData.tableData;
		SetName(tableData.questText);
		SetVisibleBG(is_visible: true);
		if (num)
		{
			infoRootAry[0].SetActive(value: true);
			infoRootAry[1].SetActive(value: false);
			lblNum.text = questSortData.GetNum().ToString();
		}
		else
		{
			infoRootAry[0].SetActive(value: false);
			infoRootAry[1].SetActive(value: true);
			lblDifficulty.text = ((int)(tableData.difficulty + 1)).ToString();
			lblEnemyName.text = Singleton<EnemyTable>.I.GetEnemyData((uint)tableData.GetMainEnemyID()).name;
		}
	}
}

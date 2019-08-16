public class ItemIconDetailQuestItemSetupper : ItemIconDetailSetuperBase
{
	public UILabel lblNum;

	public UILabel lblDifficulty;

	public UILabel lblEnemyName;

	public override void Set(object[] data = null)
	{
		base.Set();
		QuestSortData questSortData = data[0] as QuestSortData;
		bool flag = (bool)data[1];
		QuestTable.QuestTableData tableData = questSortData.itemData.infoData.questData.tableData;
		SetName(tableData.questText);
		SetVisibleBG(is_visible: true);
		if (flag)
		{
			infoRootAry[0].SetActive(true);
			infoRootAry[1].SetActive(false);
			lblNum.text = questSortData.GetNum().ToString();
		}
		else
		{
			infoRootAry[0].SetActive(false);
			infoRootAry[1].SetActive(true);
			lblDifficulty.text = ((int)(tableData.difficulty + 1)).ToString();
			lblEnemyName.text = Singleton<EnemyTable>.I.GetEnemyData((uint)tableData.GetMainEnemyID()).name;
		}
	}
}

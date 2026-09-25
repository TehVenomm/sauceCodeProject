// Decompiled with JetBrains decompiler
// Type: ItemIconDetailQuestItemSetupper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ItemIconDetailQuestItemSetupper : ItemIconDetailSetuperBase
{
  public UILabel lblNum;
  public UILabel lblDifficulty;
  public UILabel lblEnemyName;

  public override void Set(object[] data = null)
  {
    base.Set();
    QuestSortData questSortData = data[0] as QuestSortData;
    int num = (bool) data[1] ? 1 : 0;
    QuestTable.QuestTableData tableData = questSortData.itemData.infoData.questData.tableData;
    this.SetName(tableData.questText);
    this.SetVisibleBG(true);
    if (num != 0)
    {
      this.infoRootAry[0].SetActive(true);
      this.infoRootAry[1].SetActive(false);
      this.lblNum.text = questSortData.GetNum().ToString();
    }
    else
    {
      this.infoRootAry[0].SetActive(false);
      this.infoRootAry[1].SetActive(true);
      this.lblDifficulty.text = ((int) (tableData.difficulty + 1)).ToString();
      this.lblEnemyName.text = Singleton<EnemyTable>.I.GetEnemyData((uint) tableData.GetMainEnemyID()).name;
    }
  }
}

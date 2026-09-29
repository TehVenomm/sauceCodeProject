// Decompiled with JetBrains decompiler
// Type: QuestSortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestSortData : SortCompareData
{
  public int enemyModelID;
  public QuestItemInfo itemData;

  public override object GetItemData() => (object) this.itemData;

  public override void SetItem(object item)
  {
    this.itemData = (QuestItemInfo) item;
    this.enemyModelID = Singleton<EnemyTable>.I.GetEnemyData((uint) this.GetMainEnemyID()).modelId;
  }

  public ELEMENT_TYPE GetEnemyElement()
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.GetMainEnemyID());
    return enemyData == null ? ELEMENT_TYPE.MAX : enemyData.element;
  }

  public EnemyTable.EnemyData GetEnemyData()
  {
    return Singleton<EnemyTable>.I.GetEnemyData((uint) this.GetMainEnemyID());
  }

  private int GetMainEnemyID() => this.itemData.infoData.questData.tableData.GetMainEnemyID();

  public override void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
    switch (requirement)
    {
      case SortBase.SORT_REQUIREMENT.NUM:
        this.sortingData = (long) this.itemData.infoData.questData.num;
        break;
      case SortBase.SORT_REQUIREMENT.RARITY:
        this.sortingData = (long) this.itemData.infoData.questData.tableData.rarity;
        break;
      case SortBase.SORT_REQUIREMENT.DIFFICULTY:
        this.sortingData = (long) this.itemData.infoData.questData.tableData.difficulty;
        break;
      case SortBase.SORT_REQUIREMENT.ENEMY:
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.itemData.infoData.questData.tableData.GetMainEnemyID());
        if (enemyData == null)
        {
          this.sortingData = 0L;
          break;
        }
        this.sortingData = (long) enemyData.type;
        break;
      default:
        this.sortingData = (long) this.itemData.infoData.questData.tableData.questID;
        break;
    }
  }

  public override bool IsAbsFirst()
  {
    return MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(this.GetTableID());
  }

  public override bool IsFavorite() => false;

  public override ulong GetUniqID() => this.itemData.uniqueID;

  public override int GetItemType() => this.EnemyTypeToSortItemType();

  public override int GetNum() => this.itemData.infoData.questData.num;

  public override uint GetTableID() => this.itemData.infoData.questData.tableData.questID;

  public override string GetName() => this.itemData.infoData.questData.tableData.questText;

  public override RARITY_TYPE GetRarity() => this.itemData.infoData.questData.tableData.rarity;

  public override ITEM_ICON_TYPE GetIconType()
  {
    return ItemIcon.GetItemIconType(this.itemData.infoData.questData.tableData.questType);
  }

  public override ELEMENT_TYPE GetIconElement() => this.GetEnemyElement();

  public override bool CanSale() => !this.itemData.infoData.questData.tableData.cantSale;

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.QUEST_ITEM;

  public override int GetIconID()
  {
    EnemyTable.EnemyData enemyData = this.GetEnemyData();
    if (enemyData != null)
      return enemyData.iconId;
    Log.Error("ENEMY_TABLE_DATA is Not Found : main_enemy_id = " + (object) this.GetMainEnemyID());
    return 0;
  }

  private int EnemyTypeToSortItemType()
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.GetMainEnemyID());
    if (enemyData == null)
      return 0;
    int sortItemType;
    switch (enemyData.type)
    {
      case ENEMY_TYPE.GORILLA:
        sortItemType = 1;
        break;
      case ENEMY_TYPE.RABBIT:
        sortItemType = 2;
        break;
      case ENEMY_TYPE.CHIMERA:
        sortItemType = 4;
        break;
      case ENEMY_TYPE.WOLF:
        sortItemType = 8;
        break;
      case ENEMY_TYPE.DRAGON:
        sortItemType = 16 /*0x10*/;
        break;
      case ENEMY_TYPE.DRAKE:
        sortItemType = 32 /*0x20*/;
        break;
      case ENEMY_TYPE.WYVERN:
        sortItemType = 64 /*0x40*/;
        break;
      case ENEMY_TYPE.UNDEAD_KNIGHT:
        sortItemType = 128 /*0x80*/;
        break;
      case ENEMY_TYPE.WRAITH:
        sortItemType = 256 /*0x0100*/;
        break;
      case ENEMY_TYPE.GIANT:
        sortItemType = 512 /*0x0200*/;
        break;
      case ENEMY_TYPE.GOLEM:
        sortItemType = 1024 /*0x0400*/;
        break;
      case ENEMY_TYPE.ELEMENTAL:
        sortItemType = 2048 /*0x0800*/;
        break;
      case ENEMY_TYPE.CHICKEN:
        sortItemType = 4096 /*0x1000*/;
        break;
      case ENEMY_TYPE.MUSHROOM:
        sortItemType = 8192 /*0x2000*/;
        break;
      case ENEMY_TYPE.COW:
        sortItemType = 16384 /*0x4000*/;
        break;
      case ENEMY_TYPE.FROG:
        sortItemType = 32768 /*0x8000*/;
        break;
      case ENEMY_TYPE.BAT:
        sortItemType = 65536 /*0x010000*/;
        break;
      case ENEMY_TYPE.SLIME:
        sortItemType = 131072 /*0x020000*/;
        break;
      case ENEMY_TYPE.SAHUAGIN:
        sortItemType = 262144 /*0x040000*/;
        break;
      default:
        return 1;
    }
    return sortItemType;
  }
}

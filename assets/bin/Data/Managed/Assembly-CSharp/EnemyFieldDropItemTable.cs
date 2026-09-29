// Decompiled with JetBrains decompiler
// Type: EnemyFieldDropItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class EnemyFieldDropItemTable : Singleton<EnemyFieldDropItemTable>
{
  public const int PART_REWARD_NUM_MUX = 5;
  private Dictionary<uint, List<EnemyFieldDropItemTable.EnemyFieldDropItemData>> enemyToItemTable = new Dictionary<uint, List<EnemyFieldDropItemTable.EnemyFieldDropItemData>>();
  private object tableLock = new object();

  public void Add(uint enemyId, uint itemId, uint fieldId, List<int> partIds)
  {
    lock (this.tableLock)
    {
      List<EnemyFieldDropItemTable.EnemyFieldDropItemData> source;
      if (!this.enemyToItemTable.TryGetValue(enemyId, out source))
      {
        source = new List<EnemyFieldDropItemTable.EnemyFieldDropItemData>();
        this.enemyToItemTable[enemyId] = source;
      }
      if (source.Any<EnemyFieldDropItemTable.EnemyFieldDropItemData>((Func<EnemyFieldDropItemTable.EnemyFieldDropItemData, bool>) (x => (int) x.itemId == (int) itemId && (int) x.fieldId == (int) fieldId)))
        return;
      EnemyFieldDropItemTable.EnemyFieldDropItemData fieldDropItemData = new EnemyFieldDropItemTable.EnemyFieldDropItemData(enemyId, itemId, fieldId, partIds);
      source.Add(fieldDropItemData);
    }
  }

  public List<EnemyFieldDropItemTable.EnemyFieldDropItemData> GetEnemyData(uint enemyId)
  {
    List<EnemyFieldDropItemTable.EnemyFieldDropItemData> tempList = new List<EnemyFieldDropItemTable.EnemyFieldDropItemData>();
    if (this.enemyToItemTable.ContainsKey(enemyId))
      this.enemyToItemTable[enemyId].ForEach((Action<EnemyFieldDropItemTable.EnemyFieldDropItemData>) (x =>
      {
        if ((int) x.enemyId != (int) enemyId)
          return;
        tempList.Add(x);
      }));
    return tempList;
  }

  public class EnemyFieldDropItemData
  {
    public uint enemyId;
    public uint itemId;
    public uint fieldId;
    public List<int> partIds = new List<int>(5);

    public EnemyFieldDropItemData(uint eId, uint iId, uint fId, List<int> pIds)
    {
      this.enemyId = eId;
      this.itemId = iId;
      this.fieldId = fId;
      this.partIds = pIds;
    }

    public override string ToString()
    {
      return $"enemyId:{this.enemyId} itemId:{this.itemId} fieldId:{this.fieldId} partsCount:{this.partIds.Count}";
    }
  }
}

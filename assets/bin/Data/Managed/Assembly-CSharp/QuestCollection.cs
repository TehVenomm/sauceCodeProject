// Decompiled with JetBrains decompiler
// Type: QuestCollection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class QuestCollection
{
  public List<ENEMY_TYPE>[] enemyTypeListAry { get; private set; }

  public List<ENEMY_TYPE> GetEnemyTypeList(QUEST_TYPE type) => this._GetEnemyTypeList(type);

  public List<ENEMY_TYPE> GetAllEnemyTypeList()
  {
    List<ENEMY_TYPE> ret = new List<ENEMY_TYPE>((IEnumerable<ENEMY_TYPE>) this.enemyTypeListAry[0]);
    int index = 1;
    for (int length = this.enemyTypeListAry.Length; index < length; ++index)
      this.enemyTypeListAry[index].ForEach((Action<ENEMY_TYPE>) (add_data =>
      {
        if (ret.FindIndex((Predicate<ENEMY_TYPE>) (_data => _data == add_data)) != -1)
          return;
        ret.Add(add_data);
      }));
    return ret;
  }

  public QuestCollection()
  {
    this.enemyTypeListAry = new List<ENEMY_TYPE>[3];
    int index = 0;
    for (int length = this.enemyTypeListAry.Length; index < length; ++index)
      this.enemyTypeListAry[index] = new List<ENEMY_TYPE>();
  }

  public void Collect(ENEMY_TYPE type, QUEST_TYPE quest_type)
  {
    List<ENEMY_TYPE> enemyTypeList = this._GetEnemyTypeList(quest_type);
    if (enemyTypeList == null || enemyTypeList.IndexOf(type) != -1)
      return;
    enemyTypeList.Add(type);
  }

  public void Sort()
  {
    int index = 0;
    for (int length = this.enemyTypeListAry.Length; index < length; ++index)
      this.enemyTypeListAry[index].Sort();
  }

  private List<ENEMY_TYPE> _GetEnemyTypeList(QUEST_TYPE quest_type)
  {
    int index;
    switch (quest_type)
    {
      case QUEST_TYPE.NORMAL:
        index = 0;
        break;
      case QUEST_TYPE.EVENT:
        index = 1;
        break;
      case QUEST_TYPE.ORDER:
        index = 2;
        break;
      default:
        return (List<ENEMY_TYPE>) null;
    }
    return this.enemyTypeListAry[index];
  }
}

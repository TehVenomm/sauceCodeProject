// Decompiled with JetBrains decompiler
// Type: EquipModelHQTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class EquipModelHQTable : ScriptableObject
{
  public EquipModelHQTable.Data[] weaponDatas;

  public byte GetWeaponFlag(int id)
  {
    byte weaponFlag = 0;
    if (this.weaponDatas != null)
    {
      EquipModelHQTable.Data data = Array.Find<EquipModelHQTable.Data>(this.weaponDatas, (Predicate<EquipModelHQTable.Data>) (o => o.id == id));
      if (data != null)
        weaponFlag = data.flag;
    }
    return weaponFlag;
  }

  [Serializable]
  public class Data
  {
    public int id;
    public byte flag;

    public Data(int id, byte flag)
    {
      this.id = id;
      this.flag = flag;
    }
  }
}

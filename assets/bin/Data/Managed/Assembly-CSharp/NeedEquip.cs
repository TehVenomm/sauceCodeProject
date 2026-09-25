// Decompiled with JetBrains decompiler
// Type: NeedEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class NeedEquip
{
  public bool isKey;
  public uint equipItemID;
  public int num;
  public int needLv;

  public NeedEquip(uint _equip_item_id, int _num, int _need_lv)
  {
    this.isKey = false;
    this.equipItemID = _equip_item_id;
    this.num = _num;
    this.needLv = _need_lv;
  }

  public NeedEquip(bool _is_key, uint _equip_item_id, int _num, int _need_lv)
  {
    this.isKey = _is_key;
    this.equipItemID = _equip_item_id;
    this.num = _num;
    this.needLv = _need_lv;
  }

  public NeedEquip Copy() => new NeedEquip(this.isKey, this.equipItemID, this.num, this.needLv);

  public static NeedEquip[] DivideNeedEquip(NeedEquip[] old)
  {
    List<NeedEquip> needEquipList = new List<NeedEquip>();
    for (int index1 = 0; index1 < old.Length; ++index1)
    {
      for (int index2 = 0; index2 < old[index1].num; ++index2)
      {
        NeedEquip needEquip = old[index1].Copy();
        needEquip.num = 1;
        needEquipList.Add(needEquip);
      }
    }
    return needEquipList.ToArray();
  }
}

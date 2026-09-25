// Decompiled with JetBrains decompiler
// Type: EquipItemStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class EquipItemStatus : ItemStatus
{
  public ItemStatus[] equipTypeBuff;

  public EquipItemStatus() => this.Init();

  public EquipItemStatus(EquipItemStatus _base)
  {
    this.Init();
    this.Add(_base);
  }

  protected override void Init()
  {
    base.Init();
    this.equipTypeBuff = new ItemStatus[MonoBehaviourSingleton<StatusManager>.I.ENABLE_EQUIP_TYPE_MAX];
    int index = 0;
    for (int length = this.equipTypeBuff.Length; index < length; ++index)
      this.equipTypeBuff[index] = new ItemStatus();
  }

  public void Add(EquipItemStatus param)
  {
    if (param == null)
      return;
    this.Add((ItemStatus) param);
    int index = 0;
    for (int length = this.equipTypeBuff.Length; index < length; ++index)
      this.equipTypeBuff[index].Add(param.equipTypeBuff[index]);
  }

  public void Add(ItemStatus[] equip_type_buff)
  {
    int index = 0;
    for (int length = this.equipTypeBuff.Length; index < length; ++index)
      this.equipTypeBuff[index].Add(equip_type_buff[index]);
  }

  public int GetEquipTypeAtkBuf(EquipItemInfo item)
  {
    int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(item.tableData.type);
    int elemAtkType = item.GetElemAtkType();
    int num = 0;
    ItemStatus itemStatus = this.equipTypeBuff[equipmentTypeIndex];
    int equipTypeAtkBuf = num + itemStatus.atk;
    switch (elemAtkType)
    {
      case -1:
        int index = 0;
        for (int length = itemStatus.elemAtk.Length; index < length; ++index)
          equipTypeAtkBuf += itemStatus.elemAtk[index];
        goto case 6;
      case 6:
        return equipTypeAtkBuf;
      default:
        equipTypeAtkBuf += itemStatus.elemAtk[elemAtkType];
        goto case 6;
    }
  }

  public int GetEquipTypeDefBuf()
  {
    return 0 + this.equipTypeBuff[MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(EQUIPMENT_TYPE.ARMOR)].def;
  }

  public int GetAllAtkElem()
  {
    int[] elemAtk = this.elemAtk;
    int allAtkElem = 0;
    int index = 0;
    for (int length = elemAtk.Length; index < length; ++index)
      allAtkElem += elemAtk[index];
    return allAtkElem;
  }
}

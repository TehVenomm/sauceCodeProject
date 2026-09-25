// Decompiled with JetBrains decompiler
// Type: ItemStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ItemStatus
{
  public int atk;
  public int def;
  public int hp;
  public int[] elemAtk;
  public int[] elemDef;

  public ItemStatus() => this.Init();

  protected virtual void Init()
  {
    this.atk = 0;
    this.def = 0;
    this.hp = 0;
    this.elemAtk = new int[6];
    this.elemDef = new int[6];
  }

  public void Add(ItemStatus param)
  {
    if (param == null)
      return;
    this.atk += param.atk;
    this.def += param.def;
    this.hp += param.hp;
    int index1 = 0;
    for (int index2 = 6; index1 < index2; ++index1)
    {
      this.elemAtk[index1] += param.elemAtk[index1];
      this.elemDef[index1] += param.elemDef[index1];
    }
  }

  public int GetElemAtk(EquipItemInfo item)
  {
    if (item == null)
      return 0;
    int elemAtkType = item.GetElemAtkType();
    return elemAtkType == -1 ? 0 : this._GetElem(elemAtkType, this.elemAtk);
  }

  public int GetElemAtk(int elem) => this._GetElem(elem, this.elemAtk);

  public int GetElemDef(EquipItemInfo item)
  {
    if (item == null)
      return 0;
    int elemDefType = item.GetElemDefType();
    return elemDefType == -1 ? 0 : this._GetElem(elemDefType, this.elemDef);
  }

  public int GetElemDef(int elem) => this._GetElem(elem, this.elemDef);

  private int _GetElem(int elem, int[] target_elem)
  {
    if (elem == 6)
      return 0;
    int elem1 = 0;
    if (elem == -1)
    {
      for (int index = 0; index < 6; ++index)
        elem1 += target_elem[index];
    }
    else
      elem1 = target_elem[elem];
    return elem1;
  }
}

// Decompiled with JetBrains decompiler
// Type: NeedMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class NeedMaterial
{
  public bool isKey;
  public uint itemID;
  public int num;

  public NeedMaterial(uint _item_id, int _num)
  {
    this.isKey = false;
    this.itemID = _item_id;
    this.num = _num;
  }

  public NeedMaterial(bool _is_key, uint _item_id, int _num)
  {
    this.isKey = _is_key;
    this.itemID = _item_id;
    this.num = _num;
  }

  public override bool Equals(object obj)
  {
    return obj != null && obj is NeedMaterial needMaterial && this.isKey == needMaterial.isKey && (int) this.itemID == (int) needMaterial.itemID && this.num == needMaterial.num;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override string ToString()
  {
    return $"isKey:{this.isKey.ToString()}, itemID:{(object) this.itemID}, num:{(object) this.num}";
  }
}

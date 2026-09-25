// Decompiled with JetBrains decompiler
// Type: Network.SkillItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class SkillItem
{
  public string uniqId;
  public int skillItemId;
  public XorInt level = (XorInt) 0;
  public int exceed;
  public int exceedExp;
  public int is_locked;
  public int exp;
  public int expPrev;
  public int expNext;
  public float growCost;
  public int price;
  public List<SkillItem.EquipSetSlot> equipSlots = new List<SkillItem.EquipSetSlot>();
  public SkillItem.UniqueEquipSetSlot uniqueEquipSlots = new SkillItem.UniqueEquipSetSlot();

  public int RelativeExp => this.exp - this.expPrev;

  public int RelativeExpNext => this.expNext - this.expPrev;

  public float ExpProgress01
  {
    get => this.RelativeExpNext > 0 ? (float) this.RelativeExp / (float) this.RelativeExpNext : 1f;
  }

  public class EquipSetSlot
  {
    public int setNo;
    public string euid;
    public int slotNo;

    public EquipSetSlot()
    {
    }

    public EquipSetSlot(int sNo, string eId, int slNo)
    {
      this.setNo = sNo;
      this.euid = eId;
      this.slotNo = slNo;
    }
  }

  public class UniqueEquipSetSlot
  {
    public string euid;
    public int slotNo;

    public UniqueEquipSetSlot()
    {
    }

    public UniqueEquipSetSlot(string eId, int slNo)
    {
      this.euid = eId;
      this.slotNo = slNo;
    }
  }

  public class DiffEquipSetSlot : SkillItem.EquipSetSlot
  {
    public string uniqId;

    public DiffEquipSetSlot()
    {
    }

    public DiffEquipSetSlot(int sNo, string eId, int slNo, string uId)
      : base(sNo, eId, slNo)
    {
      this.uniqId = uId;
    }
  }
}

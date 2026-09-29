// Decompiled with JetBrains decompiler
// Type: EquipSetSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class EquipSetSkillData
{
  public ulong equipItemUniqId;
  public int equipSlotNo;
  public int equipSetNo;

  public EquipSetSkillData()
  {
  }

  public EquipSetSkillData(Network.SkillItem.EquipSetSlot setSkill)
  {
    ulong result;
    if (ulong.TryParse(setSkill.euid, out result))
      this.equipItemUniqId = result;
    else
      Log.Error("Equip Item EquipUniqueId Error euid:{0} setNo:{1}", (object) setSkill.euid, (object) setSkill.setNo);
    this.equipSlotNo = setSkill.slotNo;
    this.equipSetNo = setSkill.setNo;
  }

  public EquipSetSkillData(Network.SkillItem.UniqueEquipSetSlot setSkill)
  {
    ulong result;
    if (ulong.TryParse(setSkill.euid, out result))
      this.equipItemUniqId = result;
    else if (string.IsNullOrEmpty(setSkill.euid))
      this.equipItemUniqId = 0UL;
    else
      Log.Error("Equip Item EquipUniqueId Error euid:{0}", (object) setSkill.euid);
    this.equipSlotNo = setSkill.slotNo;
    this.equipSetNo = 0;
  }
}

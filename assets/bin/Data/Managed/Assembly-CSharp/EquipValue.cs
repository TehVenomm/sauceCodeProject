// Decompiled with JetBrains decompiler
// Type: EquipValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class EquipValue
{
  public EQUIPMENT_TYPE type;
  public SP_ATTACK_TYPE spAttackType;
  public SimpleStatus baseStatus = new SimpleStatus();
  public int constHp;
  public int[] constAtks = new int[7];
  public int[] constDefs = new int[7];
  public int[] constTols = new int[6];
  public List<EquipValue.SkillSupport> skillSupport = new List<EquipValue.SkillSupport>();
  public Dictionary<int, int> ability = new Dictionary<int, int>();

  private void _Reset()
  {
    this.type = EQUIPMENT_TYPE.NONE;
    this.spAttackType = SP_ATTACK_TYPE.NONE;
    this.baseStatus.Reset();
    this.constHp = 0;
    for (int index = 0; index < 7; ++index)
    {
      this.constAtks[index] = 0;
      this.constDefs[index] = 0;
    }
    for (int index = 0; index < 6; ++index)
      this.constTols[index] = 0;
    this.skillSupport.Clear();
    this.ability.Clear();
  }

  public void Parse(CharaInfo.EquipItem item, EquipItemTable.EquipItemData data)
  {
    this._Reset();
    this.type = data.type;
    this.spAttackType = data.spAttackType;
    GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(data.growID, (uint) item.lv);
    if (growEquipItemData == null)
    {
      this.baseStatus.hp = (int) data.baseHp;
      this.baseStatus.attacks[0] = (int) data.baseAtk;
      this.baseStatus.defences[0] = (int) data.baseDef;
      for (int index = 0; index < 6; ++index)
      {
        this.baseStatus.attacks[index + 1] = data.atkElement[index];
        this.baseStatus.tolerances[index] = data.defElement[index];
      }
    }
    else
    {
      this.baseStatus.hp = growEquipItemData.GetGrowParamHp((int) data.baseHp);
      this.baseStatus.attacks[0] = growEquipItemData.GetGrowParamAtk((int) data.baseAtk);
      this.baseStatus.defences[0] = growEquipItemData.GetGrowParamDef((int) data.baseDef);
      int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(data.atkElement);
      int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(data.defElement);
      for (int index = 0; index < 6; ++index)
      {
        this.baseStatus.attacks[index + 1] = growParamElemAtk[index];
        this.baseStatus.tolerances[index] = growParamElemDef[index];
      }
    }
    EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = data.GetExceedParam((uint) item.exceed);
    if (exceedParam != null)
    {
      this.baseStatus.hp += (int) exceedParam.hp;
      this.baseStatus.attacks[0] += (int) exceedParam.atk;
      this.baseStatus.defences[0] += (int) exceedParam.def;
      for (int index = 0; index < 6; ++index)
      {
        this.baseStatus.attacks[index + 1] += exceedParam.atkElement[index];
        this.baseStatus.tolerances[index] += exceedParam.defElement[index];
      }
    }
    int num = 0;
    for (int count = item.sIds.Count; num < count; ++num)
    {
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) item.sIds[num]);
      GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, item.sLvs[num], item.GetSkillExceed(num));
      this.constHp += growSkillItemData.GetGrowParamHp((int) skillItemData.baseHp);
      this.constAtks[0] += growSkillItemData.GetGrowParamAtk((int) skillItemData.baseAtk);
      this.constDefs[0] += growSkillItemData.GetGrowParamDef((int) skillItemData.baseDef);
      int[] growParamElemAtk = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
      int[] growParamElemDef = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
      for (int index = 0; index < 6; ++index)
      {
        this.constAtks[index + 1] += growParamElemAtk[index];
        this.constTols[index] += growParamElemDef[index];
      }
      if (skillItemData.IsPassive())
      {
        int index = 0;
        for (int length = skillItemData.supportType.Length; index < length; ++index)
        {
          if (skillItemData.supportType[index] != BuffParam.BUFFTYPE.NONE)
            this.skillSupport.Add(new EquipValue.SkillSupport(skillItemData.supportPassiveEqType[index], skillItemData.supportType[index], growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, index), skillItemData.supportPassiveSpAttackType));
        }
      }
    }
    int index1 = 0;
    for (int count = item.aIds.Count; index1 < count; ++index1)
    {
      int aId = item.aIds[index1];
      int aPt = item.aPts[index1];
      if (this.ability.ContainsKey(aId))
        this.ability[aId] += aPt;
      else
        this.ability.Add(aId, aPt);
    }
    int index2 = 0;
    for (int length = data.fixedAbility.Length; index2 < length; ++index2)
    {
      EquipItem.Ability ability = data.fixedAbility[index2];
      if (this.ability.ContainsKey(ability.id))
        this.ability[ability.id] += ability.pt;
      else
        this.ability.Add(ability.id, ability.pt);
    }
    if (exceedParam == null)
      return;
    int index3 = 0;
    for (int length = exceedParam.ability.Length; index3 < length; ++index3)
    {
      EquipItem.Ability ability = exceedParam.ability[index3];
      if (this.ability.ContainsKey(ability.id))
        this.ability[ability.id] += ability.pt;
      else
        this.ability.Add(ability.id, ability.pt);
    }
  }

  public class SkillSupport
  {
    public ENABLE_EQUIP_TYPE targetEquip;
    public BuffParam.BUFFTYPE type;
    public SP_ATTACK_TYPE targetSpAttackType;
    public int value;

    public SkillSupport(
      ENABLE_EQUIP_TYPE e,
      BuffParam.BUFFTYPE t,
      int v,
      SP_ATTACK_TYPE spAttackType)
    {
      this.targetEquip = e;
      this.type = t;
      this.value = v;
      this.targetSpAttackType = spAttackType;
    }
  }
}

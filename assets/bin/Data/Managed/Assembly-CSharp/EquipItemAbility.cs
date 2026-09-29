// Decompiled with JetBrains decompiler
// Type: EquipItemAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class EquipItemAbility
{
  public uint id;
  public int ap;

  public EquipItemAbility(uint _id, int _ap)
  {
    this.id = _id;
    this.ap = _ap;
  }

  public string GetName()
  {
    AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(this.id);
    return ability == null ? string.Empty : ability.name;
  }

  public string GetNameAndAP()
  {
    return this.ap >= 0 ? $"{this.GetName()} +{this.ap}" : $"{this.GetName()} {this.ap}";
  }

  public string GetAP() => this.ap >= 0 ? $"+{this.ap}" : $"{this.ap}";

  public string GetDescription()
  {
    return (Singleton<AbilityDataTable>.I.GetAbilityData(this.id, this.ap) ?? Singleton<AbilityDataTable>.I.GetMinimumAbilityData(this.id)).description;
  }

  public bool IsNeedUpdate()
  {
    AbilityDataTable.AbilityData minimumAbilityData = Singleton<AbilityDataTable>.I.GetMinimumAbilityData(this.id);
    return minimumAbilityData != null && minimumAbilityData.HasNeedUpdateAbility();
  }

  public bool IsActiveAbility()
  {
    AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(this.id);
    return ability != null && ability.IsActive();
  }

  public EquipItemAbility Inverse() => new EquipItemAbility(this.id, -this.ap);
}

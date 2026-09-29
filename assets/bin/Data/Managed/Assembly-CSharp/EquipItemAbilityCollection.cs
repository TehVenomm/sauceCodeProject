// Decompiled with JetBrains decompiler
// Type: EquipItemAbilityCollection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class EquipItemAbilityCollection
{
  private static int EQUIP_LENGTH;

  public EquipItemAbility ability { private set; get; }

  public int[] equip { private set; get; }

  public int[] swapValue { private set; get; }

  public EquipItemAbilityCollection(
    EquipItemAbility a,
    int equip_index,
    EquipItemAbilityCollection.COLLECTION_TYPE type = EquipItemAbilityCollection.COLLECTION_TYPE.NORMAL)
  {
    if (EquipItemAbilityCollection.EQUIP_LENGTH == 0)
    {
      EquipItemAbilityCollection.EQUIP_LENGTH = 3;
      EQUIPMENT_TYPE[] values = (EQUIPMENT_TYPE[]) Enum.GetValues(typeof (EQUIPMENT_TYPE));
      int index = 0;
      for (int length = values.Length; index < length; ++index)
      {
        int num = (int) values[index];
        if (num >= 100 && num <= 400)
          ++EquipItemAbilityCollection.EQUIP_LENGTH;
      }
    }
    this.equip = new int[EquipItemAbilityCollection.EQUIP_LENGTH];
    this.swapValue = new int[EquipItemAbilityCollection.EQUIP_LENGTH];
    if (type != EquipItemAbilityCollection.COLLECTION_TYPE.NORMAL)
      this.swapValue[equip_index] += a.ap;
    if (type != EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_OUT && equip_index < EquipItemAbilityCollection.EQUIP_LENGTH && equip_index >= 0)
    {
      this.equip[equip_index] += a.ap;
      this.ability = new EquipItemAbility(a.id, a.ap);
    }
    else
      this.ability = new EquipItemAbility(a.id, 0);
  }

  public void Add(int ad_ap, int equip_index, EquipItemAbilityCollection.COLLECTION_TYPE type = EquipItemAbilityCollection.COLLECTION_TYPE.NORMAL)
  {
    if (type != EquipItemAbilityCollection.COLLECTION_TYPE.NORMAL)
      this.swapValue[equip_index] += ad_ap;
    if (type == EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_OUT)
      return;
    this.ability.ap += ad_ap;
    this.equip[equip_index] += ad_ap;
  }

  public string GetAP(int index)
  {
    if (index >= EquipItemAbilityCollection.EQUIP_LENGTH || index < 0 || this.equip[index] == 0 && this.swapValue[index] == 0)
      return string.Empty;
    if (this.swapValue[index] != 0 && this.equip[index] == 0)
      return "-";
    int num = this.equip[index];
    return string.Format(num >= 0 ? "+{0}" : "{0}", (object) num);
  }

  public int GetSwapBalance()
  {
    int total = 0;
    Array.ForEach<int>(this.swapValue, (Action<int>) (num => total += num));
    return total;
  }

  public bool IsAbilityOn()
  {
    AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(this.ability.id, this.ability.ap);
    if (abilityData == null)
      return false;
    AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(this.ability.id);
    return !abilityData.HasNeedUpdateAbility() && ability.IsActive();
  }

  public class SwapData
  {
    public int index;
    public EquipItemInfo item;

    public SwapData(int _index, EquipItemInfo _item)
    {
      this.index = _index;
      this.item = _item;
    }
  }

  public enum COLLECTION_TYPE
  {
    NORMAL,
    SWAP_IN,
    SWAP_OUT,
  }
}

// Decompiled with JetBrains decompiler
// Type: AbilityItemLotTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AbilityItemLotTable : Singleton<AbilityItemLotTable>, IDataTable
{
  public const int INFO_MAX = 3;
  private UIntKeyTable<AbilityItemLotTable.AbilityItemLot> abilityItemLot;

  public void CreateTable(string csv_text)
  {
    this.abilityItemLot = TableUtility.CreateUIntKeyTable<AbilityItemLotTable.AbilityItemLot>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<AbilityItemLotTable.AbilityItemLot>(AbilityItemLotTable.AbilityItemLot.cb), "id,itemId,abilityType,target,spTarget,spAttackType,unlockEventId,format,minValue,maxValue,lotGroup,rate");
    this.abilityItemLot.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<AbilityItemLotTable.AbilityItemLot>(this.abilityItemLot, csv_text, new TableUtility.CallBackUIntKeyReadCSV<AbilityItemLotTable.AbilityItemLot>(AbilityItemLotTable.AbilityItemLot.cb), "id,itemId,abilityType,target,spTarget,spAttackType,unlockEventId,format,minValue,maxValue,lotGroup,rate");
  }

  public AbilityItemLotTable.AbilityItemLot GetAbilityItemLot(uint id)
  {
    if (this.abilityItemLot == null)
      return (AbilityItemLotTable.AbilityItemLot) null;
    AbilityItemLotTable.AbilityItemLot abilityItemLot = this.abilityItemLot.Get(id);
    if (abilityItemLot == null)
    {
      Log.TableError((object) this, id);
      abilityItemLot = new AbilityItemLotTable.AbilityItemLot();
    }
    return abilityItemLot;
  }

  public void ForEach(Action<AbilityItemLotTable.AbilityItemLot> cb)
  {
    this.abilityItemLot.ForEach(cb);
  }

  public class AbilityItemLot
  {
    public uint id;
    public uint itemId;
    public ABILITY_TYPE abilityType;
    public string target;
    public string spTarget;
    public string spAttackType;
    public int unlockEventId;
    public string format;
    public int minValue;
    public int maxValue;
    public int lotGroup;
    public int rate;
    public const string NT = "id,itemId,abilityType,target,spTarget,spAttackType,unlockEventId,format,minValue,maxValue,lotGroup,rate";

    public static bool cb(
      CSVReader csv_reader,
      AbilityItemLotTable.AbilityItemLot data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.itemId);
      if (!CSVReader.PopResult.IsParseSucceeded(csv_reader.PopEnum<ABILITY_TYPE>(ref data.abilityType, ABILITY_TYPE.NONE)))
        data.abilityType = ABILITY_TYPE.NEED_UPDATE;
      csv_reader.Pop(ref data.target);
      csv_reader.Pop(ref data.spTarget);
      csv_reader.Pop(ref data.spAttackType);
      int num = 0;
      csv_reader.Pop(ref num);
      data.unlockEventId = num;
      csv_reader.Pop(ref data.format);
      csv_reader.Pop(ref data.minValue);
      csv_reader.Pop(ref data.maxValue);
      csv_reader.Pop(ref data.lotGroup);
      csv_reader.Pop(ref data.rate);
      return true;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: AbilityTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AbilityTable : Singleton<AbilityTable>, IDataTable
{
  public const int INFO_MAX = 3;
  private UIntKeyTable<AbilityTable.Ability> abilityTable;

  public void CreateTable(string csv_text)
  {
    this.abilityTable = TableUtility.CreateUIntKeyTable<AbilityTable.Ability>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<AbilityTable.Ability>(AbilityTable.Ability.cb), "abilityId,name,iconId,startDate,endDate,unlockEventId");
    this.abilityTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<AbilityTable.Ability>(this.abilityTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<AbilityTable.Ability>(AbilityTable.Ability.cb), "abilityId,name,iconId,startDate,endDate,unlockEventId");
  }

  public AbilityTable.Ability GetAbility(uint id)
  {
    if (this.abilityTable == null)
      return (AbilityTable.Ability) null;
    AbilityTable.Ability ability = this.abilityTable.Get(id);
    if (ability == null)
    {
      Log.TableError((object) this, id);
      ability = new AbilityTable.Ability();
      ability.name = Log.NON_DATA_NAME;
    }
    return ability;
  }

  public void ForEach(Action<AbilityTable.Ability> cb) => this.abilityTable.ForEach(cb);

  public class Ability
  {
    public uint id;
    public string name;
    public int iconId;
    public DateTime? startDate;
    public DateTime? endDate;
    public int unlockEventId;
    public const string NT = "abilityId,name,iconId,startDate,endDate,unlockEventId";

    public static bool cb(CSVReader csv_reader, AbilityTable.Ability data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.iconId);
      string s1 = "";
      csv_reader.Pop(ref s1);
      DateTime result;
      if (!string.IsNullOrEmpty(s1) && DateTime.TryParse(s1, out result))
        data.startDate = new DateTime?(result);
      string s2 = "";
      csv_reader.Pop(ref s2);
      if (!string.IsNullOrEmpty(s2) && DateTime.TryParse(s2, out result))
        data.endDate = new DateTime?(result);
      int num = 0;
      csv_reader.Pop(ref num);
      data.unlockEventId = num;
      return true;
    }

    public bool IsActive(DateTime checkedTime)
    {
      return (!this.startDate.HasValue || !(checkedTime < this.startDate.Value)) && (!this.endDate.HasValue || !(checkedTime >= this.endDate.Value));
    }

    public bool IsActive() => this.IsActive(TimeManager.GetNow());
  }
}

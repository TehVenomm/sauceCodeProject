// Decompiled with JetBrains decompiler
// Type: Network.AbilityItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Network;

[Serializable]
public class AbilityItem
{
  public string uniqId;
  public int abilityItemId;
  public string equipItemUniqId;
  public List<AbilityItem.Data> data = new List<AbilityItem.Data>();

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0},", (object) this.uniqId);
    stringBuilder.AppendFormat("{0},", (object) this.abilityItemId);
    stringBuilder.AppendFormat("{0},", (object) this.equipItemUniqId);
    int index = 0;
    for (int count = this.data.Count; index < count; ++index)
    {
      stringBuilder.Append("d(");
      stringBuilder.AppendFormat("{0},", (object) this.data[index].abilityType);
      stringBuilder.AppendFormat("{0},", (object) this.data[index].value);
      stringBuilder.AppendFormat("{0},", (object) this.data[index].target);
      stringBuilder.AppendFormat("{0},", (object) this.data[index].spTarget);
      stringBuilder.AppendFormat("{0},", (object) this.data[index].spAttackType);
      stringBuilder.Append("),");
    }
    return base.ToString() + stringBuilder.ToString();
  }

  public class Data
  {
    public int abilityItemLotId;
    public string abilityType;
    public int value;
    public string target;
    public string spTarget;
    public string spAttackType;
    public string format;
  }
}

// Decompiled with JetBrains decompiler
// Type: SortComparison
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class SortComparison
{
  public Comparison<SortCompareData> comparison;

  public SortComparison(bool is_asc) => this.SetComparison(is_asc);

  private void SetComparison(bool order_type_asc)
  {
    if (order_type_asc)
      this.comparison = new Comparison<SortCompareData>(this.Compare);
    else
      this.comparison = new Comparison<SortCompareData>(this.Compare_Desc);
  }

  private int Compare(SortCompareData lp, SortCompareData rp)
  {
    if (lp.sortingData == rp.sortingData)
    {
      if (lp.GetUniqID() > rp.GetUniqID())
        return 1;
      return lp.GetUniqID() < rp.GetUniqID() ? -1 : 0;
    }
    return lp.sortingData > rp.sortingData ? 1 : -1;
  }

  private int Compare_Desc(SortCompareData lp, SortCompareData rp)
  {
    if (rp.sortingData == lp.sortingData)
    {
      if (rp.GetUniqID() > lp.GetUniqID())
        return 1;
      if (rp.GetUniqID() < lp.GetUniqID())
        return -1;
      if (rp.GetTableID() > lp.GetTableID())
        return 1;
      return rp.GetTableID() < lp.GetTableID() ? -1 : 0;
    }
    return rp.sortingData > lp.sortingData ? 1 : -1;
  }
}

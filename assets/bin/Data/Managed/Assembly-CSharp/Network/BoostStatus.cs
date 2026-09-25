// Decompiled with JetBrains decompiler
// Type: Network.BoostStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class BoostStatus
{
  public int type;
  public int value;
  public EndDate endDate;
  public int endTimestamp;

  public USE_ITEM_EFFECT_TYPE Type => (USE_ITEM_EFFECT_TYPE) this.type;

  public string GetBoostRateText()
  {
    return ((float) (100 + this.value) / 100f).ToString(".#") + StringTable.Get(STRING_CATEGORY.STATUS, 1000U);
  }

  public string GetRemainTime() => UIUtility.TimeFormat(this.endDate.CalcRemainTime());

  public bool IsRemain()
  {
    if (this.endDate == null)
      return this.value != 0;
    DateTime now = TimeManager.GetNow();
    return DateTime.Parse(this.endDate.date).Subtract(now).TotalSeconds > 0.0;
  }
}

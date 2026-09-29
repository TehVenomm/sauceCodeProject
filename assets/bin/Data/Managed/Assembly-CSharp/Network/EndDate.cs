// Decompiled with JetBrains decompiler
// Type: Network.EndDate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class EndDate
{
  public string date;
  public int timezone_type;
  public string timezone;

  public DateTime ConvToDateTime()
  {
    DateTime dateTime = DateTime.Parse(this.date);
    dateTime = dateTime.AddSeconds(1.0);
    return dateTime;
  }

  public TimeSpan CalcRemainTime()
  {
    DateTime now = TimeManager.GetNow();
    return this.ConvToDateTime().Subtract(now);
  }
}

// Decompiled with JetBrains decompiler
// Type: Network.ExpiredItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class ExpiredItem
{
  public string uniqId;
  public int itemId;
  public int used;
  public string expiredAt;

  public bool CanUse()
  {
    return (string.IsNullOrEmpty(this.expiredAt) ? 1 : (TimeManager.GetRemainTime(this.expiredAt).CompareTo(TimeSpan.Zero) > 0 ? 1 : 0)) != 0 && this.used == 0;
  }
}

// Decompiled with JetBrains decompiler
// Type: Network.HomeBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class HomeBanner
{
  public int id;
  public int bannerId;
  public int homeType;
  public string targetString;
  public EndDate endDate = new EndDate();

  public HOME_TYPE HomeType => (HOME_TYPE) this.homeType;
}

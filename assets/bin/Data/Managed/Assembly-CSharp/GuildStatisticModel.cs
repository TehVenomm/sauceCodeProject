// Decompiled with JetBrains decompiler
// Type: GuildStatisticModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class GuildStatisticModel : BaseModel
{
  public static string URL = "clan/ClanStatistics.go";
  public GuildStatisticInfo result;

  public class Form
  {
    public int clanId;
  }
}

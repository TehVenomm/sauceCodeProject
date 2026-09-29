// Decompiled with JetBrains decompiler
// Type: GuildChangeSettingModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GuildChangeSettingModel : BaseModel
{
  public static string URL = "clan/ClanSettings.go";
  public GuildChangeSettingModel.Result result = new GuildChangeSettingModel.Result();

  public class Result
  {
    public int level;
    public string description;
    public int currentMem;
    public int memberNum;
    public int privacy;
    public string createAt;
    public string clanName;
    public int location;
    public string tag;
    public int[] emblem;
    public int memCap;
    public int minLevel;
    public int exp;
  }
}

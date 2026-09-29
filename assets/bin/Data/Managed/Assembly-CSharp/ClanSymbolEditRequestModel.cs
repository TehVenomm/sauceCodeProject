// Decompiled with JetBrains decompiler
// Type: ClanSymbolEditRequestModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ClanSymbolEditRequestModel : BaseModel
{
  public static string URL = "ajax/clan/edit-symbol";

  public class RequestSendForm
  {
    public int mark;
    public int markOption;
    public int frame;
    public int frameOption;
    public int pattern;
    public int patternOption;
  }
}

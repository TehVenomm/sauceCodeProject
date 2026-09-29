// Decompiled with JetBrains decompiler
// Type: DebugSetBoostModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class DebugSetBoostModel : BaseModel
{
  public static string URL = "ajax/debug/setboost";

  public class RequestSendForm
  {
    public int type;
    public int value;
    public string end;
  }
}

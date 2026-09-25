// Decompiled with JetBrains decompiler
// Type: DebugAddPresentModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class DebugAddPresentModel : BaseModel
{
  public static string URL = "ajax/debug/addpresent";
  public Present result = new Present();

  public class RequestSendForm
  {
    public int type;
    public int actionType;
    public string comment;
    public int num;
    public int id;
    public int p0;
    public int p1;
  }
}

// Decompiled with JetBrains decompiler
// Type: DebugChangeGatherTimeModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class DebugChangeGatherTimeModel : BaseModel
{
  public static string URL = "ajax/debug/changegathertime";

  public class RequestSendForm
  {
    public int pid;
    public string appear;
    public string disappear;
    public string gatherStart;
    public string gatherEnd;
  }
}

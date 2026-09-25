// Decompiled with JetBrains decompiler
// Type: LoungeLeaveModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class LoungeLeaveModel : BaseModel
{
  public static string URL = "ajax/lounge/leave";
  public LoungeLeaveModel.Param result = new LoungeLeaveModel.Param();

  public class Param
  {
    public LoungeModel.Lounge lounge;
  }

  public class RequestSendForm
  {
    public string id;
  }
}

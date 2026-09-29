// Decompiled with JetBrains decompiler
// Type: FieldLeaveModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class FieldLeaveModel : BaseModel
{
  public static string URL = "ajax/field/leave";

  public class RequestSendForm
  {
    public int toHome;
    public int retire;
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
  }
}

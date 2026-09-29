// Decompiled with JetBrains decompiler
// Type: RegistAuthRobModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RegistAuthRobModel : BaseModel
{
  public static string URL = "ajax/regist/authrob";
  public RegistAuthRobModel.Param result = new RegistAuthRobModel.Param();

  public class Param : RegistCreateModel.Param
  {
    public int newsId = 1;
  }

  public class RequestSendForm
  {
    public string email;
    public string password;
    public string uid;
    public string d;
  }
}

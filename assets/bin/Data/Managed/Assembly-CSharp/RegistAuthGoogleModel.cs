// Decompiled with JetBrains decompiler
// Type: RegistAuthGoogleModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RegistAuthGoogleModel : BaseModel
{
  public static string URL = "ajax/regist/authgoogle";
  public RegistAuthRobModel.Param result = new RegistAuthRobModel.Param();

  public class RequestSendForm
  {
    public string account;
    public string accountKey;
    public string password;
    public string d;
    public int purchasetype;
  }
}

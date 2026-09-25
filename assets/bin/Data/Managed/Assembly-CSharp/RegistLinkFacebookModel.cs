// Decompiled with JetBrains decompiler
// Type: RegistLinkFacebookModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RegistLinkFacebookModel : BaseModel
{
  public static string URL = "ajax/regist/linkfacebook";
  public Network.UserInfo result = new Network.UserInfo();
  public RegistLinkFacebookModel.ExistInfoParam existInfo = new RegistLinkFacebookModel.ExistInfoParam();

  public class ExistInfoParam
  {
    public int id;
    public string name;
    public int level;
    public string code;
    public int crystal;
    public int money;
    public string lastLogin;
  }

  public class RequestOverrideSendForm
  {
    public string accessToken;
    public int overwriteOldData;
  }

  public class RequestSendForm
  {
    public string accessToken;
  }
}

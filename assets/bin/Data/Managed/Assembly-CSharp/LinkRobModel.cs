// Decompiled with JetBrains decompiler
// Type: LinkRobModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class LinkRobModel : BaseModel
{
  public static string URL = "ajax/regist/linkrob";
  public Network.UserInfo result = new Network.UserInfo();
  public LinkRobModel.ExistInfoParam existInfo = new LinkRobModel.ExistInfoParam();

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

  public class RequestSendForm
  {
    public string email;
    public string password;
  }
}

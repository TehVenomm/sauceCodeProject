// Decompiled with JetBrains decompiler
// Type: CheckRegisterModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class CheckRegisterModel : BaseModel
{
  public static string URL = "ajax/regist/checkregister";
  public CheckRegisterModel.Param result = new CheckRegisterModel.Param();

  public class Param
  {
    public string uh = "";
    public int userId;
    public string userIdHash;
    public Network.UserInfo userInfo = new Network.UserInfo();
    public int newsId = 1;
    public int tutorialStep;
    public bool sendAsset;
    public bool termsCheck;
    public string termsUpdateDay = "";
    public bool recommendUpdate;
  }

  public class RequestSendForm
  {
    public string d;
    public string data;
  }
}

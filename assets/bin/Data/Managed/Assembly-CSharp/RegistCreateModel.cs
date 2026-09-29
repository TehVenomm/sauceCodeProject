// Decompiled with JetBrains decompiler
// Type: RegistCreateModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RegistCreateModel : BaseModel
{
  public static string URL = "ajax/regist/create";
  public RegistCreateModel.Param result = new RegistCreateModel.Param();

  public class Param
  {
    public string uh = "";
    public int userId;
    public string userIdHash;
    public bool guestUser;
    public Network.UserInfo userInfo = new Network.UserInfo();
  }
}

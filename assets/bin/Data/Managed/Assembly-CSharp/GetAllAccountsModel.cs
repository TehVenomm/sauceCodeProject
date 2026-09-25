// Decompiled with JetBrains decompiler
// Type: GetAllAccountsModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class GetAllAccountsModel : BaseModel
{
  public static string URL = "ajax/regist/getallaccounts";
  public List<GetAllAccountsModel.ServerAccountInfo> result = new List<GetAllAccountsModel.ServerAccountInfo>();

  public class RequestSendForm
  {
    public string env;
  }

  public class ServerAccountInfo
  {
    public string email;
    public string fbId;
    public string uid;
    public int level;
    public int crystal;
    public int money;
    public Network.UserInfo userAccount;
  }
}

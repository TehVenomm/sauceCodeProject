// Decompiled with JetBrains decompiler
// Type: ClanChatMessageUpdateModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class ClanChatMessageUpdateModel : BaseModel
{
  public static string URL = "ajax/clan-message/messageupdate";
  public ClanChatMessageUpdateModel.Param result = new ClanChatMessageUpdateModel.Param();

  public class Param
  {
    public List<ClanChatMessageModel> messages = new List<ClanChatMessageModel>();
    public int displayNum;
    public int updateInterval = 10;
  }

  public class RequestSendForm
  {
    public string cLatestId;
  }
}

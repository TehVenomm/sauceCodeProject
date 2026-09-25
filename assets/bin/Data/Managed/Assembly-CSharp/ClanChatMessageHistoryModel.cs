// Decompiled with JetBrains decompiler
// Type: ClanChatMessageHistoryModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class ClanChatMessageHistoryModel : BaseModel
{
  public static string URL = "ajax/clan-message/messagehistory";
  public ClanChatMessageHistoryModel.Param result = new ClanChatMessageHistoryModel.Param();

  public class Param
  {
    public List<ClanChatMessageModel> messages = new List<ClanChatMessageModel>();
    public bool isRemaining;
    public int displayNum;
    public int updateInterval = 10;
  }

  public class RequestSendForm
  {
    public string fromId;
  }
}

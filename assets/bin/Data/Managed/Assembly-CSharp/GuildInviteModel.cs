// Decompiled with JetBrains decompiler
// Type: GuildInviteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class GuildInviteModel : BaseModel
{
  public static string URL = "clan/AtvInvite.go";

  public class RequestSendForm
  {
    public string id;
    public List<int> inviteList = new List<int>();
  }
}

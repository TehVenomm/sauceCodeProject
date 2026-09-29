// Decompiled with JetBrains decompiler
// Type: GuildChatPinModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GuildChatPinModel : BaseModel
{
  public static string URL = "clan/ChatPin.go";
  public GuildChatPinModel.Param result = new GuildChatPinModel.Param();

  [Serializable]
  public class Param
  {
    public int id;
    public string uuid;
    public string message;
    public int type;
    public CharaInfo charInfo;
  }

  public class SendForm
  {
    public int id;
    public string uuid;
    public string message;
    public int type;
    public int fromUserId;
  }
}

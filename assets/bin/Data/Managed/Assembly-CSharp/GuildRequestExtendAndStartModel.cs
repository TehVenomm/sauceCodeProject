// Decompiled with JetBrains decompiler
// Type: GuildRequestExtendAndStartModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GuildRequestExtendAndStartModel : BaseModel
{
  public static string URL = "ajax/guild-request/extend-and-start";

  public class RequestSendForm
  {
    public int slotNo;
    public int questId;
    public int num;
    public int isQuestItem;
    public int crystalCL;
  }
}

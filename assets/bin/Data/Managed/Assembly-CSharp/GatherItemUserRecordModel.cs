// Decompiled with JetBrains decompiler
// Type: GatherItemUserRecordModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class GatherItemUserRecordModel : BaseModel
{
  public static string URL = "ajax/gather-item/user-record";
  public GatherItemUserRecordModel.Param result = new GatherItemUserRecordModel.Param();

  public class Param
  {
    public int totalNum;
    public List<GatherItemRecord> gatherItems = new List<GatherItemRecord>();
  }

  public class RequestSendForm
  {
    public int userId;
    public int eventId;
  }
}

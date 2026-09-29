// Decompiled with JetBrains decompiler
// Type: ArenaUserRecordModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class ArenaUserRecordModel : BaseModel
{
  public static string URL = "ajax/arena/user-record";
  public ArenaUserRecordModel.Param result = new ArenaUserRecordModel.Param();

  [Serializable]
  public class Param
  {
    public int userRank;
    public List<int> clearMilliSecList;
    public int totalMilliSec;
  }

  public class RequestSendForm
  {
    public int userId;
    public int eventId;
  }
}

// Decompiled with JetBrains decompiler
// Type: TheaterModeStatusModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class TheaterModeStatusModel : BaseModel
{
  public static string URL = "ajax/thater/list";
  public TheaterModeStatusModel.Param result = new TheaterModeStatusModel.Param();

  [Serializable]
  public class Param
  {
    public List<TheaterModeGetData> theaterList = new List<TheaterModeGetData>();
  }

  public class RequestSendForm
  {
    public List<TheaterModeStatusModel.TheaterModeStatusData> dataList = new List<TheaterModeStatusModel.TheaterModeStatusData>();
  }

  public class TheaterModeStatusData
  {
    public int theaterId;
    public int statusId;
  }
}

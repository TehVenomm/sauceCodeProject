// Decompiled with JetBrains decompiler
// Type: SerialListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class SerialListModel : BaseModel
{
  public static string URL = "ajax/serial/list";
  public SerialListModel.Param result = new SerialListModel.Param();

  [Serializable]
  public class Param
  {
    public List<SerialListModel.Serials> serials = new List<SerialListModel.Serials>();
  }

  [Serializable]
  public class Serials
  {
    public int serialId;
    public string name;
    public EndDate endDate = new EndDate();
  }
}

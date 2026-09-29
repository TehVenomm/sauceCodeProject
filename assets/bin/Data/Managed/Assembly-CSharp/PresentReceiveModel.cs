// Decompiled with JetBrains decompiler
// Type: PresentReceiveModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class PresentReceiveModel : BaseModel
{
  public static string URL = "ajax/present/receive";
  public PresentReceiveModel.Param result = new PresentReceiveModel.Param();

  [Serializable]
  public class Param
  {
    public int receivePresentNum;
    public PresentList list;
  }

  public class RequestSendForm
  {
    public List<string> uids = new List<string>();
    public int page;
  }
}

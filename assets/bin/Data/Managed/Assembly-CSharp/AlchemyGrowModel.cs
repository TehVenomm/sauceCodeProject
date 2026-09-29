// Decompiled with JetBrains decompiler
// Type: AlchemyGrowModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class AlchemyGrowModel : BaseModel
{
  public static string URL = "ajax/alchemy/grow";
  public AlchemyGrowModel.Param result = new AlchemyGrowModel.Param();

  [Serializable]
  public class Param
  {
    public bool greatSuccess;
  }

  public class RequestSendForm
  {
    public string suid;
    public List<string> uuids = new List<string>();
    public List<AlchemyGrowModel.RequestSendForm.MagiItem> uiuids = new List<AlchemyGrowModel.RequestSendForm.MagiItem>();

    [Serializable]
    public class MagiItem
    {
      public string uiuid;
      public int num;
    }
  }
}

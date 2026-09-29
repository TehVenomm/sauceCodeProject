// Decompiled with JetBrains decompiler
// Type: AlchemyExceedModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class AlchemyExceedModel : BaseModel
{
  public static string URL = "ajax/alchemy/exceed";
  public AlchemyExceedModel.Param result = new AlchemyExceedModel.Param();

  [Serializable]
  public class Param
  {
    public bool greatSuccess;
  }

  public class RequestSendForm
  {
    public string suid;
    public List<string> uuids = new List<string>();
  }
}

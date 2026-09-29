// Decompiled with JetBrains decompiler
// Type: SerialInputModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class SerialInputModel : BaseModel
{
  public static string URL = "ajax/serial/input";
  public SerialInputModel.Param result = new SerialInputModel.Param();

  [Serializable]
  public class Param
  {
    public string message;
  }

  public class RequestSendForm
  {
    public int id;
    public string code;
  }
}

// Decompiled with JetBrains decompiler
// Type: DebugAddFollowerModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class DebugAddFollowerModel : BaseModel
{
  public static string URL = "ajax/debug/addfollower";

  public class RequestSendForm
  {
    public List<int> ids = new List<int>();
  }
}

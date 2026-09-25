// Decompiled with JetBrains decompiler
// Type: LoungeSearchModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class LoungeSearchModel : BaseModel
{
  public static string URL = "ajax/lounge/search";
  public LoungeSearchModel.Param result = new LoungeSearchModel.Param();

  public class Param
  {
    public List<LoungeModel.Lounge> lounges = new List<LoungeModel.Lounge>();
  }

  public class RequestSendForm
  {
    public int order;
    public int label;
    public string name;
  }
}

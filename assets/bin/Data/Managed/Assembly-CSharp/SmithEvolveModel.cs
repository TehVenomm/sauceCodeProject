// Decompiled with JetBrains decompiler
// Type: SmithEvolveModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class SmithEvolveModel : BaseModel
{
  public static string URL = "ajax/smith/evolve";
  public SmithEvolveModel.Param result = new SmithEvolveModel.Param();

  public class RequestSendForm
  {
    public int vid;
    public string euid;
    public List<string> meids;
  }

  public class Param
  {
    public int evolveCount;
  }
}

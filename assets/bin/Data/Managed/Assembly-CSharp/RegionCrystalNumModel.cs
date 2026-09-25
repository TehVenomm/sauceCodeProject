// Decompiled with JetBrains decompiler
// Type: RegionCrystalNumModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class RegionCrystalNumModel : BaseModel
{
  public static string URL = "ajax/region/crystalnum";
  public RegionCrystalNumModel.Param result = new RegionCrystalNumModel.Param();

  [Serializable]
  public class Param
  {
    public int crystalNum;
    public string text;
  }

  public class RequestSendForm
  {
    public int regionId;
  }
}

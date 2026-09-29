// Decompiled with JetBrains decompiler
// Type: GatherCompleteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GatherCompleteModel : BaseModel
{
  public static string URL = "ajax/gather/complete";
  public GatherCompleteModel.Param result = new GatherCompleteModel.Param();

  [Serializable]
  public class Param : GatherEnterData
  {
    public bool isNewOpen;
    public GatherRewardList reward = new GatherRewardList();
  }

  public class RequestSendForm
  {
    public int pid;
  }
}

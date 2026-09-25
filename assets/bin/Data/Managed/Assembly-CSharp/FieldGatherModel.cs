// Decompiled with JetBrains decompiler
// Type: FieldGatherModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class FieldGatherModel : BaseModel
{
  public static string URL = "ajax/field/gather";
  public FieldGatherModel.Param result = new FieldGatherModel.Param();

  [Serializable]
  public class Param
  {
    public FieldGatherRewardList reward = new FieldGatherRewardList();
  }

  public class RequestSendForm
  {
    public int pId;
  }
}

// Decompiled with JetBrains decompiler
// Type: TradingPostUserAgreementModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class TradingPostUserAgreementModel : BaseModel
{
  public static string URL = "ajax/trading-post/user-agreement";
  public TradingPostUserAgreementModel.Result result = new TradingPostUserAgreementModel.Result();

  [Serializable]
  public class Result
  {
    public int tradingDay;
    public int tradingStatus;
    public int tradingAccept;
  }
}

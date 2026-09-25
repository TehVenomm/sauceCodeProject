// Decompiled with JetBrains decompiler
// Type: PaymentsAupurchaseDepositModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class PaymentsAupurchaseDepositModel : BaseModel
{
  public static string URL = "ajax/payments/aupurchase/deposit";
  public int code;
  public string name;

  public class RequestSendForm
  {
    public string mainToken;
    public string signature;
    public string signedData;
  }
}

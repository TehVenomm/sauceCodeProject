// Decompiled with JetBrains decompiler
// Type: GoPayRequestModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GoPayRequestModel : BaseModel
{
  public static string URL = "ajax/payments-secure/gopay/request";
  public string payload;

  public class SendForm
  {
    public string mainToken;
    public string productId;
  }
}

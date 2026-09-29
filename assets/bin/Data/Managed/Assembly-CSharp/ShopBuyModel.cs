// Decompiled with JetBrains decompiler
// Type: ShopBuyModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ShopBuyModel : BaseModel
{
  public static string URL = "ajax/shop/buy";
  public ShopBuyResult result = new ShopBuyResult();

  public class RequestSendForm
  {
    public int id;
    public int crystalCL;
  }
}

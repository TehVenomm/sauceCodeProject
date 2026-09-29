// Decompiled with JetBrains decompiler
// Type: InventoryAutoItemModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class InventoryAutoItemModel : BaseModel
{
  public static string URL = "ajax/inventory/useautopotion";
  public InventoryAutoItemModel.Param result = new InventoryAutoItemModel.Param();

  public class Param
  {
    public double timeLeft;
  }

  public class RequestSendForm
  {
    public string uid;
  }
}

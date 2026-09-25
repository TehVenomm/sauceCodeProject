// Decompiled with JetBrains decompiler
// Type: DeliveryDropRareTextColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class DeliveryDropRareTextColor : MonoBehaviour
{
  public Color NormalColor;
  public Color RareColor;
  public Color SuperRareColor;

  public Color GetRarityColor(DELIVERY_DROP_DIFFICULTY type)
  {
    switch (type)
    {
      case DELIVERY_DROP_DIFFICULTY.RARE:
        return this.RareColor;
      case DELIVERY_DROP_DIFFICULTY.SUPER_RARE:
        return this.SuperRareColor;
      default:
        return this.NormalColor;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: UIOracleStockIconController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIOracleStockIconController : MonoBehaviour
{
  [SerializeField]
  private UISprite baseSprite;
  [SerializeField]
  private UISprite stockSprite;

  public bool Stocked => ((Component) this.stockSprite).gameObject.activeSelf;

  public void Initialize(int index, int depth, bool stocked = false)
  {
    this.baseSprite.depth = depth;
    this.stockSprite.depth = depth + 1;
    this.Stock(stocked);
  }

  public void Stock(bool enable)
  {
    if (enable == ((Component) this.stockSprite).gameObject.activeSelf)
      return;
    ((Component) this.stockSprite).gameObject.SetActive(enable);
  }
}

// Decompiled with JetBrains decompiler
// Type: UIOracleStockUIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIOracleStockUIController : MonoBehaviour
{
  private static readonly string stockIconPath = "InternalUI/UI_InGame/Oracle/InGameUIOracleStockIcon";
  [SerializeField]
  private GameObject iconsRoot;
  [SerializeField]
  private UISprite baseSprite;
  [SerializeField]
  private UISprite fulledEffect;
  [SerializeField]
  private int iconMax = 8;
  [SerializeField]
  private int iconWidth = 24;
  [SerializeField]
  private int baseWidth = 126;
  [SerializeField]
  private int baseEffectWidth = 90;
  [SerializeField]
  private float btnColliderExpansion = 20f;
  private List<UIOracleStockIconController> icons = new List<UIOracleStockIconController>();

  public int StockedCount
  {
    get
    {
      int count = 0;
      this.icons.ForEach((Action<UIOracleStockIconController>) (o =>
      {
        if (!o.Stocked)
          return;
        ++count;
      }));
      return count;
    }
  }

  public int StockMax => this.icons.Count;

  public void Initialize(int max)
  {
    for (int index = 0; index < max && this.icons.Count <= index; ++index)
    {
      UIOracleStockIconController icon = this.CreateIcon(index);
      if (!Object.op_Equality((Object) icon, (Object) null))
        this.icons.Add(icon);
      else
        break;
    }
    this.baseSprite.width = this.baseWidth + this.iconWidth * this.icons.Count;
    BoxCollider component = ((Component) this.baseSprite).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.size = new Vector3((float) this.baseSprite.width, (float) this.baseSprite.height + this.btnColliderExpansion, 1f);
      component.center = new Vector3((float) this.baseSprite.width / 2f, 0.0f, 0.0f);
    }
    this.fulledEffect.width = this.baseEffectWidth + this.iconWidth * this.icons.Count;
  }

  private UIOracleStockIconController CreateIcon(int index)
  {
    Transform transform = ResourceUtility.Realizes(Resources.Load(UIOracleStockUIController.stockIconPath), this.iconsRoot.transform);
    if (Object.op_Equality((Object) transform, (Object) null))
    {
      Debug.LogError((object) $"failed to create icon({(object) index}). {UIOracleStockUIController.stockIconPath}");
      return (UIOracleStockIconController) null;
    }
    ((Component) transform).transform.localPosition = new Vector3((float) (index * this.iconWidth), 0.0f, 0.0f);
    UIOracleStockIconController component = ((Component) transform).GetComponent<UIOracleStockIconController>();
    component.Initialize(index, Object.op_Equality((Object) this.baseSprite, (Object) null) ? 0 : this.baseSprite.depth + 1);
    return component;
  }

  public void UpdateStock(int stockedCount)
  {
    for (int index = 0; index < this.StockMax; ++index)
      this.icons[index].Stock(stockedCount > index);
    ((Behaviour) this.fulledEffect).enabled = stockedCount >= this.StockMax;
  }

  public void SetActive(bool enabled)
  {
    if (enabled && this.icons.Count > 0)
    {
      this.iconsRoot.SetActive(true);
      ((Component) this.baseSprite).gameObject.SetActive(true);
    }
    else
    {
      this.iconsRoot.SetActive(false);
      ((Component) this.baseSprite).gameObject.SetActive(false);
    }
  }

  public void StartGutsManually()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.self.spearCtrl.StartOracleGutsMode();
  }
}

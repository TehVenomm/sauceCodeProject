// Decompiled with JetBrains decompiler
// Type: PointShopBuy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class PointShopBuy : GameSection
{
  private PointShopItem currentItem;
  private PointShop pointShop;
  private Action<PointShopItem, int> onBuy;
  private int currentNum = 1;
  private int changableNum;

  public override string overrideBackKeyEvent => "NO";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    if (eventData.Length == 3)
    {
      this.currentItem = eventData[0] as PointShopItem;
      this.pointShop = eventData[1] as PointShop;
      this.onBuy = eventData[2] as Action<PointShopItem, int>;
      this.changableNum = Mathf.Min(Mathf.Min(this.currentItem.hasLimit ? this.currentItem.limit - this.currentItem.buyCount : int.MaxValue, this.pointShop.userPoint / this.currentItem.needPoint), GameDefine.POINT_SHOP_MAX_BUY_LIMIT);
    }
    if (this.changableNum == 1)
    {
      this.SetActive((Enum) PointShopBuy.UI.BTN_L, false);
      this.SetActive((Enum) PointShopBuy.UI.BTN_R, false);
    }
    else
    {
      this.SetRepeatButton((Enum) PointShopBuy.UI.BTN_L, "L");
      this.SetRepeatButton((Enum) PointShopBuy.UI.BTN_R, "R");
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    if (this.currentItem != null)
    {
      this.SetLabelText((Enum) PointShopBuy.UI.LBL_ITEM_NAME, this.currentItem.name);
      ItemIcon.CreateRewardItemIcon((REWARD_TYPE) this.currentItem.type, (uint) this.currentItem.itemId, this.GetCtrl((Enum) PointShopBuy.UI.OBJ_ITEM_ICON_ROOT)).SetEnableCollider(false);
      ResourceLoad.LoadPointIconImageTexture(((Component) this.GetCtrl((Enum) PointShopBuy.UI.TEX_POINT_ICON)).GetComponent<UITexture>(), (uint) this.pointShop.pointShopId);
    }
    this.SetLabelText((Enum) PointShopBuy.UI.LBL_CURRENT_CHANGE_NUM, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) this.currentNum));
    this.SetLabelText((Enum) PointShopBuy.UI.LBL_NEED_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) (this.currentItem.needPoint * this.currentNum)));
  }

  private void OnQuery_R()
  {
    if (this.currentNum < this.changableNum)
      ++this.currentNum;
    else
      this.currentNum = 1;
    this.RefreshUI();
  }

  private void OnQuery_L()
  {
    if (this.currentNum > 1)
      --this.currentNum;
    else
      this.currentNum = this.changableNum;
    this.RefreshUI();
  }

  private void OnQuery_NO() => GameSection.BackSection();

  private void OnQuery_YES()
  {
    if (this.onBuy == null || this.currentNum <= 0)
      return;
    this.onBuy(this.currentItem, this.currentNum);
  }

  private enum UI
  {
    TEX_POINT_ICON,
    LBL_NEED_POINT,
    LBL_ITEM_NAME,
    OBJ_ITEM_ICON_ROOT,
    LBL_CURRENT_CHANGE_NUM,
    BTN_R,
    BTN_L,
  }
}

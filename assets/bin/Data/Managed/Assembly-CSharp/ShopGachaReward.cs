// Decompiled with JetBrains decompiler
// Type: ShopGachaReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ShopGachaReward : GameSection
{
  private List<QuestItem.SellItem> sellItem;

  public override void Initialize()
  {
    this.sellItem = GameSection.GetEventData() as List<QuestItem.SellItem>;
    this.sellItem.Sort((Comparison<QuestItem.SellItem>) ((l, r) => l.pri - r.pri));
    base.Initialize();
  }

  public override void UpdateUI()
  {
    QuestItem.SellItem[] data_ary = this.sellItem.ToArray();
    this.SetGrid((Enum) ShopGachaReward.UI.GRD_ICON, (string) null, data_ary.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      uint itemId = (uint) data_ary[i].itemId;
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) data_ary[i].type, itemId, t, data_ary[i].num);
      if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        return;
      rewardItemIcon.SetRewardBG(true);
      this.SetMaterialInfo(rewardItemIcon.transform, (REWARD_TYPE) data_ary[i].type, itemId);
    }));
  }

  private enum UI
  {
    GRD_ICON,
  }
}

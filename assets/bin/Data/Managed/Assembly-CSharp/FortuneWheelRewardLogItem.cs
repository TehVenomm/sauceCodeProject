// Decompiled with JetBrains decompiler
// Type: FortuneWheelRewardLogItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FortuneWheelRewardLogItem : MonoBehaviour
{
  [SerializeField]
  private Transform OBJ_ITEM_ICON;
  [SerializeField]
  private GameObject OBJ_JACKPOT;

  public void InitJackpot()
  {
    this.OBJ_JACKPOT.SetActive(true);
    ((Component) this.OBJ_ITEM_ICON).gameObject.SetActive(false);
  }

  public void InitLog(REWARD_TYPE item_type, uint icon_id, int num)
  {
    this.OBJ_JACKPOT.SetActive(false);
    ((Component) this.OBJ_ITEM_ICON).gameObject.SetActive(true);
    this.OBJ_JACKPOT.SetActive(false);
    ((Component) this.OBJ_ITEM_ICON).gameObject.SetActive(true);
    ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, this.OBJ_ITEM_ICON, num);
    if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
      return;
    rewardItemIcon.SetSpinUserLogIcon();
  }
}

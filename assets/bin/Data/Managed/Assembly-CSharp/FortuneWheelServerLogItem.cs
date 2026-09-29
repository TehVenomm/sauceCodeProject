// Decompiled with JetBrains decompiler
// Type: FortuneWheelServerLogItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FortuneWheelServerLogItem : MonoBehaviour
{
  [SerializeField]
  private UILabel LBL_LOG;
  [SerializeField]
  private Transform OBJ_ITEM_ICON;
  [SerializeField]
  private GameObject OBJ_JACKPOT;

  public void InitJackpot(string textLog)
  {
    this.LBL_LOG.text = textLog;
    float num = (float) ((double) this.LBL_LOG.printedSize.x - 210.0 + 30.0);
    Vector3 localPosition = this.OBJ_JACKPOT.transform.localPosition;
    localPosition.x = num;
    this.OBJ_JACKPOT.transform.localPosition = localPosition;
    this.OBJ_JACKPOT.SetActive(true);
    ((Component) this.OBJ_ITEM_ICON).gameObject.SetActive(false);
  }

  public void InitLog(string textLog, REWARD_TYPE item_type, uint icon_id)
  {
    this.OBJ_JACKPOT.SetActive(false);
    ((Component) this.OBJ_ITEM_ICON).gameObject.SetActive(true);
    this.LBL_LOG.text = textLog;
    this.OBJ_JACKPOT.SetActive(false);
    ((Component) this.OBJ_ITEM_ICON).gameObject.SetActive(true);
    ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(item_type, icon_id, this.OBJ_ITEM_ICON);
    if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
      rewardItemIcon.SetSpinLogIcon();
    float num = (float) ((double) this.LBL_LOG.printedSize.x - 210.0 + 30.0);
    Vector3 localPosition = this.OBJ_ITEM_ICON.localPosition;
    localPosition.x = num;
    this.OBJ_ITEM_ICON.localPosition = localPosition;
  }
}

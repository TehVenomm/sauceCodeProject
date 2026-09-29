// Decompiled with JetBrains decompiler
// Type: ItemIconDetailSkillSetupper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconDetailSkillSetupper : ItemIconDetailSetuperBase
{
  public UILabel lblLv;
  public UILabel[] LABELS_LV;
  public UILabel[] LABELS_LV_HEAD;
  public UILabel lblDescription;
  public UISprite spMaterialSelectNumber;
  public UISprite spGrayOut;
  public UISprite spEnableExceed;
  public UISprite spSameSkillExceedExp;
  public UISprite spSameSkillExceedExpUp;
  public UISprite[] spriteBg;
  private bool isSameSkillExceed;

  protected override UISprite selectSP => this.spMaterialSelectNumber;

  public void GrayOut(ItemIconDetail.ICON_STATUS status)
  {
    this.SetGrayOut(status == ItemIconDetail.ICON_STATUS.GRAYOUT);
  }

  public override void Set(object[] data = null)
  {
    base.Set();
    if (Object.op_Inequality((Object) this.infoRootAry[0], (Object) null))
    {
      this.infoRootAry[0].SetActive(false);
      if (Object.op_Inequality((Object) this.selectSP, (Object) null))
        ((Behaviour) this.selectSP).enabled = false;
    }
    SkillItemSortData skillItemSortData = data[0] as SkillItemSortData;
    this.isSameSkillExceed = (bool) data[4];
    this.SetupSelectNumberSprite((int) data[2]);
    ItemIconDetail.ICON_STATUS status = (ItemIconDetail.ICON_STATUS) data[3];
    SkillItemInfo itemData = skillItemSortData.GetItemData() as SkillItemInfo;
    string str = $"{skillItemSortData.GetLevel()}/{itemData.GetMaxLevel()}";
    if (itemData.IsExceeded())
      str += UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) itemData.exceedCnt), ExceedSkillItemTable.color);
    this.infoRootAry[1].SetActive(true);
    this.infoRootAry[2].SetActive(false);
    this.SetName(skillItemSortData.GetName());
    this.SetVisibleBG(true);
    this.lblDescription.supportEncoding = true;
    this.lblDescription.text = itemData.GetExplanationText();
    foreach (Component component in this.LABELS_LV_HEAD)
      component.gameObject.SetActive(true);
    foreach (UILabel uiLabel in this.LABELS_LV)
    {
      ((Component) uiLabel).gameObject.SetActive(true);
      uiLabel.supportEncoding = true;
      uiLabel.text = str;
    }
    ((Component) this.spEnableExceed).gameObject.SetActive(status == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0);
    ((Component) this.spSameSkillExceedExpUp).gameObject.SetActive(this.isSameSkillExceed);
    bool flag = status == ItemIconDetail.ICON_STATUS.VALID_EXCEED || status == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
    ((Component) this.spriteBg[0]).gameObject.SetActive(!flag);
    ((Component) this.spriteBg[1]).gameObject.SetActive(flag);
    if (status == ItemIconDetail.ICON_STATUS.GRAYOUT)
      this.infoRootAry[0].SetActive(true);
    this.GrayOut(status);
  }

  private void SetGrayOut(bool is_visible) => ((Behaviour) this.spGrayOut).enabled = is_visible;

  public override void SetupSelectNumberSprite(int select_number)
  {
    base.SetupSelectNumberSprite(select_number);
    ((Component) this.spSameSkillExceedExp).gameObject.SetActive(this.isSameSkillExceed && select_number <= 0);
  }
}

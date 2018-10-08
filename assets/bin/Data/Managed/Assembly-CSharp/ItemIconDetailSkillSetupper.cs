using UnityEngine;

public class ItemIconDetailSkillSetupper : ItemIconDetailSetuperBase
{
	public UILabel lblLv;

	public UILabel[] LABELS_LV;

	public UILabel[] LABELS_LV_HEAD;

	public UILabel lblDescription;

	public UISprite spMaterialSelectNumber;

	public UISprite spGrayOut;

	public UISprite spEnableExceed;

	public UISprite[] spriteBg;

	protected override UISprite selectSP => spMaterialSelectNumber;

	public void GrayOut(ItemIconDetail.ICON_STATUS status)
	{
		SetGrayOut(status == ItemIconDetail.ICON_STATUS.GRAYOUT);
	}

	public override void Set(object[] data = null)
	{
		base.Set(null);
		if ((Object)infoRootAry[0] != (Object)null)
		{
			infoRootAry[0].SetActive(false);
			if ((Object)selectSP != (Object)null)
			{
				selectSP.enabled = false;
			}
		}
		SkillItemSortData skillItemSortData = data[0] as SkillItemSortData;
		SetupSelectNumberSprite((int)data[2]);
		ItemIconDetail.ICON_STATUS iCON_STATUS = (ItemIconDetail.ICON_STATUS)(int)data[3];
		SkillItemInfo skillItemInfo = skillItemSortData.GetItemData() as SkillItemInfo;
		string text = $"{skillItemSortData.GetLevel()}/{skillItemInfo.GetMaxLevel()}";
		if (skillItemInfo.IsExceeded())
		{
			text += UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 9u, skillItemInfo.exceedCnt), ExceedSkillItemTable.color);
		}
		infoRootAry[1].SetActive(true);
		infoRootAry[2].SetActive(false);
		SetName(skillItemSortData.GetName());
		SetVisibleBG(true);
		lblDescription.supportEncoding = true;
		lblDescription.text = skillItemInfo.GetExplanationText(false);
		UILabel[] lABELS_LV_HEAD = LABELS_LV_HEAD;
		foreach (UILabel uILabel in lABELS_LV_HEAD)
		{
			uILabel.gameObject.SetActive(true);
		}
		UILabel[] lABELS_LV = LABELS_LV;
		foreach (UILabel uILabel2 in lABELS_LV)
		{
			uILabel2.gameObject.SetActive(true);
			uILabel2.supportEncoding = true;
			uILabel2.text = text;
		}
		spEnableExceed.gameObject.SetActive(iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0);
		bool flag = iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED || iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
		spriteBg[0].gameObject.SetActive(!flag);
		spriteBg[1].gameObject.SetActive(flag);
		if (iCON_STATUS == ItemIconDetail.ICON_STATUS.GRAYOUT)
		{
			infoRootAry[0].SetActive(true);
		}
		GrayOut(iCON_STATUS);
	}

	private void SetGrayOut(bool is_visible)
	{
		spGrayOut.enabled = is_visible;
	}
}

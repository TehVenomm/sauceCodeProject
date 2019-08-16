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

	protected override UISprite selectSP => spMaterialSelectNumber;

	public void GrayOut(ItemIconDetail.ICON_STATUS status)
	{
		SetGrayOut(status == ItemIconDetail.ICON_STATUS.GRAYOUT);
	}

	public override void Set(object[] data = null)
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		base.Set();
		if (infoRootAry[0] != null)
		{
			infoRootAry[0].SetActive(false);
			if (selectSP != null)
			{
				selectSP.set_enabled(false);
			}
		}
		SkillItemSortData skillItemSortData = data[0] as SkillItemSortData;
		isSameSkillExceed = (bool)data[4];
		SetupSelectNumberSprite((int)data[2]);
		ItemIconDetail.ICON_STATUS iCON_STATUS = (ItemIconDetail.ICON_STATUS)data[3];
		SkillItemInfo skillItemInfo = skillItemSortData.GetItemData() as SkillItemInfo;
		string text = $"{skillItemSortData.GetLevel()}/{skillItemInfo.GetMaxLevel()}";
		if (skillItemInfo.IsExceeded())
		{
			text += UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 9u, skillItemInfo.exceedCnt), ExceedSkillItemTable.color);
		}
		infoRootAry[1].SetActive(true);
		infoRootAry[2].SetActive(false);
		SetName(skillItemSortData.GetName());
		SetVisibleBG(is_visible: true);
		lblDescription.supportEncoding = true;
		lblDescription.text = skillItemInfo.GetExplanationText();
		UILabel[] lABELS_LV_HEAD = LABELS_LV_HEAD;
		foreach (UILabel uILabel in lABELS_LV_HEAD)
		{
			uILabel.get_gameObject().SetActive(true);
		}
		UILabel[] lABELS_LV = LABELS_LV;
		foreach (UILabel uILabel2 in lABELS_LV)
		{
			uILabel2.get_gameObject().SetActive(true);
			uILabel2.supportEncoding = true;
			uILabel2.text = text;
		}
		spEnableExceed.get_gameObject().SetActive(iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0);
		spSameSkillExceedExpUp.get_gameObject().SetActive(isSameSkillExceed);
		bool flag = iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED || iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
		spriteBg[0].get_gameObject().SetActive(!flag);
		spriteBg[1].get_gameObject().SetActive(flag);
		if (iCON_STATUS == ItemIconDetail.ICON_STATUS.GRAYOUT)
		{
			infoRootAry[0].SetActive(true);
		}
		GrayOut(iCON_STATUS);
	}

	private void SetGrayOut(bool is_visible)
	{
		spGrayOut.set_enabled(is_visible);
	}

	public override void SetupSelectNumberSprite(int select_number)
	{
		base.SetupSelectNumberSprite(select_number);
		spSameSkillExceedExp.get_gameObject().SetActive(isSameSkillExceed && select_number <= 0);
	}
}

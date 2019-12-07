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
		base.Set();
		if (infoRootAry[0] != null)
		{
			infoRootAry[0].SetActive(value: false);
			if (selectSP != null)
			{
				selectSP.enabled = false;
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
		infoRootAry[1].SetActive(value: true);
		infoRootAry[2].SetActive(value: false);
		SetName(skillItemSortData.GetName());
		SetVisibleBG(is_visible: true);
		lblDescription.supportEncoding = true;
		lblDescription.text = skillItemInfo.GetExplanationText();
		UILabel[] lABELS_LV_HEAD = LABELS_LV_HEAD;
		for (int i = 0; i < lABELS_LV_HEAD.Length; i++)
		{
			lABELS_LV_HEAD[i].gameObject.SetActive(value: true);
		}
		lABELS_LV_HEAD = LABELS_LV;
		foreach (UILabel obj in lABELS_LV_HEAD)
		{
			obj.gameObject.SetActive(value: true);
			obj.supportEncoding = true;
			obj.text = text;
		}
		spEnableExceed.gameObject.SetActive(iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0);
		spSameSkillExceedExpUp.gameObject.SetActive(isSameSkillExceed);
		bool flag = iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED || iCON_STATUS == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
		spriteBg[0].gameObject.SetActive(!flag);
		spriteBg[1].gameObject.SetActive(flag);
		if (iCON_STATUS == ItemIconDetail.ICON_STATUS.GRAYOUT)
		{
			infoRootAry[0].SetActive(value: true);
		}
		GrayOut(iCON_STATUS);
	}

	private void SetGrayOut(bool is_visible)
	{
		spGrayOut.enabled = is_visible;
	}

	public override void SetupSelectNumberSprite(int select_number)
	{
		base.SetupSelectNumberSprite(select_number);
		spSameSkillExceedExp.gameObject.SetActive(isSameSkillExceed && select_number <= 0);
	}
}

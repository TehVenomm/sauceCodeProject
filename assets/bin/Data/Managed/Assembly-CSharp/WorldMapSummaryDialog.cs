using System;

public class WorldMapSummaryDialog : GameSection
{
	protected enum UI
	{
		LBL_SUMMARY,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ARROW_R,
		OBJ_ACTIVE_ARROW_L,
		SPR_INACTIVE_ARROW_R,
		SPR_INACTIVE_ARROW_L
	}

	private int maxPage;

	private int currentPage;

	private string[] splitedSummary;

	public override void Initialize()
	{
		int num = (int)GameSection.GetEventData();
		int num2 = num - 1;
		for (int num3 = num2; num3 < 1; num3--)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)num);
			if (data.difficulty == REGION_DIFFICULTY_TYPE.NORMAL)
			{
				num = (int)data.regionId;
				break;
			}
		}
		string text = StringTable.Get(STRING_CATEGORY.SUMMARY, (uint)num);
		splitedSummary = text.Split('@');
		currentPage = 1;
		maxPage = splitedSummary.Length;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdatePageUI();
		UpdateSummary();
	}

	private void UpdateSummary()
	{
		string text = splitedSummary[currentPage - 1];
		string name = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
		text = text.Replace("{USER_NAME}", name);
		SetLabelText((Enum)UI.LBL_SUMMARY, text);
	}

	private void UpdatePageUI()
	{
		SetLabelText((Enum)UI.LBL_NOW, currentPage.ToString());
		SetLabelText((Enum)UI.LBL_MAX, maxPage.ToString());
		UpdatePageArrows();
	}

	private void UpdatePageArrows()
	{
		if (currentPage == 1)
		{
			bool flag = currentPage < maxPage;
			SetActive((Enum)UI.OBJ_ACTIVE_ARROW_L, is_visible: false);
			SetActive((Enum)UI.OBJ_ACTIVE_ARROW_R, flag);
			SetActive((Enum)UI.SPR_INACTIVE_ARROW_L, is_visible: true);
			SetActive((Enum)UI.SPR_INACTIVE_ARROW_R, !flag);
		}
		else if (currentPage >= maxPage)
		{
			SetActive((Enum)UI.OBJ_ACTIVE_ARROW_L, is_visible: true);
			SetActive((Enum)UI.OBJ_ACTIVE_ARROW_R, is_visible: false);
			SetActive((Enum)UI.SPR_INACTIVE_ARROW_L, is_visible: false);
			SetActive((Enum)UI.SPR_INACTIVE_ARROW_R, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_ACTIVE_ARROW_L, is_visible: true);
			SetActive((Enum)UI.OBJ_ACTIVE_ARROW_R, is_visible: true);
			SetActive((Enum)UI.SPR_INACTIVE_ARROW_L, is_visible: false);
			SetActive((Enum)UI.SPR_INACTIVE_ARROW_R, is_visible: false);
		}
	}

	private void OnQuery_PAGE_NEXT()
	{
		currentPage++;
		RefreshUI();
	}

	private void OnQuery_PAGE_PREV()
	{
		currentPage--;
		RefreshUI();
	}
}

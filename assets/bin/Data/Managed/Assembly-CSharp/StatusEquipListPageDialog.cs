using System.Collections;
using UnityEngine;

public class StatusEquipListPageDialog : GameSection
{
	private enum UI
	{
		LBL_INPUT_PASS_1,
		LBL_INPUT_PASS_2,
		LBL_INPUT_PASS_3
	}

	private const int PAGE_NO_DIGIT = 3;

	private const string INITIAL_DIGIT_VALUE = "-";

	private string[] pageNo = new string[3]
	{
		"-",
		"-",
		"-"
	};

	private int pageNoIndex;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return (object)null;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_INPUT_PASS_1, pageNo[0]);
		SetLabelText(UI.LBL_INPUT_PASS_2, pageNo[1]);
		SetLabelText(UI.LBL_INPUT_PASS_3, pageNo[2]);
		base.UpdateUI();
	}

	private void OnQuery_0()
	{
		InputNumber(0);
	}

	private void OnQuery_1()
	{
		InputNumber(1);
	}

	private void OnQuery_2()
	{
		InputNumber(2);
	}

	private void OnQuery_3()
	{
		InputNumber(3);
	}

	private void OnQuery_4()
	{
		InputNumber(4);
	}

	private void OnQuery_5()
	{
		InputNumber(5);
	}

	private void OnQuery_6()
	{
		InputNumber(6);
	}

	private void OnQuery_7()
	{
		InputNumber(7);
	}

	private void OnQuery_8()
	{
		InputNumber(8);
	}

	private void OnQuery_9()
	{
		InputNumber(9);
	}

	private void OnQuery_CLEAR()
	{
		pageNoIndex = 0;
		for (int i = 0; i < 3; i++)
		{
			pageNo[i] = "-";
		}
		RefreshUI();
	}

	private void OnQuery_LIST()
	{
		int result = -1;
		if (!int.TryParse(pageNo[0] + pageNo[1] + pageNo[2], out result))
		{
			GameSection.ChangeEvent("ERROR_NOT_NUMBER", null);
		}
		else
		{
			StatusEquipList statusEquipList = MonoBehaviourSingleton<GameSceneManager>.I.FindSection("StatusEquipList") as StatusEquipList;
			if ((Object)statusEquipList != (Object)null)
			{
				int maxPageNum = statusEquipList.GetMaxPageNum();
				if (maxPageNum < result)
				{
					GameSection.ChangeEvent("OVER_NUMBER", null);
				}
				else
				{
					result = Mathf.Max(0, result - 1);
					statusEquipList.UpdateUI(result);
				}
			}
		}
	}

	private void InputNumber(int num)
	{
		if (pageNoIndex != 3)
		{
			pageNo[pageNoIndex++] = num.ToString();
			RefreshUI();
		}
	}
}

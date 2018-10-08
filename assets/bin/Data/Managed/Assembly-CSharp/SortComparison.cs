using System;

public class SortComparison
{
	public Comparison<SortCompareData> comparison;

	public SortComparison(bool is_asc)
	{
		SetComparison(is_asc);
	}

	private void SetComparison(bool order_type_asc)
	{
		if (order_type_asc)
		{
			comparison = Compare;
		}
		else
		{
			comparison = Compare_Desc;
		}
	}

	private int Compare(SortCompareData lp, SortCompareData rp)
	{
		if (lp.sortingData == rp.sortingData)
		{
			if (lp.GetUniqID() > rp.GetUniqID())
			{
				return 1;
			}
			if (lp.GetUniqID() < rp.GetUniqID())
			{
				return -1;
			}
			return 0;
		}
		if (lp.sortingData > rp.sortingData)
		{
			return 1;
		}
		return -1;
	}

	private int Compare_Desc(SortCompareData lp, SortCompareData rp)
	{
		if (rp.sortingData == lp.sortingData)
		{
			if (rp.GetUniqID() > lp.GetUniqID())
			{
				return 1;
			}
			if (rp.GetUniqID() < lp.GetUniqID())
			{
				return -1;
			}
			if (rp.GetTableID() > lp.GetTableID())
			{
				return 1;
			}
			if (rp.GetTableID() < lp.GetTableID())
			{
				return -1;
			}
			return 0;
		}
		if (rp.sortingData > lp.sortingData)
		{
			return 1;
		}
		return -1;
	}
}

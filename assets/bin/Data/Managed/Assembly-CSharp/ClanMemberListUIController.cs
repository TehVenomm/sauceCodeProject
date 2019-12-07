using System;
using System.Collections;
using UnityEngine;

public class ClanMemberListUIController : ScrollItemListControllerBase
{
	public class InitParam : InitializeParameter
	{
	}

	private static readonly string LIST_ITEM_PREFAB_NAME = "";

	public ClanMemberListUIController()
	{
	}

	public ClanMemberListUIController(InitParam _initParam)
		: base(_initParam)
	{
	}

	protected override IEnumerator RequestNextPageInfo(int _nextPageNum, Action<bool, int> _callback)
	{
		yield return null;
	}

	protected override void OnCallbackRequestPageInfo(bool _isSucceeded, int _nextPageNum)
	{
		base.OnCallbackRequestPageInfo(_isSucceeded, _nextPageNum);
	}

	public override string GetItemPrefabName()
	{
		return LIST_ITEM_PREFAB_NAME;
	}

	public override void SetListItem(int i, Transform t, bool is_recycle)
	{
	}
}

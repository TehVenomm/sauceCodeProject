using Network;
using System.Collections;
using UnityEngine;

public class FishingRecordList : GameSection
{
	protected enum UI
	{
		STR_NON_LIST,
		GRD_LIST,
		OBJ_SCROLL_BAR,
		LBL_SUM_VALUE
	}

	private int eventId;

	private GatherItemUserRecordModel.Param records;

	private bool isResetUI;

	public override void Initialize()
	{
		eventId = (int)GameSection.GetEventData();
		StartCoroutine(DoInitialize());
	}

	protected IEnumerator DoInitialize()
	{
		bool isReceived = false;
		MonoBehaviourSingleton<UserInfoManager>.I.SendGatherItemRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, eventId, delegate(bool b, GatherItemUserRecordModel r)
		{
			if (r != null)
			{
				records = r.result;
			}
			isReceived = true;
		});
		while (!isReceived)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (records != null)
		{
			SetupSrollBarCollider();
			SetActive(UI.STR_NON_LIST, records.gatherItems.IsNullOrEmpty());
			SetLabelText(UI.LBL_SUM_VALUE, $"{records.totalNum.ToString():#,0}");
			if (!records.gatherItems.IsNullOrEmpty())
			{
				SetDynamicList(UI.GRD_LIST, "FishingRecordItem", records.gatherItems.Count, isResetUI, null, null, delegate(int i, Transform t, bool is_recycle)
				{
					SetupItem(t, records.gatherItems[i]);
					SetActive(t, is_visible: true);
				});
			}
			isResetUI = false;
		}
	}

	private void SetupSrollBarCollider()
	{
		Transform ctrl = GetCtrl(UI.OBJ_SCROLL_BAR);
		if (ctrl == null)
		{
			return;
		}
		UIWidget component = ctrl.GetComponent<UIWidget>();
		if (!(component == null))
		{
			BoxCollider component2 = ctrl.GetComponent<BoxCollider>();
			if (!(component2 == null))
			{
				component2.size = new Vector2(component2.size.x, component.localSize.y);
			}
		}
	}

	private void SetupItem(Transform t, GatherItemRecord info)
	{
		FishingRecordItem fishingRecordItem = t.GetComponent<FishingRecordItem>();
		if (fishingRecordItem == null)
		{
			fishingRecordItem = t.gameObject.AddComponent<FishingRecordItem>();
		}
		fishingRecordItem.InitUI();
		fishingRecordItem.Setup(t, info);
	}

	private void OnQuery_SECTION_BACK()
	{
		Save();
	}

	private void Save()
	{
		Transform ctrl = GetCtrl(UI.GRD_LIST);
		if (ctrl == null)
		{
			return;
		}
		FishingRecordItem[] componentsInChildren = ctrl.GetComponentsInChildren<FishingRecordItem>(includeInactive: true);
		if (componentsInChildren.IsNullOrEmpty())
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].SaveState())
			{
				flag = true;
			}
		}
		if (flag)
		{
			PlayerPrefs.Save();
		}
	}
}

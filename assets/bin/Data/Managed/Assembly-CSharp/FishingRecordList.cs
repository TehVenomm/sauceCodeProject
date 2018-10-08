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
				((_003CDoInitialize_003Ec__Iterator54)/*Error near IL_0047: stateMachine*/)._003C_003Ef__this.records = r.result;
			}
			((_003CDoInitialize_003Ec__Iterator54)/*Error near IL_0047: stateMachine*/)._003CisReceived_003E__0 = true;
		});
		while (!isReceived)
		{
			yield return (object)null;
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
					SetActive(t, true);
				});
			}
			isResetUI = false;
		}
	}

	private void SetupSrollBarCollider()
	{
		Transform ctrl = GetCtrl(UI.OBJ_SCROLL_BAR);
		if (!((Object)ctrl == (Object)null))
		{
			UIWidget component = ctrl.GetComponent<UIWidget>();
			if (!((Object)component == (Object)null))
			{
				BoxCollider component2 = ctrl.GetComponent<BoxCollider>();
				if (!((Object)component2 == (Object)null))
				{
					BoxCollider boxCollider = component2;
					Vector3 size = component2.size;
					float x = size.x;
					Vector2 localSize = component.localSize;
					boxCollider.size = new Vector2(x, localSize.y);
				}
			}
		}
	}

	private void SetupItem(Transform t, GatherItemRecord info)
	{
		FishingRecordItem fishingRecordItem = t.GetComponent<FishingRecordItem>();
		if ((Object)fishingRecordItem == (Object)null)
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
		if (!((Object)ctrl == (Object)null))
		{
			FishingRecordItem[] componentsInChildren = ctrl.GetComponentsInChildren<FishingRecordItem>(true);
			if (!componentsInChildren.IsNullOrEmpty())
			{
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
	}
}

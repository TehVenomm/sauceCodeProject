using Network;
using System;
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
		this.StartCoroutine(DoInitialize());
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
			SetActive((Enum)UI.STR_NON_LIST, records.gatherItems.IsNullOrEmpty());
			SetLabelText((Enum)UI.LBL_SUM_VALUE, $"{records.totalNum.ToString():#,0}");
			if (!records.gatherItems.IsNullOrEmpty())
			{
				SetDynamicList((Enum)UI.GRD_LIST, "FishingRecordItem", records.gatherItems.Count, isResetUI, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
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
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
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
				BoxCollider obj = component2;
				Vector3 size = component2.get_size();
				float x = size.x;
				Vector2 localSize = component.localSize;
				obj.set_size(Vector2.op_Implicit(new Vector2(x, localSize.y)));
			}
		}
	}

	private void SetupItem(Transform t, GatherItemRecord info)
	{
		FishingRecordItem fishingRecordItem = t.GetComponent<FishingRecordItem>();
		if (fishingRecordItem == null)
		{
			fishingRecordItem = t.get_gameObject().AddComponent<FishingRecordItem>();
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
		FishingRecordItem[] componentsInChildren = ctrl.GetComponentsInChildren<FishingRecordItem>(true);
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

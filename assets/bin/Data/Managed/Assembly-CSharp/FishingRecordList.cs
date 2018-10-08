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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		eventId = (int)GameSection.GetEventData();
		this.StartCoroutine(DoInitialize());
	}

	protected unsafe IEnumerator DoInitialize()
	{
		bool isReceived = false;
		MonoBehaviourSingleton<UserInfoManager>.I.SendGatherItemRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, eventId, new Action<bool, GatherItemUserRecordModel>((object)/*Error near IL_0047: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (!isReceived)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		if (records != null)
		{
			SetupSrollBarCollider();
			SetActive((Enum)UI.STR_NON_LIST, records.gatherItems.IsNullOrEmpty());
			SetLabelText((Enum)UI.LBL_SUM_VALUE, $"{records.totalNum.ToString():#,0}");
			if (!records.gatherItems.IsNullOrEmpty())
			{
				SetDynamicList((Enum)UI.GRD_LIST, "FishingRecordItem", records.gatherItems.Count, isResetUI, null, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		if (!(ctrl == null))
		{
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
	}

	private void SetupItem(Transform t, GatherItemRecord info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
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
		if (!(ctrl == null))
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

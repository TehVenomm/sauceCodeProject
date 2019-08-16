using System;
using UnityEngine;

public class Reel : GameSection
{
	private enum UI
	{
		TBL_REEL,
		OBJ_REEL_SIZE_BASE,
		GRD_REEL_LIST,
		SCR_LIST,
		GRD_REEL_LIST_ITEM
	}

	public class InitData
	{
		public int[] digit;

		public int initValue;

		public Action<RecvData[]> callback;

		public InitData(int[] _digit, int _init_value = 0, Action<RecvData[]> _callback = null)
		{
			digit = _digit;
			initValue = _init_value;
			callback = _callback;
		}
	}

	public class RecvData
	{
		public int digit;

		public int selectValue;

		public int ancer;
	}

	private InitData initData;

	private RecvData[] recvData;

	public override void Initialize()
	{
		initData = (GameSection.GetEventData() as InitData);
		this.recvData = new RecvData[initData.digit.Length];
		for (int i = 0; i < this.recvData.Length; i++)
		{
			this.recvData[i] = new RecvData();
			RecvData recvData = this.recvData[i];
			recvData.digit = initData.digit[i];
			recvData.selectValue = 0;
			recvData.ancer = 0;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		int width = GetWidth(UI.OBJ_REEL_SIZE_BASE);
		int digits = 0;
		Array.ForEach(initData.digit, delegate(int data)
		{
			digits += ((data <= 0) ? 1 : data);
		});
		int reel_list_width_base = width / digits;
		SetTable(UI.TBL_REEL, "ReelList", initData.digit.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			if (initData.digit[i] <= 0)
			{
				SetActive(t, is_visible: false);
			}
			else
			{
				SetActive(t, is_visible: true);
				float width2 = reel_list_width_base * initData.digit[i];
				SetWidth(t, (int)width2);
				UIScrollView component = base.GetComponent<UIScrollView>(t, (Enum)UI.SCR_LIST);
				Vector4 baseClipRegion = component.panel.baseClipRegion;
				baseClipRegion.z = width2;
				component.panel.baseClipRegion = baseClipRegion;
				base.GetComponent<UIGrid>(t, (Enum)UI.GRD_REEL_LIST).cellWidth = reel_list_width_base;
				SetGrid(t, UI.GRD_REEL_LIST, "ReelListItem", 10, reset: false, delegate(int i2, Transform t2, bool is_recycle2)
				{
					base.GetComponent<UIGrid>(t2, (Enum)UI.GRD_REEL_LIST_ITEM).cellWidth = reel_list_width_base;
					SetWidth(t2, (int)width2);
					SetGrid(t2, UI.GRD_REEL_LIST_ITEM, "ReelListText", initData.digit[i], reset: false, delegate(int i3, Transform t3, bool is_recycle3)
					{
						int num3 = (i3 == 0) ? i2 : 0;
						SetLabelText(t3, num3.ToString());
						SetWidth(t3, (int)width2);
					});
				});
				int num = digits;
				int j = 0;
				for (int num2 = i; j < num2; j++)
				{
					num -= initData.digit[j];
				}
				int index = initData.initValue / (int)Mathf.Pow(10f, (float)(num - 1)) % 10;
				SetCenter(t, UI.GRD_REEL_LIST, index, is_instant: true);
				SetCenterOnChildFunc(t, UI.GRD_REEL_LIST, OnCenter);
			}
		});
	}

	public void OnCenter(GameObject go)
	{
		int result = 0;
		if (!int.TryParse(go.get_name(), out result))
		{
			return;
		}
		int result2 = 0;
		if (int.TryParse(go.get_transform().get_parent().get_parent()
			.get_parent()
			.get_name(), out result2) && result2 < recvData.Length)
		{
			int digits = 0;
			Array.ForEach(initData.digit, delegate(int data)
			{
				digits += ((data <= 0) ? 1 : data);
			});
			int num = digits;
			int i = 0;
			for (int num2 = result2; i < num2; i++)
			{
				num -= initData.digit[i];
			}
			recvData[result2].selectValue = result;
			recvData[result2].ancer = result * (int)Mathf.Pow(10f, (float)(num - 1));
		}
	}

	public void OnQuery_DECISION()
	{
		if (initData.callback != null)
		{
			initData.callback(recvData);
		}
	}
}

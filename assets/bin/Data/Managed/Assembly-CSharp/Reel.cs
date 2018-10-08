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

	public unsafe override void UpdateUI()
	{
		int width = GetWidth(UI.OBJ_REEL_SIZE_BASE);
		int digits = 0;
		Array.ForEach(initData.digit, delegate(int data)
		{
			digits += ((data <= 0) ? 1 : data);
		});
		int reel_list_width_base = width / digits;
		_003CUpdateUI_003Ec__AnonStorey7B1 _003CUpdateUI_003Ec__AnonStorey7B;
		SetTable(UI.TBL_REEL, "ReelList", initData.digit.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey7B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public void OnCenter(GameObject go)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		if (int.TryParse(go.get_name(), out result))
		{
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
	}

	public void OnQuery_DECISION()
	{
		if (initData.callback != null)
		{
			initData.callback(recvData);
		}
	}
}

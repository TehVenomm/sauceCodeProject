using System;

public class StatusScreenShotViewerSetting : GameSection
{
	public enum UI
	{
		BTN_NAME,
		BTN_LEVEL,
		BTN_ID,
		BTN_COMMENT,
		BTN_TITLE
	}

	private UI[] toggle = new UI[5]
	{
		UI.BTN_NAME,
		UI.BTN_LEVEL,
		UI.BTN_ID,
		UI.BTN_COMMENT,
		UI.BTN_TITLE
	};

	private int filter;

	public override void Initialize()
	{
		base.Initialize();
		filter = (int)GameSection.GetEventData();
	}

	public override void UpdateUI()
	{
		for (int i = 0; i < toggle.Length; i++)
		{
			bool value = (filter & (1 << i)) != 0;
			SetEvent((Enum)toggle[i], "FILTER", i);
			SetToggle(GetCtrl(toggle[i]).get_parent(), value);
		}
	}

	private void OnQuery_FILTER()
	{
		OnQueryEvent_Filter(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(toggle[_index]).get_parent(), _is_enable);
	}

	private void OnQuery_OK()
	{
		GameSection.SetEventData(filter);
		GameSaveData.instance.SetScreenShotUIFilterType(filter);
	}

	private void OnQueryEvent_Filter(out int _index, out bool _is_enable)
	{
		_index = (int)GameSection.GetEventData();
		int num = 1 << _index;
		if ((filter & num) == 0)
		{
			_is_enable = true;
			filter += num;
		}
		else
		{
			_is_enable = false;
			filter -= num;
		}
	}
}

using System;
using UnityEngine;

public class Picker : GameSection
{
	public class DESC
	{
		public string[] text;

		public bool enableLoop;

		public DESC(string[] _texts, bool _enable_loop = true)
		{
			text = _texts;
			enableLoop = _enable_loop;
		}
	}

	private enum UI
	{
		GRD_PICKER,
		LBL_PICKER
	}

	private DESC desc;

	private int selectIndex;

	public override void Initialize()
	{
		selectIndex = 0;
		desc = (GameSection.GetEventData() as DESC);
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetGrid(UI.GRD_PICKER, "PickerItem", desc.text.Length, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		UIWrapContent component = base.GetComponent<UIWrapContent>((Enum)UI.GRD_PICKER);
		if (component != null)
		{
			component.set_enabled(desc.enableLoop);
		}
		SetCenterOnChildFunc((Enum)UI.GRD_PICKER, (UICenterOnChild.OnCenterCallback)OnCenter);
		SetCenter((Enum)UI.GRD_PICKER, selectIndex, false);
	}

	public void OnCenter(GameObject go)
	{
		if (int.TryParse(go.get_name(), out int result))
		{
			selectIndex = result;
		}
	}

	private void OnQuery_DECISION()
	{
		GameSection.SetEventData(desc.text[selectIndex]);
	}
}

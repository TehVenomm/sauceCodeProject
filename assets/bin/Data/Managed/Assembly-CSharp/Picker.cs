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

	public override void UpdateUI()
	{
		SetGrid(UI.GRD_PICKER, "PickerItem", desc.text.Length, false, delegate(int i, Transform t, bool is_recycle)
		{
			SetLabelText(t, UI.LBL_PICKER, desc.text[i]);
		});
		UIWrapContent component = GetComponent<UIWrapContent>(UI.GRD_PICKER);
		if ((Object)component != (Object)null)
		{
			component.enabled = desc.enableLoop;
		}
		SetCenterOnChildFunc(UI.GRD_PICKER, OnCenter);
		SetCenter(UI.GRD_PICKER, selectIndex, false);
	}

	public void OnCenter(GameObject go)
	{
		if (int.TryParse(go.name, out int result))
		{
			selectIndex = result;
		}
	}

	private void OnQuery_DECISION()
	{
		GameSection.SetEventData(desc.text[selectIndex]);
	}
}

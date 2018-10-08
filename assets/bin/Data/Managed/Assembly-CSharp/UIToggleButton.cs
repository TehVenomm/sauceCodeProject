using System;

public class UIToggleButton
{
	public UIButton activeButton;

	public UIButton inactiveButton;

	public bool isActive;

	public Action<bool> onChanged;

	public UIToggleButton()
		: this()
	{
	}

	public void Initialize()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (!(activeButton == null) && !(inactiveButton == null))
		{
			EventDelegate item = new EventDelegate(OnChange);
			activeButton.get_gameObject().SetActive(isActive);
			activeButton.onClick.Clear();
			activeButton.onClick.Add(item);
			inactiveButton.get_gameObject().SetActive(!isActive);
			inactiveButton.onClick.Clear();
			inactiveButton.onClick.Add(item);
		}
	}

	public void Change()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		isActive = !isActive;
		if (activeButton != null)
		{
			activeButton.get_gameObject().SetActive(isActive);
		}
		if (inactiveButton != null)
		{
			inactiveButton.get_gameObject().SetActive(!isActive);
		}
	}

	public void OnChange()
	{
		Change();
		if (onChanged != null)
		{
			onChanged(isActive);
		}
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
	}
}

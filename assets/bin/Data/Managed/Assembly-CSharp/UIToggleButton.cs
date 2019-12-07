using System;
using UnityEngine;

public class UIToggleButton : MonoBehaviour
{
	public UIButton activeButton;

	public UIButton inactiveButton;

	public bool isActive;

	public Action<bool> onChanged;

	public void Initialize()
	{
		if (!(activeButton == null) && !(inactiveButton == null))
		{
			EventDelegate item = new EventDelegate(OnChange);
			activeButton.gameObject.SetActive(isActive);
			activeButton.onClick.Clear();
			activeButton.onClick.Add(item);
			inactiveButton.gameObject.SetActive(!isActive);
			inactiveButton.onClick.Clear();
			inactiveButton.onClick.Add(item);
		}
	}

	public void Change()
	{
		isActive = !isActive;
		if (activeButton != null)
		{
			activeButton.gameObject.SetActive(isActive);
		}
		if (inactiveButton != null)
		{
			inactiveButton.gameObject.SetActive(!isActive);
		}
	}

	public void OnChange()
	{
		Change();
		if (onChanged != null)
		{
			onChanged(isActive);
		}
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
	}
}

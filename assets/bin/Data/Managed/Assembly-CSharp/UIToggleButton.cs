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
		if (!((UnityEngine.Object)activeButton == (UnityEngine.Object)null) && !((UnityEngine.Object)inactiveButton == (UnityEngine.Object)null))
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
		if ((UnityEngine.Object)activeButton != (UnityEngine.Object)null)
		{
			activeButton.gameObject.SetActive(isActive);
		}
		if ((UnityEngine.Object)inactiveButton != (UnityEngine.Object)null)
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
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
	}
}

using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Sound Volume")]
[RequireComponent(typeof(UISlider))]
public class UISoundVolume
{
	public UISoundVolume()
		: this()
	{
	}

	private void Awake()
	{
		UISlider component = this.GetComponent<UISlider>();
		component.value = NGUITools.soundVolume;
		EventDelegate.Add(component.onChange, OnChange);
	}

	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}
}

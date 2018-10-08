using UnityEngine;

[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection
{
	private UIPopupList mList;

	public LanguageSelection()
		: this()
	{
	}

	private void Awake()
	{
		mList = this.GetComponent<UIPopupList>();
		Refresh();
	}

	private void Start()
	{
		EventDelegate.Add(mList.onChange, delegate
		{
			Localization.language = UIPopupList.current.value;
		});
	}

	public void Refresh()
	{
		if (mList != null && Localization.knownLanguages != null)
		{
			mList.Clear();
			int i = 0;
			for (int num = Localization.knownLanguages.Length; i < num; i++)
			{
				mList.items.Add(Localization.knownLanguages[i]);
			}
			mList.value = Localization.language;
		}
	}
}

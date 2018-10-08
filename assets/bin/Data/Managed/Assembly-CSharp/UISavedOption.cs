using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	public string keyName;

	private UIPopupList mList;

	private UIToggle mCheck;

	private UIProgressBar mSlider;

	private string key => (!string.IsNullOrEmpty(keyName)) ? keyName : ("NGUI State: " + base.name);

	private void Awake()
	{
		mList = GetComponent<UIPopupList>();
		mCheck = GetComponent<UIToggle>();
		mSlider = GetComponent<UIProgressBar>();
	}

	private void OnEnable()
	{
		if ((Object)mList != (Object)null)
		{
			EventDelegate.Add(mList.onChange, SaveSelection);
			string @string = PlayerPrefs.GetString(key);
			if (!string.IsNullOrEmpty(@string))
			{
				mList.value = @string;
			}
		}
		else if ((Object)mCheck != (Object)null)
		{
			EventDelegate.Add(mCheck.onChange, SaveState);
			mCheck.value = (PlayerPrefs.GetInt(key, mCheck.startsActive ? 1 : 0) != 0);
		}
		else if ((Object)mSlider != (Object)null)
		{
			EventDelegate.Add(mSlider.onChange, SaveProgress);
			mSlider.value = PlayerPrefs.GetFloat(key, mSlider.value);
		}
		else
		{
			string string2 = PlayerPrefs.GetString(key);
			UIToggle[] componentsInChildren = GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				UIToggle uIToggle = componentsInChildren[i];
				uIToggle.value = (uIToggle.name == string2);
			}
		}
	}

	private void OnDisable()
	{
		if ((Object)mCheck != (Object)null)
		{
			EventDelegate.Remove(mCheck.onChange, SaveState);
		}
		else if ((Object)mList != (Object)null)
		{
			EventDelegate.Remove(mList.onChange, SaveSelection);
		}
		else if ((Object)mSlider != (Object)null)
		{
			EventDelegate.Remove(mSlider.onChange, SaveProgress);
		}
		else
		{
			UIToggle[] componentsInChildren = GetComponentsInChildren<UIToggle>(true);
			int num = 0;
			int num2 = componentsInChildren.Length;
			UIToggle uIToggle;
			while (true)
			{
				if (num >= num2)
				{
					return;
				}
				uIToggle = componentsInChildren[num];
				if (uIToggle.value)
				{
					break;
				}
				num++;
			}
			PlayerPrefs.SetString(key, uIToggle.name);
		}
	}

	public void SaveSelection()
	{
		PlayerPrefs.SetString(key, UIPopupList.current.value);
	}

	public void SaveState()
	{
		PlayerPrefs.SetInt(key, UIToggle.current.value ? 1 : 0);
	}

	public void SaveProgress()
	{
		PlayerPrefs.SetFloat(key, UIProgressBar.current.value);
	}
}

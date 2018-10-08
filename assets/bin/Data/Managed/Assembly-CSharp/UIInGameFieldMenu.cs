using UnityEngine;

public class UIInGameFieldMenu : UIInGamePopBase
{
	private const string HomeButtonName = "BTN_HOME";

	private const string LoungeButtonName = "BTN_LOUNGE";

	private const string FieldMemberButtonName = "BTN_MEMBER_LIST";

	private const string LoungeMemberButtonName = "BTN_LOUNGE_MEMBER";

	private const string mapButtonName = "BTN_MAP";

	private const string eventButtonName = "BTN_EVENT";

	private static UIInGameFieldMenu instance;

	[SerializeField]
	protected Transform portrait;

	[SerializeField]
	protected Transform landscape;

	[SerializeField]
	protected UIButton[] menuBtns;

	private Transform _transform;

	public static UIInGameFieldMenu I => instance;

	public static bool IsValid()
	{
		return (Object)instance != (Object)null;
	}

	protected override void Awake()
	{
		instance = this;
		_transform = base.transform;
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		base.Awake();
		if (!FieldManager.IsValidInGameNoQuest())
		{
			base.gameObject.SetActive(false);
		}
		bool flag = LoungeMatchingManager.IsValidInLounge();
		SetVisibleButton("BTN_HOME", flag);
		SetVisibleButton("BTN_LOUNGE", !flag);
		SetVisibleButton("BTN_MEMBER_LIST", flag);
		SetVisibleButton("BTN_LOUNGE_MEMBER", !flag);
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		if ((Object)instance == (Object)this)
		{
			instance = null;
		}
	}

	public void OnClickReturn()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			string event_name = "RETURN";
			if (LoungeMatchingManager.IsValidInLounge())
			{
				event_name = "RETURN_LOUNGE";
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameFieldMenu.OnClickReturn", base.gameObject, event_name, null, null, true);
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			Utility.Attach(portrait, _transform);
		}
		else
		{
			Utility.Attach(landscape, _transform);
		}
	}

	public void SetDisableEventButton(bool disable)
	{
		if (menuBtns != null)
		{
			int i = 0;
			for (int num = menuBtns.Length; i < num; i++)
			{
				if ((Object)menuBtns[i] != (Object)null && menuBtns[i].name == "BTN_EVENT")
				{
					menuBtns[i].isEnabled = !disable;
				}
			}
		}
	}

	public void SetDisableMapButton(bool disable)
	{
		if (menuBtns != null)
		{
			int i = 0;
			for (int num = menuBtns.Length; i < num; i++)
			{
				if ((Object)menuBtns[i] != (Object)null && menuBtns[i].name == "BTN_MAP")
				{
					menuBtns[i].isEnabled = !disable;
				}
			}
		}
	}

	public void SetDisableButtons(bool disable)
	{
		if (menuBtns != null)
		{
			int i = 0;
			for (int num = menuBtns.Length; i < num; i++)
			{
				if ((Object)menuBtns[i] != (Object)null)
				{
					menuBtns[i].isEnabled = !disable;
				}
			}
		}
	}

	public void SetEnableButton(string btn_name, bool is_enable)
	{
		if (menuBtns != null)
		{
			int i = 0;
			for (int num = menuBtns.Length; i < num; i++)
			{
				if ((Object)menuBtns[i] != (Object)null && menuBtns[i].name == btn_name)
				{
					menuBtns[i].isEnabled = is_enable;
				}
			}
		}
	}

	private void SetVisibleButton(string btn_name, bool isVisible)
	{
		if (menuBtns != null)
		{
			for (int i = 0; i < menuBtns.Length; i++)
			{
				if ((Object)menuBtns[i] != (Object)null && menuBtns[i].name == btn_name)
				{
					menuBtns[i].gameObject.SetActive(!isVisible);
				}
			}
		}
	}

	public void SetButtonOnTutorial()
	{
		UIButton[] array = menuBtns;
		foreach (UIButton uIButton in array)
		{
			uIButton.enabled = false;
		}
	}

	public bool IsPopMenu()
	{
		return isPopMenu;
	}
}

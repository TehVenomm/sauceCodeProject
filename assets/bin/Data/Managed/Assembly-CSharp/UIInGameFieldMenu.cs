using UnityEngine;

public class UIInGameFieldMenu : UIInGamePopBase
{
	private static UIInGameFieldMenu instance;

	[SerializeField]
	protected Transform portrait;

	[SerializeField]
	protected Transform landscape;

	[SerializeField]
	protected UIButton[] menuBtns;

	private Transform _transform;

	private const string HomeButtonName = "BTN_HOME";

	private const string LoungeButtonName = "BTN_LOUNGE";

	private const string ClanButtonName = "BTN_CLAN";

	private const string FieldMemberButtonName = "BTN_MEMBER_LIST";

	private const string LoungeMemberButtonName = "BTN_LOUNGE_MEMBER";

	private const string mapButtonName = "BTN_MAP";

	private const string eventButtonName = "BTN_EVENT";

	public static UIInGameFieldMenu I => instance;

	public static bool IsValid()
	{
		return instance != null;
	}

	protected override void Awake()
	{
		instance = this;
		_transform = this.get_transform();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		base.Awake();
		if (!FieldManager.IsValidInGameNoQuest())
		{
			this.get_gameObject().SetActive(false);
		}
		bool flag = LoungeMatchingManager.IsValidInLounge();
		bool flag2 = ClanMatchingManager.IsValidInClan();
		bool flag3 = !flag && !flag2;
		SetVisibleButton("BTN_HOME", !flag3);
		SetVisibleButton("BTN_LOUNGE", !flag);
		SetVisibleButton("BTN_CLAN", !flag2);
		SetVisibleButton("BTN_MEMBER_LIST", flag);
		SetVisibleButton("BTN_LOUNGE_MEMBER", !flag);
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		if (instance == this)
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
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameFieldMenu.OnClickReturn", this.get_gameObject(), event_name);
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			Utility.Attach(portrait, _transform);
			return;
		}
		if (landscape != null && SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGameMenuPosition)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			UIWidget component = landscape.GetComponent<UIWidget>();
			if (component != null)
			{
				component.leftAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.left;
				component.rightAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.right;
				component.bottomAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.bottom;
				component.topAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.top;
				component.UpdateAnchors();
			}
		}
		Utility.Attach(landscape, _transform);
	}

	public void SetDisableEventButton(bool disable)
	{
		if (menuBtns == null)
		{
			return;
		}
		int i = 0;
		for (int num = menuBtns.Length; i < num; i++)
		{
			if (menuBtns[i] != null && menuBtns[i].get_name() == "BTN_EVENT")
			{
				menuBtns[i].isEnabled = !disable;
			}
		}
	}

	public void SetDisableMapButton(bool disable)
	{
		if (menuBtns == null)
		{
			return;
		}
		int i = 0;
		for (int num = menuBtns.Length; i < num; i++)
		{
			if (menuBtns[i] != null && menuBtns[i].get_name() == "BTN_MAP")
			{
				menuBtns[i].isEnabled = !disable;
			}
		}
	}

	public void SetDisableButtons(bool disable)
	{
		if (menuBtns == null)
		{
			return;
		}
		int i = 0;
		for (int num = menuBtns.Length; i < num; i++)
		{
			if (menuBtns[i] != null)
			{
				menuBtns[i].isEnabled = !disable;
			}
		}
	}

	public void SetEnableButton(string btn_name, bool is_enable)
	{
		if (menuBtns == null)
		{
			return;
		}
		int i = 0;
		for (int num = menuBtns.Length; i < num; i++)
		{
			if (menuBtns[i] != null && menuBtns[i].get_name() == btn_name)
			{
				menuBtns[i].isEnabled = is_enable;
			}
		}
	}

	private void SetVisibleButton(string btn_name, bool isVisible)
	{
		if (menuBtns == null)
		{
			return;
		}
		for (int i = 0; i < menuBtns.Length; i++)
		{
			if (menuBtns[i] != null && menuBtns[i].get_name() == btn_name)
			{
				menuBtns[i].get_gameObject().SetActive(!isVisible);
			}
		}
	}

	public void SetButtonOnTutorial()
	{
		UIButton[] array = menuBtns;
		foreach (UIButton uIButton in array)
		{
			uIButton.set_enabled(false);
		}
	}

	public bool IsPopMenu()
	{
		return isPopMenu;
	}
}

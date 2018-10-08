using UnityEngine;

public class UITutorialFieldHelper
{
	public enum MessageState
	{
		None,
		Wait,
		CollectItemImg,
		CollectItem,
		BackHome,
		MenuPop
	}

	private InGameMain sectionIngameMain;

	[SerializeField]
	private UITweenCtrl tweenStratCtrl;

	[SerializeField]
	private UITweenCtrl tweenEndCtrl;

	[SerializeField]
	private UIButton skilBtn;

	private static UITutorialFieldHelper instance;

	public static MessageState m_State
	{
		get;
		private set;
	}

	public bool IsLoading
	{
		get;
		private set;
	}

	public static UITutorialFieldHelper I => instance;

	public UITutorialFieldHelper()
		: this()
	{
	}

	public static bool IsValid()
	{
		return instance != null;
	}

	public static bool IsCollectedFieldItem()
	{
		return instance != null && m_State == MessageState.BackHome;
	}

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit && instance == this)
		{
			instance = null;
		}
	}

	public void Setup(InGameMain ingame_main_section)
	{
		sectionIngameMain = ingame_main_section;
		switch (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			m_State = MessageState.CollectItem;
			break;
		case 4:
			m_State = MessageState.BackHome;
			break;
		}
		SetState(m_State);
	}

	private void UpdateMessage()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		switch (m_State)
		{
		case MessageState.CollectItemImg:
		case MessageState.CollectItem:
			if (UIInGameFieldMenu.IsValid())
			{
				UIInGameFieldMenu.I.SetDisableButtons(true);
				UIInGameFieldMenu.I.get_gameObject().SetActive(false);
			}
			break;
		case MessageState.BackHome:
			if (UIInGameFieldMenu.IsValid())
			{
				tweenEndCtrl.get_gameObject().SetActive(true);
				UIInGameFieldMenu.I.SetDisableButtons(true);
				UIInGameFieldMenu.I.get_gameObject().SetActive(true);
				UIInGameFieldMenu.I.SetEnableButton("BTN_REQUEST", true);
			}
			break;
		}
	}

	public void OpenTutorialFirstDelivery()
	{
		tweenStratCtrl.Reset();
		tweenEndCtrl.Reset();
		tweenStratCtrl.Play(true, delegate
		{
			SetState(MessageState.CollectItemImg);
			tweenEndCtrl.Play(true, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				tweenStratCtrl.get_gameObject().SetActive(false);
				tweenEndCtrl.get_gameObject().SetActive(false);
			});
		});
		skilBtn.onClick.Clear();
		skilBtn.onClick.Add(new EventDelegate(delegate
		{
			tweenStratCtrl.Skip(true);
		}));
	}

	public void OnCollectItem()
	{
		SetState(MessageState.BackHome);
		sectionIngameMain.NoticeTutorialOnCollectItem();
	}

	private void SetState(MessageState state)
	{
		m_State = state;
		UpdateMessage();
	}
}

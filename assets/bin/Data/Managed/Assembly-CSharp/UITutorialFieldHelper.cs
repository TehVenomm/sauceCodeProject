using UnityEngine;

public class UITutorialFieldHelper : MonoBehaviour
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

	public static bool IsValid()
	{
		return instance != null;
	}

	public static bool IsCollectedFieldItem()
	{
		if (instance != null)
		{
			return m_State == MessageState.BackHome;
		}
		return false;
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
		switch (m_State)
		{
		case MessageState.CollectItemImg:
		case MessageState.CollectItem:
			if (UIInGameFieldMenu.IsValid())
			{
				UIInGameFieldMenu.I.SetDisableButtons(disable: true);
				UIInGameFieldMenu.I.gameObject.SetActive(value: false);
			}
			break;
		case MessageState.BackHome:
			if (UIInGameFieldMenu.IsValid())
			{
				tweenEndCtrl.gameObject.SetActive(value: true);
				UIInGameFieldMenu.I.SetDisableButtons(disable: true);
				UIInGameFieldMenu.I.gameObject.SetActive(value: true);
				UIInGameFieldMenu.I.SetEnableButton("BTN_REQUEST", is_enable: true);
			}
			break;
		}
	}

	public void OpenTutorialFirstDelivery()
	{
		tweenStratCtrl.Reset();
		tweenEndCtrl.Reset();
		tweenStratCtrl.Play(forward: true, delegate
		{
			SetState(MessageState.CollectItemImg);
			tweenEndCtrl.Play(forward: true, delegate
			{
				tweenStratCtrl.gameObject.SetActive(value: false);
				tweenEndCtrl.gameObject.SetActive(value: false);
			});
		});
		skilBtn.onClick.Clear();
		skilBtn.onClick.Add(new EventDelegate(delegate
		{
			tweenStratCtrl.Skip();
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

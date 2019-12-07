using Network;
using System;
using UnityEngine;

public class GuildMessageDonateListItem : MonoBehaviour
{
	[SerializeField]
	private UISprite m_PinButton;

	[SerializeField]
	private UISprite m_OwnerBackground;

	[SerializeField]
	private UISprite m_TargetBackground;

	[SerializeField]
	private UISprite m_Clock;

	[SerializeField]
	private UILabel m_TimeExpire;

	[SerializeField]
	private UIButton m_ButtonGift;

	[SerializeField]
	private UIButton m_AskForHelp;

	private DonateInfo info;

	private double timeLeft;

	private double minChangeColorTime;

	private double minShowLastTime;

	private bool canPinMsg;

	private float tick = 1f;

	private float counter;

	private float delayShow = 3f;

	private float startPressTime;

	private Vector2 mousePosition;

	private bool checkLongPress;

	private void OnStart()
	{
		if (m_PinButton != null)
		{
			m_PinButton.gameObject.SetActive(value: false);
		}
	}

	public void SetDonateInfo(DonateInfo _info)
	{
		info = _info;
		timeLeft = (info.expired - MonoBehaviourSingleton<GuildManager>.I.SystemTime) / 1000.0;
		m_TimeExpire.text = SecondToTime(timeLeft);
		minChangeColorTime = 900.0;
		SetUIActive();
		bool flag = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1) > 0 && info.itemNum < info.quantity;
		m_ButtonGift.SetState(UIButtonColor.State.Normal, flag);
		m_ButtonGift.state = ((!flag) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal);
		SetupUI();
	}

	private void OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
	{
		if (clanUpdateStatusData.type == 2 && clanUpdateStatusData.status == 2 && timeLeft <= 0.0)
		{
			SetUIDisable();
		}
	}

	private void SetUIActive()
	{
		m_TimeExpire.color = Color.white;
		m_OwnerBackground.color = Color.white;
		m_TargetBackground.color = Color.white;
		m_Clock.color = Color.white;
		if (m_AskForHelp.gameObject.activeInHierarchy)
		{
			m_AskForHelp.SetState(UIButtonColor.State.Normal, immediate: true);
			m_AskForHelp.isEnabled = true;
		}
		if (m_ButtonGift.gameObject.activeInHierarchy)
		{
			m_ButtonGift.SetState(UIButtonColor.State.Normal, immediate: true);
			m_ButtonGift.isEnabled = true;
		}
	}

	public void SetUIDisable()
	{
		m_TimeExpire.text = "Expired!";
		m_TimeExpire.color = Color.gray;
		m_OwnerBackground.color = Color.gray;
		m_TargetBackground.color = Color.gray;
		m_Clock.color = Color.gray;
		if (m_AskForHelp.gameObject.activeInHierarchy)
		{
			m_AskForHelp.SetState(UIButtonColor.State.Disabled, immediate: true);
			m_AskForHelp.isEnabled = false;
		}
		if (m_ButtonGift.gameObject.activeInHierarchy)
		{
			m_ButtonGift.isEnabled = false;
			m_ButtonGift.SetState(UIButtonColor.State.Disabled, immediate: true);
		}
	}

	protected void OnDestroy()
	{
	}

	private void Start()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canPinMsg = true;
		}
	}

	private void Update()
	{
		if (checkLongPress)
		{
			if (Vector2.Distance(mousePosition, Input.mousePosition) > 10f)
			{
				checkLongPress = false;
				return;
			}
			if (Time.time - startPressTime > 1f)
			{
				checkLongPress = false;
				m_PinButton.gameObject.SetActive(value: true);
				m_PinButton.gameObject.GetComponent<UIGameSceneEventSender>().eventData = info;
				m_PinButton.gameObject.SetActive(value: true);
			}
		}
		if (counter <= tick)
		{
			counter += Time.deltaTime;
			return;
		}
		counter = 0f;
		timeLeft -= 1.0;
		SetupUI();
	}

	private void SetupUI()
	{
		if (timeLeft <= minChangeColorTime && timeLeft >= 0.0 && m_Clock.color != Color.yellow)
		{
			m_Clock.color = Color.yellow;
			m_TimeExpire.color = Color.yellow;
			m_TimeExpire.text = SecondToTime(timeLeft);
		}
		if (timeLeft < 0.0)
		{
			SetUIDisable();
		}
		else
		{
			m_TimeExpire.text = SecondToTime(timeLeft);
		}
	}

	private void OnPress(bool isDown)
	{
		if (canPinMsg)
		{
			if (isDown)
			{
				checkLongPress = true;
				startPressTime = Time.time;
				mousePosition = Input.mousePosition;
			}
			else
			{
				checkLongPress = false;
			}
		}
	}

	public void HidePinButton()
	{
		m_PinButton.gameObject.SetActive(value: false);
	}

	private double DateTimeToTimestampSeconds()
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow - d).TotalSeconds;
	}

	private string SecondToTime(double time)
	{
		int num = (int)time;
		int num2 = num % 60;
		int num3 = num / 60;
		num3 %= 60;
		int num4 = num / 3600;
		return $"{num4:D2}:{num3:D2}:{num2:D2}";
	}
}

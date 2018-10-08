using Network;
using System.Collections;
using UnityEngine;

public class GuildChatItem : MonoBehaviour
{
	[SerializeField]
	private const int MESSAGE_SPRITE_WIDTH_MARGIN = 50;

	[SerializeField]
	private const int MESSAGE_SPRITE_HEIGHT_MARGIN = 25;

	[SerializeField]
	private UILabel m_LabelNotification;

	[SerializeField]
	private UISprite m_PinButton;

	[SerializeField]
	private BoxCollider m_BoxCollider;

	[SerializeField]
	private UIWidget m_Widget;

	[SerializeField]
	private UILabel m_LabelSender;

	[SerializeField]
	private UILabel m_LabelMessage;

	[SerializeField]
	private UISprite m_SpriteBase;

	[SerializeField]
	private UITexture m_TexStamp;

	[SerializeField]
	private UISprite m_NotificationSpriteBase;

	[SerializeField]
	private GameObject m_PivotMessageRight;

	[SerializeField]
	private GameObject m_PivotMessaageLeft;

	[SerializeField]
	private GameObject m_PivotStampRight;

	[SerializeField]
	private GameObject m_PivotStampLeft;

	private Vector3 MESSAGE_LABEL_POSITION_NOTIFI = new Vector3(-178f, -12f, 0f);

	private Vector3 MESSAGE_LABEL_POSITION_OTHER = new Vector3(-178f, -36f, 0f);

	private Vector3 MESSAGE_LABEL_POSITION_SELF = new Vector3(185f, -12f, 0f);

	private int stampId;

	private bool isMyMessage;

	private bool isNotifi;

	private bool canPinMsg;

	private int msgId_;

	private string uuId_;

	private int senderId_;

	private IEnumerator m_CoroutineLoadStamp;

	private float startPressTime;

	private Vector2 mousePosition;

	private bool checkLongPress;

	public UIWidget widget => m_Widget;

	public float height => (float)m_Widget.height;

	public int msgId => msgId_;

	public string uuId => uuId_;

	public string msg => m_LabelMessage.text;

	public int senderId => senderId_;

	private void Init(string uuId, int chatId, int userId, string userName, bool isText, bool isNotification)
	{
		msgId_ = chatId;
		senderId_ = userId;
		uuId_ = uuId;
		isNotifi = isNotification;
		isMyMessage = (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		m_LabelSender.text = ((!isMyMessage) ? userName : string.Empty);
		CancelLoadStamp();
		m_SpriteBase.gameObject.SetActive(isText && !isNotification);
		m_LabelMessage.gameObject.SetActive(isText);
		m_TexStamp.gameObject.SetActive(!isText);
		m_NotificationSpriteBase.gameObject.SetActive(false);
		m_LabelSender.gameObject.SetActive(!isNotification);
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canPinMsg = true;
		}
	}

	public void Init(string uuId, int chatId, int userId, string userName, string desc)
	{
		Init(uuId, chatId, userId, userName, true, false);
		SetMessage(desc);
		UpdateWidgetSize(true);
	}

	public void Init(string desc)
	{
		Init(string.Empty, 0, -1, string.Empty, true, true);
		m_LabelMessage.supportEncoding = true;
		m_LabelMessage.color = Color.white;
		m_LabelMessage.fontStyle = FontStyle.Italic;
		SetMessage(desc);
		UpdateWidgetSize(true);
	}

	private void SetMessage(string message)
	{
		m_LabelMessage.text = message;
		m_LabelMessage.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		if (isMyMessage)
		{
			Vector3 mESSAGE_LABEL_POSITION_SELF = MESSAGE_LABEL_POSITION_SELF;
			float x = mESSAGE_LABEL_POSITION_SELF.x;
			float num = (float)m_LabelMessage.width;
			Vector2 printedSize = m_LabelMessage.printedSize;
			mESSAGE_LABEL_POSITION_SELF.x = x + (num - printedSize.x);
			m_LabelMessage.transform.localPosition = mESSAGE_LABEL_POSITION_SELF;
		}
		else
		{
			m_LabelMessage.transform.localPosition = MESSAGE_LABEL_POSITION_OTHER;
		}
		if (isNotifi)
		{
			m_LabelMessage.transform.localPosition = MESSAGE_LABEL_POSITION_NOTIFI;
		}
		GameObject gameObject = (!isMyMessage) ? m_PivotMessaageLeft : m_PivotMessageRight;
		UIWidget.Pivot pivot = isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
		string spriteName = (!isMyMessage) ? "ChatHukidashiBlue" : "ChatHukidashiMine";
		if (isNotifi)
		{
			Transform transform = gameObject.transform;
			Vector3 localPosition = gameObject.transform.localPosition;
			float x2 = localPosition.x;
			Vector3 localPosition2 = gameObject.transform.localPosition;
			transform.localPosition = new Vector3(x2, 0f, localPosition2.z);
		}
		m_SpriteBase.transform.parent = gameObject.transform;
		m_SpriteBase.pivot = pivot;
		m_SpriteBase.transform.localPosition = Vector3.zero;
		m_SpriteBase.spriteName = spriteName;
		UISprite spriteBase = m_SpriteBase;
		Vector2 printedSize2 = m_LabelMessage.printedSize;
		spriteBase.width = (int)(printedSize2.x + 50f);
		UISprite spriteBase2 = m_SpriteBase;
		Vector2 printedSize3 = m_LabelMessage.printedSize;
		spriteBase2.height = (int)(printedSize3.y + 25f);
		m_PinButton.transform.SetParent(gameObject.transform);
		if (isMyMessage)
		{
			m_PinButton.transform.localPosition = new Vector3(-55f, 0f, 0f);
			m_BoxCollider.size = new Vector3((float)m_SpriteBase.width, (float)m_SpriteBase.height, 1f);
			BoxCollider boxCollider = m_BoxCollider;
			Vector3 localPosition3 = gameObject.transform.localPosition;
			boxCollider.center = new Vector3(localPosition3.x - (float)m_SpriteBase.width / 2f, (0f - (float)m_SpriteBase.height) / 2f, 0f);
		}
		else
		{
			m_PinButton.transform.localPosition = new Vector3(55f, 0f, 0f);
			m_BoxCollider.size = new Vector3((float)m_SpriteBase.width, (float)m_SpriteBase.height, 1f);
			BoxCollider boxCollider2 = m_BoxCollider;
			Vector3 localPosition4 = gameObject.transform.localPosition;
			boxCollider2.center = new Vector3(localPosition4.x + (float)m_SpriteBase.width / 2f, (0f - (float)m_SpriteBase.height) / 2f - 16f, 0f);
		}
	}

	public void Init(string uuID, int chatId, int userId, string userName, int stampId)
	{
		Init(uuID, chatId, userId, userName, false, false);
		GameObject gameObject = (!isMyMessage) ? m_PivotStampLeft : m_PivotStampRight;
		m_TexStamp.transform.parent = gameObject.transform;
		m_TexStamp.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		m_TexStamp.transform.localPosition = Vector3.zero;
		RequestLoadStamp(stampId);
		UpdateWidgetSize(false);
		m_PinButton.transform.SetParent(gameObject.transform);
		m_BoxCollider.size = new Vector3((float)m_TexStamp.width, (float)m_TexStamp.height, 1f);
		if (isMyMessage)
		{
			m_PinButton.transform.localPosition = new Vector3(-40f, 0f, 0f);
			BoxCollider boxCollider = m_BoxCollider;
			Vector3 localPosition = gameObject.transform.localPosition;
			boxCollider.center = new Vector3(localPosition.x - (float)m_TexStamp.width / 2f, (0f - (float)m_TexStamp.height) / 2f, 0f);
		}
		else
		{
			m_PinButton.transform.localPosition = new Vector3(40f, 0f, 0f);
			BoxCollider boxCollider2 = m_BoxCollider;
			Vector3 localPosition2 = gameObject.transform.localPosition;
			boxCollider2.center = new Vector3(localPosition2.x + (float)m_TexStamp.width / 2f, (0f - (float)m_TexStamp.height) / 2f - 16f, 0f);
		}
	}

	private void UpdateWidgetSize(bool isText)
	{
		int num = (!isMyMessage) ? m_LabelSender.height : 0;
		if (isNotifi)
		{
			num = 0;
		}
		int num2 = 0;
		int num3 = 0;
		m_BoxCollider.enabled = true;
		if (isText)
		{
			num2 = m_SpriteBase.height;
			num3 = m_SpriteBase.width;
		}
		else
		{
			num2 = m_TexStamp.height;
			num3 = m_TexStamp.width;
		}
		m_Widget.width = num3;
		m_Widget.height = num2 + num;
	}

	private void RequestLoadStamp(int stampId)
	{
		this.stampId = stampId;
		CancelLoadStamp();
		m_CoroutineLoadStamp = CoroutineLoadStamp(stampId);
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(_Update());
		}
	}

	private IEnumerator CoroutineLoadStamp(int stampId)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId, false);
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (lo_stamp.loadedObject != (Object)null)
		{
			Texture2D stamp = lo_stamp.loadedObject as Texture2D;
			m_TexStamp.gameObject.SetActive(true);
			m_TexStamp.mainTexture = stamp;
		}
		m_CoroutineLoadStamp = null;
	}

	private void CancelLoadStamp()
	{
		if (m_CoroutineLoadStamp != null)
		{
			m_CoroutineLoadStamp = null;
			m_TexStamp.gameObject.SetActive(false);
		}
	}

	private IEnumerator _Update()
	{
		while (m_CoroutineLoadStamp != null && m_CoroutineLoadStamp.MoveNext())
		{
			yield return (object)null;
		}
	}

	private void OnEnable()
	{
		if (m_CoroutineLoadStamp != null)
		{
			RequestLoadStamp(stampId);
		}
	}

	private void Update()
	{
		if (checkLongPress)
		{
			float num = Vector2.Distance(mousePosition, Input.mousePosition);
			if (num > 10f)
			{
				checkLongPress = false;
			}
			else if (Time.time - startPressTime > 1f)
			{
				checkLongPress = false;
				ClanChatLogMessageData clanChatLogMessageData = new ClanChatLogMessageData();
				clanChatLogMessageData.fromUserId = senderId;
				clanChatLogMessageData.id = msgId;
				clanChatLogMessageData.uuid = uuId;
				clanChatLogMessageData.stampId = stampId;
				if (stampId <= 0)
				{
					clanChatLogMessageData.type = 0;
					clanChatLogMessageData.message = msg;
				}
				else
				{
					clanChatLogMessageData.type = 1;
					clanChatLogMessageData.message = stampId.ToString();
				}
				m_PinButton.gameObject.GetComponent<UIGameSceneEventSender>().eventData = clanChatLogMessageData;
				m_PinButton.gameObject.SetActive(true);
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("GuildChatItem", base.gameObject, "HIDE_PIN_BTN", msgId_.ToString(), null, true);
			}
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
		m_PinButton.gameObject.SetActive(false);
	}
}

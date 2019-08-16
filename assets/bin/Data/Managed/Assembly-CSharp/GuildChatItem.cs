using Network;
using System.Collections;
using UnityEngine;

public class GuildChatItem : MonoBehaviour
{
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

	[SerializeField]
	private const int MESSAGE_SPRITE_WIDTH_MARGIN = 50;

	[SerializeField]
	private const int MESSAGE_SPRITE_HEIGHT_MARGIN = 25;

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

	public float height => m_Widget.height;

	public int msgId => msgId_;

	public string uuId => uuId_;

	public string msg => m_LabelMessage.text;

	public int senderId => senderId_;

	public GuildChatItem()
		: this()
	{
	}//IL_0010: Unknown result type (might be due to invalid IL or missing references)
	//IL_0015: Unknown result type (might be due to invalid IL or missing references)
	//IL_002a: Unknown result type (might be due to invalid IL or missing references)
	//IL_002f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0044: Unknown result type (might be due to invalid IL or missing references)
	//IL_0049: Unknown result type (might be due to invalid IL or missing references)


	private void Init(string uuId, int chatId, int userId, string userName, bool isText, bool isNotification)
	{
		msgId_ = chatId;
		senderId_ = userId;
		uuId_ = uuId;
		isNotifi = isNotification;
		isMyMessage = (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		m_LabelSender.text = ((!isMyMessage) ? userName : string.Empty);
		CancelLoadStamp();
		m_SpriteBase.get_gameObject().SetActive(isText && !isNotification);
		m_LabelMessage.get_gameObject().SetActive(isText);
		m_TexStamp.get_gameObject().SetActive(!isText);
		m_NotificationSpriteBase.get_gameObject().SetActive(false);
		m_LabelSender.get_gameObject().SetActive(!isNotification);
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canPinMsg = true;
		}
	}

	public void Init(string uuId, int chatId, int userId, string userName, string desc)
	{
		Init(uuId, chatId, userId, userName, isText: true, isNotification: false);
		SetMessage(desc);
		UpdateWidgetSize(isText: true);
	}

	public void Init(string desc)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Init(string.Empty, 0, -1, string.Empty, isText: true, isNotification: true);
		m_LabelMessage.supportEncoding = true;
		m_LabelMessage.color = Color.get_white();
		m_LabelMessage.fontStyle = 2;
		SetMessage(desc);
		UpdateWidgetSize(isText: true);
	}

	private void SetMessage(string message)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		m_LabelMessage.text = message;
		m_LabelMessage.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		if (isMyMessage)
		{
			Vector3 mESSAGE_LABEL_POSITION_SELF = MESSAGE_LABEL_POSITION_SELF;
			float x = mESSAGE_LABEL_POSITION_SELF.x;
			float num = m_LabelMessage.width;
			Vector2 printedSize = m_LabelMessage.printedSize;
			mESSAGE_LABEL_POSITION_SELF.x = x + (num - printedSize.x);
			m_LabelMessage.get_transform().set_localPosition(mESSAGE_LABEL_POSITION_SELF);
		}
		else
		{
			m_LabelMessage.get_transform().set_localPosition(MESSAGE_LABEL_POSITION_OTHER);
		}
		if (isNotifi)
		{
			m_LabelMessage.get_transform().set_localPosition(MESSAGE_LABEL_POSITION_NOTIFI);
		}
		GameObject val = (!isMyMessage) ? m_PivotMessaageLeft : m_PivotMessageRight;
		UIWidget.Pivot pivot = isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
		string spriteName = (!isMyMessage) ? "ChatHukidashiBlue" : "ChatHukidashiMine";
		if (isNotifi)
		{
			Transform transform = val.get_transform();
			Vector3 localPosition = val.get_transform().get_localPosition();
			float x2 = localPosition.x;
			Vector3 localPosition2 = val.get_transform().get_localPosition();
			transform.set_localPosition(new Vector3(x2, 0f, localPosition2.z));
		}
		m_SpriteBase.get_transform().set_parent(val.get_transform());
		m_SpriteBase.pivot = pivot;
		m_SpriteBase.get_transform().set_localPosition(Vector3.get_zero());
		m_SpriteBase.spriteName = spriteName;
		UISprite spriteBase = m_SpriteBase;
		Vector2 printedSize2 = m_LabelMessage.printedSize;
		spriteBase.width = (int)(printedSize2.x + 50f);
		UISprite spriteBase2 = m_SpriteBase;
		Vector2 printedSize3 = m_LabelMessage.printedSize;
		spriteBase2.height = (int)(printedSize3.y + 25f);
		m_PinButton.get_transform().SetParent(val.get_transform());
		if (isMyMessage)
		{
			m_PinButton.get_transform().set_localPosition(new Vector3(-55f, 0f, 0f));
			m_BoxCollider.set_size(new Vector3((float)m_SpriteBase.width, (float)m_SpriteBase.height, 1f));
			BoxCollider boxCollider = m_BoxCollider;
			Vector3 localPosition3 = val.get_transform().get_localPosition();
			boxCollider.set_center(new Vector3(localPosition3.x - (float)m_SpriteBase.width / 2f, (0f - (float)m_SpriteBase.height) / 2f, 0f));
		}
		else
		{
			m_PinButton.get_transform().set_localPosition(new Vector3(55f, 0f, 0f));
			m_BoxCollider.set_size(new Vector3((float)m_SpriteBase.width, (float)m_SpriteBase.height, 1f));
			BoxCollider boxCollider2 = m_BoxCollider;
			Vector3 localPosition4 = val.get_transform().get_localPosition();
			boxCollider2.set_center(new Vector3(localPosition4.x + (float)m_SpriteBase.width / 2f, (0f - (float)m_SpriteBase.height) / 2f - 16f, 0f));
		}
	}

	public void Init(string uuID, int chatId, int userId, string userName, int stampId)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		Init(uuID, chatId, userId, userName, isText: false, isNotification: false);
		GameObject val = (!isMyMessage) ? m_PivotStampLeft : m_PivotStampRight;
		m_TexStamp.get_transform().set_parent(val.get_transform());
		m_TexStamp.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		m_TexStamp.get_transform().set_localPosition(Vector3.get_zero());
		RequestLoadStamp(stampId);
		UpdateWidgetSize(isText: false);
		m_PinButton.get_transform().SetParent(val.get_transform());
		m_BoxCollider.set_size(new Vector3((float)m_TexStamp.width, (float)m_TexStamp.height, 1f));
		if (isMyMessage)
		{
			m_PinButton.get_transform().set_localPosition(new Vector3(-40f, 0f, 0f));
			BoxCollider boxCollider = m_BoxCollider;
			Vector3 localPosition = val.get_transform().get_localPosition();
			boxCollider.set_center(new Vector3(localPosition.x - (float)m_TexStamp.width / 2f, (0f - (float)m_TexStamp.height) / 2f, 0f));
		}
		else
		{
			m_PinButton.get_transform().set_localPosition(new Vector3(40f, 0f, 0f));
			BoxCollider boxCollider2 = m_BoxCollider;
			Vector3 localPosition2 = val.get_transform().get_localPosition();
			boxCollider2.set_center(new Vector3(localPosition2.x + (float)m_TexStamp.width / 2f, (0f - (float)m_TexStamp.height) / 2f - 16f, 0f));
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
		m_BoxCollider.set_enabled(true);
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
		if (this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_Update());
		}
	}

	private IEnumerator CoroutineLoadStamp(int stampId)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId);
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D mainTexture = lo_stamp.loadedObject as Texture2D;
			m_TexStamp.get_gameObject().SetActive(true);
			m_TexStamp.mainTexture = mainTexture;
		}
		m_CoroutineLoadStamp = null;
	}

	private void CancelLoadStamp()
	{
		if (m_CoroutineLoadStamp != null)
		{
			m_CoroutineLoadStamp = null;
			m_TexStamp.get_gameObject().SetActive(false);
		}
	}

	private IEnumerator _Update()
	{
		while (m_CoroutineLoadStamp != null && m_CoroutineLoadStamp.MoveNext())
		{
			yield return null;
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (!checkLongPress)
		{
			return;
		}
		float num = Vector2.Distance(mousePosition, Vector2.op_Implicit(Input.get_mousePosition()));
		if (num > 10f)
		{
			checkLongPress = false;
		}
		else if (Time.get_time() - startPressTime > 1f)
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
			m_PinButton.get_gameObject().GetComponent<UIGameSceneEventSender>().eventData = clanChatLogMessageData;
			m_PinButton.get_gameObject().SetActive(true);
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("GuildChatItem", this.get_gameObject(), "HIDE_PIN_BTN", msgId_.ToString());
		}
	}

	private void OnPress(bool isDown)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (canPinMsg)
		{
			if (isDown)
			{
				checkLongPress = true;
				startPressTime = Time.get_time();
				mousePosition = Vector2.op_Implicit(Input.get_mousePosition());
			}
			else
			{
				checkLongPress = false;
			}
		}
	}

	public void HidePinButton()
	{
		m_PinButton.get_gameObject().SetActive(false);
	}
}

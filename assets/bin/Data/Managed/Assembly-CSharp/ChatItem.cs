using System.Collections;
using UnityEngine;

public class ChatItem : MonoBehaviour
{
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

	private Vector3 MESSAGE_LABEL_POSITION_OTHER = new Vector3(-173f, -36f, 0f);

	private Vector3 MESSAGE_LABEL_POSITION_SELF = new Vector3(170f, -12f, 0f);

	public int stampId;

	public bool isMyMessage;

	public string chatItemId;

	public static Color DefaultMessageColor = new Color(0f, 0f, 0f);

	private IEnumerator m_CoroutineLoadStamp;

	public UIWidget widget => m_Widget;

	public float height => m_Widget.height;

	private void Init(int userId, string userName, bool isText, bool isNotification, string chatItemId = "")
	{
		isMyMessage = (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		m_LabelSender.text = (isMyMessage ? string.Empty : userName);
		this.chatItemId = chatItemId;
		CancelLoadStamp();
		m_SpriteBase.gameObject.SetActive(isText && !isNotification);
		m_LabelMessage.gameObject.SetActive(isText);
		m_LabelMessage.color = DefaultMessageColor;
		m_LabelMessage.supportEncoding = false;
		m_TexStamp.gameObject.SetActive(!isText);
		m_NotificationSpriteBase.gameObject.SetActive(isText && isNotification);
		m_LabelSender.gameObject.SetActive(!isNotification);
	}

	public void Init(int userId, string userName, string desc, string chatItemId = "")
	{
		Init(userId, userName, isText: true, isNotification: false, chatItemId);
		SetMessage(desc);
		UpdateWidgetSize(isText: true);
	}

	public void Init(string desc, string chatItemId = "", bool fontWhite = false)
	{
		Init(-1, "", isText: true, isNotification: true, chatItemId);
		if (fontWhite)
		{
			m_LabelMessage.color = Color.white;
			m_NotificationSpriteBase.gameObject.SetActive(value: false);
		}
		SetMessage(desc);
		UpdateWidgetSize(isText: true);
		m_LabelMessage.supportEncoding = true;
	}

	private void SetMessage(string message)
	{
		m_LabelMessage.text = message;
		m_LabelMessage.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		if (isMyMessage)
		{
			Vector3 mESSAGE_LABEL_POSITION_SELF = MESSAGE_LABEL_POSITION_SELF;
			mESSAGE_LABEL_POSITION_SELF.x += (float)m_LabelMessage.width - m_LabelMessage.printedSize.x;
			m_LabelMessage.transform.localPosition = mESSAGE_LABEL_POSITION_SELF;
		}
		else
		{
			m_LabelMessage.transform.localPosition = MESSAGE_LABEL_POSITION_OTHER;
		}
		GameObject gameObject = isMyMessage ? m_PivotMessageRight : m_PivotMessaageLeft;
		UIWidget.Pivot pivot = isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
		string spriteName = isMyMessage ? "ChatHukidashiMine" : "ChatHukidashiBlue";
		m_SpriteBase.transform.parent = gameObject.transform;
		m_SpriteBase.pivot = pivot;
		m_SpriteBase.transform.localPosition = Vector3.zero;
		m_SpriteBase.spriteName = spriteName;
		m_SpriteBase.width = (int)(m_LabelMessage.printedSize.x + 50f);
		m_SpriteBase.height = (int)(m_LabelMessage.printedSize.y + 25f);
		m_NotificationSpriteBase.height = (int)(m_LabelMessage.printedSize.y + 25f);
	}

	public void Init(int userId, string userName, int stampId, string chatItemId = "")
	{
		Init(userId, userName, isText: false, isNotification: false, chatItemId);
		GameObject gameObject = isMyMessage ? m_PivotStampRight : m_PivotStampLeft;
		m_TexStamp.transform.parent = gameObject.transform;
		m_TexStamp.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		m_TexStamp.transform.localPosition = Vector3.zero;
		RequestLoadStamp(stampId);
		UpdateWidgetSize(isText: false);
	}

	private void UpdateWidgetSize(bool isText)
	{
		int num = (!isMyMessage) ? m_LabelSender.height : 0;
		int num2 = 0;
		int num3 = 0;
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
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId);
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D mainTexture = lo_stamp.loadedObject as Texture2D;
			m_TexStamp.gameObject.SetActive(value: true);
			m_TexStamp.mainTexture = mainTexture;
		}
		m_CoroutineLoadStamp = null;
	}

	private void CancelLoadStamp()
	{
		if (m_CoroutineLoadStamp != null)
		{
			m_CoroutineLoadStamp = null;
			m_TexStamp.gameObject.SetActive(value: false);
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
}

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

	public ChatItem()
		: this()
	{
	}//IL_0010: Unknown result type (might be due to invalid IL or missing references)
	//IL_0015: Unknown result type (might be due to invalid IL or missing references)
	//IL_002a: Unknown result type (might be due to invalid IL or missing references)
	//IL_002f: Unknown result type (might be due to invalid IL or missing references)


	private void Init(int userId, string userName, bool isText, bool isNotification, string chatItemId = "")
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		isMyMessage = (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		m_LabelSender.text = ((!isMyMessage) ? userName : string.Empty);
		this.chatItemId = chatItemId;
		CancelLoadStamp();
		m_SpriteBase.get_gameObject().SetActive(isText && !isNotification);
		m_LabelMessage.get_gameObject().SetActive(isText);
		m_LabelMessage.color = DefaultMessageColor;
		m_LabelMessage.supportEncoding = false;
		m_TexStamp.get_gameObject().SetActive(!isText);
		m_NotificationSpriteBase.get_gameObject().SetActive(isText && isNotification);
		m_LabelSender.get_gameObject().SetActive(!isNotification);
	}

	public void Init(int userId, string userName, string desc, string chatItemId = "")
	{
		Init(userId, userName, isText: true, isNotification: false, chatItemId);
		SetMessage(desc);
		UpdateWidgetSize(isText: true);
	}

	public void Init(string desc, string chatItemId = "", bool fontWhite = false)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Init(-1, string.Empty, isText: true, isNotification: true, chatItemId);
		if (fontWhite)
		{
			m_LabelMessage.color = Color.get_white();
			m_NotificationSpriteBase.get_gameObject().SetActive(false);
		}
		SetMessage(desc);
		UpdateWidgetSize(isText: true);
		m_LabelMessage.supportEncoding = true;
	}

	private void SetMessage(string message)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
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
		GameObject val = (!isMyMessage) ? m_PivotMessaageLeft : m_PivotMessageRight;
		UIWidget.Pivot pivot = isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
		string spriteName = (!isMyMessage) ? "ChatHukidashiBlue" : "ChatHukidashiMine";
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
		UISprite notificationSpriteBase = m_NotificationSpriteBase;
		Vector2 printedSize4 = m_LabelMessage.printedSize;
		notificationSpriteBase.height = (int)(printedSize4.y + 25f);
	}

	public void Init(int userId, string userName, int stampId, string chatItemId = "")
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Init(userId, userName, isText: false, isNotification: false, chatItemId);
		GameObject val = (!isMyMessage) ? m_PivotStampLeft : m_PivotStampRight;
		m_TexStamp.get_transform().set_parent(val.get_transform());
		m_TexStamp.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		m_TexStamp.get_transform().set_localPosition(Vector3.get_zero());
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
}

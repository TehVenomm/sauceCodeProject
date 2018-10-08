using System.Collections;
using UnityEngine;

public class ChatItem : MonoBehaviour
{
	[SerializeField]
	private const int MESSAGE_SPRITE_WIDTH_MARGIN = 50;

	[SerializeField]
	private const int MESSAGE_SPRITE_HEIGHT_MARGIN = 25;

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

	private Vector3 MESSAGE_LABEL_POSITION_OTHER = new Vector3(-173f, -36f, 0f);

	private Vector3 MESSAGE_LABEL_POSITION_SELF = new Vector3(170f, -12f, 0f);

	private int stampId;

	private bool isMyMessage;

	private IEnumerator m_CoroutineLoadStamp;

	public UIWidget widget => m_Widget;

	public float height => (float)m_Widget.height;

	private void Init(int userId, string userName, bool isText, bool isNotification)
	{
		isMyMessage = (userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
		m_LabelSender.text = ((!isMyMessage) ? userName : string.Empty);
		CancelLoadStamp();
		m_SpriteBase.gameObject.SetActive(isText && !isNotification);
		m_LabelMessage.gameObject.SetActive(isText);
		m_TexStamp.gameObject.SetActive(!isText);
		m_NotificationSpriteBase.gameObject.SetActive(isText && isNotification);
		m_LabelSender.gameObject.SetActive(!isNotification);
	}

	public void Init(int userId, string userName, string desc)
	{
		Init(userId, userName, true, false);
		SetMessage(desc);
		UpdateWidgetSize(true);
	}

	public void Init(string desc)
	{
		Init(-1, string.Empty, true, true);
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
		GameObject gameObject = (!isMyMessage) ? m_PivotMessaageLeft : m_PivotMessageRight;
		UIWidget.Pivot pivot = isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
		string spriteName = (!isMyMessage) ? "ChatHukidashiBlue" : "ChatHukidashiMine";
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
	}

	public void Init(int userId, string userName, int stampId)
	{
		Init(userId, userName, false, false);
		GameObject gameObject = (!isMyMessage) ? m_PivotStampLeft : m_PivotStampRight;
		m_TexStamp.transform.parent = gameObject.transform;
		m_TexStamp.pivot = (isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft);
		m_TexStamp.transform.localPosition = Vector3.zero;
		RequestLoadStamp(stampId);
		UpdateWidgetSize(false);
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
}

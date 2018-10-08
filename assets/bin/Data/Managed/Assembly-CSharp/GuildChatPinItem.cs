using System.Collections;
using UnityEngine;

public class GuildChatPinItem
{
	private const int MIN_HEIGHT = 140;

	[SerializeField]
	private UISprite m_UnPinButton;

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
	private UITexture m_Avatar;

	private bool canUnPinMsg;

	[SerializeField]
	private UITexture m_TexStamp;

	private IEnumerator m_CoroutineLoadStamp;

	private float startPressTime;

	private Vector2 mousePosition;

	private bool checkLongPress;

	public int GetHeight => m_SpriteBase.height;

	public GuildChatPinItem()
		: this()
	{
	}

	public void ShowPinMsg(string userPin, string pinMsg)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		m_TexStamp.get_gameObject().SetActive(false);
		m_LabelMessage.get_gameObject().SetActive(true);
		m_LabelMessage.text = pinMsg;
		m_LabelSender.text = userPin;
		Vector2 printedSize = m_LabelMessage.printedSize;
		int num = (int)(printedSize.y + 50f);
		m_SpriteBase.height = ((num <= 140) ? 140 : num);
		m_BoxCollider.set_center(new Vector3(0f, (0f - (float)m_SpriteBase.height) / 2f, 0f));
		Transform transform = m_UnPinButton.get_transform();
		Vector3 localPosition = m_UnPinButton.get_transform().get_localPosition();
		transform.set_localPosition(new Vector3(localPosition.x, 0f - ((float)m_SpriteBase.height - 35f), 0f));
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canUnPinMsg = true;
		}
	}

	public void ShowPinStamp(string userPin, int stampId)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		m_LabelMessage.text = string.Empty;
		m_LabelSender.text = userPin;
		m_LabelMessage.get_gameObject().SetActive(false);
		RequestLoadStamp(stampId);
		m_SpriteBase.height = 140;
		m_BoxCollider.set_center(new Vector3(0f, (0f - (float)m_SpriteBase.height) / 2f, 0f));
		Transform transform = m_UnPinButton.get_transform();
		Vector3 localPosition = m_UnPinButton.get_transform().get_localPosition();
		transform.set_localPosition(new Vector3(localPosition.x, 0f - ((float)m_SpriteBase.height - 35f), 0f));
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canUnPinMsg = true;
		}
	}

	private void RequestLoadStamp(int stampId)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
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
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId, false);
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D stamp = lo_stamp.loadedObject as Texture2D;
			m_TexStamp.get_gameObject().SetActive(true);
			m_TexStamp.mainTexture = stamp;
		}
		m_CoroutineLoadStamp = null;
	}

	private void CancelLoadStamp()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
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
			yield return (object)null;
		}
	}

	private void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (checkLongPress)
		{
			float num = Vector2.Distance(mousePosition, Vector2.op_Implicit(Input.get_mousePosition()));
			if (num > 10f)
			{
				checkLongPress = false;
			}
			else if (Time.get_time() - startPressTime > 1f)
			{
				checkLongPress = false;
				m_UnPinButton.get_gameObject().SetActive(true);
			}
		}
	}

	private void OnPress(bool isDown)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (canUnPinMsg)
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

	public void HideUnPinButton()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		m_UnPinButton.get_gameObject().SetActive(false);
	}
}

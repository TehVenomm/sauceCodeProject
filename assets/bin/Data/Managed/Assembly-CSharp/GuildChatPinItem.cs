using System.Collections;
using UnityEngine;

public class GuildChatPinItem : MonoBehaviour
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

	public void ShowPinMsg(string userPin, string pinMsg)
	{
		m_TexStamp.gameObject.SetActive(false);
		m_LabelMessage.gameObject.SetActive(true);
		m_LabelMessage.text = pinMsg;
		m_LabelSender.text = userPin;
		Vector2 printedSize = m_LabelMessage.printedSize;
		int num = (int)(printedSize.y + 50f);
		m_SpriteBase.height = ((num <= 140) ? 140 : num);
		m_BoxCollider.center = new Vector3(0f, (0f - (float)m_SpriteBase.height) / 2f, 0f);
		Transform transform = m_UnPinButton.transform;
		Vector3 localPosition = m_UnPinButton.transform.localPosition;
		transform.localPosition = new Vector3(localPosition.x, 0f - ((float)m_SpriteBase.height - 35f), 0f);
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canUnPinMsg = true;
		}
	}

	public void ShowPinStamp(string userPin, int stampId)
	{
		m_LabelMessage.text = string.Empty;
		m_LabelSender.text = userPin;
		m_LabelMessage.gameObject.SetActive(false);
		RequestLoadStamp(stampId);
		m_SpriteBase.height = 140;
		m_BoxCollider.center = new Vector3(0f, (0f - (float)m_SpriteBase.height) / 2f, 0f);
		Transform transform = m_UnPinButton.transform;
		Vector3 localPosition = m_UnPinButton.transform.localPosition;
		transform.localPosition = new Vector3(localPosition.x, 0f - ((float)m_SpriteBase.height - 35f), 0f);
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canUnPinMsg = true;
		}
	}

	private void RequestLoadStamp(int stampId)
	{
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
				m_UnPinButton.gameObject.SetActive(true);
			}
		}
	}

	private void OnPress(bool isDown)
	{
		if (canUnPinMsg)
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

	public void HideUnPinButton()
	{
		m_UnPinButton.gameObject.SetActive(false);
	}
}

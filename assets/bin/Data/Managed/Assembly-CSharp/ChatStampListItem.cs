using System;
using System.Collections;
using UnityEngine;

public class ChatStampListItem : MonoBehaviour
{
	[SerializeField]
	private BoxCollider m_Colider;

	[SerializeField]
	private UITexture m_Texture;

	public Action onButton;

	private bool isDummy;

	private IEnumerator m_CoroutineLoadStamp;

	public int StampId
	{
		get;
		protected set;
	}

	public bool IsReady
	{
		get;
		protected set;
	}

	private void Awake()
	{
		UIButton component = m_Texture.GetComponent<UIButton>();
		component.CacheDefaultColor();
		component.tweenTarget = null;
	}

	public void Init(int _stampId)
	{
		StampId = _stampId;
		IsReady = false;
		isDummy = false;
		SetActiveComponents(isActive: false);
		RequestLoadStamp();
	}

	public void SetAsDummy()
	{
		isDummy = true;
		SetActiveComponents(isActive: false);
	}

	public void SetActiveComponents(bool isActive)
	{
		if (!(isDummy && isActive))
		{
			m_Colider.enabled = isActive;
			m_Texture.alpha = (isActive ? 1 : 0);
		}
	}

	public void OnButten()
	{
		if (IsReady && onButton != null)
		{
			onButton();
		}
	}

	private void RequestLoadStamp()
	{
		CancelLoadStamp();
		m_CoroutineLoadStamp = CoroutineLoadStamp();
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(_Update());
		}
	}

	private IEnumerator CoroutineLoadStamp()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(StampId);
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D mainTexture = lo_stamp.loadedObject as Texture2D;
			m_Texture.mainTexture = mainTexture;
			SetActiveComponents(isActive: true);
			IsReady = true;
		}
		m_CoroutineLoadStamp = null;
	}

	private void CancelLoadStamp()
	{
		m_CoroutineLoadStamp = null;
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
			RequestLoadStamp();
		}
	}
}

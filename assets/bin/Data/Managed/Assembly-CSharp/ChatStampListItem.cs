using System;
using System.Collections;
using UnityEngine;

public class ChatStampListItem
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

	public ChatStampListItem()
		: this()
	{
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
		SetActiveComponents(false);
		RequestLoadStamp();
	}

	public void SetAsDummy()
	{
		isDummy = true;
		SetActiveComponents(false);
	}

	public void SetActiveComponents(bool isActive)
	{
		if (!isDummy || !isActive)
		{
			m_Colider.set_enabled(isActive);
			m_Texture.alpha = (float)(isActive ? 1 : 0);
		}
	}

	public void OnButten()
	{
		if (IsReady && onButton != null)
		{
			onButton.Invoke();
		}
	}

	private void RequestLoadStamp()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		CancelLoadStamp();
		m_CoroutineLoadStamp = CoroutineLoadStamp();
		if (this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_Update());
		}
	}

	private IEnumerator CoroutineLoadStamp()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(StampId, false);
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D stamp = lo_stamp.loadedObject as Texture2D;
			m_Texture.mainTexture = stamp;
			SetActiveComponents(true);
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
			yield return (object)null;
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

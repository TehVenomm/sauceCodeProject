using System;
using UnityEngine;

public class ChatInputFrame : MonoBehaviour
{
	[SerializeField]
	private UITweener[] m_OpenTweens;

	[SerializeField]
	private UITweener[] m_CloseTweens;

	[SerializeField]
	private UIButton m_OpenCloseBtn;

	[SerializeField]
	private UISprite m_OpenCloseSprite;

	[SerializeField]
	private UILabel m_InputTextLabel;

	[SerializeField]
	private UISprite m_BackgroundSprite;

	[SerializeField]
	private BoxCollider m_InputCollider;

	private int m_BusyCount;

	private const float INPUT_COLLIDER_MARGIN = 10f;

	private const float FRAME_OFFSET = 4f;

	private bool m_IsOpen;

	public Action onChange;

	public Action onSubmit;

	[SerializeField]
	private UISprite m_AgeConfirmSprite;

	[SerializeField]
	private UISprite m_DenyChatSprite;

	private bool IsBusy => m_BusyCount != 0;

	public ChatInputFrame()
		: this()
	{
	}

	private void Awake()
	{
		InitTweens(m_OpenTweens);
		InitTweens(m_CloseTweens);
		m_IsOpen = true;
	}

	private void InitTweens(UITweener[] tweens)
	{
		if (tweens != null)
		{
			int i = 0;
			for (int num = tweens.Length; i < num; i++)
			{
				tweens[i].set_enabled(false);
				tweens[i].AddOnFinished(new EventDelegate(OnFinished));
			}
		}
	}

	private void OnFinished()
	{
		m_BusyCount--;
		if (!IsBusy)
		{
			m_InputCollider.set_enabled(m_IsOpen);
		}
	}

	public void Open()
	{
		if (StartAnim(m_OpenTweens))
		{
			SetOpenCloseBtnSprite("ChatBtnHome");
		}
		m_IsOpen = true;
	}

	public void Close()
	{
		if (StartAnim(m_CloseTweens))
		{
			m_IsOpen = false;
			SetOpenCloseBtnSprite("ChatBtnHome");
		}
	}

	public void Reset()
	{
		UpdateAgeConfirm();
		m_IsOpen = false;
		SetOpenCloseBtnSprite("ChatBtnHome");
		FrameResize();
	}

	private void SetOpenCloseBtnSprite(string spriteName)
	{
	}

	private bool StartAnim(UITweener[] tweens)
	{
		if (IsBusy)
		{
			return false;
		}
		m_BusyCount = tweens.Length;
		m_InputCollider.set_enabled(false);
		int i = 0;
		for (int num = tweens.Length; i < num; i++)
		{
			tweens[i].set_enabled(true);
			tweens[i].ResetToBeginning();
		}
		return true;
	}

	public bool IsEnableInput()
	{
		if (IsBusy)
		{
			return false;
		}
		return m_IsOpen;
	}

	public bool IsOpenOrBusy()
	{
		return m_IsOpen || IsBusy;
	}

	public void FrameResize()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		BoxCollider inputCollider = m_InputCollider;
		Vector3 size = m_InputCollider.get_size();
		float x = size.x;
		float num = (float)m_BackgroundSprite.height + 10f;
		Vector3 size2 = m_InputCollider.get_size();
		inputCollider.set_size(new Vector3(x, num, size2.z));
	}

	public void ChangeText()
	{
		if (onChange != null)
		{
			onChange();
		}
	}

	public void SubmitText()
	{
		if (onSubmit != null)
		{
			onSubmit();
		}
	}

	public void OnTouchOpenCloseBtn()
	{
		if (IsBusy)
		{
		}
	}

	public void OnTouchCloseBtn()
	{
		if (IsBusy)
		{
		}
	}

	public void UpdateAgeConfirm()
	{
		bool active = !UserInfoManager.IsRegisterdAge();
		bool active2 = UserInfoManager.IsRegisterdAge() && !UserInfoManager.IsEnableCommunication();
		if (m_AgeConfirmSprite != null)
		{
			m_AgeConfirmSprite.get_gameObject().SetActive(active);
		}
		if (m_DenyChatSprite != null)
		{
			m_DenyChatSprite.get_gameObject().SetActive(active2);
		}
	}
}

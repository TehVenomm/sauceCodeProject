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
				tweens[i].enabled = false;
				tweens[i].AddOnFinished(new EventDelegate(OnFinished));
			}
		}
	}

	private void OnFinished()
	{
		m_BusyCount--;
		if (!IsBusy)
		{
			m_InputCollider.enabled = m_IsOpen;
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
		m_InputCollider.enabled = false;
		int i = 0;
		for (int num = tweens.Length; i < num; i++)
		{
			tweens[i].enabled = true;
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
		if (!m_IsOpen)
		{
			return IsBusy;
		}
		return true;
	}

	public void FrameResize()
	{
		m_InputCollider.size = new Vector3(m_InputCollider.size.x, (float)m_BackgroundSprite.height + 10f, m_InputCollider.size.z);
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
		_ = IsBusy;
	}

	public void OnTouchCloseBtn()
	{
		_ = IsBusy;
	}

	public void UpdateAgeConfirm()
	{
		bool active = !UserInfoManager.IsRegisterdAge();
		bool active2 = UserInfoManager.IsRegisterdAge() && !UserInfoManager.IsEnableCommunication();
		if (m_AgeConfirmSprite != null)
		{
			m_AgeConfirmSprite.gameObject.SetActive(active);
		}
		if (m_DenyChatSprite != null)
		{
			m_DenyChatSprite.gameObject.SetActive(active2);
		}
	}
}

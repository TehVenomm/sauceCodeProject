using System;
using UnityEngine;

public class ChatChannelInputPanel
{
	private ChatUITweenGroup rootPanelTween;

	private UILabel[] numberLabels;

	private UIButton okButton;

	private int currentPosition;

	private int[] number;

	private Action<int> onOK;

	private Action onClose;

	private static string NONE = "-";

	public bool isOpened => rootPanelTween.isOpened;

	public ChatChannelInputPanel(ChatUITweenGroup root)
	{
		rootPanelTween = root;
	}

	public void SetNumLabels(params UILabel[] labels)
	{
		numberLabels = labels;
		number = new int[numberLabels.Length];
		ClearNumbers();
	}

	public void SetNumButtons(params UIButton[] buttons)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].onClick.Add(CreateNumButtonEvent(i));
		}
	}

	private EventDelegate CreateNumButtonEvent(int num)
	{
		return new EventDelegate(delegate
		{
			OnNumber(num);
		});
	}

	public void SetOKButton(UIButton button)
	{
		button.onClick.Add(new EventDelegate(OnOK));
		okButton = button;
	}

	public void SetClearButton(UIButton button)
	{
		button.onClick.Add(new EventDelegate(OnClear));
	}

	public void SetCloseButton(UIButton button)
	{
		button.onClick.Add(new EventDelegate(delegate
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
			OnClose();
		}));
	}

	public void SetOnOKDelegate(Action<int> onOK)
	{
		this.onOK = onOK;
	}

	public void SetOnCloseButtonDelegate(Action onClose)
	{
		this.onClose = onClose;
	}

	public void Open()
	{
		UpdateOKButton();
		UpdateNumberLabels();
		rootPanelTween.Open(delegate
		{
		});
	}

	public void Close()
	{
		rootPanelTween.Close(delegate
		{
			ClearNumbers();
		});
	}

	private void OnNumber(int num)
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
		if (currentPosition >= 0)
		{
			number[currentPosition] = num;
			currentPosition--;
			UpdateOKButton();
			UpdateNumberLabels();
		}
	}

	private void OnOK()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.OK);
		int num = 0;
		for (int i = 0; i < number.Length; i++)
		{
			if (number[i] >= 0)
			{
				num += Mathf.RoundToInt((float)number[i] * Mathf.Pow(10f, i));
			}
		}
		if (onOK != null)
		{
			onOK(num);
		}
	}

	private void OnClear()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
		ClearNumbers();
		UpdateOKButton();
		UpdateNumberLabels();
	}

	private void OnClose()
	{
		if (onClose != null)
		{
			onClose();
		}
		Close();
	}

	private void ClearNumbers()
	{
		currentPosition = number.Length - 1;
		for (int i = 0; i < number.Length; i++)
		{
			number[i] = -1;
		}
	}

	private void UpdateOKButton()
	{
		okButton.isEnabled = (currentPosition < 0);
	}

	private void UpdateNumberLabels()
	{
		for (int i = 0; i < numberLabels.Length; i++)
		{
			if (number[i] >= 0)
			{
				numberLabels[i].text = number[i].ToString();
			}
			else
			{
				numberLabels[i].text = NONE;
			}
		}
	}
}

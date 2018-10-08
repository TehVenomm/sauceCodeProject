using System;
using UnityEngine;

public class TutorialNameInputDialog : GameSection
{
	protected enum UI
	{
		TERMS_OF_SERVICE,
		SPR_CHECK,
		SPR_CHECK_OFF,
		IPT_NAME,
		BTN_CONFIRM,
		OBJ_INPUT
	}

	private UINameInput inputName;

	private bool isTermsEnable;

	private int sexId;

	private string initName;

	public override void Initialize()
	{
		sexId = (int)GameSection.GetEventData();
		inputName = base.GetComponent<UINameInput>(GetCtrl(UI.OBJ_INPUT), (Enum)UI.IPT_NAME);
		SetActive((Enum)UI.SPR_CHECK, isTermsEnable);
		SetActive((Enum)UI.SPR_CHECK_OFF, !isTermsEnable);
		SetButtonEnabled((Enum)UI.BTN_CONFIRM, false);
		initName = base.sectionData.GetText("DEFAULT_NAME_TEXT");
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetInput(GetCtrl(UI.OBJ_INPUT), UI.IPT_NAME, "DPRO Hunter ", 14, OnChangeName);
		inputName.selectAllTextOnFocus = false;
		inputName.isSelected = true;
	}

	private void OnQuery_TERMS()
	{
		isTermsEnable = !isTermsEnable;
		SetActive((Enum)UI.SPR_CHECK, isTermsEnable);
		SetActive((Enum)UI.SPR_CHECK_OFF, !isTermsEnable);
		SetButtonEnabled((Enum)UI.BTN_CONFIRM, GetInputName().Length > 0 && isTermsEnable);
	}

	protected void OnQuery_CONFIRM()
	{
		string text = GetInputName();
		SendEditFigure(delegate(bool success)
		{
			if (success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
			}
		});
	}

	private void OnQuery_TutorialNameConfirmDialog_YES()
	{
		SendEditFigure(delegate(bool success)
		{
			if (success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
			}
		});
	}

	private void OnChangeName()
	{
		string text = GetInputName();
		if (text.Length == 0)
		{
			if (inputName != null)
			{
				inputName.InActiveName();
			}
			SetButtonEnabled((Enum)UI.BTN_CONFIRM, false);
		}
		else
		{
			if (inputName != null)
			{
				inputName.ActiveName();
				inputName.SetName(text);
			}
			SetButtonEnabled((Enum)UI.BTN_CONFIRM, isTermsEnable);
		}
	}

	private string GetInputName()
	{
		return GetInputValue((Enum)UI.IPT_NAME);
	}

	public unsafe void SendEditFigure(Action<bool> call_back)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		OptionEditFigureModel.RequestSendForm send_form = new OptionEditFigureModel.RequestSendForm();
		send_form.sex = sexId;
		string text = GetInputName().Replace(" ", string.Empty);
		send_form.name = ((!(text == initName.Replace(" ", string.Empty))) ? text : "/colopl_rob");
		send_form.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		_003CSendEditFigure_003Ec__AnonStorey4A5 _003CSendEditFigure_003Ec__AnonStorey4A;
		Protocol.Force(new Action((object)_003CSendEditFigure_003Ec__AnonStorey4A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		PlayerPrefs.SetString("Tut_Name", send_form.name);
	}
}

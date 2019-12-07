using Network;
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
		inputName = GetComponent<UINameInput>(GetCtrl(UI.OBJ_INPUT), UI.IPT_NAME);
		SetActive(UI.SPR_CHECK, isTermsEnable);
		SetActive(UI.SPR_CHECK_OFF, !isTermsEnable);
		SetButtonEnabled(UI.BTN_CONFIRM, is_enabled: false);
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
		SetActive(UI.SPR_CHECK, isTermsEnable);
		SetActive(UI.SPR_CHECK_OFF, !isTermsEnable);
		SetButtonEnabled(UI.BTN_CONFIRM, GetInputName().Length > 0 && isTermsEnable);
	}

	protected void OnQuery_CONFIRM()
	{
		GetInputName();
		SendEditFigure(delegate(bool success)
		{
			if (success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
			}
		});
	}

	private void OnQuery_TutorialNameConfirmDialog_YES()
	{
		SendEditFigure(delegate(bool success)
		{
			if (success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
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
			SetButtonEnabled(UI.BTN_CONFIRM, is_enabled: false);
			return;
		}
		if (inputName != null)
		{
			inputName.ActiveName();
			inputName.SetName(text);
		}
		SetButtonEnabled(UI.BTN_CONFIRM, isTermsEnable);
	}

	private string GetInputName()
	{
		return GetInputValue(UI.IPT_NAME);
	}

	public void SendEditFigure(Action<bool> call_back)
	{
		OptionEditFigureModel.RequestSendForm send_form = new OptionEditFigureModel.RequestSendForm();
		send_form.sex = sexId;
		string text = GetInputName().Replace(" ", "");
		send_form.name = ((text == initName.Replace(" ", "")) ? "/colopl_rob" : text);
		send_form.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Force(delegate
		{
			Protocol.Send(OptionEditFigureModel.URL, send_form, delegate(OptionEditFigureModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
				}
				call_back(obj);
			});
		});
		PlayerPrefs.SetString("Tut_Name", send_form.name);
	}

	private void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}

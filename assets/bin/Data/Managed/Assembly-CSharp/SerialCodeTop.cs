using Network;
using System;
using System.Collections;
using UnityEngine;

public class SerialCodeTop : GameSection
{
	private enum UI
	{
		TBL_LIST,
		LBL_NAME,
		IPT_CODE,
		BTN_SEND
	}

	private SerialListModel.Param serialList;

	private void SendGetSerialCodeList(Action<bool> callback)
	{
		Protocol.Send(SerialListModel.URL, delegate(SerialListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				serialList = ret.result;
			}
			callback(obj);
		}, string.Empty);
	}

	private void SendInputSerialCode(int id, string code, Action<bool, string> callback)
	{
		SerialInputModel.RequestSendForm requestSendForm = new SerialInputModel.RequestSendForm();
		requestSendForm.id = id;
		requestSendForm.code = code;
		Protocol.Send(SerialInputModel.URL, requestSendForm, delegate(SerialInputModel ret)
		{
			bool arg = false;
			string arg2 = string.Empty;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg2 = ret.result.message;
			}
			callback(arg, arg2);
		}, string.Empty);
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		SendGetSerialCodeList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator147)/*Error near IL_002e: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		int count = serialList.serials.Count;
		SetTable(UI.TBL_LIST, "SerialCodeListItem", count, false, delegate(int i, Transform t, bool b)
		{
			SerialListModel.Serials serials = serialList.serials[i];
			SetInput(t, UI.IPT_CODE, string.Empty, 64, null);
			SetLabelText(t, UI.LBL_NAME, serials.name);
			SetEvent(t, UI.BTN_SEND, "SEND", i);
		});
	}

	private void OnQuery_SEND()
	{
		int index = (int)GameSection.GetEventData();
		Transform t = GetCtrl(UI.TBL_LIST).FindChild(index.ToString());
		string inputValue = GetInputValue(t, UI.IPT_CODE);
		if (string.IsNullOrEmpty(inputValue))
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.StayEvent();
			SendInputSerialCode(serialList.serials[index].serialId, inputValue, delegate(bool is_success, string msg)
			{
				if (is_success)
				{
					GameSection.SetEventData(msg);
					SetInputValue(t, UI.IPT_CODE, string.Empty);
				}
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}
}

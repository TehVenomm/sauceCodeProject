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
			bool flag = false;
			string text = string.Empty;
			if (ret.Error == Error.None)
			{
				flag = true;
				text = ret.result.message;
			}
			callback.Invoke(flag, text);
		}, string.Empty);
	}

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		SendGetSerialCodeList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator153)/*Error near IL_002e: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		base.UpdateUI();
		int count = serialList.serials.Count;
		SetTable(UI.TBL_LIST, "SerialCodeListItem", count, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe void OnQuery_SEND()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
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
			_003COnQuery_SEND_003Ec__AnonStorey45A _003COnQuery_SEND_003Ec__AnonStorey45A;
			SendInputSerialCode(serialList.serials[index].serialId, inputValue, new Action<bool, string>((object)_003COnQuery_SEND_003Ec__AnonStorey45A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}

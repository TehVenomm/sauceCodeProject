using Network;
using System;

public class OnceManager : MonoBehaviourSingleton<OnceManager>
{
	private bool firstSendOnce = true;

	public OnceAllModel.Param result
	{
		get;
		private set;
	}

	public void SendGetOnce(Action<bool> callBack)
	{
		if (!firstSendOnce)
		{
			callBack(true);
		}
		else
		{
			OnceAllModel.RequestSendForm requestSendForm = new OnceAllModel.RequestSendForm();
			requestSendForm.req_e = 1;
			requestSendForm.req_i = 1;
			requestSendForm.req_qi = 1;
			requestSendForm.req_s = 1;
			requestSendForm.req_ai = 1;
			requestSendForm.req_ac = 1;
			Protocol.Send(OnceAllModel.URL, requestSendForm, delegate(OnceAllModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					result = ret.result;
					firstSendOnce = false;
				}
				callBack(obj);
			}, string.Empty);
		}
	}
}

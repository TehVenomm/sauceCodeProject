using System;
using System.Collections;
using System.Collections.Generic;

public class InformationDialog : WebViewDialog
{
	protected IEnumerator GetDiff(Action<bool> call_back)
	{
		bool wait = true;
		bool isSuccess = true;
		MonoBehaviourSingleton<PresentManager>.I.SendGetPresentTotalCount(delegate(bool b)
		{
			wait = false;
			isSuccess = b;
		});
		while (wait)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistNotClearDelivery(new DELIVERY_CONDITION_TYPE[1]
		{
			DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
		}))
		{
			wait = true;
			MonoBehaviourSingleton<DeliveryManager>.I.SendGetClearStatusList(new List<DELIVERY_CONDITION_TYPE>
			{
				DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
			}, delegate(bool b, DeliveryGetClearStatusModel.Param param)
			{
				if (b)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.UpdateClearStatuses(param.clearStatusDelivery);
				}
				wait = false;
				isSuccess = (isSuccess && b);
			});
		}
		while (wait)
		{
			yield return null;
		}
		call_back(isSuccess);
	}

	private void OnQuery_CLOSE()
	{
		GameSection.StayEvent();
		this.StartCoroutine(GetDiff(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}
}

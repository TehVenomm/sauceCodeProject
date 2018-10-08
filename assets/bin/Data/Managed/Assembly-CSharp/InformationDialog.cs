using System.Collections.Generic;

public class InformationDialog : WebViewDialog
{
	private void OnQuery_CLOSE()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistNotClearDelivery(new DELIVERY_CONDITION_TYPE[1]
		{
			DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
		}))
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<DeliveryManager>.I.SendGetClearStatusList(new List<DELIVERY_CONDITION_TYPE>
			{
				DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
			}, delegate(bool isSuccess, DeliveryGetClearStatusModel.Param param)
			{
				if (isSuccess)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.UpdateClearStatuses(param.clearStatusDelivery);
				}
				GameSection.ResumeEvent(isSuccess, null);
			});
		}
	}
}

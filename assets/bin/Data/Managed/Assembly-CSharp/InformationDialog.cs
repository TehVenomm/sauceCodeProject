using System;
using System.Collections.Generic;

public class InformationDialog : WebViewDialog
{
	private unsafe void OnQuery_CLOSE()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistNotClearDelivery(new DELIVERY_CONDITION_TYPE[1]
		{
			DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
		}))
		{
			GameSection.StayEvent();
			DeliveryManager i = MonoBehaviourSingleton<DeliveryManager>.I;
			List<DELIVERY_CONDITION_TYPE> condiditionTypeList = new List<DELIVERY_CONDITION_TYPE>
			{
				DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
			};
			if (_003C_003Ef__am_0024cache0 == null)
			{
				_003C_003Ef__am_0024cache0 = new Action<bool, DeliveryGetClearStatusModel.Param>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendGetClearStatusList(condiditionTypeList, _003C_003Ef__am_0024cache0);
		}
	}
}

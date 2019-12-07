using UnityEngine;

namespace gogame
{
	public class GoWrapComponent : MonoBehaviour
	{
		private void Start()
		{
			GoWrap.INSTANCE.initGoWrap(base.name);
		}

		private bool hasDelegate()
		{
			return GoWrap.INSTANCE.getDelegate() != null;
		}

		public void handleAdsCompletedWithReward(string message)
		{
			JSONObject jSONObject = new JSONObject(message);
			jSONObject.GetField(out string field, "rewardId", "DEFAULT");
			jSONObject.GetField(out int field2, "rewardQuantity", -1);
			if (hasDelegate())
			{
				GoWrap.INSTANCE.getDelegate().didCompleteRewardedAd(field, field2);
			}
		}

		public void handleMenuOpened(string message)
		{
			if (hasDelegate())
			{
				GoWrap.INSTANCE.getDelegate().onMenuOpened();
			}
		}

		public void handleMenuClosed(string message)
		{
			if (hasDelegate())
			{
				GoWrap.INSTANCE.getDelegate().onMenuClosed();
			}
		}

		public void handleOnCustomUrl(string message)
		{
			if (hasDelegate())
			{
				GoWrap.INSTANCE.getDelegate().onCustomUrl(message);
			}
		}

		public void handleOnOffersAvailable(string message)
		{
			if (hasDelegate())
			{
				GoWrap.INSTANCE.getDelegate().onOffersAvailable();
			}
		}
	}
}

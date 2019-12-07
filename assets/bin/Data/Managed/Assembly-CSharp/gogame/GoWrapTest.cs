using UnityEngine;

namespace gogame
{
	public class GoWrapTest : MonoBehaviour, IGoWrapDelegate
	{
		private void Start()
		{
			GoWrap.INSTANCE.setGuid("GUID123");
			GoWrap.INSTANCE.setDelegate(this);
		}

		public void ShowGoWrapDebugMenu()
		{
			GoWrapDebugMenu.ShowGoWrapDebugMenu();
		}

		public void didCompleteRewardedAd(string rewardId, int rewardQuantity)
		{
			Debug.Log($"[goWrap/test] didCompleteRewardedAd({rewardId}, {rewardQuantity})");
		}

		public void onMenuOpened()
		{
			Debug.Log("[goWrap/test] onMenuOpened()");
		}

		public void onMenuClosed()
		{
			Debug.Log("[goWrap/test] onMenuClosed()");
		}

		public void onCustomUrl(string url)
		{
			Debug.Log($"[goWrap/test] onCustomUrl({url})");
		}

		public void onOffersAvailable()
		{
			Debug.Log("[goWrap/test] onOffersAvailable()");
		}
	}
}

namespace gogame
{
	public interface IGoWrapDelegate
	{
		void didCompleteRewardedAd(string rewardId, int rewardQuantity);

		void onMenuOpened();

		void onMenuClosed();

		void onCustomUrl(string url);

		void onOffersAvailable();
	}
}

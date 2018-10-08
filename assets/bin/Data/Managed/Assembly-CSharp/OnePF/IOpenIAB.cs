namespace OnePF
{
	public interface IOpenIAB
	{
		void setGameUserId(string id);

		void setEmail(string email);

		void init(Options options, string goPayAppKey, string goPaySecret);

		void mapSku(string sku, string storeName, string storeSku);

		void unbindService();

		bool areSubscriptionsSupported();

		void queryInventory();

		void queryInventory(string[] inAppSkus);

		void purchaseProduct(string sku, string developerPayload = "");

		void purchaseSubscription(string sku, string developerPayload = "");

		void consumeProduct(Purchase purchase);

		void restoreTransactions();

		bool isDebugLog();

		void enableDebugLogging(bool enabled);

		void enableDebugLogging(bool enabled, string tag);
	}
}

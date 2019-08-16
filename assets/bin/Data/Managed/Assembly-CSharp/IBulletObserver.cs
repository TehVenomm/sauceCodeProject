public interface IBulletObserver
{
	int GetObservedID();

	void RegisterObservable(IBulletObservable observable);

	void RegisterObservableID(int observedID);

	void OnBreak(int brokenBulletID, bool isSendOnlyOriginal);

	void OnBulletDestroy(int brokenBulletID);
}

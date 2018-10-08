public interface IBulletObservable
{
	int GetObservedID();

	void SetObservedID(int id);

	void RegisterObserver();

	void NotifyBroken(bool isSendOnlyOriginal = true);

	void NotifyDestroy();

	void ForceBreak();
}

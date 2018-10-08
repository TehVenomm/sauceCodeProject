public interface IBulletObservable
{
	int GetObservedID();

	void SetObservedID(int id);

	void RegisterObserver();

	void NotifyBroken();

	void NotifyDestroy();

	void ForceBreak();
}

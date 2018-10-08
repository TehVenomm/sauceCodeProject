public interface IObservable
{
	void RegisterObserver(IObserver observer);

	void NotifyObservers();
}

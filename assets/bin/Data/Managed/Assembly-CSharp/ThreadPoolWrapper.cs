using System.Threading;

public class ThreadPoolWrapper
{
	public static void QueueUserWorkItem(WaitCallback callback)
	{
		ThreadPool.QueueUserWorkItem(callback);
	}
}

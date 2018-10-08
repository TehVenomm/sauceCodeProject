using System;

namespace GetSocialSdk.Core
{
	internal class RunnableProxy : JavaInterfaceProxy
	{
		private readonly Action _runnable;

		public RunnableProxy(Action runnable)
			: base("java.lang.Runnable")
		{
			_runnable = runnable;
		}

		private void run()
		{
			JavaInterfaceProxy.ExecuteOnMainThread(_runnable);
		}
	}
}

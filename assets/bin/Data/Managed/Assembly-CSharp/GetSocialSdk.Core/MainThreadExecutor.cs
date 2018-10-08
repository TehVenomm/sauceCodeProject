using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class MainThreadExecutor : Singleton<MainThreadExecutor>
	{
		private readonly object _queueLock = new object();

		private readonly List<Action> _queuedActions = new List<Action>();

		private readonly List<Action> _executingActions = new List<Action>();

		[RuntimeInitializeOnLoadMethod()]
		private static void Init()
		{
			Singleton<MainThreadExecutor>.LoadInstance();
		}

		internal static void Queue(Action action)
		{
			if (action == null)
			{
				Debug.LogWarning((object)"Trying to queue null action");
			}
			else
			{
				lock (Singleton<MainThreadExecutor>.Instance._queueLock)
				{
					Singleton<MainThreadExecutor>.Instance._queuedActions.Add(action);
				}
			}
		}

		private void Update()
		{
			MoveQueuedActionsToExecuting();
			while (_executingActions.Count > 0)
			{
				Action val = _executingActions[0];
				_executingActions.RemoveAt(0);
				val.Invoke();
			}
		}

		private void MoveQueuedActionsToExecuting()
		{
			lock (_queueLock)
			{
				while (_queuedActions.Count > 0)
				{
					Action item = _queuedActions[0];
					_executingActions.Add(item);
					_queuedActions.RemoveAt(0);
				}
			}
		}
	}
}

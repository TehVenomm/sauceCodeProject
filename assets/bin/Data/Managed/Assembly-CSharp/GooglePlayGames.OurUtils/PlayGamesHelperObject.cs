using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	public class PlayGamesHelperObject
	{
		private static PlayGamesHelperObject instance = null;

		private static bool sIsDummy = false;

		private static List<Action> sQueue = new List<Action>();

		private List<Action> localQueue = new List<Action>();

		private static volatile bool sQueueEmpty = true;

		private static List<Action<bool>> sPauseCallbackList = new List<Action<bool>>();

		private static List<Action<bool>> sFocusCallbackList = new List<Action<bool>>();

		public PlayGamesHelperObject()
			: this()
		{
		}

		public static void CreateObject()
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			if (!(instance != null))
			{
				if (Application.get_isPlaying())
				{
					GameObject val = new GameObject("PlayGames_QueueRunner");
					Object.DontDestroyOnLoad(val);
					instance = val.AddComponent<PlayGamesHelperObject>();
				}
				else
				{
					instance = new PlayGamesHelperObject();
					sIsDummy = true;
				}
			}
		}

		public void Awake()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			Object.DontDestroyOnLoad(this.get_gameObject());
		}

		public void OnDisable()
		{
			if (instance == this)
			{
				instance = null;
			}
		}

		public unsafe static void RunCoroutine(IEnumerator action)
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			if (instance != null)
			{
				_003CRunCoroutine_003Ec__AnonStorey7D7 _003CRunCoroutine_003Ec__AnonStorey7D;
				RunOnGameThread(new Action((object)_003CRunCoroutine_003Ec__AnonStorey7D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public static void RunOnGameThread(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (!sIsDummy)
			{
				lock (sQueue)
				{
					sQueue.Add(action);
					sQueueEmpty = false;
				}
			}
		}

		public void Update()
		{
			if (!sIsDummy && !sQueueEmpty)
			{
				localQueue.Clear();
				lock (sQueue)
				{
					localQueue.AddRange(sQueue);
					sQueue.Clear();
					sQueueEmpty = true;
				}
				for (int i = 0; i < localQueue.Count; i++)
				{
					localQueue[i].Invoke();
				}
			}
		}

		public void OnApplicationFocus(bool focused)
		{
			foreach (Action<bool> sFocusCallback in sFocusCallbackList)
			{
				try
				{
					sFocusCallback(focused);
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("Exception in OnApplicationFocus:" + ex.Message + "\n" + ex.StackTrace));
				}
			}
		}

		public void OnApplicationPause(bool paused)
		{
			foreach (Action<bool> sPauseCallback in sPauseCallbackList)
			{
				try
				{
					sPauseCallback(paused);
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("Exception in OnApplicationPause:" + ex.Message + "\n" + ex.StackTrace));
				}
			}
		}

		public static void AddFocusCallback(Action<bool> callback)
		{
			if (!sFocusCallbackList.Contains(callback))
			{
				sFocusCallbackList.Add(callback);
			}
		}

		public static bool RemoveFocusCallback(Action<bool> callback)
		{
			return sFocusCallbackList.Remove(callback);
		}

		public static void AddPauseCallback(Action<bool> callback)
		{
			if (!sPauseCallbackList.Contains(callback))
			{
				sPauseCallbackList.Add(callback);
			}
		}

		public static bool RemovePauseCallback(Action<bool> callback)
		{
			return sPauseCallbackList.Remove(callback);
		}
	}
}

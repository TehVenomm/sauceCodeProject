using System.Collections;
using UnityEngine;

public class GameSceneEvent
{
	public static GameSceneEvent current = new GameSceneEvent();

	public static GameSceneEvent request = null;

	private static GameSceneEvent stay = null;

	private static Stack stayStack = null;

	public bool isExecute;

	public string eventName = string.Empty;

	public GameObject sender;

	public object userData;

	public static void Initialize()
	{
		ResetCurrent();
		stay = null;
		stayStack = null;
	}

	public GameSceneEvent()
	{
		_Init();
	}

	public GameSceneEvent(GameSceneEvent e)
	{
		isExecute = e.isExecute;
		eventName = e.eventName;
		sender = e.sender;
		userData = e.userData;
	}

	public void _Init()
	{
		isExecute = false;
		eventName = string.Empty;
		sender = null;
		userData = null;
	}

	public static void ResetCurrent()
	{
		current._Init();
	}

	public static void Stay()
	{
		if (stay != null)
		{
			Log.Error(LOG.GAMESCENE, "ERR :: GameSceneEvent Is Already Staying");
			return;
		}
		stay = new GameSceneEvent(current);
		ResetCurrent();
	}

	public static bool IsStay()
	{
		return stay != null;
	}

	public static void Resume(object userData = null, bool is_send_query = false)
	{
		if (stay != null && MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			current = stay;
			stay = null;
			if (userData != null)
			{
				current.userData = userData;
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("GameSceneEvent.Resume", current.sender, current.eventName, current.userData, null, is_send_query);
		}
	}

	public static void PushStay()
	{
		if (stayStack == null)
		{
			stayStack = new Stack();
		}
		stayStack.Push(stay);
		stay = null;
	}

	public static void PopStay()
	{
		if (stayStack != null && stayStack.Count != 0)
		{
			stay = (GameSceneEvent)stayStack.Pop();
		}
	}

	public static void ChangeStay(string event_name, object user_data = null)
	{
		if (stay != null)
		{
			stay.eventName = event_name;
			if (user_data != null)
			{
				stay.userData = user_data;
			}
		}
	}

	public static void ChangeStayEventData(object user_data)
	{
		if (stay != null && user_data != null)
		{
			stay.userData = user_data;
		}
	}

	public static void Cancel()
	{
		ResetCurrent();
		stay = null;
	}
}

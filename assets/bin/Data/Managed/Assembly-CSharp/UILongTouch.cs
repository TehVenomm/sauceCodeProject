using UnityEngine;

public class UILongTouch : MonoBehaviour
{
	private const float TOUCH_TIME = 0.75f;

	protected string eventName;

	protected object eventData;

	protected float time;

	public static void Set(GameObject button, string event_name, object event_data = null)
	{
		if (!(button.GetComponent<UIButton>() == null))
		{
			UILongTouch uILongTouch = button.GetComponent<UILongTouch>();
			if (uILongTouch == null)
			{
				uILongTouch = button.AddComponent<UILongTouch>();
			}
			uILongTouch.eventName = event_name;
			uILongTouch.eventData = event_data;
		}
	}

	private void OnHover(bool isOver)
	{
		if (!isOver)
		{
			time = 0f;
		}
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (TutorialMessage.IsActiveButton(base.gameObject))
		{
			if (isPressed)
			{
				time = 0.75f;
			}
			else
			{
				time = 0f;
			}
		}
	}

	private void OnDragOut(GameObject go)
	{
		time = 0f;
	}

	private void Update()
	{
		if (time <= 0f)
		{
			return;
		}
		time -= Time.deltaTime;
		if (time <= 0f)
		{
			UIScrollView componentInParent = GetComponentInParent<UIScrollView>();
			if ((componentInParent == null || (componentInParent != null && !componentInParent.isDragging)) && TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.isCallingOnQuery)
			{
				_SendEvent();
			}
		}
	}

	private void OnDisable()
	{
		time = 0f;
	}

	protected virtual void _SendEvent()
	{
		UIGameSceneEventSender.SendEvent("UILongTouch", base.gameObject, eventName, eventData);
	}
}

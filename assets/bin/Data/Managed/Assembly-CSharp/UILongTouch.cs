using UnityEngine;

public class UILongTouch
{
	private const float TOUCH_TIME = 0.75f;

	protected string eventName;

	protected object eventData;

	protected float time;

	public UILongTouch()
		: this()
	{
	}

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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		if (TutorialMessage.IsActiveButton(this.get_gameObject()))
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
		if (!(time <= 0f))
		{
			time -= Time.get_deltaTime();
			if (time <= 0f)
			{
				UIScrollView componentInParent = this.GetComponentInParent<UIScrollView>();
				if ((componentInParent == null || (componentInParent != null && !componentInParent.isDragging)) && TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.isCallingOnQuery)
				{
					_SendEvent();
				}
			}
		}
	}

	private void OnDisable()
	{
		time = 0f;
	}

	protected virtual void _SendEvent()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		UIGameSceneEventSender.SendEvent("UILongTouch", this.get_gameObject(), eventName, eventData, null);
	}
}

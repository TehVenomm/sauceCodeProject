using UnityEngine;

public class UITouchAndRelease : MonoBehaviour
{
	private string touchEventName;

	private string releaseEventName;

	private object eventData;

	private bool touched;

	public UITouchAndRelease()
		: this()
	{
	}

	public static void Set(GameObject button, string touch_event_name, string release_event_name = null, object event_data = null)
	{
		if (!(button.GetComponent<UIButton>() == null))
		{
			UITouchAndRelease uITouchAndRelease = button.GetComponent<UITouchAndRelease>();
			if (uITouchAndRelease == null)
			{
				uITouchAndRelease = button.AddComponent<UITouchAndRelease>();
			}
			uITouchAndRelease.touchEventName = touch_event_name;
			uITouchAndRelease.releaseEventName = release_event_name;
			uITouchAndRelease.eventData = event_data;
		}
	}

	public static void NoEventRelease(GameObject button)
	{
		UITouchAndRelease component = button.GetComponent<UITouchAndRelease>();
		if (Object.op_Implicit(component))
		{
			component.touched = false;
		}
	}

	private void OnHover(bool isOver)
	{
		if (!isOver && touched)
		{
			Send(is_touch: false);
		}
	}

	private void OnPress(bool isPressed)
	{
		Send(isPressed);
	}

	private void OnDisable()
	{
		if (!AppMain.isApplicationQuit && touched)
		{
			Send(is_touch: false);
		}
	}

	private void Send(bool is_touch)
	{
		if (touched != is_touch)
		{
			touched = is_touch;
			string text = (!is_touch) ? releaseEventName : touchEventName;
			if (!string.IsNullOrEmpty(text))
			{
				UIGameSceneEventSender.SendEvent("UITouchAndRelease", this.get_gameObject(), text, eventData);
			}
		}
	}
}

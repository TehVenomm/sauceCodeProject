using UnityEngine;

public class UITouchAndRelease : MonoBehaviour
{
	private string touchEventName;

	private string releaseEventName;

	private object eventData;

	private bool touched;

	public static void Set(GameObject button, string touch_event_name, string release_event_name = null, object event_data = null)
	{
		if (!((Object)button.GetComponent<UIButton>() == (Object)null))
		{
			UITouchAndRelease uITouchAndRelease = button.GetComponent<UITouchAndRelease>();
			if ((Object)uITouchAndRelease == (Object)null)
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
		if ((bool)component)
		{
			component.touched = false;
		}
	}

	private void OnHover(bool isOver)
	{
		if (!isOver && touched)
		{
			Send(false);
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
			Send(false);
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
				UIGameSceneEventSender.SendEvent("UITouchAndRelease", base.gameObject, text, eventData, null);
			}
		}
	}
}

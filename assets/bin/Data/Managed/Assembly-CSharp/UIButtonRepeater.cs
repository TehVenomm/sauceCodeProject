using UnityEngine;

[RequireComponent(typeof(UIGameSceneEventSender))]
public class UIButtonRepeater : UILongTouch
{
	private const float FAST_MODE_PUSH_TIME = -1f;

	public float firstInterval = 0.15f;

	public float interval = 0.05f;

	public float shortInterval = 0.025f;

	private float pushTime;

	private float repeatTime;

	private bool isFirstWait = true;

	private bool terminateRepeat;

	public static void SetRepeatButton(GameObject button, string event_name, object event_data = null)
	{
		if (!((Object)button.GetComponent<UIButton>() == (Object)null))
		{
			UIButtonRepeater uIButtonRepeater = button.GetComponent<UIButtonRepeater>();
			if ((Object)uIButtonRepeater == (Object)null)
			{
				uIButtonRepeater = button.AddComponent<UIButtonRepeater>();
			}
			uIButtonRepeater.eventName = event_name;
			uIButtonRepeater.eventData = event_data;
		}
	}

	public void Terminate()
	{
		terminateRepeat = true;
	}

	protected override void _SendEvent()
	{
		if (CheckSendTime())
		{
			UIGameSceneEventSender.SendEvent("UIButtonRepeater", base.gameObject, eventName, eventData, null);
		}
	}

	protected override void OnPress(bool isPressed)
	{
		repeatTime = 0f;
		pushTime = 0f;
		isFirstWait = true;
		terminateRepeat = false;
		base.OnPress(isPressed);
	}

	private bool CheckSendTime()
	{
		if (terminateRepeat)
		{
			time = 0f;
			return false;
		}
		time = 0.001f;
		repeatTime -= Time.deltaTime;
		if (repeatTime > 0f)
		{
			return false;
		}
		repeatTime = GetIntervalTime();
		return true;
	}

	private float GetIntervalTime()
	{
		pushTime -= Time.deltaTime;
		float result;
		if (isFirstWait)
		{
			result = firstInterval;
			isFirstWait = false;
		}
		else
		{
			result = ((!(pushTime < -1f)) ? interval : shortInterval);
		}
		return result;
	}
}

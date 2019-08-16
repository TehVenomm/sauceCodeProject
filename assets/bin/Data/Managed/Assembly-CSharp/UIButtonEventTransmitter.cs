using UnityEngine;

public class UIButtonEventTransmitter : MonoBehaviour
{
	public GameObject transmit_target;

	public UIButtonEventTransmitter()
		: this()
	{
	}

	private void OnPress(bool isPressed)
	{
		if (transmit_target != null)
		{
			transmit_target.SendMessage("OnPress", (object)isPressed, 1);
		}
	}

	private void OnHover(bool isOver)
	{
		if (transmit_target != null)
		{
			transmit_target.SendMessage("OnHover", (object)isOver, 1);
		}
	}
}

using UnityEngine;

public class UIButtonEventTransmitter : MonoBehaviour
{
	public GameObject transmit_target;

	private void OnPress(bool isPressed)
	{
		if (transmit_target != null)
		{
			transmit_target.SendMessage("OnPress", isPressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnHover(bool isOver)
	{
		if (transmit_target != null)
		{
			transmit_target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}
}

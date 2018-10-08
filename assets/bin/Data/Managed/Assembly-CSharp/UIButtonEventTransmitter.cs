using UnityEngine;

public class UIButtonEventTransmitter : MonoBehaviour
{
	public GameObject transmit_target;

	private void OnPress(bool isPressed)
	{
		if ((Object)transmit_target != (Object)null)
		{
			transmit_target.SendMessage("OnPress", isPressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnHover(bool isOver)
	{
		if ((Object)transmit_target != (Object)null)
		{
			transmit_target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}
}

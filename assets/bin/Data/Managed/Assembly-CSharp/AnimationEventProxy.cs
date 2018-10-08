using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
	public interface IEvent
	{
		void OnEvent();

		void OnEventStr(string str);

		void OnEventInt(int i);
	}

	public IEvent listener;

	private void OnEvent()
	{
		if (listener != null)
		{
			listener.OnEvent();
		}
	}

	private void OnEventStr(string str)
	{
		if (listener != null)
		{
			listener.OnEventStr(str);
		}
	}

	private void OnEventInt(int i)
	{
		if (listener != null)
		{
			listener.OnEventInt(i);
		}
	}
}

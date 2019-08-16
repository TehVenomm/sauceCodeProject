using UnityEngine;

public class RealTime : MonoBehaviour
{
	public static float time => Time.get_unscaledTime();

	public static float deltaTime => Time.get_unscaledDeltaTime();

	public RealTime()
		: this()
	{
	}
}

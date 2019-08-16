using UnityEngine;

public abstract class UIScreenRotationHandler : MonoBehaviour
{
	[SerializeField]
	private bool autoInvoke;

	private bool prevIsPortrait;

	protected UIScreenRotationHandler()
		: this()
	{
	}

	protected abstract void OnScreenRotate(bool is_portrait);

	private void Awake()
	{
		if (autoInvoke && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			InvokeRotate();
		}
	}

	public void InvokeRotate()
	{
		prevIsPortrait = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
		OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
	}

	private void OnEnable()
	{
		if (autoInvoke && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			if (prevIsPortrait != MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
			{
				InvokeRotate();
			}
		}
	}

	private void OnDisable()
	{
		if (autoInvoke && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
			prevIsPortrait = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
		}
	}
}

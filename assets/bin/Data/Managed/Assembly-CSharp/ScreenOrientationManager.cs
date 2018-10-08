using UnityEngine;

public class ScreenOrientationManager : MonoBehaviourSingleton<ScreenOrientationManager>
{
	public delegate void OnScreenRotateDelegate(bool is_portrait);

	private float timer;

	public bool isPortrait
	{
		get;
		protected set;
	}

	public event OnScreenRotateDelegate OnScreenRotate;

	protected override void Awake()
	{
		isPortrait = CheckIsPortrait();
	}

	private void Start()
	{
		EventScreenRotate(isPortrait);
	}

	private void Update()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			timer += Time.get_deltaTime();
			if (timer >= 2f)
			{
				timer = 0f;
				bool orientation = MonoBehaviourSingleton<GameSceneManager>.I.isAvailableScreenRotationScene();
				GameSceneGlobalSettings.SetOrientation(orientation);
			}
		}
		bool isPortrait = this.isPortrait;
		this.isPortrait = CheckIsPortrait();
		if (isPortrait != this.isPortrait)
		{
			EventScreenRotate(this.isPortrait);
		}
	}

	protected bool CheckIsPortrait()
	{
		return Screen.get_width() < Screen.get_height();
	}

	public void EventScreenRotate(bool is_portrait)
	{
		if (this.OnScreenRotate != null)
		{
			this.OnScreenRotate(is_portrait);
		}
	}
}

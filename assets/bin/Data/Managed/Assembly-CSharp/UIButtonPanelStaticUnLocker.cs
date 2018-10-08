using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIButtonPanelStaticUnLocker
{
	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	private UIButton btn;

	private bool isLock;

	private float timer;

	public UIButtonPanelStaticUnLocker()
		: this()
	{
	}

	private void Awake()
	{
		btn = this.GetComponent<UIButton>();
	}

	private void OnPress(bool pressed)
	{
		if (!isLock)
		{
			panelChange.UnLock();
		}
		timer = btn.duration + 0.1f;
		isLock = true;
	}

	private void LateUpdate()
	{
		if (isLock)
		{
			timer -= Time.get_deltaTime();
			if (!(timer > 0f))
			{
				panelChange.Lock();
				isLock = false;
			}
		}
	}
}

using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIButtonPanelStaticUnLocker : MonoBehaviour
{
	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	private UIButton btn;

	private bool isLock;

	private float timer;

	private void Awake()
	{
		btn = GetComponent<UIButton>();
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
			timer -= Time.deltaTime;
			if (!(timer > 0f))
			{
				panelChange.Lock();
				isLock = false;
			}
		}
	}
}

using UnityEngine;

public class UIScrollablePopupListItem : MonoBehaviour
{
	private UIScrollablePopupList popupRoot;

	private void Awake()
	{
		popupRoot = GetComponentInParent<UIScrollablePopupList>();
	}

	private void OnPress(bool isDown)
	{
		popupRoot.ClosePopup();
	}
}

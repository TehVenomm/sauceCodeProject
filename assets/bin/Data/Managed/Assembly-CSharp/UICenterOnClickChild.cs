using UnityEngine;

public class UICenterOnClickChild : MonoBehaviour
{
	private void OnClick()
	{
		UICenterOnClick uICenterOnClick = NGUITools.FindInParents<UICenterOnClick>(base.gameObject);
		if (uICenterOnClick == null)
		{
			return;
		}
		Transform transform = uICenterOnClick.transform;
		UICenterOnChild uICenterOnChild = NGUITools.FindInParents<UICenterOnChild>(base.gameObject);
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		if (uICenterOnChild != null)
		{
			if (uICenterOnChild.enabled)
			{
				uICenterOnChild.CenterOn(transform);
			}
		}
		else if (uIPanel != null && uIPanel.clipping != 0)
		{
			UIScrollView component = uIPanel.GetComponent<UIScrollView>();
			Vector3 pos = -uIPanel.cachedTransform.InverseTransformPoint(transform.position);
			if (!component.canMoveHorizontally)
			{
				pos.x = uIPanel.cachedTransform.localPosition.x;
			}
			if (!component.canMoveVertically)
			{
				pos.y = uIPanel.cachedTransform.localPosition.y;
			}
			SpringPanel.Begin(uIPanel.cachedGameObject, pos, 6f);
		}
	}
}

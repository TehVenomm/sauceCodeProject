using UnityEngine;

public class UICenterOnClickChild : MonoBehaviour
{
	private void OnClick()
	{
		UICenterOnClick uICenterOnClick = NGUITools.FindInParents<UICenterOnClick>(base.gameObject);
		if (!((Object)uICenterOnClick == (Object)null))
		{
			Transform transform = uICenterOnClick.transform;
			UICenterOnChild uICenterOnChild = NGUITools.FindInParents<UICenterOnChild>(base.gameObject);
			UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
			if ((Object)uICenterOnChild != (Object)null)
			{
				if (uICenterOnChild.enabled)
				{
					uICenterOnChild.CenterOn(transform);
				}
			}
			else if ((Object)uIPanel != (Object)null && uIPanel.clipping != 0)
			{
				UIScrollView component = uIPanel.GetComponent<UIScrollView>();
				Vector3 pos = -uIPanel.cachedTransform.InverseTransformPoint(transform.position);
				if (!component.canMoveHorizontally)
				{
					Vector3 localPosition = uIPanel.cachedTransform.localPosition;
					pos.x = localPosition.x;
				}
				if (!component.canMoveVertically)
				{
					Vector3 localPosition2 = uIPanel.cachedTransform.localPosition;
					pos.y = localPosition2.y;
				}
				SpringPanel.Begin(uIPanel.cachedGameObject, pos, 6f);
			}
		}
	}
}

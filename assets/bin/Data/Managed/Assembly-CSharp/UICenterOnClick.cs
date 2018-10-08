using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Center Scroll View on Click")]
public class UICenterOnClick
{
	public UICenterOnClick()
		: this()
	{
	}

	private void OnClick()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		UICenterOnChild uICenterOnChild = NGUITools.FindInParents<UICenterOnChild>(this.get_gameObject());
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(this.get_gameObject());
		if (uICenterOnChild != null)
		{
			if (uICenterOnChild.get_enabled())
			{
				uICenterOnChild.CenterOn(this.get_transform());
			}
		}
		else if (uIPanel != null && uIPanel.clipping != 0)
		{
			UIScrollView component = uIPanel.GetComponent<UIScrollView>();
			Vector3 pos = -uIPanel.cachedTransform.InverseTransformPoint(this.get_transform().get_position());
			if (!component.canMoveHorizontally)
			{
				Vector3 localPosition = uIPanel.cachedTransform.get_localPosition();
				pos.x = localPosition.x;
			}
			if (!component.canMoveVertically)
			{
				Vector3 localPosition2 = uIPanel.cachedTransform.get_localPosition();
				pos.y = localPosition2.y;
			}
			SpringPanel.Begin(uIPanel.cachedGameObject, pos, 6f);
		}
	}
}

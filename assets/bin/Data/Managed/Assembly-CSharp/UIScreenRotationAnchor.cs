using System;
using UnityEngine;

public class UIScreenRotationAnchor : UIScreenRotationHandler
{
	[Serializable]
	private class Anchors
	{
		[SerializeField]
		private UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

		[SerializeField]
		private UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

		[SerializeField]
		private UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

		[SerializeField]
		private UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

		public void Set(UIRect rect)
		{
			rect.leftAnchor.Set(leftAnchor.target, leftAnchor.relative, (float)leftAnchor.absolute);
			rect.rightAnchor.Set(rightAnchor.target, rightAnchor.relative, (float)rightAnchor.absolute);
			rect.bottomAnchor.Set(bottomAnchor.target, bottomAnchor.relative, (float)bottomAnchor.absolute);
			rect.topAnchor.Set(topAnchor.target, topAnchor.relative, (float)topAnchor.absolute);
		}
	}

	[SerializeField]
	private UIRect rect;

	[SerializeField]
	private Anchors portrait;

	[SerializeField]
	private Anchors landscape;

	protected override void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			portrait.Set(rect);
		}
		else
		{
			landscape.Set(rect);
		}
	}
}

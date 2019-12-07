public class AbilityBGWrapContent : UIWrapContent
{
	private UIScrollView scroll;

	protected override void OnMove(UIPanel panel)
	{
		if (scroll == null)
		{
			scroll = GetComponentInParent<UIScrollView>();
		}
		base.OnMove(panel);
		scroll.restrictWithinPanel = true;
	}
}

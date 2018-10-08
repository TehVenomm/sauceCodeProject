public class HomeStageTouchEvent : HomeStageEventBase
{
	public const int EVENT_LAYER = 1;

	public const int EVENT_LAYER_MASK = 2;

	protected override int GetLayer()
	{
		return 1;
	}
}

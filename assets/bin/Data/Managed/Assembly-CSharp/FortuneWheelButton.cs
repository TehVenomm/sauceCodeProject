public class FortuneWheelButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN
	}

	protected override void OnOpen()
	{
		PlayTween(UI.OBJ_TWEEN, true, null, false, 0);
		base.OnOpen();
	}
}

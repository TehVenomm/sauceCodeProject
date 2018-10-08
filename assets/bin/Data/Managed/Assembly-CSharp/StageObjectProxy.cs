public class StageObjectProxy
{
	public StageObject stageObject;

	public StageObjectProxy()
		: this()
	{
	}

	private void OnAnimatorMove()
	{
		stageObject.OnAnimatorMove();
	}
}

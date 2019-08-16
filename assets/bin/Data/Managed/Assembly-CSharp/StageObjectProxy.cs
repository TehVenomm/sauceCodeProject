using UnityEngine;

public class StageObjectProxy : MonoBehaviour
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

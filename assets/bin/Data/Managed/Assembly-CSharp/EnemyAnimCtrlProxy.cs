using UnityEngine;

public class EnemyAnimCtrlProxy : MonoBehaviour
{
	public EnemyAnimCtrl enemyAnimCtrl;

	public EnemyAnimCtrlProxy()
		: this()
	{
	}

	private void OnAnimatorMove()
	{
		enemyAnimCtrl.OnAnimatorMove();
	}
}

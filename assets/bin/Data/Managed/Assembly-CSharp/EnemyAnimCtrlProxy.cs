using UnityEngine;

public class EnemyAnimCtrlProxy : MonoBehaviour
{
	public EnemyAnimCtrl enemyAnimCtrl;

	private void OnAnimatorMove()
	{
		enemyAnimCtrl.OnAnimatorMove();
	}
}

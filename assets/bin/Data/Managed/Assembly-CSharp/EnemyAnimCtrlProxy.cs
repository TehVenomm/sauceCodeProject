public class EnemyAnimCtrlProxy
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

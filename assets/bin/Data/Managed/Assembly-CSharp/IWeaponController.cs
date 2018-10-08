public interface IWeaponController
{
	void Init(Player _player);

	void OnLoadComplete();

	void Update();

	void OnEndAction();

	void OnActDead();

	void OnActAvoid();

	void OnRelease();
}

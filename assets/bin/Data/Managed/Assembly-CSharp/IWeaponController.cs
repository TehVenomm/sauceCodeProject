public interface IWeaponController
{
	void Init(Player _player);

	void OnLoadComplete();

	void Update();

	void OnEndAction();

	void OnActDead();

	void OnActReaction();

	void OnActAvoid();

	void OnActSkillAction();

	void OnRelease();

	void OnActAttack(int id);

	void OnBuffStart(BuffParam.BuffData data);

	void OnBuffEnd(BuffParam.BUFFTYPE type);

	void OnChangeWeapon();

	void OnAttackedHitFix(AttackedHitStatusFix status);
}

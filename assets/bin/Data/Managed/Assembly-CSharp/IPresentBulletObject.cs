using UnityEngine;

public interface IPresentBulletObject
{
	void Initialize(int id, BulletData bulletData, Transform transform);

	void SetPosition(Vector3 position);

	void SetSkillParam(SkillInfo.SkillParam skillParam);

	int GetPresentBulletId();

	void OnPicked();
}

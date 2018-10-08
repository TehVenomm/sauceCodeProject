using UnityEngine;

public interface IFieldGimmickCannon : IFieldGimmickObject
{
	Vector3 GetPosition();

	Transform GetCannonTransform();

	bool IsUsing();

	bool IsAbleToUse();

	bool IsCooling();

	void OnBoard(Player player);

	void OnLeave();

	void Shot();

	void UpdateTargetMarker(bool isNear);

	void ApplyCannonVector(Vector3 cannonVec);
}

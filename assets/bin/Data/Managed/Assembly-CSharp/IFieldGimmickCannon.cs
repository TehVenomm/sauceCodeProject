using UnityEngine;

public interface IFieldGimmickCannon : IFieldGimmickObject
{
	Vector3 GetPosition();

	Transform GetCannonTransform();

	Transform GetBaseTransform();

	Vector3 GetBaseTransformForward();

	bool IsUsing();

	bool IsAbleToUse();

	bool IsCooling();

	bool IsAimCamera();

	void OnBoard(Player player);

	void OnLeave();

	void Shot();

	void ApplyCannonVector(Vector3 cannonVec);
}

using UnityEngine;

public interface IFieldGimmickObject
{
	int GetId();

	void Initialize(FieldMapTable.FieldGimmickPointTableData pointData);

	void RequestDestroy();

	void SetTransform(Transform trans);

	Transform GetTransform();

	string GetObjectName();

	float GetTargetRadius();

	float GetTargetSqrRadius();

	void UpdateTargetMarker(bool isNear);
}

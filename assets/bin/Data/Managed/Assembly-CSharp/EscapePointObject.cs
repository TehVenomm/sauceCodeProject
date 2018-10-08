using UnityEngine;

public class EscapePointObject : StageObject
{
	public bool isEnemyOnEscapePoint
	{
		get;
		private set;
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		return false;
	}

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		base.Awake();
		Utility.SetLayerWithChildren(this.get_transform(), 31);
	}

	private void OnTriggerStay(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (collider.get_gameObject().get_layer() == 10)
		{
			isEnemyOnEscapePoint = true;
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (collider.get_gameObject().get_layer() == 10)
		{
			isEnemyOnEscapePoint = false;
		}
	}
}

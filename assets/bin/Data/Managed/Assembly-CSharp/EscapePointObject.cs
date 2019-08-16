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
		base.Awake();
		Utility.SetLayerWithChildren(this.get_transform(), 31);
	}

	private void OnTriggerStay(Collider collider)
	{
		if (collider.get_gameObject().get_layer() == 10)
		{
			isEnemyOnEscapePoint = true;
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.get_gameObject().get_layer() == 10)
		{
			isEnemyOnEscapePoint = false;
		}
	}
}

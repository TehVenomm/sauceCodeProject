using UnityEngine;

public class GachaDecoPosLink
{
	public Transform target;

	public float limitX;

	private Transform _transform;

	public GachaDecoPosLink()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
		if (target == null)
		{
			this.set_enabled(false);
		}
	}

	private void LateUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		_transform.set_position(target.get_position());
		Vector3 localPosition = _transform.get_localPosition();
		if (localPosition.x > limitX)
		{
			localPosition.x = limitX;
			_transform.set_localPosition(localPosition);
		}
	}
}

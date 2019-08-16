using UnityEngine;

public class HomeDragonRandomMove : MonoBehaviour
{
	private readonly float MAX_DISTANCE = 0.4f;

	private Transform _transform;

	private Vector3 originalPosition;

	private float maxDistance;

	private Vector2 coordX;

	private Vector2 coordY;

	private Vector2 coordZ;

	private Vector2 dirX;

	private Vector2 dirY;

	private Vector2 dirZ;

	public HomeDragonRandomMove()
		: this()
	{
	}

	private void Awake()
	{
		_transform = this.get_transform();
		this.set_enabled(false);
	}

	public void Reset()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		originalPosition = _transform.get_localPosition();
		coordX = Vector2.get_zero();
		coordY = Vector2.get_zero();
		coordZ = Vector2.get_zero();
		Vector2 val = default(Vector2);
		val._002Ector(Random.get_value(), Random.get_value());
		dirX = val.get_normalized();
		Vector2 val2 = default(Vector2);
		val2._002Ector(Random.get_value(), Random.get_value());
		dirY = val2.get_normalized();
		Vector2 val3 = default(Vector2);
		val3._002Ector(Random.get_value(), Random.get_value());
		dirZ = val3.get_normalized();
		maxDistance = 0f;
		this.set_enabled(true);
	}

	private void Update()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		float num = (Mathf.PerlinNoise(coordX.x, coordX.y) - 0.5f) * maxDistance;
		float num2 = (Mathf.PerlinNoise(coordY.x, coordY.y) - 0.5f) * maxDistance;
		float num3 = (Mathf.PerlinNoise(coordZ.x, coordZ.y) - 0.5f) * maxDistance;
		_transform.set_localPosition(originalPosition + new Vector3(num, num2, num3));
		float num4 = Time.get_deltaTime() * 0.5f;
		ref Vector2 reference = ref coordX;
		reference.x += dirX.x * num4;
		ref Vector2 reference2 = ref coordX;
		reference2.y += dirX.y * num4;
		num4 *= 0.9f;
		ref Vector2 reference3 = ref coordY;
		reference3.x += dirY.x * num4;
		ref Vector2 reference4 = ref coordY;
		reference4.y += dirY.y * num4;
		num4 *= 0.9f;
		ref Vector2 reference5 = ref coordZ;
		reference5.x += dirZ.x * num4;
		ref Vector2 reference6 = ref coordZ;
		reference6.y += dirZ.y * num4;
		if (maxDistance < MAX_DISTANCE)
		{
			maxDistance += Time.get_deltaTime() * 0.5f;
			if (maxDistance > MAX_DISTANCE)
			{
				maxDistance = MAX_DISTANCE;
			}
		}
	}
}

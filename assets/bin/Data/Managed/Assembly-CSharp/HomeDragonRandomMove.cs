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

	private void Awake()
	{
		_transform = base.transform;
		base.enabled = false;
	}

	public void Reset()
	{
		originalPosition = _transform.localPosition;
		coordX = Vector2.zero;
		coordY = Vector2.zero;
		coordZ = Vector2.zero;
		dirX = new Vector2(Random.value, Random.value).normalized;
		dirY = new Vector2(Random.value, Random.value).normalized;
		dirZ = new Vector2(Random.value, Random.value).normalized;
		maxDistance = 0f;
		base.enabled = true;
	}

	private void Update()
	{
		float x = (Mathf.PerlinNoise(coordX.x, coordX.y) - 0.5f) * maxDistance;
		float y = (Mathf.PerlinNoise(coordY.x, coordY.y) - 0.5f) * maxDistance;
		float z = (Mathf.PerlinNoise(coordZ.x, coordZ.y) - 0.5f) * maxDistance;
		_transform.localPosition = originalPosition + new Vector3(x, y, z);
		float num = Time.deltaTime * 0.5f;
		coordX.x += dirX.x * num;
		coordX.y += dirX.y * num;
		num *= 0.9f;
		coordY.x += dirY.x * num;
		coordY.y += dirY.y * num;
		num *= 0.9f;
		coordZ.x += dirZ.x * num;
		coordZ.y += dirZ.y * num;
		if (maxDistance < MAX_DISTANCE)
		{
			maxDistance += Time.deltaTime * 0.5f;
			if (maxDistance > MAX_DISTANCE)
			{
				maxDistance = MAX_DISTANCE;
			}
		}
	}
}

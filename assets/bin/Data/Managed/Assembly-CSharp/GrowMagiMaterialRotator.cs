using UnityEngine;

public class GrowMagiMaterialRotator : MonoBehaviour
{
	public float speed;

	public Vector3 axis;

	private void Awake()
	{
		Setup(new Vector3(Random.value * 360f, Random.value * 360f, Random.value * 360f), Random.Range(180f, 540f));
	}

	public void Setup(Vector3 axis, float speed)
	{
		this.axis = axis;
		this.speed = speed;
	}

	private void Update()
	{
		base.transform.Rotate(axis, speed * Time.deltaTime);
	}
}

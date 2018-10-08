using UnityEngine;

public class GrowMagiMaterialRotator
{
	public float speed;

	public Vector3 axis;

	public GrowMagiMaterialRotator()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Setup(new Vector3(Random.get_value() * 360f, Random.get_value() * 360f, Random.get_value() * 360f), Random.Range(180f, 540f));
	}

	public void Setup(Vector3 axis, float speed)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		this.axis = axis;
		this.speed = speed;
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.get_transform().Rotate(axis, speed * Time.get_deltaTime());
	}
}

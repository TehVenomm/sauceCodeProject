using UnityEngine;

public class Destroyer
{
	public float time;

	public Destroyer()
		: this()
	{
	}

	private void Start()
	{
		if (time <= 0f)
		{
			this.set_enabled(false);
		}
	}

	private void Update()
	{
		time -= Time.get_deltaTime();
		if (time <= 0f)
		{
			DestroyGameObject();
		}
	}

	public void DestroyGameObject()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Object.Destroy(this.get_gameObject());
	}
}

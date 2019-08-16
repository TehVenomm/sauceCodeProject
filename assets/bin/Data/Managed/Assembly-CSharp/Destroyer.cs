using UnityEngine;

public class Destroyer : MonoBehaviour
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
		Object.Destroy(this.get_gameObject());
	}
}

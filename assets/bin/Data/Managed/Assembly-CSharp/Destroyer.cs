using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public float time;

	private void Start()
	{
		if (time <= 0f)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		time -= Time.deltaTime;
		if (time <= 0f)
		{
			DestroyGameObject();
		}
	}

	public void DestroyGameObject()
	{
		Object.Destroy(base.gameObject);
	}
}

using UnityEngine;

public class GrowMagiMaterialAnimation : MonoBehaviour
{
	[SerializeField]
	public float rotate = 5f;

	private void Start()
	{
	}

	private void Anim()
	{
		GetComponentInChildren<Animation>().Play("MaterialAnim_1");
	}

	private void Update()
	{
		base.transform.Rotate(0f, rotate, 0f);
	}
}

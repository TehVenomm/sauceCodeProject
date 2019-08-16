using UnityEngine;

public class GrowMagiMaterialAnimation : MonoBehaviour
{
	[SerializeField]
	public float rotate = 5f;

	public GrowMagiMaterialAnimation()
		: this()
	{
	}

	private void Start()
	{
	}

	private void Anim()
	{
		this.GetComponentInChildren<Animation>().Play("MaterialAnim_1");
	}

	private void Update()
	{
		this.get_transform().Rotate(0f, rotate, 0f);
	}
}

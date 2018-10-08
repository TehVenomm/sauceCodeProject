using UnityEngine;

public class GrowMagiMaterialAnimation
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_transform().Rotate(0f, rotate, 0f);
	}
}

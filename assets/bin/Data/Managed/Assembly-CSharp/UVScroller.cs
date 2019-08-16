using UnityEngine;

public class UVScroller : MonoBehaviour
{
	private Material mat;

	private Vector2 offset;

	[SerializeField]
	private Vector2 speed;

	public UVScroller()
		: this()
	{
	}

	private void Awake()
	{
		mat = this.GetComponent<Renderer>().get_material();
	}

	private void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		offset += speed * Time.get_deltaTime();
		offset = new Vector2(Mathf.Repeat(offset.x, 1f), Mathf.Repeat(offset.y, 1f));
		mat.SetTextureOffset("_MainTex", offset);
	}
}

using UnityEngine;

public class UVScroller : MonoBehaviour
{
	private Material mat;

	private Vector2 offset;

	[SerializeField]
	private Vector2 speed;

	private void Awake()
	{
		mat = GetComponent<Renderer>().material;
	}

	private void Update()
	{
		offset += speed * Time.deltaTime;
		offset = new Vector2(Mathf.Repeat(offset.x, 1f), Mathf.Repeat(offset.y, 1f));
		mat.SetTextureOffset("_MainTex", offset);
	}
}

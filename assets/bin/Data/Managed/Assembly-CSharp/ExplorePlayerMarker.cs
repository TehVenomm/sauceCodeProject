using UnityEngine;

public class ExplorePlayerMarker : MonoBehaviour
{
	[SerializeField]
	private Vector3[] offsets = new Vector3[4];

	private UISprite sprite_;

	private void Awake()
	{
		sprite_ = GetComponent<UISprite>();
	}

	public void SetIndex(int idx)
	{
		if (!(null == sprite_) && 0 <= idx && 4 > idx)
		{
			sprite_.spriteName = "PlayerMarker_skin_2d_" + idx.ToString("D2");
			base.transform.localPosition = offsets[idx];
		}
	}
}

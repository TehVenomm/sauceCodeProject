using UnityEngine;

public class ExplorePlayerMarker : MonoBehaviour
{
	[SerializeField]
	private Vector3[] offsets = (Vector3[])new Vector3[4];

	private UISprite sprite_;

	public ExplorePlayerMarker()
		: this()
	{
	}

	private void Awake()
	{
		sprite_ = this.GetComponent<UISprite>();
	}

	public void SetIndex(int idx)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!(null == sprite_) && 0 <= idx && 4 > idx)
		{
			sprite_.spriteName = "PlayerMarker_skin_2d_" + idx.ToString("D2");
			this.get_transform().set_localPosition(offsets[idx]);
		}
	}
}

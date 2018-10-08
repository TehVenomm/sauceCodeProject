using System;
using UnityEngine;

public class ExplorePlayerMarkerMini : MonoBehaviour
{
	[Serializable]
	public class Size
	{
		public int width;

		public int height;
	}

	[SerializeField]
	public Color32[] colors = new Color32[4];

	public Size playerSize;

	public Size friendSize;

	[SerializeField]
	private Vector3[] offsets = new Vector3[4];

	private UISprite sprite_;

	private void Awake()
	{
		sprite_ = GetComponent<UISprite>();
	}

	public void SetIndex(int idx)
	{
		if (!((UnityEngine.Object)null == (UnityEngine.Object)sprite_) && 0 <= idx && 4 > idx)
		{
			if (idx == 0)
			{
				sprite_.spriteName = "dp_radar_player";
				sprite_.color = Color.white;
				sprite_.width = playerSize.width;
				sprite_.height = playerSize.height;
				sprite_.depth += 1;
				base.transform.localPosition = offsets[idx];
			}
			else
			{
				sprite_.spriteName = "dp_radar_another";
				sprite_.color = colors[idx];
				sprite_.width = friendSize.width;
				sprite_.height = friendSize.height;
				base.transform.localPosition = offsets[idx];
			}
		}
	}
}

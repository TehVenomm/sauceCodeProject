using System;
using UnityEngine;

public class ExplorePlayerMarkerMini
{
	[Serializable]
	public class Size
	{
		public int width;

		public int height;
	}

	[SerializeField]
	public Color32[] colors = (Color32[])new Color32[4];

	public Size playerSize;

	public Size friendSize;

	[SerializeField]
	private Vector3[] offsets = (Vector3[])new Vector3[4];

	private UISprite sprite_;

	public ExplorePlayerMarkerMini()
		: this()
	{
	}

	private void Awake()
	{
		sprite_ = this.GetComponent<UISprite>();
	}

	public void SetIndex(int idx)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		if (!(null == sprite_) && 0 <= idx && 4 > idx)
		{
			if (idx == 0)
			{
				sprite_.spriteName = "dp_radar_player";
				sprite_.color = Color.get_white();
				sprite_.width = playerSize.width;
				sprite_.height = playerSize.height;
				sprite_.depth += 1;
				this.get_transform().set_localPosition(offsets[idx]);
			}
			else
			{
				sprite_.spriteName = "dp_radar_another";
				sprite_.color = Color32.op_Implicit(colors[idx]);
				sprite_.width = friendSize.width;
				sprite_.height = friendSize.height;
				this.get_transform().set_localPosition(offsets[idx]);
			}
		}
	}
}

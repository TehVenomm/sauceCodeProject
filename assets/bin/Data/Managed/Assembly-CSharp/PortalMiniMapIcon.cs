using UnityEngine;

public class PortalMiniMapIcon : MiniMapIcon
{
	[SerializeField]
	protected string[] iconSpriteNames;

	[SerializeField]
	protected string[] overIconSpriteNames;

	private PortalObject portal;

	private PortalObject.VIEW_TYPE viewType;

	private bool isFull = true;

	public override void Initialize(MonoBehaviour root_object)
	{
		base.Initialize(root_object);
		portal = (root_object as PortalObject);
	}

	private void Update()
	{
		if (!((Object)portal == (Object)null))
		{
			if (viewType != portal.viewType || isFull != portal.isFull)
			{
				int num = (int)portal.viewType;
				if (portal.viewType == PortalObject.VIEW_TYPE.NOT_TRAVELED && !portal.isFull)
				{
					num = 5;
				}
				icon.spriteName = iconSpriteNames[num];
				if ((Object)overIcon != (Object)null)
				{
					overIcon.spriteName = overIconSpriteNames[num];
				}
			}
			viewType = portal.viewType;
			isFull = portal.isFull;
		}
	}
}

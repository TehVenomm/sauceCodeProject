using UnityEngine;

public class ExploreMapLocation : MonoBehaviour
{
	[SerializeField]
	private int _mapId;

	private Texture2D _icon;

	public int mapId
	{
		get
		{
			return _mapId;
		}
		set
		{
			_mapId = value;
		}
	}

	public Texture2D icon
	{
		get
		{
			return _icon;
		}
		set
		{
			_icon = value;
		}
	}

	public ExploreMapLocation()
		: this()
	{
	}
}

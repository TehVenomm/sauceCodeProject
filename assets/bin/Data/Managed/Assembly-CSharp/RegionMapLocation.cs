using UnityEngine;

public class RegionMapLocation : MonoBehaviour
{
	[SerializeField]
	private int _mapId;

	[SerializeField]
	private RegionMapPortal[] _portal;

	private FieldMapTable.FieldMapTableData _tableData;

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

	public RegionMapPortal[] portal => _portal;

	public FieldMapTable.FieldMapTableData tableData
	{
		get
		{
			if (_tableData == null)
			{
				_tableData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)mapId);
			}
			return _tableData;
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

	public RegionMapLocation()
		: this()
	{
	}
}

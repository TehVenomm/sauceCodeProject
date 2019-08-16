using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class RegionMapRoot : MonoBehaviour
{
	[SerializeField]
	public Material roadMaterial;

	[SerializeField]
	private RegionMapLocation[] _locations;

	[SerializeField]
	private RegionMapPortal[] _portals;

	[SerializeField]
	private MeshRenderer _map;

	private Animator _animator;

	public RegionMapLocation[] locations => _locations;

	public RegionMapPortal[] portals => _portals;

	public MeshRenderer map => _map;

	public Animator animator
	{
		get
		{
			if (_animator == null)
			{
				_animator = this.GetComponent<Animator>();
			}
			return _animator;
		}
	}

	public RegionMapRoot()
		: this()
	{
	}

	public RegionMapLocation FindLocation(int id)
	{
		return Array.Find(locations, (RegionMapLocation l) => l.mapId == id);
	}

	public RegionMapPortal FindEntrancePortal(int id)
	{
		return Array.Find(portals, (RegionMapPortal p) => p.entranceId == id);
	}

	public RegionMapPortal FindExitPortal(int id)
	{
		return Array.Find(portals, (RegionMapPortal p) => p.exitId == id);
	}

	public void InitPortalStatus(Action onComplete)
	{
		this.StartCoroutine(InitPortalStatusImpl(onComplete));
	}

	private IEnumerator InitPortalStatusImpl(Action onComplete)
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		UIntKeyTable<LoadObject> loadTextures = new UIntKeyTable<LoadObject>();
		for (int i = 0; i < locations.Length; i++)
		{
			FieldMapTable.FieldMapTableData tableData = locations[i].tableData;
			if (tableData != null && tableData.hasChildRegion && loadTextures.Get(tableData.iconId) == null)
			{
				loadTextures.Add(tableData.iconId, loadQueue.Load(RESOURCE_CATEGORY.DUNGEON_ICON, ResourceName.GetDungeonIcon(tableData.iconId)));
			}
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		for (int j = 0; j < locations.Length; j++)
		{
			FieldMapTable.FieldMapTableData tableData2 = locations[j].tableData;
			if (tableData2 != null && tableData2.hasChildRegion)
			{
				locations[j].icon = (loadTextures.Get(tableData2.iconId).loadedObject as Texture2D);
			}
		}
		for (int k = 0; k < portals.Length; k++)
		{
			RegionMapPortal regionMapPortal = portals[k];
			if (regionMapPortal.IsVisited())
			{
				regionMapPortal.Open();
			}
		}
		onComplete?.Invoke();
	}
}

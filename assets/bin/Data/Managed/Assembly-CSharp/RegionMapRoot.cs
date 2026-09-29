// Decompiled with JetBrains decompiler
// Type: RegionMapRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
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

  public RegionMapLocation[] locations => this._locations;

  public RegionMapPortal[] portals => this._portals;

  public MeshRenderer map => this._map;

  public RegionMapLocation FindLocation(int id)
  {
    return Array.Find<RegionMapLocation>(this.locations, (Predicate<RegionMapLocation>) (l => l.mapId == id));
  }

  public RegionMapPortal FindEntrancePortal(int id)
  {
    return Array.Find<RegionMapPortal>(this.portals, (Predicate<RegionMapPortal>) (p => p.entranceId == id));
  }

  public RegionMapPortal FindExitPortal(int id)
  {
    return Array.Find<RegionMapPortal>(this.portals, (Predicate<RegionMapPortal>) (p => p.exitId == id));
  }

  public Animator animator
  {
    get
    {
      if (Object.op_Equality((Object) this._animator, (Object) null))
        this._animator = ((Component) this).GetComponent<Animator>();
      return this._animator;
    }
  }

  public void InitPortalStatus(System.Action onComplete)
  {
    this.StartCoroutine(this.InitPortalStatusImpl(onComplete));
  }

  private IEnumerator InitPortalStatusImpl(System.Action onComplete)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    UIntKeyTable<LoadObject> loadTextures = new UIntKeyTable<LoadObject>();
    for (int index = 0; index < this.locations.Length; ++index)
    {
      FieldMapTable.FieldMapTableData tableData = this.locations[index].tableData;
      if (tableData != null && tableData.hasChildRegion && loadTextures.Get(tableData.iconId) == null)
        loadTextures.Add(tableData.iconId, loadingQueue.Load(RESOURCE_CATEGORY.DUNGEON_ICON, ResourceName.GetDungeonIcon(tableData.iconId)));
    }
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    for (int index = 0; index < this.locations.Length; ++index)
    {
      FieldMapTable.FieldMapTableData tableData = this.locations[index].tableData;
      if (tableData != null && tableData.hasChildRegion)
        this.locations[index].icon = loadTextures.Get(tableData.iconId).loadedObject as Texture2D;
    }
    for (int index = 0; index < this.portals.Length; ++index)
    {
      RegionMapPortal portal = this.portals[index];
      if (portal.IsVisited())
        portal.Open();
    }
    if (onComplete != null)
      onComplete();
  }
}

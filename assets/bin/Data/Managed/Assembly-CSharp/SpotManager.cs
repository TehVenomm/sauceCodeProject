// Decompiled with JetBrains decompiler
// Type: SpotManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class SpotManager
{
  [SerializeField]
  private List<SpotManager.Spot> spots = new List<SpotManager.Spot>();
  private GameObject spotRootPrehab;
  private GameObject spotPrefab;
  private Camera targetCamera;

  public Transform spotRootTransform { get; set; }

  public int Count => this.spots.Count;

  public SpotManager(GameObject _spotRootPrefab, GameObject _spotPrefab, Camera _targetCamera)
  {
    this.spotRootPrehab = _spotRootPrefab;
    this.spotPrefab = _spotPrefab;
    this.targetCamera = _targetCamera;
  }

  public Transform SetRoot(Transform t)
  {
    if (Object.op_Equality((Object) this.spotRootTransform, (Object) null))
      this.spotRootTransform = ResourceUtility.Realizes((Object) this.spotRootPrehab, t);
    else if (Object.op_Inequality((Object) t, (Object) null))
      this.spotRootTransform.parent = t;
    return this.spotRootTransform;
  }

  public SpotManager.Spot FindSpot(int _id)
  {
    return this.spots.Find((Predicate<SpotManager.Spot>) (c => c.id == _id));
  }

  public void ClearAllSpot()
  {
    for (int index = 0; index < this.spots.Count; ++index)
    {
      if (this.spots[index] != null)
      {
        Object.Destroy((Object) ((Component) this.spots[index]._transform).gameObject);
        this.spots[index] = (SpotManager.Spot) null;
      }
    }
    this.spots.Clear();
  }

  public void CreateSpotRoot()
  {
    if (!Object.op_Equality((Object) this.spotRootTransform, (Object) null))
      return;
    this.spotRootTransform = ResourceUtility.Realizes((Object) this.spotRootPrehab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform);
  }

  public SpotManager.Spot AddSpot(
    int id,
    string name,
    Vector3 pos,
    SpotManager.ICON_TYPE icon,
    string event_name,
    bool isNew = false,
    bool canUnlockNewPortal = false,
    bool viewEnemyPopBallon = false,
    object _event = null,
    Texture2D dungeon_icon = null,
    bool isExistDelivery = false,
    SpotManager.HAPPEN_CONDITION happenQuestCondition = SpotManager.HAPPEN_CONDITION.NONE,
    int mapNo = 0)
  {
    this.CreateSpotRoot();
    SpotManager.Spot spot = new SpotManager.Spot();
    spot.id = id;
    spot.originalPos = pos;
    spot.type = icon;
    spot.mapNo = mapNo;
    spot._transform = ResourceUtility.Realizes((Object) this.spotPrefab, this.spotRootTransform, 5);
    Transform transform1 = spot._transform.Find("LBL_NAME");
    if (Object.op_Inequality((Object) transform1, (Object) null))
    {
      UILabel component = ((Component) transform1).GetComponent<UILabel>();
      component.text = name;
      ((Component) component).gameObject.SetActive(icon != SpotManager.ICON_TYPE.NOT_OPENED);
      Transform transform2 = transform1.Find("SPR_NAME_BASE");
      if (Object.op_Inequality((Object) transform2, (Object) null))
        ((Component) transform2).GetComponent<UITexture>().width = component.width + 45;
    }
    if (mapNo > 0)
    {
      Transform transform3 = spot._transform.Find("LBL_LOCATION_NUMBER");
      if (Object.op_Inequality((Object) transform3, (Object) null))
      {
        ((Component) transform3).gameObject.SetActive(true);
        ((Component) transform3).gameObject.GetComponent<UILabel>().text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 25U) + mapNo.ToString();
      }
    }
    Transform transform4 = spot._transform.Find("SPR_TWN_NEW");
    if (Object.op_Inequality((Object) transform4, (Object) null))
      ((Component) transform4).gameObject.SetActive(isNew);
    Transform transform5 = spot._transform.Find("SPR_ICON_NEW");
    if (Object.op_Inequality((Object) transform5, (Object) null))
      ((Component) transform5).gameObject.SetActive(icon == SpotManager.ICON_TYPE.NEW);
    Transform transform6 = spot._transform.Find("SPR_ICON_CLEARED");
    if (Object.op_Inequality((Object) transform6, (Object) null))
      ((Component) transform6).gameObject.SetActive(icon == SpotManager.ICON_TYPE.CLEARED);
    Transform transform7 = spot._transform.Find("SPR_ICON_HOME");
    if (Object.op_Inequality((Object) transform7, (Object) null))
      ((Component) transform7).gameObject.SetActive(icon == SpotManager.ICON_TYPE.HOME);
    Transform transform8 = spot._transform.Find("SPR_ICON_NOT_OPENED");
    if (Object.op_Inequality((Object) transform8, (Object) null))
      ((Component) transform8).gameObject.SetActive(icon == SpotManager.ICON_TYPE.NOT_OPENED);
    Transform transform9 = spot._transform.Find("SPR_ICON_HARD");
    if (Object.op_Inequality((Object) transform9, (Object) null))
    {
      ((Component) transform9).gameObject.SetActive(icon == SpotManager.ICON_TYPE.HARD || icon == SpotManager.ICON_TYPE.HARD_NEW);
      if (icon == SpotManager.ICON_TYPE.HARD)
      {
        Transform transform10 = transform9.Find("DODAIADD");
        if (Object.op_Inequality((Object) null, (Object) transform10))
          ((Component) transform10).gameObject.SetActive(false);
      }
    }
    Transform transform11 = spot._transform.Find("OBJ_NEW_PORTAL");
    if (Object.op_Inequality((Object) transform11, (Object) null))
      ((Component) transform11).gameObject.SetActive(canUnlockNewPortal);
    Transform transform12 = spot._transform.Find("OBJ_POP_PORTAL");
    if (Object.op_Inequality((Object) transform12, (Object) null))
      ((Component) transform12).gameObject.SetActive(viewEnemyPopBallon);
    Transform transform13 = spot._transform.Find("SPR_ICON_DUNGEON");
    if (Object.op_Inequality((Object) transform13, (Object) null))
    {
      UITexture component = ((Component) transform13).GetComponent<UITexture>();
      if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) dungeon_icon, (Object) null) && icon == SpotManager.ICON_TYPE.CHILD_REGION)
        component.mainTexture = (Texture) dungeon_icon;
    }
    Transform transform14 = spot._transform.Find("SPR_DELIVERY_TARGET");
    if (Object.op_Inequality((Object) transform14, (Object) null))
      ((Component) transform14).gameObject.SetActive(isExistDelivery);
    Transform transform15 = spot._transform.Find("SPR_SUBMISSION_CLEARED");
    if (Object.op_Inequality((Object) transform15, (Object) null))
      ((Component) transform15).gameObject.SetActive(happenQuestCondition == SpotManager.HAPPEN_CONDITION.ALL_CLEAR);
    Transform transform16 = spot._transform.Find("SPR_SUBMISSION_NOT_CLEARED");
    if (Object.op_Inequality((Object) transform16, (Object) null))
      ((Component) transform16).gameObject.SetActive(happenQuestCondition == SpotManager.HAPPEN_CONDITION.NOT_CLEAR);
    UIGameSceneEventSender component1 = ((Component) spot._transform.Find("SPR_BUTTON")).GetComponent<UIGameSceneEventSender>();
    if (string.IsNullOrEmpty(event_name))
    {
      Object.Destroy((Object) ((Component) component1).gameObject);
    }
    else
    {
      component1.eventName = event_name;
      component1.eventData = _event;
    }
    this.spots.Add(spot);
    return spot;
  }

  public void Update()
  {
    this.spots.ForEach((Action<SpotManager.Spot>) (spot => spot.Update(this.targetCamera)));
  }

  public List<SpotManager.Spot> GetAllSpots() => this.spots;

  public SpotManager.Spot GetSpot(int regionId)
  {
    foreach (SpotManager.Spot spot in this.spots)
    {
      if (spot.id == regionId)
        return spot;
    }
    return (SpotManager.Spot) null;
  }

  public enum ICON_TYPE
  {
    NEW,
    CLEARED,
    HOME,
    NOT_OPENED,
    HARD,
    HARD_NEW,
    CHILD_REGION,
    INVISIBLE,
  }

  public enum HAPPEN_CONDITION
  {
    ALL_CLEAR,
    NOT_CLEAR,
    NONE,
  }

  [Serializable]
  public class Spot
  {
    public int id;
    public Vector3 originalPos;
    public Transform _transform;
    public SpotManager.ICON_TYPE type;
    public int mapNo;

    public void Update(Camera camera)
    {
      if (!Object.op_Inequality((Object) null, (Object) camera))
        return;
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(camera.WorldToScreenPoint(this.originalPos));
      worldPoint.z = 0.0f;
      this._transform.position = worldPoint;
    }

    public Vector2 GetScreenPos()
    {
      Vector3 viewportPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.WorldToViewportPoint(this._transform.position);
      return new Vector2(viewportPoint.x, viewportPoint.y);
    }

    public void SetIconSprite(
      string iconObjectName,
      Texture2D icon,
      int iconWidth,
      int iconHeight)
    {
      Transform transform = this._transform.Find(iconObjectName);
      if (Object.op_Equality((Object) transform, (Object) null))
        return;
      UITexture component = ((Component) transform).gameObject.GetComponent<UITexture>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      component.mainTexture = (Texture) icon;
      component.width = iconWidth;
      component.height = iconHeight;
    }

    public void ReleaseRegion(string name, Texture2D icon, string eventName)
    {
      Transform transform1 = this._transform.Find("LBL_NAME");
      if (Object.op_Inequality((Object) transform1, (Object) null))
        ((Component) transform1).GetComponent<UILabel>().text = name;
      Transform transform2 = this._transform.Find("SPR_ICON");
      if (Object.op_Equality((Object) transform2, (Object) null))
        return;
      UITexture component1 = ((Component) transform2).gameObject.GetComponent<UITexture>();
      if (Object.op_Equality((Object) component1, (Object) null))
        return;
      component1.mainTexture = (Texture) icon;
      UIGameSceneEventSender component2 = ((Component) this._transform.Find("SPR_BUTTON")).GetComponent<UIGameSceneEventSender>();
      if (string.IsNullOrEmpty(eventName))
        Object.Destroy((Object) ((Component) component2).gameObject);
      else
        component2.eventName = eventName;
    }

    public void UpdateDeliveryTargetMarker(bool isExistDelivery)
    {
      Transform transform = this._transform.Find("SPR_DELIVERY_TARGET");
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      ((Component) transform).gameObject.SetActive(isExistDelivery);
    }
  }
}

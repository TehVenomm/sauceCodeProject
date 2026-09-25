// Decompiled with JetBrains decompiler
// Type: ExploreMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ExploreMap : GameSection
{
  private const float MarkerOffsetX = 22f;
  private const float MarkerOffsetY = 33f;
  private ExploreMap.OPEN_MAP_TYPE openType;
  private Transform[] playerMarkers_ = new Transform[4];
  private ExploreMapRoot mapRoot_;
  private Transform findIcon;
  private Transform battleIcon;
  private Transform sonarDirEffect;
  private Transform redCircle;
  private Transform sonarTexture;
  private bool calledExit;
  private Transform tapToSkip;
  private UIEventListener bgEventListener;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    if (GameSection.GetEventData() is ExploreMap.OPEN_MAP_TYPE eventData)
      this.openType = eventData;
    this.StartCoroutine(this.DoInitialize());
    if (this.openType != ExploreMap.OPEN_MAP_TYPE.SONAR)
      return;
    this.StartCoroutine(this.StartSonar());
  }

  private IEnumerator DoInitialize()
  {
    uint regionId = MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedExploreMapFrame = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMapFrame");
    LoadObject loadedExploreMap = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"));
    LoadObject loadedPlayerMarker = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarker");
    LoadObject loadedCircle = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreCircle");
    LoadObject loadedBattleIcon = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreBattleMarker");
    LoadObject loadedFootprint = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreTraceMarker");
    LoadObject loadedFindIcon = (LoadObject) null;
    LoadObject loadedDirSonar = (LoadObject) null;
    LoadObject loadedSonarTexture = (LoadObject) null;
    if (this.openType == ExploreMap.OPEN_MAP_TYPE.SONAR)
    {
      loadedFindIcon = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreExclametionMarker");
      loadedDirSonar = load_queue.Load(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_sonar_02");
      loadedSonarTexture = load_queue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreSonarTexture");
      this.CacheAudio(load_queue);
    }
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    Transform transform1 = ResourceUtility.Realizes(loadedExploreMapFrame.loadedObject, this._transform);
    Transform parent = ResourceUtility.Realizes(loadedExploreMap.loadedObject, transform1);
    ((Component) parent.Find("Map")).gameObject.SetActive(true);
    this.mapRoot_ = ((Component) parent).GetComponent<ExploreMapRoot>();
    ExploreMapLocation[] locations = this.mapRoot_.locations;
    for (int index1 = 0; index1 < locations.Length; ++index1)
    {
      Transform transform2 = ((Component) locations[index1]).transform.Find("ExploreSpotActive");
      Transform transform3 = ((Component) locations[index1]).transform.Find("ExploreSpotInactive");
      Transform transform4 = ((Component) locations[index1]).transform.Find("ExploreSpotSonar");
      ((Component) transform2).gameObject.SetActive(true);
      ((Component) transform3).gameObject.SetActive(false);
      List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint) locations[index1].mapId);
      if (pointListByMapId != null && Object.op_Inequality((Object) transform4, (Object) null))
      {
        for (int index2 = 0; index2 < pointListByMapId.Count; ++index2)
        {
          if (pointListByMapId[index2].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
          {
            ((Component) transform2).gameObject.SetActive(false);
            ((Component) transform3).gameObject.SetActive(false);
            ((Component) transform4).gameObject.SetActive(true);
          }
        }
      }
    }
    this.mapRoot_.UpdatePortals(false);
    ExploreMapFrame component1 = ((Component) transform1).GetComponent<ExploreMapFrame>();
    component1.SetMap(this.mapRoot_);
    RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionId);
    if (data != null)
      component1.SetCaption(data.regionName);
    for (int idx = 0; idx < this.playerMarkers_.Length; ++idx)
    {
      this.playerMarkers_[idx] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, transform1);
      ExplorePlayerMarker component2 = ((Component) this.playerMarkers_[idx]).GetComponent<ExplorePlayerMarker>();
      if (Object.op_Inequality((Object) null, (Object) component2))
        component2.SetIndex(idx);
      ((Component) component2).gameObject.SetActive(false);
    }
    this.mapRoot_.SetMarkers(this.playerMarkers_, false);
    ExploreStatus.TraceInfo[] bossTraceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
    if (bossTraceHistory != null && bossTraceHistory.Length != 0)
    {
      Transform transform5 = ResourceUtility.Realizes(loadedFootprint.loadedObject, parent);
      Vector3 positionOnMap1 = this.mapRoot_.GetPositionOnMap(bossTraceHistory[bossTraceHistory.Length - 1].mapId);
      transform5.localPosition = new Vector3(positionOnMap1.x + 22f, positionOnMap1.y + 33f, positionOnMap1.z);
      ((Component) transform5).gameObject.SetActive(true);
      if (bossTraceHistory.Length > 1)
      {
        Transform transform6 = ResourceUtility.Realizes(loadedFootprint.loadedObject, parent);
        Vector3 positionOnMap2 = this.mapRoot_.GetPositionOnMap(bossTraceHistory[bossTraceHistory.Length - 2].mapId);
        transform6.localPosition = new Vector3(positionOnMap2.x + 22f, positionOnMap2.y + 33f, positionOnMap2.z);
        ((Component) transform6).gameObject.SetActive(true);
      }
    }
    this.redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, parent);
    this.redCircle.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    this.battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, parent);
    if (this.mapRoot_.showBattleMarker && this.openType != ExploreMap.OPEN_MAP_TYPE.SONAR)
    {
      Vector3 positionOnMap = this.mapRoot_.GetPositionOnMap(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId());
      this.redCircle.localPosition = positionOnMap;
      this.battleIcon.localPosition = new Vector3(positionOnMap.x + 22f, positionOnMap.y + 33f, positionOnMap.z);
      TweenAlpha component3 = ((Component) this.redCircle).GetComponent<TweenAlpha>();
      if (Object.op_Inequality((Object) null, (Object) component3))
        component3.from = component3.to;
      ((Component) this.redCircle).gameObject.SetActive(true);
      ((Component) this.battleIcon).gameObject.SetActive(true);
    }
    else
    {
      ((Component) this.redCircle).gameObject.SetActive(false);
      ((Component) this.battleIcon).gameObject.SetActive(false);
    }
    if (this.openType == ExploreMap.OPEN_MAP_TYPE.SONAR)
    {
      this.tapToSkip = Utility.FindChild(transform1, "TaptoSkip");
      Transform child1 = Utility.FindChild(transform1, "BG");
      ((Component) child1).gameObject.SetActive(true);
      this.bgEventListener = UIEventListener.Get(((Component) child1).gameObject);
      Transform child2 = Utility.FindChild(transform1, "CaptionRoot/Close");
      if (Object.op_Inequality((Object) child2, (Object) null))
        ((Component) child2).gameObject.SetActive(false);
      this.findIcon = ResourceUtility.Realizes(loadedFindIcon.loadedObject, parent);
      ((Component) this.findIcon).gameObject.SetActive(false);
      this.sonarTexture = ResourceUtility.Realizes(loadedSonarTexture.loadedObject, parent);
      UIRenderTexture uiRenderTexture = UIRenderTexture.Get(((Component) this.sonarTexture).gameObject.GetComponentInChildren<UITexture>(), this.mapRoot_.GetSonarFov());
      uiRenderTexture.modelTransform.localPosition = new Vector3(0.0f, 0.0f, 150f);
      this.sonarDirEffect = ResourceUtility.Realizes(loadedDirSonar.loadedObject, uiRenderTexture.modelTransform, uiRenderTexture.renderLayer);
      this.sonarDirEffect.localScale = Vector2.op_Implicit(this.mapRoot_.GetSonarScale());
      ((Component) this.sonarDirEffect).gameObject.SetActive(false);
      uiRenderTexture.Enable();
      uiRenderTexture.renderCamera.backgroundColor = this.mapRoot_.sonarBackGroundColor;
      this.mapRoot_.SetDirectionSonar(((Component) this.sonarDirEffect).gameObject);
    }
    base.Initialize();
  }

  private IEnumerator StartSonar()
  {
    while (!this.isInitialized)
      yield return (object) null;
    int bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
    Vector3 bossPos = this.mapRoot_.GetPositionOnMap(bossMapId);
    int currentMapId = (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID;
    Vector3 selfPos = this.mapRoot_.GetPositionOnMap(currentMapId);
    float num = Vector3.Distance(bossPos, selfPos);
    float sonarSize = this.GetSonarSize(currentMapId);
    bool find = (double) num <= (double) sonarSize;
    ExploreMap.SONAR_DIR dir = this.CalculateSonarDir(bossPos, selfPos);
    yield return (object) new WaitForSeconds(0.7f);
    this.PlaySonarEffect(dir, selfPos, sonarSize);
    this.PlayAudio(ExploreMap.AUDIO.SONAR);
    yield return (object) new WaitForSeconds(2f);
    TweenAlpha tweenAlpha;
    if (this.mapRoot_.showBattleMarker)
    {
      ((Component) this.sonarDirEffect).gameObject.SetActive(false);
      MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
      Vector3 pos = this.mapRoot_.GetPositionOnMap(bossMapId);
      this.redCircle.localPosition = pos;
      ((Component) this.redCircle).gameObject.SetActive(true);
      tweenAlpha = ((Component) this.redCircle).GetComponent<TweenAlpha>();
      if (Object.op_Inequality((Object) null, (Object) tweenAlpha))
      {
        while (((Behaviour) tweenAlpha).isActiveAndEnabled)
          yield return (object) null;
      }
      yield return (object) new WaitForSeconds(0.4f);
      this.battleIcon.localPosition = new Vector3(pos.x + 22f, pos.y + 33f, pos.z);
      ((Component) this.battleIcon).gameObject.SetActive(true);
      this.PlayAudio(ExploreMap.AUDIO.MARKER);
      pos = new Vector3();
      tweenAlpha = (TweenAlpha) null;
    }
    else if (find && !((Component) this.redCircle).gameObject.activeSelf)
    {
      ((Component) this.sonarDirEffect).gameObject.SetActive(false);
      this.redCircle.localPosition = bossPos;
      ((Component) this.redCircle).gameObject.SetActive(true);
      tweenAlpha = ((Component) this.redCircle).GetComponent<TweenAlpha>();
      if (Object.op_Inequality((Object) null, (Object) tweenAlpha))
      {
        while (((Behaviour) tweenAlpha).isActiveAndEnabled)
          yield return (object) null;
      }
      yield return (object) new WaitForSeconds(0.4f);
      this.findIcon.localPosition = new Vector3(bossPos.x + 22f, bossPos.y + 33f, bossPos.z);
      ((Component) this.findIcon).gameObject.SetActive(true);
      this.PlayAudio(ExploreMap.AUDIO.MARKER);
      tweenAlpha = (TweenAlpha) null;
    }
    if (Object.op_Inequality((Object) this.tapToSkip, (Object) null))
    {
      ((Component) this.tapToSkip).gameObject.SetActive(true);
      this.bgEventListener.onClick += new UIEventListener.VoidDelegate(this.onClick);
    }
  }

  private float GetSonarSize(int mapId)
  {
    List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint) mapId);
    if (pointListByMapId == null)
      return 0.0f;
    float sonarSize = 0.0f;
    for (int index = 0; index < pointListByMapId.Count; ++index)
    {
      if (pointListByMapId[index].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
        sonarSize = pointListByMapId[index].value1;
    }
    return sonarSize;
  }

  private void PlaySonarEffect(ExploreMap.SONAR_DIR dir, Vector3 pos, float size)
  {
    this.sonarTexture.localPosition = pos;
    float num = size / 100f * this.mapRoot_.GetMapScale() * this.mapRoot_.GetSonarOffset();
    Vector3 localScale = this.sonarTexture.localScale;
    this.sonarTexture.localScale = new Vector3(localScale.x * num, localScale.y * num, localScale.z);
    ((Component) this.sonarDirEffect).gameObject.SetActive(true);
    this.RotateSonarEffect(dir);
  }

  private void RotateSonarEffect(ExploreMap.SONAR_DIR dir)
  {
    switch (dir)
    {
      case ExploreMap.SONAR_DIR.UP:
        this.sonarTexture.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
        break;
      case ExploreMap.SONAR_DIR.DOWN:
        this.sonarTexture.Rotate(new Vector3(0.0f, 0.0f, 180f));
        break;
      case ExploreMap.SONAR_DIR.LEFT:
        this.sonarTexture.Rotate(new Vector3(0.0f, 0.0f, 90f));
        break;
      case ExploreMap.SONAR_DIR.RIGHT:
        this.sonarTexture.Rotate(new Vector3(0.0f, 0.0f, -90f));
        break;
    }
  }

  private ExploreMap.SONAR_DIR CalculateSonarDir(Vector3 targetPos, Vector3 currentPos)
  {
    float num = targetPos.x - currentPos.x;
    return this.GetSonarDir(Mathf.Atan2(targetPos.y - currentPos.y, num) * 57.29578f);
  }

  private ExploreMap.SONAR_DIR GetSonarDir(float deg)
  {
    if ((double) deg >= 45.0 && (double) deg < 135.0)
      return ExploreMap.SONAR_DIR.UP;
    if ((double) deg >= -135.0 && (double) deg < -45.0)
      return ExploreMap.SONAR_DIR.DOWN;
    if ((double) deg >= -45.0 && (double) deg < 45.0)
      return ExploreMap.SONAR_DIR.RIGHT;
    return (double) deg >= 135.0 && (double) deg < 180.0 || (double) deg >= -180.0 && (double) deg < -135.0 ? ExploreMap.SONAR_DIR.LEFT : ExploreMap.SONAR_DIR.NONE;
  }

  private void PlayAudio(ExploreMap.AUDIO type) => SoundManager.PlayOneShotUISE((int) type);

  private void onClick(GameObject g) => this.OnQuery_EXIT();

  private void OnQuery_EXIT()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || this.calledExit)
      return;
    if (Object.op_Inequality((Object) null, (Object) this.bgEventListener))
      this.bgEventListener.onClick -= new UIEventListener.VoidDelegate(this.onClick);
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (ExploreMap), ((Component) this).gameObject, "[BACK]");
    this.calledExit = true;
  }

  public enum OPEN_MAP_TYPE
  {
    NORMAL,
    SONAR,
  }

  private enum SONAR_DIR
  {
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT,
  }

  private enum AUDIO
  {
    SONAR = 40000094, // 0x02625A5E
    MARKER = 40000125, // 0x02625A7D
  }
}

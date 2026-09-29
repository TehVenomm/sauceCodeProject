// Decompiled with JetBrains decompiler
// Type: ExploreMapOpenNewField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ExploreMapOpenNewField : GameSection
{
  private UIEventListener bgEventListener;
  private bool calledExit;
  private Transform mapFrame_;
  private Transform map_;
  private ExploreMapRoot mapRoot_;
  private Transform[] playerMarkers_ = new Transform[4];
  private Transform selfMarker_;
  private FieldMapTable.PortalTableData portalData_;
  private ExploreMapLocation from_;
  private ExploreMapLocation to_;
  private bool fromBoss_;
  private bool toBoss_;
  private Transform redCircle;
  private Transform battleIcon;
  private GameObject fieldQuestWarningRoot;
  private const float WARNING_TIME = 3f;
  private float MarkerOffsetX = 22f;
  private float MarkerOffsetY = 33f;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "FieldMapTable";
      yield return "RegionTable";
    }
  }

  public override void Initialize() => this.StartCoroutine("DoInitialize");

  private IEnumerator DoInitialize()
  {
    if (!(GameSection.GetEventData() is ExploreMapOpenNewField.EventData eventData))
    {
      base.Initialize();
    }
    else
    {
      uint regionId = eventData.regionId;
      this.fromBoss_ = eventData.fromBoss;
      this.toBoss_ = eventData.toBoss;
      this.portalData_ = Singleton<FieldMapTable>.I.GetPortalData(eventData.portalId);
      if (this.portalData_ == null)
      {
        base.Initialize();
      }
      else
      {
        LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
        LoadObject loadedExploreMapFrame = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMapFrame");
        LoadObject loadedExploreMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"));
        LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarker");
        LoadObject loadedCircle = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreCircle");
        LoadObject loadedBattleIcon = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreBattleMarker");
        LoadObject loadedFootprint = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreTraceMarker");
        LoadObject loadedEncounterBossCutIn = (LoadObject) null;
        if (this.toBoss_)
          loadedEncounterBossCutIn = loadQueue.Load(RESOURCE_CATEGORY.UI, "InGameFieldQuestWarning");
        if (loadQueue.IsLoading())
          yield return (object) loadQueue.Wait();
        if (Object.op_Equality((Object) null, loadedExploreMap.loadedObject))
        {
          base.Initialize();
        }
        else
        {
          if (loadedEncounterBossCutIn != null)
          {
            this.fieldQuestWarningRoot = ((Component) ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject)).gameObject;
            UIPanel componentInChildren = this.fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
              componentInChildren.depth = 12000;
            if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
              MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(loadQueue);
          }
          this.mapFrame_ = ResourceUtility.Realizes(loadedExploreMapFrame.loadedObject, this._transform);
          if (this.toBoss_)
          {
            UIPanel component = ((Component) this.mapFrame_).GetComponent<UIPanel>();
            component.renderQueue = UIPanel.RenderQueue.StartAt;
            component.startingRenderQueue = 2900;
          }
          this.map_ = ResourceUtility.Realizes(loadedExploreMap.loadedObject, this.mapFrame_);
          ((Component) this.map_.Find("Map")).gameObject.SetActive(true);
          this.mapRoot_ = ((Component) this.map_).GetComponent<ExploreMapRoot>();
          ExploreMapLocation[] locations = this.mapRoot_.locations;
          for (int index1 = 0; index1 < locations.Length; ++index1)
          {
            Transform transform1 = ((Component) locations[index1]).transform.Find("ExploreSpotActive");
            Transform transform2 = ((Component) locations[index1]).transform.Find("ExploreSpotInactive");
            Transform transform3 = ((Component) locations[index1]).transform.Find("ExploreSpotSonar");
            ((Component) transform1).gameObject.SetActive(true);
            ((Component) transform2).gameObject.SetActive(false);
            List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint) locations[index1].mapId);
            if (pointListByMapId != null && Object.op_Inequality((Object) transform3, (Object) null))
            {
              for (int index2 = 0; index2 < pointListByMapId.Count; ++index2)
              {
                if (pointListByMapId[index2].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
                {
                  ((Component) transform1).gameObject.SetActive(false);
                  ((Component) transform2).gameObject.SetActive(false);
                  ((Component) transform3).gameObject.SetActive(true);
                }
              }
            }
          }
          this.mapRoot_.UpdatePortals(false);
          this.from_ = this.mapRoot_.FindLocation((int) this.portalData_.srcMapID);
          this.to_ = this.mapRoot_.FindLocation((int) this.portalData_.dstMapID);
          if (Object.op_Equality((Object) null, (Object) this.to_))
          {
            base.Initialize();
          }
          else
          {
            ExploreMapFrame component1 = ((Component) this.mapFrame_).GetComponent<ExploreMapFrame>();
            component1.SetMap(this.mapRoot_);
            RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionId);
            if (data != null)
              component1.SetCaption(data.regionName);
            for (int idx = 0; idx < this.playerMarkers_.Length; ++idx)
            {
              this.playerMarkers_[idx] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, this.mapFrame_);
              ExplorePlayerMarker component2 = ((Component) this.playerMarkers_[idx]).GetComponent<ExplorePlayerMarker>();
              if (Object.op_Inequality((Object) null, (Object) component2))
                component2.SetIndex(idx);
              ((Component) component2).gameObject.SetActive(false);
            }
            this.selfMarker_ = this.playerMarkers_[0];
            Transform transform4 = this.mapFrame_.Find("BG");
            ((Component) transform4).gameObject.SetActive(true);
            this.bgEventListener = UIEventListener.Get(((Component) transform4).gameObject);
            ((Component) this.mapFrame_.Find("TaptoSkip")).gameObject.SetActive(!this.toBoss_);
            ((Component) this.mapFrame_.Find("CaptionRoot/Close")).gameObject.SetActive(false);
            this.mapRoot_.SetMarkers(this.playerMarkers_, false);
            ExploreStatus.TraceInfo[] bossTraceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
            if (bossTraceHistory != null && bossTraceHistory.Length != 0)
            {
              Transform transform5 = ResourceUtility.Realizes(loadedFootprint.loadedObject, this.map_);
              Vector3 positionOnMap1 = this.mapRoot_.GetPositionOnMap(bossTraceHistory[bossTraceHistory.Length - 1].mapId);
              transform5.localPosition = new Vector3(positionOnMap1.x + this.MarkerOffsetX, positionOnMap1.y + this.MarkerOffsetY, positionOnMap1.z);
              ((Component) transform5).gameObject.SetActive(true);
              if (bossTraceHistory.Length > 1)
              {
                Transform transform6 = ResourceUtility.Realizes(loadedFootprint.loadedObject, this.map_);
                Vector3 positionOnMap2 = this.mapRoot_.GetPositionOnMap(bossTraceHistory[bossTraceHistory.Length - 2].mapId);
                transform6.localPosition = new Vector3(positionOnMap2.x + this.MarkerOffsetX, positionOnMap2.y + this.MarkerOffsetY, positionOnMap2.z);
                ((Component) transform6).gameObject.SetActive(true);
              }
            }
            this.redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, this.map_);
            this.redCircle.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            this.battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, this.map_);
            if (this.mapRoot_.showBattleMarker)
            {
              Vector3 positionOnMap = this.mapRoot_.GetPositionOnMap(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId());
              this.redCircle.localPosition = positionOnMap;
              this.battleIcon.localPosition = new Vector3(positionOnMap.x + this.MarkerOffsetX, positionOnMap.y + this.MarkerOffsetY, positionOnMap.z);
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
            base.Initialize();
          }
        }
      }
    }
  }

  private void OnQuery_EXIT()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || this.calledExit)
      return;
    this.StopAllCoroutines();
    if (Object.op_Inequality((Object) null, (Object) this.bgEventListener))
      this.bgEventListener.onClick -= new UIEventListener.VoidDelegate(this.onClick);
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (ExploreMapOpenNewField), ((Component) this).gameObject, "INGAME_MAIN");
    this.calledExit = true;
  }

  private void onClick(GameObject g)
  {
    if (this.toBoss_)
      return;
    this.OnQuery_EXIT();
  }

  protected override void OnOpen()
  {
    if (Object.op_Equality((Object) null, (Object) this.bgEventListener))
    {
      this.DispatchEvent("EXIT");
    }
    else
    {
      this.bgEventListener.onClick += new UIEventListener.VoidDelegate(this.onClick);
      this.StartCoroutine(this.DoExitEvent());
    }
  }

  private IEnumerator DoExitEvent()
  {
    ((Component) this.selfMarker_).gameObject.SetActive(true);
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(0.0f, 0.0f, 0.0f);
    Transform parent = this.mapRoot_.FindPortalNode(this.from_.mapId, this.to_.mapId);
    if (Object.op_Equality((Object) null, (Object) parent))
    {
      ExploreMapLocation location1 = this.mapRoot_.FindLocation(this.from_.mapId);
      ExploreMapLocation location2 = this.mapRoot_.FindLocation(this.to_.mapId);
      if (Object.op_Inequality((Object) null, (Object) location1) && Object.op_Inequality((Object) null, (Object) location2))
      {
        vector3 = Vector3.op_Subtraction(Vector3.op_Multiply(Vector3.op_Addition(((Component) location1).transform.localPosition, ((Component) location2).transform.localPosition), 0.5f), ((Component) location1).transform.localPosition);
        parent = ((Component) location1).transform;
      }
    }
    if (Object.op_Equality((Object) null, (Object) parent))
    {
      yield return (object) new WaitForSeconds(0.1f);
      this.DispatchEvent("EXIT");
    }
    else
    {
      if (Object.op_Inequality((Object) null, (Object) this.from_))
      {
        if (this.fromBoss_ && Object.op_Inequality((Object) null, (Object) parent))
        {
          Utility.Attach(parent, this.selfMarker_);
          ((Component) this.selfMarker_).GetComponent<ExplorePlayerMarker>().SetIndex(0);
          this.selfMarker_.localPosition = Vector3.op_Addition(this.selfMarker_.localPosition, vector3);
        }
        else
        {
          Utility.Attach(((Component) this.from_).transform, this.selfMarker_);
          ((Component) this.selfMarker_).GetComponent<ExplorePlayerMarker>().SetIndex(0);
        }
        yield return (object) new WaitForSeconds(0.3f);
        TweenScale.Begin(((Component) this.selfMarker_).gameObject, 0.3f, Vector3.zero);
        yield return (object) new WaitForSeconds(0.3f);
      }
      else
      {
        this.selfMarker_.localScale = Vector3.zero;
        yield return (object) new WaitForSeconds(0.3f);
      }
      yield return (object) new WaitForSeconds(0.3f);
      Utility.Attach(((Component) this.to_).transform, this.selfMarker_);
      ((Component) this.selfMarker_).GetComponent<ExplorePlayerMarker>().SetIndex(0);
      TweenScale.Begin(((Component) this.selfMarker_).gameObject, 0.3f, Vector3.one);
      yield return (object) new WaitForSeconds(0.5f);
      if (this.toBoss_)
      {
        if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
        {
          MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.isRare);
          MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(3f, 0.3f, (System.Action) (() =>
          {
            if (!Object.op_Inequality((Object) this.fieldQuestWarningRoot, (Object) null))
              return;
            Object.Destroy((Object) this.fieldQuestWarningRoot);
          }));
        }
        this.StartCoroutine(this.DoExitEncounterBossEvent());
      }
      else
        this.DispatchEvent("EXIT");
    }
  }

  private IEnumerator DoExitEncounterBossEvent()
  {
    yield return (object) new WaitForSeconds(3f);
    this.DispatchEvent("EXIT");
  }

  public class EventData
  {
    public uint regionId;
    public uint portalId;
    public bool fromBoss;
    public bool toBoss;
  }
}

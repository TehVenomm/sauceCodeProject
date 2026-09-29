// Decompiled with JetBrains decompiler
// Type: PortalObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class PortalObject : MonoBehaviour
{
  protected Transform viewObject;
  protected Animator viewAnimator;
  protected ParticleSystem[] viewParticles;
  private readonly uint NOT_UNLOCKED_TIME = 7002;

  public InGameSettingsManager.Portal parameter { get; protected set; }

  public Transform _transform { get; private set; }

  public FieldMapPortalInfo portalInfo { get; protected set; }

  public FieldMapTable.PortalTableData portalData { get; protected set; }

  public uint portalID { get; protected set; }

  public PortalObject.VIEW_TYPE viewType { get; protected set; }

  public bool isFull { get; protected set; }

  public bool isQuest { get; protected set; }

  public bool isLock { get; protected set; }

  public bool isClearOrder { get; protected set; }

  public bool isToHardMap { get; protected set; }

  public bool isUnlockedTime { get; protected set; }

  public int nowPoint { get; protected set; }

  public int maxPoint { get; protected set; }

  public UIPortalStatusGizmo uiGizmo { get; set; }

  public static PortalObject Create(FieldMapPortalInfo portal_info, Transform parent)
  {
    if (portal_info == null)
      return (PortalObject) null;
    if (portal_info.portalData == null)
      return (PortalObject) null;
    Transform gameObject = Utility.CreateGameObject(nameof (PortalObject), parent, 19);
    gameObject.position = new Vector3(portal_info.portalData.srcX, 0.0f, portal_info.portalData.srcZ);
    PortalObject portalObject = ((Component) gameObject).gameObject.AddComponent<PortalObject>();
    if (Object.op_Equality((Object) portalObject, (Object) null))
      return (PortalObject) null;
    portalObject.Initialize(portal_info);
    return portalObject;
  }

  protected virtual void Awake()
  {
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.portal;
    this._transform = ((Component) this).transform;
    SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    sphereCollider.radius = 1f;
    ((Collider) sphereCollider).isTrigger = true;
    if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
    if (!MonoBehaviourSingleton<MiniMap>.IsValid())
      return;
    MonoBehaviourSingleton<MiniMap>.I.Attach((MonoBehaviour) this);
  }

  public void Initialize(FieldMapPortalInfo portal_info)
  {
    this.portalInfo = portal_info;
    this.portalData = this.portalInfo.portalData;
    this.portalID = this.portalData.portalID;
    this.isClearOrder = FieldManager.IsOpenPortalClearOrder(this.portalData) || FieldManager.IsOpenPortal(this.portalData);
    if (GameSaveData.instance.isNewReleasePortal(this.portalID) && FieldManager.IsOpenPortal(this.portalData))
      GameSaveData.instance.newReleasePortals.Remove(this.portalID);
    this.isUnlockedTime = portal_info.portalData.isUnlockedTime();
    this.isToHardMap = FieldManager.IsToHardPortal(this.portalData);
    this.nowPoint = portal_info.GetNowPortalPoint();
    this.maxPoint = (int) portal_info.GetMaxPortalPoint();
    this.isFull = portal_info.IsFull();
    this.viewType = PortalObject.VIEW_TYPE.NORMAL;
    if (!this.isClearOrder || !this.isUnlockedTime)
      this.viewType = PortalObject.VIEW_TYPE.NOT_CLEAR_ORDER;
    else if (this.portalData.dstMapID == 0U)
      this.viewType = PortalObject.VIEW_TYPE.TO_HOME;
    else if (!MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal(this.portalData))
      this.viewType = PortalObject.VIEW_TYPE.NOT_TRAVELED;
    else if (this.isToHardMap)
      this.viewType = PortalObject.VIEW_TYPE.TO_HARD_MAP;
    if (this.portalData.dstQuestID != 0U)
    {
      if (this.portalData.dstMapID != 0U)
      {
        int num = 0;
        ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) this.portalData.dstQuestID));
        if (clearStatusQuest != null)
          num = clearStatusQuest.questStatus;
        if (num != 3 && num != 4)
        {
          this.isLock = true;
          this.isQuest = true;
        }
      }
      else
        this.isQuest = true;
    }
    this.CreateView();
    if (!MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
      return;
    MonoBehaviourSingleton<DropTargetMarkerManeger>.I.CheckTarget(this);
  }

  private void CreateView()
  {
    if (Object.op_Inequality((Object) this.viewObject, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.viewObject).gameObject);
      this.viewObject = (Transform) null;
    }
    if (this.viewType == PortalObject.VIEW_TYPE.NOT_TRAVELED && !this.isFull)
    {
      if (MonoBehaviourSingleton<InGameLinkResourcesField>.IsValid())
        this.viewObject = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesField>.I.portalIncomplete, this._transform);
      if (Object.op_Inequality((Object) this.viewObject, (Object) null))
      {
        this.viewAnimator = ((Component) this.viewObject).GetComponent<Animator>();
        if (Object.op_Inequality((Object) this.viewAnimator, (Object) null))
          this.viewAnimator.speed = 0.0f;
        this.viewParticles = ((Component) this.viewObject).GetComponentsInChildren<ParticleSystem>();
      }
    }
    else
      this.viewObject = EffectManager.GetEffect(this.parameter.effectNames[(int) this.viewType], this._transform);
    this.UpdateView();
  }

  public void SetAndCreateView(PortalObject.VIEW_TYPE type)
  {
    this.viewType = type;
    this.CreateView();
  }

  public void UpdateView()
  {
    if (this.viewType != PortalObject.VIEW_TYPE.NOT_TRAVELED || this.isFull)
      return;
    float num = (float) this.nowPoint / (float) this.maxPoint;
    if ((double) num > 1.0)
      num = 1f;
    if (!this.isClearOrder)
      num = 0.0f;
    if (Object.op_Inequality((Object) this.viewAnimator, (Object) null))
    {
      this.viewAnimator.speed = 1f;
      this.viewAnimator.Play("ef_btl_warp_unuse_01", 0, num);
      this.viewAnimator.Update(0.0f);
      this.viewAnimator.speed = 0.0f;
    }
    if (this.viewParticles == null)
      return;
    bool flag = false;
    FieldMapPortalInfo pointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
    if (this.isClearOrder && (pointToPortalInfo == this.portalInfo || this.portalInfo.IsFull()))
      flag = true;
    int index = 0;
    for (int length = this.viewParticles.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.viewParticles[index], (Object) null))
      {
        if (flag)
        {
          if (this.viewParticles[index].isStopped)
            this.viewParticles[index].Play();
        }
        else if (!this.viewParticles[index].isStopped)
          this.viewParticles[index].Stop();
      }
    }
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (Object.op_Equality((Object) ((Component) collider).gameObject.GetComponent<Self>(), (Object) null) || !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null) || MonoBehaviourSingleton<StageObjectManager>.I.self.isDead)
      return;
    if (!this.isClearOrder || !this.isUnlockedTime)
    {
      string str = this.portalData.notAppearText;
      if (this.portalData.appearQuestId > 0U && string.IsNullOrEmpty(str) && !MonoBehaviourSingleton<QuestManager>.I.IsClearQuest(this.portalData.appearQuestId))
        str = StringTable.Format(STRING_CATEGORY.IN_GAME, 7000U, (object) Singleton<QuestTable>.I.GetQuestData(this.portalData.appearQuestId).questText);
      if (this.portalData.appearDeliveryId > 0U && string.IsNullOrEmpty(str) && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(this.portalData.appearDeliveryId))
        str = StringTable.Format(STRING_CATEGORY.IN_GAME, 7001U, (object) Singleton<DeliveryTable>.I.GetDeliveryTableData(this.portalData.appearDeliveryId).name);
      if (this.portalData.travelMapId > 0U && string.IsNullOrEmpty(str))
        MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) this.portalData.travelMapId);
      if (!this.isUnlockedTime)
        str = StringTable.Get(STRING_CATEGORY.IN_GAME, this.NOT_UNLOCKED_TIME);
      if (string.IsNullOrEmpty(str) || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_NOT_APPEAR", (object) new object[1]
      {
        (object) str
      });
    }
    else if (!this.isFull)
    {
      if (MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo() == this.portalInfo || this.portalInfo.IsFull())
      {
        if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          return;
        GameSceneManager i = MonoBehaviourSingleton<GameSceneManager>.I;
        GameObject gameObject = ((Component) this).gameObject;
        object[] user_data = new object[2];
        int num = this.nowPoint;
        user_data[0] = (object) num.ToString();
        num = this.maxPoint;
        user_data[1] = (object) num.ToString();
        i.ExecuteSceneEvent("PortalObject.OnTriggerEnter", gameObject, "PORTAL_NOT_FULL", (object) user_data);
      }
      else
      {
        if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          return;
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_NOT_ACTIVE");
      }
    }
    else
    {
      MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject = this;
      if (this.isQuest)
      {
        if (this.isLock)
        {
          if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || !MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(this.portalData, true))
            return;
          MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_QUEST_LOCK");
        }
        else
        {
          if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(this.portalData.dstQuestID))
            return;
          MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_QUEST");
        }
      }
      else if (this.viewType == PortalObject.VIEW_TYPE.TO_HOME)
      {
        if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          return;
        if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
          MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_LOUNGE");
        else
          MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_HOME");
      }
      else
      {
        if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          return;
        int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
        if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(this.portalData, false))
          return;
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", ((Component) this).gameObject, "PORTAL_NEXT");
      }
    }
  }

  public void OnGetPortalPoint(int add_point)
  {
    EffectManager.OneShot(this.parameter.pointGetEffectName, this._transform.position, this._transform.rotation);
    this.nowPoint += add_point;
    if (this.nowPoint >= this.maxPoint)
    {
      if (!this.portalInfo.IsFull())
        Log.Warning(LOG.INGAME, "PortalObject.OnGetPortalPoint() Portal is not full. id : {0}", (object) this.portalID);
      this.isFull = true;
      this.CreateView();
      string str = "";
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.portalData.dstMapID);
      if (fieldMapData != null)
        str = fieldMapData.mapName;
      if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6001U, (object) str), false, 1.4f);
      else if (QuestManager.IsValidInGameExplore())
      {
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6002U, (object) str), false, 1.4f);
      }
      else
      {
        int num = this.parameter.clearCrystalNum;
        if (this.isToHardMap)
          num = this.parameter.clearHardCrystalNum;
        UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6000U, (object) str, (object) num), false);
      }
      SoundManager.PlayOneShotUISE(40000069);
      SoundManager.PlayOneshotJingle(40000071);
    }
    else
    {
      this.UpdateView();
      SoundManager.PlayOneShotUISE(40000068);
    }
    if (!Object.op_Inequality((Object) this.uiGizmo, (Object) null))
      return;
    this.uiGizmo.OnGetPortalPoint();
  }

  public enum VIEW_TYPE
  {
    NONE = -1, // 0xFFFFFFFF
    NORMAL = 0,
    NOT_TRAVELED = 1,
    TO_HOME = 2,
    TO_HARD_MAP = 3,
    NOT_CLEAR_ORDER = 4,
  }
}

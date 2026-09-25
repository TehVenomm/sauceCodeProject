// Decompiled with JetBrains decompiler
// Type: LoungePeople
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoungePeople : MonoBehaviour, IHomePeople, ILoungePeople
{
  private ILoungePeople self;
  private const int charaMax = 14;
  private const float GROUP_RADIUS = 1f;
  protected Transform peopleRoot;
  protected WayPoint centerPoint;
  private Vector3 groupCenterPos;
  private List<WayPoint> leafPoints = new List<WayPoint>();
  protected OutGameCharacterCreater creater;

  public bool isInitialized { get; protected set; }

  public bool isPeopleInitialized { get; protected set; }

  public HomeSelfCharacter selfChara { get; protected set; }

  public List<HomeCharacterBase> charas { get; protected set; }

  public List<LoungePlayer> loungePlayers { get; protected set; }

  public ILoungePeople CastToLoungePeople()
  {
    if (this.self == null)
      this.self = (ILoungePeople) this;
    return this.self;
  }

  public void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback)
  {
    if (Object.op_Inequality((Object) this.selfChara, (Object) null))
      return;
    this.selfChara = this.creater.CreateSelf((IHomePeople) this, this.peopleRoot, notice_callback) as HomeSelfCharacter;
    this.charas.Add((HomeCharacterBase) this.selfChara);
  }

  public virtual bool CreateLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry)
  {
    if (slotInfo == null || slotInfo.userInfo == null)
      return false;
    CharaInfo userInfo = slotInfo.userInfo;
    if (Object.op_Inequality((Object) this.GetLoungePlayer(userInfo.userId), (Object) null))
      return false;
    LoungePlayer loungePlayer = this.creater.CreateLoungePlayer<LoungePlayer>((IHomePeople) this, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene, this.peopleRoot, userInfo, useMovingEntry);
    this.charas.Add((HomeCharacterBase) loungePlayer);
    this.loungePlayers.Add(loungePlayer);
    return true;
  }

  public bool ChangeEquipLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry)
  {
    if (slotInfo == null || slotInfo.userInfo == null)
      return false;
    CharaInfo userInfo = slotInfo.userInfo;
    LoungePlayer loungePlayer = this.GetLoungePlayer(userInfo.userId);
    if (Object.op_Equality((Object) loungePlayer, (Object) null))
      return false;
    this.CheckEquipChanged(loungePlayer.LoungeCharaInfo, userInfo, useMovingEntry);
    return true;
  }

  public void UpdateLoungePlayersInfo(PartyModel.SlotInfo slotInfo)
  {
    for (int index = 0; index < this.loungePlayers.Count; ++index)
    {
      if (this.loungePlayers[index].LoungeCharaInfo.userId == slotInfo.userInfo.userId)
        this.loungePlayers[index].SetLoungeCharaInfo(slotInfo.userInfo);
    }
  }

  public bool DestroyLoungePlayer(int id)
  {
    LoungePlayer loungePlayer = this.GetLoungePlayer(id);
    if (Object.op_Equality((Object) loungePlayer, (Object) null) || Object.op_Equality((Object) ((Component) loungePlayer).gameObject, (Object) null))
      return false;
    this.OnDestroyHomeCharacter((HomeCharacterBase) loungePlayer);
    this.OnDestroyLoungePlayer(loungePlayer);
    Object.Destroy((Object) ((Component) loungePlayer).gameObject);
    return true;
  }

  public void SetInitialPositionLoungePlayer(int id, Vector3 initialPos, LOUNGE_ACTION_TYPE type)
  {
    this.StartCoroutine(this.DoSetInitialPositionLoungePlayer(id, initialPos, type));
  }

  public void MoveLoungePlayer(int id, Vector3 targetPos)
  {
    LoungePlayer loungePlayer = this.GetLoungePlayer(id);
    if (Object.op_Equality((Object) loungePlayer, (Object) null) || Object.op_Equality((Object) ((Component) loungePlayer).gameObject, (Object) null))
      return;
    loungePlayer.SetMoveTargetPosition(targetPos);
  }

  public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
  {
    return !(chara is LoungeMoveNPC) ? wayPoint.GetPosInCollider() : wayPoint.GetPosInCollider().ToVector2XZ().ToVector3XZ();
  }

  public HomeNPCCharacter GetHomeNPCCharacter(int npcID)
  {
    if (this.charas == null)
      return (HomeNPCCharacter) null;
    HomeNPCCharacter homeNpcCharacter = (HomeNPCCharacter) null;
    for (int index = 0; index < this.charas.Count; ++index)
    {
      HomeNPCCharacter chara = this.charas[index] as HomeNPCCharacter;
      if (!Object.op_Equality((Object) chara, (Object) null) && npcID == chara.npcInfo.npcID)
      {
        homeNpcCharacter = chara;
        break;
      }
    }
    return homeNpcCharacter;
  }

  public void OnDestroyHomeCharacter(HomeCharacterBase chara) => this.charas.Remove(chara);

  public void OnDestroyLoungePlayer(LoungePlayer chara) => this.loungePlayers.Remove(chara);

  protected virtual IEnumerator Start()
  {
    this.creater = new OutGameCharacterCreater();
    this.charas = new List<HomeCharacterBase>(16 /*0x10*/);
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      this.loungePlayers = new List<LoungePlayer>(8);
    this.peopleRoot = Utility.CreateGameObject("PeopleRoot", ((Component) this).transform);
    yield return (object) this.StartCoroutine(this.LocateLoungePeople());
    this.isInitialized = true;
    this.isPeopleInitialized = true;
  }

  private IEnumerator GetHomePlayerCharacterList()
  {
    bool wait = true;
    MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
  }

  private IEnumerator DoSetInitialPositionLoungePlayer(
    int id,
    Vector3 pos,
    LOUNGE_ACTION_TYPE type)
  {
    LoungePlayer target = this.GetLoungePlayer(id);
    while (Object.op_Equality((Object) target, (Object) null) || Object.op_Equality((Object) ((Component) target).gameObject, (Object) null))
    {
      target = this.GetLoungePlayer(id);
      yield return (object) null;
    }
    while (target.isLoading)
      yield return (object) null;
    target.SetInitialPosition(pos, type);
  }

  private IEnumerator LoadPeopleWayPoint()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    string str = "PeopleWayPoints";
    LoadObject lo_way_points = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
    {
      str
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Utility.ForEach(ResourceUtility.Realizes(lo_way_points.loadedObject, ((Component) this).transform), (Predicate<Transform>) (o =>
    {
      if (((Object) o).name.StartsWith("LEAF"))
        this.leafPoints.Add(((Component) o).GetComponent<WayPoint>());
      else if (((Object) o).name == "CENTER")
        this.centerPoint = ((Component) o).GetComponent<WayPoint>();
      return false;
    }));
  }

  protected IEnumerator LoadLoungeWayPoint(string wayPointName)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadWayPoints = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
    {
      wayPointName
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Utility.ForEach(ResourceUtility.Realizes(loadWayPoints.loadedObject, ((Component) this).transform), (Predicate<Transform>) (o =>
    {
      if (((Object) o).name == "CENTER")
        this.centerPoint = ((Component) o).GetComponent<WayPoint>();
      return false;
    }));
  }

  protected virtual IEnumerator LocateLoungePeople()
  {
    OutGameSettingsManager.HomeScene.NPC[] npcArray = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.npcs;
    for (int index = 0; index < npcArray.Length; ++index)
    {
      OutGameSettingsManager.HomeScene.NPC npc = npcArray[index];
      HomeCharacterBase homeCharacterBase;
      if (string.IsNullOrEmpty(npc.wayPointName))
      {
        homeCharacterBase = this.creater.CreateNPC((IHomePeople) this, this.peopleRoot, npc);
      }
      else
      {
        yield return (object) this.StartCoroutine(this.LoadLoungeWayPoint(npc.wayPointName));
        homeCharacterBase = this.creater.CreateLoungeMoveNPC((IHomePeople) this, this.peopleRoot, this.centerPoint, npc);
      }
      if (Object.op_Inequality((Object) homeCharacterBase, (Object) null))
        this.charas.Add(homeCharacterBase);
      npc = (OutGameSettingsManager.HomeScene.NPC) null;
    }
    npcArray = (OutGameSettingsManager.HomeScene.NPC[]) null;
    while (this.IsLoadingCharacter())
      yield return (object) null;
  }

  public LoungePlayer GetLoungePlayer(int id)
  {
    for (int index = 0; index < this.loungePlayers.Count; ++index)
    {
      if (this.loungePlayers[index].LoungeCharaInfo.userId == id)
        return this.loungePlayers[index];
    }
    return (LoungePlayer) null;
  }

  protected void CheckEquipChanged(
    CharaInfo beforeCharaInfo,
    CharaInfo currentCharaInfo,
    bool useMovingEntry)
  {
    if (!this.CheckEquipDiff(beforeCharaInfo, currentCharaInfo))
      return;
    this.DestroyLoungePlayer(beforeCharaInfo.userId);
    LoungePlayer loungePlayer = this.creater.CreateLoungePlayer((IHomePeople) this, this.peopleRoot, currentCharaInfo, useMovingEntry);
    this.charas.Add((HomeCharacterBase) loungePlayer);
    this.loungePlayers.Add(loungePlayer);
  }

  private bool CheckEquipDiff(CharaInfo beforeCharaInfo, CharaInfo currentCharaInfo)
  {
    if (beforeCharaInfo.showHelm != currentCharaInfo.showHelm)
      return true;
    List<CharaInfo.EquipItem> equipSet1 = beforeCharaInfo.equipSet;
    List<CharaInfo.EquipItem> equipSet2 = currentCharaInfo.equipSet;
    if (equipSet1.Count != equipSet2.Count)
      return true;
    List<int> intList1 = new List<int>(7);
    List<int> intList2 = new List<int>(7);
    for (int index = 0; index < equipSet1.Count; ++index)
    {
      intList1.Add(equipSet1[index].eId);
      intList2.Add(equipSet2[index].eId);
    }
    intList1.RemoveAll(new Predicate<int>(intList2.Contains));
    return intList1.Count > 0 || !beforeCharaInfo.isEqualAccessory(currentCharaInfo.accessory);
  }

  protected bool IsLoadingCharacter()
  {
    return Object.op_Inequality((Object) this.charas.Find((Predicate<HomeCharacterBase>) (o => o.isLoading)), (Object) null);
  }

  private void LateUpdate()
  {
    int index1 = 0;
    for (int count = this.charas.Count; index1 < count; ++index1)
    {
      HomeCharacterBase chara1 = this.charas[index1];
      if (!chara1.isLoading && ((Behaviour) chara1).isActiveAndEnabled)
      {
        Vector2 vector2Xz1 = chara1._transform.localPosition.ToVector2XZ();
        HomeCharacterBase homeCharacterBase = (HomeCharacterBase) null;
        float num1 = 9f;
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(0.0f, 0.0f);
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector(0.0f, 0.0f);
        for (int index2 = index1 + 1; index2 < count; ++index2)
        {
          HomeCharacterBase chara2 = this.charas[index2];
          if (!chara2.isLoading && ((Behaviour) chara2).isActiveAndEnabled)
          {
            Vector2 vector2Xz2 = chara2._transform.localPosition.ToVector2XZ();
            Vector2 vector2_3 = Vector2.op_Subtraction(vector2Xz1, vector2Xz2);
            float sqrMagnitude = ((Vector2) ref vector2_3).sqrMagnitude;
            if ((double) sqrMagnitude > 0.0 && (double) sqrMagnitude < (double) num1)
            {
              homeCharacterBase = chara2;
              vector2_2 = vector2Xz2;
              vector2_1 = vector2_3;
              num1 = sqrMagnitude;
            }
          }
        }
        if (Object.op_Inequality((Object) homeCharacterBase, (Object) null))
        {
          float num2 = Mathf.Sqrt(num1);
          Vector2 vector2_4 = Vector2.op_Division(vector2_1, num2);
          float num3 = (float) ((1.0 - (double) num2 / 3.0) * (double) Time.deltaTime * 0.89999997615814209);
          if ((double) num3 > 0.5)
            num3 = 0.5f;
          double num4 = (double) num3;
          Vector2 vector2_5 = Vector2.op_Multiply(vector2_4, (float) num4);
          if (!chara1.isStop)
            chara1._transform.localPosition = Vector2.op_Addition(vector2Xz1, vector2_5).ToVector3XZ();
          if (!homeCharacterBase.isStop)
            homeCharacterBase._transform.localPosition = Vector2.op_Subtraction(vector2_2, vector2_5).ToVector3XZ();
        }
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: HomePeople
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HomePeople : MonoBehaviour, IHomePeople
{
  private const int charaMax = 14;
  private const float GROUP_RADIUS = 1f;
  private Transform peopleRoot;
  private WayPoint centerPoint;
  private Vector3 groupCenterPos;
  private List<WayPoint> leafPoints = new List<WayPoint>();
  private OutGameCharacterCreater creater;

  public bool isInitialized { get; private set; }

  public bool isPeopleInitialized { get; private set; }

  public HomeSelfCharacter selfChara { get; private set; }

  public List<HomeCharacterBase> charas { get; private set; }

  public ILoungePeople CastToLoungePeople() => (ILoungePeople) null;

  public void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback)
  {
    if (Object.op_Inequality((Object) this.selfChara, (Object) null))
      return;
    this.selfChara = this.creater.CreateSelf((IHomePeople) this, this.peopleRoot, notice_callback) as HomeSelfCharacter;
    this.charas.Add((HomeCharacterBase) this.selfChara);
  }

  public Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint)
  {
    float num1 = 1.5f;
    float num2 = num1 * num1;
    bool flag = (double) this.groupCenterPos.y != -1.0;
    Vector2 vector2Xz1 = this.groupCenterPos.ToVector2XZ();
    for (int index1 = 0; index1 < 8; ++index1)
    {
      Vector2 vector2Xz2 = wayPoint.GetPosInCollider().ToVector2XZ();
      Vector2 vector2;
      if (flag)
      {
        vector2 = Vector2.op_Subtraction(vector2Xz2, vector2Xz1);
        if ((double) ((Vector2) ref vector2).sqrMagnitude < (double) num2)
          continue;
      }
      int index2 = 0;
      int count;
      for (count = this.charas.Count; index2 < count; ++index2)
      {
        HomeCharacterBase chara1 = this.charas[index2];
        if (Object.op_Inequality((Object) chara1, (Object) chara) && !chara1.isLoading && ((Behaviour) chara1).isActiveAndEnabled)
        {
          vector2 = Vector2.op_Subtraction(vector2Xz2, chara1.moveTargetPos.ToVector2XZ());
          if ((double) ((Vector2) ref vector2).sqrMagnitude < 0.25)
            break;
        }
      }
      if (index2 == count)
        return vector2Xz2.ToVector3XZ();
    }
    return wayPoint.GetPosInCollider();
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

  private IEnumerator Start()
  {
    this.creater = new OutGameCharacterCreater();
    this.charas = new List<HomeCharacterBase>(16 /*0x10*/);
    this.peopleRoot = Utility.CreateGameObject("PeopleRoot", ((Component) this).transform);
    yield return (object) this.StartCoroutine(this.LoadPeopleWayPoint());
    if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
      yield return (object) this.StartCoroutine(this.GetHomePlayerCharacterList());
    this.isInitialized = true;
    yield return (object) this.StartCoroutine(this.LocateHomePeople());
    this.isPeopleInitialized = true;
    yield return (object) this.StartCoroutine(this.WatchHomePeople());
  }

  private IEnumerator GetHomePlayerCharacterList()
  {
    if (MonoBehaviourSingleton<FriendManager>.I.IsHomeCharaCached)
    {
      MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList((Action<bool>) null);
      yield return (object) null;
    }
    else
    {
      bool wait = true;
      MonoBehaviourSingleton<FriendManager>.I.SendHomeCharaList((Action<bool>) (b => wait = false));
      while (wait)
        yield return (object) null;
    }
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

  private IEnumerator WatchHomePeople()
  {
    while (true)
    {
      do
      {
        yield return (object) null;
      }
      while (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) || this.charas.Count >= 14);
      yield return (object) new WaitForSeconds(Random.Range(3f, 6f));
      if (!AppMain.isReset)
        this.CreateChara(this.leafPoints[Random.Range(0, this.leafPoints.Count)]);
      else
        break;
    }
  }

  private IEnumerator LocateHomePeople()
  {
    MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.SetupNPCSituations();
    foreach (OutGameSettingsManager.HomeScene.NPC npc1 in MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.npcs)
    {
      HomeCharacterBase npc2 = this.creater.CreateNPC((IHomePeople) this, this.peopleRoot, npc1);
      if (Object.op_Inequality((Object) npc2, (Object) null))
        this.charas.Add(npc2);
    }
    this.groupCenterPos = new Vector3(0.0f, -1f, 0.0f);
    if (Random.Range(0, 8) == 0)
    {
      int num1 = Random.Range(2, 5);
      List<HomeCharacterBase> homeCharacterBaseList = new List<HomeCharacterBase>();
      for (int index = 0; index < num1; ++index)
        homeCharacterBaseList.Add(this.CreateChara(this.centerPoint));
      this.groupCenterPos = homeCharacterBaseList[0]._transform.localPosition;
      float num2 = 360f / (float) num1;
      float num3 = Random.value * 360f;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(0.0f, 0.0f, 1f);
      int index1 = 0;
      while (index1 < num1)
      {
        Transform transform = homeCharacterBaseList[index1]._transform;
        transform.localPosition = Vector3.op_Addition(Quaternion.op_Multiply(Quaternion.AngleAxis(num3, Vector3.up), vector3), this.groupCenterPos);
        transform.LookAt(this.groupCenterPos);
        homeCharacterBaseList[index1].StopDiscussion();
        ++index1;
        num3 += num2;
      }
    }
    else
    {
      int num = Random.Range(1, 5);
      if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03))
        num = 0;
      for (int index = 0; index < num; ++index)
        this.CreateChara(this.centerPoint);
    }
    while (this.IsLoadingCharacter())
      yield return (object) null;
  }

  private bool IsLoadingCharacter()
  {
    return Object.op_Inequality((Object) this.charas.Find((Predicate<HomeCharacterBase>) (o => o.isLoading)), (Object) null);
  }

  private HomeCharacterBase CreateChara(WayPoint way_point)
  {
    FriendCharaInfo chara_info = (FriendCharaInfo) null;
    if (Random.Range(0, 1) == 0)
    {
      List<FriendCharaInfo> chara = MonoBehaviourSingleton<FriendManager>.I.homeCharas.chara;
      int count = chara.Count;
      if (count > 0)
      {
        int num = Random.Range(0, count);
        int index = num;
        FriendCharaInfo info;
        do
        {
          info = chara[index];
          if (info == null || Object.op_Inequality((Object) this.charas.Find((Predicate<HomeCharacterBase>) (o => o.GetFriendCharaInfo() != null && o.GetFriendCharaInfo().userId == info.userId)), (Object) null))
            index = (index + 1) % count;
          else
            goto label_5;
        }
        while (num != index);
        goto label_6;
label_5:
        chara_info = info;
      }
    }
label_6:
    HomeCharacterBase player = this.creater.CreatePlayer((IHomePeople) this, this.peopleRoot, chara_info, way_point);
    this.charas.Add(player);
    return player;
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
        Vector2 vector2_1 = Vector2.zero;
        Vector2 vector2_2 = Vector2.zero;
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

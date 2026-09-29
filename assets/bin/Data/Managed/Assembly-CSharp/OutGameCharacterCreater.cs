// Decompiled with JetBrains decompiler
// Type: OutGameCharacterCreater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class OutGameCharacterCreater
{
  public HomeCharacterBase CreateSelf(
    IHomePeople home_people,
    Transform parent,
    Action<HomeStageAreaEvent> notice_callback)
  {
    HomeSelfCharacter objectAndComponent = Utility.CreateGameObjectAndComponent("HomeSelfCharacter", parent) as HomeSelfCharacter;
    objectAndComponent.SetHomePeople(home_people);
    objectAndComponent.StopDiscussion();
    objectAndComponent.SetNoticeCallback(notice_callback);
    OutGameSettingsManager.HomeScene sceneSetting = GameSceneGlobalSettings.GetCurrentIHomeManager().GetSceneSetting();
    Vector3 vector3;
    float num;
    if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd)
    {
      MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
      vector3 = sceneSetting.selfInitStoryEndPos;
      num = sceneSetting.selfInitStoryEndRot;
    }
    else
    {
      vector3 = sceneSetting.selfInitPos;
      num = sceneSetting.selfInitRot;
    }
    objectAndComponent._transform.position = vector3;
    objectAndComponent._transform.eulerAngles = new Vector3(0.0f, num, 0.0f);
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      objectAndComponent.SetChatEvent();
    return (HomeCharacterBase) objectAndComponent;
  }

  public LoungePlayer CreateLoungePlayer(
    IHomePeople homePeople,
    Transform parent,
    CharaInfo chara_info,
    bool useMovingEntry)
  {
    LoungePlayer objectAndComponent = Utility.CreateGameObjectAndComponent("LoungePlayer", parent) as LoungePlayer;
    objectAndComponent.SetHomePeople(homePeople);
    objectAndComponent.StopDiscussion();
    objectAndComponent.SetLoungeCharaInfo(chara_info);
    OutGameSettingsManager.LoungeScene loungeScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene;
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(0.0f, 0.0f, -1f);
    float selfInitRot = loungeScene.selfInitRot;
    objectAndComponent._transform.position = useMovingEntry ? vector3 : loungeScene.selfInitPos;
    objectAndComponent._transform.eulerAngles = new Vector3(0.0f, selfInitRot, 0.0f);
    objectAndComponent.SetMoveTargetPosition(loungeScene.selfInitPos);
    objectAndComponent.SetChatEvent();
    return objectAndComponent;
  }

  public T CreateLoungePlayer<T>(
    IHomePeople homePeople,
    OutGameSettingsManager.LoungeScene loungeSetting,
    Transform parent,
    CharaInfo chara_info,
    bool useMovingEntry)
    where T : LoungePlayer
  {
    T objectAndComponent = Utility.CreateGameObjectAndComponent(typeof (T).Name, parent) as T;
    objectAndComponent.SetHomePeople(homePeople);
    objectAndComponent.StopDiscussion();
    objectAndComponent.SetLoungeCharaInfo(chara_info);
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(0.0f, 0.0f, -1f);
    float selfInitRot = loungeSetting.selfInitRot;
    objectAndComponent._transform.position = useMovingEntry ? vector3 : loungeSetting.selfInitPos;
    objectAndComponent._transform.eulerAngles = new Vector3(0.0f, selfInitRot, 0.0f);
    objectAndComponent.SetMoveTargetPosition(loungeSetting.selfInitPos);
    objectAndComponent.SetChatEvent();
    return objectAndComponent;
  }

  public HomeCharacterBase CreateNPC(
    IHomePeople homePeople,
    Transform parent,
    OutGameSettingsManager.HomeScene.NPC npc)
  {
    if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) && npc.npcID != 0)
      return (HomeCharacterBase) null;
    OutGameSettingsManager.HomeScene.NPC.Situation situation = npc.GetSituation();
    if (situation == null)
      return (HomeCharacterBase) null;
    HomeNPCCharacter npc1 = !npc.overrideComponentName.IsNullOrWhiteSpace() ? Utility.CreateGameObjectAndComponent(npc.overrideComponentName, parent) as HomeNPCCharacter : Utility.CreateGameObjectAndComponent("HomeNPCCharacter", parent) as HomeNPCCharacter;
    npc1.SetNPCInfo(npc);
    npc1.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
    npc1.SetHomePeople(homePeople);
    npc1._transform.position = situation.pos;
    npc1._transform.eulerAngles = new Vector3(0.0f, situation.rot, 0.0f);
    npc1._transform.localScale = new Vector3(npc.scaleX, 1f, 1f);
    npc1.StopDiscussion();
    return (HomeCharacterBase) npc1;
  }

  public HomeCharacterBase CreatePlayer(
    IHomePeople homePeople,
    Transform parent,
    FriendCharaInfo chara_info,
    WayPoint way_point)
  {
    HomePlayerCharacter objectAndComponent = Utility.CreateGameObjectAndComponent("HomePlayerCharacter", parent) as HomePlayerCharacter;
    Transform transform = ((Component) objectAndComponent).transform;
    transform.position = way_point.GetPosInCollider();
    objectAndComponent.SetHomePeople(homePeople);
    objectAndComponent.SetWayPoint(way_point);
    objectAndComponent.SetFriendCharcterInfo(chara_info);
    float num;
    float time;
    if (!((Object) way_point).name.StartsWith("LEAF"))
    {
      num = ((Component) way_point).transform.eulerAngles.y;
      time = 0.0f;
    }
    else
    {
      num = (float) Random.Range(0, 360);
      time = Random.Range(-2f, 2f);
    }
    objectAndComponent.SetWaitTime(time);
    transform.eulerAngles = new Vector3(0.0f, num, 0.0f);
    return (HomeCharacterBase) objectAndComponent;
  }

  public HomeCharacterBase CreateLoungeMoveNPC(
    IHomePeople homePeople,
    Transform parent,
    WayPoint way_point,
    OutGameSettingsManager.HomeScene.NPC npc)
  {
    LoungeMoveNPC objectAndComponent = Utility.CreateGameObjectAndComponent("LoungeMoveNPC", parent) as LoungeMoveNPC;
    Transform transform = ((Component) objectAndComponent).transform;
    transform.position = way_point.GetPosInCollider();
    transform.eulerAngles = new Vector3(0.0f, (float) Random.Range(0, 360), 0.0f);
    objectAndComponent.SetWaitTime(Random.Range(-2f, 2f));
    objectAndComponent.SetHomePeople(homePeople);
    objectAndComponent.SetWayPoint(way_point);
    objectAndComponent.SetNPCData(Singleton<NPCTable>.I.GetNPCData(npc.npcID));
    objectAndComponent.SetNPCInfo(npc);
    return (HomeCharacterBase) objectAndComponent;
  }
}

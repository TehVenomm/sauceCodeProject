// Decompiled with JetBrains decompiler
// Type: CoopStageObjectUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class CoopStageObjectUtility
{
  public static void SetAI(Character chara)
  {
    switch (chara)
    {
      case Player _:
        chara.AddController<NpcController>();
        break;
      case Enemy _:
        chara.AddController<EnemyController>();
        break;
    }
    chara.SafeActIdle();
  }

  public static void SetCoopModeForAll(StageObject.COOP_MODE_TYPE coop_mode, int client_id)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.objectList.Count; index < count; ++index)
    {
      StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.objectList[index];
      if (stageObject.coopMode != coop_mode)
        stageObject.SetCoopMode(coop_mode, client_id);
    }
  }

  public static void SetOfflineForAll()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach((Action<StageObject>) (obj => ((Component) obj).gameObject.SetActive(true)));
    MonoBehaviourSingleton<StageObjectManager>.I.ClearCacheObject();
    Vector3 vector3 = Vector3.zero;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
      vector3 = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.position;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.objectList.Count; index < count; ++index)
    {
      StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.objectList[index];
      if (!stageObject.IsCoopNone())
        stageObject.SetCoopMode(StageObject.COOP_MODE_TYPE.NONE, 0);
      stageObject.isCoopInitialized = true;
      switch (stageObject)
      {
        case Player _:
          Player chara1 = stageObject as Player;
          CoopStageObjectUtility.SetAI((Character) chara1);
          if (!chara1.isSetAppearPos)
          {
            if (chara1 is Self)
              chara1.SetAppearPosOwner(vector3);
            else
              chara1.SetAppearPosGuest(vector3);
          }
          if (chara1.isWaitBattleStart)
          {
            chara1.ActBattleStart();
            break;
          }
          break;
        case Enemy _:
          Enemy chara2 = stageObject as Enemy;
          CoopStageObjectUtility.SetAI((Character) chara2);
          if (!chara2.isSetAppearPos)
          {
            chara2.SetAppearPosEnemy();
            break;
          }
          break;
      }
    }
  }

  public static void TransfarOwner(StageObject obj, int owner_client_id)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == owner_client_id)
    {
      if (!CoopStageObjectUtility.CanControll(obj))
      {
        Log.Error(LOG.COOP, "TransfarOwner. field block obj({0}) to original", (object) obj);
      }
      else
      {
        obj.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
        Character chara = obj as Character;
        if (!Object.op_Inequality((Object) chara, (Object) null))
          return;
        CoopStageObjectUtility.SetAI(chara);
        if (!chara.isSetAppearPos)
          chara.SetAppearPos(chara._position);
        chara.characterSender.SendInitialize();
      }
    }
    else
    {
      if (obj is Player)
        obj.SetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET, owner_client_id);
      else
        obj.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, owner_client_id);
      obj.isCoopInitialized = false;
      Character character = obj as Character;
      if (!Object.op_Inequality((Object) character, (Object) null))
        return;
      character.RemoveController();
      character.SafeActIdle();
      character.characterReceiver.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE);
    }
  }

  public static void TransfarOwnerForClientObjects(int client_id, int owner_client_id)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    if (!FieldManager.IsValidInGameNoQuest())
    {
      int index = 0;
      while (index < MonoBehaviourSingleton<StageObjectManager>.I.cacheList.Count)
      {
        Player cache = MonoBehaviourSingleton<StageObjectManager>.I.cacheList[index] as Player;
        if (Object.op_Inequality((Object) cache, (Object) null))
        {
          ((Component) cache).gameObject.SetActive(true);
          MonoBehaviourSingleton<StageObjectManager>.I.cacheList.RemoveAt(index);
        }
        else
          ++index;
      }
    }
    bool flag = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == client_id;
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.objectList.Count; index1 < count; ++index1)
    {
      StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.objectList[index1];
      if ((stageObject.coopClientId == client_id || stageObject.coopClientId == 0 & flag) && (!FieldManager.IsValidInGame() || !(stageObject is Enemy)) && (!FieldManager.IsValidInGameNoQuest() || !(stageObject is Player)))
        CoopStageObjectUtility.TransfarOwner(stageObject, owner_client_id);
    }
  }

  public static bool FillNonPlayer(int nonplayer_max, int client_num)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !QuestManager.IsValidInGame() || MonoBehaviourSingleton<QuestManager>.I.IsExplore() || MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() || MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle() || QuestManager.IsValidTrial() || QuestManager.IsValidInGameSeriesArena())
      return false;
    int player_num = 0;
    MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach((Action<StageObject>) (o =>
    {
      if (o.isDestroyWaitFlag)
        return;
      ++player_num;
    }));
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach((Action<StageObject>) (o =>
    {
      if (!(o is Player) || o.isDestroyWaitFlag)
        return;
      ++player_num;
    }));
    if (client_num <= 0)
      client_num = 1;
    int num1 = nonplayer_max;
    if (QuestManager.IsValidInGame())
      num1 = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestMaxTeamMemberNum();
    int num2 = num1 - Mathf.Max(player_num, client_num);
    bool flag = false;
    for (int index = 0; index < num2; ++index)
    {
      MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(MonoBehaviourSingleton<CoopManager>.I.CreateUniqueNonPlayerID(), (PlayerLoader.OnCompleteLoad) (o =>
      {
        NonPlayer nonPlayer = o as NonPlayer;
        if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsBattle())
        {
          nonPlayer.ActBattleStart();
        }
        else
        {
          if (!Object.op_Inequality((Object) nonPlayer.controller, (Object) null))
            return;
          nonPlayer.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
        }
      }));
      flag = true;
    }
    return flag;
  }

  public static void ShrinkOriginalNonPlayer(int player_max)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    int num1 = player_max;
    if (QuestManager.IsValidInGame())
      num1 = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestMaxTeamMemberNum();
    int player_num = 0;
    MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach((Action<StageObject>) (o =>
    {
      if (o.isDestroyWaitFlag)
        return;
      ++player_num;
    }));
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach((Action<StageObject>) (o =>
    {
      if (!(o is Player) || o.isDestroyWaitFlag)
        return;
      ++player_num;
    }));
    int val1 = player_num - num1;
    if (val1 <= 0)
      return;
    List<StageObject> destroyList = new List<StageObject>();
    Action<StageObject> action = (Action<StageObject>) (o =>
    {
      if (!(o is NonPlayer) || o.isDestroyWaitFlag || !o.IsCoopNone() && !o.IsOriginal())
        return;
      destroyList.Add(o);
    });
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(action);
    MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.ForEach(action);
    int num2 = Math.Min(val1, destroyList.Count);
    for (int index = 0; index < num2; ++index)
      destroyList[index].DestroyObject();
  }

  public static void DestroyAllNonPlayer()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<StageObject> destroyList = new List<StageObject>();
    Action<StageObject> action = (Action<StageObject>) (o =>
    {
      if (!(o is NonPlayer) || o.isDestroyWaitFlag || !o.IsCoopNone() && !o.IsOriginal())
        return;
      destroyList.Add(o);
    });
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(action);
    MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.ForEach(action);
    MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach((Action<StageObject>) (o =>
    {
      Player player = o as Player;
      if (!Object.op_Inequality((Object) player, (Object) null) || !player.isNpc)
        return;
      destroyList.Add(o);
    }));
    for (int index = 0; index < destroyList.Count; ++index)
      destroyList[index].DestroyObject();
  }

  public static void OnlySelf()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<StageObject> destroyList = new List<StageObject>();
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach((Action<StageObject>) (o =>
    {
      if (!(o is Player) || o is Self)
        return;
      destroyList.Add(o);
    }));
    MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach((Action<StageObject>) (o =>
    {
      if (o is Self)
        return;
      destroyList.Add(o);
    }));
    destroyList.ForEach((Action<StageObject>) (obj => obj.DestroyObject()));
  }

  public static bool CanControll(StageObject obj)
  {
    return QuestManager.IsValidInGame() || FieldManager.IsValidInTutorial() || !(obj is Player) || obj is Self;
  }
}

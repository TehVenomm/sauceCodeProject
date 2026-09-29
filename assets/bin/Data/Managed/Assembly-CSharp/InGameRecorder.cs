// Decompiled with JetBrains decompiler
// Type: InGameRecorder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameRecorder : MonoBehaviourSingleton<InGameRecorder>
{
  public InGameProgress.PROGRESS_END_TYPE progressEndType;
  public string rushRemainTimeToString = "";
  public float arenaElapsedTime;
  public string arenaRemainTimeToString = "";

  public List<InGameRecorder.PlayerRecord> players { get; private set; }

  public List<InGameRecorder.EnemyRecord> enemies { get; private set; }

  public bool isVictory { get; private set; }

  public InGameRecorder.PlayerRecord pickupPlayer { get; private set; }

  public Vector3 pickupPlayerPos { get; private set; }

  public float pickupPlayerRot { get; private set; }

  public PlayerLoader[] playerModels { get; private set; }

  public InGameRecorder()
  {
    this.players = new List<InGameRecorder.PlayerRecord>();
    this.enemies = new List<InGameRecorder.EnemyRecord>();
  }

  public InGameRecorder.PlayerRecord GetPlayer(int id, int? user_id = null)
  {
    int num = -1;
    if (user_id.HasValue)
      num = user_id.Value;
    InGameRecorder.PlayerRecord player = (InGameRecorder.PlayerRecord) null;
    if (num > 0)
      player = this.GetPlayerByUserId(num);
    if (player != null)
    {
      CoopClient byUserId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByUserId(num);
      if (Object.op_Inequality((Object) byUserId, (Object) null))
        player.id = byUserId.playerId;
    }
    else
    {
      player = this.players.Find((Predicate<InGameRecorder.PlayerRecord>) (o => o.id == id));
      if (player == null)
      {
        player = new InGameRecorder.PlayerRecord();
        player.id = id;
        this.players.Add(player);
      }
    }
    return player;
  }

  public InGameRecorder.PlayerRecord GetPlayerByUserId(int userId)
  {
    return this.players.Find((Predicate<InGameRecorder.PlayerRecord>) (o => o.charaInfo != null && o.charaInfo.userId == userId));
  }

  public List<InGameRecorder.PlayerRecordSyncHost> CreateSyncHostData()
  {
    List<InGameRecorder.PlayerRecordSyncHost> syncHostData = new List<InGameRecorder.PlayerRecordSyncHost>();
    int index = 0;
    for (int count = this.players.Count; index < count; ++index)
    {
      InGameRecorder.PlayerRecord player = this.players[index];
      if (player.id >= 0)
      {
        InGameRecorder.PlayerRecordSyncHost playerRecordSyncHost = new InGameRecorder.PlayerRecordSyncHost();
        syncHostData.Add(playerRecordSyncHost);
        playerRecordSyncHost.id = player.id;
        playerRecordSyncHost.isNPC = player.isNPC;
        playerRecordSyncHost.playerLoadInfo = player.playerLoadInfo;
        playerRecordSyncHost.animID = player.animID;
        playerRecordSyncHost.charaInfo = player.charaInfo;
        playerRecordSyncHost.beforeLevel = player.beforeLevel;
        playerRecordSyncHost.givenTotalDamage = player.givenTotalDamage;
      }
    }
    return syncHostData;
  }

  public InGameRecorder.PlayerRecordSyncHost CreateSyncHostData(int userId)
  {
    InGameRecorder.PlayerRecordSyncHost syncHostData = (InGameRecorder.PlayerRecordSyncHost) null;
    int index = 0;
    for (int count = this.players.Count; index < count; ++index)
    {
      InGameRecorder.PlayerRecord player = this.players[index];
      if (player.id >= 0 && (player.charaInfo == null || player.charaInfo.userId == userId))
      {
        syncHostData = new InGameRecorder.PlayerRecordSyncHost();
        syncHostData.id = player.id;
        syncHostData.isNPC = player.isNPC;
        syncHostData.playerLoadInfo = player.playerLoadInfo;
        syncHostData.animID = player.animID;
        syncHostData.charaInfo = player.charaInfo;
        syncHostData.beforeLevel = player.beforeLevel;
        syncHostData.givenTotalDamage = player.givenTotalDamage;
      }
    }
    return syncHostData;
  }

  public void ApplySyncHostData(
    List<InGameRecorder.PlayerRecordSyncHost> sync_host_list)
  {
    int index = 0;
    for (int count = sync_host_list.Count; index < count; ++index)
      this.ApplySyncHostData(sync_host_list[index]);
  }

  public void ApplySyncHostData(InGameRecorder.PlayerRecordSyncHost sync_host)
  {
    if (sync_host.id < 0)
      return;
    int? user_id = sync_host.charaInfo != null ? new int?(sync_host.charaInfo.userId) : new int?();
    InGameRecorder.PlayerRecord player = this.GetPlayer(sync_host.id, user_id);
    player.givenTotalDamage = sync_host.givenTotalDamage;
    player.isNPC = sync_host.isNPC;
    player.playerLoadInfo = sync_host.playerLoadInfo;
    player.animID = sync_host.animID;
    player.charaInfo = sync_host.charaInfo;
    player.beforeLevel = sync_host.beforeLevel;
  }

  public void ApplySyncOwnerData(int id)
  {
    InGameRecorder.PlayerRecord player = this.GetPlayer(id);
    if (player.isSyncOwner)
      return;
    player.isSyncOwner = true;
  }

  public void RecordEnemyHP(int id, int hp)
  {
    InGameRecorder.EnemyRecord enemyRecord = this.enemies.Find((Predicate<InGameRecorder.EnemyRecord>) (o => o.id == id));
    if (enemyRecord == null)
    {
      enemyRecord = new InGameRecorder.EnemyRecord();
      enemyRecord.id = id;
      this.enemies.Add(enemyRecord);
    }
    enemyRecord.hp = hp;
  }

  public void SetEnemyRecoveredHP(int id, int recoveredHp)
  {
    InGameRecorder.EnemyRecord enemyRecord = this.enemies.Find((Predicate<InGameRecorder.EnemyRecord>) (o => o.id == id));
    if (enemyRecord == null)
    {
      enemyRecord = new InGameRecorder.EnemyRecord();
      enemyRecord.id = id;
      this.enemies.Add(enemyRecord);
    }
    enemyRecord.recoveredHp = recoveredHp;
  }

  public void RecordEnemyRecoveredHP(int id, int recoveredHp)
  {
    InGameRecorder.EnemyRecord enemyRecord = this.enemies.Find((Predicate<InGameRecorder.EnemyRecord>) (o => o.id == id));
    if (enemyRecord == null)
    {
      enemyRecord = new InGameRecorder.EnemyRecord();
      enemyRecord.id = id;
      this.enemies.Add(enemyRecord);
    }
    enemyRecord.recoveredHp += recoveredHp;
  }

  public void RecordGivenDamage(int player_id, int damage)
  {
    InGameRecorder.PlayerRecord player = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayer(player_id);
    if (player == null)
      return;
    player.givenTotalDamage += damage;
  }

  public int GetTotalEnemyHP()
  {
    int total = 0;
    this.enemies.ForEach((Action<InGameRecorder.EnemyRecord>) (o => total += o.hp));
    return total;
  }

  public int GetTotalEnemyHpContainsHealed()
  {
    int total = 0;
    this.enemies.ForEach((Action<InGameRecorder.EnemyRecord>) (o =>
    {
      total += o.hp;
      total += o.recoveredHp;
    }));
    return total;
  }

  public int GetEnemyRecoveredHpById(int id)
  {
    InGameRecorder.EnemyRecord enemyRecord = this.enemies.Find((Predicate<InGameRecorder.EnemyRecord>) (o => o.id == id));
    return enemyRecord == null ? 0 : enemyRecord.recoveredHp;
  }

  public void OnInGameEnd(bool is_victory)
  {
    this.isVictory = is_victory;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      List<InGameRecorder.PlayerRecord> list = new List<InGameRecorder.PlayerRecord>();
      this.players.ForEach((Action<InGameRecorder.PlayerRecord>) (o =>
      {
        if (Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(o.id), (Object) null))
          return;
        list.Add(o);
      }));
      this.players = list;
    }
    this.players.Sort((Comparison<InGameRecorder.PlayerRecord>) ((a, b) =>
    {
      if (a.givenTotalDamage == b.givenTotalDamage)
        return a.id - b.id;
      return a.givenTotalDamage <= b.givenTotalDamage ? 1 : -1;
    }));
    List<InGameRecorder.PlayerRecord> playerRecordList = new List<InGameRecorder.PlayerRecord>();
    for (int index = 0; index < 8 && index < this.players.Count; ++index)
      playerRecordList.Add(this.players[index]);
    this.players = playerRecordList;
    if (!this.isVictory && MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      StageObject stageObject = (StageObject) null;
      InGameRecorder.PlayerRecord selfPlayerRecord = this.GetSelfPlayerRecord();
      if (selfPlayerRecord != null)
        stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(selfPlayerRecord.id);
      if (selfPlayerRecord != null && Object.op_Inequality((Object) stageObject, (Object) null))
      {
        Vector3 position = stageObject._transform.position;
        position.y = 0.0f;
        this.pickupPlayerPos = position;
        this.pickupPlayerRot = stageObject._transform.eulerAngles.y;
        this.pickupPlayer = selfPlayerRecord;
      }
    }
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    this.progressEndType = MonoBehaviourSingleton<InGameProgress>.I.progressEndType;
    this.rushRemainTimeToString = MonoBehaviourSingleton<InGameProgress>.I.GetRushRemainTimeToString();
    this.arenaRemainTimeToString = MonoBehaviourSingleton<InGameProgress>.I.GetArenaRemainTimeToString();
    this.arenaElapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime();
  }

  public PlayerLoader[] CreatePlayerModels()
  {
    this.DeletePlayerModels();
    int anim_id = this.isVictory ? -1 : 91;
    List<InGameRecorder.PlayerRecord> playerRecordList = this.players.FindAll((Predicate<InGameRecorder.PlayerRecord>) (x => x.isShowModel));
    if (!this.isVictory)
      this.pickupPlayer = this.GetSelfPlayerRecord();
    if (this.pickupPlayer != null)
    {
      playerRecordList = new List<InGameRecorder.PlayerRecord>();
      playerRecordList.Add(this.pickupPlayer);
      this.pickupPlayer.playerLoadInfo.SetEquipWeapon(0, (EquipItemTable.EquipItemData) null);
    }
    PlayerLoader[] playerLoaderArray = new PlayerLoader[playerRecordList.Count];
    int index = 0;
    for (int count = playerRecordList.Count; index < count; ++index)
    {
      InGameRecorder.PlayerRecord playerRecord = playerRecordList[index];
      Transform gameObject = Utility.CreateGameObject("Player:" + (object) index, MonoBehaviourSingleton<StageManager>.I._transform);
      playerLoaderArray[index] = ((Component) gameObject).gameObject.AddComponent<PlayerLoader>();
      playerLoaderArray[index].StartLoad(playerRecord.playerLoadInfo, -1, anim_id, false, false, true, true, false, false, false, false, ShaderGlobal.GetCharacterShaderType(), (PlayerLoader.OnCompleteLoad) null);
      int length = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss.Length;
      if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && this.pickupPlayer == null && index < length)
      {
        gameObject.position = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss[index];
        gameObject.eulerAngles = new Vector3(0.0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerRots[index], 0.0f);
      }
    }
    if (this.pickupPlayer != null)
    {
      Transform transform = ((Component) playerLoaderArray[0]).transform;
      transform.position = this.pickupPlayerPos;
      transform.eulerAngles = new Vector3(0.0f, this.pickupPlayerRot, 0.0f);
    }
    this.playerModels = playerLoaderArray;
    return this.playerModels;
  }

  public void CreatePlayerModelsAsync(Action<PlayerLoader[]> callback)
  {
    this.StartCoroutine(this.DoCreatePlayerModelsAsync(callback));
  }

  private IEnumerator DoCreatePlayerModelsAsync(Action<PlayerLoader[]> callback)
  {
    this.DeletePlayerModels();
    yield return (object) null;
    int anim_type = this.isVictory ? -1 : 91;
    List<InGameRecorder.PlayerRecord> player_records = this.players.FindAll((Predicate<InGameRecorder.PlayerRecord>) (x => x.isShowModel));
    if (!this.isVictory)
      this.pickupPlayer = this.GetSelfPlayerRecord();
    if (this.pickupPlayer != null)
    {
      player_records = new List<InGameRecorder.PlayerRecord>();
      player_records.Add(this.pickupPlayer);
      this.pickupPlayer.playerLoadInfo.SetEquipWeapon(0, (EquipItemTable.EquipItemData) null);
    }
    PlayerLoader[] player_loaders = new PlayerLoader[player_records.Count];
    int i = 0;
    for (int n = player_records.Count; i < n; ++i)
    {
      InGameRecorder.PlayerRecord playerRecord = player_records[i];
      Transform player_t = Utility.CreateGameObject("Player:" + (object) i, MonoBehaviourSingleton<StageManager>.I._transform);
      player_loaders[i] = ((Component) player_t).gameObject.AddComponent<PlayerLoader>();
      player_loaders[i].StartLoad(playerRecord.playerLoadInfo, -1, anim_type, false, false, true, true, false, false, false, false, ShaderGlobal.GetCharacterShaderType(), (PlayerLoader.OnCompleteLoad) null);
      while (player_loaders[i].isLoading)
        yield return (object) null;
      int length = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss.Length;
      if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && this.pickupPlayer == null && i < length)
      {
        player_t.position = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerPoss[i];
        player_t.eulerAngles = new Vector3(0.0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.playerRots[i], 0.0f);
      }
      yield return (object) null;
      player_t = (Transform) null;
    }
    if (this.pickupPlayer != null)
    {
      Transform transform = ((Component) player_loaders[0]).transform;
      transform.position = this.pickupPlayerPos;
      transform.eulerAngles = new Vector3(0.0f, this.pickupPlayerRot, 0.0f);
    }
    yield return (object) null;
    this.playerModels = player_loaders;
    if (callback != null)
      callback(this.playerModels);
  }

  public void DeletePlayerModels()
  {
    if (this.playerModels == null)
      return;
    int index = 0;
    for (int length = this.playerModels.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.playerModels[index], (Object) null))
      {
        Object.DestroyImmediate((Object) ((Component) this.playerModels[index]).gameObject);
        this.playerModels[index] = (PlayerLoader) null;
      }
    }
    this.playerModels = (PlayerLoader[]) null;
  }

  public void SetRecordsForExplore(
    List<ExplorePlayerStatus> playerStatuses,
    PartyModel.Party party,
    ExploreBossStatus bossStatus,
    bool isInGame)
  {
    List<InGameRecorder.PlayerRecord> collection = new List<InGameRecorder.PlayerRecord>();
    foreach (ExplorePlayerStatus playerStatuse in playerStatuses)
    {
      ExplorePlayerStatus p = playerStatuse;
      if (p.isSelf)
      {
        InGameRecorder.PlayerRecord selfPlayerRecord = this.GetSelfPlayerRecord();
        if (selfPlayerRecord != null)
        {
          InGameRecorder.PlayerRecord inGameRecord = p.CreateInGameRecord(selfPlayerRecord.charaInfo);
          if (isInGame && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
            inGameRecord.id = MonoBehaviourSingleton<StageObjectManager>.I.self.id;
          collection.Add(inGameRecord);
        }
      }
      else
      {
        CharaInfo _charaInfo = (CharaInfo) null;
        if (party != null)
        {
          PartyModel.SlotInfo slotInfo = party.slotInfos.Find((Predicate<PartyModel.SlotInfo>) (x => x.userInfo != null && x.userInfo.userId == p.userId));
          if (slotInfo != null)
            _charaInfo = slotInfo.userInfo;
        }
        InGameRecorder.PlayerRecord inGameRecord = p.CreateInGameRecord(_charaInfo);
        if (inGameRecord != null && inGameRecord.charaInfo != null)
          collection.Add(inGameRecord);
        InGameRecorder.PlayerRecord playerRecord = this.players.Find((Predicate<InGameRecorder.PlayerRecord>) (x => x != null && x.charaInfo != null && x.charaInfo.userId == p.userId));
        if (playerRecord != null)
        {
          if (isInGame)
            inGameRecord.id = playerRecord.id;
        }
        else
          inGameRecord.isShowModel = false;
      }
    }
    this.players.Clear();
    this.players.AddRange((IEnumerable<InGameRecorder.PlayerRecord>) collection);
    this.players.Sort((Comparison<InGameRecorder.PlayerRecord>) ((a, b) =>
    {
      int num = b.givenTotalDamage - a.givenTotalDamage;
      return num != 0 ? num : a.id - b.id;
    }));
    this.enemies.Clear();
    InGameRecorder.EnemyRecord enemyRecord = new InGameRecorder.EnemyRecord();
    if (bossStatus == null || (int) bossStatus.hpMax <= 0)
    {
      enemyRecord.hp = 10000000;
    }
    else
    {
      enemyRecord.id = (int) bossStatus.coopEnemyId;
      enemyRecord.hp = (int) bossStatus.hpMax;
      enemyRecord.recoveredHp = bossStatus.recoveredHP;
    }
    this.enemies.Add(enemyRecord);
    if (this.isVictory || !MonoBehaviourSingleton<CoopManager>.IsValid() || isInGame)
      return;
    this.pickupPlayer = this.players.Find((Predicate<InGameRecorder.PlayerRecord>) (x => x.isSelf));
  }

  private InGameRecorder.PlayerRecord GetSelfPlayerRecord()
  {
    InGameRecorder.PlayerRecord pickupPlayer = this.pickupPlayer;
    if (pickupPlayer != null)
    {
      pickupPlayer.isSelf = true;
      return pickupPlayer;
    }
    if (this.players == null)
      return (InGameRecorder.PlayerRecord) null;
    InGameRecorder.PlayerRecord selfPlayerRecord = this.players.Find((Predicate<InGameRecorder.PlayerRecord>) (x => x.isSelf));
    if (selfPlayerRecord != null)
      return selfPlayerRecord;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
    {
      int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
      foreach (InGameRecorder.PlayerRecord player in this.players)
      {
        if (player != null && player.charaInfo != null && player.charaInfo.userId == id)
        {
          player.isSelf = true;
          return player;
        }
      }
    }
    if (selfPlayerRecord == null)
    {
      string str = "";
      foreach (InGameRecorder.PlayerRecord player in this.players)
        str = $"{str}{player.charaInfo.name}(id={(object) player.id},userId={(object) player.charaInfo.userId})\n";
    }
    return (InGameRecorder.PlayerRecord) null;
  }

  public static void CheckAndRepairIsSelf(ref List<InGameRecorder.PlayerRecord> list)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userInfo == null || list == null)
      return;
    int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int num = 0;
    foreach (InGameRecorder.PlayerRecord playerRecord in list)
    {
      if (playerRecord.charaInfo != null)
      {
        bool flag = playerRecord.charaInfo.userId == id;
        if (playerRecord.isSelf != flag)
        {
          Log.Warning(LOG.INGAME, playerRecord.charaInfo.name + "'s \"isSelf\" is Incorrect. ");
          playerRecord.isSelf = flag;
        }
        if (playerRecord.isSelf)
          ++num;
      }
    }
    if (num == 1)
      return;
    Log.Error(LOG.INGAME, $"Number of self record is incorrect! (num={(object) num})");
  }

  public abstract class CharacterRecord
  {
    public int id;
  }

  public class PlayerRecord : InGameRecorder.CharacterRecord
  {
    public bool isSelf;
    public bool isSyncOwner;
    public bool isNPC;
    public PlayerLoadInfo playerLoadInfo;
    public int animID;
    public CharaInfo charaInfo;
    public int beforeLevel;
    public int givenTotalDamage;
    public bool isShowModel = true;
  }

  public class PlayerRecordSyncHost
  {
    public int id;
    public bool isNPC;
    public PlayerLoadInfo playerLoadInfo;
    public int animID;
    public CharaInfo charaInfo;
    public int beforeLevel;
    public int givenTotalDamage;
  }

  public class EnemyRecord : InGameRecorder.CharacterRecord
  {
    public int hp;
    public int recoveredHp;
  }
}

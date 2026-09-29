// Decompiled with JetBrains decompiler
// Type: StageObjectManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StageObjectManager : MonoBehaviourSingleton<StageObjectManager>
{
  protected List<StageObjectManager.IDetachedNotify> notifyInterfaces = new List<StageObjectManager.IDetachedNotify>();
  public int presentBulletObjIndex;
  public int waveMatchDropObjIndex;

  public static bool appQuit { get; private set; }

  private void OnApplicationQuit() => StageObjectManager.appQuit = true;

  public static bool IsBossAssimilated
  {
    get
    {
      return MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null) && MonoBehaviourSingleton<StageObjectManager>.I.boss.enableAssimilation;
    }
  }

  public static bool CanTargetBoss
  {
    get
    {
      return MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null) && !MonoBehaviourSingleton<StageObjectManager>.I.boss.enableAssimilation;
    }
  }

  public Self self { get; private set; }

  public Enemy boss { get; private set; }

  public Enemy fieldEnemyBoss { get; private set; }

  public Transform physicsRoot { get; private set; }

  public List<StageObject> objectList { get; protected set; }

  public List<StageObject> characterList { get; protected set; }

  public List<StageObject> playerList { get; protected set; }

  public List<StageObject> nonplayerList { get; protected set; }

  public List<StageObject> enemyList { get; protected set; }

  public List<StageObject> gimmickList { get; protected set; }

  public List<StageObject> decoyList { get; protected set; }

  public List<StageObject> waveTargetList { get; protected set; }

  public List<StageObject> cacheList { get; protected set; }

  public List<Enemy> EnemyList { get; protected set; }

  public List<Enemy> enemyStokeList { get; protected set; }

  public List<Enemy> enemySummonStokeList { get; protected set; }

  public List<IPresentBulletObject> presentBulletObjList { get; protected set; }

  public List<WaveMatchDropObject> wmDropObjList { get; protected set; }

  public List<StageObjectManager.WaveTargetLine> waveTargetLineList { get; protected set; }

  public int deadWaveTargetMaxHp { get; protected set; }

  public StageObjectManager()
  {
    this.self = (Self) null;
    this.boss = (Enemy) null;
    this.physicsRoot = (Transform) null;
    this.objectList = new List<StageObject>();
    this.characterList = new List<StageObject>();
    this.playerList = new List<StageObject>();
    this.nonplayerList = new List<StageObject>();
    this.enemyList = new List<StageObject>();
    this.gimmickList = new List<StageObject>();
    this.decoyList = new List<StageObject>();
    this.waveTargetList = new List<StageObject>();
    this.cacheList = new List<StageObject>();
    this.enemyStokeList = new List<Enemy>();
    this.enemySummonStokeList = new List<Enemy>();
    this.presentBulletObjList = new List<IPresentBulletObject>();
    this.wmDropObjList = new List<WaveMatchDropObject>();
    this.waveTargetLineList = new List<StageObjectManager.WaveTargetLine>();
    this.deadWaveTargetMaxHp = 0;
    this.EnemyList = new List<Enemy>();
  }

  protected override void OnAttachServant(DisableNotifyMonoBehaviour servant)
  {
    base.OnAttachServant(servant);
    if (!(servant is StageObject))
      return;
    StageObject stageObject = servant as StageObject;
    this.objectList.Add(stageObject);
    if (stageObject is Character)
    {
      this.characterList.Add(stageObject);
      if (stageObject is Player)
      {
        this.playerList.Add(stageObject);
        if (stageObject is NonPlayer)
          this.nonplayerList.Add(stageObject);
        if (stageObject is Self && Object.op_Equality((Object) this.self, (Object) null))
          this.self = stageObject as Self;
      }
      else if (stageObject is Enemy)
      {
        Enemy enemy = stageObject as Enemy;
        this.enemyList.Add(stageObject);
        this.EnemyList.Add(enemy);
        if (Object.op_Equality((Object) this.boss, (Object) null) && enemy.isBoss)
          this.boss = enemy;
      }
    }
    if (stageObject is GimmickObject)
      this.gimmickList.Add(stageObject);
    if (stageObject is DecoyBulletObject)
      this.decoyList.Add(stageObject);
    if (stageObject is FieldWaveTargetObject)
      this.waveTargetList.Add(stageObject);
    if (!(stageObject is GimmickGeneratorObject))
      return;
    this.gimmickList.Add(stageObject);
  }

  protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
  {
    base.OnDetachServant(servant);
    if (!(servant is StageObject))
      return;
    StageObject stageObject = servant as StageObject;
    this.objectList.Remove(stageObject);
    if (stageObject is Character)
    {
      this.characterList.Remove(stageObject);
      if (stageObject is Player)
      {
        this.playerList.Remove(stageObject);
        if (stageObject is NonPlayer)
          this.nonplayerList.Remove(stageObject);
        if (Object.op_Equality((Object) this.self, (Object) (stageObject as Self)))
          this.self = (Self) null;
      }
      else if (stageObject is Enemy)
      {
        Enemy enemy = stageObject as Enemy;
        this.enemyList.Remove(stageObject);
        this.EnemyList.Remove(enemy);
        if (Object.op_Equality((Object) this.boss, (Object) (stageObject as Enemy)))
          this.boss = (Enemy) null;
      }
    }
    if (stageObject is GimmickGeneratorObject)
      this.gimmickList.Remove(stageObject);
    if (stageObject is DecoyBulletObject)
      this.decoyList.Remove(stageObject);
    if (stageObject is FieldWaveTargetObject)
    {
      this.AddDeadWaveMatchTargetMaxHp(stageObject as FieldWaveTargetObject);
      this.waveTargetList.Remove(stageObject);
    }
    if (stageObject is GimmickObject)
      this.gimmickList.Remove(stageObject);
    if (this.notifyServants != null)
    {
      int index = 0;
      for (int count = this.notifyServants.Count; index < count; ++index)
        ((StageObject) this.notifyServants[index]).OnDetachedObject(stageObject);
    }
    if (this.notifyInterfaces != null)
    {
      int index = 0;
      for (int count = this.notifyInterfaces.Count; index < count; ++index)
        this.notifyInterfaces[index].OnDetachedObject(stageObject);
    }
    if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      MonoBehaviourSingleton<TargetMarkerManager>.I.OnDetachedObject(stageObject);
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.OnDetachedObject(stageObject);
  }

  public void AddNotifyInterface(StageObjectManager.IDetachedNotify notify)
  {
    if (this.notifyInterfaces.Contains(notify))
      return;
    this.notifyInterfaces.Add(notify);
  }

  public void RemoveNotifyInterface(StageObjectManager.IDetachedNotify notify)
  {
    this.notifyInterfaces.Remove(notify);
  }

  public void SetFieldEnemyBoss(Enemy enemy) => this.fieldEnemyBoss = enemy;

  protected override void Awake()
  {
    base.Awake();
    if (!MonoBehaviourSingleton<StageManager>.IsValid())
      return;
    StageObject[] componentsInChildren = ((Component) MonoBehaviourSingleton<StageManager>.I).GetComponentsInChildren<StageObject>();
    if (componentsInChildren == null)
      return;
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (!componentsInChildren[index].IsRegisteredStageObjectManager)
        componentsInChildren[index].SetNotifyMaster((DisableNotifyMonoBehaviour) this);
    }
  }

  private void Start()
  {
    if (!Object.op_Equality((Object) this.physicsRoot, (Object) null))
      return;
    this.physicsRoot = new GameObject("PhysicsRoot")
    {
      transform = {
        parent = ((Component) this).transform
      }
    }.transform;
  }

  private void Update()
  {
    int index1 = 0;
    for (int count = this.objectList.Count; index1 < count; ++index1)
    {
      StageObject stageObject = this.objectList[index1];
      if (stageObject.isDestroyWaitFlag)
        stageObject.DestroyObject();
      if (count != this.objectList.Count)
      {
        count = this.objectList.Count;
        --index1;
      }
    }
    int index2 = 0;
    for (int count = this.cacheList.Count; index2 < count; ++index2)
    {
      StageObject cache = this.cacheList[index2];
      if (cache.isDestroyWaitFlag)
      {
        cache.DestroyObject();
      }
      else
      {
        if (Object.op_Inequality((Object) cache.packetReceiver, (Object) null))
          cache.packetReceiver.OnUpdate();
        if (Object.op_Inequality((Object) cache.packetSender, (Object) null))
          cache.packetSender.OnUpdate();
      }
      if (count != this.cacheList.Count)
      {
        count = this.cacheList.Count;
        --index2;
      }
    }
  }

  private void LateUpdate()
  {
    float num1 = 0.0f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num1 = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy.jostleSpeed;
    if ((double) num1 <= 0.0)
      return;
    int index1 = 0;
    for (int count = this.enemyList.Count; index1 < count; ++index1)
    {
      Enemy enemy1 = this.enemyList[index1] as Enemy;
      if (enemy1.isValidPush())
      {
        Vector2 vector2Xz1 = enemy1._position.ToVector2XZ();
        Enemy enemy2 = (Enemy) null;
        float num2 = float.MaxValue;
        float num3 = 0.0f;
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(0.0f, 0.0f);
        for (int index2 = index1 + 1; index2 < count; ++index2)
        {
          Enemy enemy3 = this.enemyList[index2] as Enemy;
          if (enemy3.isValidPush())
          {
            Vector2 vector2Xz2 = enemy3._position.ToVector2XZ();
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2Xz1, vector2Xz2);
            float sqrMagnitude = ((Vector2) ref vector2_2).sqrMagnitude;
            num3 = enemy1.bodyRadius + enemy3.bodyRadius;
            if ((double) sqrMagnitude > 0.0 && (double) sqrMagnitude < (double) num2 && (double) sqrMagnitude < (double) num3 * (double) num3)
            {
              enemy2 = enemy3;
              vector2_1 = vector2_2;
              num2 = sqrMagnitude;
            }
          }
        }
        if (Object.op_Inequality((Object) enemy2, (Object) null))
        {
          float num4 = Mathf.Sqrt(num2);
          Vector2 vector2_3 = Vector2.op_Division(vector2_1, num4);
          float num5 = (float) (1.0 - (double) num4 / (double) num3) * Time.deltaTime * num1;
          if ((double) num5 > (double) num3 * 0.5)
            num5 = num3 * 0.5f;
          double num6 = (double) num5;
          Vector2 vector2_4 = Vector2.op_Multiply(vector2_3, (float) num6);
          enemy1._position = Vector3.op_Addition(enemy1._position, vector2_4.ToVector3XZ());
          enemy2._position = Vector3.op_Subtraction(enemy2._position, vector2_4.ToVector3XZ());
        }
      }
    }
  }

  public void InvokeCoroutineImmidiately(IEnumerator _enumerator)
  {
    if (_enumerator == null)
      return;
    this.StartCoroutine(_enumerator);
  }

  public void Init(InGameManager.IntervalTransferInfo transfer_info = null)
  {
    Self self = (Self) null;
    if (transfer_info != null)
    {
      int index = 0;
      for (int count = transfer_info.playerInfoList.Count; index < count; ++index)
      {
        InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = transfer_info.playerInfoList[index];
        CoopClient coopClient = (CoopClient) null;
        if (MonoBehaviourSingleton<CoopManager>.IsValid() && !playerInfo.isSelf && playerInfo.coopClientId != 0)
          coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(playerInfo.coopClientId);
        Player player1;
        if (playerInfo.isSelf)
        {
          player1 = this.CreatePlayer(0, playerInfo.createInfo, true, Vector3.zero, 0.0f, playerInfo.transferInfo);
          if (!Object.op_Equality((Object) player1, (Object) null))
          {
            self = player1 as Self;
            self.taskChecker = playerInfo.taskChecker;
          }
          else
            continue;
        }
        else
        {
          bool flag = false;
          if (MonoBehaviourSingleton<CoopManager>.IsValid())
          {
            if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay)
              flag = true;
            else if (Object.op_Inequality((Object) coopClient, (Object) null) && coopClient.isLeave && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
              flag = true;
          }
          if (flag || playerInfo.coopMode == StageObject.COOP_MODE_TYPE.NONE || playerInfo.coopMode == StageObject.COOP_MODE_TYPE.ORIGINAL)
          {
            player1 = this.CreatePlayer(playerInfo.id, playerInfo.createInfo, false, Vector3.zero, 0.0f, playerInfo.transferInfo);
            if (!Object.op_Equality((Object) player1, (Object) null))
            {
              player1.SetAppearPosGuest(Vector3.zero);
              if (Object.op_Inequality((Object) coopClient, (Object) null) && playerInfo.isCoopPlayer)
                coopClient.SetPlayerID(player1.id);
            }
            else
              continue;
          }
          else
          {
            PlayerLoader.OnCompleteLoad callback = (PlayerLoader.OnCompleteLoad) (o =>
            {
              Player player2 = o as Player;
              ((Component) player2).gameObject.SetActive(false);
              MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject((StageObject) player2);
            });
            player1 = this.CreatePlayer(playerInfo.id, playerInfo.createInfo, false, Vector3.zero, 0.0f, playerInfo.transferInfo, callback);
            if (!Object.op_Equality((Object) player1, (Object) null))
            {
              if (Object.op_Inequality((Object) coopClient, (Object) null))
                coopClient.SetCachePlayer(player1.id, playerInfo.isCoopPlayer);
            }
            else
              continue;
          }
          if (flag || playerInfo.isNpcController)
          {
            if (!(player1.controller is NpcController))
              player1.AddController<NpcController>();
            if (QuestManager.IsValidInGameDefenseBattle())
              player1.DestroyObject();
          }
        }
        if (Object.op_Inequality((Object) player1.controller, (Object) null))
          player1.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
      }
    }
    if (Object.op_Equality((Object) self, (Object) null))
    {
      self = this.CreateSelf(0, Vector3.zero, 0.0f);
      if (Object.op_Inequality((Object) self, (Object) null) && Object.op_Inequality((Object) self.controller, (Object) null))
        self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
    }
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    if (QuestManager.IsValidInGame() && !MonoBehaviourSingleton<InGameManager>.I.IsNeedInitBoss())
      self.SetAppearPosGuest(Vector3.zero);
    if (!FieldManager.IsValidInGameNoQuest() && !MonoBehaviourSingleton<QuestManager>.I.IsExplore() && !MonoBehaviourSingleton<QuestManager>.I.IsWaveMatch())
      return;
    self.SetAppearPosField();
  }

  public void InitForArena(InGameManager.IntervalTransferInfo transferInfo = null)
  {
    Self self = (Self) null;
    if (transferInfo != null)
    {
      InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = transferInfo.playerInfoList[0];
      self = this.CreatePlayer(0, playerInfo.createInfo, true, Vector3.zero, 0.0f, playerInfo.transferInfo) as Self;
      self.taskChecker = playerInfo.taskChecker;
      if (Object.op_Inequality((Object) self.controller, (Object) null))
        self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
    }
    if (Object.op_Equality((Object) self, (Object) null))
    {
      self = this.CreateSelf(0, Vector3.zero, 0.0f);
      if (Object.op_Inequality((Object) self, (Object) null) && Object.op_Inequality((Object) self.controller, (Object) null))
        self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_START);
    }
    Object.op_Equality((Object) self, (Object) null);
  }

  public Self CreateSelf(int id, Vector3 pos, float dir)
  {
    if (!MonoBehaviourSingleton<StatusManager>.IsValid())
      return (Self) null;
    StageObjectManager.CreatePlayerInfo createinfo = MonoBehaviourSingleton<StatusManager>.I.assignedCharaInfo == null || !QuestManager.IsValidInGameTrial() ? (!QuestManager.IsValidInGameSeriesArena() ? MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo() : MonoBehaviourSingleton<StatusManager>.I.GetCreateUniquePlayerInfo((int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex + 1)) : MonoBehaviourSingleton<StatusManager>.I.GetAssignedCreatePlayerInfo();
    if (createinfo == null)
      return (Self) null;
    if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
      AssignedEquipmentTable.MergeAssignedEquip(ref createinfo, MonoBehaviourSingleton<StatusManager>.I.EventEquipSet);
    return this.CreatePlayer(id, createinfo, true, pos, dir) as Self;
  }

  public Player CreateNonPlayer(int id, PlayerLoader.OnCompleteLoad callback = null)
  {
    StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info = (StageObjectManager.CreatePlayerInfo.ExtentionInfo) null;
    Player nonPlayer = this.CreateNonPlayer(id, extention_info, Vector3.zero, 0.0f, callback: callback);
    if (Object.op_Equality((Object) nonPlayer, (Object) null))
      return (Player) null;
    Vector3 center_pos = Vector3.zero;
    if (Object.op_Inequality((Object) this.boss, (Object) null))
      center_pos = this.boss._transform.position;
    nonPlayer.SetAppearPosGuest(center_pos);
    return nonPlayer;
  }

  public Player CreateNonPlayer(
    int id,
    StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info,
    Vector3 pos,
    float dir,
    StageObjectManager.PlayerTransferInfo transfer_info = null,
    PlayerLoader.OnCompleteLoad callback = null)
  {
    StageObjectManager.CreatePlayerInfo create_info = new StageObjectManager.CreatePlayerInfo();
    create_info.charaInfo = new CharaInfo();
    bool flag1 = QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType() != 0;
    bool flag2 = false;
    if (extention_info != null)
    {
      create_info.extentionInfo = extention_info;
    }
    else
    {
      create_info.extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
      flag2 = true;
    }
    NPCTable.NPCData npcData = (NPCTable.NPCData) null;
    if (flag2)
    {
      List<int> exclusion_ids = new List<int>();
      int index = 0;
      for (int count = this.nonplayerList.Count; index < count; ++index)
      {
        NonPlayer nonplayer = this.nonplayerList[index] as NonPlayer;
        if (Object.op_Inequality((Object) nonplayer, (Object) null))
          exclusion_ids.Add(nonplayer.npcId);
      }
      if (QuestManager.IsValidInGame())
        npcData = Singleton<NPCTable>.I.GetNPCDataRandomFromQuestSpecial(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId(), exclusion_ids);
      if (npcData == null)
        npcData = Singleton<NPCTable>.I.GetNPCDataRandom(NPCTable.NPC_TYPE.FIGURE, exclusion_ids);
      if (npcData != null)
        create_info.extentionInfo.npcDataID = npcData.id;
    }
    else
      npcData = Singleton<NPCTable>.I.GetNPCData(create_info.extentionInfo.npcDataID);
    if (flag1)
    {
      int npcId = VorgonPreEventController.NPC_ID_LIST[id % 3];
      npcData = Singleton<NPCTable>.I.GetNPCData(npcId);
    }
    if (npcData == null)
      return (Player) null;
    npcData.CopyCharaInfo(create_info.charaInfo);
    NpcLevelTable.NpcLevelData npcLevelData = (NpcLevelTable.NpcLevelData) null;
    int lv = 1;
    if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID() > 0)
      lv = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
    if (flag1)
      lv = 80 /*0x50*/;
    if (flag2)
    {
      if (QuestManager.IsValidInGame())
        npcLevelData = (NpcLevelTable.NpcLevelData) Singleton<NpcLevelSpecialTable>.I.GetNPCLevelSpecial((uint) lv, npcData.id, MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId());
      if (npcLevelData == null)
        npcLevelData = Singleton<NpcLevelTable>.I.GetNpcLevelRandom((uint) lv);
      if (npcLevelData != null)
      {
        create_info.extentionInfo.npcLv = (int) npcLevelData.lv;
        create_info.extentionInfo.npcLvIndex = npcLevelData.lvIndex;
      }
    }
    else
    {
      if (npcData.npcType == NPCTable.NPC_TYPE.QUEST_SPECIAL && QuestManager.IsValidInGame())
        npcLevelData = (NpcLevelTable.NpcLevelData) Singleton<NpcLevelSpecialTable>.I.GetNPCLevelSpecial((uint) lv, npcData.id, MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestId());
      if (npcLevelData == null)
        npcLevelData = Singleton<NpcLevelTable>.I.GetNpcLevel((uint) create_info.extentionInfo.npcLv, create_info.extentionInfo.npcLvIndex);
    }
    if (npcLevelData == null)
      return (Player) null;
    if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField && (id == 991 || id == 992 || id == 990))
    {
      npcLevelData.CopyHomeCharaInfo(create_info.charaInfo, flag2 ? create_info.extentionInfo : (StageObjectManager.CreatePlayerInfo.ExtentionInfo) null, MonoBehaviourSingleton<InGameSettingsManager>.I.tutorialParam.atkIncreaseRate);
      InGameSettingsManager.TutorialParam tutParams = MonoBehaviourSingleton<InGameSettingsManager>.I.tutorialParam;
      CharaInfo.EquipItem equipItem = create_info.charaInfo.equipSet.Find((Predicate<CharaInfo.EquipItem>) (i => i.eId == tutParams.botWeaponIds[0]));
      if (equipItem != null)
      {
        equipItem.sIds.Add(tutParams.botSkillIds[0]);
      }
      else
      {
        equipItem = create_info.charaInfo.equipSet.Find((Predicate<CharaInfo.EquipItem>) (i => i.eId == tutParams.botWeaponIds[1]));
        equipItem?.sIds.Add(tutParams.botSkillIds[1]);
      }
      if (equipItem != null)
      {
        equipItem.lv = 1;
        equipItem.sLvs.Add(1);
        equipItem.sExs.Add(0);
      }
      return this.CreatePlayer(id, create_info, false, pos, dir, transfer_info, callback, true);
    }
    npcLevelData.CopyHomeCharaInfo(create_info.charaInfo, flag2 ? create_info.extentionInfo : (StageObjectManager.CreatePlayerInfo.ExtentionInfo) null);
    if (flag1)
    {
      for (int index = 0; index < create_info.charaInfo.equipSet.Count; ++index)
        create_info.charaInfo.equipSet[index].eId = VorgonPreEventController.NPC_WEAPON_ID_LIST[id % 3];
    }
    return this.CreatePlayer(id, create_info, false, pos, dir, transfer_info, callback);
  }

  public Player CreateGuest(int id, CharaInfo charaInfo, PlayerLoader.OnCompleteLoad callback = null)
  {
    StageObjectManager.CreatePlayerInfo createinfo = new StageObjectManager.CreatePlayerInfo();
    createinfo.charaInfo = charaInfo;
    if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
      AssignedEquipmentTable.MergeAssignedEquip(ref createinfo, MonoBehaviourSingleton<StatusManager>.I.EventEquipSet);
    Player player = this.CreatePlayer(id, createinfo, false, Vector3.zero, 0.0f, callback: callback);
    Vector3 center_pos = Vector3.zero;
    if (Object.op_Inequality((Object) this.boss, (Object) null))
      center_pos = this.boss._transform.position;
    player.SetAppearPosGuest(center_pos);
    return player;
  }

  public Player CreatePlayer(
    int id,
    StageObjectManager.CreatePlayerInfo create_info,
    bool self,
    Vector3 pos,
    float dir,
    StageObjectManager.PlayerTransferInfo transfer_info = null,
    PlayerLoader.OnCompleteLoad callback = null,
    bool usingRealAtk = false)
  {
    if (create_info.charaInfo == null)
    {
      Log.Error("StageObjectManager.CreatePlayer() charaInfo is NULL");
      return (Player) null;
    }
    GameObject gameObject = new GameObject();
    ((Object) gameObject).name = "Player:" + (object) id;
    gameObject.transform.parent = this._transform;
    int npc_id = 0;
    if (create_info.extentionInfo != null)
      npc_id = create_info.extentionInfo.npcDataID;
    bool flag = npc_id > 0;
    Player player1;
    if (self)
    {
      player1 = (Player) gameObject.AddComponent<Self>();
      if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
        MonoBehaviourSingleton<InGameCameraManager>.I.target = player1._transform;
    }
    else if (flag)
    {
      player1 = (Player) gameObject.AddComponent<NonPlayer>();
      NonPlayer nonPlayer = player1 as NonPlayer;
      nonPlayer.npcId = npc_id;
      if (Singleton<NPCTable>.IsValid())
        nonPlayer.npcTableData = Singleton<NPCTable>.I.GetNPCData(npc_id);
      nonPlayer.lv = create_info.extentionInfo.npcLv;
      nonPlayer.lv_index = create_info.extentionInfo.npcLvIndex;
    }
    else
      player1 = gameObject.AddComponent<Player>();
    player1.id = id;
    player1._transform.position = pos;
    player1._transform.eulerAngles = new Vector3(0.0f, dir, 0.0f);
    player1.SetState(create_info, transfer_info);
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
    {
      int userId = create_info.charaInfo.userId;
      InGameRecorder.PlayerRecord player2 = MonoBehaviourSingleton<InGameRecorder>.I.GetPlayer(id, new int?(userId));
      player1.record = player2;
      player2.isSelf = self;
      player2.isNPC = flag;
      player2.charaInfo = create_info.charaInfo;
      player2.beforeLevel = (int) create_info.charaInfo.level;
    }
    if (self)
      player1.AddController<SelfController>();
    else if (flag)
      player1.AddController<NpcController>();
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      EffectPlayProcessor component = ((Component) MonoBehaviourSingleton<InGameSettingsManager>.I).gameObject.GetComponent<EffectPlayProcessor>();
      if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
      {
        player1.effectPlayProcessor = gameObject.AddComponent<EffectPlayProcessor>();
        player1.effectPlayProcessor.effectSettings = component.effectSettings;
      }
    }
    PlayerLoadInfo load_info = new PlayerLoadInfo();
    if (player1.weaponData != null)
      load_info.SetEquipWeapon(create_info.charaInfo.sex, (uint) player1.weaponData.eId);
    load_info.Apply(create_info.charaInfo, false, true, true, true);
    if (self)
      load_info.isNeedToCache = true;
    player1.Load(load_info, callback);
    if (self)
      player1.OnCheckAndResizeColliderOsMapByWeapon(player1.weaponData != null ? player1.weaponData.eId : -1);
    player1.OnSetPlayerStatus((int) create_info.charaInfo.level, (int) create_info.charaInfo.atk, (int) create_info.charaInfo.def, (int) create_info.charaInfo.hp, false, transfer_info, usingRealAtk);
    player1.StartFieldBuff(MonoBehaviourSingleton<FieldManager>.IsValid() ? MonoBehaviourSingleton<FieldManager>.I.currentFieldBuffId : 0U);
    return player1;
  }

  public Enemy CreateEnemyWithAI(
    int id,
    Vector3 pos,
    float dir,
    int enemyId,
    int enemyLv,
    bool isBoss,
    bool isBigMonster,
    EnemyLoader.OnCompleteLoad callback = null)
  {
    Enemy enemy = (Enemy) null;
    for (int index = 0; index < this.enemyStokeList.Count; ++index)
    {
      if (this.enemyStokeList[index].enemyID == enemyId)
      {
        if (QuestManager.IsValidInGameWaveMatch())
        {
          if (this.enemyStokeList[index].isWaveMatchBoss != isBoss)
            continue;
        }
        else if (this.enemyStokeList[index].isBoss != isBoss)
          continue;
        enemy = this.enemyStokeList[index];
        enemy.ClearDead();
        ((Object) ((Component) enemy).gameObject).name = "Enemy:" + (object) id;
        this.enemyStokeList.Remove(enemy);
        break;
      }
    }
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      enemy.id = id;
      enemy._transform.parent = this._transform;
      enemy._transform.position = pos;
      enemy._transform.eulerAngles = new Vector3(0.0f, dir, 0.0f);
      if (QuestManager.IsValidInGame())
        enemy.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
      callback = this.CreateWrappedEnemyLoadCompletedDelegate(callback);
      if (callback != null)
        this.StartCoroutine(this._OnCallback(enemy, callback));
      else
        ((Component) enemy).gameObject.SetActive(true);
      return enemy;
    }
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) enemyId);
    uint growId = enemyData.growId;
    GrowEnemyTable.GrowEnemyData growEnemyData = Singleton<GrowEnemyTable>.I.GetGrowEnemyData(growId, enemyLv);
    GameObject gameObject = new GameObject();
    ((Object) gameObject).name = "Enemy:" + (object) id;
    Enemy enemyWithAi = gameObject.AddComponent<Enemy>();
    gameObject.SetActive(false);
    enemyWithAi.id = id;
    enemyWithAi.enemyID = (int) enemyData.id;
    if (QuestManager.IsValidInGameWaveMatch())
    {
      enemyWithAi.isBoss = false;
      enemyWithAi.isWaveMatchBoss = isBoss;
    }
    else if (FieldManager.IsValidInGameNoBoss() && !FieldManager.IsValidInTutorial())
    {
      enemyWithAi.isBoss = false;
      enemyWithAi.isWaveMatchBoss = false;
    }
    else
    {
      enemyWithAi.isBoss = isBoss;
      enemyWithAi.isWaveMatchBoss = false;
    }
    enemyWithAi.isBigMonster = isBigMonster;
    enemyWithAi.enemyTableData = enemyData;
    enemyWithAi.growTableData = growEnemyData;
    enemyWithAi.charaName = enemyData.name;
    enemyWithAi.enemyLevel = (XorInt) (growEnemyData != null ? (int) growEnemyData.level : (int) enemyData.level);
    enemyWithAi.moveStopRange *= enemyData.modelScale;
    enemyWithAi.AddController<EnemyController>();
    if (QuestManager.IsValidInGame())
      enemyWithAi.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
    gameObject.SetActive(true);
    enemyWithAi._transform.parent = this._transform;
    enemyWithAi._transform.position = pos;
    enemyWithAi._transform.eulerAngles = new Vector3(0.0f, dir, 0.0f);
    callback = this.CreateWrappedEnemyLoadCompletedDelegate(callback);
    enemyWithAi.loader.StartLoad(enemyData.modelId, enemyData.animId, enemyData.modelScale, enemyData.baseEffectName, enemyData.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), weather_effect: enemyData.weatherChangeEffect, callback: callback);
    return enemyWithAi;
  }

  public Enemy CreateEnemy(
    int id,
    Vector3 pos,
    float dir,
    int enemy_id,
    int enemy_lv,
    bool is_boss,
    bool is_big_monster,
    bool set_ai = true,
    bool willStock = false,
    EnemyLoader.OnCompleteLoad callback = null,
    bool isOverrideScale = false)
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) enemy_id);
    uint growId = enemyData.growId;
    GrowEnemyTable.GrowEnemyData growEnemyData = Singleton<GrowEnemyTable>.I.GetGrowEnemyData(growId, enemy_lv);
    bool flag = false;
    GameObject gameObject = (GameObject) null;
    Enemy enemy = (Enemy) null;
    int index = 0;
    for (int count = this.enemyStokeList.Count; index < count; ++index)
    {
      if (this.enemyStokeList[index].enemyID == enemy_id)
      {
        if (QuestManager.IsValidInGameWaveMatch())
        {
          if (this.enemyStokeList[index].isWaveMatchBoss != is_boss)
            continue;
        }
        else if (QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena())
        {
          if (this.enemyStokeList[index].isBoss != is_boss || (int) this.enemyStokeList[index].enemyLevel != enemy_lv)
            continue;
        }
        else if (this.enemyStokeList[index].isBoss != is_boss)
          continue;
        enemy = this.enemyStokeList[index];
        enemy.ClearDead();
        gameObject = ((Component) enemy).gameObject;
        ((Object) gameObject).name = "Enemy:" + (object) id;
        this.enemyStokeList.Remove(enemy);
        flag = true;
        break;
      }
    }
    if (Object.op_Equality((Object) enemy, (Object) null))
    {
      gameObject = new GameObject();
      ((Object) gameObject).name = "Enemy:" + (object) id;
      enemy = gameObject.AddComponent<Enemy>();
      gameObject.SetActive(false);
      enemy.enemyID = (int) enemyData.id;
      if (QuestManager.IsValidInGameWaveMatch())
      {
        enemy.isBoss = false;
        enemy.isWaveMatchBoss = is_boss;
      }
      else if (FieldManager.IsValidInGameNoQuest() && !FieldManager.IsValidInTutorial())
      {
        enemy.isBoss = false;
        enemy.isWaveMatchBoss = false;
      }
      else
      {
        enemy.isBoss = is_boss;
        enemy.isWaveMatchBoss = false;
      }
      enemy.isBigMonster = is_big_monster;
      enemy.enemyTableData = enemyData;
      enemy.growTableData = growEnemyData;
      enemy.charaName = enemyData.name;
      enemy.enemyLevel = (XorInt) (growEnemyData != null ? (int) growEnemyData.level : (int) enemyData.level);
      enemy.moveStopRange *= enemyData.modelScale;
      if (set_ai)
        enemy.AddController<EnemyController>();
    }
    if (!flag)
      gameObject.SetActive(true);
    enemy.id = id;
    if (QuestManager.IsValidInGame())
      enemy.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
    enemy._transform.parent = this._transform;
    enemy._transform.position = pos;
    enemy._transform.eulerAngles = new Vector3(0.0f, dir, 0.0f);
    float scale = isOverrideScale ? MonoBehaviourSingleton<InGameSettingsManager>.I.tutorialParam.bossScale : enemyData.modelScale;
    callback = this.CreateWrappedEnemyLoadCompletedDelegate(callback);
    if (flag)
    {
      if (callback != null)
        this.StartCoroutine(this._OnCallback(enemy, callback));
      else
        gameObject.SetActive(true);
    }
    else
      enemy.loader.StartLoad(enemyData.modelId, enemyData.animId, scale, enemyData.baseEffectName, enemyData.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), will_stock: willStock, weather_effect: enemyData.weatherChangeEffect, callback: callback);
    return enemy;
  }

  public Enemy CreateEnemy_GG_Optimize(
    int id,
    Vector3 pos,
    float dir,
    int enemy_id,
    int enemy_lv,
    bool is_boss,
    bool is_big_monster,
    bool set_ai = true,
    bool willStock = false,
    EnemyLoader.OnCompleteLoad callback = null,
    bool isOverrideScale = false,
    System.Action EffectCallBack = null,
    bool use_later_load = true)
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) enemy_id);
    uint growId = enemyData.growId;
    GrowEnemyTable.GrowEnemyData growEnemyData = Singleton<GrowEnemyTable>.I.GetGrowEnemyData(growId, enemy_lv);
    bool flag = false;
    GameObject gameObject = (GameObject) null;
    Enemy enemy = (Enemy) null;
    int index = 0;
    for (int count = this.enemyStokeList.Count; index < count; ++index)
    {
      if (this.enemyStokeList[index].enemyID == enemy_id)
      {
        if (QuestManager.IsValidInGameWaveMatch())
        {
          if (this.enemyStokeList[index].isWaveMatchBoss != is_boss)
            continue;
        }
        else if (QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena())
        {
          if (this.enemyStokeList[index].isBoss != is_boss || (int) this.enemyStokeList[index].enemyLevel != enemy_lv)
            continue;
        }
        else if (this.enemyStokeList[index].isBoss != is_boss)
          continue;
        enemy = this.enemyStokeList[index];
        enemy.ClearDead();
        gameObject = ((Component) enemy).gameObject;
        ((Object) gameObject).name = "Enemy:" + (object) id;
        this.enemyStokeList.Remove(enemy);
        flag = true;
        break;
      }
    }
    if (Object.op_Equality((Object) enemy, (Object) null))
    {
      gameObject = new GameObject();
      ((Object) gameObject).name = "Enemy:" + (object) id;
      enemy = gameObject.AddComponent<Enemy>();
      gameObject.SetActive(false);
      enemy.enemyID = (int) enemyData.id;
      if (QuestManager.IsValidInGameWaveMatch())
      {
        enemy.isBoss = false;
        enemy.isWaveMatchBoss = is_boss;
      }
      else if (FieldManager.IsValidInGameNoQuest() && !FieldManager.IsValidInTutorial())
      {
        enemy.isBoss = false;
        enemy.isWaveMatchBoss = false;
      }
      else
      {
        enemy.isBoss = is_boss;
        enemy.isWaveMatchBoss = false;
      }
      enemy.isBigMonster = is_big_monster;
      enemy.enemyTableData = enemyData;
      enemy.growTableData = growEnemyData;
      enemy.charaName = enemyData.name;
      enemy.enemyLevel = (XorInt) (growEnemyData != null ? (int) growEnemyData.level : (int) enemyData.level);
      enemy.moveStopRange *= enemyData.modelScale;
      if (set_ai)
        enemy.AddController<EnemyController>();
    }
    if (!flag)
      gameObject.SetActive(true);
    enemy.id = id;
    if (QuestManager.IsValidInGame())
      enemy.enemyReward = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyReward();
    enemy._transform.parent = this._transform;
    enemy._transform.position = pos;
    enemy._transform.eulerAngles = new Vector3(0.0f, dir, 0.0f);
    float scale = isOverrideScale ? MonoBehaviourSingleton<InGameSettingsManager>.I.tutorialParam.bossScale : enemyData.modelScale;
    callback = this.CreateWrappedEnemyLoadCompletedDelegate(callback);
    if (flag)
    {
      if (callback != null)
        this.StartCoroutine(this._OnCallback(enemy, callback));
      else
        gameObject.SetActive(true);
    }
    else
      enemy.loader.StartLoad_GG_Optomize(enemyData.modelId, enemyData.animId, scale, enemyData.baseEffectName, enemyData.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), will_stock: willStock, weather_effect: enemyData.weatherChangeEffect, callback: callback, effectCallBack: EffectCallBack, use_load_later: use_later_load);
    return enemy;
  }

  public Enemy CreateEnemyForDefenseBattle(int sid, int enemyId, int enemyLv)
  {
    Vector3 bossAppearOffsetPos = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.bossAppearOffsetPos;
    float bossAppearAngleY = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.bossAppearAngleY;
    Enemy enemyWithAi = this.CreateEnemyWithAI(sid, bossAppearOffsetPos, bossAppearAngleY, enemyId, enemyLv, true, true, (EnemyLoader.OnCompleteLoad) (target =>
    {
      if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
        return;
      MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
    }));
    if (Object.op_Equality((Object) enemyWithAi, (Object) null))
      return (Enemy) null;
    enemyWithAi.SetAppearPos(bossAppearOffsetPos);
    return enemyWithAi;
  }

  public Enemy CreateEnemyForSeries(int id, int index, EnemyLoader.OnCompleteLoad callback = null)
  {
    QuestManager i = MonoBehaviourSingleton<QuestManager>.I;
    int currentQuestSeriesNum = i.GetCurrentQuestSeriesNum();
    if (index >= currentQuestSeriesNum)
      return (Enemy) null;
    int currentQuestEnemyId = i.GetCurrentQuestEnemyID(index);
    int currentQuestEnemyLv = i.GetCurrentQuestEnemyLv(index);
    return this.CreateEnemy(id, Vector3.zero, 0.0f, currentQuestEnemyId, currentQuestEnemyLv, true, true, callback: callback);
  }

  public Enemy CreateEnemyForSummonAttack(
    int id,
    Vector3 pos,
    float dir,
    int enemy_id,
    int enemy_lv,
    bool willStock,
    EnemyLoader.OnCompleteLoad callback = null)
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) enemy_id);
    GameObject gameObject = (GameObject) null;
    Enemy enemy = (Enemy) null;
    bool flag = false;
    int index = 0;
    for (int count = this.enemySummonStokeList.Count; index < count; ++index)
    {
      if (this.enemySummonStokeList[index].enemyTableData.modelId == enemyData.modelId)
      {
        enemy = this.enemySummonStokeList[index];
        enemy.ClearDead();
        gameObject = ((Component) enemy).gameObject;
        ((Object) gameObject).name = "Enemy:" + (object) id;
        flag = true;
        break;
      }
    }
    if (Object.op_Equality((Object) enemy, (Object) null))
    {
      gameObject = new GameObject();
      ((Object) gameObject).name = "Enemy:" + (object) id;
      enemy = gameObject.AddComponent<Enemy>();
      gameObject.SetActive(false);
      enemy.enemyID = (int) enemyData.id;
      enemy.isBoss = false;
      enemy.isWaveMatchBoss = false;
      enemy.isBigMonster = false;
      enemy.enemyTableData = enemyData;
      enemy.growTableData = Singleton<GrowEnemyTable>.I.GetGrowEnemyData(enemyData.growId, enemy_lv);
      enemy.charaName = enemyData.name;
      enemy.enemyLevel = (XorInt) enemy_lv;
      enemy.moveStopRange *= enemyData.modelScale;
      enemy.isSummonAttack = true;
    }
    enemy.id = id;
    if (!flag)
      gameObject.SetActive(true);
    enemy._transform.parent = this._transform;
    enemy._transform.position = pos;
    enemy._transform.eulerAngles = new Vector3(0.0f, dir, 0.0f);
    callback = this.CreateWrappedEnemyLoadCompletedDelegate(callback);
    if (flag)
    {
      if (callback != null)
        this.StartCoroutine(this._OnCallback(enemy, callback));
      else
        gameObject.SetActive(true);
    }
    else
      enemy.loader.StartLoad(enemyData.modelId, enemyData.animId, enemyData.modelScale, enemyData.baseEffectName, enemyData.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), will_stock: willStock, weather_effect: enemyData.weatherChangeEffect, callback: callback);
    return enemy;
  }

  private EnemyLoader.OnCompleteLoad CreateWrappedEnemyLoadCompletedDelegate(
    EnemyLoader.OnCompleteLoad callback)
  {
    return (EnemyLoader.OnCompleteLoad) (e =>
    {
      if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && e.isBoss)
      {
        ExploreBossStatus exploreBossStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossStatus();
        if (exploreBossStatus != null)
          e.ApplyExploreBossStatus(exploreBossStatus);
        else
          MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(e);
      }
      if (callback == null)
        return;
      callback(e);
    });
  }

  protected IEnumerator _OnCallback(Enemy enemy, EnemyLoader.OnCompleteLoad callback)
  {
    yield return (object) null;
    if (enemy.isStoke)
    {
      ((Component) enemy).gameObject.SetActive(true);
      enemy.isStoke = false;
    }
    callback(enemy);
  }

  private StageObject Find(List<StageObject> list, int id)
  {
    int index = 0;
    for (int count = list.Count; index < count; ++index)
    {
      if (list[index].id == id)
        return list[index];
    }
    return (StageObject) null;
  }

  public StageObject FindObject(int id) => this.Find(this.objectList, id);

  public StageObject FindCharacter(int id) => this.Find(this.characterList, id);

  public StageObject FindPlayer(int id) => this.Find(this.playerList, id);

  public StageObject FindNonPlayer(int id) => this.Find(this.nonplayerList, id);

  public StageObject FindEnemy(int id) => this.Find(this.enemyList, id);

  public StageObject FindGimmick(int id) => this.Find(this.gimmickList, id);

  public StageObject FindDecoy(int id) => this.Find(this.decoyList, id);

  public StageObject FindWaveTarget(int id) => this.Find(this.waveTargetList, id);

  public StageObject FindCache(int id) => this.Find(this.cacheList, id);

  private StageObject Find(List<StageObject> list, Vector3 pos, float range)
  {
    StageObject stageObject1 = (StageObject) null;
    float num = range;
    int index = 0;
    for (int count = list.Count; index < count; ++index)
    {
      StageObject stageObject2 = list[index];
      if (((Component) stageObject2).gameObject.activeSelf)
      {
        Vector3 vector3 = Vector3.op_Subtraction(stageObject2._transform.position, pos);
        float magnitude = ((Vector3) ref vector3).magnitude;
        if ((double) magnitude < (double) num)
        {
          num = magnitude;
          stageObject1 = stageObject2;
        }
      }
    }
    return stageObject1;
  }

  public StageObject FindObject(Vector3 pos, float range) => this.Find(this.objectList, pos, range);

  public StageObject FindCharacter(Vector3 pos, float range)
  {
    return this.Find(this.characterList, pos, range);
  }

  public StageObject FindPlayer(Vector3 pos, float range) => this.Find(this.playerList, pos, range);

  public StageObject FindNonPlayer(Vector3 pos, float range)
  {
    return this.Find(this.nonplayerList, pos, range);
  }

  public StageObject FindEnemy(Vector3 pos, float range) => this.Find(this.enemyList, pos, range);

  public List<Player> GetAlivePlayerList()
  {
    List<Player> alivePlayerList = new List<Player>();
    int index = 0;
    for (int count = this.playerList.Count; index < count; ++index)
    {
      Player player = this.playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && !player.isDead)
        alivePlayerList.Add(player);
    }
    return alivePlayerList;
  }

  public void AddCacheObject(StageObject obj) => this.cacheList.Add(obj);

  public void RemoveCacheObject(StageObject obj) => this.cacheList.Remove(obj);

  public void ClearCacheObject() => this.cacheList.Clear();

  public bool IsFieldEnemyBoss(int sid)
  {
    return !Object.op_Equality((Object) this.fieldEnemyBoss, (Object) null) && sid == this.fieldEnemyBoss.id;
  }

  public bool ExistsEnemyValiedHealAttack()
  {
    for (int index = 0; index < this.enemyList.Count; ++index)
    {
      Enemy enemy = this.enemyList[index] as Enemy;
      if (Object.op_Inequality((Object) enemy, (Object) null) && (double) enemy.healDamageRate > 0.0)
        return true;
    }
    return false;
  }

  public void RemovePresentBulletObject(int presentBulletId)
  {
    if (this.presentBulletObjList == null || this.presentBulletObjList.Count <= 0)
      return;
    this.presentBulletObjList.RemoveAll((Predicate<IPresentBulletObject>) (item => item.GetPresentBulletId() == presentBulletId));
  }

  private void AddDeadWaveMatchTargetMaxHp(FieldWaveTargetObject obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    this.deadWaveTargetMaxHp += obj.maxHp;
  }

  public bool IsWaveMatchTargetAllDead()
  {
    if (this.waveTargetList == null)
      return true;
    int count = this.waveTargetList.Count;
    if (count == 0)
      return true;
    for (int index = 0; index < count; ++index)
    {
      FieldWaveTargetObject waveTarget = this.waveTargetList[index] as FieldWaveTargetObject;
      if (!Object.op_Equality((Object) waveTarget, (Object) null) && !waveTarget.isDead)
        return false;
    }
    return true;
  }

  public float GetWaveMatchTargetHpRate()
  {
    if (this.waveTargetList == null)
      return 0.0f;
    int count = this.waveTargetList.Count;
    if (count == 0)
      return 0.0f;
    int deadWaveTargetMaxHp = this.deadWaveTargetMaxHp;
    int num = 0;
    for (int index = 0; index < count; ++index)
    {
      FieldWaveTargetObject waveTarget = this.waveTargetList[index] as FieldWaveTargetObject;
      if (!Object.op_Equality((Object) waveTarget, (Object) null))
      {
        deadWaveTargetMaxHp += waveTarget.maxHp;
        num += waveTarget.nowHp;
      }
    }
    return deadWaveTargetMaxHp <= 0 ? 0.0f : (float) ((double) num / (double) deadWaveTargetMaxHp * 100.0);
  }

  public void AddWaveMatchDropObject(WaveMatchDropObject obj)
  {
    if (this.wmDropObjList == null || Object.op_Equality((Object) obj, (Object) null))
      return;
    this.wmDropObjList.Add(obj);
  }

  public void PickedWaveMatchDropObject(Coop_Model_WaveMatchDropPicked model, bool isRemove)
  {
    if (this.wmDropObjList == null)
      return;
    bool flag = false;
    int index = 0;
    for (int count = this.wmDropObjList.Count; index < count; ++index)
    {
      WaveMatchDropObject wmDropObj = this.wmDropObjList[index];
      if (model.managedId == wmDropObj.GetId())
      {
        wmDropObj.OnReceiveEffect();
        if (isRemove)
          this._RemoveWaveMatchDropObject(wmDropObj);
        flag = true;
        break;
      }
    }
    if (flag || !Singleton<WaveMatchDropTable>.IsValid())
      return;
    WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(model.tableId);
    if (data == null || data.type != WAVEMATCH_ITEM_TYPE.CLOCK)
      return;
    WaveMatchDropObjectClock.PickedProcess(data);
  }

  public void RemoveWaveMatchDropObject(int id)
  {
    if (this.wmDropObjList == null || this.wmDropObjList.Count <= 0)
      return;
    int index = 0;
    for (int count = this.wmDropObjList.Count; index < count; ++index)
    {
      WaveMatchDropObject wmDropObj = this.wmDropObjList[index];
      if (id == wmDropObj.GetId())
      {
        this._RemoveWaveMatchDropObject(wmDropObj);
        break;
      }
    }
  }

  private void _RemoveWaveMatchDropObject(WaveMatchDropObject obj)
  {
    this.wmDropObjList.Remove(obj);
    obj.OnDisappear();
    obj = (WaveMatchDropObject) null;
  }

  public void SetAllEnemiesTargetDecoy()
  {
    if (this.enemyList.IsNullOrEmpty<StageObject>())
      return;
    int index = 0;
    for (int count = this.enemyList.Count; index < count; ++index)
    {
      Enemy enemy = this.enemyList[index] as Enemy;
      if (!Object.op_Equality((Object) enemy, (Object) null) && !enemy.isDead)
      {
        EnemyController controller = enemy.controller as EnemyController;
        if (!Object.op_Equality((Object) controller, (Object) null))
          controller.OnSetDecoy();
      }
    }
  }

  public void CheckAllEnemiesMissDecoy(StageObject decoyObj)
  {
    if (this.enemyList.IsNullOrEmpty<StageObject>())
      return;
    int index = 0;
    for (int count = this.enemyList.Count; index < count; ++index)
    {
      Enemy enemy = this.enemyList[index] as Enemy;
      if (!Object.op_Equality((Object) enemy, (Object) null) && !enemy.isDead)
      {
        EnemyController controller = enemy.controller as EnemyController;
        if (!Object.op_Equality((Object) controller, (Object) null))
          controller.OnCheckMissDecoy(decoyObj);
      }
    }
  }

  public void DrawWaveTargetLine(FieldMapTable.EnemyPopTableData popData)
  {
    if (!this.waveTargetLineList.IsNullOrEmpty<StageObjectManager.WaveTargetLine>() && this.waveTargetLineList.Exists((Predicate<StageObjectManager.WaveTargetLine>) (item => Vector3.op_Equality(item.fromPosition, popData.GeneratePopPosVec3()) && item.waveNo == popData.waveNo)))
      return;
    Transform trans = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.bulletLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
    if (Object.op_Equality((Object) trans, (Object) null))
      return;
    LineRenderer component = ((Component) trans).GetComponent<LineRenderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    float strategyLineWidth = GameDefine.kWaveStrategyLineWidth;
    Vector3 popPosVec3 = popData.GeneratePopPosVec3();
    Vector3 position = AIUtility.GetNearestWaveMatchTargetObject(popData.GeneratePopPosVec3())._position;
    popPosVec3.y = strategyLineWidth * 0.5f;
    position.y = strategyLineWidth * 0.5f;
    component.SetPosition(0, popPosVec3);
    component.SetPosition(1, position);
    component.startWidth = strategyLineWidth;
    component.endWidth = strategyLineWidth;
    ((Renderer) component).enabled = true;
    this.waveTargetLineList.Add(new StageObjectManager.WaveTargetLine(trans, popData.GeneratePopPosVec3(), popData.waveNo, popData.popNumTotal));
  }

  public void CountDownByWaveNo(int waveNo, Vector3 fromPosition)
  {
    if (waveNo <= 0 || this.waveTargetLineList.IsNullOrEmpty<StageObjectManager.WaveTargetLine>())
      return;
    StageObjectManager.WaveTargetLine waveTargetLine = this.waveTargetLineList.Find((Predicate<StageObjectManager.WaveTargetLine>) (o => o.waveNo == waveNo && Vector3.op_Equality(o.fromPosition, fromPosition)));
    if (waveTargetLine == null)
      return;
    --waveTargetLine.popCount;
    if (waveTargetLine.popCount > 0)
      return;
    waveTargetLine.isActive = false;
  }

  public void ClearWaveTargetLine()
  {
    if (this.waveTargetLineList.IsNullOrEmpty<StageObjectManager.WaveTargetLine>())
      return;
    int index = 0;
    for (int count = this.waveTargetLineList.Count; index < count; ++index)
    {
      if (!this.waveTargetLineList[index].isActive)
      {
        Object.Destroy((Object) ((Component) this.waveTargetLineList[index].rendererTransform).gameObject);
        this.waveTargetLineList[index].rendererTransform = (Transform) null;
      }
    }
    this.waveTargetLineList.RemoveAll((Predicate<StageObjectManager.WaveTargetLine>) (o => !o.isActive));
  }

  public List<StageObject> GetAllBreakableObject()
  {
    if (this.gimmickList == null || this.gimmickList.Count < 1)
      return (List<StageObject>) null;
    List<StageObject> allBreakableObject = new List<StageObject>();
    int index = 0;
    for (int count = this.gimmickList.Count; index < count; ++index)
    {
      if (this.gimmickList[index] is BreakObject)
        allBreakableObject.Add(this.gimmickList[index]);
    }
    return allBreakableObject;
  }

  public List<StageObject> GetAllCoopObjectList()
  {
    if (this.gimmickList.IsNullOrEmpty<StageObject>())
      return (List<StageObject>) null;
    List<StageObject> allCoopObjectList = new List<StageObject>();
    int index = 0;
    for (int count = this.gimmickList.Count; index < count; ++index)
    {
      if (this.gimmickList[index] is BreakObject || this.gimmickList[index] is GimmickGeneratorObject)
        allCoopObjectList.Add(this.gimmickList[index]);
    }
    return allCoopObjectList;
  }

  public IEnumerator CreateNextEnemyForSeriesOfBattles(Enemy enemy)
  {
    if (Object.op_Equality((Object) this.boss, (Object) null))
      ((Component) enemy).gameObject.SetActive(true);
    else if (!Object.op_Equality((Object) enemy, (Object) null))
    {
      yield return (object) this.StartCoroutine(this.boss.WaitForDeadMotionEnd());
      this.boss = enemy;
      if (!enemy.IsOriginal() && !enemy.IsCoopNone())
      {
        while (!enemy.isCoopInitialized)
          yield return (object) null;
      }
      this.ShowEnemyFromUnderGroundForSeriesOfBattles(enemy);
    }
  }

  public void DebugShowEnemyFromUnderGround(Enemy enemy)
  {
    this.ShowEnemyFromUnderGroundForSeriesOfBattles(enemy);
  }

  private void ShowEnemyFromUnderGroundForSeriesOfBattles(Enemy enemy)
  {
    Transform effectTrans;
    this.ShowEnemyEntryExitEffect(enemy, out effectTrans);
    int from = -40;
    enemy.onTheGround = false;
    Vector3 position = enemy._transform.position;
    position.y = (float) from;
    enemy._transform.position = position;
    ((Component) enemy).gameObject.SetActive(true);
    enemy.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    if (Object.op_Inequality((Object) enemy.controller, (Object) null))
      enemy.controller.SetEnableControll(false);
    enemy.PlayMotion(124);
    this.StartCoroutine(this.SimpleMoveCharacterY((Character) enemy, (float) from, StageManager.GetHeight(enemy._position), 1f, (System.Action) (() =>
    {
      if (Object.op_Inequality((Object) enemy.controller, (Object) null))
        enemy.controller.SetEnableControll(true);
      EffectManager.ReleaseEffect(((Component) effectTrans).gameObject);
      enemy.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
      if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
        MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget(enemy);
      enemy.CountShadowSealingTarget();
      if (!MonoBehaviourSingleton<SoundManager>.IsValid())
        return;
      SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
    })));
  }

  public void ShowEnemyFromUnderGroundForSummon(Enemy enemy, string summonEffectName)
  {
    Transform effectTrans;
    this.ShowEffectOnGround(summonEffectName, (StageObject) enemy, out effectTrans);
    enemy.onTheGround = false;
    int from = -10;
    Vector3 position = enemy._position;
    position.y = (float) from;
    enemy._position = position;
    ((Component) enemy).gameObject.SetActive(true);
    enemy.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    if (Object.op_Inequality((Object) enemy.controller, (Object) null))
      enemy.controller.SetEnableControll(false);
    this.StartCoroutine(this.SimpleMoveCharacterY((Character) enemy, (float) from, StageManager.GetHeight(enemy._position), 1f, (System.Action) (() =>
    {
      if (Object.op_Inequality((Object) enemy.controller, (Object) null))
        enemy.controller.SetEnableControll(true);
      EffectManager.ReleaseEffect(((Component) effectTrans).gameObject);
      enemy.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
      enemy.brainParam.scoutParam = new BrainParam.ScountingParam()
      {
        scountigRangeSqr = 250000f,
        scoutingSightCos = Mathf.Cos(1.57079637f),
        scoutingAudibilitySqr = 250000f
      };
    })));
  }

  private void ShowEnemyEntryExitEffect(Enemy enemy, out Transform trans)
  {
    trans = EffectManager.GetEffect("ef_btl_enemy_entry_01");
    Vector3 localPosition = enemy._transform.localPosition;
    localPosition.y = StageManager.GetHeight(enemy._position);
    trans.localPosition = localPosition;
  }

  private void ShowEffectOnGround(
    string effectName,
    StageObject stageObject,
    out Transform effectTrans)
  {
    effectTrans = EffectManager.GetEffect(effectName);
    Vector3 localPosition = stageObject._transform.localPosition;
    localPosition.y = StageManager.GetHeight(stageObject._position);
    effectTrans.localPosition = localPosition;
  }

  public IEnumerator SimpleMoveCharacterY(
    Character character,
    float from,
    float to,
    float time,
    System.Action OnEndAction)
  {
    character.onTheGround = false;
    Vector3 position1 = character._transform.position;
    position1.y = from;
    character._transform.position = position1;
    float speed = (to - from) / time;
    float elapsedTime = 0.0f;
    while (!Object.op_Equality((Object) character, (Object) null) && !Object.op_Equality((Object) character._transform, (Object) null))
    {
      Vector3 position2 = character._transform.position;
      position2.y += speed * Time.deltaTime;
      character._transform.position = position2;
      elapsedTime += Time.deltaTime;
      if ((double) elapsedTime <= (double) time)
      {
        yield return (object) null;
      }
      else
      {
        character.onTheGround = true;
        OnEndAction.SafeInvoke();
        break;
      }
    }
  }

  [Serializable]
  public class CreatePlayerInfo
  {
    public CharaInfo charaInfo;
    public StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo;

    [Serializable]
    public class ExtentionInfo
    {
      public List<int> weaponIndexList = new List<int>();
      public int npcDataID;
      public int npcLv;
      public int npcLvIndex;
      public int uniqueEquipmentIndex;

      public override string ToString()
      {
        string str = "";
        str += "w[";
        if (this.weaponIndexList != null)
          this.weaponIndexList.ForEach((Action<int>) (w => str = $"{str}{(object) w},"));
        str += "]";
        str = $"{str},{(object) this.npcDataID}";
        str = $"{str},{(object) this.npcLv}";
        str = $"{str},{(object) this.npcLvIndex}";
        str = $"{str},{(object) this.uniqueEquipmentIndex}";
        return base.ToString() + str;
      }
    }
  }

  [Serializable]
  public class PlayerTransferInfo
  {
    public int weaponIndex = -1;
    public CharaInfo.EquipItem weaponData;
    public int hp;
    public int healHp;
    public int rescueCount;
    public int autoReviveCount;
    public bool isUseInvincibleBuff;
    public bool isUseInvincibleBadStatusBuff;
    public bool isInitDead;
    public float initRescueTime;
    public float initContinueTime;
    public List<float> useGaugeCounterList;
    public BuffParam.BuffSyncParam buffSyncParam;
    public List<int> abilityCounterAttackNumList;
    public List<int> cleaveComboNumList;
    public float[] spActionGauges;
    public float[] evolveGauges;
    public int[] burstCurrentRestBulletCount;
    public int maxBulletCount;
    public Player.ShieldReflectInfo shieldReflectInfo;
    public int[] oracleSpearStockedCount;

    public override string ToString()
    {
      string str = "";
      str += (string) (object) this.weaponIndex;
      if (this.weaponData != null)
        str = $"{str},w={(object) this.weaponData}";
      str = $"{str},{(object) this.hp}";
      str = $"{str},{(object) this.healHp}";
      if (this.useGaugeCounterList != null)
      {
        str += ",g[";
        this.useGaugeCounterList.ForEach((Action<float>) (u => str = $"{str}{(object) u},"));
        str += "]";
      }
      str = $"{str},{(object) this.buffSyncParam}";
      if (this.spActionGauges != null)
      {
        str += ",sp[";
        for (int index = 0; index < this.spActionGauges.Length; ++index)
        {
          str += (string) (object) this.spActionGauges[index];
          str += ",";
        }
        str += "]";
      }
      if (this.evolveGauges != null)
      {
        str += ",ev[";
        for (int index = 0; index < this.evolveGauges.Length; ++index)
        {
          str += (string) (object) this.evolveGauges[index];
          str += ",";
        }
        str += "]";
      }
      return base.ToString() + str;
    }
  }

  public interface IDetachedNotify
  {
    void OnDetachedObject(StageObject stage_object);
  }

  public class WaveTargetLine
  {
    public Transform rendererTransform;
    public Vector3 fromPosition;
    public int waveNo;
    public int popCount;
    public bool isActive;

    public WaveTargetLine(Transform trans, Vector3 fromPosition, int waveNo, int popCount)
    {
      this.rendererTransform = trans;
      this.fromPosition = fromPosition;
      this.waveNo = waveNo;
      this.popCount = popCount;
      this.isActive = true;
    }
  }
}

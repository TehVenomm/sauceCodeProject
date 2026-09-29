// Decompiled with JetBrains decompiler
// Type: CoopMyClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CoopMyClient : CoopClient
{
  public CoopClientPacketSender packetSender { get; private set; }

  protected override void Awake()
  {
    this.packetSender = ((Component) this).gameObject.AddComponent<CoopClientPacketSender>();
    base.Awake();
  }

  public override string ToString()
  {
    return $"CoopMyClient[{(object) this.slotIndex}]({(object) this.status}/{this.isPartyOwner.ToString()}/{this.isStageHost.ToString()}).userId={(object) this.userId}";
  }

  public override void Init(int client_id)
  {
    base.Init(client_id);
    this.joinType = MonoBehaviourSingleton<InGameManager>.I.currentJoinType;
  }

  public override string GetPlayerName()
  {
    return MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null ? MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name : base.GetPlayerName();
  }

  protected override void SetStatus(CoopClient.CLIENT_STATUS st)
  {
    base.SetStatus(st);
    this.packetSender.SendClientStatus();
  }

  public override void SetLoadingPer(int per)
  {
    base.SetLoadingPer(per);
    this.packetSender.SendClientLoadingProgress();
  }

  public override void OnRoomLeaved()
  {
    this.Logd("OnRoomLeaved. {0}/{1}", (object) FieldManager.IsValidInGameNoQuest(), (object) MonoBehaviourSingleton<InGameProgress>.IsValid());
    this.isLeave = true;
    if (InGameManager.IsReentry())
    {
      if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
        return;
      if (this._CheckWaveMatchRetire())
        MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
      else
        MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
    }
    else
    {
      if (this.isBattleRetire || !this.IsStageStart() || MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay || MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
        return;
      UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 100U), false);
      MonoBehaviourSingleton<GoWrapManager>.I.trackBattleDisconnect();
    }
  }

  private bool _CheckWaveMatchRetire()
  {
    if (!QuestManager.IsValidInGameWaveMatch())
      return false;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.playerList.IsNullOrEmpty<StageObject>())
      return true;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index < count; ++index)
    {
      Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && !Object.op_Equality((Object) player, (Object) MonoBehaviourSingleton<StageObjectManager>.I.self) && !player.isNpc)
        return false;
    }
    return true;
  }

  public void WelcomeClient(int clientId)
  {
    this.Logd("WelcomeClient. clientId={0}", (object) clientId);
    this.packetSender.SendClientStatus(clientId);
  }

  public void ChangeEquip() => this.packetSender.SendClientChangeEquip();

  public void BattleRetire()
  {
    this.Logd("BattleRetire.");
    this.isBattleRetire = true;
    this.packetSender.SendClientBattleRetire();
  }

  public void StageStart() => this.SetStatus(CoopClient.CLIENT_STATUS.STAGE_START);

  public void LoadingStart() => this.SetStatus(CoopClient.CLIENT_STATUS.LOADING_START);

  public void LoadingFinish() => this.SetStatus(CoopClient.CLIENT_STATUS.LOADING_FINISH);

  public void StageRequest() => this.SetStatus(CoopClient.CLIENT_STATUS.STAGE_REQUEST);

  public void StartBattle() => this.SetStatus(CoopClient.CLIENT_STATUS.BATTLE_START);

  public void EndBattle() => this.SetStatus(CoopClient.CLIENT_STATUS.BATTLE_END);

  public void SeriesProgress(int endPhase)
  {
    this.isSeriesProgressEnd = true;
    this.packetSender.SendClientSeriesProgress(endPhase);
  }
}

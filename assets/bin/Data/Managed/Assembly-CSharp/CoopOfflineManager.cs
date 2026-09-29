// Decompiled with JetBrains decompiler
// Type: CoopOfflineManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopOfflineManager : MonoBehaviourSingleton<CoopOfflineManager>
{
  private CoopLocalServerSocket svSocket;
  private CoopNetworkPacketReceiver packetReceiver;
  private uint mapId;
  private List<CoopOfflineManager.EnemyPopParam> enemyPopParams;
  private int nowEnemyId = 500000;

  public bool isActivate { get; private set; }

  public static bool IsValidActivate()
  {
    return MonoBehaviourSingleton<CoopOfflineManager>.IsValid() && MonoBehaviourSingleton<CoopOfflineManager>.I.isActivate;
  }

  protected override void Awake()
  {
    base.Awake();
    this.isActivate = false;
    this.svSocket = new CoopLocalServerSocket();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<CoopNetworkPacketReceiver>();
  }

  private void Update()
  {
    if (!this.isActivate)
      return;
    this.svSocket.Update();
    this.packetReceiver.OnUpdate();
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  public void Clear()
  {
    this.isActivate = false;
    this.mapId = 0U;
    this.enemyPopParams = (List<CoopOfflineManager.EnemyPopParam>) null;
    this.packetReceiver.EraseAllPackets();
    this.Logd(nameof (Clear));
  }

  public void Deactivate()
  {
    this.isActivate = false;
    this.packetReceiver.EraseAllPackets();
    this.Logd("Deactivate.");
  }

  public void Activate()
  {
    if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
    {
      this.Logd("Activate failed with online.");
    }
    else
    {
      if (this.isActivate)
        return;
      this.isActivate = true;
      this.packetReceiver.EraseAllPackets();
      this.Logd("Activate.");
      if (this.mapId > 0U)
        this.svSocket.InitStage(this.mapId, this.enemyPopParams, this.nowEnemyId);
      if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.isActivateStart)
        return;
      MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageRequest();
    }
  }

  public void OnStageActivate()
  {
    this.mapId = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
    this.InitEnemyPopParam(this.mapId);
    this.Logd("OnStageActivate.");
    if (!this.isActivate)
      return;
    this.svSocket.InitStage(this.mapId, this.enemyPopParams, this.nowEnemyId);
  }

  public void OnStageChangeInterval()
  {
    this.mapId = 0U;
    this.enemyPopParams = (List<CoopOfflineManager.EnemyPopParam>) null;
    this.svSocket.Clear();
    this.packetReceiver.EraseAllPackets();
    this.Logd("OnStageChangeInterval.");
  }

  public void OnQuestSeriesInterval()
  {
    this.mapId = 0U;
    this.enemyPopParams = (List<CoopOfflineManager.EnemyPopParam>) null;
    this.svSocket.Clear();
    this.packetReceiver.EraseAllPackets();
    this.Logd("OnQuestSeriesInterval.");
  }

  public int Send<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null) where T : Coop_Model_Base
  {
    if (!this.isActivate)
      return -1;
    this.Logd("Recv. {0}", (object) model);
    Coop_Model_ACK coopModelAck = this.svSocket.Recv((Coop_Model_Base) model);
    if (onReceiveAck != null)
    {
      int num = onReceiveAck(coopModelAck) ? 1 : 0;
    }
    return 0;
  }

  public void Recv(CoopPacket packet)
  {
    if (!this.isActivate)
      return;
    this.Logd("Send. {0}", (object) packet);
    this.packetReceiver.Set(packet);
    this.packetReceiver.OnUpdate();
  }

  private void InitEnemyPopParam(uint map_id)
  {
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(map_id);
    if (enemyPopList == null || enemyPopList.Count <= 0)
      return;
    this.nowEnemyId = 500000;
    this.enemyPopParams = new List<CoopOfflineManager.EnemyPopParam>();
    int index = 0;
    for (int count = enemyPopList.Count; index < count; ++index)
      this.enemyPopParams.Insert(index, new CoopOfflineManager.EnemyPopParam()
      {
        data = enemyPopList[index]
      });
  }

  public CoopOfflineManager.EnemyPopParam GetEnemyPopParam(int idx)
  {
    if (this.enemyPopParams == null)
      return (CoopOfflineManager.EnemyPopParam) null;
    return idx >= this.enemyPopParams.Count ? (CoopOfflineManager.EnemyPopParam) null : this.enemyPopParams[idx];
  }

  public void OnEnemyPop(int idx, int sid)
  {
    CoopOfflineManager.EnemyPopParam enemyPopParam = this.GetEnemyPopParam(idx);
    if (enemyPopParam == null)
      return;
    ++enemyPopParam.count;
    this.Logd("OnEnemyPop. idx={0},sid={1},count={2}", (object) idx, (object) sid, (object) enemyPopParam.count);
    if (sid <= this.nowEnemyId)
      return;
    this.nowEnemyId = sid;
  }

  public void EnemyPopForSeriesArena(int index) => this.svSocket.SendEnemyPopForSeriesArena(index);

  public class EnemyPopParam
  {
    public FieldMapTable.EnemyPopTableData data;
    public int count;
  }
}

// Decompiled with JetBrains decompiler
// Type: CoopClientPacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CoopClientPacketSender : MonoBehaviour
{
  private CoopClient coopClient { get; set; }

  protected virtual void Awake()
  {
    this.coopClient = ((Component) this).gameObject.GetComponent<CoopClient>();
  }

  protected virtual void Start()
  {
  }

  private int Send<T>(
    T model,
    bool promise = true,
    int to_client_id = 0,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return to_client_id == 0 ? MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast<T>(model, promise, onReceiveAck, onPreResend) : MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<T>(to_client_id, model, promise, onReceiveAck, onPreResend);
  }

  public void SendClientStatus(int to_client_id = 0)
  {
    Coop_Model_ClientStatus model = new Coop_Model_ClientStatus();
    model.id = 1003;
    model.status = (int) this.coopClient.status;
    model.joinType = (int) this.coopClient.joinType;
    this.Send<Coop_Model_ClientStatus>(model, to_client_id: to_client_id, onPreResend: (Func<Coop_Model_Base, bool>) (send_model => (CoopClient.CLIENT_STATUS) model.status == this.coopClient.status));
  }

  public void SendClientBecameHost(int to_client_id = 0)
  {
    Coop_Model_ClientBecameHost model = new Coop_Model_ClientBecameHost();
    model.id = 1003;
    this.Send<Coop_Model_ClientBecameHost>(model, to_client_id: to_client_id);
  }

  public void SendClientLoadingProgress()
  {
    Coop_Model_ClientLoadingProgress model = new Coop_Model_ClientLoadingProgress();
    model.id = 1003;
    model.per = this.coopClient.loadingPer;
    this.Send<Coop_Model_ClientLoadingProgress>(model, false);
  }

  public void SendClientChangeEquip()
  {
    Coop_Model_ClientChangeEquip model = new Coop_Model_ClientChangeEquip();
    model.id = 1003;
    model.userInfo = this.coopClient.userInfo;
    this.Send<Coop_Model_ClientChangeEquip>(model);
  }

  public void SendClientBattleRetire()
  {
    Coop_Model_ClientBattleRetire model = new Coop_Model_ClientBattleRetire();
    model.id = 1003;
    this.Send<Coop_Model_ClientBattleRetire>(model);
  }

  public void SendClientSeriesProgress(int endPhase)
  {
    Coop_Model_ClientSeriesProgress model = new Coop_Model_ClientSeriesProgress();
    model.id = 1003;
    model.ep = endPhase;
    this.Send<Coop_Model_ClientSeriesProgress>(model);
  }
}

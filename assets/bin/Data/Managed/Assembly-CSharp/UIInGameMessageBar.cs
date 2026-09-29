// Decompiled with JetBrains decompiler
// Type: UIInGameMessageBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIInGameMessageBar : MonoBehaviourSingleton<UIInGameMessageBar>
{
  [SerializeField]
  protected Transform tweenCtrl;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected UILabel nameLabel;
  [SerializeField]
  protected UILabel messeageLabel;
  [SerializeField]
  protected float dispTime = 2f;
  [SerializeField]
  private UITexture texStamp;
  [SerializeField]
  private UIWidget anchor;
  private bool isOpen;
  private bool isLock;
  private float lockTimer;
  private List<UIInGameMessageBar.AnnounceInfo> announceQueue = new List<UIInGameMessageBar.AnnounceInfo>();
  private List<UIInGameMessageBar.AnnounceInfo> announceStock = new List<UIInGameMessageBar.AnnounceInfo>();
  private IEnumerator coroutineLoadStamp;

  protected override void Awake()
  {
    base.Awake();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  protected override void _OnDestroy()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void _AnnounceStart()
  {
    UITweenCtrl.Reset(this.tweenCtrl);
    UITweenCtrl.Play(this.tweenCtrl, callback: (EventDelegate.Callback) (() => this.isLock = true), is_input_block: false);
    this.lockTimer = this.dispTime;
    this.isOpen = true;
  }

  private void _Announce(string name, string messeage, int stamp_id)
  {
    if (this.isOpen || this.announceQueue.Count > 0)
    {
      UIInGameMessageBar.AnnounceInfo announceInfo;
      if (this.announceStock.Count > 0)
      {
        announceInfo = this.announceStock[0];
        this.announceStock.RemoveAt(0);
      }
      else
        announceInfo = new UIInGameMessageBar.AnnounceInfo();
      announceInfo.name = name;
      announceInfo.messeage = messeage;
      announceInfo.stampId = stamp_id;
      this.announceQueue.Add(announceInfo);
    }
    else
    {
      if (stamp_id != 0)
        this._AnnounceStamp(name, stamp_id);
      else
        this._AnnounceMesseage(name, messeage);
      this.panelChange.UnLock();
    }
  }

  private void _AnnounceMesseage(string name, string messeage)
  {
    this.nameLabel.text = name;
    ((Component) this.messeageLabel).gameObject.SetActive(true);
    this.messeageLabel.text = messeage;
    ((Component) this.texStamp).gameObject.SetActive(false);
    this._AnnounceStart();
  }

  private void _AnnounceStamp(string name, int stamp_id)
  {
    this.coroutineLoadStamp = this.CoroutineLoadStamp(name, stamp_id);
    this.StartCoroutine(this.coroutineLoadStamp);
  }

  private IEnumerator CoroutineLoadStamp(string name, int stampId)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_stamp = loadingQueue.LoadChatStamp(stampId);
    yield return (object) loadingQueue.Wait();
    if (!Object.op_Equality(lo_stamp.loadedObject, (Object) null))
    {
      Texture2D loadedObject = lo_stamp.loadedObject as Texture2D;
      ((Component) this.texStamp).gameObject.SetActive(true);
      this.texStamp.mainTexture = (Texture) loadedObject;
      this.nameLabel.text = name;
      ((Component) this.messeageLabel).gameObject.SetActive(false);
      this.coroutineLoadStamp = (IEnumerator) null;
      this._AnnounceStart();
    }
  }

  public void Announce(string name, string messeage)
  {
    this._Announce(name, messeage, 0);
    this.panelChange.UnLock();
  }

  public void Announce(string name, int stamp_id)
  {
    this._Announce(name, (string) null, stamp_id);
    this.panelChange.UnLock();
  }

  private void LateUpdate()
  {
    if (this.coroutineLoadStamp != null || !this.isLock)
      return;
    this.lockTimer -= Time.deltaTime;
    if ((double) this.lockTimer > 0.0)
      return;
    if (this.isOpen)
    {
      if (!this.NextAnnounce())
      {
        this.isOpen = false;
        UITweenCtrl.Play(this.tweenCtrl, false, (EventDelegate.Callback) (() => this.isLock = true), false);
        this.lockTimer = 0.1f;
      }
    }
    else if (!this.NextAnnounce())
      this.panelChange.Lock();
    this.isLock = false;
  }

  private bool NextAnnounce()
  {
    if (this.announceQueue.Count <= 0)
      return false;
    if (this.announceQueue[0].stampId != 0)
      this._AnnounceStamp(this.announceQueue[0].name, this.announceQueue[0].stampId);
    else
      this._AnnounceMesseage(this.announceQueue[0].name, this.announceQueue[0].messeage);
    this.announceStock.Add(this.announceQueue[0]);
    this.announceQueue.RemoveAt(0);
    return true;
  }

  private void OnScreenRotate(bool isPortrait) => this.anchor.UpdateAnchors();

  public class AnnounceInfo
  {
    public string name;
    public string messeage;
    public int stampId;
  }
}

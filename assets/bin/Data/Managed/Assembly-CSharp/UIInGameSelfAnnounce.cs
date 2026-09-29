// Decompiled with JetBrains decompiler
// Type: UIInGameSelfAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIInGameSelfAnnounce : MonoBehaviour
{
  [SerializeField]
  protected UITweenCtrl tweenCtrl;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected float lockInterval;
  [SerializeField]
  protected List<GameObject> visibleTargets;
  private bool isUnLock;
  private bool isLockReq;
  private float lockTimer;

  private void Awake() => this.SetVisible(false);

  public void Play(System.Action callback = null)
  {
    if (Object.op_Equality((Object) this.tweenCtrl, (Object) null))
      return;
    this.SetVisible(true);
    this.tweenCtrl.Reset();
    this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
    {
      this.isLockReq = true;
      this.lockTimer = this.lockInterval;
      if (callback != null)
        callback();
      this.SetVisible(false);
    }));
    if (!this.isUnLock)
    {
      this.panelChange.UnLock();
      this.isUnLock = true;
    }
    this.isLockReq = false;
  }

  public void Skip()
  {
    this.isLockReq = true;
    this.lockTimer = this.lockInterval;
    this.tweenCtrl.Skip();
  }

  private void LateUpdate()
  {
    if (!this.isLockReq)
      return;
    this.lockTimer -= Time.deltaTime;
    if ((double) this.lockTimer > 0.0)
      return;
    this.panelChange.Lock();
    this.isLockReq = false;
    this.isUnLock = false;
  }

  private void SetVisible(bool isVisible)
  {
    if (this.visibleTargets == null || this.visibleTargets.Count == 0)
      return;
    for (int index = 0; index < this.visibleTargets.Count; ++index)
      this.visibleTargets[index].SetActive(isVisible);
  }
}

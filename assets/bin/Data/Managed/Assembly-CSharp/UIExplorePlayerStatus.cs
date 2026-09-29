// Decompiled with JetBrains decompiler
// Type: UIExplorePlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class UIExplorePlayerStatus : MonoBehaviour
{
  [SerializeField]
  private UILabel nameLabel;
  [SerializeField]
  private UIHGauge hpGauge;
  [SerializeField]
  private UISprite weaponIcon;
  [SerializeField]
  private UIStatusIcon statusIcon;
  [SerializeField]
  private float statusIconRotationInterval = 0.78f;
  private float nextStatusIconRotate;
  private int checkStatusType;
  private ExplorePlayerStatus playerStatus;
  private IEnumerator rotateUpdate;

  public void Initialize(ExplorePlayerStatus playerStatus)
  {
    if (this.playerStatus != playerStatus)
    {
      this.Clear();
      this.playerStatus = playerStatus;
      playerStatus.onUpdateHp += new System.Action(this.UpdateHp);
      playerStatus.onUpdateWeapon += new System.Action(this.UpdateWeapon);
      playerStatus.onUpdateBuff += new System.Action(this.UpdateStatusIcon);
    }
    if (playerStatus.isInitialized)
    {
      this.OnInitializeStatus();
    }
    else
    {
      playerStatus.onInitialize += new System.Action(this.OnInitializeStatus);
      ((Component) this).gameObject.SetActive(false);
    }
  }

  private void Clear()
  {
    if (this.playerStatus == null)
      return;
    this.playerStatus.onInitialize -= new System.Action(this.OnInitializeStatus);
    this.playerStatus.onUpdateHp -= new System.Action(this.UpdateHp);
    this.playerStatus.onUpdateWeapon -= new System.Action(this.UpdateWeapon);
    this.playerStatus.onUpdateBuff -= new System.Action(this.UpdateStatusIcon);
    this.playerStatus = (ExplorePlayerStatus) null;
    if (this.rotateUpdate == null)
      return;
    this.StopCoroutine(this.rotateUpdate);
    this.rotateUpdate = (IEnumerator) null;
  }

  private void OnInitializeStatus()
  {
    ((Component) this).gameObject.SetActive(true);
    this.SetName();
    this.UpdateHp();
    this.UpdateWeapon();
    this.UpdateStatusIcon();
  }

  private void OnDestroy() => this.Clear();

  private void SetName() => this.nameLabel.text = this.playerStatus.userName;

  private void UpdateHp()
  {
    this.hpGauge.SetPercent((float) this.playerStatus.hp / (float) this.playerStatus.hpMax);
  }

  private void UpdateWeapon()
  {
    this.weaponIcon.spriteName = UIWeaponChange.WEAPONICON_PATH[(int) this.playerStatus.weaponType];
  }

  private void UpdateStatusIcon()
  {
    if (this.statusIcon.HasActiveMultipleBuffIcon(this.playerStatus.buff, this.playerStatus.extraStatus))
    {
      if (this.rotateUpdate != null)
        return;
      this.rotateUpdate = this.RotateUpdateStatusIcon();
      this.StartCoroutine(this.rotateUpdate);
    }
    else
    {
      if (this.rotateUpdate != null)
      {
        this.StopCoroutine(this.rotateUpdate);
        this.rotateUpdate = (IEnumerator) null;
      }
      this.checkStatusType = 0;
      this.statusIcon.RotatedUpdateStatusIcon(this.checkStatusType, this.playerStatus.buff, this.playerStatus.extraStatus);
    }
  }

  private IEnumerator RotateUpdateStatusIcon()
  {
    while (true)
    {
      this.nextStatusIconRotate -= Time.deltaTime;
      if ((double) this.nextStatusIconRotate <= 0.0)
      {
        this.checkStatusType = this.statusIcon.RotatedUpdateStatusIcon(this.checkStatusType, this.playerStatus.buff, this.playerStatus.extraStatus);
        ++this.checkStatusType;
        this.nextStatusIconRotate = this.statusIconRotationInterval;
      }
      yield return (object) null;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: UIInGamePopBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIInGamePopBase : MonoBehaviour
{
  [SerializeField]
  protected UITweenCtrl tweenCtrl;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected UIButton button;
  [SerializeField]
  protected UISprite buttonSprite;
  [SerializeField]
  protected string[] buttonSpriteName;
  [SerializeField]
  protected GameObject[] icons;
  protected bool isPopMenu;
  private bool isUnLock;
  private bool isLockReq;
  private float lockTimer;

  protected virtual void Awake()
  {
    if (this.isPopMenu)
    {
      if (Object.op_Inequality((Object) this.button, (Object) null))
        this.button.normalSprite = this.buttonSpriteName[0];
      else if (Object.op_Inequality((Object) this.buttonSprite, (Object) null))
        this.buttonSprite.spriteName = this.buttonSpriteName[0];
      this.icons[0].SetActive(true);
      this.icons[1].SetActive(false);
    }
    else
    {
      if (Object.op_Inequality((Object) this.button, (Object) null))
        this.button.normalSprite = this.buttonSpriteName[1];
      else if (Object.op_Inequality((Object) this.buttonSprite, (Object) null))
        this.buttonSprite.spriteName = this.buttonSpriteName[1];
      this.icons[0].SetActive(false);
      this.icons[1].SetActive(true);
    }
  }

  public virtual void OnClickPopMenu()
  {
    this.tweenCtrl.Reset();
    this.tweenCtrl.Skip(this.isPopMenu);
    this.isPopMenu = !this.isPopMenu;
    this.tweenCtrl.Play(this.isPopMenu, (EventDelegate.Callback) (() =>
    {
      this.isLockReq = true;
      this.lockTimer = 0.1f;
    }));
    if (!this.isUnLock)
    {
      this.panelChange.UnLock();
      this.isUnLock = true;
    }
    this.isLockReq = false;
    if (this.isPopMenu)
    {
      if (Object.op_Inequality((Object) this.button, (Object) null))
        this.button.normalSprite = this.buttonSpriteName[0];
      else if (Object.op_Inequality((Object) this.buttonSprite, (Object) null))
        this.buttonSprite.spriteName = this.buttonSpriteName[0];
      this.icons[0].SetActive(true);
      this.icons[1].SetActive(false);
      SoundManager.PlaySystemSE(SoundID.UISE.MENU_OPEN);
    }
    else
    {
      if (Object.op_Inequality((Object) this.button, (Object) null))
        this.button.normalSprite = this.buttonSpriteName[1];
      else if (Object.op_Inequality((Object) this.buttonSprite, (Object) null))
        this.buttonSprite.spriteName = this.buttonSpriteName[1];
      this.icons[0].SetActive(false);
      this.icons[1].SetActive(true);
      SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
    }
  }

  protected virtual void LateUpdate()
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
}

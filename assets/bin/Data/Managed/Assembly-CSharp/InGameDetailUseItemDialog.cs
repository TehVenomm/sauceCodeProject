// Decompiled with JetBrains decompiler
// Type: InGameDetailUseItemDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class InGameDetailUseItemDialog : ItemDetailUseItemDialog
{
  private bool isInActiveRotate;

  public override void Initialize()
  {
    base.Initialize();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.isInActiveRotate = true;
  }

  public override void Exit()
  {
    base.Exit();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  public override void UpdateUI()
  {
    if (this.isInActiveRotate && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      this.Reposition(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.isInActiveRotate = false;
    base.UpdateUI();
  }

  private void Reposition(bool isPortrait)
  {
    ((Component) this.GetCtrl((Enum) ItemDetailUseItem.UI.OBJ_BACK)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    ((Component) this.GetCtrl((Enum) ItemDetailUseItem.UI.BTN_USE)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    this.UpdateAnchors();
  }

  private void OnScreenRotate(bool isPortrait)
  {
    this.isInActiveRotate = !Object.op_Inequality((Object) this.transferUI, (Object) null) ? !((Component) this.collectUI).gameObject.activeInHierarchy : !((Component) this.transferUI).gameObject.activeInHierarchy;
    if (this.isInActiveRotate)
      return;
    this.Reposition(isPortrait);
  }

  protected void OnQuery_InGameDetailUseConfirm_YES() => this.SendUseItem();

  protected void OnQuery_InGameDetailUseOverWriteConfirm_YES() => this.SendUseItem();
}

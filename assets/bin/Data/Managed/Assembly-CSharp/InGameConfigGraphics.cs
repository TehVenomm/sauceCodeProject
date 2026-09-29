// Decompiled with JetBrains decompiler
// Type: InGameConfigGraphics
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameConfigGraphics : ConfigGraphics
{
  private bool isInActiveRotate;

  public override void Initialize()
  {
    base.Initialize();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.isInActiveRotate = true;
    ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.DSV_ROOT)).GetComponent<Collider>().enabled = !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait;
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
    ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.SPR_BG_FRAME)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.SCR_ROOT)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    this.UpdateAnchors();
    ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.SCR_ROOT)).GetComponent<UIScrollView>().ResetPosition();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() =>
    {
      this.RefreshUI();
      ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.SCR_ROOT)).GetComponent<UIPanel>().Refresh();
    });
    ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.DSV_ROOT)).GetComponent<Collider>().enabled = !isPortrait;
  }

  private void OnScreenRotate(bool isPortrait)
  {
    this.isInActiveRotate = !Object.op_Inequality((Object) this.transferUI, (Object) null) ? !((Component) this.collectUI).gameObject.activeInHierarchy : !((Component) this.transferUI).gameObject.activeInHierarchy;
    if (this.isInActiveRotate)
      return;
    this.Reposition(isPortrait);
  }

  protected override void OnQuery_DRAW_HEAD_NAME_OFF()
  {
    base.OnQuery_DRAW_HEAD_NAME_OFF();
    List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
    int index = 0;
    for (int count = enemyList.Count; index < count; ++index)
      (enemyList[index] as Enemy).DeleteStatusGizmo();
  }

  protected override void OnQuery_AUTO_ROTATION_ON()
  {
    base.OnQuery_AUTO_ROTATION_ON();
    GameSceneGlobalSettings.SetOrientation(true);
  }

  protected override void OnQuery_AUTO_ROTATION_OFF()
  {
    base.OnQuery_AUTO_ROTATION_OFF();
    GameSceneGlobalSettings.SetOrientation(true);
  }

  protected override void OnQuery_MINIMAP_ENEMY_ON()
  {
    base.OnQuery_MINIMAP_ENEMY_ON();
    this.RefreshEnemyMiniMap(true);
  }

  protected override void OnQuery_MINIMAP_ENEMY_OFF()
  {
    base.OnQuery_MINIMAP_ENEMY_OFF();
    this.RefreshEnemyMiniMap(false);
  }

  private void RefreshEnemyMiniMap(bool is_enable)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<MiniMap>.IsValid())
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.enemyList.ForEach((Action<StageObject>) (o =>
    {
      Enemy root_object = o as Enemy;
      if (is_enable)
        MonoBehaviourSingleton<MiniMap>.I.Attach((MonoBehaviour) root_object);
      else
        MonoBehaviourSingleton<MiniMap>.I.Detach((MonoBehaviour) root_object);
    }));
  }
}

// Decompiled with JetBrains decompiler
// Type: ConfigGraphics
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ConfigGraphics : GameSection
{
  public override void Initialize()
  {
    base.Initialize();
    ((Component) this.GetCtrl((Enum) ConfigGraphics.UI.DSV_ROOT)).GetComponent<Collider>().enabled = false;
  }

  public override void UpdateUI()
  {
    int graphicOptionType = InGameManager.GetGraphicOptionType(GameSaveData.instance.graphicOptionKey);
    int arrowCameraType = InGameManager.GetArrowCameraType(GameSaveData.instance.arrowCameraKey);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_DRAW_LOW, graphicOptionType == 0);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_DRAW_STANDARD, graphicOptionType == 1);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_DRAW_HIGH, graphicOptionType == 2);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_DRAW_HIGHEST, graphicOptionType == 3);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_HEAD_NAME_ON, GameSaveData.instance.headName);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_HEAD_NAME_OFF, !GameSaveData.instance.headName);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_AUTO_ROTATION_ON, GameSaveData.instance.enableLandscape);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_AUTO_ROTATION_OFF, !GameSaveData.instance.enableLandscape);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_MINIMAP_ENEMY_ON, GameSaveData.instance.enableMinimapEnemy);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_MINIMAP_ENEMY_OFF, !GameSaveData.instance.enableMinimapEnemy);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_ARROW_CAMERA_A, arrowCameraType == 0);
    this.SetToggle((Enum) ConfigGraphics.UI.TGL_ARROW_CAMERA_B, arrowCameraType == 1);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_DRAW_LOW, graphicOptionType != 0);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_DRAW_STANDARD, graphicOptionType != 1);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_DRAW_HIGH, graphicOptionType != 2);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_DRAW_HIGHEST, graphicOptionType != 3);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_HEAD_NAME_ON, !GameSaveData.instance.headName);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_HEAD_NAME_OFF, GameSaveData.instance.headName);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_AUTO_ROTATION_ON, !GameSaveData.instance.enableLandscape);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_AUTO_ROTATION_OFF, GameSaveData.instance.enableLandscape);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_MINIMAP_ENEMY_ON, !GameSaveData.instance.enableMinimapEnemy);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_MINIMAP_ENEMY_OFF, GameSaveData.instance.enableMinimapEnemy);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_ARROW_CAMERA_A, arrowCameraType != 0);
    this.SetButtonEnabled((Enum) ConfigGraphics.UI.BTN_ARROW_CAMERA_B, arrowCameraType != 1);
    base.UpdateUI();
  }

  private void OnQuery_DRAW_LOW()
  {
    GameSaveData.instance.graphicOptionKey = "low";
    this.RefreshUI();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
    Application.targetFrameRate = 30;
  }

  private void OnQuery_DRAW_STANDARD()
  {
    GameSaveData.instance.graphicOptionKey = "standard";
    this.RefreshUI();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
    Application.targetFrameRate = 30;
  }

  private void OnQuery_DRAW_HIGH()
  {
    GameSaveData.instance.graphicOptionKey = "high";
    this.RefreshUI();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
    Application.targetFrameRate = 30;
  }

  private void OnQuery_DRAW_HIGHEST()
  {
    GameSaveData.instance.graphicOptionKey = "highest";
    this.RefreshUI();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    MonoBehaviourSingleton<AppMain>.I.UpdateResolution(Screen.width < Screen.height);
    Application.targetFrameRate = 120;
    Time.fixedDeltaTime = 0.008333335f;
  }

  private void OnQuery_DRAW_HEAD_NAME_ON()
  {
    GameSaveData.instance.headName = true;
    this.RefreshUI();
  }

  protected virtual void OnQuery_DRAW_HEAD_NAME_OFF()
  {
    GameSaveData.instance.headName = false;
    this.RefreshUI();
  }

  protected virtual void OnQuery_AUTO_ROTATION_ON()
  {
    GameSaveData.instance.enableLandscape = true;
    this.RefreshUI();
  }

  protected virtual void OnQuery_AUTO_ROTATION_OFF()
  {
    GameSaveData.instance.enableLandscape = false;
    this.RefreshUI();
  }

  protected virtual void OnQuery_MINIMAP_ENEMY_ON()
  {
    GameSaveData.instance.enableMinimapEnemy = true;
    this.RefreshUI();
  }

  protected virtual void OnQuery_MINIMAP_ENEMY_OFF()
  {
    GameSaveData.instance.enableMinimapEnemy = false;
    this.RefreshUI();
  }

  private void OnQuery_ARROW_CAMERA_A()
  {
    GameSaveData.instance.arrowCameraKey = "typea";
    this.RefreshUI();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.SetArrowCameraMode(0);
  }

  private void OnQuery_ARROW_CAMERA_B()
  {
    GameSaveData.instance.arrowCameraKey = "typeb";
    this.RefreshUI();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.UpdateConfig();
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.SetArrowCameraMode(1);
  }

  protected enum UI
  {
    SPR_BG_FRAME,
    SCR_ROOT,
    DSV_ROOT,
    TGL_DRAW_LOW,
    TGL_DRAW_STANDARD,
    TGL_DRAW_HIGH,
    TGL_DRAW_HIGHEST,
    TGL_HEAD_NAME_ON,
    TGL_HEAD_NAME_OFF,
    TGL_AUTO_ROTATION_ON,
    TGL_AUTO_ROTATION_OFF,
    TGL_MINIMAP_ENEMY_ON,
    TGL_MINIMAP_ENEMY_OFF,
    TGL_ARROW_CAMERA_A,
    TGL_ARROW_CAMERA_B,
    BTN_DRAW_LOW,
    BTN_DRAW_STANDARD,
    BTN_DRAW_HIGH,
    BTN_DRAW_HIGHEST,
    BTN_HEAD_NAME_ON,
    BTN_HEAD_NAME_OFF,
    BTN_AUTO_ROTATION_ON,
    BTN_AUTO_ROTATION_OFF,
    BTN_MINIMAP_ENEMY_ON,
    BTN_MINIMAP_ENEMY_OFF,
    BTN_ARROW_CAMERA_A,
    BTN_ARROW_CAMERA_B,
  }
}

// Decompiled with JetBrains decompiler
// Type: StatusScreenShot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StatusScreenShot : GameSection
{
  private int filter;
  private PlayerLoader playerLoader;
  private UIEventListener eventListener;
  private bool isInitializeDegree;

  public override void Initialize()
  {
    base.Initialize();
    this.LoadFilterType();
    this.playerLoader = MonoBehaviourSingleton<StatusStageManager>.I.GetPlayerLoader();
    if (!Object.op_Equality((Object) this.eventListener, (Object) null))
      return;
    this.eventListener = ((Component) this.GetCtrl((Enum) StatusScreenShot.UI.OBJ_EVENTLISTENER)).GetComponent<UIEventListener>();
    this.eventListener.onDrag += new UIEventListener.VectorDelegate(this.OnDrag);
  }

  private void OnEnable()
  {
    if (!Object.op_Inequality((Object) this.eventListener, (Object) null))
      return;
    this.eventListener.onDrag += new UIEventListener.VectorDelegate(this.OnDrag);
  }

  private void OnDisable()
  {
    this.eventListener.onDrag -= new UIEventListener.VectorDelegate(this.OnDrag);
  }

  private void OnDrag(GameObject obj, Vector2 move)
  {
    if (Object.op_Equality((Object) this.playerLoader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != nameof (StatusScreenShot))
      return;
    ((Component) this.playerLoader).transform.Rotate(GameDefine.GetCharaRotateVector(move));
  }

  private void LoadFilterType()
  {
    this.filter = GameSaveData.instance.ScreenShotUIFilterType;
    if (this.filter != -1)
      return;
    this.filter = 31 /*0x1F*/;
  }

  public override void UpdateUI()
  {
    this.CreateDegree();
    this.SetLabelText((Enum) StatusScreenShot.UI.LBL_LV_NOW, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level.ToString());
    this.SetLabelText((Enum) StatusScreenShot.UI.LBL_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
    this.SetLabelText((Enum) StatusScreenShot.UI.LBL_HOUND_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
    this.SetLabelText((Enum) StatusScreenShot.UI.LBL_COMMENT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment);
    this.SetEnableAll();
  }

  private void SetEnableAll()
  {
    this.SetActive((Enum) StatusScreenShot.UI.LBL_NAME, (this.filter & 1) == 1);
    this.SetActive((Enum) StatusScreenShot.UI.OBJ_LEVEL, (this.filter & 2) == 2);
    this.SetActive((Enum) StatusScreenShot.UI.OBJ_FRAME_1, (this.filter & 1) == 1 || (this.filter & 2) == 2);
    this.SetActive((Enum) StatusScreenShot.UI.OBJ_FRAME_2, (this.filter & 4) == 4);
    this.SetActive((Enum) StatusScreenShot.UI.SPR_COMMENT, (this.filter & 8) == 8);
    if (!this.isInitializeDegree)
      return;
    this.SetActive((Enum) StatusScreenShot.UI.OBJ_DEGREE_ROOT, (this.filter & 16 /*0x10*/) == 16 /*0x10*/);
  }

  private void OnQuery_VIEWER_SETTING() => GameSection.SetEventData((object) this.filter);

  private void OnQuery_BACK()
  {
  }

  private void OnCloseDialog_StatusScreenShotViewerSetting()
  {
    this.LoadFilterType();
    this.SetEnableAll();
  }

  private void CreateDegree()
  {
    ((Component) this.GetCtrl((Enum) StatusScreenShot.UI.OBJ_DEGREE_ROOT)).GetComponent<DegreePlate>().Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, false, (Action<DegreePlate>) (x =>
    {
      this.SetActive((Enum) StatusScreenShot.UI.OBJ_DEGREE_ROOT, (this.filter & 16 /*0x10*/) == 16 /*0x10*/);
      this.isInitializeDegree = true;
    }));
  }

  private enum UI
  {
    OBJ_DEGREE_ROOT,
    LBL_LV_NOW,
    LBL_NAME,
    LBL_HOUND_ID,
    LBL_COMMENT,
    OBJ_FRAME_1,
    OBJ_FRAME_2,
    SPR_COMMENT,
    OBJ_LEVEL,
    OBJ_EVENTLISTENER,
  }
}

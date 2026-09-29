// Decompiled with JetBrains decompiler
// Type: InGameQuestAreaDeliveryList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class InGameQuestAreaDeliveryList : QuestAreaDeliveryList
{
  private bool isInActiveRotate;

  protected override bool showStory => false;

  protected override bool showMap => false;

  public override void Initialize()
  {
    int eventData = (int) GameSection.GetEventData();
    this.regionData = Singleton<RegionTable>.I.GetData((uint) eventData);
    base.Initialize();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.isInActiveRotate = true;
  }

  protected override IEnumerator DoInitialize()
  {
    this.SetActive((Enum) QuestAreaDeliveryList.UI.OBJ_IMAGE, false);
    this.GetDeliveryList();
    this.EndInitialize();
    yield break;
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
    foreach (UIScreenRotationHandler component in ((Component) this.GetCtrl((Enum) QuestAreaDeliveryList.UI.OBJ_FRAME)).GetComponents<UIScreenRotationHandler>())
      component.InvokeRotate();
    ((Component) this.GetCtrl((Enum) QuestAreaDeliveryList.UI.SPR_BG_FRAME)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    ((Component) this.GetCtrl((Enum) QuestAreaDeliveryList.UI.SCR_DELIVERY_QUEST)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    this.UpdateAnchors();
    ((Component) this.GetCtrl((Enum) QuestAreaDeliveryList.UI.SCR_DELIVERY_QUEST)).GetComponent<UIScrollView>().ResetPosition();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() =>
    {
      this.RefreshUI();
      ((Component) this.GetCtrl((Enum) QuestAreaDeliveryList.UI.SCR_DELIVERY_QUEST)).GetComponent<UIPanel>().Refresh();
    });
  }

  private void OnScreenRotate(bool isPortrait)
  {
    this.isInActiveRotate = !Object.op_Inequality((Object) this.transferUI, (Object) null) ? !((Component) this.collectUI).gameObject.activeInHierarchy : !((Component) this.transferUI).gameObject.activeInHierarchy;
    if (this.isInActiveRotate)
      return;
    this.Reposition(isPortrait);
  }
}

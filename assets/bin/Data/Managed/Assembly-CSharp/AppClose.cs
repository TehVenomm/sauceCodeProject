// Decompiled with JetBrains decompiler
// Type: AppClose
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class AppClose : GameSection
{
  private const string EFFECT01_NAME = "ef_ui_title_01";
  private TutorialBossDirector director;
  private GameObject titleObjectRoot;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_01");
    LoadObject lo_director = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector");
    while (load_queue.IsLoading())
      yield return (object) null;
    Transform transform = ResourceUtility.Realizes(lo_director.loadedObject);
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      this.director = ((Component) transform).GetComponent<TutorialBossDirector>();
      if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyTitleTop)
      {
        DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
        this.director.logo.camera.orthographicSize = specialDeviceInfo.TitleTopCameraSize;
        this.director.logo.bg.transform.localScale = specialDeviceInfo.TitleTopBGScale;
      }
      this.director.StartLogoAnimation(false, (System.Action) null, (System.Action) (() => this.SetActiveUI(true)));
      ((Behaviour) ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>()).enabled = false;
    }
    else
      this.SetActiveUI(true);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetApplicationVersionText((Enum) AppClose.UI.LBL_APP_VERSION);
    this.SetLabelText((Enum) AppClose.UI.LBL_SERVICE_MESSAGE, MonoBehaviourSingleton<AccountManager>.I.closedNotice.Replace("<BR>", "\n").Replace("<br>", "\n"));
    this.SetActive((Enum) AppClose.UI.BTN_REPAYMENT, MonoBehaviourSingleton<AccountManager>.I.openRefundForm);
    base.UpdateUI();
  }

  private void SetActiveUI(bool enable)
  {
    ((Component) this.GetCtrl((Enum) AppClose.UI.Container)).gameObject.SetActive(enable);
  }

  private void OnQuery_SERVICE() => GameSection.SetEventData((object) "refund/top");

  private void OnQuery_REPAYMENT() => GameSection.SetEventData((object) "refund/form");

  private enum UI
  {
    LBL_APP_VERSION,
    TEX_BG,
    Container,
    BTN_SERVICE,
    BTN_REPAYMENT,
    LBL_SERVICE_MESSAGE,
  }
}

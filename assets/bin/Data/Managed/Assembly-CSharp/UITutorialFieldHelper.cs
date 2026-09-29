// Decompiled with JetBrains decompiler
// Type: UITutorialFieldHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UITutorialFieldHelper : MonoBehaviour
{
  private InGameMain sectionIngameMain;
  [SerializeField]
  private UITweenCtrl tweenStratCtrl;
  [SerializeField]
  private UITweenCtrl tweenEndCtrl;
  [SerializeField]
  private UIButton skilBtn;
  private static UITutorialFieldHelper instance;

  public static UITutorialFieldHelper.MessageState m_State { private set; get; }

  public bool IsLoading { private set; get; }

  public static UITutorialFieldHelper I => UITutorialFieldHelper.instance;

  public static bool IsValid()
  {
    return Object.op_Inequality((Object) UITutorialFieldHelper.instance, (Object) null);
  }

  public static bool IsCollectedFieldItem()
  {
    return Object.op_Inequality((Object) UITutorialFieldHelper.instance, (Object) null) && UITutorialFieldHelper.m_State == UITutorialFieldHelper.MessageState.BackHome;
  }

  private void Awake() => UITutorialFieldHelper.instance = this;

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit || !Object.op_Equality((Object) UITutorialFieldHelper.instance, (Object) this))
      return;
    UITutorialFieldHelper.instance = (UITutorialFieldHelper) null;
  }

  public void Setup(InGameMain ingame_main_section)
  {
    this.sectionIngameMain = ingame_main_section;
    switch (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep)
    {
      case 0:
      case 1:
      case 2:
      case 3:
        UITutorialFieldHelper.m_State = UITutorialFieldHelper.MessageState.CollectItem;
        break;
      case 4:
        UITutorialFieldHelper.m_State = UITutorialFieldHelper.MessageState.BackHome;
        break;
    }
    this.SetState(UITutorialFieldHelper.m_State);
  }

  private void UpdateMessage()
  {
    switch (UITutorialFieldHelper.m_State)
    {
      case UITutorialFieldHelper.MessageState.CollectItemImg:
      case UITutorialFieldHelper.MessageState.CollectItem:
        if (!UIInGameFieldMenu.IsValid())
          break;
        UIInGameFieldMenu.I.SetDisableButtons(true);
        ((Component) UIInGameFieldMenu.I).gameObject.SetActive(false);
        break;
      case UITutorialFieldHelper.MessageState.BackHome:
        if (!UIInGameFieldMenu.IsValid())
          break;
        ((Component) this.tweenEndCtrl).gameObject.SetActive(true);
        UIInGameFieldMenu.I.SetDisableButtons(true);
        ((Component) UIInGameFieldMenu.I).gameObject.SetActive(true);
        UIInGameFieldMenu.I.SetEnableButton("BTN_REQUEST", true);
        break;
    }
  }

  public void OpenTutorialFirstDelivery()
  {
    this.tweenStratCtrl.Reset();
    this.tweenEndCtrl.Reset();
    this.tweenStratCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
    {
      this.SetState(UITutorialFieldHelper.MessageState.CollectItemImg);
      this.tweenEndCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
      {
        ((Component) this.tweenStratCtrl).gameObject.SetActive(false);
        ((Component) this.tweenEndCtrl).gameObject.SetActive(false);
      }));
    }));
    this.skilBtn.onClick.Clear();
    this.skilBtn.onClick.Add(new EventDelegate((EventDelegate.Callback) (() => this.tweenStratCtrl.Skip())));
  }

  public void OnCollectItem()
  {
    this.SetState(UITutorialFieldHelper.MessageState.BackHome);
    this.sectionIngameMain.NoticeTutorialOnCollectItem();
  }

  private void SetState(UITutorialFieldHelper.MessageState state)
  {
    UITutorialFieldHelper.m_State = state;
    this.UpdateMessage();
  }

  public enum MessageState
  {
    None,
    Wait,
    CollectItemImg,
    CollectItem,
    BackHome,
    MenuPop,
  }
}
